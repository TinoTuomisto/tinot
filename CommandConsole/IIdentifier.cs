namespace TinoT.CommandConsole
{
    using System.Collections.Generic;
    public interface IIdentifier 
    {
        string Name { get; }
        IEnumerable<string> GetSearchTerms();
    }
}
