using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace APP439B
{
  public enum States
  {
      Start, HandShaking, HandShakeComplete, TensionAdjusting, TensionAdjustComplete,
      SafetyConfirming, SafetyConfirmComplete, Altering, AlterComplete, firing, fireComplete,
      Stoping, AllClear, End
  }

  public enum Triggers
  {
      HandShake, HandShakeFailed, HandShakeSucceeded, TensionAdjust, TensionAdjustFailed, TensionAdjustSucceeded,
      SafetyConfirm, SafetyConfirmingFailed, SafetyConfirmSucceeded, Alter, AlterFailed, AlterSucceeded,
      Fire, FireFailed, FireSucceeded, Stop, StopFailed, StopSucceeded, AllClear, AllClearFailed, AllClearSucceeded
  }

  public class StateMachine : Stateless.StateMachine<States, Triggers>, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public StateMachine(Action HandShakeAction): base(States.Start)
    {
      Configure(States.Start)
        .Permit(Triggers.HandShake, States.HandShaking);

      Configure(States.HandShaking)
        .OnEntry(HandShakeAction)
        .Permit(Triggers.HandShakeSucceeded, States.HandShakeComplete)
        .Permit(Triggers.HandShakeFailed, States.Start);

      Configure(States.HandShakeComplete)
        .Permit(Triggers.TensionAdjust, States.TensionAdjusting);

      Configure(States.TensionAdjusting)
        .Permit(Triggers.TensionAdjustFailed, States.HandShakeComplete)
        .Permit(Triggers.TensionAdjustSucceeded, States.TensionAdjustComplete);

      Configure(States.TensionAdjustComplete)
        .Permit(Triggers.SafetyConfirm, States.SafetyConfirming);

      Configure(States.SafetyConfirming)
        .Permit(Triggers.SafetyConfirmingFailed, States.TensionAdjustComplete)
        .Permit(Triggers.SafetyConfirmSucceeded, States.SafetyConfirmComplete);

      Configure(States.SafetyConfirmComplete)
        .Permit(Triggers.Alter, States.Altering);

      Configure(States.Altering)
        .Permit(Triggers.Fire, States.fireComplete);

      Configure(States.fireComplete)
        .Permit(Triggers.Stop, States.Altering)
        .Permit(Triggers.AllClear, States.AllClear);

      Configure(States.AllClear)
        .Permit(Triggers.AllClearFailed, States.fireComplete)
        .Permit(Triggers.AllClearSucceeded, States.End);

      OnTransitioned
        (
          (t) => 
          {
            OnPropertyChanged("State");
            CommandManager.InvalidateRequerySuggested();
          }
        );
      
      //used to debug commands and UI components
      OnTransitioned
        (
          (t) => Debug.WriteLine
            (
              "State Machine transitioned from {0} -> {1} [{2}]", 
              t.Source, t.Destination, t.Trigger
            )
        );
    }

    private void OnPropertyChanged(string propertyName)
    {
      if(PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    
  }
}
