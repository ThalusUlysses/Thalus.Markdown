using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace Thalus.Markdown.Controls.ViewModels
{
    /// <summary>
    /// Implements the <see cref="MarkdownRenderControlModel"/> functionality to render a
    /// text as markdwon
    /// </summary>
    public class MarkdownRenderControlModel : ViewModelBase
    {
        private string _markdownText;
        private bool _isSelected;
        private bool _isNormal;
        private bool _isRaw;
        private bool _isButtonBarVisible;

        /// <summary>
        /// Creates an instance of <see cref="MarkdownRenderControlModel"/> with the passed
        /// parameters 
        /// </summary>
        /// <param name="text">Pass the to be rendered text as markdown</param>
        /// <param name="buttonBarVisible"></param>
        public MarkdownRenderControlModel(string text, bool buttonBarVisible = false)
        {
            MarkdownText = text;
            Toggle = new RelayCommand(ExecuteToggle);
            IsNormal = true;

            _isButtonBarVisible = buttonBarVisible;

            PerformSaveCommand = new RelayCommand(Execute);

        }
        public string FullName { get; set; }

        private void Execute(object o)
        {
            try
            {
                var file = FullName;
                var text = MarkdownText;
                File.WriteAllText(file, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool IsButtonBarVisible
        {
            get { return _isButtonBarVisible; }
            set
            {
                _isButtonBarVisible = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsRaw
        {
            get { return _isRaw; }
            set
            {
                _isRaw = value;
                OnPropertyChanged();
            }
        }

        public bool IsNormal
        {
            get { return _isNormal; }
            set
            {
                _isNormal = value;
                OnPropertyChanged();
                
            }
        }

        private void ExecuteToggle(object o)
        {
            if (IsRaw)
            {
                IsRaw = false;
                IsNormal = true;
            }
            else if(IsNormal)
            {
                IsNormal = false;
                IsRaw = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value)
                {
                    return;
                }
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public ICommand Toggle { get; set; }

        /// <summary>
        /// Gets the markdown tetx representation as <see cref="string"/>
        /// </summary>
        public string MarkdownText
        {
            get => _markdownText;
            set
            {
                if (_markdownText == value)
                {
                    return;
                }

                _markdownText = value;

                OnPropertyChanged();
            }
        }

        public ICommand PerformSaveCommand { get; set; }

    }
}