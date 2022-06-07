using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Model
{

    /// <summary>
    /// Class taken from
    /// www.hardcodet.net/2008/12/heterogeneous-wpf-treeview
    /// </summary>
    public class FolderItem : INotifyPropertyChanged
    {
        #region Name

        /// <summary>
        /// The name that can be displayed or used as an
        /// ID to perform more complex styling.
        /// </summary>
        private string _name;


        /// <summary>
        /// The name that can be displayed or used as an
        /// ID to perform more complex styling.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Items

        /// <summary>
        /// The child items of the folder.
        /// </summary>
        private IEnumerable _items;


        /// <summary>
        /// The child items of the folder.
        /// </summary>
        public IEnumerable Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
