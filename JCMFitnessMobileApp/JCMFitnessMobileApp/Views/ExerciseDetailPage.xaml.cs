using JCMFitnessMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDetailPage : ContentPage
    {
        ExerciseDetailViewModel ViewModel => BindingContext as ExerciseDetailViewModel;
        public ExerciseDetailPage()
        {
            InitializeComponent();
        }
    }
}