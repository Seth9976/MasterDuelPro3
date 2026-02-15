using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a folder. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AD RID: 173
	[DefaultEvent("HelpRequest")]
	[DefaultProperty("SelectedPath")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public sealed class FolderBrowserDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FolderBrowserDialog" /> class.</summary>
		// Token: 0x06000676 RID: 1654 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public FolderBrowserDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			Size empty = Size.Empty;
			Point empty2 = Point.Empty;
			object value = MWFConfig.GetValue(this.folderbrowserdialog_string, this.width_string);
			object value2 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.height_string);
			if (value2 != null && value != null)
			{
				empty = new Size((int)value, (int)value2);
			}
			object value3 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.x_string);
			object value4 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.y_string);
			if (value3 != null && value4 != null)
			{
				empty2 = new Point((int)value3, (int)value4);
			}
			this.newFolderButton = new Button();
			this.folderBrowserTreeView = new FolderBrowserDialog.FolderBrowserTreeView(this);
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.descriptionLabel = new Label();
			this.folderBrowserTreeViewContextMenu = new ContextMenu();
			this.form.AcceptButton = this.okButton;
			this.form.CancelButton = this.cancelButton;
			this.form.SuspendLayout();
			this.form.ClientSize = new Size(322, 324);
			this.form.MinimumSize = new Size(310, 254);
			this.form.Text = "Browse For Folder";
			this.form.SizeGripStyle = SizeGripStyle.Show;
			this.newFolderMenuItem = new MenuItem("New Folder", new EventHandler(this.OnClickNewFolderButton));
			this.folderBrowserTreeViewContextMenu.MenuItems.Add(this.newFolderMenuItem);
			this.descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.descriptionLabel.Location = new Point(15, 14);
			this.descriptionLabel.Size = new Size(292, 40);
			this.descriptionLabel.TabIndex = 0;
			this.descriptionLabel.Text = string.Empty;
			this.folderBrowserTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.folderBrowserTreeView.ImageIndex = -1;
			this.folderBrowserTreeView.Location = new Point(15, 60);
			this.folderBrowserTreeView.SelectedImageIndex = -1;
			this.folderBrowserTreeView.Size = new Size(292, 212);
			this.folderBrowserTreeView.TabIndex = 3;
			this.folderBrowserTreeView.ShowLines = false;
			this.folderBrowserTreeView.ShowPlusMinus = true;
			this.folderBrowserTreeView.HotTracking = true;
			this.folderBrowserTreeView.BorderStyle = BorderStyle.Fixed3D;
			this.folderBrowserTreeView.ContextMenu = this.folderBrowserTreeViewContextMenu;
			this.newFolderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.newFolderButton.FlatStyle = FlatStyle.System;
			this.newFolderButton.Location = new Point(15, 285);
			this.newFolderButton.Size = new Size(105, 23);
			this.newFolderButton.TabIndex = 4;
			this.newFolderButton.Text = "Make New Folder";
			this.newFolderButton.Enabled = true;
			this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Location = new Point(135, 285);
			this.okButton.Size = new Size(80, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(227, 285);
			this.cancelButton.Size = new Size(80, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.okButton);
			this.form.Controls.Add(this.newFolderButton);
			this.form.Controls.Add(this.folderBrowserTreeView);
			this.form.Controls.Add(this.descriptionLabel);
			this.form.ResumeLayout(false);
			if (empty != Size.Empty)
			{
				this.form.Size = empty;
			}
			if (empty2 != Point.Empty)
			{
				this.form.Location = empty2;
			}
			this.okButton.Click += this.OnClickOKButton;
			this.cancelButton.Click += this.OnClickCancelButton;
			this.newFolderButton.Click += this.OnClickNewFolderButton;
			this.form.VisibleChanged += this.OnFormVisibleChanged;
			this.RootFolder = this.rootFolder;
		}

		/// <summary>Gets or sets the descriptive text displayed above the tree view control in the dialog box.</summary>
		/// <returns>The description to display. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001C231 File Offset: 0x0001A431
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001C223 File Offset: 0x0001A423
		[Browsable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public string Description
		{
			get
			{
				return this.descriptionLabel.Text;
			}
			set
			{
				this.descriptionLabel.Text = value;
			}
		}

		/// <summary>Gets or sets the root folder where the browsing starts from.</summary>
		/// <returns>One of the <see cref="T:System.Environment.SpecialFolder" /> values. The default is Desktop.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Environment.SpecialFolder" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001C27C File Offset: 0x0001A47C
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001C240 File Offset: 0x0001A440
		[Browsable(true)]
		[DefaultValue(Environment.SpecialFolder.Desktop)]
		[Localizable(false)]
		[TypeConverter(typeof(SpecialFolderEnumConverter))]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				return this.rootFolder;
			}
			set
			{
				Type typeFromHandle = typeof(Environment.SpecialFolder);
				if (!Enum.IsDefined(typeFromHandle, (int)value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeFromHandle);
				}
				this.rootFolder = value;
			}
		}

		/// <summary>Gets or sets the path selected by the user.</summary>
		/// <returns>The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001C29E File Offset: 0x0001A49E
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001C284 File Offset: 0x0001A484
		[Browsable(true)]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public string SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.selectedPath = value;
				this.old_selectedPath = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.</summary>
		/// <returns>true if the New Folder button is shown in the dialog box; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001C2A6 File Offset: 0x0001A4A6
		[Browsable(true)]
		[DefaultValue(true)]
		[Localizable(false)]
		public bool ShowNewFolderButton
		{
			get
			{
				return this.showNewFolderButton;
			}
			set
			{
				if (value != this.showNewFolderButton)
				{
					this.newFolderButton.Visible = value;
					this.showNewFolderButton = value;
				}
			}
		}

		/// <summary>Resets properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600067F RID: 1663 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		public override void Reset()
		{
			this.Description = string.Empty;
			this.RootFolder = Environment.SpecialFolder.Desktop;
			this.selectedPath = string.Empty;
			this.ShowNewFolderButton = true;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C2F2 File Offset: 0x0001A4F2
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			this.folderBrowserTreeView.RootFolder = this.RootFolder;
			this.folderBrowserTreeView.SelectedPath = this.SelectedPath;
			this.form.Refresh();
			return true;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C322 File Offset: 0x0001A522
		private void OnClickOKButton(object sender, EventArgs e)
		{
			this.WriteConfigValues();
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001C336 File Offset: 0x0001A536
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			this.WriteConfigValues();
			this.selectedPath = this.old_selectedPath;
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001C356 File Offset: 0x0001A556
		private void OnClickNewFolderButton(object sender, EventArgs e)
		{
			this.folderBrowserTreeView.CreateNewFolder();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C363 File Offset: 0x0001A563
		private void OnFormVisibleChanged(object sender, EventArgs e)
		{
			if (this.form.Visible && this.okButton.Enabled)
			{
				this.okButton.Select();
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C38C File Offset: 0x0001A58C
		private void WriteConfigValues()
		{
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.width_string, this.form.Width);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.height_string, this.form.Height);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.x_string, this.form.Location.X);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.y_string, this.form.Location.Y);
		}

		// Token: 0x04000442 RID: 1090
		private Environment.SpecialFolder rootFolder;

		// Token: 0x04000443 RID: 1091
		private string selectedPath = string.Empty;

		// Token: 0x04000444 RID: 1092
		private bool showNewFolderButton = true;

		// Token: 0x04000445 RID: 1093
		private Label descriptionLabel;

		// Token: 0x04000446 RID: 1094
		private Button cancelButton;

		// Token: 0x04000447 RID: 1095
		private Button okButton;

		// Token: 0x04000448 RID: 1096
		private FolderBrowserDialog.FolderBrowserTreeView folderBrowserTreeView;

		// Token: 0x04000449 RID: 1097
		private Button newFolderButton;

		// Token: 0x0400044A RID: 1098
		private ContextMenu folderBrowserTreeViewContextMenu;

		// Token: 0x0400044B RID: 1099
		private MenuItem newFolderMenuItem;

		// Token: 0x0400044C RID: 1100
		private string old_selectedPath = string.Empty;

		// Token: 0x0400044D RID: 1101
		private readonly string folderbrowserdialog_string = "FolderBrowserDialog";

		// Token: 0x0400044E RID: 1102
		private readonly string width_string = "Width";

		// Token: 0x0400044F RID: 1103
		private readonly string height_string = "Height";

		// Token: 0x04000450 RID: 1104
		private readonly string x_string = "X";

		// Token: 0x04000451 RID: 1105
		private readonly string y_string = "Y";

		// Token: 0x020000AE RID: 174
		internal class FolderBrowserTreeView : TreeView
		{
			// Token: 0x06000686 RID: 1670 RVA: 0x0001C42D File Offset: 0x0001A62D
			public FolderBrowserTreeView(FolderBrowserDialog parent_dialog)
			{
				this.parentDialog = parent_dialog;
				base.HideSelection = false;
				base.ImageList = this.imageList;
				this.SetupImageList();
			}

			// Token: 0x17000192 RID: 402
			// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001C46C File Offset: 0x0001A66C
			public Environment.SpecialFolder RootFolder
			{
				set
				{
					this.rootFolder = value;
					string text = string.Empty;
					Environment.SpecialFolder specialFolder = this.rootFolder;
					if (specialFolder <= Environment.SpecialFolder.MyDocuments)
					{
						if (specialFolder == Environment.SpecialFolder.Desktop)
						{
							this.root_node = new FolderBrowserDialog.FBTreeNode("Desktop");
							this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
							text = MWFVFS.DesktopPrefix;
							goto IL_011B;
						}
						if (specialFolder == Environment.SpecialFolder.MyDocuments)
						{
							this.root_node = new FolderBrowserDialog.FBTreeNode("Personal");
							text = MWFVFS.PersonalPrefix;
							this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
							goto IL_011B;
						}
					}
					else
					{
						if (specialFolder == Environment.SpecialFolder.Recent)
						{
							this.root_node = new FolderBrowserDialog.FBTreeNode("My Recent Documents");
							this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesRecentDocuments);
							text = MWFVFS.RecentlyUsedPrefix;
							goto IL_011B;
						}
						if (specialFolder == Environment.SpecialFolder.MyComputer)
						{
							this.root_node = new FolderBrowserDialog.FBTreeNode("My Computer");
							text = MWFVFS.MyComputerPrefix;
							goto IL_011B;
						}
					}
					this.root_node = new FolderBrowserDialog.FBTreeNode(this.rootFolder.ToString());
					this.root_node.RealPath = Environment.GetFolderPath(this.rootFolder);
					text = this.root_node.RealPath;
					IL_011B:
					this.root_node.Tag = text;
					this.root_node.ImageIndex = this.NodeImageIndex(text);
					base.BeginUpdate();
					base.Nodes.Clear();
					base.EndUpdate();
					this.FillNode(this.root_node);
					this.root_node.Expand();
					base.Nodes.Add(this.root_node);
				}
			}

			// Token: 0x17000193 RID: 403
			// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
			public string SelectedPath
			{
				set
				{
					if (value.Length == 0)
					{
						return;
					}
					if (!Path.IsPathRooted(value))
					{
						return;
					}
					try
					{
						if (this.Check_if_path_is_child_of_RootFolder(value))
						{
							this.SetSelectedPath(Path.GetFullPath(value));
						}
					}
					catch (Exception)
					{
						base.EndUpdate();
						this.RootFolder = this.rootFolder;
					}
				}
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0001C650 File Offset: 0x0001A850
			public void CreateNewFolder()
			{
				FolderBrowserDialog.FBTreeNode fbtreeNode = ((this.node_under_mouse == null) ? (base.SelectedNode as FolderBrowserDialog.FBTreeNode) : (this.node_under_mouse as FolderBrowserDialog.FBTreeNode));
				if (fbtreeNode == null || fbtreeNode.RealPath == null)
				{
					return;
				}
				string text = "New Folder";
				if (Directory.Exists(Path.Combine(fbtreeNode.RealPath, text)))
				{
					int num = 1;
					if (XplatUI.RunningOnUnix)
					{
						text = text + "-" + num;
					}
					else
					{
						text = string.Concat(new object[] { text, " (", num, ")" });
					}
					while (Directory.Exists(Path.Combine(fbtreeNode.RealPath, text)))
					{
						num++;
						if (XplatUI.RunningOnUnix)
						{
							text = "New Folder-" + num;
						}
						else
						{
							text = "New Folder (" + num + ")";
						}
					}
				}
				this.parent_real_path = fbtreeNode.RealPath;
				this.FillNode(fbtreeNode);
				this.dont_do_onbeforeexpand = true;
				fbtreeNode.Expand();
				this.dont_do_onbeforeexpand = false;
				string text2 = Path.Combine(fbtreeNode.RealPath, text);
				if (!this.vfs.CreateFolder(text2))
				{
					return;
				}
				FolderBrowserDialog.FBTreeNode fbtreeNode2 = new FolderBrowserDialog.FBTreeNode(text);
				fbtreeNode2.ImageIndex = this.NodeImageIndex(text);
				fbtreeNode2.Tag = (fbtreeNode2.RealPath = text2);
				fbtreeNode.Nodes.Add(fbtreeNode2);
				base.LabelEdit = true;
				fbtreeNode2.BeginEdit();
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
			protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
			{
				if (e.Label != null)
				{
					if (e.Label.Length <= 0)
					{
						e.CancelEdit = true;
						e.Node.BeginEdit();
						return;
					}
					FolderBrowserDialog.FBTreeNode fbtreeNode = e.Node as FolderBrowserDialog.FBTreeNode;
					string realPath = fbtreeNode.RealPath;
					string text = Path.Combine(this.parent_real_path, e.Label);
					if (!this.vfs.MoveFolder(realPath, text))
					{
						e.CancelEdit = true;
						e.Node.BeginEdit();
						return;
					}
					fbtreeNode.Tag = (fbtreeNode.RealPath = text);
				}
				if (this.node_under_mouse == base.SelectedNode)
				{
					base.SelectedNode = e.Node;
				}
				base.LabelEdit = false;
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x0001C870 File Offset: 0x0001AA70
			private void SetSelectedPath(string path)
			{
				base.BeginUpdate();
				FolderBrowserDialog.FBTreeNode fbtreeNode = this.FindPathInNodes(path, base.Nodes);
				if (fbtreeNode == null)
				{
					Stack stack = new Stack();
					string text = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
					if (!XplatUI.RunningOnUnix && text.Length == 2)
					{
						text += Path.DirectorySeparatorChar.ToString();
					}
					while (fbtreeNode == null && text.Length > 0)
					{
						fbtreeNode = this.FindPathInNodes(text, base.Nodes);
						if (fbtreeNode == null)
						{
							string text2 = text.Substring(0, text.LastIndexOf(Path.DirectorySeparatorChar));
							string text3 = text.Replace(text2, string.Empty);
							stack.Push(text3);
							text = text2;
						}
					}
					if (fbtreeNode == null)
					{
						base.EndUpdate();
						this.RootFolder = this.rootFolder;
						return;
					}
					this.FillNode(fbtreeNode);
					fbtreeNode.Expand();
					while (stack.Count > 0)
					{
						string text4 = stack.Pop() as string;
						foreach (object obj in fbtreeNode.Nodes)
						{
							FolderBrowserDialog.FBTreeNode fbtreeNode2 = ((TreeNode)obj) as FolderBrowserDialog.FBTreeNode;
							if (text + text4 == fbtreeNode2.RealPath)
							{
								fbtreeNode = fbtreeNode2;
								text += text4;
								this.FillNode(fbtreeNode);
								fbtreeNode.Expand();
								break;
							}
						}
					}
					foreach (object obj2 in fbtreeNode.Nodes)
					{
						FolderBrowserDialog.FBTreeNode fbtreeNode3 = ((TreeNode)obj2) as FolderBrowserDialog.FBTreeNode;
						if (path == fbtreeNode3.RealPath)
						{
							fbtreeNode = fbtreeNode3;
							break;
						}
					}
				}
				if (fbtreeNode != null)
				{
					base.SelectedNode = fbtreeNode;
					fbtreeNode.EnsureVisible();
				}
				base.EndUpdate();
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0001CA60 File Offset: 0x0001AC60
			private FolderBrowserDialog.FBTreeNode FindPathInNodes(string path, TreeNodeCollection nodes)
			{
				if (!XplatUI.RunningOnUnix && path.Length == 2)
				{
					path += Path.DirectorySeparatorChar.ToString();
				}
				foreach (object obj in nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					FolderBrowserDialog.FBTreeNode fbtreeNode = treeNode as FolderBrowserDialog.FBTreeNode;
					if (fbtreeNode != null && fbtreeNode.RealPath != null && fbtreeNode.RealPath == path)
					{
						return fbtreeNode;
					}
					FolderBrowserDialog.FBTreeNode fbtreeNode2 = this.FindPathInNodes(path, treeNode.Nodes);
					if (fbtreeNode2 != null)
					{
						return fbtreeNode2;
					}
				}
				return null;
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x0001CB18 File Offset: 0x0001AD18
			private bool Check_if_path_is_child_of_RootFolder(string path)
			{
				string realPath = this.root_node.RealPath;
				if (realPath != null || this.rootFolder == Environment.SpecialFolder.MyComputer)
				{
					try
					{
						if (!Directory.Exists(path))
						{
							return false;
						}
						Environment.SpecialFolder specialFolder = this.rootFolder;
						if (specialFolder != Environment.SpecialFolder.Desktop)
						{
							if (specialFolder != Environment.SpecialFolder.MyDocuments)
							{
								if (specialFolder != Environment.SpecialFolder.MyComputer)
								{
									return false;
								}
							}
							else
							{
								if (!path.StartsWith(realPath))
								{
									return false;
								}
								return true;
							}
						}
						return true;
					}
					catch
					{
					}
					return false;
				}
				return false;
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001CB90 File Offset: 0x0001AD90
			private void FillNode(TreeNode node)
			{
				base.BeginUpdate();
				node.Nodes.Clear();
				this.vfs.ChangeDirectory((string)node.Tag);
				foreach (object obj in this.vfs.GetFoldersOnly())
				{
					FSEntry fsentry = (FSEntry)obj;
					if (!fsentry.Name.StartsWith("."))
					{
						FolderBrowserDialog.FBTreeNode fbtreeNode = new FolderBrowserDialog.FBTreeNode(fsentry.Name);
						fbtreeNode.Tag = fsentry.FullName;
						fbtreeNode.RealPath = ((fsentry.RealName == null) ? fsentry.FullName : fsentry.RealName);
						fbtreeNode.ImageIndex = this.NodeImageIndex(fsentry.FullName);
						this.vfs.ChangeDirectory(fsentry.FullName);
						using (IEnumerator enumerator2 = this.vfs.GetFoldersOnly().GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (!((FSEntry)enumerator2.Current).Name.StartsWith("."))
								{
									fbtreeNode.Nodes.Add(new TreeNode(string.Empty));
									break;
								}
							}
						}
						node.Nodes.Add(fbtreeNode);
					}
				}
				base.EndUpdate();
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x0001CD0C File Offset: 0x0001AF0C
			private void SetupImageList()
			{
				this.imageList.ColorDepth = ColorDepth.Depth32Bit;
				this.imageList.ImageSize = new Size(16, 16);
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.NormalFolder, 16));
				this.imageList.TransparentColor = Color.Transparent;
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0001CDFC File Offset: 0x0001AFFC
			private int NodeImageIndex(string path)
			{
				int num = 5;
				if (path == MWFVFS.DesktopPrefix)
				{
					num = 1;
				}
				else if (path == MWFVFS.RecentlyUsedPrefix)
				{
					num = 0;
				}
				else if (path == MWFVFS.PersonalPrefix)
				{
					num = 2;
				}
				else if (path == MWFVFS.MyComputerPrefix)
				{
					num = 3;
				}
				else if (path == MWFVFS.MyNetworkPrefix)
				{
					num = 4;
				}
				return num;
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x0001CE60 File Offset: 0x0001B060
			protected override void OnAfterSelect(TreeViewEventArgs e)
			{
				if (e.Node == null)
				{
					return;
				}
				FolderBrowserDialog.FBTreeNode fbtreeNode = e.Node as FolderBrowserDialog.FBTreeNode;
				if (fbtreeNode.RealPath == null || fbtreeNode.RealPath.IndexOf("://") != -1)
				{
					this.parentDialog.okButton.Enabled = false;
					this.parentDialog.newFolderButton.Enabled = false;
					this.parentDialog.newFolderMenuItem.Enabled = false;
					this.dont_enable = true;
				}
				else
				{
					this.parentDialog.okButton.Enabled = true;
					this.parentDialog.newFolderButton.Enabled = true;
					this.parentDialog.newFolderMenuItem.Enabled = true;
					this.parentDialog.selectedPath = fbtreeNode.RealPath;
					this.dont_enable = false;
				}
				base.OnAfterSelect(e);
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x0001CF2B File Offset: 0x0001B12B
			protected internal override void OnBeforeExpand(TreeViewCancelEventArgs e)
			{
				if (!this.dont_do_onbeforeexpand)
				{
					if (e.Node == this.root_node)
					{
						return;
					}
					this.FillNode(e.Node);
				}
				base.OnBeforeExpand(e);
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0001CF57 File Offset: 0x0001B157
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.node_under_mouse = base.GetNodeAt(e.X, e.Y);
				base.OnMouseDown(e);
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x0001CF78 File Offset: 0x0001B178
			protected override void OnMouseUp(MouseEventArgs e)
			{
				if (base.SelectedNode == null)
				{
					this.parentDialog.okButton.Enabled = false;
					this.parentDialog.newFolderButton.Enabled = false;
					this.parentDialog.newFolderMenuItem.Enabled = false;
				}
				else if (!this.dont_enable)
				{
					this.parentDialog.okButton.Enabled = true;
					this.parentDialog.newFolderButton.Enabled = true;
					this.parentDialog.newFolderMenuItem.Enabled = true;
				}
				this.node_under_mouse = null;
				base.OnMouseUp(e);
			}

			// Token: 0x04000452 RID: 1106
			private MWFVFS vfs = new MWFVFS();

			// Token: 0x04000453 RID: 1107
			private new FolderBrowserDialog.FBTreeNode root_node;

			// Token: 0x04000454 RID: 1108
			private FolderBrowserDialog parentDialog;

			// Token: 0x04000455 RID: 1109
			private ImageList imageList = new ImageList();

			// Token: 0x04000456 RID: 1110
			private Environment.SpecialFolder rootFolder;

			// Token: 0x04000457 RID: 1111
			private bool dont_enable;

			// Token: 0x04000458 RID: 1112
			private TreeNode node_under_mouse;

			// Token: 0x04000459 RID: 1113
			private string parent_real_path;

			// Token: 0x0400045A RID: 1114
			private bool dont_do_onbeforeexpand;
		}

		// Token: 0x020000AF RID: 175
		internal class FBTreeNode : TreeNode
		{
			// Token: 0x06000695 RID: 1685 RVA: 0x0001D00B File Offset: 0x0001B20B
			public FBTreeNode(string text)
			{
				base.Text = text;
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001D023 File Offset: 0x0001B223
			// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001D01A File Offset: 0x0001B21A
			public string RealPath
			{
				get
				{
					return this.realPath;
				}
				set
				{
					this.realPath = value;
				}
			}

			// Token: 0x0400045B RID: 1115
			private string realPath;
		}
	}
}
