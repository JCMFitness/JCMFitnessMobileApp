using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCMFitnessMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var image = new Image { Source = "waterfront.jpg" };
            image.Source = Device.RuntimePlatform == Device.Android
                ? ImageSource.FromFile("Logo.png")
                : ImageSource.FromFile("Assets/logo.jpg");
        }

        
    }
}
