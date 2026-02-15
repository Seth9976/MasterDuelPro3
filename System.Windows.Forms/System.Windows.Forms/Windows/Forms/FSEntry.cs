using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200009A RID: 154
	internal class FSEntry : IDisposable
	{
		// Token: 0x17000173 RID: 371
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0001AA68 File Offset: 0x00018C68
		public MasterMount.FsTypes FsType
		{
			set
			{
				this.fsType = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001AA71 File Offset: 0x00018C71
		public string DeviceShort
		{
			set
			{
				this.device_short = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001AA83 File Offset: 0x00018C83
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001AA7A File Offset: 0x00018C7A
		public string FullName
		{
			get
			{
				return this.fullName;
			}
			set
			{
				this.fullName = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001AA94 File Offset: 0x00018C94
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0001AA8B File Offset: 0x00018C8B
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001AAA5 File Offset: 0x00018CA5
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001AA9C File Offset: 0x00018C9C
		public string RealName
		{
			get
			{
				return this.realName;
			}
			set
			{
				this.realName = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001AAB6 File Offset: 0x00018CB6
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001AAAD File Offset: 0x00018CAD
		public FileAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001AAC7 File Offset: 0x00018CC7
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001AABE File Offset: 0x00018CBE
		public long FileSize
		{
			get
			{
				return this.fileSize;
			}
			set
			{
				this.fileSize = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001AACF File Offset: 0x00018CCF
		public FSEntry.FSEntryType FileType
		{
			get
			{
				return this.fileType;
			}
			set
			{
				this.fileType = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001AAE9 File Offset: 0x00018CE9
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public DateTime LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
			set
			{
				this.lastAccessTime = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001AAFA File Offset: 0x00018CFA
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001AAF1 File Offset: 0x00018CF1
		public int IconIndex
		{
			get
			{
				return this.iconIndex;
			}
			set
			{
				this.iconIndex = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001AB0B File Offset: 0x00018D0B
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001AB02 File Offset: 0x00018D02
		public FSEntry MainTopNode
		{
			get
			{
				return this.mainTopNode;
			}
			set
			{
				this.mainTopNode = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001AB13 File Offset: 0x00018D13
		public string Parent
		{
			get
			{
				this.parent = this.GetParent();
				return this.parent;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001AB28 File Offset: 0x00018D28
		private string GetParent()
		{
			if (this.fullName == MWFVFS.PersonalPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.MyComputerPersonalPrefix)
			{
				return MWFVFS.MyComputerPrefix;
			}
			if (this.fullName == MWFVFS.MyComputerPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.MyNetworkPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.DesktopPrefix)
			{
				return null;
			}
			if (this.fullName == MWFVFS.RecentlyUsedPrefix)
			{
				return null;
			}
			foreach (object obj in MWFVFS.MyComputerDevicesPrefix)
			{
				FSEntry fsentry = ((DictionaryEntry)obj).Value as FSEntry;
				if (this.fullName == fsentry.FullName)
				{
					return fsentry.MainTopNode.FullName;
				}
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(this.fullName).Parent;
			if (directoryInfo == null)
			{
				return null;
			}
			FSEntry fsentry2 = MWFVFS.MyComputerDevicesPrefix[directoryInfo.FullName + "://"] as FSEntry;
			if (fsentry2 != null)
			{
				return fsentry2.FullName;
			}
			if (this.mainTopNode != null)
			{
				if (directoryInfo.FullName == ThemeEngine.Current.Places(UIIcon.PlacesDesktop) && this.mainTopNode.FullName == MWFVFS.DesktopPrefix)
				{
					return this.mainTopNode.FullName;
				}
				if (directoryInfo.FullName == ThemeEngine.Current.Places(UIIcon.PlacesPersonal) && this.mainTopNode.FullName == MWFVFS.PersonalPrefix)
				{
					return this.mainTopNode.FullName;
				}
				if (directoryInfo.FullName == ThemeEngine.Current.Places(UIIcon.PlacesPersonal) && this.mainTopNode.FullName == MWFVFS.MyComputerPersonalPrefix)
				{
					return this.mainTopNode.FullName;
				}
			}
			return directoryInfo.FullName;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001AD44 File Offset: 0x00018F44
		internal bool IsImageFile()
		{
			string extension = Path.GetExtension(this.FullName);
			if (string.IsNullOrEmpty(extension))
			{
				return false;
			}
			string text = extension.ToLowerInvariant();
			return text == ".bmp" || text == ".gif" || text == ".jpg" || text == ".jpeg" || text == ".png" || text == ".tif" || text == ".tiff";
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001ADC9 File Offset: 0x00018FC9
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001ADD1 File Offset: 0x00018FD1
		internal Image Image { get; set; }

		// Token: 0x0600063C RID: 1596 RVA: 0x0001ADDC File Offset: 0x00018FDC
		internal void SetImage()
		{
			try
			{
				Image.GetThumbnailImageAbort getThumbnailImageAbort = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
				using (Bitmap bitmap = new Bitmap(this.FullName))
				{
					this.Image = bitmap.GetThumbnailImage(48, 48, getThumbnailImageAbort, IntPtr.Zero);
					this.fMustDisposeImage = true;
				}
			}
			catch (Exception)
			{
				this.Image = null;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00002D70 File Offset: 0x00000F70
		private bool ThumbnailCallback()
		{
			return false;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001AE54 File Offset: 0x00019054
		public void Dispose()
		{
			if (this.Image != null && this.fMustDisposeImage)
			{
				this.Image.Dispose();
				this.Image = null;
			}
		}

		// Token: 0x040003EE RID: 1006
		private MasterMount.FsTypes fsType;

		// Token: 0x040003EF RID: 1007
		private string device_short;

		// Token: 0x040003F0 RID: 1008
		private string fullName;

		// Token: 0x040003F1 RID: 1009
		private string name;

		// Token: 0x040003F2 RID: 1010
		private string realName;

		// Token: 0x040003F3 RID: 1011
		private FileAttributes attributes = FileAttributes.Normal;

		// Token: 0x040003F4 RID: 1012
		private long fileSize;

		// Token: 0x040003F5 RID: 1013
		private FSEntry.FSEntryType fileType;

		// Token: 0x040003F6 RID: 1014
		private DateTime lastAccessTime;

		// Token: 0x040003F7 RID: 1015
		private FSEntry mainTopNode;

		// Token: 0x040003F8 RID: 1016
		private int iconIndex;

		// Token: 0x040003F9 RID: 1017
		private string parent;

		// Token: 0x040003FB RID: 1019
		private bool fMustDisposeImage;

		// Token: 0x0200009B RID: 155
		public enum FSEntryType
		{
			// Token: 0x040003FD RID: 1021
			Desktop,
			// Token: 0x040003FE RID: 1022
			RecentlyUsed,
			// Token: 0x040003FF RID: 1023
			MyComputer,
			// Token: 0x04000400 RID: 1024
			File,
			// Token: 0x04000401 RID: 1025
			Directory,
			// Token: 0x04000402 RID: 1026
			Device,
			// Token: 0x04000403 RID: 1027
			RemovableDevice,
			// Token: 0x04000404 RID: 1028
			Network
		}
	}
}
