namespace TinoT.CommandConsole
{
    using System;
    using UnityEngine;
    public class HelloWorld : MonoBehaviour
    {
        private CommandExecutor CommandExecutor;
        
        [SerializeField] private string inputField;
        private void Start()
        {
            ICommand[] commands = {
                new HelloCommand(),
                new MathCommand()
            };
            
            CommandExecutor = new CommandExecutor(commands);
        }

        [ContextMenu("Test1")]
        public void Test1()
        {
            CommandExecutor.ExecuteCommand(inputField);
        }

        [ContextMenu("Test2")]
        public void Test2()
        {
            foreach (var suggestion in CommandExecutor.GetSuggestions(inputField))
            {
                Debug.Log(suggestion);
            }
        }
    }
    
    public class HelloCommand : ICommand
    {
        public IIdentifier Identifier { get; private set; }

        public HelloCommand()
        {
            Identifier = new Identifier("hello", "", new []{"Hi", "World", "Moi"});
        }
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Debug.Log("Hello World");
            }
            else
            {
                var name = args[0];
                Debug.Log($"Hello {name}");
            }
        }
    }
    
    public class MathCommand : ICommand
    {
        public IIdentifier Identifier { get; private set; }

        public MathCommand()
        {
            Identifier = new Identifier(
                "math", 
                "Performs basic arithmetic operations.", 
                new[] { "calc" });
        }
        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Debug.LogError("Not enough arguments.");
                return;
            }

            if (float.TryParse(args[0], out var num1) && float.TryParse(args[2], out var num2))
            {
                var operation = args[1];
                var result = operation switch
                {
                    "+" => num1 + num2,
                    "-" => num1 - num2,
                    "*" => num1 * num2,
                    "/" => num1 / num2,
                    _ => throw new InvalidOperationException("Unknown operation")
                };
                Debug.Log($"Result: {result}");
            }
            else
            {
                Debug.LogError("Invalid numbers.");
            }
        }
    }
}
