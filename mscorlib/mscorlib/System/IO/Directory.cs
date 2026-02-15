using System;
using System.Collections.Generic;
using System.IO.Enumeration;

namespace System.IO
{
	/// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007B8 RID: 1976
	public static class Directory
	{
		/// <summary>Creates all directories and subdirectories in the specified path.</summary>
		/// <returns>An object that represents the directory for the specified path.</returns>
		/// <param name="path">The directory path to create. </param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.-or-The network name is not known.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or-<paramref name="path" /> is prefixed with, or contains only a colon character (:).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE1 RID: 16097 RVA: 0x000F2553 File Offset: 0x000F0753
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			string fullPath = Path.GetFullPath(path);
			FileSystem.CreateDirectory(fullPath);
			return new DirectoryInfo(fullPath, null, null, false);
		}

		/// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
		/// <returns>true if <paramref name="path" /> refers to an existing directory; otherwise, false.</returns>
		/// <param name="path">The path to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE2 RID: 16098 RVA: 0x000F2590 File Offset: 0x000F0790
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
				return FileSystem.DirectoryExists(Path.GetFullPath(path));
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

		/// <summary>Sets the creation date and time for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information. </param>
		/// <param name="creationTime">An object that contains the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE3 RID: 16099 RVA: 0x000F25F4 File Offset: 0x000F07F4
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), creationTime, true);
		}

		/// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information. </param>
		/// <param name="creationTimeUtc">An object that  contains the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE4 RID: 16100 RVA: 0x000F2608 File Offset: 0x000F0808
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(creationTimeUtc), true);
		}

		/// <summary>Sets the date and time a directory was last written to.</summary>
		/// <param name="path">The path of the directory. </param>
		/// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.  </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE5 RID: 16101 RVA: 0x000F261C File Offset: 0x000F081C
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), lastWriteTime, true);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
		/// <param name="path">The path of the directory. </param>
		/// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE6 RID: 16102 RVA: 0x000F2630 File Offset: 0x000F0830
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastWriteTimeUtc), true);
		}

		/// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information. </param>
		/// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000F2644 File Offset: 0x000F0844
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), lastAccessTime, true);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information. </param>
		/// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE8 RID: 16104 RVA: 0x000F2658 File Offset: 0x000F0858
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastAccessTimeUtc), true);
		}

		/// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory.</returns>
		/// <param name="path">The directory from which to retrieve the files. </param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000F266C File Offset: 0x000F086C
		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EEA RID: 16106 RVA: 0x000F267E File Offset: 0x000F087E
		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. -or- <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchpattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		// Token: 0x06003EEB RID: 16107 RVA: 0x000F268C File Offset: 0x000F088C
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000F269B File Offset: 0x000F089B
		public static string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Files, enumerationOptions).ToArray<string>();
		}

		/// <summary>Gets the names of subdirectories (including their paths) in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) of subdirectories in the specified path.</returns>
		/// <param name="path">The path for which an array of subdirectory names is returned. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EED RID: 16109 RVA: 0x000F26AB File Offset: 0x000F08AB
		public static string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Gets the names of subdirectories (including their paths) that match the specified search pattern in the current directory.</summary>
		/// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern.</returns>
		/// <param name="path">The path to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EEE RID: 16110 RVA: 0x000F26BD File Offset: 0x000F08BD
		public static string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetDirectories(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000F26CB File Offset: 0x000F08CB
		public static string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions).ToArray<string>();
		}

		/// <summary>Returns the names of all files and subdirectories in the specified directory.</summary>
		/// <returns>An array of the names of files and subdirectories in the specified directory.</returns>
		/// <param name="path">The directory for which file and subdirectory names are returned. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EF0 RID: 16112 RVA: 0x000F26DB File Offset: 0x000F08DB
		public static string[] GetFileSystemEntries(string path)
		{
			return Directory.GetFileSystemEntries(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an array of file system entries that match the specified search criteria.</summary>
		/// <returns>An array of file system entries that match the specified search criteria.</returns>
		/// <param name="path">The path to be searched. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The <paramref name="searchPattern" /> parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EF1 RID: 16113 RVA: 0x000F26ED File Offset: 0x000F08ED
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000F26FB File Offset: 0x000F08FB
		public static string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<string>();
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000F270C File Offset: 0x000F090C
		internal static IEnumerable<string> InternalEnumeratePaths(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.UserFiles(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.UserDirectories(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.UserEntries(path, searchPattern, options);
			default:
				throw new ArgumentOutOfRangeException("searchTarget");
			}
		}

		/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06003EF4 RID: 16116 RVA: 0x000F277A File Offset: 0x000F097A
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateDirectories(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000F2789 File Offset: 0x000F0989
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions);
		}

		/// <summary>Returns the volume information, root information, or both for the specified path.</summary>
		/// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
		/// <param name="path">The path of a file or directory. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EF6 RID: 16118 RVA: 0x000F2794 File Offset: 0x000F0994
		public static string GetDirectoryRoot(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPath = Path.GetFullPath(path);
			return fullPath.Substring(0, PathInternal.GetRootLength(fullPath));
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000F27C8 File Offset: 0x000F09C8
		internal static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, PathInternal.GetRootLength(path));
		}

		/// <summary>Gets the current working directory of the application.</summary>
		/// <returns>A string that contains the path of the current working directory, and does not end with a backslash (\).</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows CE, which does not have current directory functionality.This method is available in the .NET Compact Framework, but is not currently supported.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EF8 RID: 16120 RVA: 0x000F27E1 File Offset: 0x000F09E1
		public static string GetCurrentDirectory()
		{
			return Environment.CurrentDirectory;
		}

		/// <summary>Moves a file or a directory and its contents to a new location.</summary>
		/// <param name="sourceDirName">The path of the file or directory to move. </param>
		/// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume. -or- <paramref name="destDirName" /> already exists. -or- The <paramref name="sourceDirName" /> and <paramref name="destDirName" /> parameters refer to the same file or directory. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified by <paramref name="sourceDirName" /> is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EF9 RID: 16121 RVA: 0x000F27E8 File Offset: 0x000F09E8
		public static void Move(string sourceDirName, string destDirName)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (sourceDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destDirName");
			}
			string fullPath = Path.GetFullPath(sourceDirName);
			string text = PathInternal.EnsureTrailingSeparator(fullPath);
			string fullPath2 = Path.GetFullPath(destDirName);
			string text2 = PathInternal.EnsureTrailingSeparator(fullPath2);
			StringComparison stringComparison = PathInternal.StringComparison;
			if (string.Equals(text, text2, stringComparison))
			{
				throw new IOException("Source and destination path must be different.");
			}
			string pathRoot = Path.GetPathRoot(text);
			string pathRoot2 = Path.GetPathRoot(text2);
			if (!string.Equals(pathRoot, pathRoot2, stringComparison))
			{
				throw new IOException("Source and destination path must have identical roots. Move will not work across volumes.");
			}
			if (!FileSystem.DirectoryExists(fullPath) && !FileSystem.FileExists(fullPath))
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", fullPath));
			}
			if (FileSystem.DirectoryExists(fullPath2))
			{
				throw new IOException(SR.Format("Cannot create '{0}' because a file or directory with the same name already exists.", fullPath2));
			}
			FileSystem.MoveDirectory(fullPath, fullPath2);
		}

		/// <summary>Deletes an empty directory from a specified path.</summary>
		/// <param name="path">The name of the empty directory to remove. This directory must be writable and empty. </param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory is the application's current working directory.-or-The directory specified by <paramref name="path" /> is not empty.-or-The directory is read-only or contains a read-only file.-or-The directory is being used by another process.-or-There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from directories. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.-or-<paramref name="path" /> refers to a file instead of a directory.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EFA RID: 16122 RVA: 0x000F28DA File Offset: 0x000F0ADA
		public static void Delete(string path)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), false);
		}

		/// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory. </summary>
		/// <param name="path">The name of the directory to remove. </param>
		/// <param name="recursive">true to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, false. </param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory specified by <paramref name="path" /> is read-only, or <paramref name="recursive" /> is false and <paramref name="path" /> is not an empty directory. -or-The directory is the application's current working directory. -or-The directory contains a read-only file.-or-The directory is being used by another process.There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.-or-<paramref name="path" /> refers to a file instead of a directory.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003EFB RID: 16123 RVA: 0x000F28E8 File Offset: 0x000F0AE8
		public static void Delete(string path, bool recursive)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), recursive);
		}

		/// <summary>Retrieves the names of the logical drives on this computer in the form "&lt;drive letter&gt;:\".</summary>
		/// <returns>The logical drives on this computer.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occured (for example, a disk error). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003EFC RID: 16124 RVA: 0x000F28F6 File Offset: 0x000F0AF6
		public static string[] GetLogicalDrives()
		{
			return FileSystem.GetLogicalDrives();
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000F2900 File Offset: 0x000F0B00
		internal static string InsecureGetCurrentDirectory()
		{
			MonoIOError monoIOError;
			string currentDirectory = MonoIO.GetCurrentDirectory(out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			return currentDirectory;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000F2920 File Offset: 0x000F0B20
		internal static void InsecureSetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("path string must not be an empty string or whitespace string");
			}
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
			}
			MonoIOError monoIOError;
			MonoIO.SetCurrentDirectory(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}
	}
}
