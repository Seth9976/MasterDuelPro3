using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files and folders. This class cannot be inherited.</summary>
	// Token: 0x0200035C RID: 860
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001E08 RID: 7688 RVA: 0x000766E6 File Offset: 0x000748E6
		public FileIOPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.m_Unrestricted = true;
				this.m_AllFilesAccess = FileIOPermissionAccess.AllAccess;
				this.m_AllLocalFilesAccess = FileIOPermissionAccess.AllAccess;
			}
			this.CreateLists();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="path">The absolute path of the file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter does not specify the absolute path to the file or directory. </exception>
		// Token: 0x06001E09 RID: 7689 RVA: 0x00076715 File Offset: 0x00074915
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CreateLists();
			this.AddPathList(access, path);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00076739 File Offset: 0x00074939
		internal void CreateLists()
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.appendList = new ArrayList();
			this.pathList = new ArrayList();
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>The set of file I/O flags for all files.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00076767 File Offset: 0x00074967
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0007676F File Offset: 0x0007496F
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_AllFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllFilesAccess = value;
				}
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>The set of file I/O flags for all local files.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00076780 File Offset: 0x00074980
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x00076788 File Offset: 0x00074988
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_AllLocalFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllLocalFilesAccess = value;
				}
			}
		}

		/// <summary>Adds access for the specified file or directory to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="path">The absolute path of a file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter did not specify the absolute path to the file or directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter has an invalid format. </exception>
		// Token: 0x06001E0F RID: 7695 RVA: 0x00076799 File Offset: 0x00074999
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.AddPathInternal(access, path);
		}

		/// <summary>Adds access for the specified files and directories to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not valid. </exception>
		/// <exception cref="T:System.NotSupportedException">An entry in the <paramref name="pathList" /> array has an invalid format. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		// Token: 0x06001E10 RID: 7696 RVA: 0x000767B8 File Offset: 0x000749B8
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			foreach (string text in pathList)
			{
				this.AddPathInternal(access, text);
			}
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000767F8 File Offset: 0x000749F8
		internal void AddPathInternal(FileIOPermissionAccess access, string path)
		{
			path = Path.InsecureGetFullPath(path);
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Add(path);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Add(path);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001E12 RID: 7698 RVA: 0x0007685C File Offset: 0x00074A5C
		public override IPermission Copy()
		{
			if (this.m_Unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			return new FileIOPermission(PermissionState.None)
			{
				readList = (ArrayList)this.readList.Clone(),
				writeList = (ArrayList)this.writeList.Clone(),
				appendList = (ArrayList)this.appendList.Clone(),
				pathList = (ArrayList)this.pathList.Clone(),
				m_AllFilesAccess = this.m_AllFilesAccess,
				m_AllLocalFilesAccess = this.m_AllLocalFilesAccess
			};
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not compatible. </exception>
		// Token: 0x06001E13 RID: 7699 RVA: 0x000768F0 File Offset: 0x00074AF0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.m_Unrestricted = true;
				return;
			}
			this.m_Unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Read, array);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Write, array);
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Append, array);
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.PathDiscovery, array);
			}
		}

		/// <summary>Gets all files and directories with the specified <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.</summary>
		/// <returns>An array containing the paths of the files and directories to which access specified by the <paramref name="access" /> parameter is granted.</returns>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents a single type of file access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- <paramref name="access" /> is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, which represents more than one type of file access, or <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, which does not represent any type of file access. </exception>
		// Token: 0x06001E14 RID: 7700 RVA: 0x000769A8 File Offset: 0x00074BA8
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			ArrayList arrayList = new ArrayList();
			switch (access)
			{
			case FileIOPermissionAccess.NoAccess:
				goto IL_007F;
			case FileIOPermissionAccess.Read:
				arrayList.AddRange(this.readList);
				goto IL_007F;
			case FileIOPermissionAccess.Write:
				arrayList.AddRange(this.writeList);
				goto IL_007F;
			case FileIOPermissionAccess.Append:
				arrayList.AddRange(this.appendList);
				goto IL_007F;
			case FileIOPermissionAccess.PathDiscovery:
				arrayList.AddRange(this.pathList);
				goto IL_007F;
			}
			FileIOPermission.ThrowInvalidFlag(access, false);
			IL_007F:
			if (arrayList.Count <= 0)
			{
				return null;
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E15 RID: 7701 RVA: 0x00076A54 File Offset: 0x00074C54
		public override IPermission Intersect(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission2 = new FileIOPermission(PermissionState.None);
			fileIOPermission2.AllFiles = this.m_AllFilesAccess & fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles = this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles;
			FileIOPermission.IntersectKeys(this.readList, fileIOPermission.readList, fileIOPermission2.readList);
			FileIOPermission.IntersectKeys(this.writeList, fileIOPermission.writeList, fileIOPermission2.writeList);
			FileIOPermission.IntersectKeys(this.appendList, fileIOPermission.appendList, fileIOPermission2.appendList);
			FileIOPermission.IntersectKeys(this.pathList, fileIOPermission.pathList, fileIOPermission2.pathList);
			if (!fileIOPermission2.IsEmpty())
			{
				return fileIOPermission2;
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E16 RID: 7702 RVA: 0x00076B20 File Offset: 0x00074D20
		public override bool IsSubsetOf(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return false;
			}
			if (fileIOPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.IsUnrestricted();
			}
			return fileIOPermission.IsUnrestricted() || ((this.m_AllFilesAccess & fileIOPermission.AllFiles) == this.m_AllFilesAccess && (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles) == this.m_AllLocalFilesAccess && FileIOPermission.KeyIsSubsetOf(this.appendList, fileIOPermission.appendList) && FileIOPermission.KeyIsSubsetOf(this.readList, fileIOPermission.readList) && FileIOPermission.KeyIsSubsetOf(this.writeList, fileIOPermission.writeList) && FileIOPermission.KeyIsSubsetOf(this.pathList, fileIOPermission.pathList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06001E17 RID: 7703 RVA: 0x00076BE4 File Offset: 0x00074DE4
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001E18 RID: 7704 RVA: 0x00076BEC File Offset: 0x00074DEC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string[] array = this.GetPathList(FileIOPermissionAccess.Append);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Append", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Read);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Read", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Write);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Write", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.PathDiscovery);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("PathDiscovery", string.Join(";", array));
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E19 RID: 7705 RVA: 0x00076CB4 File Offset: 0x00074EB4
		public override IPermission Union(IPermission other)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(other);
			if (fileIOPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && fileIOPermission.IsEmpty())
			{
				return null;
			}
			FileIOPermission fileIOPermission2 = (FileIOPermission)this.Copy();
			fileIOPermission2.AllFiles |= fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles |= fileIOPermission.AllLocalFiles;
			string[] array = fileIOPermission.GetPathList(FileIOPermissionAccess.Read);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.readList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Write);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.writeList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Append);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.appendList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.PathDiscovery);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.pathList, array);
			}
			return fileIOPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.FileIOPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.FileIOPermission" />. </param>
		// Token: 0x06001E1A RID: 7706 RVA: 0x00033991 File Offset: 0x00031B91
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return false;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.FileIOPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object.</returns>
		// Token: 0x06001E1B RID: 7707 RVA: 0x00076D8A File Offset: 0x00074F8A
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00076D94 File Offset: 0x00074F94
		private bool IsEmpty()
		{
			return !this.m_Unrestricted && this.appendList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0 && this.pathList.Count == 0;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00076DE0 File Offset: 0x00074FE0
		private static FileIOPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileIOPermission));
			}
			return fileIOPermission;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00076E00 File Offset: 0x00075000
		internal static void ThrowInvalidFlag(FileIOPermissionAccess access, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, access), "access");
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00076E40 File Offset: 0x00075040
		internal static void ThrowIfInvalidPath(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != null && directoryName.LastIndexOfAny(FileIOPermission.BadPathNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), path), "path");
			}
			string fileName = Path.GetFileName(path);
			if (fileName != null && fileName.LastIndexOfAny(FileIOPermission.BadFileNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), path), "path");
			}
			if (!Path.IsPathRooted(path))
			{
				throw new ArgumentException(Locale.GetText("Absolute path information is required."), "path");
			}
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00076ED0 File Offset: 0x000750D0
		internal static void ThrowIfInvalidPath(string[] paths)
		{
			for (int i = 0; i < paths.Length; i++)
			{
				FileIOPermission.ThrowIfInvalidPath(paths[i]);
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00076EF8 File Offset: 0x000750F8
		internal static bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				using (IEnumerator enumerator2 = target.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (Path.IsPathSubsetOf((string)enumerator2.Current, text))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00076FA0 File Offset: 0x000751A0
		internal static void UnionKeys(IList list, string[] paths)
		{
			foreach (string text in paths)
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					int j;
					for (j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (Path.IsPathSubsetOf(text, text2))
						{
							list[j] = text;
							break;
						}
						if (Path.IsPathSubsetOf(text2, text))
						{
							break;
						}
					}
					if (j == count)
					{
						list.Add(text);
					}
				}
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00077020 File Offset: 0x00075220
		internal static void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (Path.IsPathSubsetOf(text, text2))
						{
							result.Add(text2);
						}
					}
					else if (Path.IsPathSubsetOf(text2, text))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x04000DF3 RID: 3571
		private static char[] BadPathNameCharacters = Path.GetInvalidPathChars();

		// Token: 0x04000DF4 RID: 3572
		private static char[] BadFileNameCharacters = Path.GetInvalidFileNameChars();

		// Token: 0x04000DF5 RID: 3573
		private bool m_Unrestricted;

		// Token: 0x04000DF6 RID: 3574
		private FileIOPermissionAccess m_AllFilesAccess;

		// Token: 0x04000DF7 RID: 3575
		private FileIOPermissionAccess m_AllLocalFilesAccess;

		// Token: 0x04000DF8 RID: 3576
		private ArrayList readList;

		// Token: 0x04000DF9 RID: 3577
		private ArrayList writeList;

		// Token: 0x04000DFA RID: 3578
		private ArrayList appendList;

		// Token: 0x04000DFB RID: 3579
		private ArrayList pathList;
	}
}
