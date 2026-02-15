using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000095 RID: 149
	internal abstract class FileSystem
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00019164 File Offset: 0x00017364
		public FSEntry ChangeDirectory(string folder)
		{
			if (folder == MWFVFS.DesktopPrefix)
			{
				this.currentTopFolder = MWFVFS.DesktopPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetDesktopFSEntry());
			}
			else if (folder == MWFVFS.PersonalPrefix)
			{
				this.currentTopFolder = MWFVFS.PersonalPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetPersonalFSEntry());
			}
			else if (folder == MWFVFS.MyComputerPersonalPrefix)
			{
				this.currentTopFolder = MWFVFS.MyComputerPersonalPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyComputerPersonalFSEntry());
			}
			else if (folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.currentTopFolder = MWFVFS.RecentlyUsedPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetRecentlyUsedFSEntry());
			}
			else if (folder == MWFVFS.MyComputerPrefix)
			{
				this.currentTopFolder = MWFVFS.MyComputerPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyComputerFSEntry());
			}
			else if (folder == MWFVFS.MyNetworkPrefix)
			{
				this.currentTopFolder = MWFVFS.MyNetworkPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyNetworkFSEntry());
			}
			else
			{
				bool flag = false;
				foreach (object obj in MWFVFS.MyComputerDevicesPrefix)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					FSEntry fsentry = dictionaryEntry.Value as FSEntry;
					if (folder == fsentry.FullName)
					{
						this.currentTopFolder = dictionaryEntry.Key as string;
						this.currentTopFolderFSEntry = (this.currentFolderFSEntry = fsentry);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.currentFolderFSEntry = this.GetDirectoryFSEntry(new DirectoryInfo(folder), this.currentTopFolderFSEntry);
				}
			}
			return this.currentFolderFSEntry;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001934C File Offset: 0x0001754C
		public string GetParent()
		{
			return this.currentFolderFSEntry.Parent;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001935C File Offset: 0x0001755C
		public void GetFolderContent(StringCollection filters, out ArrayList directories_out, out ArrayList files_out)
		{
			directories_out = new ArrayList();
			files_out = new ArrayList();
			if (this.currentFolderFSEntry.FullName == MWFVFS.DesktopPrefix)
			{
				FSEntry personalFSEntry = this.GetPersonalFSEntry();
				directories_out.Add(personalFSEntry);
				FSEntry myComputerFSEntry = this.GetMyComputerFSEntry();
				directories_out.Add(myComputerFSEntry);
				FSEntry myNetworkFSEntry = this.GetMyNetworkFSEntry();
				directories_out.Add(myNetworkFSEntry);
				ArrayList arrayList = null;
				ArrayList arrayList2 = null;
				this.GetNormalFolderContent(ThemeEngine.Current.Places(UIIcon.PlacesDesktop), filters, out arrayList, out arrayList2);
				directories_out.AddRange(arrayList);
				files_out.AddRange(arrayList2);
				return;
			}
			if (this.currentFolderFSEntry.FullName == MWFVFS.RecentlyUsedPrefix)
			{
				files_out = this.GetRecentlyUsedFiles();
				return;
			}
			if (this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPrefix)
			{
				directories_out.AddRange(this.GetMyComputerContent());
				return;
			}
			if (this.currentFolderFSEntry.FullName == MWFVFS.PersonalPrefix || this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPersonalPrefix)
			{
				ArrayList arrayList3 = null;
				ArrayList arrayList4 = null;
				this.GetNormalFolderContent(ThemeEngine.Current.Places(UIIcon.PlacesPersonal), filters, out arrayList3, out arrayList4);
				directories_out.AddRange(arrayList3);
				files_out.AddRange(arrayList4);
				return;
			}
			if (this.currentFolderFSEntry.FullName == MWFVFS.MyNetworkPrefix)
			{
				directories_out.AddRange(this.GetMyNetworkContent());
				return;
			}
			this.GetNormalFolderContent(this.currentFolderFSEntry.FullName, filters, out directories_out, out files_out);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000194C8 File Offset: 0x000176C8
		public ArrayList GetFoldersOnly()
		{
			ArrayList arrayList = new ArrayList();
			if (this.currentFolderFSEntry.FullName == MWFVFS.DesktopPrefix)
			{
				FSEntry personalFSEntry = this.GetPersonalFSEntry();
				arrayList.Add(personalFSEntry);
				FSEntry myComputerFSEntry = this.GetMyComputerFSEntry();
				arrayList.Add(myComputerFSEntry);
				FSEntry myNetworkFSEntry = this.GetMyNetworkFSEntry();
				arrayList.Add(myNetworkFSEntry);
				ArrayList normalFolders = this.GetNormalFolders(ThemeEngine.Current.Places(UIIcon.PlacesDesktop));
				arrayList.AddRange(normalFolders);
			}
			else if (!(this.currentFolderFSEntry.FullName == MWFVFS.RecentlyUsedPrefix))
			{
				if (this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPrefix)
				{
					arrayList.AddRange(this.GetMyComputerContent());
				}
				else if (this.currentFolderFSEntry.FullName == MWFVFS.PersonalPrefix || this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPersonalPrefix)
				{
					ArrayList normalFolders2 = this.GetNormalFolders(ThemeEngine.Current.Places(UIIcon.PlacesPersonal));
					arrayList.AddRange(normalFolders2);
				}
				else if (this.currentFolderFSEntry.FullName == MWFVFS.MyNetworkPrefix)
				{
					arrayList.AddRange(this.GetMyNetworkContent());
				}
				else
				{
					arrayList = this.GetNormalFolders(this.currentFolderFSEntry.FullName);
				}
			}
			return arrayList;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019604 File Offset: 0x00017804
		protected void GetNormalFolderContent(string from_folder, StringCollection filters, out ArrayList directories_out, out ArrayList files_out)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(from_folder);
			directories_out = new ArrayList();
			DirectoryInfo[] array = null;
			try
			{
				array = directoryInfo.GetDirectories();
			}
			catch (Exception)
			{
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					directories_out.Add(this.GetDirectoryFSEntry(array[i], this.currentTopFolderFSEntry));
				}
			}
			directories_out.Sort(this.fsEntryComparer);
			files_out = new ArrayList();
			ArrayList arrayList = new ArrayList();
			try
			{
				if (filters == null)
				{
					arrayList.AddRange(directoryInfo.GetFiles());
				}
				else
				{
					foreach (string text in filters)
					{
						arrayList.AddRange(directoryInfo.GetFiles(text));
					}
					arrayList.Sort(this.fileInfoComparer);
				}
			}
			catch (Exception)
			{
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				FSEntry fileFSEntry = this.GetFileFSEntry(arrayList[j] as FileInfo);
				if (fileFSEntry != null)
				{
					files_out.Add(fileFSEntry);
				}
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019734 File Offset: 0x00017934
		protected ArrayList GetNormalFolders(string from_folder)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(from_folder);
			ArrayList arrayList = new ArrayList();
			DirectoryInfo[] array = null;
			try
			{
				array = directoryInfo.GetDirectories();
			}
			catch (Exception)
			{
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(this.GetDirectoryFSEntry(array[i], this.currentTopFolderFSEntry));
				}
			}
			return arrayList;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019794 File Offset: 0x00017994
		protected virtual FSEntry GetDirectoryFSEntry(DirectoryInfo dirinfo, FSEntry topFolderFSEntry)
		{
			return new FSEntry
			{
				Attributes = dirinfo.Attributes,
				FullName = dirinfo.FullName,
				Name = dirinfo.Name,
				MainTopNode = topFolderFSEntry,
				FileType = FSEntry.FSEntryType.Directory,
				IconIndex = MimeIconEngine.GetIconIndexForMimeType("inode/directory"),
				LastAccessTime = dirinfo.LastAccessTime
			};
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000197F4 File Offset: 0x000179F4
		protected virtual FSEntry GetFileFSEntry(FileInfo fileinfo)
		{
			if ((fileinfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
			{
				return null;
			}
			return new FSEntry
			{
				Attributes = fileinfo.Attributes,
				FullName = fileinfo.FullName,
				Name = fileinfo.Name,
				FileType = FSEntry.FSEntryType.File,
				IconIndex = MimeIconEngine.GetIconIndexForFile(fileinfo.FullName),
				FileSize = fileinfo.Length,
				LastAccessTime = fileinfo.LastAccessTime
			};
		}

		// Token: 0x060005FE RID: 1534
		protected abstract FSEntry GetDesktopFSEntry();

		// Token: 0x060005FF RID: 1535
		protected abstract FSEntry GetRecentlyUsedFSEntry();

		// Token: 0x06000600 RID: 1536
		protected abstract FSEntry GetPersonalFSEntry();

		// Token: 0x06000601 RID: 1537
		protected abstract FSEntry GetMyComputerPersonalFSEntry();

		// Token: 0x06000602 RID: 1538
		protected abstract FSEntry GetMyComputerFSEntry();

		// Token: 0x06000603 RID: 1539
		protected abstract FSEntry GetMyNetworkFSEntry();

		// Token: 0x06000604 RID: 1540
		public abstract void WriteRecentlyUsedFiles(string fileToAdd);

		// Token: 0x06000605 RID: 1541
		public abstract ArrayList GetRecentlyUsedFiles();

		// Token: 0x06000606 RID: 1542
		public abstract ArrayList GetMyComputerContent();

		// Token: 0x06000607 RID: 1543
		public abstract ArrayList GetMyNetworkContent();

		// Token: 0x040003D9 RID: 985
		protected string currentTopFolder = string.Empty;

		// Token: 0x040003DA RID: 986
		protected FSEntry currentFolderFSEntry;

		// Token: 0x040003DB RID: 987
		protected FSEntry currentTopFolderFSEntry;

		// Token: 0x040003DC RID: 988
		private FileSystem.FileInfoComparer fileInfoComparer = new FileSystem.FileInfoComparer();

		// Token: 0x040003DD RID: 989
		private FileSystem.FSEntryComparer fsEntryComparer = new FileSystem.FSEntryComparer();

		// Token: 0x02000096 RID: 150
		internal class FileInfoComparer : IComparer
		{
			// Token: 0x06000609 RID: 1545 RVA: 0x00019892 File Offset: 0x00017A92
			public int Compare(object fileInfo1, object fileInfo2)
			{
				return string.Compare(((FileInfo)fileInfo1).Name, ((FileInfo)fileInfo2).Name);
			}
		}

		// Token: 0x02000097 RID: 151
		internal class FSEntryComparer : IComparer
		{
			// Token: 0x0600060B RID: 1547 RVA: 0x000198AF File Offset: 0x00017AAF
			public int Compare(object fileInfo1, object fileInfo2)
			{
				return string.Compare(((FSEntry)fileInfo1).Name, ((FSEntry)fileInfo2).Name);
			}
		}
	}
}
