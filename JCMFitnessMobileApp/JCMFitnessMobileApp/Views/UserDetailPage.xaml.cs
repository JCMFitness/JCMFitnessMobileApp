using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JCMFitnessMobileApp.Themes;
using JCMFitnessMobileApp.Services;
using Xamarin.Essentials;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailPage : ContentPage, IModalPage
    {
        public UserDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        void OnPickerSelectionChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Theme theme = (Theme)picker.SelectedItem;

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
                statusLabel.Text = $"{theme.ToString()} theme loaded.";
                Preferences.Set("theme", theme.ToString());
            }

         }
        public async Task Dismiss()
        {
            await Navigation.PopModalAsync();
        }

    }
}