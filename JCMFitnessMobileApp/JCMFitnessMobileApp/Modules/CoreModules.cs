﻿using JCMFitnessMobileApp.ViewModel;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using JCMFitnessMobileApp.Services;
using Refit;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace JCMFitnessMobileApp.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            //Bind<SignInViewModel>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<WorkoutDetailViewModel>().ToSelf();
            Bind<NewWorkoutViewModel>().ToSelf();

            var baseUrl = "https://jcmfitnessapi.herokuapp.com";

            //var apiAuthToken = Preferences.Get("apitoken", "");

            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());

            JsonConvert.DefaultSettings =
                () => new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter() }
                };

            IFitApi refitInstance = RestService.For<IFitApi>(baseUrl);


            var tripLogService = new FitnessService(refitInstance);



            /*var fitApiService = new IFitApi();

            containerBuilder.RegisterInstance(refitInstance)
                .As<IFitApi>();*/

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);

            //Bind<IAuthService>().To<AuthService>().InSingletonScope();

            Bind<IFitnessService>()
                .ToMethod(x => tripLogService)
                .InSingletonScope();

        }
    }
}
