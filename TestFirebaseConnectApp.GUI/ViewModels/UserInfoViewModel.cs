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
        public bool _changePasswordPopupState;
        public bool ChangePasswordPopupState 
        { 
            get => _changePasswordPopupState;
            set
            {
                _changePasswordPopupState = value;
                OnPropertyChanged("ChangePasswordPopupState");
            }
        }

        private RelayCommand _openChangePasswordPopupCommand;
        public RelayCommand OpenChangePasswordPopupCommand =>
            _openChangePasswordPopupCommand ??= new RelayCommand(o =>
            {
                ChangePasswordPopupState = true;
            });

        private RelayCommand _confirmChangePasswordCommand;
        public RelayCommand ConfirmChangePasswordCommand => 
            _confirmChangePasswordCommand ??= new RelayCommand(async o =>
            {
                var uii = (UserInputInfo)o;

                var errorText = string.Empty;
                if (!PasswordValidator.Validate(uii.Password, uii.ConfirmPassword, ref errorText))
                {
                    MessagePopupText = errorText;
                    MessagePopupState = true;
                    return;
                }

                var token = _mediator.FirebaseAuth.FirebaseToken;
                try
                {
                    await _authProvider.ChangeUserPasswordAsync(token, uii.Password);
                    ChangePasswordPopupState = false;
                    _mediator.Notify(this, "goLogin");
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.WeakPassword)
                {
                    MessagePopupText = "Пароль слишком короткий.";
                    MessagePopupState = true;
                }
            });

        private RelayCommand _cancelChangePasswordCommand;
        public RelayCommand CancelChangePasswordCommand =>
            _cancelChangePasswordCommand ??= new RelayCommand(o =>
            {
                ChangePasswordPopupState = false;
            });
        #endregion

        #region delete account popup
        public bool _confirmDeleteAccountPopupState;
        public bool ConfirmDeleteAccountPopupState 
        { 
            get => _confirmDeleteAccountPopupState;
            set 
            {
                _confirmDeleteAccountPopupState = value;
                OnPropertyChanged("ConfirmDeleteAccountPopupState");
            } 
        }

        private RelayCommand _openDeleteAccountPopupCommand;
        public RelayCommand OpenDeleteAccountPopupCommand =>
            _openDeleteAccountPopupCommand ??= new RelayCommand(o =>
            {
                ConfirmDeleteAccountPopupState = true;
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
            });
        #endregion

        #region info popup
        private bool _messagePopupState;
        public bool MessagePopupState
        {
            get => _messagePopupState;
            set
            {
                _messagePopupState = value;
                OnPropertyChanged("MessagePopupState");
            }
        }

        private string _messagePopupText;
        public string MessagePopupText
        {
            get => _messagePopupText;
            set
            {
                _messagePopupText = value;
                OnPropertyChanged("MessagePopupText");
            }
        }

        private RelayCommand _closePopupCommand;
        public RelayCommand ClosePopupCommand =>
            _closePopupCommand ??= new RelayCommand(o =>
            {
                MessagePopupState = false;
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
