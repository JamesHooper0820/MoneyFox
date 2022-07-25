﻿namespace MoneyFox.Core.Notifications.DatabaseChanged
{

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationCore.UseCases.BackupUpload;
    using Common.Facades;
    using MediatR;

    public static class DataBaseChanged
    {
        public sealed class Notification : INotification { }

        public class Handler : INotificationHandler<Notification>
        {
            private readonly ISettingsFacade settingsFacade;
            private readonly ISender sender;

            public Handler(ISender sender, ISettingsFacade settingsFacade)
            {
                this.sender = sender;
                this.settingsFacade = settingsFacade;
            }

            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                settingsFacade.LastDatabaseUpdate = DateTime.Now;
                if (settingsFacade.IsBackupAutoUploadEnabled && settingsFacade.IsLoggedInToBackupService)
                {
                    await sender.Send(request: new UploadBackup.Command(), cancellationToken: cancellationToken);
                }
            }
        }
    }

}