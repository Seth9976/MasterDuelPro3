using System;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007BD RID: 1981
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
		// Token: 0x06003F4F RID: 16207 RVA: 0x000F355C File Offset: 0x000F175C
		protected FileSystemInfo()
		{
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000F356B File Offset: 0x000F176B
		internal static FileSystemInfo Create(string fullPath, ref FileSystemEntry findData)
		{
			DirectoryInfo directoryInfo = (findData.IsDirectory ? new DirectoryInfo(fullPath, null, new string(findData.FileName), true) : new FileInfo(fullPath, null, new string(findData.FileName), true));
			directoryInfo.Init(findData._info);
			return directoryInfo;
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000F35AC File Offset: 0x000F17AC
		internal unsafe void Init(Interop.NtDll.FILE_FULL_DIR_INFORMATION* info)
		{
			this._data.dwFileAttributes = (int)info->FileAttributes;
			this._data.ftCreationTime = *(Interop.Kernel32.FILE_TIME*)(&info->CreationTime);
			this._data.ftLastAccessTime = *(Interop.Kernel32.FILE_TIME*)(&info->LastAccessTime);
			this._data.ftLastWriteTime = *(Interop.Kernel32.FILE_TIME*)(&info->LastWriteTime);
			this._data.nFileSizeHigh = (uint)(info->EndOfFile >> 32);
			this._data.nFileSizeLow = (uint)info->EndOfFile;
			this._dataInitialized = 0;
		}

		/// <summary>Gets or sets the attributes for the current file or directory.</summary>
		/// <returns>
		///   <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute. -or-The user attempts to set an attribute value but does not have write permission.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000F363D File Offset: 0x000F183D
		public FileAttributes Attributes
		{
			get
			{
				this.EnsureDataInitialized();
				return (FileAttributes)this._data.dwFileAttributes;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x000F3650 File Offset: 0x000F1850
		internal bool ExistsCore
		{
			get
			{
				if (this._dataInitialized == -1)
				{
					this.Refresh();
				}
				return this._dataInitialized == 0 && this._data.dwFileAttributes != -1 && this is DirectoryInfo == ((this._data.dwFileAttributes & 16) == 16);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x000F36A3 File Offset: 0x000F18A3
		internal DateTimeOffset CreationTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftCreationTime.ToDateTimeOffset();
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000F36BB File Offset: 0x000F18BB
		internal DateTimeOffset LastAccessTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftLastAccessTime.ToDateTimeOffset();
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000F36D3 File Offset: 0x000F18D3
		internal DateTimeOffset LastWriteTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftLastWriteTime.ToDateTimeOffset();
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000F36EB File Offset: 0x000F18EB
		internal long LengthCore
		{
			get
			{
				this.EnsureDataInitialized();
				return (long)(((ulong)this._data.nFileSizeHigh << 32) | ((ulong)this._data.nFileSizeLow & (ulong)(-1)));
			}
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000F3712 File Offset: 0x000F1912
		private void EnsureDataInitialized()
		{
			if (this._dataInitialized == -1)
			{
				this._data = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
				this.Refresh();
			}
			if (this._dataInitialized != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(this._dataInitialized, this.FullPath);
			}
		}

		/// <summary>Refreshes the state of the object.</summary>
		/// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F59 RID: 16217 RVA: 0x000F3749 File Offset: 0x000F1949
		public void Refresh()
		{
			this._dataInitialized = FileSystem.FillAttributeInfo(this.FullPath, ref this._data, false);
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x000F3763 File Offset: 0x000F1963
		internal string NormalizedPath
		{
			get
			{
				if (!PathInternal.EndsWithPeriodOrSpace(this.FullPath))
				{
					return this.FullPath;
				}
				return PathInternal.EnsureExtendedPrefix(this.FullPath);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> is null.</exception>
		// Token: 0x06003F5B RID: 16219 RVA: 0x000F3784 File Offset: 0x000F1984
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
			this.OriginalPath = info.GetString("OriginalPath");
			this._name = info.GetString("Name");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06003F5C RID: 16220 RVA: 0x000F37E4 File Offset: 0x000F19E4
		[ComVisible(false)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
			info.AddValue("Name", this.Name, typeof(string));
		}

		/// <summary>Gets the full path of the directory or file.</summary>
		/// <returns>A string containing the full path.</returns>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000F3842 File Offset: 0x000F1A42
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		/// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the Name property gets the name of the directory.</summary>
		/// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000F3554 File Offset: 0x000F1754
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a value indicating whether the file or directory exists.</summary>
		/// <returns>true if the file or directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x000F384C File Offset: 0x000F1A4C
		public virtual bool Exists
		{
			get
			{
				bool flag;
				try
				{
					flag = this.ExistsCore;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		/// <summary>Deletes a file or directory.</summary>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">There is an open handle on the file or directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F60 RID: 16224
		public abstract void Delete();

		/// <summary>Gets or sets the creation time of the current file or directory.</summary>
		/// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000F3878 File Offset: 0x000F1A78
		public DateTime CreationTime
		{
			get
			{
				return this.CreationTimeUtc.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
		/// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000F3894 File Offset: 0x000F1A94
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.CreationTimeCore.UtcDateTime;
			}
		}

		/// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
		/// <returns>The time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x000F38B0 File Offset: 0x000F1AB0
		public DateTime LastAccessTime
		{
			get
			{
				return this.LastAccessTimeUtc.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
		/// <returns>The UTC time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000F38CC File Offset: 0x000F1ACC
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTimeCore.UtcDateTime;
			}
		}

		/// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
		/// <returns>The time the current file was last written.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x000F38E8 File Offset: 0x000F1AE8
		public DateTime LastWriteTime
		{
			get
			{
				return this.LastWriteTimeUtc.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
		/// <returns>The UTC time when the current file was last written to.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000F3904 File Offset: 0x000F1B04
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTimeCore.UtcDateTime;
			}
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000F391F File Offset: 0x000F1B1F
		public override string ToString()
		{
			return this.OriginalPath ?? string.Empty;
		}

		// Token: 0x0400202F RID: 8239
		private Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA _data;

		// Token: 0x04002030 RID: 8240
		private int _dataInitialized = -1;

		/// <summary>Represents the fully qualified path of the directory or file.</summary>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		// Token: 0x04002031 RID: 8241
		protected string FullPath;

		/// <summary>The path originally specified by the user, whether relative or absolute.</summary>
		// Token: 0x04002032 RID: 8242
		protected string OriginalPath;

		// Token: 0x04002033 RID: 8243
		internal string _name;
	}
}
