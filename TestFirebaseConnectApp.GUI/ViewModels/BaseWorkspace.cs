using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public abstract class BaseWorkspace : INotifyPropertyChanged
    {
        protected readonly IFirebaseAuthProvider _authProvider;

        protected readonly IWorkspaceMediator _mediator;

        protected BaseWorkspace(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
        {
            _mediator = mediator;
            _authProvider = provider;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
