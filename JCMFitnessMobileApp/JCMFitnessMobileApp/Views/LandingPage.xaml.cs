using JCMFitnessMobileApp.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
            ReconcileTheme();
            animationView.PlayAnimation();
        }

        private void ReconcileTheme()
        {
            if (Preferences.ContainsKey("theme"))
            {
                var theme = Preferences.Get("theme", "");
                if (theme != null)
                {
                    ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                    mergedDictionaries.Clear();
                    if (theme == "Dark")
                    {

                        mergedDictionaries.Add(new DarkTheme());

                    }
                    else
                    {
                        mergedDictionaries.Add(new LightTheme());
                    }
                }
            }
            else
            {

            }

        }
    }
}