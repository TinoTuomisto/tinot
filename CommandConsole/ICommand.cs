namespace TinoT.CommandConsole
{ 
    public interface ICommand 
    {
        IIdentifier Identifier { get; }
        void Execute(string[] args);
    }
}
