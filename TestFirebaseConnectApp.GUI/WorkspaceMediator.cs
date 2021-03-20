using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Windows;
using TestFirebaseConnectApp.GUI.ViewModels;

namespace TestFirebaseConnectApp.GUI
{
    class WorkspaceMediator : IWorkspaceMediator
    {
        private readonly Window _window;

        private readonly IFirebaseAuthProvider _authProvider;

        private readonly Dictionary<string, Action> _mediatorMethods;

        public WorkspaceMediator(Window window, IFirebaseAuthProvider authProvider)
        {
            _window = window;
            _authProvider = authProvider;

            _mediatorMethods = new Dictionary<string, Action>
            {
                { "goRegister", SetRegisterViewModel },
                { "register", SetUserInfoViewModel },
                { "goLogin", SetLoginViewModel },
                { "login", SetUserInfoViewModel },
                { "goRestorePassword", SetRestorePasswordViewModel }
            };
        }

        public void Notify(object sender, string key)
        {
            _mediatorMethods.GetValueOrDefault(key, null)?.Invoke();
        }

        public FirebaseAuth FirebaseAuth { get; set; }

        private void SetRegisterViewModel()
        {
            _window.DataContext = new RegisterNewAccountViewModel(this, _authProvider);
        }

        private void SetLoginViewModel()
        {
            _window.DataContext = new LoginViewModel(this, _authProvider);
        }

        private void SetUserInfoViewModel()
        {
            _window.DataContext = new UserInfoViewModel(this, _authProvider);
        }

        private void SetRestorePasswordViewModel()
        {
            _window.DataContext = new RestorePasswordViewModel(this, _authProvider);
        }
    }
}
