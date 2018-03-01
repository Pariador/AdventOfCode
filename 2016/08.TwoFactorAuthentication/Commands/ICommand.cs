namespace TwoFactorAuthentication.Commands
{
    public interface ICommand
    {
        void Execute(bool[,] screen);    
    }
}