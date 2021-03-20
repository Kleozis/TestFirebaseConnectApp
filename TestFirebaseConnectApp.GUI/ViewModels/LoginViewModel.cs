using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class LoginViewModel
    {
        private readonly IFirebaseAuthProvider _authProvider;

        private readonly IWorkspaceMediator _mediator;

        public LoginViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
        {
            _mediator = mediator;
            _authProvider = provider;
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand =>
            _loginCommand ??= new RelayCommand(async o => 
            {
                (string email, string password) = (ValueTuple<string, string>)o;
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
                _mediator.Notify(this, "login");
            });

        private RelayCommand _goToRestorePasswordViewCommand;
        public RelayCommand GoToRestorePasswordViewCommand =>
            _goToRestorePasswordViewCommand ??= new RelayCommand(o => 
            {
                _mediator.Notify(this, "goRestorePassword");
            });

        private RelayCommand _goToRegisterViewCommand;
        public RelayCommand GoToRegisterViewCommand =>
            _goToRegisterViewCommand ??= new RelayCommand(o =>
            {
                _mediator.Notify(this, "goRegister");
            });
    }
}
