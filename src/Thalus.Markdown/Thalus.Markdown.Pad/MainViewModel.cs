using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thalus.Markdown.Controls.ViewModels;

namespace Thalus.Markdown.Pad
{
    public class MainViewModel : ViewModelBase
    {
        private MarkdownRenderTabControlModel _tabs;
        public MainViewModel()
        {
            Tabs = new MarkdownRenderTabControlModel(true);
        }


        public MarkdownRenderTabControlModel Tabs
        {
            get => _tabs;
            set
            {
                if (_tabs == value)
                {
                    return;
                }
                _tabs = value;
                OnPropertyChanged();
            }
        }
    }
}
