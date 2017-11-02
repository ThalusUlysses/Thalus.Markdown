using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thalus.Markdown.Controls.ViewModels;

namespace Thalus.Markdown.Controls
{
    /// <summary>
    /// Interaction logic for MArkdownRenderTabControl.xaml
    /// </summary>
    public partial class MarkdownRenderTabControl : UserControl
    {
        public MarkdownRenderTabControl()
        {
            InitializeComponent();
        }

        private void MarkdownRenderTabControl_OnDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // Note that you can have more than one file.
                    string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (!fi.Exists || fi.Extension != ".md")
                        {
                            continue;
                        }

                        var txt = File.ReadAllText(fi.FullName);

                        MarkdownRenderTabItemControlModel itm = ((MarkdownRenderTabControlModel) DataContext).AddItem();

                        itm.HeaderText = fi.Name;
                        itm.MarkdownText = txt;
                        itm.FullName = fi.FullName;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
