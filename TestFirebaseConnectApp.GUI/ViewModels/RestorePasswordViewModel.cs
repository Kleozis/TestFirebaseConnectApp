using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class RestorePasswordViewModel : BaseWorkspace
    {
        public RestorePasswordViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
            : base(mediator, provider) { }

        private RelayCommand _restorePasswordCommand;
        public RelayCommand RestorePasswordCommand =>
            _restorePasswordCommand ??= new RelayCommand(async o => 
            {
                var email = (string)o;
                await _authProvider.SendPasswordResetEmailAsync(email); // переделать
                _mediator.Notify(this, "restorePassword");
            });

        private RelayCommand _goToLoginViewCommand;
        public RelayCommand GoToLoginViewCommand =>
            _goToLoginViewCommand ??= new RelayCommand(o =>
            {
                _mediator.Notify(this, "goLogin");
            });

        private RelayCommand _goToRegisterViewCommand;

        public RelayCommand GoToRegisterViewCommand =>
            _goToRegisterViewCommand ??= new RelayCommand(o =>
            {
                _mediator.Notify(this, "goRegister");
            });
    }
}
