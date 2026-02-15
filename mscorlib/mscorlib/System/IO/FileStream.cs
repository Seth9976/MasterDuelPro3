using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a file, supporting both synchronous and asynchronous read and write operations.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007D9 RID: 2009
	[ComVisible(true)]
	public class FileStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, FileStream instance ownership, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x0600407B RID: 16507 RVA: 0x000F82F7 File Offset: 0x000F64F7
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize)
			: this(handle, access, ownsHandle, bufferSize, false, false)
		{
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000F8308 File Offset: 0x000F6508
		internal FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (handle == MonoIO.InvalidHandle)
			{
				throw new ArgumentException("handle", Locale.GetText("Invalid."));
			}
			this.Init(new SafeFileHandle(handle, false), access, ownsHandle, bufferSize, isAsync, isConsoleWrapper);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path and creation mode.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x0600407D RID: 16509 RVA: 0x000F835D File Offset: 0x000F655D
		public FileStream(string path, FileMode mode)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, and read/write permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x0600407E RID: 16510 RVA: 0x000F8377 File Offset: 0x000F6577
		public FileStream(string path, FileMode mode, FileAccess access)
			: this(path, mode, access, (access == FileAccess.Write) ? FileShare.None : FileShare.Read, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write permission, and sharing permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x0600407F RID: 16511 RVA: 0x000F8391 File Offset: 0x000F6591
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: this(path, mode, access, share, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, and buffer size.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06004080 RID: 16512 RVA: 0x000F83A5 File Offset: 0x000F65A5
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: this(path, mode, access, share, bufferSize, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="useAsync">Specifies whether to use asynchronous I/O or synchronous I/O. However, note that the underlying operating system might not support asynchronous I/O, so when specifying true, the handle might be opened synchronously depending on the platform. When opened asynchronously, the <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> and <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> methods perform better on large reads or writes, but they might be much slower for small reads or writes. If the application is designed to take advantage of asynchronous I/O, set the <paramref name="useAsync" /> parameter to true. Using asynchronous I/O correctly can speed up applications by as much as a factor of 10, but using it without redesigning the application for asynchronous I/O can decrease performance by as much as a factor of 10. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or- The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06004081 RID: 16513 RVA: 0x000F83B6 File Offset: 0x000F65B6
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
			: this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="options">A value that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06004082 RID: 16514 RVA: 0x000F83D1 File Offset: 0x000F65D1
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
			: this(path, mode, access, share, bufferSize, false, options)
		{
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000F83E3 File Offset: 0x000F65E3
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool isAsync, bool anonymous)
			: this(path, mode, access, share, bufferSize, anonymous, isAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000F8400 File Offset: 0x000F6600
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool anonymous, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			this.anonymous = anonymous;
			share &= ~FileShare.Inheritable;
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				throw new ArgumentOutOfRangeException("mode", "Enum value was out of legal range.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if ((share < FileShare.None) || share > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				throw new ArgumentOutOfRangeException("share", "Enum value was out of legal range.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Name has invalid chars");
			}
			path = Path.InsecureGetFullPath(path);
			if (Directory.Exists(path))
			{
				throw new UnauthorizedAccessException(string.Format(Locale.GetText("Access to the path '{0}' is denied."), this.GetSecureFileName(path, false)));
			}
			if (mode == FileMode.Append && (access & FileAccess.Read) == FileAccess.Read)
			{
				throw new ArgumentException("Append access can be requested only in write-only mode.");
			}
			if ((access & FileAccess.Write) == (FileAccess)0 && mode != FileMode.Open && mode != FileMode.OpenOrCreate)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Combining FileMode: {0} with FileAccess: {1} is invalid."), access, mode));
			}
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName.Length > 0 && !Directory.Exists(Path.GetFullPath(directoryName)))
			{
				string text = Locale.GetText("Could not find a part of the path \"{0}\".");
				string text2 = (anonymous ? directoryName : Path.GetFullPath(path));
				throw new DirectoryNotFoundException(string.Format(text, text2));
			}
			if (!anonymous)
			{
				this.name = path;
			}
			MonoIOError monoIOError;
			IntPtr intPtr = MonoIO.Open(path, mode, access, share, options, out monoIOError);
			if (intPtr == MonoIO.InvalidHandle)
			{
				throw MonoIO.GetException(this.GetSecureFileName(path), monoIOError);
			}
			this.safeHandle = new SafeFileHandle(intPtr, false);
			this.access = access;
			this.owner = true;
			if (MonoIO.GetFileType(this.safeHandle, out monoIOError) == MonoFileType.Disk)
			{
				this.canseek = true;
				this.async = (options & FileOptions.Asynchronous) > FileOptions.None;
			}
			else
			{
				this.canseek = false;
				this.async = false;
			}
			if (access == FileAccess.Read && this.canseek && bufferSize == 4096)
			{
				long length = this.Length;
				if ((long)bufferSize > length)
				{
					bufferSize = (int)((length < 1000L) ? 1000L : length);
				}
			}
			this.InitBuffer(bufferSize, false);
			if (mode == FileMode.Append)
			{
				this.Seek(0L, SeekOrigin.End);
				this.append_startpos = this.Position;
				return;
			}
			this.append_startpos = 0L;
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000F8668 File Offset: 0x000F6868
		private void Init(SafeFileHandle safeHandle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			if (!isConsoleWrapper && safeHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid handle."), "handle");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (!isConsoleWrapper && bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			MonoIOError monoIOError;
			MonoFileType fileType = MonoIO.GetFileType(safeHandle, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.name, monoIOError);
			}
			if (fileType == MonoFileType.Unknown)
			{
				throw new IOException("Invalid handle.");
			}
			if (fileType == MonoFileType.Disk)
			{
				this.canseek = true;
			}
			else
			{
				this.canseek = false;
			}
			this.safeHandle = safeHandle;
			this.ExposeHandle();
			this.access = access;
			this.owner = ownsHandle;
			this.async = isAsync;
			this.anonymous = false;
			if (this.canseek)
			{
				this.buf_start = MonoIO.Seek(safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.name, monoIOError);
				}
			}
			this.append_startpos = 0L;
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; false if the stream is closed or was opened with write-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000F875D File Offset: 0x000F695D
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.Read || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; false if the stream is closed or was opened with read-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000F8773 File Offset: 0x000F6973
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.Write || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; false if the stream is closed or if the FileStream was constructed from an operating-system handle such as a pipe or output to the console.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000F8789 File Offset: 0x000F6989
		public override bool CanSeek
		{
			get
			{
				return this.canseek;
			}
		}

		/// <summary>Gets the name of the FileStream that was passed to the constructor.</summary>
		/// <returns>A string that is the name of the FileStream.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x000F8791 File Offset: 0x000F6991
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.FileStream.CanSeek" /> for this stream is false. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as the file being closed, occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000F879C File Offset: 0x000F699C
		public override long Length
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				this.FlushBufferIfDirty();
				MonoIOError monoIOError;
				long length = MonoIO.GetLength(this.safeHandle, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return length;
			}
		}

		/// <summary>Gets or sets the current position of this stream.</summary>
		/// <returns>The current position of this stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. - or -The position was set to a very large value beyond the end of the stream in Windows 98 or earlier.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the position to a negative value. </exception>
		/// <exception cref="T:System.IO.EndOfStreamException">Attempted seeking past the end of a stream that does not support this. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000F8800 File Offset: 0x000F6A00
		// (set) Token: 0x0600408C RID: 16524 RVA: 0x000F8875 File Offset: 0x000F6A75
		public override long Position
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				if (!this.isExposed)
				{
					return this.buf_start + (long)this.buf_offset;
				}
				MonoIOError monoIOError;
				long num = MonoIO.Seek(this.safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return num;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>Gets the operating system file handle for the file that the current FileStream object encapsulates.</summary>
		/// <returns>The operating system file handle for the file encapsulated by this FileStream object, or -1 if the FileStream has been closed.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000F889A File Offset: 0x000F6A9A
		[Obsolete("Use SafeFileHandle instead")]
		public virtual IntPtr Handle
		{
			get
			{
				IntPtr intPtr = this.safeHandle.DangerousGetHandle();
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return intPtr;
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</summary>
		/// <returns>An object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000F88B5 File Offset: 0x000F6AB5
		public virtual SafeFileHandle SafeFileHandle
		{
			get
			{
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return this.safeHandle;
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x000F88CB File Offset: 0x000F6ACB
		private void ExposeHandle()
		{
			this.isExposed = true;
			this.FlushBuffer();
			this.InitBuffer(0, true);
		}

		/// <summary>Reads a byte from the file and advances the read position one byte.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004090 RID: 16528 RVA: 0x000F88E4 File Offset: 0x000F6AE4
		public override int ReadByte()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.buf_size != 0)
			{
				if (this.buf_offset >= this.buf_length)
				{
					this.RefillBuffer();
					if (this.buf_length == 0)
					{
						return -1;
					}
				}
				byte[] array = this.buf;
				int num = this.buf_offset;
				this.buf_offset = num + 1;
				return array[num];
			}
			if (this.ReadData(this.safeHandle, this.buf, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.buf[0];
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte to write to the stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004091 RID: 16529 RVA: 0x000F897C File Offset: 0x000F6B7C
		public override void WriteByte(byte value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.buf_offset == this.buf_size)
			{
				this.FlushBuffer();
			}
			if (this.buf_size == 0)
			{
				this.buf[0] = value;
				this.buf_dirty = true;
				this.buf_length = 1;
				this.FlushBuffer();
				return;
			}
			byte[] array = this.buf;
			int num = this.buf_offset;
			this.buf_offset = num + 1;
			array[num] = value;
			if (this.buf_offset > this.buf_length)
			{
				this.buf_length = this.buf_offset;
			}
			this.buf_dirty = true;
		}

		/// <summary>Reads a block of bytes from the stream and writes the data in a given buffer.</summary>
		/// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached.</returns>
		/// <param name="array">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1<paramref name=")" /> replaced by the bytes read from the current source. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004092 RID: 16530 RVA: 0x000F8A28 File Offset: 0x000F6C28
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			int num = array.Length;
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginRead(array, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000F8AE0 File Offset: 0x000F6CE0
		private int ReadInternal(byte[] dest, int offset, int count)
		{
			int num = this.ReadSegment(dest, offset, count);
			if (num == count)
			{
				return count;
			}
			int num2 = num;
			count -= num;
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				num = this.ReadData(this.safeHandle, dest, offset + num, count);
				this.buf_start += (long)num;
			}
			else
			{
				this.RefillBuffer();
				num = this.ReadSegment(dest, offset + num2, count);
			}
			return num2 + num;
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that references the asynchronous read.</returns>
		/// <param name="array">The buffer to read data into. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading. </param>
		/// <param name="numBytes">The maximum number of bytes to read. </param>
		/// <param name="userCallback">The method to be called when the asynchronous read operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">The array length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An asynchronous read was attempted past the end of the file. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004094 RID: 16532 RVA: 0x000F8B4C File Offset: 0x000F6D4C
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("Buffer too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
			}
			return new FileStream.ReadDelegate(this.ReadInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Waits for the pending asynchronous read operation to complete. (Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 and the number of bytes you requested. Streams only return 0 at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004095 RID: 16533 RVA: 0x000F8BFC File Offset: 0x000F6DFC
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				return base.EndRead(asyncResult);
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Writes a block of bytes to the file stream.</summary>
		/// <param name="array">The buffer containing data to write to the stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> from which to begin copying bytes to the stream. </param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. - or -Another thread may have caused an unexpected change in the position of the operating system's file handle. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream instance does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004096 RID: 16534 RVA: 0x000F8C64 File Offset: 0x000F6E64
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginWrite(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			this.WriteInternal(array, offset, count);
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000F8D0C File Offset: 0x000F6F0C
		private void WriteInternal(byte[] src, int offset, int count)
		{
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = count;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num = MonoIO.Write(this.safeHandle, src, offset, i, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num;
					offset += num;
				}
				this.buf_start += (long)count;
				return;
			}
			int num2 = 0;
			while (count > 0)
			{
				int num3 = this.WriteSegment(src, offset + num2, count);
				num2 += num3;
				count -= num3;
				if (count == 0)
				{
					break;
				}
				this.FlushBuffer();
			}
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that references the asynchronous write.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="numBytes">The maximum number of bytes to write. </param>
		/// <param name="userCallback">The method to be called when the asynchronous write operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous write request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004098 RID: 16536 RVA: 0x000F8DD8 File Offset: 0x000F6FD8
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("array too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
			}
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(userCallback, stateObject);
			fileStreamAsyncResult.BytesRead = -1;
			fileStreamAsyncResult.Count = numBytes;
			fileStreamAsyncResult.OriginalCount = numBytes;
			return new FileStream.WriteDelegate(this.WriteInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Ends an asynchronous write operation and blocks until the I/O operation is complete. (Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">The pending asynchronous I/O request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004099 RID: 16537 RVA: 0x000F8EA4 File Offset: 0x000F70A4
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				base.EndWrite(asyncResult);
				return;
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Sets the current position of this stream to the given value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point relative to <paramref name="origin" /> from which to begin seeking. </param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="origin" />, using a value of type <see cref="T:System.IO.SeekOrigin" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the FileStream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ArgumentException">Seeking is attempted before the beginning of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600409A RID: 16538 RVA: 0x000F8F0C File Offset: 0x000F710C
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin", "Invalid SeekOrigin");
			}
			if (num < 0L)
			{
				throw new IOException("Attempted to Seek before the beginning of the stream");
			}
			if (num < this.append_startpos)
			{
				throw new IOException("Can't seek back over pre-existing data in append mode");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			this.buf_start = MonoIO.Seek(this.safeHandle, num, SeekOrigin.Begin, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			return this.buf_start;
		}

		/// <summary>Sets the length of this stream to the given value.</summary>
		/// <param name="value">The new length of the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the <paramref name="value" /> parameter to less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600409B RID: 16539 RVA: 0x000F8FE0 File Offset: 0x000F71E0
		public override void SetLength(long value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value is less than 0");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			MonoIO.SetLength(this.safeHandle, value, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (this.Position > value)
			{
				this.Position = value;
			}
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600409C RID: 16540 RVA: 0x000F9076 File Offset: 0x000F7276
		public override void Flush()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000F9098 File Offset: 0x000F7298
		~FileStream()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600409E RID: 16542 RVA: 0x000F90C8 File Offset: 0x000F72C8
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (this.safeHandle != null && !this.safeHandle.IsClosed)
			{
				try
				{
					this.FlushBuffer();
				}
				catch (Exception ex)
				{
				}
				if (this.owner)
				{
					MonoIOError monoIOError;
					MonoIO.Close(this.safeHandle.DangerousGetHandle(), out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					this.safeHandle.DangerousRelease();
				}
			}
			this.canseek = false;
			this.access = (FileAccess)0;
			if (disposing && this.buf != null)
			{
				if (this.buf.Length == 4096 && FileStream.buf_recycle == null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle == null)
						{
							FileStream.buf_recycle = this.buf;
						}
					}
				}
				this.buf = null;
				GC.SuppressFinalize(this);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests. </summary>
		/// <returns>A task that represents the asynchronous flush operation. </returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x0600409F RID: 16543 RVA: 0x000F91C0 File Offset: 0x000F73C0
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			return base.FlushAsync(cancellationToken);
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x060040A0 RID: 16544 RVA: 0x000F91E1 File Offset: 0x000F73E1
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.ReadAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests. </summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x060040A1 RID: 16545 RVA: 0x000F91EE File Offset: 0x000F73EE
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000F91FB File Offset: 0x000F73FB
		private int ReadSegment(byte[] dest, int dest_offset, int count)
		{
			count = Math.Min(count, this.buf_length - this.buf_offset);
			if (count > 0)
			{
				Buffer.InternalBlockCopy(this.buf, this.buf_offset, dest, dest_offset, count);
				this.buf_offset += count;
			}
			return count;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000F923C File Offset: 0x000F743C
		private int WriteSegment(byte[] src, int src_offset, int count)
		{
			if (count > this.buf_size - this.buf_offset)
			{
				count = this.buf_size - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(src, src_offset, this.buf, this.buf_offset, count);
				this.buf_offset += count;
				if (this.buf_offset > this.buf_length)
				{
					this.buf_length = this.buf_offset;
				}
				this.buf_dirty = true;
			}
			return count;
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000F92B0 File Offset: 0x000F74B0
		private void FlushBuffer()
		{
			if (this.buf_dirty)
			{
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = this.buf_length;
				int num = 0;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num2 = MonoIO.Write(this.safeHandle, this.buf, num, this.buf_length, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num2;
					num += num2;
				}
			}
			this.buf_start += (long)this.buf_offset;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x000F9378 File Offset: 0x000F7578
		private void FlushBufferIfDirty()
		{
			if (this.buf_dirty)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000F9388 File Offset: 0x000F7588
		private void RefillBuffer()
		{
			this.FlushBuffer();
			this.buf_length = this.ReadData(this.safeHandle, this.buf, 0, this.buf_size);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x000F93B0 File Offset: 0x000F75B0
		private int ReadData(SafeHandle safeHandle, byte[] buf, int offset, int count)
		{
			MonoIOError monoIOError;
			int num = MonoIO.Read(safeHandle, buf, offset, count, out monoIOError);
			if (monoIOError == MonoIOError.ERROR_BROKEN_PIPE)
			{
				num = 0;
			}
			else if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (num == -1)
			{
				throw new IOException();
			}
			return num;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000F93F8 File Offset: 0x000F75F8
		private void InitBuffer(int size, bool isZeroSize)
		{
			if (isZeroSize)
			{
				size = 0;
				this.buf = new byte[1];
			}
			else
			{
				if (size <= 0)
				{
					throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
				}
				size = Math.Max(size, 8);
				if (size <= 4096 && FileStream.buf_recycle != null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle != null)
						{
							this.buf = FileStream.buf_recycle;
							FileStream.buf_recycle = null;
						}
					}
				}
				if (this.buf == null)
				{
					this.buf = new byte[size];
				}
				else
				{
					Array.Clear(this.buf, 0, size);
				}
			}
			this.buf_size = size;
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000F94B8 File Offset: 0x000F76B8
		private string GetSecureFileName(string filename)
		{
			if (!this.anonymous)
			{
				return Path.GetFullPath(filename);
			}
			return Path.GetFileName(filename);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000F94CF File Offset: 0x000F76CF
		private string GetSecureFileName(string filename, bool full)
		{
			if (this.anonymous)
			{
				return Path.GetFileName(filename);
			}
			if (!full)
			{
				return filename;
			}
			return Path.GetFullPath(filename);
		}

		// Token: 0x040020D1 RID: 8401
		private static byte[] buf_recycle;

		// Token: 0x040020D2 RID: 8402
		private static readonly object buf_recycle_lock = new object();

		// Token: 0x040020D3 RID: 8403
		private byte[] buf;

		// Token: 0x040020D4 RID: 8404
		private string name;

		// Token: 0x040020D5 RID: 8405
		private SafeFileHandle safeHandle;

		// Token: 0x040020D6 RID: 8406
		private bool isExposed;

		// Token: 0x040020D7 RID: 8407
		private long append_startpos;

		// Token: 0x040020D8 RID: 8408
		private FileAccess access;

		// Token: 0x040020D9 RID: 8409
		private bool owner;

		// Token: 0x040020DA RID: 8410
		private bool async;

		// Token: 0x040020DB RID: 8411
		private bool canseek;

		// Token: 0x040020DC RID: 8412
		private bool anonymous;

		// Token: 0x040020DD RID: 8413
		private bool buf_dirty;

		// Token: 0x040020DE RID: 8414
		private int buf_size;

		// Token: 0x040020DF RID: 8415
		private int buf_length;

		// Token: 0x040020E0 RID: 8416
		private int buf_offset;

		// Token: 0x040020E1 RID: 8417
		private long buf_start;

		// Token: 0x020007DA RID: 2010
		// (Invoke) Token: 0x060040AD RID: 16557
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x020007DB RID: 2011
		// (Invoke) Token: 0x060040B1 RID: 16561
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
