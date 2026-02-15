using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F7 RID: 503
	[DefaultProperty("Text")]
	[TypeConverter(typeof(TreeNodeConverter))]
	[Serializable]
	public class TreeNode : MarshalByRefObject, ICloneable, ISerializable
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0006B012 File Offset: 0x00069212
		internal TreeNode(TreeView tree_view)
			: this()
		{
			this.tree_view = tree_view;
			this.is_expanded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class using the specified serialization information and context.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to deserialize the class.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream.</param>
		// Token: 0x0600154C RID: 5452 RVA: 0x0006B028 File Offset: 0x00069228
		protected TreeNode(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				SerializationEntry serializationEntry = enumerator.Current;
				string text = serializationEntry.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num2 <= 2041341998U)
				{
					if (num2 != 1011358670U)
					{
						if (num2 != 1041509726U)
						{
							if (num2 == 2041341998U)
							{
								if (text == "ImageIndex")
								{
									this.image_index = (int)serializationEntry.Value;
								}
							}
						}
						else if (text == "Text")
						{
							this.Text = (string)serializationEntry.Value;
						}
					}
					else if (text == "PropBag")
					{
						this.prop_bag = (OwnerDrawPropertyBag)serializationEntry.Value;
					}
				}
				else if (num2 <= 3693047415U)
				{
					if (num2 != 2569126364U)
					{
						if (num2 == 3693047415U)
						{
							if (text == "SelectedImageIndex")
							{
								this.selected_image_index = (int)serializationEntry.Value;
							}
						}
					}
					else if (text == "ChildCount")
					{
						num = (int)serializationEntry.Value;
					}
				}
				else if (num2 != 3931153718U)
				{
					if (num2 == 4169356339U)
					{
						if (text == "Tag")
						{
							this.tag = serializationEntry.Value;
						}
					}
				}
				else if (text == "IsChecked")
				{
					this.check = (bool)serializationEntry.Value;
				}
			}
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					TreeNode treeNode = (TreeNode)serializationInfo.GetValue("children" + i, typeof(TreeNode));
					this.Nodes.Add(treeNode);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class.</summary>
		// Token: 0x0600154D RID: 5453 RVA: 0x0006B218 File Offset: 0x00069418
		public TreeNode()
		{
			this.nodes = new TreeNodeCollection(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
		// Token: 0x0600154E RID: 5454 RVA: 0x0001D00B File Offset: 0x0001B20B
		public TreeNode(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>Copies the tree node and the entire subtree rooted at this tree node.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600154F RID: 5455 RVA: 0x0006B28C File Offset: 0x0006948C
		public virtual object Clone()
		{
			TreeNode treeNode = (TreeNode)Activator.CreateInstance(base.GetType());
			treeNode.name = this.name;
			treeNode.text = this.text;
			treeNode.image_key = this.image_key;
			treeNode.image_index = this.image_index;
			treeNode.selected_image_index = this.selected_image_index;
			treeNode.selected_image_key = this.selected_image_key;
			treeNode.state_image_index = this.state_image_index;
			treeNode.state_image_key = this.state_image_key;
			treeNode.tag = this.tag;
			treeNode.check = this.check;
			treeNode.tool_tip_text = this.tool_tip_text;
			treeNode.context_menu = this.context_menu;
			treeNode.context_menu_strip = this.context_menu_strip;
			if (this.nodes != null)
			{
				foreach (object obj in this.nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj;
					treeNode.nodes.Add((TreeNode)treeNode2.Clone());
				}
			}
			if (this.prop_bag != null)
			{
				treeNode.prop_bag = OwnerDrawPropertyBag.Copy(this.prop_bag);
			}
			return treeNode;
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination information for this serialization.</param>
		// Token: 0x06001550 RID: 5456 RVA: 0x0006B3C4 File Offset: 0x000695C4
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Text", this.Text);
			si.AddValue("prop_bag", this.prop_bag, typeof(OwnerDrawPropertyBag));
			si.AddValue("ImageIndex", this.ImageIndex);
			si.AddValue("SelectedImageIndex", this.SelectedImageIndex);
			si.AddValue("Tag", this.Tag);
			si.AddValue("Checked", this.Checked);
			si.AddValue("NumberOfChildren", this.Nodes.Count);
			for (int i = 0; i < this.Nodes.Count; i++)
			{
				si.AddValue("Child-" + i, this.Nodes[i], typeof(TreeNode));
			}
		}

		/// <summary>Gets or sets the background color of the tree node.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the tree node. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0006B499 File Offset: 0x00069699
		public Color BackColor
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.BackColor;
				}
				return Color.Empty;
			}
		}

		/// <summary>Gets the bounds of the tree node.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0006B4B4 File Offset: 0x000696B4
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				if (this.TreeView == null)
				{
					return Rectangle.Empty;
				}
				int x = this.GetX();
				int y = this.GetY();
				if (this.width == -1)
				{
					this.width = this.TreeView.GetNodeWidth(this);
				}
				return new Rectangle(x, y, this.width, this.TreeView.ActualItemHeight);
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0006B510 File Offset: 0x00069710
		internal int GetY()
		{
			if (this.TreeView == null)
			{
				return 0;
			}
			return (this.visible_order - 1) * this.TreeView.ActualItemHeight - this.TreeView.skipped_nodes * this.TreeView.ActualItemHeight;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0006B548 File Offset: 0x00069748
		internal int GetX()
		{
			if (this.TreeView == null)
			{
				return 0;
			}
			int indentLevel = this.IndentLevel;
			int num = (this.TreeView.ShowRootLines ? 1 : 0);
			int num2 = (this.TreeView.CheckBoxes ? 19 : 0);
			if (!this.TreeView.CheckBoxes && this.StateImage != null)
			{
				num2 = 19;
			}
			int num3 = ((this.TreeView.ImageList != null) ? (this.TreeView.ImageList.ImageSize.Width + 3) : 0);
			return (indentLevel + num) * this.TreeView.Indent + num2 + num3 - this.TreeView.hbar_offset;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0006B5EC File Offset: 0x000697EC
		internal int GetLinesX()
		{
			int num = (this.TreeView.ShowRootLines ? 1 : 0);
			return (this.IndentLevel + num) * this.TreeView.Indent - this.TreeView.hbar_offset;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0006B62B File Offset: 0x0006982B
		internal int GetImageX()
		{
			return this.GetLinesX() + ((this.TreeView.CheckBoxes || this.StateImage != null) ? 19 : 0);
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0006B650 File Offset: 0x00069850
		internal int IndentLevel
		{
			get
			{
				TreeNode treeNode = this;
				int num = 0;
				while (treeNode.Parent != null)
				{
					treeNode = treeNode.Parent;
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree node is in a checked state.</summary>
		/// <returns>true if the tree node is in a checked state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0006B677 File Offset: 0x00069877
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0006B680 File Offset: 0x00069880
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.check;
			}
			set
			{
				if (this.check == value)
				{
					return;
				}
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, this.check_reason);
				if (this.TreeView != null)
				{
					this.TreeView.OnBeforeCheck(treeViewCancelEventArgs);
				}
				if (!treeViewCancelEventArgs.Cancel)
				{
					this.check = value;
					if (this.TreeView != null)
					{
						this.TreeView.OnAfterCheck(new TreeViewEventArgs(this, this.check_reason));
					}
					if (this.TreeView != null)
					{
						this.TreeView.UpdateNode(this);
					}
				}
				this.check_reason = TreeViewAction.Unknown;
			}
		}

		/// <summary>Gets the shortcut menu that is associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that is associated with the tree node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0006B702 File Offset: 0x00069902
		[DefaultValue(null)]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.context_menu;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the tree node.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0006B70A File Offset: 0x0006990A
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.context_menu_strip;
			}
		}

		/// <summary>Gets the first child tree node in the tree node collection.</summary>
		/// <returns>The first child <see cref="T:System.Windows.Forms.TreeNode" /> in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0006B712 File Offset: 0x00069912
		[Browsable(false)]
		public TreeNode FirstNode
		{
			get
			{
				if (this.nodes.Count > 0)
				{
					return this.nodes[0];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the foreground color of the tree node.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0006B730 File Offset: 0x00069930
		public Color ForeColor
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.ForeColor;
				}
				if (this.TreeView != null)
				{
					return this.TreeView.ForeColor;
				}
				return Color.Empty;
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed when the tree node is in the unselected state.</summary>
		/// <returns>A zero-based index value that represents the image position in the assigned <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006B75F File Offset: 0x0006995F
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0006B768 File Offset: 0x00069968
		[DefaultValue(-1)]
		[RelatedImageList("TreeView.ImageList")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (this.image_index == value)
				{
					return;
				}
				this.image_index = value;
				this.image_key = string.Empty;
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.UpdateNode(this);
				}
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in an editable state.</summary>
		/// <returns>true if the tree node is in editable state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0006B7A4 File Offset: 0x000699A4
		[Browsable(false)]
		public bool IsEditing
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.edit_node == this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the expanded state.</summary>
		/// <returns>true if the tree node is in the expanded state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0006B7C8 File Offset: 0x000699C8
		[Browsable(false)]
		public bool IsExpanded
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && treeView.IsHandleCreated)
				{
					bool flag = false;
					using (IEnumerator enumerator = this.TreeView.Nodes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((TreeNode)enumerator.Current).Nodes.Count > 0)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return this.is_expanded;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the selected state.</summary>
		/// <returns>true if the tree node is in the selected state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0006B84C File Offset: 0x00069A4C
		[Browsable(false)]
		public bool IsSelected
		{
			get
			{
				return this.TreeView != null && this.TreeView.IsHandleCreated && this.TreeView.SelectedNode == this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is visible or partially visible.</summary>
		/// <returns>true if the tree node is visible or partially visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0006B874 File Offset: 0x00069A74
		[Browsable(false)]
		public bool IsVisible
		{
			get
			{
				return this.TreeView != null && this.TreeView.IsHandleCreated && this.TreeView.Visible && this.visible_order > this.TreeView.skipped_nodes && this.visible_order - this.TreeView.skipped_nodes <= this.TreeView.VisibleCount && this.ArePreviousNodesExpanded;
			}
		}

		/// <summary>Gets the last child tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the last child tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0006B8DF File Offset: 0x00069ADF
		[Browsable(false)]
		public TreeNode LastNode
		{
			get
			{
				if (this.nodes != null && this.nodes.Count != 0)
				{
					return this.nodes[this.nodes.Count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the next sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next sibling tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0006B910 File Offset: 0x00069B10
		[Browsable(false)]
		public TreeNode NextNode
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				int index = this.Index;
				if (this.parent.Nodes.Count > index + 1)
				{
					return this.parent.Nodes[index + 1];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the font that is used to display the text on the tree node label.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that is used to display the text on the tree node label.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0006B958 File Offset: 0x00069B58
		[DefaultValue(null)]
		[Localizable(true)]
		public Font NodeFont
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.Font;
				}
				if (this.TreeView != null)
				{
					return this.TreeView.Font;
				}
				return null;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects assigned to the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the current tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0006B983 File Offset: 0x00069B83
		[Browsable(false)]
		[ListBindable(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
				}
				return this.nodes;
			}
		}

		/// <summary>Gets the parent tree node of the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the parent of the current tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0006B9A0 File Offset: 0x00069BA0
		[Browsable(false)]
		public TreeNode Parent
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && treeView.root_node == this.parent)
				{
					return null;
				}
				return this.parent;
			}
		}

		/// <summary>Gets the previous sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous sibling tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0006B9D0 File Offset: 0x00069BD0
		[Browsable(false)]
		public TreeNode PrevNode
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				int index = this.Index;
				if (index <= 0 || index > this.parent.Nodes.Count)
				{
					return null;
				}
				return this.parent.Nodes[index - 1];
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when the tree node is in the selected state.</summary>
		/// <returns>A zero-based index value that represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0006BA1A File Offset: 0x00069C1A
		[DefaultValue(-1)]
		[RelatedImageList("TreeView.ImageList")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public int SelectedImageIndex
		{
			get
			{
				return this.selected_image_index;
			}
		}

		/// <summary>Gets or sets the object that contains data about the tree node.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the tree node. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0006BA22 File Offset: 0x00069C22
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0006BA2A File Offset: 0x00069C2A
		[Bindable(true)]
		[Localizable(false)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the label of the tree node.</summary>
		/// <returns>The text displayed in the label of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0006BA33 File Offset: 0x00069C33
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x0006BA4C File Offset: 0x00069C4C
		[Localizable(true)]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text;
			}
			set
			{
				if (this.text == value)
				{
					return;
				}
				this.text = value;
				this.Invalidate();
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.OnUIANodeTextChanged(new TreeViewEventArgs(this));
				}
			}
		}

		/// <summary>Gets or sets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>Gets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0006BA8B File Offset: 0x00069C8B
		[DefaultValue("")]
		[Localizable(false)]
		public string ToolTipText
		{
			get
			{
				return this.tool_tip_text;
			}
		}

		/// <summary>Gets the parent tree view that the tree node is assigned to.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeView" /> that represents the parent tree view that the tree node is assigned to, or null if the node has not been assigned to a tree view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0006BA94 File Offset: 0x00069C94
		[Browsable(false)]
		public TreeView TreeView
		{
			get
			{
				if (this.tree_view != null)
				{
					return this.tree_view;
				}
				TreeNode treeNode = this.parent;
				while (treeNode != null && treeNode.TreeView == null)
				{
					treeNode = treeNode.parent;
				}
				if (treeNode == null)
				{
					return null;
				}
				return treeNode.TreeView;
			}
		}

		/// <summary>Initiates the editing of the tree node label.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.TreeView.LabelEdit" /> is set to false. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001571 RID: 5489 RVA: 0x0006BAD8 File Offset: 0x00069CD8
		public void BeginEdit()
		{
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				treeView.BeginEdit(this);
			}
		}

		/// <summary>Collapses the tree node.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001572 RID: 5490 RVA: 0x0006BAF6 File Offset: 0x00069CF6
		public void Collapse()
		{
			this.CollapseInternal(false);
		}

		/// <summary>Ends the editing of the tree node label.</summary>
		/// <param name="cancel">true if the editing of the tree node label text was canceled without being saved; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001573 RID: 5491 RVA: 0x0006BB00 File Offset: 0x00069D00
		public void EndEdit(bool cancel)
		{
			TreeView treeView = this.TreeView;
			if (!cancel && treeView != null)
			{
				treeView.EndEdit(this);
				return;
			}
			if (cancel && treeView != null)
			{
				treeView.CancelEdit(this);
			}
		}

		/// <summary>Expands the tree node.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001574 RID: 5492 RVA: 0x0006BB2F File Offset: 0x00069D2F
		public void Expand()
		{
			this.Expand(false);
		}

		/// <summary>Expands all the child tree nodes.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001575 RID: 5493 RVA: 0x0006BB38 File Offset: 0x00069D38
		public void ExpandAll()
		{
			this.ExpandRecursive(this);
			if (this.TreeView != null)
			{
				this.TreeView.UpdateNode(this.TreeView.root_node);
			}
		}

		/// <summary>Ensures that the tree node is visible, expanding tree nodes and scrolling the tree view control as necessary.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001576 RID: 5494 RVA: 0x0006BB60 File Offset: 0x00069D60
		public void EnsureVisible()
		{
			if (this.TreeView == null)
			{
				return;
			}
			if (this.Parent != null)
			{
				this.ExpandParentRecursive(this.Parent);
			}
			Rectangle bounds = this.Bounds;
			if (bounds.Y < 0)
			{
				this.TreeView.SetTop(this);
				return;
			}
			if (bounds.Bottom > this.TreeView.ViewportRectangle.Bottom)
			{
				this.TreeView.SetBottom(this);
			}
		}

		/// <summary>Toggles the tree node to either the expanded or collapsed state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001577 RID: 5495 RVA: 0x0006BBD0 File Offset: 0x00069DD0
		public void Toggle()
		{
			if (this.is_expanded)
			{
				this.Collapse();
				return;
			}
			this.Expand();
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001578 RID: 5496 RVA: 0x0006BBE7 File Offset: 0x00069DE7
		public override string ToString()
		{
			return "TreeNode: " + this.Text;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0006BBFC File Offset: 0x00069DFC
		internal bool ArePreviousNodesExpanded
		{
			get
			{
				for (TreeNode treeNode = this.Parent; treeNode != null; treeNode = treeNode.Parent)
				{
					if (!treeNode.is_expanded)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006BC28 File Offset: 0x00069E28
		internal bool IsRoot
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.root_node == this;
			}
		}

		/// <summary>Gets the position of the tree node in the tree node collection.</summary>
		/// <returns>A zero-based index value that represents the position of the tree node in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0006BC4D File Offset: 0x00069E4D
		public int Index
		{
			get
			{
				if (this.parent == null)
				{
					return 0;
				}
				return this.parent.Nodes.IndexOf(this);
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0006BC6C File Offset: 0x00069E6C
		private void Expand(bool byInternal)
		{
			if (this.is_expanded || this.nodes.Count < 1)
			{
				this.is_expanded = true;
				return;
			}
			bool flag = false;
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Expand);
				treeView.OnBeforeExpand(treeViewCancelEventArgs);
				flag = treeViewCancelEventArgs.Cancel;
			}
			if (!flag)
			{
				this.is_expanded = true;
				int num = this.CountToNext();
				if (treeView != null)
				{
					treeView.OnAfterExpand(new TreeViewEventArgs(this));
					treeView.RecalculateVisibleOrder(this);
					treeView.UpdateScrollBars(false);
					if (this.visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && this.ArePreviousNodesExpanded)
					{
						treeView.ExpandBelow(this, num);
					}
				}
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006BD10 File Offset: 0x00069F10
		private void CollapseInternal(bool byInternal)
		{
			if (!this.is_expanded || this.nodes.Count < 1)
			{
				return;
			}
			if (this.IsRoot)
			{
				return;
			}
			bool flag = false;
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Collapse);
				treeView.OnBeforeCollapse(treeViewCancelEventArgs);
				flag = treeViewCancelEventArgs.Cancel;
			}
			if (!flag)
			{
				int num = this.CountToNext();
				this.is_expanded = false;
				if (treeView != null)
				{
					treeView.OnAfterCollapse(new TreeViewEventArgs(this));
					bool visible = treeView.hbar.Visible;
					bool visible2 = treeView.vbar.Visible;
					treeView.RecalculateVisibleOrder(this);
					treeView.UpdateScrollBars(false);
					if (this.visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && this.ArePreviousNodesExpanded)
					{
						treeView.CollapseBelow(this, num);
					}
					if (!byInternal && this.HasFocusInChildren())
					{
						treeView.SelectedNode = this;
					}
					if ((visible & !treeView.hbar.Visible) || (visible2 & !treeView.vbar.Visible))
					{
						treeView.Invalidate();
					}
				}
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0006BE10 File Offset: 0x0006A010
		private int CountToNext()
		{
			bool flag = this.is_expanded;
			this.is_expanded = false;
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
			TreeNode treeNode = null;
			if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
			{
				treeNode = openTreeNodeEnumerator.CurrentNode;
			}
			this.is_expanded = flag;
			openTreeNodeEnumerator.Reset();
			openTreeNodeEnumerator.MoveNext();
			int num = 0;
			while (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.CurrentNode != treeNode)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0006BE7C File Offset: 0x0006A07C
		private bool HasFocusInChildren()
		{
			if (this.TreeView == null)
			{
				return false;
			}
			foreach (object obj in this.nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode == this.TreeView.SelectedNode)
				{
					return true;
				}
				if (treeNode.HasFocusInChildren())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0006BEFC File Offset: 0x0006A0FC
		private void ExpandRecursive(TreeNode node)
		{
			node.Expand(true);
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.ExpandRecursive(treeNode);
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0006BF5C File Offset: 0x0006A15C
		private void ExpandParentRecursive(TreeNode node)
		{
			node.Expand(true);
			if (node.Parent != null)
			{
				this.ExpandParentRecursive(node.Parent);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006BF79 File Offset: 0x0006A179
		internal void SetNodes(TreeNodeCollection nodes)
		{
			this.nodes = nodes;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0006BF84 File Offset: 0x0006A184
		internal void Invalidate()
		{
			this.width = -1;
			TreeView treeView = this.TreeView;
			if (treeView == null)
			{
				return;
			}
			treeView.UpdateNode(this);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0006BFAA File Offset: 0x0006A1AA
		internal void InvalidateWidth()
		{
			this.width = -1;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0006BFB4 File Offset: 0x0006A1B4
		internal Image StateImage
		{
			get
			{
				if (this.TreeView != null)
				{
					if (this.TreeView.StateImageList == null)
					{
						return null;
					}
					if (this.state_image_index >= 0)
					{
						return this.TreeView.StateImageList.Images[this.state_image_index];
					}
					if (this.state_image_key != string.Empty)
					{
						return this.TreeView.StateImageList.Images[this.state_image_key];
					}
				}
				return null;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0006C02C File Offset: 0x0006A22C
		internal int Image
		{
			get
			{
				if (this.TreeView == null || this.TreeView.ImageList == null)
				{
					return -1;
				}
				if (this.IsSelected)
				{
					if (this.selected_image_index >= 0)
					{
						return this.selected_image_index;
					}
					if (!string.IsNullOrEmpty(this.selected_image_key))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.selected_image_key);
					}
					if (!string.IsNullOrEmpty(this.TreeView.SelectedImageKey))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.TreeView.SelectedImageKey);
					}
					if (this.TreeView.SelectedImageIndex >= 0)
					{
						return this.TreeView.SelectedImageIndex;
					}
				}
				else
				{
					if (this.image_index >= 0)
					{
						return this.image_index;
					}
					if (!string.IsNullOrEmpty(this.image_key))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.image_key);
					}
					if (!string.IsNullOrEmpty(this.TreeView.ImageKey))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.TreeView.ImageKey);
					}
					if (this.TreeView.ImageIndex >= 0)
					{
						return this.TreeView.ImageIndex;
					}
				}
				if (this.TreeView.ImageList.Images.Count > 0)
				{
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x04000CDF RID: 3295
		private TreeView tree_view;

		// Token: 0x04000CE0 RID: 3296
		internal TreeNode parent;

		// Token: 0x04000CE1 RID: 3297
		private string text;

		// Token: 0x04000CE2 RID: 3298
		private int image_index = -1;

		// Token: 0x04000CE3 RID: 3299
		private int selected_image_index = -1;

		// Token: 0x04000CE4 RID: 3300
		private ContextMenu context_menu;

		// Token: 0x04000CE5 RID: 3301
		private ContextMenuStrip context_menu_strip;

		// Token: 0x04000CE6 RID: 3302
		private string image_key = string.Empty;

		// Token: 0x04000CE7 RID: 3303
		private string selected_image_key = string.Empty;

		// Token: 0x04000CE8 RID: 3304
		private int state_image_index = -1;

		// Token: 0x04000CE9 RID: 3305
		private string state_image_key = string.Empty;

		// Token: 0x04000CEA RID: 3306
		private string tool_tip_text = string.Empty;

		// Token: 0x04000CEB RID: 3307
		internal TreeNodeCollection nodes;

		// Token: 0x04000CEC RID: 3308
		internal TreeViewAction check_reason;

		// Token: 0x04000CED RID: 3309
		internal int visible_order;

		// Token: 0x04000CEE RID: 3310
		internal int width = -1;

		// Token: 0x04000CEF RID: 3311
		internal bool is_expanded;

		// Token: 0x04000CF0 RID: 3312
		private bool check;

		// Token: 0x04000CF1 RID: 3313
		internal OwnerDrawPropertyBag prop_bag;

		// Token: 0x04000CF2 RID: 3314
		private object tag;

		// Token: 0x04000CF3 RID: 3315
		private string name = string.Empty;
	}
}
