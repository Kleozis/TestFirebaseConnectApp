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

                var errorText = string.Empty;
                if (!EmailValidator.Validate(email, ref errorText))
                {
                    MessagePopupText = errorText;
                    MessagePopupState = true;
                    return;
                }

                try
                {
                    await _authProvider.SendPasswordResetEmailAsync(email);
                    MessagePopupText = "Письмо со сменой пароля отправлено на почту.";
                    MessagePopupState = true;
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.UnknownEmailAddress)
                {
                    MessagePopupText = "Пользователь с такой почтой не найден.";
                    MessagePopupState = true;
                }

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

        #region popup
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
    }
}
