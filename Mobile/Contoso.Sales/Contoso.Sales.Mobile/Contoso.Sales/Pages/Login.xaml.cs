using Contoso.Sales.Behaviors;
using Contoso.Sales.Entities;
using Contoso.Sales.Helpers;
using Contoso.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Contoso.Sales.Pages
{
    public partial class Login : ContentPage
    {

        public   Login()
        {
            InitializeComponent();
                btnLogin.Clicked += BtnLogin_Clicked;
 }

     

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var auth = DependencyService.Get<IAuthenticator>();
            var data = await auth.Authenticate(Contoso.Sales.Entities.AppConstants.commonAuthority, Contoso.Sales.Entities.AppConstants.graphResourceUri, Contoso.Sales.Entities.AppConstants.clientId, Contoso.Sales.Entities.AppConstants.returnUri);
            if (data != null && !string.IsNullOrEmpty(data.AccessToken))
            {
                AppConstants.ID_TOKEN = data.IdToken;
                AppConstants.ACCESS_TOKEN = data.AccessToken;
                AppConstants.USER_NAME = data.UserInfo.DisplayableId;
                AppConstants.FAMILYNAME = data.UserInfo.FamilyName;
                AppConstants.GIVENNAME = data.UserInfo.GivenName;
                DependencyService.Get<IPushNotification>().Register(AppConstants.USER_NAME);
                await Navigation.PushAsync(new CheckQuota());
            }
            else
            {
                await this.DisplayAlert("Error", "Login Failed", "Ok");
            }
        }

    }
}
    
