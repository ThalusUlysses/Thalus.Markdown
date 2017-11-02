using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Thalus.Markdown.Pad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainViewModel GetDataContext()
        {
            var args = Environment.GetCommandLineArgs();

            MainViewModel mdl = new MainViewModel();

            try
            {
                foreach (string s in args.Where(i=> !i.EndsWith(".exe")))
                {
                    FileInfo fi = new FileInfo(s);
                    if (fi.Exists)
                    {
                        var st = File.ReadAllText(fi.FullName);

                        var item = mdl.Tabs.AddItem();

                        item.HeaderText = fi.Name;
                        item.MarkdownText = st;
                        item.FullName = fi.FullName;
                    }
                }

                return mdl;

            }
            catch
            {
                return new MainViewModel();
            }
        }

    }
}
