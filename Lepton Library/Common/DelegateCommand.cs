using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lepton_Library.Common
{
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// Command
        /// </summary>
        private Action<T> _Command;

        /// <summary>
        /// Decide whether it can be executed
        /// </summary>
        private Func<T, bool> _CanExecute;

        /// <summary>
        /// Event happened when it finished
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="command">CommandExecuted</param>
        public DelegateCommand(Action<T> command) : this(command, null)
        {

        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="command">CommandExecuted </param>
        /// <param name="canexecute">DecisionOfWhetherItCanExecute</param>
        public DelegateCommand(Action<T> command, Func<T, bool> canexecute)
        {
            if (command == null)
            {
                throw new ArgumentException("command");
            }
            _Command = command;
            _CanExecute = canexecute;
        }

        /// <summary>
        /// Return whether it can be executed
        /// </summary>
        /// <param name="parameter">DecisionParameter</param>
        /// <returns>DecideResult（True：CanExecute，False：CanNotExecute）</returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute((T)parameter);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Parameter</param>
        public void Execute(object parameter)
        {
            _Command((T)parameter);
        }
    }

}
