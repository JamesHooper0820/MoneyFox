﻿namespace MoneyFox.Infrastructure.DbBackup
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.ApplicationCore.Domain.Exceptions;
    using Legacy;
    using Microsoft.Graph;
    using Microsoft.Identity.Client;
    using Serilog;

    internal sealed class OneDriveAuthenticationService : IOneDriveAuthenticationService
    {
        private const string ERROR_CODE_CANCELED = "authentication_canceled";

        private readonly string[] scopes = { "Files.ReadWrite" };

        private readonly IPublicClientApplication clientApp;
        private readonly IGraphClientFactory graphClientFactory;

        public OneDriveAuthenticationService(IPublicClientApplication clientApp, IGraphClientFactory graphClientFactory)
        {
            this.clientApp = clientApp;
            this.graphClientFactory = graphClientFactory;
        }

        public async Task<GraphServiceClient> CreateServiceClient(CancellationToken cancellationToken = default)
        {
            var accounts = await clientApp.GetAccountsAsync();
            AuthenticationResult? result;
            try
            {
                result = await clientApp.AcquireTokenSilent(scopes: scopes, account: accounts.FirstOrDefault()).ExecuteAsync(cancellationToken);
            }
            catch (MsalUiRequiredException ex)
            {
                Log.Information(exception: ex, messageTemplate: "Acquire Token Silent failed");
                try
                {
                    result = await clientApp.AcquireTokenInteractive(scopes)
                        .WithUseEmbeddedWebView(true)
                        .WithParentActivityOrWindow(ParentActivityWrapper.ParentActivity) // this is required for Android
                        .ExecuteAsync(cancellationToken);
                }
                catch (MsalException)
                {
                    throw new BackupAuthenticationFailedException();
                }
            }
            catch (MsalClientException ex)
            {
                if (ex.ErrorCode == ERROR_CODE_CANCELED)
                {
                    throw new BackupOperationCanceledException();
                }

                throw;
            }

            return result != null ? graphClientFactory.CreateClient(result) : throw new BackupAuthenticationFailedException();
        }

        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                List<IAccount> accounts = (await clientApp.GetAccountsAsync()).ToList();
                while (accounts.Any())
                {
                    await clientApp.RemoveAsync(accounts.FirstOrDefault());
                    accounts = (await clientApp.GetAccountsAsync()).ToList();
                }
            }
            catch (MsalClientException ex)
            {
                if (ex.ErrorCode == ERROR_CODE_CANCELED)
                {
                    throw new BackupOperationCanceledException();
                }

                throw;
            }
            catch (Exception ex)
            {
                throw new BackupAuthenticationFailedException(ex);
            }
        }

        private async Task<AuthenticationResult> AcquireInteractive()
        {
            return await clientApp.AcquireTokenInteractive(scopes)
                .WithUseEmbeddedWebView(true)
                .WithParentActivityOrWindow(ParentActivityWrapper.ParentActivity) // this is required for Android
                .ExecuteAsync();
        }
    }

}
