using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200008B RID: 139
	internal class MWFFileView : ListView
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x000170B4 File Offset: 0x000152B4
		public MWFFileView(MWFVFS vfs)
		{
			this.vfs = vfs;
			this.vfs.RegisterUpdateDelegate(new MWFVFS.UpdateDelegate(this.RealFileViewUpdate), this);
			base.SuspendLayout();
			this.contextMenu = new ContextMenu();
			this.toolTip = new ToolTip();
			this.toolTip.InitialDelay = 300;
			this.toolTip.ReshowDelay = 0;
			this.menuItemView = new MenuItem(Locale.GetText("View"));
			this.smallIconMenutItem = new MenuItem(Locale.GetText("Small Icon"), new EventHandler(this.OnClickViewMenuSubItem));
			this.smallIconMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.smallIconMenutItem);
			this.tilesMenutItem = new MenuItem(Locale.GetText("Tiles"), new EventHandler(this.OnClickViewMenuSubItem));
			this.tilesMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.tilesMenutItem);
			this.largeIconMenutItem = new MenuItem(Locale.GetText("Large Icon"), new EventHandler(this.OnClickViewMenuSubItem));
			this.largeIconMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.largeIconMenutItem);
			this.listMenutItem = new MenuItem(Locale.GetText("List"), new EventHandler(this.OnClickViewMenuSubItem));
			this.listMenutItem.RadioCheck = true;
			this.listMenutItem.Checked = true;
			this.menuItemView.MenuItems.Add(this.listMenutItem);
			this.previousCheckedMenuItemIndex = this.listMenutItem.Index;
			this.detailsMenutItem = new MenuItem(Locale.GetText("Details"), new EventHandler(this.OnClickViewMenuSubItem));
			this.detailsMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.detailsMenutItem);
			this.contextMenu.MenuItems.Add(this.menuItemView);
			this.contextMenu.MenuItems.Add(new MenuItem("-"));
			this.menuItemNew = new MenuItem(Locale.GetText("New"));
			this.newFolderMenuItem = new MenuItem(Locale.GetText("New Folder"), new EventHandler(this.OnClickNewFolderMenuItem));
			this.menuItemNew.MenuItems.Add(this.newFolderMenuItem);
			this.contextMenu.MenuItems.Add(this.menuItemNew);
			this.contextMenu.MenuItems.Add(new MenuItem("-"));
			this.showHiddenFilesMenuItem = new MenuItem(Locale.GetText("Show hidden files"), new EventHandler(this.OnClickContextMenu));
			this.showHiddenFilesMenuItem.Checked = this.showHiddenFiles;
			this.contextMenu.MenuItems.Add(this.showHiddenFilesMenuItem);
			base.LabelWrap = true;
			base.SmallImageList = MimeIconEngine.SmallIcons;
			base.LargeImageList = MimeIconEngine.LargeIcons;
			base.View = (this.old_view = View.List);
			base.LabelEdit = true;
			this.ContextMenu = this.contextMenu;
			this.columns = new ColumnHeader[4];
			this.columns[0] = this.CreateColumnHeader(Locale.GetText(" Name"), 170, HorizontalAlignment.Left);
			this.columns[1] = this.CreateColumnHeader(Locale.GetText("Size "), 80, HorizontalAlignment.Right);
			this.columns[2] = this.CreateColumnHeader(Locale.GetText(" Type"), 100, HorizontalAlignment.Left);
			this.columns[3] = this.CreateColumnHeader(Locale.GetText(" Last Access"), 150, HorizontalAlignment.Left);
			base.AllowColumnReorder = true;
			base.ResumeLayout(false);
			base.KeyDown += this.MWF_KeyDown;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000174BD File Offset: 0x000156BD
		private ColumnHeader CreateColumnHeader(string text, int width, HorizontalAlignment alignment)
		{
			return new ColumnHeader
			{
				Text = text,
				Width = width,
				TextAlign = alignment
			};
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000174D9 File Offset: 0x000156D9
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x000174E1 File Offset: 0x000156E1
		public string CurrentFolder
		{
			get
			{
				return this.currentFolder;
			}
			set
			{
				this.currentFolder = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000174EA File Offset: 0x000156EA
		public string CurrentRealFolder
		{
			get
			{
				return this.currentRealFolder;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000174F2 File Offset: 0x000156F2
		public FSEntry CurrentFSEntry
		{
			get
			{
				return this.currentFSEntry;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000174FC File Offset: 0x000156FC
		public MenuItem[] ViewMenuItems
		{
			get
			{
				MenuItem[] array = new MenuItem[]
				{
					this.smallIconMenutItem.CloneMenu(),
					this.tilesMenutItem.CloneMenu(),
					this.largeIconMenutItem.CloneMenu(),
					this.listMenutItem.CloneMenu(),
					this.detailsMenutItem.CloneMenu()
				};
				this.viewMenuItemClones.Add(array);
				return array;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001756D File Offset: 0x0001576D
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00017564 File Offset: 0x00015764
		public ArrayList FilterArrayList
		{
			get
			{
				return this.filterArrayList;
			}
			set
			{
				this.filterArrayList = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00017575 File Offset: 0x00015775
		public bool ShowHiddenFiles
		{
			get
			{
				return this.showHiddenFiles;
			}
		}

		// Token: 0x1700016B RID: 363
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001757D File Offset: 0x0001577D
		public int FilterIndex
		{
			set
			{
				this.filterIndex = value;
				if (base.Visible)
				{
					this.UpdateFileView();
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00017594 File Offset: 0x00015794
		public string SelectedFilesString
		{
			get
			{
				return this.selectedFilesString;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001759C File Offset: 0x0001579C
		public void PushDir()
		{
			if (this.currentFolder != null)
			{
				this.directoryStack.Push(this.currentFolder);
			}
			this.EnableOrDisableDirstackObjects();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000175BD File Offset: 0x000157BD
		public void PopDir()
		{
			this.PopDir(null);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000175C8 File Offset: 0x000157C8
		public void PopDir(string filter)
		{
			if (this.directoryStack.Count == 0)
			{
				return;
			}
			string text = this.directoryStack.Pop() as string;
			this.EnableOrDisableDirstackObjects();
			this.should_push = false;
			this.ChangeDirectory(null, text, filter);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001760A File Offset: 0x0001580A
		public void RegisterSender(IUpdateFolder iud)
		{
			this.registered_senders.Add(iud);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001761C File Offset: 0x0001581C
		public void CreateNewFolder()
		{
			if (this.currentFolder == MWFVFS.MyComputerPrefix || this.currentFolder == MWFVFS.RecentlyUsedPrefix)
			{
				return;
			}
			FSEntry fsentry = new FSEntry();
			fsentry.Attributes = FileAttributes.Directory;
			fsentry.FileType = FSEntry.FSEntryType.Directory;
			fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("inode/directory");
			fsentry.LastAccessTime = DateTime.Now;
			TextEntryDialog textEntryDialog = new TextEntryDialog();
			textEntryDialog.IconPictureBoxImage = MimeIconEngine.LargeIcons.Images.GetImage(fsentry.IconIndex);
			string text = string.Empty;
			if (this.currentFolderFSEntry.RealName != null)
			{
				text = this.currentFolderFSEntry.RealName;
			}
			else
			{
				text = this.currentFolder;
			}
			string text2 = Locale.GetText("New Folder");
			if (Directory.Exists(Path.Combine(text, text2)))
			{
				int num = 1;
				if (XplatUI.RunningOnUnix)
				{
					text2 = text2 + "-" + num;
				}
				else
				{
					text2 = string.Concat(new object[] { text2, " (", num, ")" });
				}
				while (Directory.Exists(Path.Combine(text, text2)))
				{
					num++;
					if (XplatUI.RunningOnUnix)
					{
						text2 = Locale.GetText("New Folder") + "-" + num;
					}
					else
					{
						text2 = string.Concat(new object[]
						{
							Locale.GetText("New Folder"),
							" (",
							num,
							")"
						});
					}
				}
			}
			textEntryDialog.FileName = text2;
			if (textEntryDialog.ShowDialog() == DialogResult.OK)
			{
				string text3 = Path.Combine(text, textEntryDialog.FileName);
				if (this.vfs.CreateFolder(text3))
				{
					fsentry.FullName = text3;
					fsentry.Name = textEntryDialog.FileName;
					FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsentry);
					base.BeginUpdate();
					base.Items.Add(fileViewListViewItem);
					base.EndUpdate();
					fileViewListViewItem.EnsureVisible();
				}
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00017807 File Offset: 0x00015A07
		public void OneDirUp()
		{
			this.OneDirUp(null);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017810 File Offset: 0x00015A10
		public void OneDirUp(string filter)
		{
			string parent = this.vfs.GetParent();
			if (parent != null)
			{
				this.ChangeDirectory(null, parent, filter);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017835 File Offset: 0x00015A35
		public void ChangeDirectory(object sender, string folder)
		{
			this.ChangeDirectory(sender, folder, null);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017840 File Offset: 0x00015A40
		public void ChangeDirectory(object sender, string folder, string filter)
		{
			if (folder == MWFVFS.DesktopPrefix || folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.folderUpToolBarButton.Enabled = false;
			}
			else
			{
				this.folderUpToolBarButton.Enabled = true;
			}
			foreach (object obj in this.registered_senders)
			{
				((IUpdateFolder)obj).CurrentFolder = folder;
			}
			if (this.should_push)
			{
				this.PushDir();
			}
			else
			{
				this.should_push = true;
			}
			this.currentFolderFSEntry = this.vfs.ChangeDirectory(folder);
			this.currentFolder = folder;
			if (this.currentFolder.IndexOf("://") != -1)
			{
				this.currentRealFolder = this.currentFolderFSEntry.RealName;
			}
			else
			{
				this.currentRealFolder = this.currentFolder;
			}
			base.BeginUpdate();
			base.Items.Clear();
			base.SelectedItems.Clear();
			if (folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.old_view = base.View;
				base.View = View.Details;
				this.old_menuitem_index = this.previousCheckedMenuItemIndex;
				this.UpdateMenuItems(this.detailsMenutItem);
				this.do_update_view = true;
			}
			else if (base.View != this.old_view && this.do_update_view)
			{
				this.UpdateMenuItems(this.menuItemView.MenuItems[this.old_menuitem_index]);
				base.View = this.old_view;
				this.do_update_view = false;
			}
			base.EndUpdate();
			try
			{
				this.UpdateFileView(filter);
			}
			catch (Exception ex)
			{
				if (this.should_push)
				{
					this.PopDir();
				}
				MessageBox.Show(ex.Message, Locale.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017A14 File Offset: 0x00015C14
		public void UpdateFileView()
		{
			this.UpdateFileView(null);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017A1D File Offset: 0x00015C1D
		internal void StopThumbnailCreation()
		{
			if (this.thumbCreator != null)
			{
				this.thumbCreator.Stop();
				this.thumbCreator = null;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017A3C File Offset: 0x00015C3C
		public void UpdateFileView(string custom_filter)
		{
			this.StopThumbnailCreation();
			if (custom_filter != null)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.Add(custom_filter);
				this.vfs.GetFolderContent(stringCollection);
				return;
			}
			if (this.filterArrayList != null && this.filterArrayList.Count != 0)
			{
				FilterStruct filterStruct = (FilterStruct)this.filterArrayList[this.filterIndex - 1];
				this.vfs.GetFolderContent(filterStruct.filters);
				return;
			}
			this.vfs.GetFolderContent();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017AB8 File Offset: 0x00015CB8
		public void RealFileViewUpdate(ArrayList directoriesArrayList, ArrayList fileArrayList)
		{
			base.BeginUpdate();
			this.DeleteOldThumbnails();
			base.Items.Clear();
			base.SelectedItems.Clear();
			foreach (object obj in directoriesArrayList)
			{
				FSEntry fsentry = (FSEntry)obj;
				if (this.ShowHiddenFiles || (!fsentry.Name.StartsWith(".") && (fsentry.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
				{
					FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsentry);
					base.Items.Add(fileViewListViewItem);
				}
			}
			StringCollection stringCollection = new StringCollection();
			foreach (object obj2 in fileArrayList)
			{
				FSEntry fsentry2 = (FSEntry)obj2;
				if (stringCollection.Contains(fsentry2.Name))
				{
					string text = fsentry2.Name;
					if (stringCollection.Contains(text))
					{
						int num = 1;
						while (stringCollection.Contains(string.Concat(new object[] { text, "(", num, ")" })))
						{
							num++;
						}
						text = string.Concat(new object[] { text, "(", num, ")" });
					}
					fsentry2.Name = text;
				}
				stringCollection.Add(fsentry2.Name);
				this.DoOneFSEntry(fsentry2);
			}
			base.EndUpdate();
			stringCollection.Clear();
			stringCollection = null;
			directoriesArrayList.Clear();
			fileArrayList.Clear();
			this.thumbCreator = new MWFFileView.ThumbnailCreator(new MWFFileView.ThumbnailDelegate(this.RedrawTheItem), this);
			new Thread(new ThreadStart(this.thumbCreator.MakeThumbnails))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00017CB0 File Offset: 0x00015EB0
		private void RedrawTheItem(FileViewListViewItem fi)
		{
			base.RedrawItems(fi.Index, fi.Index, false);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00017CC5 File Offset: 0x00015EC5
		public void AddControlToEnableDisableByDirStack(object control)
		{
			this.dirStackControlsOrComponents.Add(control);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00017CD4 File Offset: 0x00015ED4
		public void SetFolderUpToolBarButton(ToolBarButton tb)
		{
			this.folderUpToolBarButton = tb;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00017CDD File Offset: 0x00015EDD
		public void WriteRecentlyUsed(string fullfilename)
		{
			this.vfs.WriteRecentlyUsedFiles(fullfilename);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017CEC File Offset: 0x00015EEC
		private void EnableOrDisableDirstackObjects()
		{
			foreach (object obj in this.dirStackControlsOrComponents)
			{
				if (obj is Control)
				{
					(obj as Control).Enabled = this.directoryStack.Count > 1;
				}
				else if (obj is ToolBarButton)
				{
					(obj as ToolBarButton).Enabled = this.directoryStack.Count > 0;
				}
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00017D80 File Offset: 0x00015F80
		private void DoOneFSEntry(FSEntry fsEntry)
		{
			if (!this.ShowHiddenFiles && (fsEntry.Name.StartsWith(".") || (fsEntry.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
			{
				return;
			}
			FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsEntry);
			base.Items.Add(fileViewListViewItem);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private void MWF_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				this.OneDirUp();
				return;
			}
			if (e.Control && e.KeyCode == Keys.A && base.MultiSelect)
			{
				foreach (object obj in base.Items)
				{
					((ListViewItem)obj).Selected = true;
				}
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00017E4C File Offset: 0x0001604C
		protected override void OnClick(EventArgs e)
		{
			if (!base.MultiSelect && base.SelectedItems.Count > 0)
			{
				FSEntry fsentry = (base.SelectedItems[0] as FileViewListViewItem).FSEntry;
				if (fsentry.FileType == FSEntry.FSEntryType.File)
				{
					this.currentFSEntry = fsentry;
					EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFileChangedEvent];
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
			base.OnClick(e);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00017EC4 File Offset: 0x000160C4
		protected override void OnDoubleClick(EventArgs e)
		{
			if (base.SelectedItems.Count > 0)
			{
				FSEntry fsentry = (base.SelectedItems[0] as FileViewListViewItem).FSEntry;
				if ((fsentry.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
				{
					this.currentFSEntry = fsentry;
					EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFileChangedEvent];
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
					eventHandler = (EventHandler)base.Events[MWFFileView.MForceDialogEndEvent];
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
					return;
				}
				this.ChangeDirectory(null, fsentry.FullName);
				EventHandler eventHandler2 = (EventHandler)base.Events[MWFFileView.MDirectoryChangedEvent];
				if (eventHandler2 != null)
				{
					eventHandler2(this, EventArgs.Empty);
				}
			}
			base.OnDoubleClick(e);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00017F94 File Offset: 0x00016194
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.SelectedItems.Count > 0)
			{
				this.selectedFilesString = string.Empty;
				if (base.SelectedItems.Count == 1)
				{
					if (((base.SelectedItems[0] as FileViewListViewItem).FSEntry.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
					{
						this.selectedFilesString = base.SelectedItems[0].Text;
					}
				}
				else
				{
					foreach (object obj in base.SelectedItems)
					{
						FileViewListViewItem fileViewListViewItem = (FileViewListViewItem)obj;
						if ((fileViewListViewItem.FSEntry.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
						{
							this.selectedFilesString = this.selectedFilesString + "\"" + fileViewListViewItem.Text + "\" ";
						}
					}
				}
				EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFilesChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
			base.OnSelectedIndexChanged(e);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000180B0 File Offset: 0x000162B0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			FileViewListViewItem fileViewListViewItem = base.GetItemAt(e.X, e.Y) as FileViewListViewItem;
			if (fileViewListViewItem != null)
			{
				int index = fileViewListViewItem.Index;
				if (index != this.oldItemIndexForToolTip)
				{
					this.oldItemIndexForToolTip = index;
					if (this.toolTip != null && this.toolTip.Active)
					{
						this.toolTip.Active = false;
					}
					FSEntry fsentry = fileViewListViewItem.FSEntry;
					string text = string.Empty;
					if (fsentry.FileType == FSEntry.FSEntryType.Directory)
					{
						text = Locale.GetText("Directory: {0}", new object[] { fsentry.FullName });
					}
					else if (fsentry.FileType == FSEntry.FSEntryType.Device)
					{
						text = Locale.GetText("Device: {0}", new object[] { fsentry.FullName });
					}
					else if (fsentry.FileType == FSEntry.FSEntryType.Network)
					{
						text = Locale.GetText("Network: {0}", new object[] { fsentry.FullName });
					}
					else
					{
						text = Locale.GetText("File: {0}", new object[] { fsentry.FullName });
					}
					this.toolTip.SetToolTip(this, text);
					this.toolTip.Active = true;
				}
			}
			else
			{
				this.toolTip.Active = false;
			}
			base.OnMouseMove(e);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000181DC File Offset: 0x000163DC
		private void OnClickContextMenu(object sender, EventArgs e)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem == this.showHiddenFilesMenuItem)
			{
				menuItem.Checked = !menuItem.Checked;
				this.showHiddenFiles = menuItem.Checked;
				this.UpdateFileView();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001821C File Offset: 0x0001641C
		private void OnClickViewMenuSubItem(object sender, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			this.UpdateMenuItems(menuItem);
			base.BeginUpdate();
			switch (menuItem.Index)
			{
			case 0:
				base.View = View.SmallIcon;
				break;
			case 1:
				base.View = View.Tile;
				break;
			case 2:
				base.View = View.LargeIcon;
				break;
			case 3:
				base.View = View.List;
				break;
			case 4:
				base.View = View.Details;
				break;
			}
			if (base.View == View.Details)
			{
				base.Columns.AddRange(this.columns);
			}
			else
			{
				base.ListViewItemSorter = null;
				base.Columns.Clear();
			}
			base.EndUpdate();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000182C0 File Offset: 0x000164C0
		protected override void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			FSEntry fsentry = (base.SelectedItems[0] as FileViewListViewItem).FSEntry;
			if (fsentry.FileType != FSEntry.FSEntryType.Directory && fsentry.FileType != FSEntry.FSEntryType.File)
			{
				e.CancelEdit = true;
			}
			base.OnBeforeLabelEdit(e);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018304 File Offset: 0x00016504
		protected override void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			base.OnAfterLabelEdit(e);
			if (e.Label == null || base.Items[e.Item].Text == e.Label)
			{
				return;
			}
			FSEntry fsentry = (base.SelectedItems[0] as FileViewListViewItem).FSEntry;
			string text = ((this.currentFolderFSEntry.RealName != null) ? this.currentFolderFSEntry.RealName : this.currentFolder);
			FSEntry.FSEntryType fileType = fsentry.FileType;
			if (fileType != FSEntry.FSEntryType.File)
			{
				if (fileType == FSEntry.FSEntryType.Directory)
				{
					string text2 = ((fsentry.RealName != null) ? fsentry.RealName : fsentry.FullName);
					string text3 = Path.Combine(text, e.Label);
					if (!this.vfs.MoveFolder(text2, text3))
					{
						e.CancelEdit = true;
						return;
					}
					if (fsentry.RealName != null)
					{
						fsentry.RealName = text3;
						return;
					}
					fsentry.FullName = text3;
					return;
				}
			}
			else
			{
				string text4 = ((fsentry.RealName != null) ? fsentry.RealName : fsentry.FullName);
				string text5 = Path.Combine(text, e.Label);
				if (!this.vfs.MoveFile(text4, text5))
				{
					e.CancelEdit = true;
					return;
				}
				if (fsentry.RealName != null)
				{
					fsentry.RealName = text5;
					return;
				}
				fsentry.FullName = text5;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018440 File Offset: 0x00016640
		private void UpdateMenuItems(MenuItem senderMenuItem)
		{
			this.menuItemView.MenuItems[this.previousCheckedMenuItemIndex].Checked = false;
			this.menuItemView.MenuItems[senderMenuItem.Index].Checked = true;
			foreach (object obj in this.viewMenuItemClones)
			{
				MenuItem[] array = (MenuItem[])obj;
				array[this.previousCheckedMenuItemIndex].Checked = false;
				array[senderMenuItem.Index].Checked = true;
			}
			this.previousCheckedMenuItemIndex = senderMenuItem.Index;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000184F0 File Offset: 0x000166F0
		private void OnClickNewFolderMenuItem(object sender, EventArgs e)
		{
			this.CreateNewFolder();
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060005C9 RID: 1481 RVA: 0x000184F8 File Offset: 0x000166F8
		// (remove) Token: 0x060005CA RID: 1482 RVA: 0x0001850B File Offset: 0x0001670B
		public event EventHandler SelectedFileChanged
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MSelectedFileChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MSelectedFileChangedEvent, value);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060005CB RID: 1483 RVA: 0x0001851E File Offset: 0x0001671E
		// (remove) Token: 0x060005CC RID: 1484 RVA: 0x00018531 File Offset: 0x00016731
		public event EventHandler SelectedFilesChanged
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MSelectedFilesChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MSelectedFilesChangedEvent, value);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060005CD RID: 1485 RVA: 0x00018544 File Offset: 0x00016744
		// (remove) Token: 0x060005CE RID: 1486 RVA: 0x00018557 File Offset: 0x00016757
		public event EventHandler ForceDialogEnd
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MForceDialogEndEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MForceDialogEndEvent, value);
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001856C File Offset: 0x0001676C
		private void DeleteOldThumbnails()
		{
			foreach (object obj in base.Items)
			{
				FileViewListViewItem fileViewListViewItem = obj as FileViewListViewItem;
				if (fileViewListViewItem != null && fileViewListViewItem.FSEntry != null)
				{
					fileViewListViewItem.FSEntry.Dispose();
					fileViewListViewItem.FSEntry = null;
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000185DC File Offset: 0x000167DC
		protected override void Dispose(bool disposing)
		{
			this.DeleteOldThumbnails();
			base.Dispose(disposing);
		}

		// Token: 0x04000393 RID: 915
		private MWFFileView.ThumbnailCreator thumbCreator;

		// Token: 0x04000394 RID: 916
		private ArrayList filterArrayList;

		// Token: 0x04000395 RID: 917
		private bool showHiddenFiles;

		// Token: 0x04000396 RID: 918
		private string selectedFilesString;

		// Token: 0x04000397 RID: 919
		private int filterIndex = 1;

		// Token: 0x04000398 RID: 920
		private ToolTip toolTip;

		// Token: 0x04000399 RID: 921
		private int oldItemIndexForToolTip = -1;

		// Token: 0x0400039A RID: 922
		private ContextMenu contextMenu;

		// Token: 0x0400039B RID: 923
		private MenuItem menuItemView;

		// Token: 0x0400039C RID: 924
		private MenuItem menuItemNew;

		// Token: 0x0400039D RID: 925
		private MenuItem smallIconMenutItem;

		// Token: 0x0400039E RID: 926
		private MenuItem tilesMenutItem;

		// Token: 0x0400039F RID: 927
		private MenuItem largeIconMenutItem;

		// Token: 0x040003A0 RID: 928
		private MenuItem listMenutItem;

		// Token: 0x040003A1 RID: 929
		private MenuItem detailsMenutItem;

		// Token: 0x040003A2 RID: 930
		private MenuItem newFolderMenuItem;

		// Token: 0x040003A3 RID: 931
		private MenuItem showHiddenFilesMenuItem;

		// Token: 0x040003A4 RID: 932
		private int previousCheckedMenuItemIndex;

		// Token: 0x040003A5 RID: 933
		private ArrayList viewMenuItemClones = new ArrayList();

		// Token: 0x040003A6 RID: 934
		private FSEntry currentFSEntry;

		// Token: 0x040003A7 RID: 935
		private string currentFolder;

		// Token: 0x040003A8 RID: 936
		private string currentRealFolder;

		// Token: 0x040003A9 RID: 937
		private FSEntry currentFolderFSEntry;

		// Token: 0x040003AA RID: 938
		private Stack directoryStack = new Stack();

		// Token: 0x040003AB RID: 939
		private ArrayList dirStackControlsOrComponents = new ArrayList();

		// Token: 0x040003AC RID: 940
		private ToolBarButton folderUpToolBarButton;

		// Token: 0x040003AD RID: 941
		private ArrayList registered_senders = new ArrayList();

		// Token: 0x040003AE RID: 942
		private bool should_push = true;

		// Token: 0x040003AF RID: 943
		private MWFVFS vfs;

		// Token: 0x040003B0 RID: 944
		private View old_view;

		// Token: 0x040003B1 RID: 945
		private int old_menuitem_index;

		// Token: 0x040003B2 RID: 946
		private bool do_update_view;

		// Token: 0x040003B3 RID: 947
		private ColumnHeader[] columns;

		// Token: 0x040003B4 RID: 948
		private static object MSelectedFileChangedEvent = new object();

		// Token: 0x040003B5 RID: 949
		private static object MSelectedFilesChangedEvent = new object();

		// Token: 0x040003B6 RID: 950
		private static object MDirectoryChangedEvent = new object();

		// Token: 0x040003B7 RID: 951
		private static object MForceDialogEndEvent = new object();

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x060005D3 RID: 1491
		public delegate void ThumbnailDelegate(FileViewListViewItem fi);

		// Token: 0x0200008D RID: 141
		internal class ThumbnailCreator
		{
			// Token: 0x060005D4 RID: 1492 RVA: 0x00018615 File Offset: 0x00016815
			public ThumbnailCreator(MWFFileView.ThumbnailDelegate thumbnailDelegate, ListView listView)
			{
				this.thumbnailDelegate = thumbnailDelegate;
				this.control = listView;
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x00018638 File Offset: 0x00016838
			public void MakeThumbnails()
			{
				foreach (object obj in this.control.Items)
				{
					FileViewListViewItem fileViewListViewItem = obj as FileViewListViewItem;
					if (fileViewListViewItem != null && fileViewListViewItem.FSEntry != null && fileViewListViewItem.FSEntry.IsImageFile())
					{
						fileViewListViewItem.FSEntry.SetImage();
						if (this.stopped)
						{
							break;
						}
						if (this.thumbnailDelegate != null)
						{
							object obj2 = this.lockobject;
							lock (obj2)
							{
								object[] array = new object[] { fileViewListViewItem };
								this.control.Invoke(this.thumbnailDelegate, array);
							}
						}
					}
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x00018714 File Offset: 0x00016914
			public void Stop()
			{
				object obj = this.lockobject;
				lock (obj)
				{
					this.stopped = true;
				}
			}

			// Token: 0x040003B8 RID: 952
			private MWFFileView.ThumbnailDelegate thumbnailDelegate;

			// Token: 0x040003B9 RID: 953
			private ListView control;

			// Token: 0x040003BA RID: 954
			private readonly object lockobject = new object();

			// Token: 0x040003BB RID: 955
			private bool stopped;
		}
	}
}
