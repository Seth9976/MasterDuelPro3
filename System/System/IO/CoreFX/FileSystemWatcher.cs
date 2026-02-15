using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Enumeration;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO.CoreFX
{
	// Token: 0x0200036C RID: 876
	public class FileSystemWatcher : Component
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
		private void StartRaisingEvents()
		{
			if (this.IsSuspended())
			{
				this._enabled = true;
				return;
			}
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			this._directoryHandle = global::Interop.Kernel32.CreateFile(this._directory, 1, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, 1107296256);
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle = null;
				throw new FileNotFoundException(SR.Format("Error reading the {0} directory.", this._directory));
			}
			FileSystemWatcher.AsyncReadState asyncReadState;
			try
			{
				int num = Interlocked.Increment(ref this._currentSession);
				byte[] array = this.AllocateBuffer();
				asyncReadState = new FileSystemWatcher.AsyncReadState(num, array, this._directoryHandle, ThreadPoolBoundHandle.BindHandle(this._directoryHandle));
				asyncReadState.PreAllocatedOverlapped = new PreAllocatedOverlapped(new IOCompletionCallback(this.ReadDirectoryChangesCallback), asyncReadState, array);
			}
			catch
			{
				this._directoryHandle.Dispose();
				this._directoryHandle = null;
				throw;
			}
			this._enabled = true;
			this.Monitor(asyncReadState);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005C89C File Offset: 0x0005AA9C
		private void StopRaisingEvents()
		{
			this._enabled = false;
			if (this.IsSuspended())
			{
				return;
			}
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			Interlocked.Increment(ref this._currentSession);
			this._directoryHandle.Dispose();
			this._directoryHandle = null;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005C8DA File Offset: 0x0005AADA
		private void FinalizeDispose()
		{
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle.Dispose();
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005C8F4 File Offset: 0x0005AAF4
		private static bool IsHandleInvalid(SafeFileHandle handle)
		{
			return handle == null || handle.IsInvalid || handle.IsClosed;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005C90C File Offset: 0x0005AB0C
		private unsafe void Monitor(FileSystemWatcher.AsyncReadState state)
		{
			NativeOverlapped* ptr = null;
			bool flag = false;
			try
			{
				if (this._enabled && !FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
				{
					ptr = state.ThreadPoolBinding.AllocateNativeOverlapped(state.PreAllocatedOverlapped);
					int num;
					flag = global::Interop.Kernel32.ReadDirectoryChangesW(state.DirectoryHandle, state.Buffer, this._internalBufferSize, this._includeSubdirectories, (int)this._notifyFilters, out num, ptr, IntPtr.Zero);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			finally
			{
				if (!flag)
				{
					if (ptr != null)
					{
						state.ThreadPoolBinding.FreeNativeOverlapped(ptr);
					}
					state.PreAllocatedOverlapped.Dispose();
					state.ThreadPoolBinding.Dispose();
					if (!FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		private unsafe void ReadDirectoryChangesCallback(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			FileSystemWatcher.AsyncReadState asyncReadState = (FileSystemWatcher.AsyncReadState)ThreadPoolBoundHandle.GetNativeOverlappedState(overlappedPointer);
			try
			{
				if (!FileSystemWatcher.IsHandleInvalid(asyncReadState.DirectoryHandle))
				{
					if (errorCode != 0U)
					{
						if (errorCode != 995U)
						{
							this.OnError(new ErrorEventArgs(new Win32Exception((int)errorCode)));
							this.EnableRaisingEvents = false;
						}
					}
					else if (asyncReadState.Session == Volatile.Read(ref this._currentSession))
					{
						if (numBytes == 0U)
						{
							this.NotifyInternalBufferOverflowEvent();
						}
						else
						{
							this.ParseEventBufferAndNotifyForEach(asyncReadState.Buffer);
						}
					}
				}
			}
			finally
			{
				asyncReadState.ThreadPoolBinding.FreeNativeOverlapped(overlappedPointer);
				this.Monitor(asyncReadState);
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005CA90 File Offset: 0x0005AC90
		private unsafe void ParseEventBufferAndNotifyForEach(byte[] buffer)
		{
			int num = 0;
			string text = null;
			int num2;
			do
			{
				int num3;
				string text2;
				fixed (byte* ptr = &buffer[0])
				{
					byte* ptr2 = ptr;
					num2 = *(int*)(ptr2 + num);
					num3 = *(int*)(ptr2 + num + 4);
					int num4 = *(int*)(ptr2 + num + 8);
					text2 = new string((char*)(ptr2 + num + 12), 0, num4 / 2);
				}
				if (num3 == 4)
				{
					text = text2;
				}
				else if (num3 == 5)
				{
					this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
					text = null;
				}
				else
				{
					if (text != null)
					{
						this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
						text = null;
					}
					switch (num3)
					{
					case 1:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Created, text2);
						break;
					case 2:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Deleted, text2);
						break;
					case 3:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Changed, text2);
						break;
					}
				}
				num += num2;
			}
			while (num2 != 0);
			if (text != null)
			{
				this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0005CB75 File Offset: 0x0005AD75
		public FileSystemWatcher()
		{
			this._directory = string.Empty;
		}

		// Token: 0x17000494 RID: 1172
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x0005CBA8 File Offset: 0x0005ADA8
		public NotifyFilters NotifyFilter
		{
			set
			{
				if ((value & ~(NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size)) != (NotifyFilters)0)
				{
					throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", "value", (int)value, "NotifyFilters"));
				}
				if (this._notifyFilters != value)
				{
					this._notifyFilters = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
		public Collection<string> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x0005CBFC File Offset: 0x0005ADFC
		public bool EnableRaisingEvents
		{
			set
			{
				if (this._enabled == value)
				{
					return;
				}
				if (this.IsSuspended())
				{
					this._enabled = value;
					return;
				}
				if (value)
				{
					this.StartRaisingEventsIfNotDisposed();
					return;
				}
				this.StopRaisingEvents();
			}
		}

		// Token: 0x17000497 RID: 1175
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x0005CC28 File Offset: 0x0005AE28
		public string Filter
		{
			set
			{
				this.Filters.Clear();
				this.Filters.Add(value);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x0005CC41 File Offset: 0x0005AE41
		public bool IncludeSubdirectories
		{
			set
			{
				if (this._includeSubdirectories != value)
				{
					this._includeSubdirectories = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x0005CC59 File Offset: 0x0005AE59
		public int InternalBufferSize
		{
			set
			{
				if ((ulong)this._internalBufferSize != (ulong)((long)value))
				{
					if (value < 4096)
					{
						this._internalBufferSize = 4096U;
					}
					else
					{
						this._internalBufferSize = (uint)value;
					}
					this.Restart();
				}
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0005CC88 File Offset: 0x0005AE88
		private byte[] AllocateBuffer()
		{
			byte[] array;
			try
			{
				array = new byte[this._internalBufferSize];
			}
			catch (OutOfMemoryException)
			{
				throw new OutOfMemoryException(SR.Format("The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer.", this._internalBufferSize));
			}
			return array;
		}

		// Token: 0x1700049A RID: 1178
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0005CCD0 File Offset: 0x0005AED0
		public string Path
		{
			set
			{
				value = ((value == null) ? string.Empty : value);
				if (!string.Equals(this._directory, value, PathInternal.StringComparison))
				{
					if (value.Length == 0)
					{
						throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
					}
					if (!Directory.Exists(value))
					{
						throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
					}
					this._directory = value;
					this.Restart();
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001592 RID: 5522 RVA: 0x0005CD46 File Offset: 0x0005AF46
		// (remove) Token: 0x06001593 RID: 5523 RVA: 0x0005CD5F File Offset: 0x0005AF5F
		public event FileSystemEventHandler Changed
		{
			add
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Combine(this._onChangedHandler, value);
			}
			remove
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Remove(this._onChangedHandler, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001594 RID: 5524 RVA: 0x0005CD78 File Offset: 0x0005AF78
		// (remove) Token: 0x06001595 RID: 5525 RVA: 0x0005CD91 File Offset: 0x0005AF91
		public event FileSystemEventHandler Created
		{
			add
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Combine(this._onCreatedHandler, value);
			}
			remove
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Remove(this._onCreatedHandler, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001596 RID: 5526 RVA: 0x0005CDAA File Offset: 0x0005AFAA
		// (remove) Token: 0x06001597 RID: 5527 RVA: 0x0005CDC3 File Offset: 0x0005AFC3
		public event FileSystemEventHandler Deleted
		{
			add
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Combine(this._onDeletedHandler, value);
			}
			remove
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Remove(this._onDeletedHandler, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001598 RID: 5528 RVA: 0x0005CDDC File Offset: 0x0005AFDC
		// (remove) Token: 0x06001599 RID: 5529 RVA: 0x0005CDF5 File Offset: 0x0005AFF5
		public event ErrorEventHandler Error
		{
			add
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Combine(this._onErrorHandler, value);
			}
			remove
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Remove(this._onErrorHandler, value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600159A RID: 5530 RVA: 0x0005CE0E File Offset: 0x0005B00E
		// (remove) Token: 0x0600159B RID: 5531 RVA: 0x0005CE27 File Offset: 0x0005B027
		public event RenamedEventHandler Renamed
		{
			add
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Combine(this._onRenamedHandler, value);
			}
			remove
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Remove(this._onRenamedHandler, value);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005CE40 File Offset: 0x0005B040
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.StopRaisingEvents();
					this._onChangedHandler = null;
					this._onCreatedHandler = null;
					this._onDeletedHandler = null;
					this._onRenamedHandler = null;
					this._onErrorHandler = null;
				}
				else
				{
					this.FinalizeDispose();
				}
			}
			finally
			{
				this._disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
		private bool MatchPattern(ReadOnlySpan<char> relativePath)
		{
			if (relativePath.IsWhiteSpace())
			{
				return false;
			}
			ReadOnlySpan<char> fileName = global::System.IO.Path.GetFileName(relativePath);
			if (fileName.Length == 0)
			{
				return false;
			}
			string[] filters = this._filters.GetFilters();
			if (filters.Length == 0)
			{
				return true;
			}
			string[] array = filters;
			for (int i = 0; i < array.Length; i++)
			{
				if (FileSystemName.MatchesSimpleExpression(array[i], fileName, !PathInternal.IsCaseSensitive))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005CF0A File Offset: 0x0005B10A
		private void NotifyInternalBufferOverflowEvent()
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler == null)
			{
				return;
			}
			onErrorHandler(this, new ErrorEventArgs(new InternalBufferOverflowException(SR.Format("Too many changes at once in directory:{0}.", this._directory))));
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005CF38 File Offset: 0x0005B138
		private void NotifyRenameEventArgs(WatcherChangeTypes action, ReadOnlySpan<char> name, ReadOnlySpan<char> oldName)
		{
			RenamedEventHandler onRenamedHandler = this._onRenamedHandler;
			if (onRenamedHandler != null && (this.MatchPattern(name) || this.MatchPattern(oldName)))
			{
				onRenamedHandler(this, new RenamedEventArgs(action, this._directory, name.IsEmpty ? null : name.ToString(), oldName.IsEmpty ? null : oldName.ToString()));
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0005CFA6 File Offset: 0x0005B1A6
		private FileSystemEventHandler GetHandler(WatcherChangeTypes changeType)
		{
			switch (changeType)
			{
			case WatcherChangeTypes.Created:
				return this._onCreatedHandler;
			case WatcherChangeTypes.Deleted:
				return this._onDeletedHandler;
			case WatcherChangeTypes.Changed:
				return this._onChangedHandler;
			}
			return null;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0005CFD8 File Offset: 0x0005B1D8
		private void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, string name)
		{
			FileSystemEventHandler handler = this.GetHandler(changeType);
			if (handler != null && this.MatchPattern(string.IsNullOrEmpty(name) ? this._directory : name))
			{
				handler(this, new FileSystemEventArgs(changeType, this._directory, name));
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0005D024 File Offset: 0x0005B224
		protected void OnError(ErrorEventArgs e)
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(onErrorHandler, new object[] { this, e });
					return;
				}
				onErrorHandler(this, e);
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005D06C File Offset: 0x0005B26C
		private void Restart()
		{
			if (!this.IsSuspended() && this._enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEventsIfNotDisposed();
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0005D08A File Offset: 0x0005B28A
		private void StartRaisingEventsIfNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			this.StartRaisingEvents();
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0005AEC4 File Offset: 0x000590C4
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0005D0AB File Offset: 0x0005B2AB
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		public ISynchronizeInvoke SynchronizingObject { get; }

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005D0D8 File Offset: 0x0005B2D8
		private bool IsSuspended()
		{
			return this._initializing || base.DesignMode;
		}

		// Token: 0x04000D15 RID: 3349
		private int _currentSession;

		// Token: 0x04000D16 RID: 3350
		private SafeFileHandle _directoryHandle;

		// Token: 0x04000D17 RID: 3351
		private readonly FileSystemWatcher.NormalizedFilterCollection _filters = new FileSystemWatcher.NormalizedFilterCollection();

		// Token: 0x04000D18 RID: 3352
		private string _directory;

		// Token: 0x04000D19 RID: 3353
		private NotifyFilters _notifyFilters = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

		// Token: 0x04000D1A RID: 3354
		private bool _includeSubdirectories;

		// Token: 0x04000D1B RID: 3355
		private bool _enabled;

		// Token: 0x04000D1C RID: 3356
		private bool _initializing;

		// Token: 0x04000D1D RID: 3357
		private uint _internalBufferSize = 8192U;

		// Token: 0x04000D1E RID: 3358
		private bool _disposed;

		// Token: 0x04000D1F RID: 3359
		private FileSystemEventHandler _onChangedHandler;

		// Token: 0x04000D20 RID: 3360
		private FileSystemEventHandler _onCreatedHandler;

		// Token: 0x04000D21 RID: 3361
		private FileSystemEventHandler _onDeletedHandler;

		// Token: 0x04000D22 RID: 3362
		private RenamedEventHandler _onRenamedHandler;

		// Token: 0x04000D23 RID: 3363
		private ErrorEventHandler _onErrorHandler;

		// Token: 0x04000D24 RID: 3364
		private static readonly char[] s_wildcards = new char[] { '?', '*' };

		// Token: 0x0200036D RID: 877
		private sealed class AsyncReadState
		{
			// Token: 0x060015AA RID: 5546 RVA: 0x0005D101 File Offset: 0x0005B301
			internal AsyncReadState(int session, byte[] buffer, SafeFileHandle handle, ThreadPoolBoundHandle binding)
			{
				this.Session = session;
				this.Buffer = buffer;
				this.DirectoryHandle = handle;
				this.ThreadPoolBinding = binding;
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005D126 File Offset: 0x0005B326
			// (set) Token: 0x060015AC RID: 5548 RVA: 0x0005D12E File Offset: 0x0005B32E
			internal int Session { get; private set; }

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x060015AD RID: 5549 RVA: 0x0005D137 File Offset: 0x0005B337
			// (set) Token: 0x060015AE RID: 5550 RVA: 0x0005D13F File Offset: 0x0005B33F
			internal byte[] Buffer { get; private set; }

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x060015AF RID: 5551 RVA: 0x0005D148 File Offset: 0x0005B348
			// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0005D150 File Offset: 0x0005B350
			internal SafeFileHandle DirectoryHandle { get; private set; }

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005D159 File Offset: 0x0005B359
			// (set) Token: 0x060015B2 RID: 5554 RVA: 0x0005D161 File Offset: 0x0005B361
			internal ThreadPoolBoundHandle ThreadPoolBinding { get; private set; }

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0005D16A File Offset: 0x0005B36A
			// (set) Token: 0x060015B4 RID: 5556 RVA: 0x0005D172 File Offset: 0x0005B372
			internal PreAllocatedOverlapped PreAllocatedOverlapped { get; set; }
		}

		// Token: 0x0200036E RID: 878
		private sealed class NormalizedFilterCollection : Collection<string>
		{
			// Token: 0x060015B5 RID: 5557 RVA: 0x0005D17B File Offset: 0x0005B37B
			internal NormalizedFilterCollection()
				: base(new FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList())
			{
			}

			// Token: 0x060015B6 RID: 5558 RVA: 0x0005D188 File Offset: 0x0005B388
			protected override void InsertItem(int index, string item)
			{
				base.InsertItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x060015B7 RID: 5559 RVA: 0x0005D1AE File Offset: 0x0005B3AE
			protected override void SetItem(int index, string item)
			{
				base.SetItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x060015B8 RID: 5560 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
			internal string[] GetFilters()
			{
				return ((FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList)base.Items).Items;
			}

			// Token: 0x0200036F RID: 879
			private sealed class ImmutableStringList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x170004A2 RID: 1186
				public string this[int index]
				{
					get
					{
						string[] items = this.Items;
						if (index >= items.Length)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						return items[index];
					}
					set
					{
						string[] array = (string[])this.Items.Clone();
						array[index] = value;
						this.Items = array;
					}
				}

				// Token: 0x170004A3 RID: 1187
				// (get) Token: 0x060015BB RID: 5563 RVA: 0x0005D239 File Offset: 0x0005B439
				public int Count
				{
					get
					{
						return this.Items.Length;
					}
				}

				// Token: 0x170004A4 RID: 1188
				// (get) Token: 0x060015BC RID: 5564 RVA: 0x000028AE File Offset: 0x00000AAE
				public bool IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x060015BD RID: 5565 RVA: 0x00003132 File Offset: 0x00001332
				public void Add(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060015BE RID: 5566 RVA: 0x0005D243 File Offset: 0x0005B443
				public void Clear()
				{
					this.Items = Array.Empty<string>();
				}

				// Token: 0x060015BF RID: 5567 RVA: 0x0005D250 File Offset: 0x0005B450
				public bool Contains(string item)
				{
					return Array.IndexOf<string>(this.Items, item) != -1;
				}

				// Token: 0x060015C0 RID: 5568 RVA: 0x0005D264 File Offset: 0x0005B464
				public void CopyTo(string[] array, int arrayIndex)
				{
					this.Items.CopyTo(array, arrayIndex);
				}

				// Token: 0x060015C1 RID: 5569 RVA: 0x0005D273 File Offset: 0x0005B473
				public IEnumerator<string> GetEnumerator()
				{
					return ((IEnumerable<string>)this.Items).GetEnumerator();
				}

				// Token: 0x060015C2 RID: 5570 RVA: 0x0005D280 File Offset: 0x0005B480
				public int IndexOf(string item)
				{
					return Array.IndexOf<string>(this.Items, item);
				}

				// Token: 0x060015C3 RID: 5571 RVA: 0x0005D290 File Offset: 0x0005B490
				public void Insert(int index, string item)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length + 1];
					items.AsSpan(0, index).CopyTo(array);
					items.AsSpan(index).CopyTo(array.AsSpan(index + 1));
					array[index] = item;
					this.Items = array;
				}

				// Token: 0x060015C4 RID: 5572 RVA: 0x00003132 File Offset: 0x00001332
				public bool Remove(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060015C5 RID: 5573 RVA: 0x0005D2E8 File Offset: 0x0005B4E8
				public void RemoveAt(int index)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length - 1];
					items.AsSpan(0, index).CopyTo(array);
					items.AsSpan(index + 1).CopyTo(array.AsSpan(index));
					this.Items = array;
				}

				// Token: 0x060015C6 RID: 5574 RVA: 0x0005D33A File Offset: 0x0005B53A
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04000D2B RID: 3371
				public string[] Items = Array.Empty<string>();
			}
		}
	}
}
