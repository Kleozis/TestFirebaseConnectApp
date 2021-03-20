using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class UserInfoViewModel : BaseWorkspace
    {
        public UserInfoViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
            : base(mediator, provider) { }

        public FirebaseUser User => _mediator.FirebaseAuth.User;

        private RelayCommand _goToChangePasswordCommand;
        public RelayCommand GoToChangePasswordCommand =>
            _goToChangePasswordCommand ??= new RelayCommand(o => 
            {
                
            });

        private RelayCommand _goToDeleteAccountCommand;
        public RelayCommand GoToDeleteAccountCommand =>
            _goToDeleteAccountCommand ??= new RelayCommand(o =>
            {

            });

        private RelayCommand _goToExitCommand;
        public RelayCommand GoToExitCommand =>
            _goToExitCommand ??= new RelayCommand(o =>
            {

            });


    }
}
