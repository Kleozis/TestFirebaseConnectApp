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

        #region change password popup
        public bool ChangePasswordPopupState { get; set; }

        private RelayCommand _openChangePasswordPopupCommand;
        public RelayCommand OpenChangePasswordPopupCommand =>
            _openChangePasswordPopupCommand ??= new RelayCommand(o =>
            {
                ChangePasswordPopupState = true;
                OnPropertyChanged("ChangePasswordPopupState");
            });

        private RelayCommand _confirmChangePasswordCommand;
        public RelayCommand ConfirmChangePasswordCommand => 
            _confirmChangePasswordCommand ??= new RelayCommand(o =>
            {
                var password = (string)o;
                var token = _mediator.FirebaseAuth.FirebaseToken;
                _authProvider.ChangeUserPasswordAsync(token, password);

                ChangePasswordPopupState = false;
                OnPropertyChanged("ChangePasswordPopupState");
            });

        private RelayCommand _cancelChangePasswordCommand;
        public RelayCommand CancelChangePasswordCommand =>
            _cancelChangePasswordCommand ??= new RelayCommand(o =>
            {
                ChangePasswordPopupState = false;
                OnPropertyChanged("ChangePasswordPopupState");
            });
        #endregion

        #region delete account popup
        public bool ConfirmDeleteAccountPopupState { get; set; }

        private RelayCommand _openDeleteAccountPopupCommand;
        public RelayCommand OpenDeleteAccountPopupCommand =>
            _openDeleteAccountPopupCommand ??= new RelayCommand(o =>
            {
                ConfirmDeleteAccountPopupState = true;
                OnPropertyChanged("ConfirmDeleteAccountPopupState");
            });

        private RelayCommand _confirmDeleteAccountCommand;
        public RelayCommand ConfirmDeleteAccountCommand =>
            _confirmDeleteAccountCommand ??= new RelayCommand(o =>
            {
                var token = _mediator.FirebaseAuth.FirebaseToken;
                _authProvider.DeleteUserAsync(token);
                _mediator.Notify(this, "delete");
            });

        private RelayCommand _cancelDeleteAccountCommand;
        public RelayCommand CancelDeleteAccountCommand =>
            _cancelDeleteAccountCommand ??= new RelayCommand(o =>
            {
                ConfirmDeleteAccountPopupState = false;
                OnPropertyChanged("ConfirmDeleteAccountPopupState");
            });
        #endregion

        private RelayCommand _exitFromAccountCommand;
        public RelayCommand ExitFromAccountCommand =>
            _exitFromAccountCommand ??= new RelayCommand(o =>
            {
                _mediator.Notify(this, "exit");
            });
    }
}
