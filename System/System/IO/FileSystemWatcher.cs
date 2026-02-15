using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000355 RID: 853
	[IODescription("")]
	[DefaultEvent("Changed")]
	public class FileSystemWatcher : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x0600151D RID: 5405 RVA: 0x0005AAE8 File Offset: 0x00058CE8
		public FileSystemWatcher()
		{
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.enableRaisingEvents = false;
			this.filter = "*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = "";
			this.InitWatcher();
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0005AB38 File Offset: 0x00058D38
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (this.watcher_handle == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					bool flag2 = false;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					switch (num)
					{
					case 1:
						flag2 = DefaultWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 2:
						flag2 = FAMWatcher.GetInstance(out this.watcher, false);
						this.watcher_handle = this;
						break;
					case 3:
						flag2 = KeventWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 4:
						flag2 = FAMWatcher.GetInstance(out this.watcher, true);
						this.watcher_handle = this;
						break;
					case 6:
						flag2 = CoreFXFileSystemWatcherProxy.GetInstance(out this.watcher);
						this.watcher_handle = (this.watcher as CoreFXFileSystemWatcherProxy).NewWatcher(this);
						break;
					}
					if (num == 0 || !flag2)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out this.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out this.watcher);
							this.watcher_handle = this;
						}
					}
					this.inited = true;
				}
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0005AC80 File Offset: 0x00058E80
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x0005AC88 File Offset: 0x00058E88
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0005AC91 File Offset: 0x00058E91
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				return "*.*";
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005ACC0 File Offset: 0x00058EC0
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					IFileWatcher fileWatcher = this.watcher;
					if (((fileWatcher != null) ? fileWatcher.GetType() : null) == typeof(KeventWatcher))
					{
						this.pattern = new SearchPattern2(this.MangledFilter, true);
					}
					else
					{
						this.pattern = new SearchPattern2(this.MangledFilter);
					}
				}
				return this.pattern;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0005AD24 File Offset: 0x00058F24
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == "")
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = global::System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>true if the component is enabled; otherwise, false. The default is false. If you are using the component on a designer in Visual Studio 2005, the default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0005AD77 File Offset: 0x00058F77
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0005AD80 File Offset: 0x00058F80
		[IODescription("Flag to indicate if this instance is active")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.start_requested = true;
				if (!this.inited)
				{
					return;
				}
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
					return;
				}
				this.Stop();
				this.start_requested = false;
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.) </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005ADDE File Offset: 0x00058FDE
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[IODescription("File name filter pattern")]
		[DefaultValue("*.*")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>true if you want to monitor subdirectories; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0005ADE6 File Offset: 0x00058FE6
		[IODescription("Flag to indicate we want to watch subdirectories")]
		[DefaultValue(false)]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0005ADEE File Offset: 0x00058FEE
		[Browsable(false)]
		[DefaultValue(8192)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of LastWrite, FileName, and DirectoryName.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0005ADF6 File Offset: 0x00058FF6
		[DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.-or- The specified path contains wildcard characters.-or- The specified path contains invalid path characters.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0005ADFE File Offset: 0x00058FFE
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0005AE08 File Offset: 0x00059008
		[IODescription("The directory to monitor")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				value = ((value == null) ? string.Empty : value);
				if (string.Equals(this.path, value, PathInternal.StringComparison))
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex)
				{
				}
				if (ex != null)
				{
					throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
				}
				if (!flag)
				{
					throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005AEC4 File Offset: 0x000590C4
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0005AECC File Offset: 0x000590CC
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600152E RID: 5422 RVA: 0x0005AEF4 File Offset: 0x000590F4
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				IFileWatcher fileWatcher = this.watcher;
				if (fileWatcher != null)
				{
					fileWatcher.StopDispatching(this.watcher_handle);
				}
				IFileWatcher fileWatcher2 = this.watcher;
				if (fileWatcher2 != null)
				{
					fileWatcher2.Dispose(this.watcher_handle);
				}
			}
			catch (Exception)
			{
			}
			this.watcher_handle = null;
			this.watcher = null;
			this.disposed = true;
			base.Dispose(disposing);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005AF70 File Offset: 0x00059170
		~FileSystemWatcher()
		{
			if (!this.disposed)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005AFA8 File Offset: 0x000591A8
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (this.disposed)
			{
				return;
			}
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (Delegate @delegate in ev.GetInvocationList())
				{
					switch (evtype)
					{
					case FileSystemWatcher.EventType.FileSystemEvent:
						((FileSystemEventHandler)@delegate)(this, (FileSystemEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.ErrorEvent:
						((ErrorEventHandler)@delegate)(this, (ErrorEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.RenameEvent:
						((RenamedEventHandler)@delegate)(this, (RenamedEventArgs)arg);
						break;
					}
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[] { this, arg });
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001531 RID: 5425 RVA: 0x0005B04D File Offset: 0x0005924D
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001532 RID: 5426 RVA: 0x0005B05D File Offset: 0x0005925D
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001533 RID: 5427 RVA: 0x0005B06D File Offset: 0x0005926D
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06001534 RID: 5428 RVA: 0x0005B07D File Offset: 0x0005927D
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001535 RID: 5429 RVA: 0x0005B08D File Offset: 0x0005928D
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0005B09D File Offset: 0x0005929D
		internal void DispatchErrorEvents(ErrorEventArgs args)
		{
			if (this.disposed)
			{
				return;
			}
			this.OnError(args);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0005B0B0 File Offset: 0x000592B0
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			FileSystemWatcher.<>c__DisplayClass70_0 CS$<>8__locals1 = new FileSystemWatcher.<>c__DisplayClass70_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.filename = filename;
			if (this.disposed)
			{
				return;
			}
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Removed:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Modified:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, CS$<>8__locals1.filename, "");
				return;
			case FileAction.RenamedNewName:
			{
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, "", CS$<>8__locals1.filename);
				}
				RenamedEventArgs renamed_ref = renamed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnRenamed(renamed_ref);
				});
				renamed = null;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005B274 File Offset: 0x00059474
		private void Start()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StartDispatching(this.watcher_handle);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005B29E File Offset: 0x0005949E
		private void Stop()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StopDispatching(this.watcher_handle);
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600153A RID: 5434 RVA: 0x0005B2C8 File Offset: 0x000594C8
		// (remove) Token: 0x0600153B RID: 5435 RVA: 0x0005B300 File Offset: 0x00059500
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created;

		// Token: 0x0600153C RID: 5436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x04000C71 RID: 3185
		private bool inited;

		// Token: 0x04000C72 RID: 3186
		private bool start_requested;

		// Token: 0x04000C73 RID: 3187
		private bool enableRaisingEvents;

		// Token: 0x04000C74 RID: 3188
		private string filter;

		// Token: 0x04000C75 RID: 3189
		private bool includeSubdirectories;

		// Token: 0x04000C76 RID: 3190
		private int internalBufferSize;

		// Token: 0x04000C77 RID: 3191
		private NotifyFilters notifyFilter;

		// Token: 0x04000C78 RID: 3192
		private string path;

		// Token: 0x04000C79 RID: 3193
		private string fullpath;

		// Token: 0x04000C7A RID: 3194
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000C7B RID: 3195
		private WaitForChangedResult lastData;

		// Token: 0x04000C7C RID: 3196
		private bool waiting;

		// Token: 0x04000C7D RID: 3197
		private SearchPattern2 pattern;

		// Token: 0x04000C7E RID: 3198
		private bool disposed;

		// Token: 0x04000C7F RID: 3199
		private string mangledFilter;

		// Token: 0x04000C80 RID: 3200
		private IFileWatcher watcher;

		// Token: 0x04000C81 RID: 3201
		private object watcher_handle;

		// Token: 0x04000C82 RID: 3202
		private static object lockobj = new object();

		// Token: 0x04000C83 RID: 3203
		[CompilerGenerated]
		private FileSystemEventHandler Changed;

		// Token: 0x04000C85 RID: 3205
		[CompilerGenerated]
		private FileSystemEventHandler Deleted;

		// Token: 0x04000C86 RID: 3206
		[CompilerGenerated]
		private ErrorEventHandler Error;

		// Token: 0x04000C87 RID: 3207
		[CompilerGenerated]
		private RenamedEventHandler Renamed;

		// Token: 0x02000356 RID: 854
		private enum EventType
		{
			// Token: 0x04000C89 RID: 3209
			FileSystemEvent,
			// Token: 0x04000C8A RID: 3210
			ErrorEvent,
			// Token: 0x04000C8B RID: 3211
			RenameEvent
		}
	}
}
