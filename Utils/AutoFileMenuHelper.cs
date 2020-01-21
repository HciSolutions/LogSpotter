using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Properties;

namespace Triamun.Log4NetViewer.Utils
{
    /// <summary>
    /// Helps into the creation of the automatic log files list menu.
    /// </summary>
    /// <remarks>
    /// The class creates menu item and uses their <see cref="MenuItem.Tag" /> property to store
    /// <see cref="DirectoryInfo" /> or <see cref="FileInfo" /> entries.
    /// </remarks>
    public class AutoFileMenuHelper
    {
        #region Private Constants

        private const string LOG_FILE_FILTER = "*.log4net";

        #endregion

        #region Private Fields

        private static readonly Regex EnvVarRegex = new Regex(@"^aplog\d*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex HistoryFileRegex = new Regex(@"^.*\.\d*\.log4net$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly SynchronizationContext _uiSyncContext;
        private readonly int _uiThreadId;

        #endregion

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="AutoFileMenuHelper" /> class from being created.
        /// </summary>
        private AutoFileMenuHelper()
        {
            _uiSyncContext = SynchronizationContext.Current;
            _uiThreadId = _uiSyncContext != null ? Thread.CurrentThread.ManagedThreadId : -1;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Populates the root menu.
        /// </summary>
        /// <param name="menu">The <see cref="ToolStripMenuItem" /> to populate.</param>
        /// <param name="clickEventHandler">The click event handler to assign to menus that represent files.</param>
        /// <remarks>
        /// The behavior of the method depends on the content of the <see cref="MenuItem.Tag" /> property of the specified
        /// <paramref name="menu" />.
        /// If the <see cref="MenuItem.Tag" /> property is null, the menu is considered as the root menu.
        /// If the <see cref="MenuItem.Tag" /> contains a <see cref="DirectoryInfo" /> instance, then child items are searched and
        /// the menu is populated.
        /// If the <see cref="MenuItem.Tag" /> contains a <see cref="FileInfo" /> instance, this method returns immediately.
        /// </remarks>
        public static void PopulateMenuAsync(ToolStripMenuItem menu, EventHandler clickEventHandler)
        {
            if (menu == null)
                return;

            var helper = new AutoFileMenuHelper();
            // Process asynchronously
            ThreadPool.QueueUserWorkItem(o1 => helper.PopulateMenu(menu, clickEventHandler));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the menu info recursive.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileSystemEntry">The file system entry.</param>
        /// <returns>
        /// The created <see cref="MenuInfo" />; <c>null</c> if no log files were found in
        /// <paramref name="fileSystemEntry" />.
        /// </returns>
        private MenuInfo CreateMenuInfoRecursive(string text, FileSystemInfo fileSystemEntry)
        {
            var fileEntry = fileSystemEntry as FileInfo;
            if (fileEntry != null)
                return new MenuInfo(text, fileSystemEntry, null);

            var dirEntry = fileSystemEntry as DirectoryInfo;
            if (dirEntry == null)
                return null;

            IEnumerable<MenuInfo> childFiles = dirEntry.EnumerateFiles(LOG_FILE_FILTER)
                                                       .Where(f => !HistoryFileRegex.IsMatch(f.Name))
                                                       .OrderBy(f => f.Name)
                                                       .Select(f => new MenuInfo(f.Name, f, null));
            IEnumerable<MenuInfo> childDirs = dirEntry.EnumerateDirectories()
                                                      .OrderBy(d => d.Name)
                                                      .Select(d => CreateMenuInfoRecursive(d.Name, d))
                                                      .Where(i => i != null);

            var result = new MenuInfo(
                text,
                fileSystemEntry,
                childFiles.Concat(childDirs).ToList());

            if (result.Childrens.Count > 0)
                return result;

            return null;
        }

        /// <summary>
        /// Creates the root menu info.
        /// </summary>
        /// <param name="name">The name of the menu.</param>
        /// <param name="path">The path for the menu.</param>
        /// <returns>
        /// A new <see cref="MenuInfo" />.
        /// </returns>
        private MenuInfo CreateRootMenuInfo(string name, string path)
        {
            var dirEntry = new DirectoryInfo(path);
            string text = GetRootMenuEntryDisplayText(name, path);

            return new MenuInfo(text, dirEntry, new MenuInfo[0]);
        }

        /// <summary>
        /// Creates the root menu entries.
        /// </summary>
        /// <param name="menu">The menu to which to add the root menu entries.</param>
        /// <returns>The collection of root menu infos created.</returns>
        private IEnumerable<RootMenuInfo> CreateRootMenuInfos(ToolStripMenuItem menu)
        {
            try
            {
                List<MenuInfo> rootInfos = Environment.GetEnvironmentVariables()
                                                      .OfType<DictionaryEntry>()
                                                      .Select(de => new { Name = Convert.ToString(de.Key), Path = Convert.ToString(de.Value) })
                                                      .Where(o => !string.IsNullOrEmpty(o.Name) && !string.IsNullOrEmpty(o.Path) && EnvVarRegex.IsMatch(o.Name))
                                                      .Select(o => CreateRootMenuInfo(o.Name, o.Path))
                                                      .OrderBy(i => i.Text, StringComparer.OrdinalIgnoreCase)
                                                      .ToList();

                return SetupLoadingRootMenus(menu, rootInfos);
            }
            catch
            {
                return SetupLoadingRootMenus(menu, Enumerable.Empty<MenuInfo>());
            }
        }

        /// <summary>
        /// Fills the children of the specified menu.
        /// </summary>
        /// <param name="menu">The menu to fill.</param>
        /// <param name="info">The information to use to fill.</param>
        /// <param name="clickEventHandler">The click event handler.</param>
        private void FillMenu(ToolStripMenuItem menu, MenuInfo info, EventHandler clickEventHandler)
        {
            if (RedirectOnUiThread(() => FillMenu(menu, info, clickEventHandler))) return;

            // Clean
            menu.DropDownItems.Clear();
            menu.Click -= clickEventHandler;
            menu.Tag = null;

            // Null case...
            if (info == null)
            {
                menu.Text = Resources.AutoLogFileMenuEmpty;
                return;
            }

            // Fill menu
            menu.Text = info.Text;
            if (info.FileSystemEntry is FileInfo)
            {
                menu.Click += clickEventHandler;
                menu.Tag = info.FileSystemEntry;
            }

            // Create children
            if (info.Childrens == null || info.Childrens.Count == 0) return;

            foreach (MenuInfo child in info.Childrens)
            {
                var childMenu = new ToolStripMenuItem();
                FillMenu(childMenu, child, clickEventHandler);
                menu.DropDownItems.Add(childMenu);
            }
        }

        /// <summary>
        /// Creates the root menu info.
        /// </summary>
        /// <param name="info">The empty file menu information to use as base.</param>
        /// <param name="clickEventHandler">The click event handler.</param>
        private void FillRootMenuInfo(RootMenuInfo info, EventHandler clickEventHandler)
        {
            if (info == null || info.MenuItem == null) return;
            try
            {
                MenuInfo result =
                    CreateMenuInfoRecursive(info.Text, info.FileSystemEntry) ??
                    new MenuInfo(info.Text, info.FileSystemEntry, new[] { new MenuInfo(Resources.AutoLogFileMenuEmpty, null, null) });

                FillMenu(info.MenuItem, result, clickEventHandler);
            }
            catch
            {
                FillMenu(info.MenuItem, info, clickEventHandler);
            }
        }

        /// <summary>
        /// Gets the display text for a root menu entry.
        /// </summary>
        /// <param name="envVarName">The name of the environment variable.</param>
        /// <param name="path">The file system path.</param>
        /// <returns>The name to give to the root menu child entry.</returns>
        private string GetRootMenuEntryDisplayText(string envVarName, string path)
        {
            try
            {
                Uri dirUri = null;
                string machineName = Environment.MachineName;

                if (Uri.TryCreate(path, UriKind.Absolute, out dirUri) && dirUri.IsUnc)
                {
                    machineName = dirUri.Host;

                    // Try to resolve IP addresses
                    try
                    {
                        IPAddress hostIp = null;
                        if (IPAddress.TryParse(dirUri.Host, out hostIp))
                        {
                            IPHostEntry dnsEntry = Dns.GetHostEntry(hostIp);
                            if (dnsEntry != null && !string.IsNullOrEmpty(dnsEntry.HostName))
                                machineName = dnsEntry.HostName;
                        }
                    }
                    catch
                    {
                        // Fails silently
                    }
                }

                // Returns the best possible description
                if (!string.IsNullOrEmpty(envVarName))
                {
                    if (!string.IsNullOrEmpty(machineName))
                        return envVarName + " (" + machineName + ")";
                    return envVarName;
                }
                if (!string.IsNullOrEmpty(machineName))
                    return machineName;
            }
            catch
            {
            }

            return String.Empty;
        }

        /// <summary>
        /// Populates the root menu.
        /// </summary>
        /// <param name="menu">The <see cref="ToolStripMenuItem" /> to populate.</param>
        /// <param name="clickEventHandler">The click event handler to assign to menus that represent files.</param>
        /// <remarks>
        /// The behavior of the method depends on the content of the <see cref="MenuItem.Tag" /> property of the specified
        /// <paramref name="menu" />.
        /// If the <see cref="MenuItem.Tag" /> property is null, the menu is considered as the root menu.
        /// If the <see cref="MenuItem.Tag" /> contains a <see cref="DirectoryInfo" /> instance, then child items are searched and
        /// the menu is populated.
        /// If the <see cref="MenuItem.Tag" /> contains a <see cref="FileInfo" /> instance, this method returns immediately.
        /// </remarks>
        private void PopulateMenu(ToolStripMenuItem menu, EventHandler clickEventHandler)
        {
            // Pre-load list of root entries and setup the base menus while loading
            IEnumerable<RootMenuInfo> rootInfos = CreateRootMenuInfos(menu);

            Parallel.ForEach(rootInfos, info => FillRootMenuInfo(info, clickEventHandler));
        }

        /// <summary>
        /// Calls a method the on UI thread only if the calling thread is not the UI thread.
        /// </summary>
        /// <param name="action">The action to call if the calling thread is not the UI thread.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="action" /> has been called on the UI thread because the calling thread was not the UI
        /// thread;
        /// <c>false</c> if <paramref name="action" /> was not invoked because the calling thread was the UI thread.
        /// </returns>
        private bool RedirectOnUiThread(Action action)
        {
            if (action == null || _uiSyncContext == null || _uiThreadId == Thread.CurrentThread.ManagedThreadId) return false;

            _uiSyncContext.Send(o => ((Action)o)(), action);
            return true;
        }

        /// <summary>
        /// Setups the loading root menus.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="rootInfos">The root infos.</param>
        /// <returns>The collection of menu items linked to the infos that was used to create them.</returns>
        private IEnumerable<RootMenuInfo> SetupLoadingRootMenus(ToolStripMenuItem menu, IEnumerable<MenuInfo> rootInfos)
        {
            IEnumerable<RootMenuInfo> redirectResults = Enumerable.Empty<RootMenuInfo>();
            if (RedirectOnUiThread(() => redirectResults = SetupLoadingRootMenus(menu, rootInfos))) return redirectResults;

            menu.DropDownItems.Clear();

            return rootInfos.Select(
                rootInfo =>
                {
                    var menuItem = new ToolStripMenuItem(rootInfo.Text + " " + Resources.AutoLogFileMenuLoading);
                    menu.DropDownItems.Add(menuItem);
                    return new RootMenuInfo(rootInfo.Text, rootInfo.FileSystemEntry, rootInfo.Childrens, menuItem);
                }).ToList();
        }

        #endregion

        #region Nested Classes

        #region MenuInfo

        /// <summary>
        /// Holds menu information.
        /// </summary>
        [DebuggerDisplay("Text={Text}, Children={Childrens.Count}")]
        private class MenuInfo
        {
            #region Private Fields

            private readonly ICollection<MenuInfo> _childrens;
            private readonly FileSystemInfo _fileSystemEntry;
            private readonly string _text;

            #endregion

            #region Public Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="MenuInfo" /> class.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="fileSystemEntry">The file system entry.</param>
            /// <param name="childrens">The collection of child menus.</param>
            public MenuInfo(string text, FileSystemInfo fileSystemEntry, ICollection<MenuInfo> childrens)
            {
                _text = text;
                _fileSystemEntry = fileSystemEntry;
                _childrens = childrens;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the collection of child menus.
            /// </summary>
            /// <value>
            /// The children menus.
            /// </value>
            public ICollection<MenuInfo> Childrens
            {
                get { return _childrens; }
            }

            /// <summary>
            /// Gets the file system entry.
            /// </summary>
            /// <value>
            /// The file system entry.
            /// </value>
            public FileSystemInfo FileSystemEntry
            {
                get { return _fileSystemEntry; }
            }

            /// <summary>
            /// Gets the text.
            /// </summary>
            /// <value>
            /// The text.
            /// </value>
            public string Text
            {
                get { return _text; }
            }

            #endregion
        }

        #endregion

        #region RootMenuInfo

        /// <summary>
        /// Holds menu information for root menus.
        /// </summary>
        [DebuggerDisplay("Text={Text}, Children={Childrens.Count}")]
        private class RootMenuInfo : MenuInfo
        {
            #region Private Fields

            private readonly ToolStripMenuItem _menuItem;

            #endregion

            #region Public Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RootMenuInfo" /> class.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="fileSystemEntry">The file system entry.</param>
            /// <param name="childrens">The collection of child menus.</param>
            /// <param name="menuItem">The associated menu item.</param>
            public RootMenuInfo(string text, FileSystemInfo fileSystemEntry, ICollection<MenuInfo> childrens, ToolStripMenuItem menuItem)
                : base(text, fileSystemEntry, childrens)
            {
                _menuItem = menuItem;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the menu item.
            /// </summary>
            /// <value>
            /// The menu item.
            /// </value>
            public ToolStripMenuItem MenuItem
            {
                get { return _menuItem; }
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
