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
    public partial class WorkoutDisplayView : ContentPage
    {
        public WorkoutDisplayView()
        {
            InitializeComponent();
            Task.Run(async () => await Initialize());
        }



        private async Task Initialize()
        {
            var viewModel = Resolver.Resolve<WorkoutDisplayViewModel>();
            BindingContext = viewModel;
            await viewModel.Init();
        }

    }
}