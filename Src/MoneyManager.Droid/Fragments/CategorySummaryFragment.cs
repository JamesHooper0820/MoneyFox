using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;

namespace MoneyManager.Droid.Fragments
{
    public class CategorySummaryFragment : MvxFragment
    {
        //public new StatisticViewModel ViewModel
        //{
        //    get { return (StatisticViewModel)base.ViewModel; }
        //    set { base.ViewModel = value; }
        //}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.CategorySummaryLayout, null);
        }
    }
}