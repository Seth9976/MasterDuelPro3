using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007BC RID: 1980
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		// Token: 0x06003F44 RID: 16196 RVA: 0x000F345E File Offset: 0x000F165E
		private FileInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileInfo" /> class, which acts as a wrapper for a file path.</summary>
		/// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="fileName" /> contains a colon (:) in the middle of the string. </exception>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000F3466 File Offset: 0x000F1666
		public FileInfo(string fileName)
			: this(fileName, null, null, false)
		{
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000F3474 File Offset: 0x000F1674
		internal FileInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.OriginalPath = originalPath;
			fullPath = fullPath ?? originalPath;
			this.FullPath = (isNormalized ? (fullPath ?? originalPath) : Path.GetFullPath(fullPath));
			this._name = fileName ?? Path.GetFileName(originalPath);
		}

		/// <summary>Gets the size, in bytes, of the current file.</summary>
		/// <returns>The size of the current file in bytes.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.-or- The Length property is called for a directory. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000F34CE File Offset: 0x000F16CE
		public long Length
		{
			get
			{
				if ((base.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
				{
					throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", this.FullPath), this.FullPath);
				}
				return base.LengthCore;
			}
		}

		/// <summary>Gets a string representing the directory's full path.</summary>
		/// <returns>A string representing the directory's full path.</returns>
		/// <exception cref="T:System.ArgumentNullException">null was passed in for the directory name. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000F34FF File Offset: 0x000F16FF
		public string DirectoryName
		{
			get
			{
				return Path.GetDirectoryName(this.FullPath);
			}
		}

		/// <summary>Gets an instance of the parent directory.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object representing the parent directory of this file.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000F350C File Offset: 0x000F170C
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that writes a new text file.</summary>
		/// <returns>A new StreamWriter.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The file name is a directory. </exception>
		/// <exception cref="T:System.IO.IOException">The disk is read-only. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F4A RID: 16202 RVA: 0x000F352B File Offset: 0x000F172B
		public StreamWriter CreateText()
		{
			return new StreamWriter(base.NormalizedPath, false);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="T:System.IO.FileInfo" />.</summary>
		/// <returns>A new StreamWriter.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F4B RID: 16203 RVA: 0x000F3539 File Offset: 0x000F1739
		public StreamWriter AppendText()
		{
			return new StreamWriter(base.NormalizedPath, true);
		}

		/// <summary>Permanently deletes a file.</summary>
		/// <exception cref="T:System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.-or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The path is a directory. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003F4C RID: 16204 RVA: 0x000F3547 File Offset: 0x000F1747
		public override void Delete()
		{
			FileSystem.DeleteFile(this.FullPath);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000F2B77 File Offset: 0x000F0D77
		private FileInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the name of the file.</summary>
		/// <returns>The name of the file.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000F3554 File Offset: 0x000F1754
		public override string Name
		{
			get
			{
				return this._name;
			}
		}
	}
}
