using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TestFirebaseConnectApp.GUI.ViewModels;

namespace TestFirebaseConnectApp.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IWorkspaceMediator _workspaceMediator;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // https://github.com/vice-code/ViceCode.Firebase.Auth/blob/master/src/Firebase.Auth.Tests/IntegrationTests.cs
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("config.json").Build())
            .AddHttpClient()
            .AddTransient<IFirebaseAuthProvider, FirebaseAuthProvider>(x =>
            {
                var config = x.GetRequiredService<IConfiguration>();
                var apiKey = config.GetValue<string>("ApiKey");
                return new FirebaseAuthProvider(apiKey, x.GetRequiredService<IHttpClientFactory>());
            })
            .BuildServiceProvider();

            var main = new MainWindow();
            _workspaceMediator = new WorkspaceMediator(main, serviceProvider.GetRequiredService<IFirebaseAuthProvider>());
            _workspaceMediator.Notify(this, "goRegister");
            main.Show();
        }
    }
}
