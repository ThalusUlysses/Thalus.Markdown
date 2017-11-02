using System;
using System.Windows.Input;

namespace Thalus.Markdown.Controls.ViewModels
{
    public class MarkdownRenderTabItemControlModel : MarkdownRenderControlModel
    {
        private string _headerText;

        public string HeaderText
        {
            get => _headerText;
            set
            {
                if (_headerText == value)
                {
                    return;
                }
                _headerText = value;
                OnPropertyChanged();
            }
        }


        public MarkdownRenderTabItemControlModel(string text, bool buttonbarvisible, Action<Object> action) : base(text, buttonbarvisible)
        {
            CloseCommand = new RelayCommand(action);

        }

        public ICommand CloseCommand { get; set; }
    }
}