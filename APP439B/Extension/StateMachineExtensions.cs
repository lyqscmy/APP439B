using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Stateless;

namespace APP439B
{
  public static class StateMachineExtensions
  {
    public static ICommand CreateCommand<TState, TTrigger>(this StateMachine<TState, TTrigger> stateMachine, TTrigger trigger)
    {
      return new RelayCommand
        (
          () => stateMachine.Fire(trigger),
          () => stateMachine.CanFire(trigger)
        );
    }
  }
}
