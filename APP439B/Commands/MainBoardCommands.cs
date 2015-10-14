using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APP439B.Commands
{
    public static class MainBoardCommands
    {
        public readonly static RoutedUICommand Connect= new RoutedUICommand(
            "连接主板", "连接主板", typeof(MainBoardCommands));

        public readonly static RoutedUICommand Disconnect = new RoutedUICommand(
         "断开连接", "断开连接", typeof(MainBoardCommands));

        public readonly static RoutedUICommand Settings = new RoutedUICommand(
         "设置串口", "设置串口", typeof(MainBoardCommands));

        public readonly static RoutedUICommand Query = new RoutedUICommand(
         "查询", "查询", typeof(MainBoardCommands));

    }
}
