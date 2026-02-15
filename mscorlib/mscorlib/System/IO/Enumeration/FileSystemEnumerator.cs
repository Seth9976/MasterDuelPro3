using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x020007F5 RID: 2037
	public abstract class FileSystemEnumerator<TResult> : CriticalFinalizerObject, IEnumerator<TResult>, IDisposable, IEnumerator
	{
		// Token: 0x06004165 RID: 16741 RVA: 0x000FC09C File Offset: 0x000FA29C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool GetData()
		{
			Interop.NtDll.IO_STATUS_BLOCK io_STATUS_BLOCK;
			int num = Interop.NtDll.NtQueryDirectoryFile(this._directoryHandle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, out io_STATUS_BLOCK, this._buffer, (uint)this._bufferLength, Interop.NtDll.FILE_INFORMATION_CLASS.FileFullDirectoryInformation, Interop.BOOLEAN.FALSE, null, Interop.BOOLEAN.FALSE);
			uint num2 = (uint)num;
			if (num2 == 0U)
			{
				return true;
			}
			if (num2 == 2147483654U)
			{
				this.DirectoryFinished();
				return false;
			}
			int num3 = (int)Interop.NtDll.RtlNtStatusToDosError(num);
			if ((num3 == 5 && this._options.IgnoreInaccessible) || this.ContinueOnError(num3))
			{
				this.DirectoryFinished();
				return false;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num3, this._currentPath);
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000FC124 File Offset: 0x000FA324
		private IntPtr CreateRelativeDirectoryHandle(ReadOnlySpan<char> relativePath, string fullPath)
		{
			ValueTuple<int, IntPtr> valueTuple = Interop.NtDll.CreateFile(relativePath, this._directoryHandle, Interop.NtDll.CreateDisposition.FILE_OPEN, Interop.NtDll.DesiredAccess.FILE_READ_DATA | Interop.NtDll.DesiredAccess.SYNCHRONIZE, FileShare.Read | FileShare.Write | FileShare.Delete, (FileAttributes)0, (Interop.NtDll.CreateOptions)16417U, Interop.NtDll.ObjectAttributes.OBJ_CASE_INSENSITIVE);
			int item = valueTuple.Item1;
			IntPtr item2 = valueTuple.Item2;
			if (item == 0)
			{
				return item2;
			}
			int num = (int)Interop.NtDll.RtlNtStatusToDosError(item);
			if (this.ContinueOnDirectoryError(num, true))
			{
				return IntPtr.Zero;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000FC17C File Offset: 0x000FA37C
		public FileSystemEnumerator(string directory, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._originalRootDirectory = directory;
			this._rootDirectory = PathInternal.TrimEndingDirectorySeparator(Path.GetFullPath(directory));
			this._options = options ?? EnumerationOptions.Default;
			using (default(DisableMediaInsertionPrompt))
			{
				this._directoryHandle = this.CreateDirectoryHandle(this._rootDirectory, false);
				if (this._directoryHandle == IntPtr.Zero)
				{
					this._lastEntryFound = true;
				}
			}
			this._currentPath = this._rootDirectory;
			int bufferSize = this._options.BufferSize;
			this._bufferLength = ((bufferSize <= 0) ? 4096 : Math.Max(1024, bufferSize));
			try
			{
				this._buffer = Marshal.AllocHGlobal(this._bufferLength);
			}
			catch
			{
				this.CloseDirectoryHandle();
				throw;
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000FC284 File Offset: 0x000FA484
		private void CloseDirectoryHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this._directoryHandle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				Interop.Kernel32.CloseHandle(intPtr);
			}
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000FC2B8 File Offset: 0x000FA4B8
		private IntPtr CreateDirectoryHandle(string path, bool ignoreNotFound = false)
		{
			IntPtr intPtr = Interop.Kernel32.CreateFile_IntPtr(path, 1, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, 33554432);
			if (!(intPtr == IntPtr.Zero) && !(intPtr == (IntPtr)(-1)))
			{
				return intPtr;
			}
			int num = Marshal.GetLastWin32Error();
			if (this.ContinueOnDirectoryError(num, ignoreNotFound))
			{
				return IntPtr.Zero;
			}
			if (num == 2)
			{
				num = 3;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num, path);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000FC314 File Offset: 0x000FA514
		private bool ContinueOnDirectoryError(int error, bool ignoreNotFound)
		{
			return (ignoreNotFound && (error == 2 || error == 3 || error == 267)) || (error == 5 && this._options.IgnoreInaccessible) || this.ContinueOnError(error);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000FC344 File Offset: 0x000FA544
		public unsafe bool MoveNext()
		{
			if (this._lastEntryFound)
			{
				return false;
			}
			FileSystemEntry fileSystemEntry = default(FileSystemEntry);
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				if (this._lastEntryFound)
				{
					flag2 = false;
				}
				else
				{
					for (;;)
					{
						this.FindNextEntry();
						if (this._lastEntryFound)
						{
							break;
						}
						FileSystemEntry.Initialize(ref fileSystemEntry, this._entry, this._currentPath, this._rootDirectory, this._originalRootDirectory);
						if ((this._entry->FileAttributes & this._options.AttributesToSkip) == (FileAttributes)0)
						{
							if ((this._entry->FileAttributes & FileAttributes.Directory) != (FileAttributes)0)
							{
								if (this._entry->FileName.Length <= 2 && *this._entry->FileName[0] == 46 && (this._entry->FileName.Length != 2 || *this._entry->FileName[1] == 46))
								{
									if (!this._options.ReturnSpecialDirectories)
									{
										continue;
									}
								}
								else if (this._options.RecurseSubdirectories && this.ShouldRecurseIntoEntry(ref fileSystemEntry))
								{
									string text = Path.Join(this._currentPath, this._entry->FileName);
									IntPtr intPtr = this.CreateRelativeDirectoryHandle(this._entry->FileName, text);
									if (intPtr != IntPtr.Zero)
									{
										try
										{
											if (this._pending == null)
											{
												this._pending = new Queue<ValueTuple<IntPtr, string>>();
											}
											this._pending.Enqueue(new ValueTuple<IntPtr, string>(intPtr, text));
										}
										catch
										{
											Interop.Kernel32.CloseHandle(intPtr);
											throw;
										}
									}
								}
							}
							if (this.ShouldIncludeEntry(ref fileSystemEntry))
							{
								goto Block_15;
							}
						}
					}
					return false;
					Block_15:
					this._current = this.TransformEntry(ref fileSystemEntry);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000FC554 File Offset: 0x000FA754
		private unsafe void FindNextEntry()
		{
			this._entry = Interop.NtDll.FILE_FULL_DIR_INFORMATION.GetNextInfo(this._entry);
			if (this._entry != null)
			{
				return;
			}
			if (this.GetData())
			{
				this._entry = (Interop.NtDll.FILE_FULL_DIR_INFORMATION*)(void*)this._buffer;
			}
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000FC58C File Offset: 0x000FA78C
		private bool DequeueNextDirectory()
		{
			if (this._pending == null || this._pending.Count == 0)
			{
				return false;
			}
			ValueTuple<IntPtr, string> valueTuple = this._pending.Dequeue();
			this._directoryHandle = valueTuple.Item1;
			this._currentPath = valueTuple.Item2;
			return true;
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000FC5D8 File Offset: 0x000FA7D8
		private void InternalDispose(bool disposing)
		{
			if (this._lock != null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._lastEntryFound = true;
					this.CloseDirectoryHandle();
					if (this._pending != null)
					{
						while (this._pending.Count > 0)
						{
							Interop.Kernel32.CloseHandle(this._pending.Dequeue().Item1);
						}
						this._pending = null;
					}
					if (this._buffer != (IntPtr)0)
					{
						Marshal.FreeHGlobal(this._buffer);
					}
					this._buffer = 0;
				}
			}
			this.Dispose(disposing);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0000C091 File Offset: 0x0000A291
		protected virtual bool ShouldIncludeEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x0000C091 File Offset: 0x0000A291
		protected virtual bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06004171 RID: 16753
		protected abstract TResult TransformEntry(ref FileSystemEntry entry);

		// Token: 0x06004172 RID: 16754 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnDirectoryFinished(ReadOnlySpan<char> directory)
		{
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x00033991 File Offset: 0x00031B91
		protected virtual bool ContinueOnError(int error)
		{
			return false;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x000FC68C File Offset: 0x000FA88C
		public TResult Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000FC694 File Offset: 0x000FA894
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000FC6A1 File Offset: 0x000FA8A1
		private unsafe void DirectoryFinished()
		{
			this._entry = default(Interop.NtDll.FILE_FULL_DIR_INFORMATION*);
			this.CloseDirectoryHandle();
			this.OnDirectoryFinished(this._currentPath);
			if (!this.DequeueNextDirectory())
			{
				this._lastEntryFound = true;
				return;
			}
			this.FindNextEntry();
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000339FF File Offset: 0x00031BFF
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000FC6DC File Offset: 0x000FA8DC
		public void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000FC6EC File Offset: 0x000FA8EC
		~FileSystemEnumerator()
		{
			this.InternalDispose(false);
		}

		// Token: 0x04002144 RID: 8516
		private readonly string _originalRootDirectory;

		// Token: 0x04002145 RID: 8517
		private readonly string _rootDirectory;

		// Token: 0x04002146 RID: 8518
		private readonly EnumerationOptions _options;

		// Token: 0x04002147 RID: 8519
		private readonly object _lock = new object();

		// Token: 0x04002148 RID: 8520
		private unsafe Interop.NtDll.FILE_FULL_DIR_INFORMATION* _entry;

		// Token: 0x04002149 RID: 8521
		private TResult _current;

		// Token: 0x0400214A RID: 8522
		private IntPtr _buffer;

		// Token: 0x0400214B RID: 8523
		private int _bufferLength;

		// Token: 0x0400214C RID: 8524
		private IntPtr _directoryHandle;

		// Token: 0x0400214D RID: 8525
		private string _currentPath;

		// Token: 0x0400214E RID: 8526
		private bool _lastEntryFound;

		// Token: 0x0400214F RID: 8527
		[TupleElementNames(new string[] { "Handle", "Path" })]
		private Queue<ValueTuple<IntPtr, string>> _pending;
	}
}
