namespace TinoT.CommandConsole
{
    using System.Collections.Generic;
    using System.Linq;
    public class Identifier : IIdentifier
    { 
        public string Name { get; set; }
        private IEnumerable<string> Aliases { get; set; }
        public string Description { get; set; }
        public Identifier(string name, string description , IEnumerable<string> aliases)
        {
            Name = name;
            Description = description;
            Aliases = aliases ?? new List<string>();
        }
        
        public Identifier(string name, string description)
        {
            Name = name;
            Description = description;
            
            Aliases = new List<string>();
        }
        
        public Identifier(string name)
        {
            Name = name;
            
            Description = string.Empty;
            Aliases = new List<string>();
        }
        
        public IEnumerable<string> GetSearchTerms()
        {
            return new[] { Name }.Concat(Aliases);
        }
    }
}
