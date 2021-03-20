using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class UserInfoViewModel : BaseWorkspace
    {
        public UserInfoViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
            : base(mediator, provider) 
        {
            ChangePasswordPopupState = false;
            ConfirmDeleteAccountPopupState = false;
        }

        public FirebaseUser User => _mediator.FirebaseAuth.User;

        private RelayCommand _changePasswordCommand;
        public RelayCommand ChangePasswordCommand =>
            _changePasswordCommand ??= new RelayCommand(o => 
            {
                ChangePasswordPopupState = true;
                OnPropertyChanged("ChangePasswordPopupState");
            });

        private RelayCommand _deleteAccountCommand;
        public RelayCommand DeleteAccountCommand =>
            _deleteAccountCommand ??= new RelayCommand(o =>
            {
                // код удаления аккаунта
                _mediator.Notify(this, "delete");
            });

        private RelayCommand _exitFromAccountCommand;
        public RelayCommand ExitFromAccountCommand =>
            _exitFromAccountCommand ??= new RelayCommand(o =>
            {
                _mediator.Notify(this, "exit");
            });


        public bool ChangePasswordPopupState { get; set; }

        public bool ConfirmDeleteAccountPopupState { get; set; }

    }
}
