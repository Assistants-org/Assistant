﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assistant.Commands.Models;
using Assistant.Facade.Commands;
using Assistant.Facade.Messages;

namespace Assistant.Commands.Managers

{
    public class CommandsManager : ICommandsManager
    {
        public List<ICommand> Commands { get; } = new List<ICommand>();
        public ICommand DefaultCommand { get; set; }

        public IAssistantMessage TryExecuteCommands(IAssistantContext context, IEnumerable<ICommandFindResult> commands)
        {
            if (commands.Count() == 0) return null;

            var list = commands.ToList();

            // sort list by priority
            list.Sort((a, b) => b.Command.Info.Priority - a.Command.Info.Priority);

            //get current execute command
            foreach (CommandFindResult current in list)
            {
                var result = current.Command.Execute(current.Context);
                if (result != null)
                {
                    return result;
                }
            }

            return DefaultCommand.Execute(context);
        }

        private bool KeySearchMatchesInCommand(IEnumerable<string> commandKey, IEnumerable<string> key)
        {
            commandKey = commandKey.Distinct();
            key = key.Distinct();

            return commandKey.Count(e => key.Contains(e)) == key.Count();
        }

        public IEnumerable<ICommandFindResult> FindCommands(IAssistantContext context)
        {
            List<CommandFindResult> findResult = Commands
                .Select(e => new CommandFindResult { Command = e, Context = context })
                    .ToList();

            IEnumerable<CommandFindResult> result =
                findResult.FindAll(e => e.Command.Info.Keys
                    .Any(delegate (IEnumerable<string> key) {
                        bool isFind = KeySearchMatchesInCommand(context.Message.CommandKey, key);
                        if (isFind)
                        {
                            e.Context.Message.ExcuteCommandKey = key;
                        }
                        return isFind;
                    }));

            return result;
        }

        public Task<IAssistantMessage> TryExecuteCommandsAsync(IAssistantContext context, IEnumerable<ICommandFindResult> commands)
        {
            return Task.Run(() => TryExecuteCommands(context, commands));
        }

        public Task<IEnumerable<ICommandFindResult>> FindCommandsAsync(IAssistantContext context)
        {
            return Task.Run(() => FindCommands(context));
        }

        public IAssistantMessage TryFindAndExecuteCommands(IAssistantContext context)
        {
            return TryExecuteCommands(context, FindCommands(context));
        }

        public async Task<IAssistantMessage> TryFindAndExecuteCommandsAsync(IAssistantContext context)
        {
            return await TryExecuteCommandsAsync(context, await FindCommandsAsync(context));
        }
    }
}
