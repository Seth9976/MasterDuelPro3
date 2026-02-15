using System;

namespace System.Windows.Forms
{
	// Token: 0x0200008E RID: 142
	internal class FileViewListViewItem : ListViewItem
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00018758 File Offset: 0x00016958
		public FileViewListViewItem(FSEntry fsEntry)
		{
			this.fsEntry = fsEntry;
			base.ImageIndex = fsEntry.IconIndex;
			base.Text = fsEntry.Name;
			switch (fsEntry.FileType)
			{
			case FSEntry.FSEntryType.File:
			{
				long num = 1L;
				try
				{
					if (fsEntry.FileSize > 1024L)
					{
						num = fsEntry.FileSize / 1024L;
					}
				}
				catch (Exception)
				{
					num = 1L;
				}
				base.SubItems.Add(num.ToString() + " KB");
				base.SubItems.Add(Locale.GetText("File"));
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				return;
			}
			case FSEntry.FSEntryType.Directory:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add(Locale.GetText("Directory"));
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				return;
			case FSEntry.FSEntryType.Device:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add(Locale.GetText("Device"));
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				return;
			case FSEntry.FSEntryType.RemovableDevice:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add(Locale.GetText("RemovableDevice"));
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				return;
			default:
				return;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001895D File Offset: 0x00016B5D
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00018954 File Offset: 0x00016B54
		public FSEntry FSEntry
		{
			get
			{
				return this.fsEntry;
			}
			set
			{
				this.fsEntry = value;
			}
		}

		// Token: 0x040003BC RID: 956
		private FSEntry fsEntry;
	}
}
