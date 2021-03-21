using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class LoginViewModel : BaseWorkspace
    {
        public LoginViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
            : base(mediator, provider) { }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand =>
            _loginCommand ??= new RelayCommand(async o => 
            {
                var uii = (UserInputInfo)o;

                var errorText = string.Empty;
                if (!EmailValidator.Validate(uii.Email, ref errorText))
                {
                    ErrorMessageText = errorText;
                    ErrorMessagePopupState = true;
                    return;
                }

                if (!PasswordValidator.Validate(uii.Password, ref errorText))
                {
                    ErrorMessageText = errorText;
                    ErrorMessagePopupState = true;
                    return;
                }

                try
                {
                    var auth = await _authProvider.SignInWithEmailAndPasswordAsync(uii.Email, uii.Password);
                    _mediator.FirebaseAuth = auth;
                    _mediator.Notify(this, "login");
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.UnknownEmailAddress)
                {
                    ErrorMessageText = "Пользователь с такой почтой не найден.";
                    ErrorMessagePopupState = true;
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.WrongPassword)
                {
                    ErrorMessageText = "Неверный пароль.";
                    ErrorMessagePopupState = true;
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.TooManyAttemptsTryLater)
                {
                    ErrorMessageText = "Слишком много попыток входа, попробуйте позже.";
                    ErrorMessagePopupState = true;
                }
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

        #region error popup
        private bool _errorMessagePopupState;
        public bool ErrorMessagePopupState
        {
            get => _errorMessagePopupState;
            set
            {
                _errorMessagePopupState = value;
                OnPropertyChanged("ErrorMessagePopupState");
            }
        }

        private string _errorMessageText;
        public string ErrorMessageText
        {
            get => _errorMessageText;
            set
            {
                _errorMessageText = value;
                OnPropertyChanged("ErrorMessageText");
            }
        }

        private RelayCommand _closeErrorPopupCommand;
        public RelayCommand CloseErrorPopupCommand =>
            _closeErrorPopupCommand ??= new RelayCommand(o =>
            {
                ErrorMessagePopupState = false;
                OnPropertyChanged("ErrorMessagePopupState");
            });
        #endregion
    }
}
