using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

            if (args.Any(i => i == "."))
            {
                args = Directory.GetFiles(Environment.CurrentDirectory, "*.md");
            }

            try
            {
                foreach (string s in args)
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
