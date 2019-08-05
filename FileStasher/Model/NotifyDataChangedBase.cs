using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Model
{
    public class NotifyDataChangedBase : INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// This event gets called, when any of the properties change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// This event gets called when a collection changes
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Fires the CollectionChanged event
        /// </summary>
        /// <param name="action">The type of the change that occured on the collection</param>
        internal void NotifyCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Fires the PropertyChanged event
        /// </summary>
        /// <param name="propName">The name of the property that changed. By default this is the name of the caller member</param>
        internal void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
