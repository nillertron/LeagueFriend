using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LeagueFriend.Command
{
    class CommandHandler : ICommand
    {
        private Action _Action;
        private Func<bool> _CanExecute;
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _Action = action;
            _CanExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return _CanExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _Action();
        }
    }
}
