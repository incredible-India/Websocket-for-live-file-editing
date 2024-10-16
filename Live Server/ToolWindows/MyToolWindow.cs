using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Live_Server
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        private static MyToolWindow _instance;
        public override string GetTitle(int toolWindowId) => "Live Server Window";
        private MyToolWindowControl _control;
        public override Type PaneType => typeof(Pane);
        public MyToolWindow()
        {
            _instance = this; // Set the instance on construction
        }
        public static MyToolWindow Instance => _instance;
        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            _control = new MyToolWindowControl();
            return Task.FromResult<FrameworkElement>(new MyToolWindowControl());
        }
        public void UpdateText(string content)
        {
            _control.AppendText(content); // Update the control with new content
        }
        [Guid("8e1fd02c-bdb7-49c3-9b91-0e69d8d985d3")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}