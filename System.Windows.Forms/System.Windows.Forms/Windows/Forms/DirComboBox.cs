using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000087 RID: 135
	internal class DirComboBox : ComboBox, IUpdateFolder
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x000165F4 File Offset: 0x000147F4
		public DirComboBox(MWFVFS vfs)
		{
			this.vfs = vfs;
			base.SuspendLayout();
			base.DrawMode = DrawMode.OwnerDrawFixed;
			this.imageList.ColorDepth = ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new Size(16, 16);
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.NormalFolder, 16));
			this.imageList.TransparentColor = Color.Transparent;
			this.recentlyUsedDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 0, Locale.GetText("Recently used"), MWFVFS.RecentlyUsedPrefix, 0);
			this.desktopDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 1, Locale.GetText("Desktop"), MWFVFS.DesktopPrefix, 0);
			this.personalDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 2, Locale.GetText("Personal folder"), MWFVFS.PersonalPrefix, DirComboBox.indent);
			this.myComputerDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 3, Locale.GetText("My Computer"), MWFVFS.MyComputerPrefix, DirComboBox.indent);
			this.networkDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 4, Locale.GetText("My Network"), MWFVFS.MyNetworkPrefix, DirComboBox.indent);
			ArrayList arrayList = this.vfs.GetMyComputerContent();
			foreach (object obj in arrayList)
			{
				FSEntry fsentry = (FSEntry)obj;
				this.myComputerItems.Add(new DirComboBox.DirComboBoxItem(MimeIconEngine.LargeIcons, fsentry.IconIndex, fsentry.Name, fsentry.FullName, DirComboBox.indent * 2));
			}
			arrayList.Clear();
			arrayList = null;
			this.mainParentDirComboBoxItem = this.myComputerDirComboboxItem;
			base.ResumeLayout(false);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00016877 File Offset: 0x00014A77
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00016868 File Offset: 0x00014A68
		public string CurrentFolder
		{
			get
			{
				return this.currentPath;
			}
			set
			{
				this.currentPath = value;
				this.CreateComboList();
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00016880 File Offset: 0x00014A80
		private void CreateComboList()
		{
			this.real_parent = null;
			DirComboBox.DirComboBoxItem dirComboBoxItem = null;
			if (this.currentPath == MWFVFS.RecentlyUsedPrefix)
			{
				this.mainParentDirComboBoxItem = this.recentlyUsedDirComboboxItem;
				dirComboBoxItem = this.recentlyUsedDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.DesktopPrefix)
			{
				dirComboBoxItem = this.desktopDirComboboxItem;
				this.mainParentDirComboBoxItem = this.desktopDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.PersonalPrefix)
			{
				dirComboBoxItem = this.personalDirComboboxItem;
				this.mainParentDirComboBoxItem = this.personalDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.MyComputerPrefix)
			{
				dirComboBoxItem = this.myComputerDirComboboxItem;
				this.mainParentDirComboBoxItem = this.myComputerDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.MyNetworkPrefix)
			{
				dirComboBoxItem = this.networkDirComboboxItem;
				this.mainParentDirComboBoxItem = this.networkDirComboboxItem;
			}
			else
			{
				foreach (object obj in this.myComputerItems)
				{
					DirComboBox.DirComboBoxItem dirComboBoxItem2 = (DirComboBox.DirComboBoxItem)obj;
					if (dirComboBoxItem2.Path == this.currentPath)
					{
						dirComboBoxItem = (this.mainParentDirComboBoxItem = dirComboBoxItem2);
						break;
					}
				}
			}
			base.BeginUpdate();
			base.Items.Clear();
			base.Items.Add(this.recentlyUsedDirComboboxItem);
			base.Items.Add(this.desktopDirComboboxItem);
			base.Items.Add(this.personalDirComboboxItem);
			base.Items.Add(this.myComputerDirComboboxItem);
			base.Items.AddRange(this.myComputerItems);
			base.Items.Add(this.networkDirComboboxItem);
			if (dirComboBoxItem == null)
			{
				this.real_parent = this.CreateFolderStack();
			}
			if (this.real_parent != null)
			{
				int num;
				if (this.real_parent == this.desktopDirComboboxItem)
				{
					num = 1;
				}
				else if (this.real_parent == this.personalDirComboboxItem || this.real_parent == this.networkDirComboboxItem)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
				dirComboBoxItem = this.AppendToParent(num, this.real_parent);
			}
			base.EndUpdate();
			if (dirComboBoxItem != null)
			{
				base.SelectedItem = dirComboBoxItem;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016AB4 File Offset: 0x00014CB4
		private DirComboBox.DirComboBoxItem CreateFolderStack()
		{
			this.folderStack.Clear();
			DirectoryInfo directoryInfo = new DirectoryInfo(this.currentPath);
			this.folderStack.Push(directoryInfo);
			bool flag = !XplatUI.RunningOnUnix;
			while (directoryInfo.Parent != null)
			{
				directoryInfo = directoryInfo.Parent;
				if (this.mainParentDirComboBoxItem != this.personalDirComboboxItem && string.Compare(directoryInfo.FullName, ThemeEngine.Current.Places(UIIcon.PlacesDesktop), flag) == 0)
				{
					return this.desktopDirComboboxItem;
				}
				if (this.mainParentDirComboBoxItem == this.personalDirComboboxItem)
				{
					if (string.Compare(directoryInfo.FullName, ThemeEngine.Current.Places(UIIcon.PlacesPersonal), flag) == 0)
					{
						return this.personalDirComboboxItem;
					}
				}
				else
				{
					foreach (object obj in this.myComputerItems)
					{
						DirComboBox.DirComboBoxItem dirComboBoxItem = (DirComboBox.DirComboBoxItem)obj;
						if (string.Compare(dirComboBoxItem.Path, directoryInfo.FullName, flag) == 0)
						{
							return dirComboBoxItem;
						}
					}
				}
				this.folderStack.Push(directoryInfo);
			}
			return null;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016BD4 File Offset: 0x00014DD4
		private DirComboBox.DirComboBoxItem AppendToParent(int nr_indents, DirComboBox.DirComboBoxItem parentDirComboBoxItem)
		{
			DirComboBox.DirComboBoxItem dirComboBoxItem = null;
			int num = base.Items.IndexOf(parentDirComboBoxItem) + 1;
			int num2 = DirComboBox.indent * nr_indents;
			while (this.folderStack.Count != 0)
			{
				DirectoryInfo directoryInfo = this.folderStack.Pop() as DirectoryInfo;
				DirComboBox.DirComboBoxItem dirComboBoxItem2 = new DirComboBox.DirComboBoxItem(this.imageList, 5, directoryInfo.Name, directoryInfo.FullName, num2);
				base.Items.Insert(num, dirComboBoxItem2);
				num++;
				dirComboBoxItem = dirComboBoxItem2;
				num2 += DirComboBox.indent;
			}
			return dirComboBoxItem;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00016C54 File Offset: 0x00014E54
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			DirComboBox.DirComboBoxItem dirComboBoxItem = base.Items[e.Index] as DirComboBox.DirComboBoxItem;
			Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height, e.Graphics);
			Graphics graphics = Graphics.FromImage(bitmap);
			Color backColor = e.BackColor;
			Color color = e.ForeColor;
			int num = dirComboBoxItem.XPos;
			if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.None)
			{
				num = 0;
			}
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(backColor), new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && (!base.DroppedDown || (e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit))
			{
				color = ThemeEngine.Current.ColorHighlightText;
				int num2 = (int)graphics.MeasureString(dirComboBoxItem.Name, e.Font).Width;
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), new Rectangle(num + 23, 1, num2 + 3, e.Bounds.Height - 2));
				if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
				{
					ControlPaint.DrawFocusRectangle(graphics, new Rectangle(num + 22, 0, num2 + 5, e.Bounds.Height), color, ThemeEngine.Current.ColorHighlight);
				}
			}
			graphics.DrawString(dirComboBoxItem.Name, e.Font, ThemeEngine.Current.ResPool.GetSolidBrush(color), new Point(24 + num, (bitmap.Height - e.Font.Height) / 2));
			graphics.DrawImage(dirComboBoxItem.ImageList.Images[dirComboBoxItem.ImageIndex], new Rectangle(new Point(num + 2, 0), new Size(16, 16)));
			e.Graphics.DrawImage(bitmap, e.Bounds.X, e.Bounds.Y);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00016E88 File Offset: 0x00015088
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.Items.Count > 0)
			{
				DirComboBox.DirComboBoxItem dirComboBoxItem = base.Items[this.SelectedIndex] as DirComboBox.DirComboBoxItem;
				this.currentPath = dirComboBoxItem.Path;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016EC8 File Offset: 0x000150C8
		protected override void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DirComboBox.CDirectoryChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600058E RID: 1422 RVA: 0x00016EFA File Offset: 0x000150FA
		// (remove) Token: 0x0600058F RID: 1423 RVA: 0x00016F0D File Offset: 0x0001510D
		public event EventHandler DirectoryChanged
		{
			add
			{
				base.Events.AddHandler(DirComboBox.CDirectoryChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DirComboBox.CDirectoryChangedEvent, value);
			}
		}

		// Token: 0x0400037C RID: 892
		private ImageList imageList = new ImageList();

		// Token: 0x0400037D RID: 893
		private string currentPath;

		// Token: 0x0400037E RID: 894
		private Stack folderStack = new Stack();

		// Token: 0x0400037F RID: 895
		private static readonly int indent = 6;

		// Token: 0x04000380 RID: 896
		private DirComboBox.DirComboBoxItem recentlyUsedDirComboboxItem;

		// Token: 0x04000381 RID: 897
		private DirComboBox.DirComboBoxItem desktopDirComboboxItem;

		// Token: 0x04000382 RID: 898
		private DirComboBox.DirComboBoxItem personalDirComboboxItem;

		// Token: 0x04000383 RID: 899
		private DirComboBox.DirComboBoxItem myComputerDirComboboxItem;

		// Token: 0x04000384 RID: 900
		private DirComboBox.DirComboBoxItem networkDirComboboxItem;

		// Token: 0x04000385 RID: 901
		private ArrayList myComputerItems = new ArrayList();

		// Token: 0x04000386 RID: 902
		private DirComboBox.DirComboBoxItem mainParentDirComboBoxItem;

		// Token: 0x04000387 RID: 903
		private DirComboBox.DirComboBoxItem real_parent;

		// Token: 0x04000388 RID: 904
		private MWFVFS vfs;

		// Token: 0x04000389 RID: 905
		private static object CDirectoryChangedEvent = new object();

		// Token: 0x02000088 RID: 136
		internal class DirComboBoxItem
		{
			// Token: 0x06000591 RID: 1425 RVA: 0x00016F32 File Offset: 0x00015132
			public DirComboBoxItem(ImageList imageList, int imageIndex, string name, string path, int xPos)
			{
				this.imageList = imageList;
				this.imageIndex = imageIndex;
				this.name = name;
				this.path = path;
				this.xPos = xPos;
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000592 RID: 1426 RVA: 0x00016F5F File Offset: 0x0001515F
			public int ImageIndex
			{
				get
				{
					return this.imageIndex;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000593 RID: 1427 RVA: 0x00016F67 File Offset: 0x00015167
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000594 RID: 1428 RVA: 0x00016F6F File Offset: 0x0001516F
			public string Path
			{
				get
				{
					return this.path;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000595 RID: 1429 RVA: 0x00016F77 File Offset: 0x00015177
			public int XPos
			{
				get
				{
					return this.xPos;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x00016F7F File Offset: 0x0001517F
			public ImageList ImageList
			{
				get
				{
					return this.imageList;
				}
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00016F67 File Offset: 0x00015167
			public override string ToString()
			{
				return this.name;
			}

			// Token: 0x0400038A RID: 906
			private int imageIndex;

			// Token: 0x0400038B RID: 907
			private string name;

			// Token: 0x0400038C RID: 908
			private string path;

			// Token: 0x0400038D RID: 909
			private int xPos;

			// Token: 0x0400038E RID: 910
			private ImageList imageList;
		}
	}
}
