using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APP439B.Commands
{
    public static class Commands
    {
        public readonly static RoutedUICommand HandShake = new RoutedUICommand(
         "自检", "自检", typeof(Commands));

        public readonly static RoutedUICommand PlayBoardCast = new RoutedUICommand(
         "播放警报", "播放警报", typeof(Commands));

        public readonly static RoutedUICommand Start = new RoutedUICommand(
        "点火", "点火", typeof(Commands));

        public readonly static RoutedUICommand Stop = new RoutedUICommand(
        "停止", "停止", typeof(Commands));
    }
}
