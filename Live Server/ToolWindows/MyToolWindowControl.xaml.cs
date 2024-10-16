using System.Windows;
using System.Windows.Controls;

namespace Live_Server
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void AppendText(string text)
        {

       
            if (OutputTextBox.Dispatcher.CheckAccess())
            {
                // We're on the UI thread
                OutputTextBox.AppendText(text + Environment.NewLine);
                OutputTextBox.ScrollToEnd();
            }
            else
            {
                // We're on a different thread, invoke the update on the UI thread
                OutputTextBox.Dispatcher.Invoke(() =>
                {
                    VS.MessageBox.Show("Live_Server", text);
                    OutputTextBox.AppendText(text + Environment.NewLine);
                    OutputTextBox.ScrollToEnd();
                });
            }
        }

    }
}