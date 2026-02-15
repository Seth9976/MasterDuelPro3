using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020007DE RID: 2014
	internal static class MonoIO
	{
		// Token: 0x060040BA RID: 16570 RVA: 0x000F9569 File Offset: 0x000F7769
		public static Exception GetException(MonoIOError error)
		{
			if (error == MonoIOError.ERROR_ACCESS_DENIED)
			{
				return new UnauthorizedAccessException("Access to the path is denied.");
			}
			if (error != MonoIOError.ERROR_FILE_EXISTS)
			{
				return MonoIO.GetException(string.Empty, error);
			}
			return new IOException("Cannot create a file that already exist.", -2147024816);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000F959C File Offset: 0x000F779C
		public static Exception GetException(string path, MonoIOError error)
		{
			if (error <= MonoIOError.ERROR_FILE_EXISTS)
			{
				if (error <= MonoIOError.ERROR_NOT_SAME_DEVICE)
				{
					switch (error)
					{
					case MonoIOError.ERROR_FILE_NOT_FOUND:
						return new FileNotFoundException(string.Format("Could not find file \"{0}\"", path), path);
					case MonoIOError.ERROR_PATH_NOT_FOUND:
						return new DirectoryNotFoundException(string.Format("Could not find a part of the path \"{0}\"", path));
					case MonoIOError.ERROR_TOO_MANY_OPEN_FILES:
						if (MonoIO.dump_handles)
						{
							MonoIO.DumpHandles();
						}
						return new IOException("Too many open files", (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_ACCESS_DENIED:
						return new UnauthorizedAccessException(string.Format("Access to the path \"{0}\" is denied.", path));
					case MonoIOError.ERROR_INVALID_HANDLE:
						return new IOException(string.Format("Invalid handle to path \"{0}\"", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_INVALID_DRIVE)
						{
							return new DriveNotFoundException(string.Format("Could not find the drive  '{0}'. The drive might not be ready or might not be mapped.", path));
						}
						if (error == MonoIOError.ERROR_NOT_SAME_DEVICE)
						{
							return new IOException("Source and destination are not on the same device", (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
				else
				{
					switch (error)
					{
					case MonoIOError.ERROR_WRITE_FAULT:
						return new IOException(string.Format("Write fault on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_READ_FAULT:
					case MonoIOError.ERROR_GEN_FAILURE:
						break;
					case MonoIOError.ERROR_SHARING_VIOLATION:
						return new IOException(string.Format("Sharing violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_LOCK_VIOLATION:
						return new IOException(string.Format("Lock violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_HANDLE_DISK_FULL)
						{
							return new IOException(string.Format("Disk full. Path {0}", path), (int)((MonoIOError)(-2147024896) | error));
						}
						if (error == MonoIOError.ERROR_FILE_EXISTS)
						{
							return new IOException(string.Format("Could not create file \"{0}\". File already exists.", path), (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
			}
			else if (error <= MonoIOError.ERROR_DIR_NOT_EMPTY)
			{
				if (error == MonoIOError.ERROR_CANNOT_MAKE)
				{
					return new IOException(string.Format("Path {0} is a directory", path), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_INVALID_PARAMETER)
				{
					return new IOException(string.Format("Invalid parameter", Array.Empty<object>()), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_DIR_NOT_EMPTY)
				{
					return new IOException(string.Format("Directory {0} is not empty", path), (int)((MonoIOError)(-2147024896) | error));
				}
			}
			else
			{
				if (error == MonoIOError.ERROR_FILENAME_EXCED_RANGE)
				{
					return new PathTooLongException(string.Format("Path is too long. Path: {0}", path));
				}
				if (error == MonoIOError.ERROR_DIRECTORY)
				{
					return new IOException("The directory name is invalid", (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_ENCRYPTION_FAILED)
				{
					return new IOException("Encryption failed", (int)((MonoIOError)(-2147024896) | error));
				}
			}
			return new IOException(string.Format("Win32 IO returned {0}. Path: {1}", error, path), (int)((MonoIOError)(-2147024896) | error));
		}

		// Token: 0x060040BC RID: 16572
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetCurrentDirectory(out MonoIOError error);

		// Token: 0x060040BD RID: 16573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetCurrentDirectory(char* path, out MonoIOError error);

		// Token: 0x060040BE RID: 16574 RVA: 0x000F9808 File Offset: 0x000F7A08
		public unsafe static bool SetCurrentDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetCurrentDirectory(ptr, out error);
		}

		// Token: 0x060040BF RID: 16575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetFileAttributes(char* path, FileAttributes attrs, out MonoIOError error);

		// Token: 0x060040C0 RID: 16576 RVA: 0x000F982C File Offset: 0x000F7A2C
		public unsafe static bool SetFileAttributes(string path, FileAttributes attrs, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetFileAttributes(ptr, attrs, out error);
		}

		// Token: 0x060040C1 RID: 16577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MonoFileType GetFileType(IntPtr handle, out MonoIOError error);

		// Token: 0x060040C2 RID: 16578 RVA: 0x000F9854 File Offset: 0x000F7A54
		public static MonoFileType GetFileType(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			MonoFileType fileType;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				fileType = MonoIO.GetFileType(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return fileType;
		}

		// Token: 0x060040C3 RID: 16579
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FindCloseFile(IntPtr hnd);

		// Token: 0x060040C4 RID: 16580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Open(char* filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error);

		// Token: 0x060040C5 RID: 16581 RVA: 0x000F9898 File Offset: 0x000F7A98
		public unsafe static IntPtr Open(string filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error)
		{
			char* ptr = filename;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.Open(ptr, mode, access, share, options, out error);
		}

		// Token: 0x060040C6 RID: 16582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Cancel_internal(IntPtr handle, out MonoIOError error);

		// Token: 0x060040C7 RID: 16583 RVA: 0x000F98C4 File Offset: 0x000F7AC4
		internal static bool Cancel(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.Cancel_internal(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060040C8 RID: 16584
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x060040C9 RID: 16585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Read(IntPtr handle, byte[] dest, int dest_offset, int count, out MonoIOError error);

		// Token: 0x060040CA RID: 16586 RVA: 0x000F9908 File Offset: 0x000F7B08
		public static int Read(SafeHandle safeHandle, byte[] dest, int dest_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Read(safeHandle.DangerousGetHandle(), dest, dest_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060040CB RID: 16587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Write(IntPtr handle, [In] byte[] src, int src_offset, int count, out MonoIOError error);

		// Token: 0x060040CC RID: 16588 RVA: 0x000F9950 File Offset: 0x000F7B50
		public static int Write(SafeHandle safeHandle, byte[] src, int src_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Write(safeHandle.DangerousGetHandle(), src, src_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060040CD RID: 16589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long Seek(IntPtr handle, long offset, SeekOrigin origin, out MonoIOError error);

		// Token: 0x060040CE RID: 16590 RVA: 0x000F9998 File Offset: 0x000F7B98
		public static long Seek(SafeHandle safeHandle, long offset, SeekOrigin origin, out MonoIOError error)
		{
			bool flag = false;
			long num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Seek(safeHandle.DangerousGetHandle(), offset, origin, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060040CF RID: 16591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLength(IntPtr handle, out MonoIOError error);

		// Token: 0x060040D0 RID: 16592 RVA: 0x000F99DC File Offset: 0x000F7BDC
		public static long GetLength(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			long length;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				length = MonoIO.GetLength(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return length;
		}

		// Token: 0x060040D1 RID: 16593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetLength(IntPtr handle, long length, out MonoIOError error);

		// Token: 0x060040D2 RID: 16594 RVA: 0x000F9A20 File Offset: 0x000F7C20
		public static bool SetLength(SafeHandle safeHandle, long length, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.SetLength(safeHandle.DangerousGetHandle(), length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060040D3 RID: 16595
		public static extern IntPtr ConsoleOutput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060040D4 RID: 16596
		public static extern IntPtr ConsoleInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060040D5 RID: 16597
		public static extern IntPtr ConsoleError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060040D6 RID: 16598
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CreatePipe(out IntPtr read_handle, out IntPtr write_handle, out MonoIOError error);

		// Token: 0x060040D7 RID: 16599
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DuplicateHandle(IntPtr source_process_handle, IntPtr source_handle, IntPtr target_process_handle, out IntPtr target_handle, int access, int inherit, int options, out MonoIOError error);

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060040D8 RID: 16600
		public static extern char VolumeSeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060040D9 RID: 16601
		public static extern char DirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060040DA RID: 16602
		public static extern char AltDirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060040DB RID: 16603
		public static extern char PathSeparator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060040DC RID: 16604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DumpHandles();

		// Token: 0x060040DD RID: 16605
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RemapPath(string path, out string newPath);

		// Token: 0x040020F1 RID: 8433
		public static readonly IntPtr InvalidHandle = (IntPtr)(-1L);

		// Token: 0x040020F2 RID: 8434
		private static bool dump_handles = Environment.GetEnvironmentVariable("MONO_DUMP_HANDLES_ON_ERROR_TOO_MANY_OPEN_FILES") != null;
	}
}
