using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using AutoMapper;

using Caliburn.Micro;

using RMDesktopUI.Helpers;
using RMDesktopUI.Models;
using RMDesktopUI.ViewModels;

using RMDesktopUILibrary.API;
using RMDesktopUILibrary.Helpers;
using RMDesktopUILibrary.Models;

namespace RMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        protected override void Configure()
        {
            IMapper mapper = InitializeAutoMapper();
            container.Instance(mapper);
            container.Instance(container).PerRequest<IProductEndpoint, ProductEndpoint>();
            container.Instance(container).PerRequest<ISaleEndpoint, SaleEndpoint>();
            container.Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IAPIHelper, APIHelper>()
                .Singleton<IConfigHelper, ConfigHelper>();
            GetType().Assembly.GetTypes().Where(type => type.IsClass).Where(type => type.Name.EndsWith("ViewModel")).ToList().ForEach(viewModelType => container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));
        }

        private IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>().Wait();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
