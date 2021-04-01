using JCMFitnessMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.ComponentModel;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewExercisePage : ContentPage
    {
        NewExerciseViewModel ViewModel =>
        BindingContext as NewExerciseViewModel;
        public NewExercisePage()
        {
            InitializeComponent();
            BindingContextChanged += Page_BindingContextChanged;


            void Page_BindingContextChanged(object sender, EventArgs e)
            {
                ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
            }
            void ViewModel_ErrorsChanged(object sender,
                DataErrorsChangedEventArgs e)
            {
                var propHasErrors = (ViewModel.GetErrors(e.PropertyName)
                    as List<string>)?.Any() == true;
                switch (e.PropertyName)
                {
                    case nameof(ViewModel.Title):
                        title.LabelColor = propHasErrors
                            ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.Id):
                        title.LabelColor = propHasErrors
                            ? Color.Red : Color.Black;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}