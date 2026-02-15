using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace System.IO
{
	/// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007BB RID: 1979
	public static class File
	{
		/// <summary>Opens an existing UTF-8 encoded text file for reading.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> on the specified path.</returns>
		/// <param name="path">The file to be opened for reading. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F1E RID: 16158 RVA: 0x000F2C8C File Offset: 0x000F0E8C
		public static StreamReader OpenText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamReader(path);
		}

		/// <summary>Creates or opens a file for writing UTF-8 encoded text.</summary>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that writes to the specified file using UTF-8 encoding.</returns>
		/// <param name="path">The file to be opened for writing. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F1F RID: 16159 RVA: 0x000F2CA2 File Offset: 0x000F0EA2
		public static StreamWriter CreateText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, false);
		}

		/// <summary>Copies an existing file to a new file. Overwriting a file of the same name is not allowed.</summary>
		/// <param name="sourceFileName">The file to copy. </param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destFileName" /> exists.-or- An I/O error has occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F20 RID: 16160 RVA: 0x000F2CB9 File Offset: 0x000F0EB9
		public static void Copy(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName, false);
		}

		/// <summary>Copies an existing file to a new file. Overwriting a file of the same name is allowed.</summary>
		/// <param name="sourceFileName">The file to copy. </param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory. </param>
		/// <param name="overwrite">true if the destination file can be overwritten; otherwise, false. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. -or-<paramref name="destFileName" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destFileName" /> exists and <paramref name="overwrite" /> is false.-or- An I/O error has occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F21 RID: 16161 RVA: 0x000F2CC4 File Offset: 0x000F0EC4
		public static void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", "File name cannot be null.");
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			FileSystem.CopyFile(Path.GetFullPath(sourceFileName), Path.GetFullPath(destFileName), overwrite);
		}

		/// <summary>Creates or overwrites a file in the specified path.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> that provides read/write access to the file specified in <paramref name="path" />.</returns>
		/// <param name="path">The path and name of the file to create. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F22 RID: 16162 RVA: 0x000F2D39 File Offset: 0x000F0F39
		public static FileStream Create(string path)
		{
			return File.Create(path, 4096);
		}

		/// <summary>Creates or overwrites the specified file.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> with the specified buffer size that provides read/write access to the file specified in <paramref name="path" />.</returns>
		/// <param name="path">The name of the file. </param>
		/// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F23 RID: 16163 RVA: 0x000F2D46 File Offset: 0x000F0F46
		public static FileStream Create(string path, int bufferSize)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
		}

		/// <summary>Deletes the specified file. </summary>
		/// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> is a directory.-or- <paramref name="path" /> specified a read-only file. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F24 RID: 16164 RVA: 0x000F2D52 File Offset: 0x000F0F52
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			FileSystem.DeleteFile(Path.GetFullPath(path));
		}

		/// <summary>Determines whether the specified file exists.</summary>
		/// <returns>true if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, false. This method also returns false if <paramref name="path" /> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of <paramref name="path" />.</returns>
		/// <param name="path">The file to check. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F25 RID: 16165 RVA: 0x000F2D70 File Offset: 0x000F0F70
		public static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				path = Path.GetFullPath(path);
				if (path.Length > 0 && PathInternal.IsDirectorySeparator(path[path.Length - 1]))
				{
					return false;
				}
				return FileSystem.FileExists(path);
			}
			catch (ArgumentException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path with read/write access.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> opened in the specified mode and path, with read/write access and not shared.</returns>
		/// <param name="path">The file to open. </param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> specified an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F26 RID: 16166 RVA: 0x000F2DF8 File Offset: 0x000F0FF8
		public static FileStream Open(string path, FileMode mode)
		{
			return File.Open(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, with the specified mode and access.</summary>
		/// <returns>An unshared <see cref="T:System.IO.FileStream" /> that provides access to the specified file, with the specified mode and access.</returns>
		/// <param name="path">The file to open. </param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="access" /> specified Read and <paramref name="mode" /> specified Create, CreateNew, Truncate, or Append. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not Read.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> or <paramref name="access" /> specified an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F27 RID: 16167 RVA: 0x000F2E0A File Offset: 0x000F100A
		public static FileStream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access, FileShare.None);
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
		/// <param name="path">The file to open. </param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file. </param>
		/// <param name="share">A <see cref="T:System.IO.FileShare" /> value specifying the type of access other threads have to the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="access" /> specified Read and <paramref name="mode" /> specified Create, CreateNew, Truncate, or Append. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not Read.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> specified an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F28 RID: 16168 RVA: 0x000F2E15 File Offset: 0x000F1015
		public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(path, mode, access, share);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000F2E20 File Offset: 0x000F1020
		internal static DateTimeOffset GetUtcDateTimeOffset(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Unspecified)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			}
			return dateTime.ToUniversalTime();
		}

		/// <summary>Sets the date and time the file was created.</summary>
		/// <param name="path">The file for which to set the creation date and time information. </param>
		/// <param name="creationTime">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2A RID: 16170 RVA: 0x000F2E44 File Offset: 0x000F1044
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), creationTime, false);
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the file was created.</summary>
		/// <param name="path">The file for which to set the creation date and time information. </param>
		/// <param name="creationTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2B RID: 16171 RVA: 0x000F2E58 File Offset: 0x000F1058
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(creationTimeUtc), false);
		}

		/// <summary>Returns the creation date and time of the specified file or directory.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in local time.</returns>
		/// <param name="path">The file or directory for which to obtain creation date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2C RID: 16172 RVA: 0x000F2E6C File Offset: 0x000F106C
		public static DateTime GetCreationTime(string path)
		{
			return FileSystem.GetCreationTime(Path.GetFullPath(path)).LocalDateTime;
		}

		/// <summary>Sets the date and time the specified file was last accessed.</summary>
		/// <param name="path">The file for which to set the access date and time information. </param>
		/// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2D RID: 16173 RVA: 0x000F2E8C File Offset: 0x000F108C
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), lastAccessTime, false);
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.</summary>
		/// <param name="path">The file for which to set the access date and time information. </param>
		/// <param name="lastAccessTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2E RID: 16174 RVA: 0x000F2EA0 File Offset: 0x000F10A0
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastAccessTimeUtc), false);
		}

		/// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <param name="path">The file or directory for which to obtain access date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F2F RID: 16175 RVA: 0x000F2EB4 File Offset: 0x000F10B4
		public static DateTime GetLastAccessTime(string path)
		{
			return FileSystem.GetLastAccessTime(Path.GetFullPath(path)).LocalDateTime;
		}

		/// <summary>Sets the date and time that the specified file was last written to.</summary>
		/// <param name="path">The file for which to set the date and time information. </param>
		/// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F30 RID: 16176 RVA: 0x000F2ED4 File Offset: 0x000F10D4
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), lastWriteTime, false);
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.</summary>
		/// <param name="path">The file for which to set the date and time information. </param>
		/// <param name="lastWriteTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F31 RID: 16177 RVA: 0x000F2EE8 File Offset: 0x000F10E8
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastWriteTimeUtc), false);
		}

		/// <summary>Returns the date and time the specified file or directory was last written to.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <param name="path">The file or directory for which to obtain write date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F32 RID: 16178 RVA: 0x000F2EFC File Offset: 0x000F10FC
		public static DateTime GetLastWriteTime(string path)
		{
			return FileSystem.GetLastWriteTime(Path.GetFullPath(path)).LocalDateTime;
		}

		/// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time.</returns>
		/// <param name="path">The file or directory for which to obtain write date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F33 RID: 16179 RVA: 0x000F2F1C File Offset: 0x000F111C
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return FileSystem.GetLastWriteTime(Path.GetFullPath(path)).UtcDateTime;
		}

		/// <summary>Gets the <see cref="T:System.IO.FileAttributes" /> of the file on the path.</summary>
		/// <returns>The <see cref="T:System.IO.FileAttributes" /> of the file on the path.</returns>
		/// <param name="path">The path to the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="path" /> represents a file and is invalid, such as being on an unmapped drive, or the file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> represents a directory and is invalid, such as being on an unmapped drive, or the directory cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">This file is being used by another process.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F34 RID: 16180 RVA: 0x000F2F3C File Offset: 0x000F113C
		public static FileAttributes GetAttributes(string path)
		{
			return FileSystem.GetAttributes(Path.GetFullPath(path));
		}

		/// <summary>Sets the specified <see cref="T:System.IO.FileAttributes" /> of the file on the specified path.</summary>
		/// <param name="path">The path to the file. </param>
		/// <param name="fileAttributes">A bitwise combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty, contains only white spaces, contains invalid characters, or the file attribute is invalid. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F35 RID: 16181 RVA: 0x000F2F4C File Offset: 0x000F114C
		public static void SetAttributes(string path, FileAttributes fileAttributes)
		{
			if ((fileAttributes & (FileAttributes)(-2147483648)) == (FileAttributes)0)
			{
				FileSystem.SetAttributes(Path.GetFullPath(path), fileAttributes);
				return;
			}
			Path.Validate(path);
			MonoIOError monoIOError;
			if (!MonoIO.SetFileAttributes(path, fileAttributes, out monoIOError))
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		/// <summary>Opens an existing file for reading.</summary>
		/// <returns>A read-only <see cref="T:System.IO.FileStream" /> on the specified path.</returns>
		/// <param name="path">The file to be opened for reading. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F36 RID: 16182 RVA: 0x000F2F88 File Offset: 0x000F1188
		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		/// <summary>Opens an existing file or creates a new file for writing.</summary>
		/// <returns>An unshared <see cref="T:System.IO.FileStream" /> object on the specified path with <see cref="F:System.IO.FileAccess.Write" /> access.</returns>
		/// <param name="path">The file to be opened for writing. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a read-only file or directory. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F37 RID: 16183 RVA: 0x000F2F93 File Offset: 0x000F1193
		public static FileStream OpenWrite(string path)
		{
			return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		/// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
		/// <returns>A string containing all lines of the file.</returns>
		/// <param name="path">The file to open for reading. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F38 RID: 16184 RVA: 0x000F2F9E File Offset: 0x000F119E
		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllText(path, Encoding.UTF8);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000F2FD4 File Offset: 0x000F11D4
		private static string InternalReadAllText(string path, Encoding encoding)
		{
			string text;
			using (StreamReader streamReader = new StreamReader(path, encoding, true))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		/// <summary>Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to. </param>
		/// <param name="contents">The string to write to the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null or <paramref name="contents" /> is empty.  </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F3A RID: 16186 RVA: 0x000F3010 File Offset: 0x000F1210
		public static void WriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(contents);
			}
		}

		/// <summary>Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to. </param>
		/// <param name="contents">The string to write to the file. </param>
		/// <param name="encoding">The encoding to apply to the string.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null or <paramref name="contents" /> is empty. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F3B RID: 16187 RVA: 0x000F3070 File Offset: 0x000F1270
		public static void WriteAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		/// <summary>Opens a binary file, reads the contents of the file into a byte array, and then closes the file.</summary>
		/// <returns>A byte array containing the contents of the file.</returns>
		/// <param name="path">The file to open for reading. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F3C RID: 16188 RVA: 0x000F30E0 File Offset: 0x000F12E0
		public static byte[] ReadAllBytes(string path)
		{
			byte[] array;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1))
			{
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException("The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size.");
				}
				if (length == 0L)
				{
					array = File.ReadAllBytesUnknownLength(fileStream);
				}
				else
				{
					int num = 0;
					int i = (int)length;
					byte[] array2 = new byte[i];
					while (i > 0)
					{
						int num2 = fileStream.Read(array2, num, i);
						if (num2 == 0)
						{
							throw Error.GetEndOfFile();
						}
						num += num2;
						i -= num2;
					}
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000F3178 File Offset: 0x000F1378
		private unsafe static byte[] ReadAllBytesUnknownLength(FileStream fs)
		{
			byte[] array = null;
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)512], 512);
			byte[] array3;
			try
			{
				int num = 0;
				for (;;)
				{
					if (num == span.Length)
					{
						uint num2 = (uint)(span.Length * 2);
						if (num2 > 2147483591U)
						{
							num2 = (uint)Math.Max(2147483591, span.Length + 1);
						}
						byte[] array2 = ArrayPool<byte>.Shared.Rent((int)num2);
						span.CopyTo(array2);
						if (array != null)
						{
							ArrayPool<byte>.Shared.Return(array, false);
						}
						span = (array = array2);
					}
					int num3 = fs.Read(span.Slice(num));
					if (num3 == 0)
					{
						break;
					}
					num += num3;
				}
				array3 = span.Slice(0, num).ToArray();
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return array3;
		}

		/// <summary>Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to. </param>
		/// <param name="bytes">The bytes to write to the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null or the byte array is empty. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F3E RID: 16190 RVA: 0x000F3258 File Offset: 0x000F1458
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Path cannot be null.");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000F32A8 File Offset: 0x000F14A8
		private static void InternalWriteAllBytes(string path, byte[] bytes)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		/// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
		/// <returns>A string array containing all lines of the file.</returns>
		/// <param name="path">The file to open for reading. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F40 RID: 16192 RVA: 0x000F32E8 File Offset: 0x000F14E8
		public static string[] ReadAllLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllLines(path, Encoding.UTF8);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000F331C File Offset: 0x000F151C
		private static string[] InternalReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		/// <summary>Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.</summary>
		/// <param name="path">The file to append the specified string to. </param>
		/// <param name="contents">The string to append to the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn’t exist or it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000F336C File Offset: 0x000F156C
		public static void AppendAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, true))
			{
				streamWriter.Write(contents);
			}
		}

		/// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
		/// <param name="sourceFileName">The name of the file to move. </param>
		/// <param name="destFileName">The new path for the file. </param>
		/// <exception cref="T:System.IO.IOException">The destination file already exists.-or-<paramref name="sourceFileName" /> was not found. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains invalid characters as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F43 RID: 16195 RVA: 0x000F33CC File Offset: 0x000F15CC
		public static void Move(string sourceFileName, string destFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", "File name cannot be null.");
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(sourceFileName);
			string fullPath2 = Path.GetFullPath(destFileName);
			if (!FileSystem.FileExists(fullPath))
			{
				throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", fullPath), fullPath);
			}
			FileSystem.MoveFile(fullPath, fullPath2);
		}
	}
}
