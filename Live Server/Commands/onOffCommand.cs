using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live_Server.Commands
{
    [Command(PackageIds.OnCommand)]
    internal class onOffCommand : BaseCommand<onOffCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            VS.MessageBox.ShowError("Start Live Server command executed.");
            return MyToolWindow.ShowAsync();
        }
    }
}
