﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework
{
    public abstract class PropertyChangedNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void BindObject<T>(T obj)
            where T : INotifyPropertyChanged
        {
            if (obj != null)
            {
                obj.PropertyChanged += Obj_PropertyChanged;
            }
        }

        protected void UnbindObject<T>(T obj)
            where T : INotifyPropertyChanged
        {
            if (obj != null)
            {
                obj.PropertyChanged -= Obj_PropertyChanged;
            }
        }

        protected void BindObservableCollection<T>(ObservableCollection<T> collection)
        {
            collection.CollectionChanged += Collection_CollectionChanged;
        }

        protected void UnbindObservableCollection<T>(ObservableCollection<T> collection)
        {
            foreach(var item in collection)
            {
                if (item is PropertyChangedNotifier notifier)
                {
                    UnbindObject(notifier);
                }
            }

            collection.CollectionChanged -= Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(var item in e.NewItems)
                    {
                        if (item is PropertyChangedNotifier notifier)
                        {
                            BindObject(notifier);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(var item in e.OldItems)
                    {
                        if (item is PropertyChangedNotifier notifier)
                        {
                            UnbindObject(notifier);
                        }
                    }
                    break;
                default:
                    break;
            }

            this.OnPropertyChanged(sender.GetType().Name);
        }

        private void Obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(sender, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
