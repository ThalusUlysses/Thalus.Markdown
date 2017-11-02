using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Thalus.Markdown.Controls.ViewModels;

namespace Thalus.Markdown.Controls
{
    /// <summary>
    /// Interaction logic for MarkdownRenderControl.xaml
    /// </summary>
    public partial class MarkdownRenderControl : UserControl
    {
        public MarkdownRenderControl()
        {
            InitializeComponent();

        }

        

        private void CanGoToPage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PerformGoToPage(object sender, ExecutedRoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Parameter.ToString()));
            e.Handled = true;
        }

    }
}
