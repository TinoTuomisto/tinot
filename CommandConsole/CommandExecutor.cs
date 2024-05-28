namespace TinoT.CommandConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    public class CommandExecutor
    {
        private readonly Dictionary<string, ICommand> Commands;
        public CommandExecutor(IEnumerable<ICommand> commands)
        {
            Commands = new Dictionary<string, ICommand>();
            
            foreach (var command in commands)
            {
                RegisterCommand(command);
            }
        }
        
        private void RegisterCommand(ICommand command)
        {
            if (!Commands.TryAdd(command.Identifier.Name.ToLower(), command))
            {
                throw new InvalidCastException($"Command word already registered: {command.Identifier.Name}");
            }
        }
        
        public void ExecuteCommand(string input)
        {
            var parts = input.Split(' ');
            var name = parts[0];
            var args = parts.Skip(1).ToArray();
            
            if (!Commands.TryGetValue(name.ToLower(), out var command)) return;
            
            try
            {
                command.Execute(args);
            }
            
            catch (Exception ex)
            {
                Debug.Log($"Error with command execution: {ex.Message}");
            }
        }

        public IEnumerable<string> GetSuggestions(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Enumerable.Empty<string>();
            }

            var commandSuggestions = Commands.Keys.Where(term => term.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();

            var searchTerms = Commands.Values.SelectMany(command => command.Identifier.GetSearchTerms()
                .Where(x => x.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .Select(x => command.Identifier.Name).Distinct()).ToList();

            return commandSuggestions.Concat(searchTerms).Distinct();
        }
    }
}
