using System;
using System.Collections;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000099 RID: 153
	internal class WinFileSystem : FileSystem
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0001A5DC File Offset: 0x000187DC
		public WinFileSystem()
		{
			this.desktopFSEntry = new FSEntry();
			this.desktopFSEntry.Attributes = FileAttributes.Directory;
			this.desktopFSEntry.FullName = MWFVFS.DesktopPrefix;
			this.desktopFSEntry.Name = Locale.GetText("Desktop");
			this.desktopFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
			this.desktopFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.desktopFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("desktop/desktop");
			this.desktopFSEntry.LastAccessTime = DateTime.Now;
			this.recentlyusedFSEntry = new FSEntry();
			this.recentlyusedFSEntry.Attributes = FileAttributes.Directory;
			this.recentlyusedFSEntry.FullName = MWFVFS.RecentlyUsedPrefix;
			this.recentlyusedFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesRecentDocuments);
			this.recentlyusedFSEntry.Name = Locale.GetText("Recently Used");
			this.recentlyusedFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.recentlyusedFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("recently/recently");
			this.recentlyusedFSEntry.LastAccessTime = DateTime.Now;
			this.personalFSEntry = new FSEntry();
			this.personalFSEntry.Attributes = FileAttributes.Directory;
			this.personalFSEntry.FullName = MWFVFS.PersonalPrefix;
			this.personalFSEntry.Name = Locale.GetText("Personal");
			this.personalFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.personalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.personalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.personalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.personalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerpersonalFSEntry = new FSEntry();
			this.mycomputerpersonalFSEntry.Attributes = FileAttributes.Directory;
			this.mycomputerpersonalFSEntry.FullName = MWFVFS.MyComputerPersonalPrefix;
			this.mycomputerpersonalFSEntry.Name = Locale.GetText("Personal");
			this.mycomputerpersonalFSEntry.MainTopNode = this.GetMyComputerFSEntry();
			this.mycomputerpersonalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.mycomputerpersonalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerpersonalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.mycomputerpersonalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerFSEntry = new FSEntry();
			this.mycomputerFSEntry.Attributes = FileAttributes.Directory;
			this.mycomputerFSEntry.FullName = MWFVFS.MyComputerPrefix;
			this.mycomputerFSEntry.Name = Locale.GetText("My Computer");
			this.mycomputerFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mycomputerFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("workplace/workplace");
			this.mycomputerFSEntry.LastAccessTime = DateTime.Now;
			this.mynetworkFSEntry = new FSEntry();
			this.mynetworkFSEntry.Attributes = FileAttributes.Directory;
			this.mynetworkFSEntry.FullName = MWFVFS.MyNetworkPrefix;
			this.mynetworkFSEntry.Name = Locale.GetText("My Network");
			this.mynetworkFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mynetworkFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mynetworkFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
			this.mynetworkFSEntry.LastAccessTime = DateTime.Now;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000493C File Offset: 0x00002B3C
		public override void WriteRecentlyUsedFiles(string fileToAdd)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A920 File Offset: 0x00018B20
		public override ArrayList GetRecentlyUsedFiles()
		{
			ArrayList arrayList = new ArrayList();
			foreach (FileInfo fileInfo in new DirectoryInfo(this.recentlyusedFSEntry.RealName).GetFiles())
			{
				FSEntry fileFSEntry = this.GetFileFSEntry(fileInfo);
				if (fileFSEntry != null)
				{
					arrayList.Add(fileFSEntry);
				}
			}
			return arrayList;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001A974 File Offset: 0x00018B74
		public override ArrayList GetMyComputerContent()
		{
			string[] logicalDrives = Directory.GetLogicalDrives();
			ArrayList arrayList = new ArrayList();
			foreach (string text in logicalDrives)
			{
				FSEntry fsentry = new FSEntry();
				fsentry.FileType = FSEntry.FSEntryType.Device;
				fsentry.FullName = text;
				fsentry.Name = text;
				fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("harddisk/harddisk");
				fsentry.Attributes = FileAttributes.Directory;
				fsentry.MainTopNode = this.GetMyComputerFSEntry();
				arrayList.Add(fsentry);
				string text2 = fsentry.FullName + "://";
				if (!MWFVFS.MyComputerDevicesPrefix.Contains(text2))
				{
					MWFVFS.MyComputerDevicesPrefix.Add(text2, fsentry);
				}
			}
			arrayList.Add(this.GetMyComputerPersonalFSEntry());
			return arrayList;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001AA31 File Offset: 0x00018C31
		public override ArrayList GetMyNetworkContent()
		{
			return new ArrayList();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001AA38 File Offset: 0x00018C38
		protected override FSEntry GetDesktopFSEntry()
		{
			return this.desktopFSEntry;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001AA40 File Offset: 0x00018C40
		protected override FSEntry GetRecentlyUsedFSEntry()
		{
			return this.recentlyusedFSEntry;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001AA48 File Offset: 0x00018C48
		protected override FSEntry GetPersonalFSEntry()
		{
			return this.personalFSEntry;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001AA50 File Offset: 0x00018C50
		protected override FSEntry GetMyComputerPersonalFSEntry()
		{
			return this.mycomputerpersonalFSEntry;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001AA58 File Offset: 0x00018C58
		protected override FSEntry GetMyComputerFSEntry()
		{
			return this.mycomputerFSEntry;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001AA60 File Offset: 0x00018C60
		protected override FSEntry GetMyNetworkFSEntry()
		{
			return this.mynetworkFSEntry;
		}

		// Token: 0x040003E8 RID: 1000
		private FSEntry desktopFSEntry;

		// Token: 0x040003E9 RID: 1001
		private FSEntry recentlyusedFSEntry;

		// Token: 0x040003EA RID: 1002
		private FSEntry personalFSEntry;

		// Token: 0x040003EB RID: 1003
		private FSEntry mycomputerpersonalFSEntry;

		// Token: 0x040003EC RID: 1004
		private FSEntry mycomputerFSEntry;

		// Token: 0x040003ED RID: 1005
		private FSEntry mynetworkFSEntry;
	}
}
