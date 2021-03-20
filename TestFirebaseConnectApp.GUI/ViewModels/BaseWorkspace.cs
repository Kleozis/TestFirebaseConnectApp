using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirebaseConnectApp.GUI.ViewModels
{
    public abstract class BaseWorkspace
    {
        protected readonly IFirebaseAuthProvider _authProvider;

        protected readonly IWorkspaceMediator _mediator;

        protected BaseWorkspace(IWorkspaceMediator mediator, IFirebaseAuthProvider provider)
        {
            _mediator = mediator;
            _authProvider = provider;
        }
    }
}
