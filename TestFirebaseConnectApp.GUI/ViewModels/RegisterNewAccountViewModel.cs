using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public class RegisterNewAccountViewModel : BaseWorkspace
    {
        public RegisterNewAccountViewModel(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
            : base(mediator, provider) { }

        private RelayCommand _registerNewAccountCommand;
        public RelayCommand RegisterNewAccountCommand =>
            _registerNewAccountCommand ??= new RelayCommand(async o => 
            {
                (var email, var password, var confirmPassword) = (ValueTuple<string, string, string>)o;

                var errorText = string.Empty;
                if(!EmailValidator.Validate(email, ref errorText))
                {
                    ErrorMessageText = errorText;
                    ErrorMessagePopupState = true;
                    return;
                }

                if(!PasswordValidator.Validate(password, confirmPassword, ref errorText))
                {
                    ErrorMessageText = errorText;
                    ErrorMessagePopupState = true;
                    return;
                }

                try
                {
                    var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                    _mediator.FirebaseAuth = auth;
                    _mediator.Notify(this, "register");
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.WeakPassword)
                {

                    ErrorMessageText = "Пароль слишком короткий.";
                    ErrorMessagePopupState = true;
                }
                catch (FirebaseAuthException fae) when (fae.Reason == AuthErrorReason.EmailExists)
                {
                    ErrorMessageText = "Пользователь с такой почтой уже зарегистрирован.";
                    ErrorMessagePopupState = true;
                }

            });


        private RelayCommand _goToLoginViewCommand;
        public RelayCommand GoToLoginViewCommand =>
            _goToLoginViewCommand ??= new RelayCommand(o => 
            {
                _mediator.Notify(this, "goLogin");
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
