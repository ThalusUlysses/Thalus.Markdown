using System.Collections.ObjectModel;
using System.Linq;

namespace Thalus.Markdown.Controls.ViewModels
{
    public class MarkdownRenderTabControlModel : ViewModelBase
    {
        private bool _canEditMarkdwon;
        public MarkdownRenderTabControlModel(bool canEditMarkdwon)
        {
            _canEditMarkdwon = canEditMarkdwon;
            Items = new ObservableCollection<MarkdownRenderTabItemControlModel>();


        }

        public bool HasItems
        {
            get => _hasItems;
            private set
            {
                if (_hasItems == value)
                {
                    return;
                }
                _hasItems = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<MarkdownRenderTabItemControlModel> _items;
        private MarkdownRenderTabControlModel _parent;
        private bool _hasParent;
        private bool _hasItems;
        
        public ObservableCollection<MarkdownRenderTabItemControlModel> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                {
                    return;
                }
                _items = value;
                OnPropertyChanged();

            }
        }

        public MarkdownRenderTabItemControlModel AddItem()
        {
            foreach (MarkdownRenderTabItemControlModel item in Items)
            {
                item.IsSelected = false;
            }

            var ctrl = new MarkdownRenderTabItemControlModel(null, _canEditMarkdwon, RemoveAction);
            Items.Add(ctrl);

            ctrl.IsSelected = true;
            HasItems = _items != null && _items.Any();

            return ctrl;
        }

        private void RemoveAction(object o)
        {
            var t = Items.FirstOrDefault(i => i.IsSelected);

            if (t != null)
            {
                RemoveItem(t);
            }
        }

        public void AddItem(MarkdownRenderTabItemControlModel ctrl)
        {
            foreach (MarkdownRenderTabItemControlModel item in Items)
            {
                item.IsSelected = false;
            }

            Items.Add(ctrl);

            ctrl.IsSelected = true;
            HasItems = _items != null && _items.Any();
        }

        public void RemoveItem(MarkdownRenderTabItemControlModel ctrl)
        {
            Items.Remove(ctrl);
            HasItems = _items != null && _items.Any();
        }
    }
}