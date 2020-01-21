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
    public class LogEventCollection : IDisposable, IList<LogEvent>, IBindingListView
    {
        #region Constants
        private const int MAX_EVENT_TOLERANCE = 1000;
        #endregion

        #region Private Members
        private LogDataSource _dataSource;
        private List<LogEvent> _all;
        private List<LogEvent> _shown;
        private LogEventFilter _filter;
        private LogEventComparer _sorter;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventCollection"/> class.
        /// </summary>
        /// <param name="dataSource">The data source that the list must open and close when disposed.</param>
        public LogEventCollection(LogDataSource dataSource)
        {
            LogEvent[] initialEvents = null;

            if (dataSource == null)
                throw new ArgumentNullException("dataSource");

            _dataSource = dataSource;
            _dataSource.NewLog += new NewLogEventHandler(HandleNewLogEvent);
            _dataSource.Reset += new ResetEventHandler(HandleReset);

            initialEvents = dataSource.Open();

            _all = new List<LogEvent>(initialEvents.Length);
            _shown = _all;
            _filter = null;
            _sorter = null;

            AddCore(initialEvents);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the unfiltered number of items within the collection.
        /// </summary>
        /// <value>The unfiltered number of items within the collection.</value>
        public int UnfilteredCount
        {
            get { lock (this) { return _all.Count; } }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds a new item at the end of the list.
        /// </summary>
        /// <param name="item">The list of items to add.</param>
        private void AddCore(LogEvent[] items)
        {
            int shownIndex = -1;
            int skipItems = 0;
            bool forceResetNotify = false;

            if (items == null || items.Length == 0)
                return;

            lock (this)
            {
                // Truncate if required
                if (items.Length > Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents)
                {
                    skipItems = items.Length - Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents;
                    _all.Clear();

                    ApplySortAndFilter(false);
                    forceResetNotify = true;
                }
                else if (_all.Count + items.Length >= Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents + MAX_EVENT_TOLERANCE)
                {
                    _all.RemoveRange(0, (_all.Count + items.Length) - Triamun.Log4NetViewer.Data.Config.Config.Current.MaxEvents);

                    ApplySortAndFilter(false);
                    forceResetNotify = true;
                }
                for (int i = skipItems; i < items.Length; i++)
                {
                    _all.Add(items[i]);

                    if (_filter == null || _filter.IsMatch(items[i]))
                    {
                        if (_sorter != null)
                        {
                            // Easy if sorted, we add, resort and locate the index of the item
                            shownIndex = _shown.BinarySearch(items[i], _sorter);
                            _shown.Insert(shownIndex < 0 ? ~shownIndex : shownIndex, items[i]);
                        }
                        else if (_filter != null)
                        {
                            // If not sorted, we insert at the end !
                            _shown.Add(items[i]);
                            shownIndex = _shown.Count - 1;
                        }
                        else
                        {
                            // No filtering and no sorting, shown and complete list are the same !
                            shownIndex = _all.Count - 1;
                        }
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

        /// <summary>
        /// Resets the list so that it now contains only the specified events.
        /// </summary>
        /// <param name="items">The items.</param>
        private void ResetCore(LogEvent[] items)
        {
            lock (this)
            {
                _all.Clear();
                if (items != null && items.Length > 0)
                    _all.AddRange(items);
                ApplySortAndFilter(true);
            }
        }

        /// <summary>
        /// Rebuilds the shown list so that it reflects the current filter and sort.
        /// </summary>
        /// <param name="notify">if set to <c>true</c> the change.</param>
        private void ApplySortAndFilter(bool notify)
        {
            Console.WriteLine("Starting filtering...");
            // No filter !
            if (_filter == null && _sorter == null)
            {
                _shown = _all;
                if (notify)
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
            {
                // Reset the shown list in the most optimal way.
                if (_shown == _all)
                {
                    if (_filter == null)
                        _shown = new List<LogEvent>(_all.Count);
                    else
                        _shown = new List<LogEvent>();
                }
                else
                    _shown.Clear();

                // Fills the shown list based on the filter
                if (_filter == null)
                    _shown.AddRange(_all);
                else
                {
                    foreach (LogEvent item in _all)
                    {
                        if (_filter.IsMatch(item))
                            _shown.Add(item);
                    }
                }

                // Sort if requied
                if (_sorter != null && _shown.Count > 0)
                    _shown.Sort(_sorter);

                if (notify)
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            Console.WriteLine("Finished filtering...");
        }

        /// <summary>
        /// Handles the new log event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NewLogEventArgs"/> instance containing the event data.</param>
        void HandleNewLogEvent(object sender, NewLogEventArgs e)
        {
            if (e.Events != null)
                AddCore(e.Events);
        }

        /// <summary>
        /// Handles the reset of the data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ResetEventArgs"/> instance containing the event data.</param>
        void HandleReset(object sender, ResetEventArgs e)
        {
            ResetCore(e.Events);
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
        #endregion

        #region Protected Methods
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
                _sorter = null;
                _filter = null;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ListChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ListChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (ListChanged != null)
                ListChanged(this, e);
        }
        #endregion

        #region IList<LogEvent> Members
        /// <summary>
        /// Determines the index of a specific item in the <see cref="LogEvent"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="LogEvent"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(LogEvent item)
        {
            lock (this)
            {
                return LocateItemInList(item, _shown);
            }
        }

        /// <summary>
        /// Inserts an item to the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the collection.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the collection.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The collection is read-only.
        /// </exception>
        public void Insert(int index, LogEvent item)
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
        public void RemoveAt(int index)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Gets or sets the <see cref="LogEventCollection"/> at the specified index.
        /// </summary>
        /// <value>The index for which to return the item.</value>
        public LogEvent this[int index]
        {
            get { lock (this) { return _shown[index]; } }
            set { throw new NotSupportedException("The collection is read-only."); }
        }

        #endregion

        #region ICollection<LogEvent> Members

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Add(LogEvent item)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Clear()
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(LogEvent item)
        {
            lock (this)
            {
                return LocateItemInList(item, _shown) >= 0;
            }
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        /// -or-
        /// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(LogEvent[] array, int arrayIndex)
        {
            lock (this)
            {
                _shown.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { lock (this) { return _shown.Count; } }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(LogEvent item)
        {
            throw new NotSupportedException("The collection is read-only.");
        }
        #endregion

        #region IBindingListView Members

        /// <summary>
        /// Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/>.
        /// </summary>
        /// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/> containing the sorts to apply to the data source.</param>
        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            if (sorts == null || sorts.Count == 0)
                RemoveSort();
            else if (sorts.Count == 1)
                ApplySort(sorts[0].PropertyDescriptor, sorts[0].SortDirection);
            else
                throw new NotSupportedException("The collection does not support sorting on multiple columns.");
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
                lock (this)
                {
                    if (_filter == null)
                        return String.Empty;
                    return _filter.ToString();
                }
            }
            set
            {
                lock (this)
                {
                    if (String.IsNullOrEmpty(value) && _filter != null)
                    {
                        _filter = null;
                        ApplySortAndFilter(true);
                    }
                    else if (!String.IsNullOrEmpty(value) && (_filter == null || _filter.ToString() != value))
                    {
                        _filter = new LogEventFilter(value);
                        ApplySortAndFilter(true);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the current filter applied to the data source.
        /// </summary>
        public void RemoveFilter()
        {
            Filter = String.Empty;
        }

        /// <summary>
        /// Gets the collection of sort descriptions currently applied to the data source.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.ListSortDescriptionCollection"/> currently applied to the data source.
        /// </returns>
        public ListSortDescriptionCollection SortDescriptions
        {
            get
            {
                lock (this)
                {
                    if (_sorter == null)
                        return new ListSortDescriptionCollection();
                    return new ListSortDescriptionCollection(new ListSortDescription[] { new ListSortDescription(_sorter.Property, _sorter.SortDirection) });
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the data source supports advanced sorting.
        /// </summary>
        /// <value></value>
        /// <returns>true if the data source supports advanced sorting; otherwise, false.
        /// </returns>
        public bool SupportsAdvancedSorting
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the data source supports filtering.
        /// </summary>
        /// <value></value>
        /// <returns>true if the data source supports filtering; otherwise, false.
        /// </returns>
        public bool SupportsFiltering
        {
            get { return true; }
        }
        #endregion

        #region IBindingList Members

        /// <summary>
        /// Adds the <see cref="T:System.ComponentModel.PropertyDescriptor"/> to the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to add to the indexes used for searching.</param>
        public void AddIndex(PropertyDescriptor property)
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
        public object AddNew()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets whether you can update items in the list.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can update the items in the list; otherwise, false.
        /// </returns>
        public bool AllowEdit
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>; otherwise, false.
        /// </returns>
        public bool AllowNew
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)"/> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)"/>.
        /// </summary>
        /// <value></value>
        /// <returns>true if you can remove items from the list; otherwise, false.
        /// </returns>
        public bool AllowRemove
        {
            get { return false; }
        }

        /// <summary>
        /// Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor"/> and a <see cref="T:System.ComponentModel.ListSortDirection"/>.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to sort by.</param>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            lock (this)
            {
                _sorter = new LogEventComparer(direction, property);
                ApplySortAndFilter(true);
            }
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
        public int Find(PropertyDescriptor property, object key)
        {
            lock (this)
            {
                for (int i = 0; i < _shown.Count; i++)
                {
                    if (Object.Equals(property.GetValue(_shown[i]), key))
                        return i;
                }

                return -1;
            }
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
        public bool IsSorted
        {
            get { lock (this) { return _sorter != null; } }
        }

        public event ListChangedEventHandler ListChanged;

        /// <summary>
        /// Removes the <see cref="T:System.ComponentModel.PropertyDescriptor"/> from the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to remove from the indexes used for searching.</param>
        public void RemoveIndex(PropertyDescriptor property)
        {
            // Nothing done here
        }

        /// <summary>
        /// Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
        /// </exception>
        public void RemoveSort()
        {
            lock (this)
            {
                _sorter = null;
                ApplySortAndFilter(true);
            }
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
        public ListSortDirection SortDirection
        {
            get
            {
                lock (this)
                {
                    if (_sorter == null)
                        return ListSortDirection.Ascending;
                    return _sorter.SortDirection;
                }
            }
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
        public PropertyDescriptor SortProperty
        {
            get
            {
                lock (this)
                {
                    if (_sorter == null)
                        return null;
                    return _sorter.Property;
                }
            }
        }

        /// <summary>
        /// Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or an item in the list changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or when an item changes; otherwise, false.
        /// </returns>
        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method.
        /// </summary>
        /// <value></value>
        /// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method; otherwise, false.
        /// </returns>
        public bool SupportsSearching
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether the list supports sorting.
        /// </summary>
        /// <value></value>
        /// <returns>true if the list supports sorting; otherwise, false.
        /// </returns>
        public bool SupportsSorting
        {
            get { return true; }
        }

        #endregion

        #region IList Members

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
        public int Add(object value)
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
        public bool Contains(object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"/> to locate in the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>
        /// The index of <paramref name="value"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(object value)
        {
            lock (this)
            {
                return LocateItemInList(value as LogEvent, _shown);
            }
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
        public void Insert(int index, object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.IList"/> has a fixed size; otherwise, false.
        /// </returns>
        public bool IsFixedSize
        {
            get { return false; }
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
        public void Remove(object value)
        {
            throw new NotSupportedException("The collection is read-only.");
        }

        /// <summary>
        /// Gets or sets the <see cref="LogEventCollection"/> at the specified index.
        /// </summary>
        /// <value>The index for which to return the item.</value>
        object System.Collections.IList.this[int index]
        {
            get { lock (this) { return _shown[index]; } }
            set { throw new NotSupportedException("The collection is read-only."); }
        }
        #endregion

        #region ICollection Members

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
        public void CopyTo(Array array, int index)
        {
            lock (this)
            {
                ((System.Collections.ICollection)_shown).CopyTo(array, index);
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        public bool IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot
        {
            get { return this; }
        }

        #endregion

        #region IEnumerable<LogEvent> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<LogEvent> GetEnumerator()
        {
            lock (this)
            {
                return _shown.GetEnumerator();
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Nested Classes
        /// <summary>
        /// Compares two <see cref="LogEvent"/> instances based on the a specific property.
        /// </summary>
        private class LogEventComparer : IComparer<LogEvent>
        {
            #region Private Members
            private ListSortDirection _sortDirection;
            private PropertyDescriptor _property;
            private Func<LogEvent, LogEvent, int> _compareMethod;
            #endregion

            #region Constructor
            /// <summary>
            /// Initializes a new instance of the <see cref="LogEventComparer"/> class.
            /// </summary>
            /// <param name="sortDirection">The sort direction.</param>
            /// <param name="property">The property to use to compare the <see cref="LogEvent"/> instances.</param>
            public LogEventComparer(ListSortDirection sortDirection, PropertyDescriptor property)
            {
                if (property == null)
                    throw new ArgumentNullException("property");

                _sortDirection = sortDirection;
                _property = property;

                switch (property.Name)
                {
                    case "Logger":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByLogger);
                        break;
                    case "TimeStamp":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByTimeStamp);
                        break;
                    case "Level":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByLevel);
                        break;
                    case "Thread":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByThread);
                        break;
                    case "Domain":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByDomain);
                        break;
                    case "UserName":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByUserName);
                        break;
                    case "Message":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByMessage);
                        break;
                    case "Exception":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByException);
                        break;
                    case "ClassName":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByClassName);
                        break;
                    case "MethodName":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByMethodName);
                        break;
                    case "FileName":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByFileName);
                        break;
                    case "LineNumber":
                        _compareMethod = new Func<LogEvent, LogEvent, int>(CompareByLineNumber);
                        break;
                    default:
                        _compareMethod = null;
                        break;
                }
            }
            #endregion

            #region Public Properties
            /// <summary>
            /// Gets the sort direction.
            /// </summary>
            /// <value>The sort direction.</value>
            public ListSortDirection SortDirection
            {
                get { return _sortDirection; }
            }

            /// <summary>
            /// Gets the <see cref="PropertyDescriptor"/> specified at the constructor.
            /// </summary>
            /// <value>The <see cref="PropertyDescriptor"/> specified at the constructor.</value>
            public PropertyDescriptor Property
            {
                get { return _property; }
            }
            #endregion

            #region Private Methods
            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Logger"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByLogger(LogEvent x, LogEvent y)
            {
                return String.Compare(x.Logger, y.Logger);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.TimeStamp"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByTimeStamp(LogEvent x, LogEvent y)
            {
                return DateTime.Compare(x.TimeStamp, y.TimeStamp);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Level"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByLevel(LogEvent x, LogEvent y)
            {
                return (int)x.Level - (int)y.Level;
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Thread"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByThread(LogEvent x, LogEvent y)
            {
                return String.Compare(x.Thread, y.Thread);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Domain"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByDomain(LogEvent x, LogEvent y)
            {
                return String.Compare(x.Domain, y.Domain);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.UserName"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByUserName(LogEvent x, LogEvent y)
            {
                return String.Compare(x.UserName, y.UserName);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Message"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByMessage(LogEvent x, LogEvent y)
            {
                return String.Compare(x.Message, y.Message);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.Exception"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByException(LogEvent x, LogEvent y)
            {
                return String.Compare(x.Exception, y.Exception);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.ClassName"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByClassName(LogEvent x, LogEvent y)
            {
                return String.Compare(x.ClassName, y.ClassName);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.MethodName"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByMethodName(LogEvent x, LogEvent y)
            {
                return String.Compare(x.MethodName, y.MethodName);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.FileName"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByFileName(LogEvent x, LogEvent y)
            {
                return String.Compare(x.FileName, y.FileName);
            }

            /// <summary>
            /// Compares two <see cref="LogEvent"/> based on the <see cref="LogEvent.LineNumber"/> property and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            private int CompareByLineNumber(LogEvent x, LogEvent y)
            {
                return x.LineNumber - y.LineNumber;
            }
            #endregion

            #region IComparer<LogEvent> Members

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first <see cref="LogEvent"/> to compare.</param>
            /// <param name="y">The second <see cref="LogEvent"/> to compare.</param>
            /// <returns>
            /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
            /// Zero: <paramref name="x"/> equals <paramref name="y"/>.
            /// Greater than zero:<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            public int Compare(LogEvent x, LogEvent y)
            {
                int res = 0;

                if (x == null)
                {
                    if (y != null)
                        res = -1;
                }
                else if (y == null)
                    res = 1;
                else if (_compareMethod != null)
                    res = _compareMethod(x, y);

                if (SortDirection == ListSortDirection.Ascending)
                    return res;

                return -res;
            }

            #endregion
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
