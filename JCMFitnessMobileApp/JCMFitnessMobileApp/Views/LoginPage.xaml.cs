using JCMFitnessMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lottie;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
       
        public LoginPage()
        {
            InitializeComponent();
            animationView.PlayAnimation();
            NavigationPage.SetHasBackButton(this, true);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }


    }
}