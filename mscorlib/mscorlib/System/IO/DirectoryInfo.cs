using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007B9 RID: 1977
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryInfo" /> class on the specified path.</summary>
		/// <param name="path">A string specifying the path on which to create the DirectoryInfo. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		// Token: 0x06003EFF RID: 16127 RVA: 0x000F2985 File Offset: 0x000F0B85
		public DirectoryInfo(string path)
		{
			this.Init(path, Path.GetFullPath(path), null, true);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x000F299C File Offset: 0x000F0B9C
		internal DirectoryInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			this.Init(originalPath, fullPath, fileName, isNormalized);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000F29B0 File Offset: 0x000F0BB0
		private void Init(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("path");
			}
			this.OriginalPath = originalPath;
			fullPath = fullPath ?? originalPath;
			fullPath = (isNormalized ? fullPath : Path.GetFullPath(fullPath));
			this._name = fileName ?? (PathInternal.IsRoot(fullPath) ? fullPath : Path.GetFileName(PathInternal.TrimEndingDirectorySeparator(fullPath.AsSpan()))).ToString();
			this.FullPath = fullPath;
		}

		/// <summary>Gets the parent directory of a specified subdirectory.</summary>
		/// <returns>The parent directory, or null if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x000F2A30 File Offset: 0x000F0C30
		public DirectoryInfo Parent
		{
			get
			{
				string directoryName = Path.GetDirectoryName(PathInternal.IsRoot(this.FullPath) ? this.FullPath : PathInternal.TrimEndingDirectorySeparator(this.FullPath));
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName, null, null, false);
			}
		}

		/// <summary>Returns a file list from the current directory.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F03 RID: 16131 RVA: 0x000F2A76 File Offset: 0x000F0C76
		public FileInfo[] GetFiles()
		{
			return this.GetFiles("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <param name="searchPattern">The search string, such as "*.txt". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F04 RID: 16132 RVA: 0x000F2A88 File Offset: 0x000F0C88
		public FileInfo[] GetFiles(string searchPattern)
		{
			return this.GetFiles(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000F2A96 File Offset: 0x000F0C96
		public FileInfo[] GetFiles(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<FileInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Files, enumerationOptions)).ToArray<FileInfo>();
		}

		/// <summary>Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries representing all the files and subdirectories in a directory.</summary>
		/// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F06 RID: 16134 RVA: 0x000F2AB0 File Offset: 0x000F0CB0
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.GetFileSystemInfos("*", EnumerationOptions.Compatible);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000F2AC2 File Offset: 0x000F0CC2
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<FileSystemInfo>();
		}

		/// <summary>Returns the subdirectories of the current directory.</summary>
		/// <returns>An array of <see cref="T:System.IO.DirectoryInfo" /> objects.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F08 RID: 16136 RVA: 0x000F2AD7 File Offset: 0x000F0CD7
		public DirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories("*", EnumerationOptions.Compatible);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000F2AE9 File Offset: 0x000F0CE9
		public DirectoryInfo[] GetDirectories(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<DirectoryInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Directories, enumerationOptions)).ToArray<DirectoryInfo>();
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000F2B04 File Offset: 0x000F0D04
		internal static IEnumerable<FileSystemInfo> InternalEnumerateInfos(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.FileInfos(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.DirectoryInfos(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.FileSystemInfos(path, searchPattern, options);
			default:
				throw new ArgumentException("Enum value was out of legal range.", "searchTarget");
			}
		}

		/// <summary>Deletes this <see cref="T:System.IO.DirectoryInfo" /> if it is empty.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is not empty. -or-The directory is the application's current working directory.-or-There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F0B RID: 16139 RVA: 0x000F2B69 File Offset: 0x000F0D69
		public override void Delete()
		{
			FileSystem.RemoveDirectory(this.FullPath, false);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000F2B77 File Offset: 0x000F0D77
		private DirectoryInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
