using Contoso.Sales.Entities;
using Contoso.Sales.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Contoso.Sales.Pages
{
    public partial class CheckQuota : ContentPage
    {
       
        public CheckQuota()
        {
            InitializeComponent();
            widgetTarget.EntryValue.IsEnabled = false;
            lblUpdationInfo.IsVisible = false;
            lblWelcome.Text = "Welcome " + AppConstants.GIVENNAME + " " + AppConstants.FAMILYNAME;
            lblUserName.Text = AppConstants.USER_NAME;
            btnGo.Clicked += BtnGo_Clicked;
            Task.Run(() =>
            {
                GetUserQuota();
            });
        }

        private async void BtnGo_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(widgetAchived.EntryValue.Text))
            {
                widgetAchived.Unfocus();
                lblUpdationInfo.IsVisible = false;
                btnGo.Text = "Processing...";
                btnGo.IsEnabled = false;
                UserHelper userHelper = new UserHelper();
                List<Profile> profiles = await userHelper.UpdateProfile(AppConstants.USER_NAME, widgetAchived.EntryValue.Text);
                if (profiles != null && profiles.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        RefreshData(profiles);
                        lblUpdationInfo.Text = "Data updated successfully";
                        lblUpdationInfo.IsVisible = true;
                        btnGo.Text = "Go";
                        btnGo.IsEnabled = true;
                    });
                }
            }
        }

        private async void GetUserQuota()
        {
            UserHelper userHelper = new UserHelper();
            List<Profile> profiles = await userHelper.GetProfile(AppConstants.USER_NAME);
            if (profiles != null && profiles.Count > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    RefreshData(profiles);
                });
            }
        }
        private void RefreshData(List<Profile> profile)
        {
            if (profile != null)
            {
                    widgetTarget.EntryValue.Text = profile[0].Quota;
                    widgetAchived.EntryValue.Text = profile[0].Actual;
            }
        }
    }
}
