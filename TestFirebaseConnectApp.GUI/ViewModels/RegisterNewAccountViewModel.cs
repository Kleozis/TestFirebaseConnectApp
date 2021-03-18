using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class RegisterNewAccountViewModel
    {
        private IFirebaseAuthProvider _authProvider;

        private RelayCommand registerNewAccountCommand;
        public RelayCommand RegisterNewAccountCommand =>
            registerNewAccountCommand ??= new RelayCommand(async o => 
            {
                System.Diagnostics.Debug.WriteLine("RegisterNewAccountCommand");
                // создать нового пользователя
            });


        private RelayCommand goToLoginViewCommand;
        public RelayCommand GoToLoginViewCommand =>
            goToLoginViewCommand ??= new RelayCommand(o => 
            {
                System.Diagnostics.Debug.WriteLine("GoToLoginViewCommand");
                // переключить RegisterNewAccountViewModel на LoginViewModel
            });
    }
}
