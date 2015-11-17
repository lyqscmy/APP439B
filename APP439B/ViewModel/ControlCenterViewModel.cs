using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using APP439B.View;

namespace APP439B.ViewModel
{
    public  class ControlCenterViewModel : BindableObject
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public ControlCenterViewModel()
        {
            StateMachine = new StateMachine
            (
              HandShakeAction: () => HandShake()
            );
            HandShakeCommand = StateMachine.CreateCommand(Triggers.HandShake);
            AlterCommand = StateMachine.CreateCommand(Triggers.Alter);
            FireCommand = StateMachine.CreateCommand(Triggers.Fire);
            StopCommand = StateMachine.CreateCommand(Triggers.Stop);
            AllClearCommand = StateMachine.CreateCommand(Triggers.AllClear);
            HandShakeState = 3;
        }

        public ICommand HandShakeCommand { get; private set; }
        public ICommand AlterCommand { get; private set; }
        public ICommand FireCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand AllClearCommand { get; private set; }

        public StateMachine StateMachine { get; private set; }

        private int handShakeState;
        public int HandShakeState {
            get
            {
                return handShakeState;
            }
            set
            {
                handShakeState = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HandShakeState"));
            }
        }
        

        public void HandShake()
        {
            // Configure the message box to be displayed
            string messageBoxText = "自检是否成功?";
            string caption = "自检结果";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    StateMachine.Fire(Triggers.HandShakeSucceeded);
                    HandShakeState = 2;
                    HandshakeWindow handShakeWindow = new HandshakeWindow();
                    handShakeWindow.ShowDialog();
                    TensionAdjust();
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    StateMachine.Fire(Triggers.HandShakeFailed);
                    break;
            }

        }

        private void TensionAdjust()
        {
            StateMachine.Fire(Triggers.TensionAdjust);
            string messageBoxText = "拉力是否到位?";
            string caption = "拉力";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    StateMachine.Fire(Triggers.TensionAdjustSucceeded);
                    SafetyConfirm();
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    StateMachine.Fire(Triggers.TensionAdjustFailed);
                    break;
            }
        }

        private void SafetyConfirm()
        {
            StateMachine.Fire(Triggers.SafetyConfirm);
            string messageBoxText = "靶区是否安全?";
            string caption = "安全确认";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    StateMachine.Fire(Triggers.SafetyConfirmSucceeded);
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    StateMachine.Fire(Triggers.SafetyConfirmingFailed);
                    break;
            }
        }
          
    }
}
