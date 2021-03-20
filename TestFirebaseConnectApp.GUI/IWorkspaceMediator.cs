using Firebase.Auth;

namespace TestFirebaseConnectApp.GUI
{
    public interface IWorkspaceMediator
    {
        void Notify(object sender, string key);

        FirebaseAuth FirebaseAuth { get; set; }
    }
}