using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Thalus.Markdown.Controls.DragDrop
{
    /// <summary>
    /// Implemnets a simple drag and drop functionality using <see cref="EventHandler"/>
    /// </summary>
    /// <typeparam name="TTargetDataContext">Data context type of bound control</typeparam>
    /// <typeparam name="TDroppedDataContext">Data context type of dragged data</typeparam>
    public abstract class ItemsControlDragDrop<TDroppedDataContext> : ItemDragDrop<TDroppedDataContext, TDroppedDataContext> where TDroppedDataContext : class
    {
        private ItemsControl _ctrl;
        private Selector _selector;

        protected ItemsControlDragDrop(ItemsControl ctrl)
        {
            _ctrl = ctrl;
            _selector = ctrl as Selector;
        }

        protected abstract IList GetTargetList();

        protected override DragDropEffects InteralGetEffects()
        {
            return DragDropEffects.Move;
        }

        protected override void InternalDragEnter(TDroppedDataContext target, TDroppedDataContext draggedData, DragEventArgs e)
        {
        }

        protected override void InternalDragLeave(TDroppedDataContext target, TDroppedDataContext draggedData, DragEventArgs e)
        {
            
        }
        protected override void InternalDragOver(TDroppedDataContext target, TDroppedDataContext draggedData, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy | DragDropEffects.Move;
        }

        protected override void InternalHandleDrop(TDroppedDataContext target, TDroppedDataContext draggedData,
            DragEventArgs e)
        {
            int removedIdx = _ctrl.Items.IndexOf(draggedData);
            int targetIdx = _ctrl.Items.IndexOf(target);

            var pData = GetTargetList();

            if (removedIdx < targetIdx)
            {
                pData.Insert(targetIdx + 1, draggedData);
                pData.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (pData.Count + 1 > remIdx)


                    pData.Insert(targetIdx, draggedData);
                pData.RemoveAt(remIdx);
            }

            if (_selector != null)
            {
                _selector.SelectedItem = draggedData;
            }
        }
    }
}