using Live_Server.SocketServer;

namespace Live_Server
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyToolWindowCommand : BaseCommand<MyToolWindowCommand>
    {
        private bool isLiveServerActive = false;
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            //if liver server is on
            SockerServer.StartServer();

            

            
            
            return MyToolWindow.ShowAsync();


        }
    }

    //[Command(PackageIds.OnCommand)]
    //internal sealed class OnCommand : BaseCommand<OnCommand>
    //{
    //    protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
    //    {
    //        VS.MessageBox.ShowError("Start Live Server command executed.");
    //        return MyToolWindow.ShowAsync();
    //    }
    //}

    [Command(PackageIds.OffCommand)]
    internal sealed class OffCommand : BaseCommand<OffCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            VS.MessageBox.ShowError("Stop Live Server command executed.");
            return MyToolWindow.HideAsync();
        }
    }


}
