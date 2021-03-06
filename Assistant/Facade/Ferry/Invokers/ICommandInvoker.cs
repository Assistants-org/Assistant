﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rovecode.Assistant.Facade.Domain.Commands;
using Rovecode.Assistant.Facade.Domain.Messages;
using Rovecode.Assistant.Facade.Ferry.Commands;
using Rovecode.Assistant.Facade.Ferry.Contexts;

namespace Rovecode.Assistant.Facade.Ferry.Invokers
{
    public interface ICommandInvoker
    {
        public List<Type> CommandsTypes { get; }
        public Type DefaultCommandType { get; set; }

        public Task<IDispatchMessage> RunAsync(ICommandContext context);
    }
}
