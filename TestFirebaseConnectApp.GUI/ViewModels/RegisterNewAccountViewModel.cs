using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class RegisterNewAccountViewModel
    {
        private readonly IFirebaseAuthProvider _authProvider;

        private readonly IWorkspaceMediator _mediator;

        public RegisterNewAccountViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
        {           
            _mediator = mediator;
            _authProvider = provider;
        }

        private RelayCommand _registerNewAccountCommand;
        public RelayCommand RegisterNewAccountCommand =>
            _registerNewAccountCommand ??= new RelayCommand(async o => 
            {
                (string email, string password) = (ValueTuple<string, string>)o;
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                _ = auth.User;
                _mediator.Notify(this, "register");
            });


        private RelayCommand _goToLoginViewCommand;
        public RelayCommand GoToLoginViewCommand =>
            _goToLoginViewCommand ??= new RelayCommand(o => 
            {
                _mediator.Notify(this, "goLogin");
            });
    }
}
