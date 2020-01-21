using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Triamun.Log4NetViewer.Data.Sources;

namespace Triamun.Log4NetViewer.Data
{
    /// <summary>
    /// Represents a bindable collection of <see cref="LogEvent"/> that can be bound, sorted and filtered.
    /// </summary>
    public class LogEventCollection : IDisposable, IBindingListView
    {
        #region Constants

        private const int MAX_EVENT_TOLERANCE = 1000;

        #endregion Constants

        #region Private Members

        private LogDataSource _dataSource;
        private List<LogEvent> _all;
        private List<LogEvent> _shown;
        private LogEventFilter _filter;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventCollection"/> class.
        /// </summary>
        /// <param name="dataSource">The data source that the list must open and close when disposed.</param>
        public LogEventCollection()
        {
            _all = new List<LogEvent>();
            _shown = _all;
            _filter = null;
        }

        #endregion Constructor

        #region Public Events

        /// <summary>
        /// Occurs when the list changes or an item in the list changes.
        /// </summary>
        public event ListChangedEventHandler ListChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return _shown.Count; }
        }

        /// <summary>
        /// Gets or sets the filter to be used to exclude items from the collection of items returned by the data source
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The string used to filter items out in the item collection returned by the data source.
        /// </returns>
        public string Filter
        {
            get
            {
                if (_filter == null)
                    return String.Empty;
                return _filter.ToString();
            }
            set
            {
                if (String.IsNullOrEmpty(value) && _filter != null)
                {
                    _filter = null;
                    ApplyFilter(true);
                }
                else if (!String.IsNullOrEmpty(value) && (_filter == null || _filter.ToString() != value))
                {
                    _filter = new LogEventFilter(value);
                    ApplyFilter(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="LogEventCollection"/> at the specified index.
        /// </summary>
        /// <value>The index for which to return the item.</value>
        public LogEvent this[int index]
        {
            get { return _shown[index]; }
            set { throw new NotSupportedException("The collection is read-only."); }
        }

        /// <summary>
        /// Gets the unfiltered number of items within the collection.
        /// </summary>
        /// <value>The unfiltered number of items within the collection.</value>
        public int UnfilteredCount
        {
            get { return _all.Count; }
        }

        #endregion Public Properties

        #region Private Properties

        /// <summary>
        /// Gets whether you can update items in the list.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can update the items in the list; otherwise, false.
        /// </returns>
        bool IBindingList.AllowEdit
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>; otherwise, false.
        /// </returns>
        bool IBindingList.AllowNew
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)"/> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)"/>.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can remove items from the list; otherwise, false.
        /// </returns>
        bool IBindingList.AllowRemove
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether the items in the list are sorted.
        /// </summary>
        /// <value></value>
        /// <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort"/> has not been called; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        bool IBindingList.IsSorted
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the direction of the sort.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        ListSortDirection IBindingList.SortDirection
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the <see cref="T:System.ComponentModel.PropertyDescriptor"/> that is being used for sorting.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.PropertyDescriptor"/> that is being used for sorting.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        PropertyDescriptor IBindingList.SortProperty
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or an item in the list changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or when an item changes; otherwise, false.
        /// </returns>
        bool IBindingList.SupportsChangeNotification
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method.
        /// </summary>
        /// <value></value>
        /// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method; otherwise, false.
        /// </returns>
        bool IBindingList.SupportsSearching
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether the list supports sorting.
        /// </summary>
        /// <value></value>
        /// <returns>true if the list supports sorting; otherwise, false.
        /// </returns>
        bool IBindingList.SupportsSorting
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the collection of sort descriptions currently applied to the data source.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/> currently applied to the data source.
        /// </returns>
        ListSortDescriptionCollection IBindingListView.SortDescriptions
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the data source supports advanced sorting.
        /// </summary>
        /// <value></value>
        /// <returns>true if the data source supports advanced sorting; otherwise, false.
        /// </returns>
        bool IBindingListView.SupportsAdvancedSorting
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the data source supports filtering.
        /// </summary>
        /// <value></value>
        /// <returns>true if the data source supports filtering; otherwise, false.
        /// </returns>
        bool IBindingListView.SupportsFiltering
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        object System.Collections.ICollection.SyncRoot
        {
            get { return null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.IList"/> has a fixed size; otherwise, false.
        /// </returns>
        bool System.Collections.IList.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        bool System.Collections.IList.IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the <see cref="LogEventCollection"/> at the specified index.
        /// </summary>
        /// <value>The index for which to return the item.</value>
        object System.Collections.IList.this[int index]
        {
            get { return _shown[index]; }
            set { throw new NotSupportedException("The collection is read-only."); }
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Find the event with the specified number.
        /// </summary>
        /// <param name="eventNumber">The event number to find.</param>
        /// <returns>
        /// The zero-based index of the log entry that matches the specified id; <c>-1</c> if no such log is found.
        /// </returns>
        public int FindEvent(int eventNumber)
        {
            int count = _shown.Count;

            for (int i = 0; i < count; i++)
            {
                if (_shown[i].EventNumber == eventNumber)
                    return i;
            }

            return -1;
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dataSource != null)
                    _dataSource.Dispose();

                _dataSource = null;
                _all = null;
                _shown = null;
                _filter = null;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ListChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ListChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            try
            {
                if (ListChanged != null)
                    ListChanged(this, e);
            }
            catch { }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Rebuilds the shown list so that it reflects the current filter.
        /// </summary>
        /// <param name="notify">if set to <c>true</c> the change.</param>
        private void ApplyFilter(bool notify)
        {
            List<LogEvent> newSownItems = null;

            // No filter !
            if (_filter == null)
            {
                newSownItems = _all;
            }
            else
            {
                // Reset the shown list in the most optimal way.
                if (_filter == null)
                    newSownItems = new List<LogEvent>(_all.Count);
                else
                    newSownItems = new List<LogEvent>();

                // Fills the shown list based on the filter
                foreach (LogEvent item in _all)
                {
                    if (_filter.IsMatch(item))
                        newSownItems.Add(item);
                }
            }

            _shown = newSownItems;
            if (notify)
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Adds the <see cref="T:System.ComponentModel.PropertyDescriptor"/> to the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to add to the indexes used for searching.</param>
        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            // Do nothing
        }

        /// <summary>
        /// Adds a new item to the list.
        /// </summary>
        /// <returns>The item added to the list.</returns>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.AllowNew"/> is false.
        /// </exception>
        object IBindingList.AddNew()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor"/> and a <see cref="T:System.ComponentModel.ListSortDirection"/>.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to sort by.</param>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"/>.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to search on.</param>
        /// <param name="key">The value of the <paramref name="property"/> parameter to search for.</param>
        /// <returns>
        /// The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSearching"/> is false.
        /// </exception>
        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            for (int i = 0; i < _shown.Count; i++)
            {
                if (Object.Equals(property.GetValue(_shown[i]), key))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Removes the <see cref="T:System.ComponentModel.PropertyDescriptor"/> from the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to remove from the indexes used for searching.</param>
        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            // Nothing done here
        }

        /// <summary>
        /// Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        void IBindingList.RemoveSort()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/>.
        /// </summary>
        /// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/> containing the sorts to apply to the data source.</param>
        void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
        {
            if (sorts == null || sorts.Count == 0)
                ((IBindingList)this).RemoveSort();
            else if (sorts.Count == 1)
                ((IBindingList)this).ApplySort(sorts[0].PropertyDescriptor, sorts[0].SortDirection);
            else
                throw new NotSupportedException("The collection does not support sorting on multiple columns.");
        }

        /// <summary>
        /// Removes the current filter applied to the data source.
        /// </summary>
        void IBindingListView.RemoveFilter()
        {
            Filter = String.Empty;
        }

        /// <summary>
        /// Locates the item reference in the specified list.
        /// </summary>
        /// <param name="item">The item to locate within <paramref name="list"/>.</param>
        /// <param name="list">The list of items into which to search for <paramref name="item"/>.</param>
        /// <returns>
        /// The zero-based index of <paramref name="item"/> within <paramref name="list"/> if the item could be found; -1 otherwise.
        /// </returns>
        private int LocateItemInList(LogEvent item, List<LogEvent> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (item == null)
                return -1;

            for (int i = 0; i < list.Count; i++)
            {
                if (Object.ReferenceEquals(item, _all[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            ((System.Collections.ICollection)_shown).CopyTo(array, index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _shown.GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"/> to add to the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>
        /// The position into which the new element was inserted.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.IList"/> is read-only.
        /// -or-
        /// The <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </exception>
        int System.Collections.IList.Add(object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        void System.Collections.IList.Clear()
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IList"/> contains a specific value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"/> to locate in the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>
        /// true if the <see cref="T:System.Object"/> is found in the <see cref="T:System.Collections.IList"/>; otherwise, false.
        /// </returns>
        bool System.Collections.IList.Contains(object value)
        {
            return LocateItemInList(value as LogEvent, _shown) >= 0;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"/> to locate in the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>
        /// The index of <paramref name="value"/> if found in the list; otherwise, -1.
        /// </returns>
        int System.Collections.IList.IndexOf(object value)
        {
            return LocateItemInList(value as LogEvent, _shown);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.IList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value"/> should be inserted.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to insert into the <see cref="T:System.Collections.IList"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.IList"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.IList"/> is read-only.
        /// -or-
        /// The <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </exception>
        /// <exception cref="T:System.NullReferenceException">
        /// 	<paramref name="value"/> is null reference in the <see cref="T:System.Collections.IList"/>.
        /// </exception>
        void System.Collections.IList.Insert(int index, object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"/> to remove from the <see cref="T:System.Collections.IList"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.IList"/> is read-only.
        /// -or-
        /// The <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </exception>
        void System.Collections.IList.Remove(object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the collection.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The collection is read-only.
        /// </exception>
        void System.Collections.IList.RemoveAt(int index)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        #endregion Private Methods

        /// <summary>
        /// Resets the list so that it now contains only the specified events.
        /// </summary>
        /// <param name="items">The items that the list must contain after the reset.</param>
        public void Reset(LogEvent[] items)
        {
            _all.Clear();
            if (items != null && items.Length > 0)
                _all.AddRange(items);
            ApplyFilter(true);
        }

        /// <summary>
        /// Adds new events at the end of the list.
        /// </summary>
        /// <param name="item">The list of events to add.</param>
        public void AddEvents(LogEvent[] items)
        {
            int shownIndex = -1;
            int skipItems = 0;
            bool forceResetNotify = false;

            if (items == null || items.Length == 0)
                return;

            // Truncate if required
            if (items.Length > Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents)
            {
                skipItems = items.Length - Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents;
                _all.Clear();

                ApplyFilter(false);
                forceResetNotify = true;
            }
            else if (_all.Count + items.Length >= Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents + MAX_EVENT_TOLERANCE)
            {
                _all.RemoveRange(0, (_all.Count + items.Length) - Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents);

                ApplyFilter(false);
                forceResetNotify = true;
            }

            for (int i = skipItems; i < items.Length; i++)
            {
                _all.Add(items[i]);

                if (_filter == null)
                {
                    // No filtering and no sorting, shown and complete list are the same !
                    shownIndex = _all.Count - 1;
                }
                else if (_filter.IsMatch(items[i]))
                {
                    // If not sorted, we insert at the end !
                    _shown.Add(items[i]);
                    shownIndex = _shown.Count - 1;
                }

                // Individual notification up to 10 items
                // Otherwise, reset at N-1 and added at N
                if (shownIndex >= 0 && ((items.Length <= 10 && !forceResetNotify) || i == items.Length - 1))
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, shownIndex));
                else if ((items.Length > 10 || forceResetNotify) && i == items.Length - 2)
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
    }
}
