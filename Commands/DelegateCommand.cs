using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFINAL.Commands
{
    public class DelegateCommand : Command
    {
        private static readonly Func<bool> defaultExecuteMethod = () => true;

        private Func<bool> canExecuteMethod;
        private readonly Action executeMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, defaultExecuteMethod)
        { }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public override void Execute()
        {
            executeMethod();
        }

        public override bool CanExecute()
        {
            return canExecuteMethod();
        }
    }
}
