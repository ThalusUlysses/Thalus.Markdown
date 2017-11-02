using System;
using System.Windows;
using System.Windows.Input;

namespace Thalus.Markdown.Controls.DragDrop
{
    /// <summary>
    /// Implemnets a simple drag and drop functionality using <see cref="EventHandler"/>
    /// </summary>
    /// <typeparam name="TTargetDataContext">Data context type of bound control</typeparam>
    /// <typeparam name="TDroppedDataContext">Data context type of dragged data</typeparam>
    public abstract class ItemDragDrop<TTargetDataContext, TDroppedDataContext> where TTargetDataContext : class where TDroppedDataContext : class
    {
        /// <summary>
        /// Called when a drop event needs to be handled
        /// </summary>
        /// <param name="target">Pass the target data of the target where to drop to</param>
        /// <param name="draggedData">Pass the dragged data of the target to drag from</param>
        /// <param name="e">Pass the <see cref="DragEventArgs"/></param>
        protected abstract void InternalHandleDrop(TTargetDataContext target, TDroppedDataContext draggedData, DragEventArgs e);
       
        /// <summary>
        /// Called when a enter event needs to be handled
        /// </summary>
        /// <param name="target">Pass the target data of the target where to drop to</param>
        /// <param name="draggedData">Pass the dragged data of the target to drag from</param>
        /// <param name="e">Pass the <see cref="DragEventArgs"/></param>
        protected abstract void InternalDragEnter(TTargetDataContext target, TDroppedDataContext draggedData, DragEventArgs e);

        /// <summary>
        /// Called when a drag leave event needs to be handled
        /// </summary>
        /// <param name="target">Pass the target data of the target where to drop to</param>
        /// <param name="draggedData">Pass the dragged data of the target to drag from</param>
        /// <param name="e">Pass the <see cref="DragEventArgs"/></param>
        protected abstract void InternalDragLeave(TTargetDataContext target, TDroppedDataContext draggedData, DragEventArgs e);


        /// <summary>
        /// Called when a drag over event needs to be handled
        /// </summary>
        /// <param name="target">Pass the target data of the target where to drop to</param>
        /// <param name="draggedData">Pass the dragged data of the target to drag from</param>
        /// <param name="e">Pass the <see cref="DragEventArgs"/></param>
        protected abstract void InternalDragOver(TTargetDataContext target, TDroppedDataContext draggedData, DragEventArgs e);

        /// <summary>
        /// Gets the drag effect
        /// </summary>
        /// <returns></returns>
        protected abstract DragDropEffects InteralGetEffects();

        /// <summary>
        /// Safely handles Drop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDrop(object sender, DragEventArgs e)
        {
            try
            {
                var elm = sender as FrameworkElement;

                if (elm?.DataContext == null || !HasDropData<TDroppedDataContext>(e))
                {
                    return;
                }

                var item = elm.DataContext as TTargetDataContext;
                InternalHandleDrop(item, GetItem<TDroppedDataContext>(e), e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to drop target to {sender} {ex}");
            }

            Console.WriteLine("End drag drop action");

            _point = null;
            _dragging = false;
        }


        private bool HasDropData<TItem>(DragEventArgs e)
        {
            return e.Data.GetDataPresent(typeof(TItem));
        }

        private TItem GetItem<TItem>(DragEventArgs e)
        {
            return (TItem)e.Data.GetData(typeof(TItem));
        }

        /// <summary>
        /// Safely handles drag enter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDragEnter(object sender, DragEventArgs e)
        {
            try
            {
                var elm = sender as FrameworkElement;

                if (elm?.DataContext == null || !HasDropData<TDroppedDataContext>(e))
                {
                    return;
                }

                var item = elm.DataContext as TTargetDataContext;
                InternalDragEnter(item, GetItem<TDroppedDataContext>(e), e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to drag enter target to {sender} {ex}");
            }
        }

        /// <summary>
        /// Safely handles drag over event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDragOver(object sender, DragEventArgs e)
        {
            try
            {
                var elm = sender as FrameworkElement;

                if (elm?.DataContext == null || !HasDropData<TDroppedDataContext>(e))
                {
                    return;
                }

                var item = elm.DataContext as TTargetDataContext;
                InternalDragOver(item, GetItem<TDroppedDataContext>(e), e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to drag over target to {sender} {ex}");
            }
        }

        /// <summary>
        /// Safely handles drag leave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDragLeave(object sender, DragEventArgs e)
        {
            try
            {
                var elm = sender as FrameworkElement;

                if (elm?.DataContext == null || !HasDropData<TDroppedDataContext>(e))
                {
                    return;
                }

                var item = elm.DataContext as TTargetDataContext;
                InternalDragLeave(item, GetItem<TDroppedDataContext>(e), e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to drag leave target to {sender} {ex}");
            }
        }

        private bool _dragging;
        private Point? _point = null;
        /// <summary>
        /// Safely handles mouse move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleOnMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                FrameworkElement elm = sender as FrameworkElement;

                if (!(elm?.DataContext is TTargetDataContext) || e.LeftButton != MouseButtonState.Pressed)
                {
                    return;
                }

                Point current = e.GetPosition(elm);
                if (_point == null)
                {
                    _point = current;
                    return;
                }

                if (_dragging)
                {
                    return;
                }

                var p = current - _point;

                if (p.Value.Length > 15)
                {
                    Console.WriteLine("Started drag drop action");
                    _point = null;

                    var effects = InteralGetEffects();
                    _dragging = true;
                    System.Windows.DragDrop.DoDragDrop(elm, elm.DataContext, effects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to start drag target to {sender} {ex}");
                _dragging = false;
            }
        }
    }
    
}