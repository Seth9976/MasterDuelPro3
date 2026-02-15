using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents an item in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011A RID: 282
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[TypeConverter(typeof(ListViewItemConverter))]
	[Serializable]
	public class ListViewItem : ICloneable, ISerializable
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x0002F01B File Offset: 0x0002D21B
		internal void OnUIATextChanged()
		{
			if (this.UIATextChanged != null)
			{
				this.UIATextChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002F036 File Offset: 0x0002D236
		internal void OnUIASubItemTextChanged(LabelEditEventArgs args)
		{
			if (args.Item == 0)
			{
				this.OnUIATextChanged();
			}
			if (this.UIASubItemTextChanged != null)
			{
				this.UIASubItemTextChanged(this, args);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with default values.</summary>
		// Token: 0x06000B10 RID: 2832 RVA: 0x0002F05B File Offset: 0x0002D25B
		public ListViewItem()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		// Token: 0x06000B11 RID: 2833 RVA: 0x0002F068 File Offset: 0x0002D268
		public ListViewItem(string text)
			: this(text, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		// Token: 0x06000B12 RID: 2834 RVA: 0x0002F074 File Offset: 0x0002D274
		public ListViewItem(string text, int imageIndex)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.image_index = imageIndex;
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing information about the <see cref="T:System.Windows.Forms.ListViewItem" /> to be initialized.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source destination and context information of a serialized stream.</param>
		// Token: 0x06000B13 RID: 2835 RVA: 0x0002F0F0 File Offset: 0x0002D2F0
		protected ListViewItem(SerializationInfo info, StreamingContext context)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.Deserialize(info, context);
		}

		/// <summary>Gets or sets the background color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002F160 File Offset: 0x0002D360
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0002F1A0 File Offset: 0x0002D3A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color BackColor
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].BackColor;
				}
				if (this.owner != null)
				{
					return this.owner.BackColor;
				}
				return ThemeEngine.Current.ColorWindow;
			}
			set
			{
				this.SubItems[0].BackColor = value;
			}
		}

		/// <summary>Gets the bounding rectangle of the item, including subitems.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				return this.GetBounds(ItemBoundsPortion.Entire);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002F1BD File Offset: 0x0002D3BD
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0002F1C8 File Offset: 0x0002D3C8
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool Checked
		{
			get
			{
				return this.is_checked;
			}
			set
			{
				if (this.is_checked == value)
				{
					return;
				}
				if (this.owner != null)
				{
					CheckState checkState = (this.is_checked ? CheckState.Checked : CheckState.Unchecked);
					CheckState checkState2 = (value ? CheckState.Checked : CheckState.Unchecked);
					ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(this.Index, checkState2, checkState);
					this.owner.OnItemCheck(itemCheckEventArgs);
					if (checkState2 != checkState)
					{
						this.owner.CheckedItems.Reset();
						this.is_checked = checkState2 == CheckState.Checked;
						this.Invalidate();
						ItemCheckedEventArgs itemCheckedEventArgs = new ItemCheckedEventArgs(this);
						this.owner.OnItemChecked(itemCheckedEventArgs);
						return;
					}
				}
				else
				{
					this.is_checked = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the item has focus within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>true if the item has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002F255 File Offset: 0x0002D455
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0002F290 File Offset: 0x0002D490
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Focused
		{
			get
			{
				if (this.owner == null)
				{
					return false;
				}
				if (this.owner.VirtualMode)
				{
					return this.Index == this.owner.focused_item_index;
				}
				return this.owner.FocusedItem == this;
			}
			set
			{
				if (this.owner == null)
				{
					return;
				}
				if (this.Focused == value)
				{
					return;
				}
				ListViewItem focusedItem = this.owner.FocusedItem;
				if (focusedItem != null)
				{
					focusedItem.UpdateFocusedState();
				}
				this.owner.focused_item_index = (value ? this.Index : (-1));
				if (value)
				{
					this.owner.OnUIAFocusedItemChanged();
				}
				this.UpdateFocusedState();
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property if the <see cref="T:System.Windows.Forms.ListViewItem" /> is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, the font specified in the <see cref="P:System.Windows.Forms.Control.Font" /> property for the <see cref="T:System.Windows.Forms.ListView" /> control is used.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font Font
		{
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.owner != null)
				{
					return this.owner.Font;
				}
				return ThemeEngine.Current.DefaultFont;
			}
		}

		/// <summary>Gets or sets the foreground color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002F31F File Offset: 0x0002D51F
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0002F35F File Offset: 0x0002D55F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ForeColor
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].ForeColor;
				}
				if (this.owner != null)
				{
					return this.owner.ForeColor;
				}
				return ThemeEngine.Current.ColorWindowText;
			}
			set
			{
				this.SubItems[0].ForeColor = value;
			}
		}

		/// <summary>Gets or sets the index of the image that is displayed for the item.</summary>
		/// <returns>The zero-based index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002F373 File Offset: 0x0002D573
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002F37B File Offset: 0x0002D57B
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("Invalid ImageIndex. It must be greater than or equal to -1.");
				}
				this.image_index = value;
				this.image_key = string.Empty;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the key for the image that is displayed for the item.</summary>
		/// <returns>The key for the image that is displayed for the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002F3B2 File Offset: 0x0002D5B2
		[DefaultValue("")]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
		}

		/// <summary>Gets or sets the number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When setting <see cref="P:System.Windows.Forms.ListViewItem.IndentCount" />, the number specified is less than 0.</exception>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002F3BA File Offset: 0x0002D5BA
		[DefaultValue(0)]
		public int IndentCount
		{
			get
			{
				return this.indent_count;
			}
		}

		/// <summary>Gets the zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control, or -1 if the item is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.owner == null)
				{
					return -1;
				}
				if (this.owner.VirtualMode)
				{
					return this.display_index;
				}
				if (this.display_index == -1)
				{
					return this.owner.Items.IndexOf(this);
				}
				return this.owner.GetItemIndex(this.display_index);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> that contains the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002F41B File Offset: 0x0002D61B
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
		/// <returns>true if the item is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002F423 File Offset: 0x0002D623
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002F457 File Offset: 0x0002D657
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					return this.owner.SelectedIndices.Contains(this.Index);
				}
				return this.selected;
			}
			set
			{
				if (this.selected == value && this.owner != null && !this.owner.VirtualMode)
				{
					return;
				}
				this.SetSelectedCore(value);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002F480 File Offset: 0x0002D680
		internal void SetSelectedCore(bool value)
		{
			if (this.owner != null)
			{
				if (value && !this.owner.MultiSelect)
				{
					this.owner.SelectedIndices.Clear();
				}
				if (this.owner.VirtualMode)
				{
					if (value)
					{
						this.owner.SelectedIndices.InsertIndex(this.Index);
					}
					else
					{
						this.owner.SelectedIndices.RemoveIndex(this.Index);
					}
				}
				else
				{
					this.selected = value;
					this.owner.SelectedIndices.Reset();
				}
				this.owner.OnItemSelectionChanged(new ListViewItemSelectionChangedEventArgs(this, this.Index, value));
				this.owner.OnSelectedIndexChanged();
				this.Invalidate();
				return;
			}
			this.selected = value;
		}

		/// <summary>Gets a collection containing all subitems of the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> that contains the subitems.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002F540 File Offset: 0x0002D740
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ListViewItem.ListViewSubItemCollection SubItems
		{
			get
			{
				if (this.sub_items.Count == 0)
				{
					this.sub_items.Add(string.Empty);
				}
				return this.sub_items;
			}
		}

		/// <summary>Gets or sets the text of the item.</summary>
		/// <returns>The text to display for the item. This should not exceed 259 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002F566 File Offset: 0x0002D766
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0002F590 File Offset: 0x0002D790
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Text
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].Text;
				}
				return string.Empty;
			}
			set
			{
				if (this.SubItems[0].Text == value)
				{
					return;
				}
				this.sub_items[0].Text = value;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
				this.OnUIATextChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ListViewItem.Font" />, <see cref="P:System.Windows.Forms.ListViewItem.ForeColor" />, and <see cref="P:System.Windows.Forms.ListViewItem.BackColor" /> properties for the item are used for all its subitems.</summary>
		/// <returns>true if all subitems use the font, foreground color, and background color settings of the item; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002F5E3 File Offset: 0x0002D7E3
		[DefaultValue(true)]
		public bool UseItemStyleForSubItems
		{
			get
			{
				return this.use_item_style;
			}
		}

		/// <summary>Gets or sets the group to which the item is assigned.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> to which the item is assigned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002F5EB File Offset: 0x0002D7EB
		[Localizable(true)]
		[DefaultValue(null)]
		public ListViewGroup Group
		{
			get
			{
				return this.group;
			}
		}

		/// <summary>Gets or sets the text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002F5F3 File Offset: 0x0002D7F3
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tooltip_text;
			}
		}

		/// <summary>Creates an identical copy of the item.</summary>
		/// <returns>An object that represents an item that has the same text, image, and subitems associated with it as the cloned item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B2D RID: 2861 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		public virtual object Clone()
		{
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.image_index = this.image_index;
			listViewItem.is_checked = this.is_checked;
			listViewItem.selected = this.selected;
			listViewItem.font = this.font;
			listViewItem.state_image_index = this.state_image_index;
			listViewItem.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			foreach (object obj in this.sub_items)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
				listViewItem.sub_items.Add(listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font);
			}
			listViewItem.tag = this.tag;
			listViewItem.use_item_style = this.use_item_style;
			listViewItem.owner = null;
			listViewItem.name = this.name;
			listViewItem.tooltip_text = this.tooltip_text;
			return listViewItem;
		}

		/// <summary>Ensures that the item is visible within the control, scrolling the contents of the control, if necessary.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B2E RID: 2862 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		public virtual void EnsureVisible()
		{
			if (this.owner != null)
			{
				this.owner.EnsureVisible(this.owner.Items.IndexOf(this));
			}
		}

		/// <summary>Retrieves the specified portion of the bounding rectangle for the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the item.</returns>
		/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the item for which to retrieve the bounding rectangle. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B2F RID: 2863 RVA: 0x0002F724 File Offset: 0x0002D924
		public Rectangle GetBounds(ItemBoundsPortion portion)
		{
			if (this.owner == null)
			{
				return Rectangle.Empty;
			}
			Rectangle rectangle;
			switch (portion)
			{
			case ItemBoundsPortion.Entire:
				rectangle = this.bounds;
				break;
			case ItemBoundsPortion.Icon:
				rectangle = this.icon_rect;
				break;
			case ItemBoundsPortion.Label:
				rectangle = this.label_rect;
				break;
			case ItemBoundsPortion.ItemOnly:
				rectangle = this.item_rect;
				break;
			default:
				throw new ArgumentException("Invalid value for portion.");
			}
			Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
			rectangle.X += itemLocation.X;
			rectangle.Y += itemLocation.Y;
			return rectangle;
		}

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item.  </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized.</param>
		// Token: 0x06000B30 RID: 2864 RVA: 0x0002F7C3 File Offset: 0x0002D9C3
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B31 RID: 2865 RVA: 0x0002F7CD File Offset: 0x0002D9CD
		public override string ToString()
		{
			return string.Format("ListViewItem: {0}", this.Text);
		}

		/// <summary>Deserializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to deserialize the item. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being deserialized. </param>
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002F7E0 File Offset: 0x0002D9E0
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			int num = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				string text = serializationEntry.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num2 <= 1388626601U)
				{
					if (num2 <= 1041509726U)
					{
						if (num2 != 91525164U)
						{
							if (num2 == 1041509726U)
							{
								if (text == "Text")
								{
									this.sub_items.Add((string)serializationEntry.Value);
								}
							}
						}
						else if (text == "Group")
						{
							this.group = (ListViewGroup)serializationEntry.Value;
						}
					}
					else if (num2 != 1371155046U)
					{
						if (num2 == 1388626601U)
						{
							if (text == "UseItemStyleForSubItems")
							{
								this.use_item_style = (bool)serializationEntry.Value;
							}
						}
					}
					else if (text == "Checked")
					{
						this.is_checked = (bool)serializationEntry.Value;
					}
				}
				else if (num2 <= 2041341998U)
				{
					if (num2 != 1606954993U)
					{
						if (num2 == 2041341998U)
						{
							if (text == "ImageIndex")
							{
								this.image_index = (int)serializationEntry.Value;
							}
						}
					}
					else if (text == "ImageKey")
					{
						if (this.image_index == -1)
						{
							this.image_key = (string)serializationEntry.Value;
						}
					}
				}
				else if (num2 != 2143661137U)
				{
					if (num2 != 2610712773U)
					{
						if (num2 == 2809814704U)
						{
							if (text == "Font")
							{
								this.font = (Font)serializationEntry.Value;
							}
						}
					}
					else if (text == "SubItemCount")
					{
						num = (int)serializationEntry.Value;
					}
				}
				else if (text == "StateImageIndex")
				{
					this.state_image_index = (int)serializationEntry.Value;
				}
			}
			Type typeFromHandle = typeof(ListViewItem.ListViewSubItem);
			if (num > 0)
			{
				this.sub_items.Clear();
				this.Text = info.GetString("Text");
				for (int i = 0; i < num - 1; i++)
				{
					this.sub_items.Add((ListViewItem.ListViewSubItem)info.GetValue("SubItem" + (i + 1), typeFromHandle));
				}
			}
			this.ForeColor = (Color)info.GetValue("ForeColor", typeof(Color));
			this.BackColor = (Color)info.GetValue("BackColor", typeof(Color));
		}

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized. </param>
		// Token: 0x06000B33 RID: 2867 RVA: 0x0002FAEC File Offset: 0x0002DCEC
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Text", this.Text);
			info.AddValue("Font", this.Font);
			info.AddValue("ImageIndex", this.image_index);
			info.AddValue("Checked", this.is_checked);
			info.AddValue("StateImageIndex", this.state_image_index);
			info.AddValue("UseItemStyleForSubItems", this.use_item_style);
			info.AddValue("BackColor", this.BackColor);
			info.AddValue("ForeColor", this.ForeColor);
			info.AddValue("ImageKey", this.image_key);
			if (this.group != null)
			{
				info.AddValue("Group", this.group);
			}
			if (this.sub_items.Count > 1)
			{
				info.AddValue("SubItemCount", this.sub_items.Count);
				for (int i = 1; i < this.sub_items.Count; i++)
				{
					info.AddValue("SubItem" + i, this.sub_items[i]);
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002FC14 File Offset: 0x0002DE14
		internal Rectangle CheckRectReal
		{
			get
			{
				Rectangle rectangle = this.checkbox_rect;
				Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
				rectangle.X += itemLocation.X;
				rectangle.Y += itemLocation.Y;
				return rectangle;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0002FC68 File Offset: 0x0002DE68
		internal Rectangle TextBounds
		{
			get
			{
				if (this.owner.VirtualMode && this.bounds == new Rectangle(-1, -1, -1, -1))
				{
					this.Layout();
				}
				Rectangle rectangle = this.text_bounds;
				Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
				rectangle.X += itemLocation.X;
				rectangle.Y += itemLocation.Y;
				return rectangle;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0002FCE2 File Offset: 0x0002DEE2
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0002FD05 File Offset: 0x0002DF05
		internal int DisplayIndex
		{
			get
			{
				if (this.display_index == -1)
				{
					return this.owner.Items.IndexOf(this);
				}
				return this.display_index;
			}
			set
			{
				this.display_index = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0002FD0E File Offset: 0x0002DF0E
		internal bool Hot
		{
			get
			{
				return this.Index == this.owner.HotItemIndex;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002FD23 File Offset: 0x0002DF23
		internal Font HotFont
		{
			get
			{
				if (this.hot_font == null)
				{
					this.hot_font = new Font(this.Font, this.Font.Style | FontStyle.Underline);
				}
				return this.hot_font;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0002FD51 File Offset: 0x0002DF51
		internal ListView Owner
		{
			set
			{
				if (this.owner == value)
				{
					return;
				}
				this.owner = value;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002FD64 File Offset: 0x0002DF64
		internal void SetGroup(ListViewGroup group)
		{
			this.group = group;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002FD6D File Offset: 0x0002DF6D
		internal void SetPosition(Point position)
		{
			this.position = position;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002FD76 File Offset: 0x0002DF76
		private void UpdateFocusedState()
		{
			if (this.owner != null)
			{
				this.Invalidate();
				this.Layout();
				this.Invalidate();
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002FD94 File Offset: 0x0002DF94
		internal void Invalidate()
		{
			if (this.owner == null || this.owner.item_control == null || this.owner.updating)
			{
				return;
			}
			Rectangle rectangle = this.Bounds;
			rectangle.Inflate(1, 1);
			this.owner.item_control.Invalidate(rectangle);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		internal void Layout()
		{
			if (this.owner == null)
			{
				return;
			}
			Size text_size = this.owner.text_size;
			this.checkbox_rect = Rectangle.Empty;
			if (this.owner.CheckBoxes)
			{
				this.checkbox_rect.Size = this.owner.CheckBoxSize;
			}
			switch (this.owner.View)
			{
			case View.LargeIcon:
				break;
			case View.Details:
			{
				int num = 0;
				if (this.owner.SmallImageList != null)
				{
					num = this.indent_count * this.owner.SmallImageList.ImageSize.Width;
				}
				if (this.owner.Columns.Count > 0)
				{
					this.checkbox_rect.X = this.owner.Columns[0].Rect.X + num;
				}
				this.icon_rect = (this.label_rect = Rectangle.Empty);
				this.icon_rect.X = this.checkbox_rect.Right + 2;
				int num2 = this.owner.ItemSize.Height;
				if (this.owner.SmallImageList != null)
				{
					this.icon_rect.Width = this.owner.SmallImageList.ImageSize.Width;
				}
				this.label_rect.Height = (this.icon_rect.Height = num2);
				this.checkbox_rect.Y = num2 - this.checkbox_rect.Height;
				this.label_rect.X = ((this.icon_rect.Width > 0) ? (this.icon_rect.Right + 1) : this.icon_rect.Right);
				if (this.owner.Columns.Count > 0)
				{
					this.label_rect.Width = this.owner.Columns[0].Wd - this.label_rect.X + this.checkbox_rect.X;
				}
				else
				{
					this.label_rect.Width = text_size.Width;
				}
				SizeF sizeF = TextRenderer.MeasureString(this.Text, this.Font);
				this.text_bounds = this.label_rect;
				this.text_bounds.Width = (int)sizeF.Width;
				Rectangle rectangle = (this.item_rect = Rectangle.Union(Rectangle.Union(this.checkbox_rect, this.icon_rect), this.label_rect));
				this.bounds.Size = rectangle.Size;
				this.item_rect.Width = 0;
				this.bounds.Width = 0;
				for (int i = 0; i < this.owner.Columns.Count; i++)
				{
					this.item_rect.Width = this.item_rect.Width + this.owner.Columns[i].Wd;
					this.bounds.Width = this.bounds.Width + this.owner.Columns[i].Wd;
				}
				int num3 = Math.Min(this.owner.Columns.Count, this.sub_items.Count);
				for (int j = 0; j < num3; j++)
				{
					Rectangle rect = this.owner.Columns[j].Rect;
					this.sub_items[j].SetBounds(rect.X, 0, rect.Width, num2);
				}
				return;
			}
			case View.SmallIcon:
			case View.List:
			{
				this.label_rect = (this.icon_rect = Rectangle.Empty);
				this.icon_rect.X = this.checkbox_rect.Width + 1;
				int num2 = Math.Max(this.owner.CheckBoxSize.Height, text_size.Height);
				if (this.owner.SmallImageList != null)
				{
					num2 = Math.Max(num2, this.owner.SmallImageList.ImageSize.Height);
					this.icon_rect.Width = this.owner.SmallImageList.ImageSize.Width;
					this.icon_rect.Height = this.owner.SmallImageList.ImageSize.Height;
				}
				this.checkbox_rect.Y = num2 - this.checkbox_rect.Height;
				this.label_rect.X = this.icon_rect.Right + 1;
				this.label_rect.Width = text_size.Width;
				this.label_rect.Height = (this.icon_rect.Height = num2);
				this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
				this.bounds.Size = Rectangle.Union(this.item_rect, this.checkbox_rect).Size;
				return;
			}
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.label_rect = (this.icon_rect = Rectangle.Empty);
					if (this.owner.LargeImageList != null)
					{
						this.icon_rect.Width = this.owner.LargeImageList.ImageSize.Width;
						this.icon_rect.Height = this.owner.LargeImageList.ImageSize.Height;
					}
					int num4 = 2;
					SizeF sizeF2 = TextRenderer.MeasureString(this.Text, this.Font);
					int num5 = (int)Math.Ceiling((double)sizeF2.Height);
					int num6 = (int)Math.Ceiling((double)sizeF2.Width);
					this.sub_items[0].bounds.Height = num5;
					int num7 = num5;
					int num8 = num6;
					int num9 = Math.Min(this.owner.Columns.Count, this.sub_items.Count);
					for (int k = 1; k < num9; k++)
					{
						ListViewItem.ListViewSubItem listViewSubItem = this.sub_items[k];
						if (listViewSubItem.Text != null && listViewSubItem.Text.Length != 0)
						{
							sizeF2 = TextRenderer.MeasureString(listViewSubItem.Text, listViewSubItem.Font);
							int num10 = (int)Math.Ceiling((double)sizeF2.Width);
							if (num10 > num8)
							{
								num8 = num10;
							}
							int num11 = (int)Math.Ceiling((double)sizeF2.Height);
							num7 += num11 + num4;
							listViewSubItem.bounds.Height = num11;
						}
					}
					num8 = Math.Min(num8, this.owner.TileSize.Width - (this.icon_rect.Width + 4));
					this.label_rect.X = this.icon_rect.Right + 4;
					this.label_rect.Y = this.owner.TileSize.Height / 2 - num7 / 2;
					this.label_rect.Width = num8;
					this.label_rect.Height = num7;
					this.sub_items[0].SetBounds(this.label_rect.X, this.label_rect.Y, num8, this.sub_items[0].bounds.Height);
					int num12 = this.sub_items[0].bounds.Bottom + num4;
					for (int l = 1; l < num9; l++)
					{
						ListViewItem.ListViewSubItem listViewSubItem2 = this.sub_items[l];
						if (listViewSubItem2.Text != null && listViewSubItem2.Text.Length != 0)
						{
							listViewSubItem2.SetBounds(this.label_rect.X, num12, num8, listViewSubItem2.bounds.Height);
							num12 += listViewSubItem2.Bounds.Height + num4;
						}
					}
					this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
					this.bounds.Size = this.item_rect.Size;
					return;
				}
				break;
			default:
				return;
			}
			this.label_rect = (this.icon_rect = Rectangle.Empty);
			SizeF sizeF3 = TextRenderer.MeasureString(this.Text, this.Font);
			if ((int)sizeF3.Width > text_size.Width)
			{
				if (this.Focused && this.owner.InternalContainsFocus)
				{
					int width = text_size.Width;
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = StringAlignment.Center;
					text_size.Height = (int)TextRenderer.MeasureString(this.Text, this.Font, width, stringFormat).Height;
				}
				else
				{
					text_size.Height = 2 * (int)sizeF3.Height;
				}
			}
			if (this.owner.LargeImageList != null)
			{
				this.icon_rect.Width = this.owner.LargeImageList.ImageSize.Width;
				this.icon_rect.Height = this.owner.LargeImageList.ImageSize.Height;
			}
			if (this.checkbox_rect.Height > this.icon_rect.Height)
			{
				this.icon_rect.Y = this.checkbox_rect.Height - this.icon_rect.Height;
			}
			else
			{
				this.checkbox_rect.Y = this.icon_rect.Height - this.checkbox_rect.Height;
			}
			if (text_size.Width <= this.icon_rect.Width)
			{
				this.icon_rect.X = this.checkbox_rect.Width + 1;
				this.label_rect.X = this.icon_rect.X + (this.icon_rect.Width - text_size.Width) / 2;
				this.label_rect.Y = this.icon_rect.Bottom + 2;
				this.label_rect.Size = text_size;
			}
			else
			{
				int num13 = text_size.Width / 2;
				this.icon_rect.X = this.checkbox_rect.Width + 1 + num13 - this.icon_rect.Width / 2;
				this.label_rect.X = this.checkbox_rect.Width + 1;
				this.label_rect.Y = this.icon_rect.Bottom + 2;
				this.label_rect.Size = text_size;
			}
			this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
			this.bounds.Size = Rectangle.Union(this.item_rect, this.checkbox_rect).Size;
		}

		// Token: 0x04000743 RID: 1859
		private int image_index;

		// Token: 0x04000744 RID: 1860
		private bool is_checked;

		// Token: 0x04000745 RID: 1861
		private int state_image_index;

		// Token: 0x04000746 RID: 1862
		private ListViewItem.ListViewSubItemCollection sub_items;

		// Token: 0x04000747 RID: 1863
		private object tag;

		// Token: 0x04000748 RID: 1864
		private bool use_item_style;

		// Token: 0x04000749 RID: 1865
		private int display_index;

		// Token: 0x0400074A RID: 1866
		private ListViewGroup group;

		// Token: 0x0400074B RID: 1867
		private string name;

		// Token: 0x0400074C RID: 1868
		private string image_key;

		// Token: 0x0400074D RID: 1869
		private string tooltip_text;

		// Token: 0x0400074E RID: 1870
		private int indent_count;

		// Token: 0x0400074F RID: 1871
		private Point position;

		// Token: 0x04000750 RID: 1872
		private Rectangle bounds;

		// Token: 0x04000751 RID: 1873
		private Rectangle checkbox_rect;

		// Token: 0x04000752 RID: 1874
		private Rectangle icon_rect;

		// Token: 0x04000753 RID: 1875
		private Rectangle item_rect;

		// Token: 0x04000754 RID: 1876
		private Rectangle label_rect;

		// Token: 0x04000755 RID: 1877
		private ListView owner;

		// Token: 0x04000756 RID: 1878
		private Font font;

		// Token: 0x04000757 RID: 1879
		private Font hot_font;

		// Token: 0x04000758 RID: 1880
		private bool selected;

		// Token: 0x04000759 RID: 1881
		[CompilerGenerated]
		private EventHandler UIATextChanged;

		// Token: 0x0400075A RID: 1882
		[CompilerGenerated]
		private LabelEditEventHandler UIASubItemTextChanged;

		// Token: 0x0400075B RID: 1883
		private Rectangle text_bounds;

		/// <summary>Represents a subitem of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x0200011B RID: 283
		[DefaultProperty("Text")]
		[DesignTimeVisible(false)]
		[ToolboxItem(false)]
		[TypeConverter(typeof(ListViewSubItemConverter))]
		[Serializable]
		public class ListViewSubItem
		{
			// Token: 0x14000038 RID: 56
			// (add) Token: 0x06000B40 RID: 2880 RVA: 0x00030840 File Offset: 0x0002EA40
			// (remove) Token: 0x06000B41 RID: 2881 RVA: 0x00030878 File Offset: 0x0002EA78
			[field: NonSerialized]
			internal event EventHandler UIATextChanged;

			// Token: 0x06000B42 RID: 2882 RVA: 0x000308AD File Offset: 0x0002EAAD
			private void OnUIATextChanged()
			{
				if (this.UIATextChanged != null)
				{
					this.UIATextChanged(this, EventArgs.Empty);
				}
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with default values.</summary>
			// Token: 0x06000B43 RID: 2883 RVA: 0x000308C8 File Offset: 0x0002EAC8
			public ListViewSubItem()
				: this(null, string.Empty, Color.Empty, Color.Empty, null)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner and text.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
			/// <param name="text">The text to display for the subitem. </param>
			// Token: 0x06000B44 RID: 2884 RVA: 0x000308E1 File Offset: 0x0002EAE1
			public ListViewSubItem(ListViewItem owner, string text)
				: this(owner, text, Color.Empty, Color.Empty, null)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner, text, foreground color, background color, and font values.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
			/// <param name="text">The text to display for the subitem. </param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the subitem's text in. </param>
			// Token: 0x06000B45 RID: 2885 RVA: 0x000308F6 File Offset: 0x0002EAF6
			public ListViewSubItem(ListViewItem owner, string text, Color foreColor, Color backColor, Font font)
			{
				this.owner = owner;
				this.Text = text;
				this.style = new ListViewItem.ListViewSubItem.SubItemStyle(foreColor, backColor, font);
			}

			/// <summary>Gets or sets the background color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem's text.</returns>
			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00030928 File Offset: 0x0002EB28
			// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00030988 File Offset: 0x0002EB88
			public Color BackColor
			{
				get
				{
					if (this.style.backColor != Color.Empty)
					{
						return this.style.backColor;
					}
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.BackColor;
					}
					return ThemeEngine.Current.ColorWindow;
				}
				set
				{
					this.style.backColor = value;
					this.Invalidate();
				}
			}

			/// <summary>Gets the bounding rectangle of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0003099C File Offset: 0x0002EB9C
			[Browsable(false)]
			public Rectangle Bounds
			{
				get
				{
					Rectangle rectangle = this.bounds;
					if (this.owner != null)
					{
						rectangle.X += this.owner.Bounds.X;
						rectangle.Y += this.owner.Bounds.Y;
					}
					return rectangle;
				}
			}

			/// <summary>Gets or sets the font of the text displayed by the subitem.</summary>
			/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000B49 RID: 2889 RVA: 0x000309FB File Offset: 0x0002EBFB
			[Localizable(true)]
			public Font Font
			{
				get
				{
					if (this.style.font != null)
					{
						return this.style.font;
					}
					if (this.owner != null)
					{
						return this.owner.Font;
					}
					return ThemeEngine.Current.DefaultFont;
				}
			}

			/// <summary>Gets or sets the foreground color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem's text.</returns>
			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00030A34 File Offset: 0x0002EC34
			// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00030A94 File Offset: 0x0002EC94
			public Color ForeColor
			{
				get
				{
					if (this.style.foreColor != Color.Empty)
					{
						return this.style.foreColor;
					}
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.ForeColor;
					}
					return ThemeEngine.Current.ColorWindowText;
				}
				set
				{
					this.style.foreColor = value;
					this.Invalidate();
				}
			}

			/// <summary>Gets or sets the text of the subitem.</summary>
			/// <returns>The text to display for the subitem.</returns>
			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00030AA8 File Offset: 0x0002ECA8
			// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00030AB0 File Offset: 0x0002ECB0
			[Localizable(true)]
			public string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					if (this.text == value)
					{
						return;
					}
					if (value == null)
					{
						this.text = string.Empty;
					}
					else
					{
						this.text = value;
					}
					this.Invalidate();
					this.OnUIATextChanged();
				}
			}

			/// <returns>A string that represents the current object.</returns>
			// Token: 0x06000B4E RID: 2894 RVA: 0x00030AE4 File Offset: 0x0002ECE4
			public override string ToString()
			{
				return string.Format("ListViewSubItem {{0}}", this.text);
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x00030AF6 File Offset: 0x0002ECF6
			private void Invalidate()
			{
				if (this.owner == null || this.owner.owner == null)
				{
					return;
				}
				this.owner.Invalidate();
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x00030B19 File Offset: 0x0002ED19
			[OnDeserialized]
			private void OnDeserialized(StreamingContext context)
			{
				this.name = null;
				this.userData = null;
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x00030B29 File Offset: 0x0002ED29
			internal void SetBounds(int x, int y, int width, int height)
			{
				this.bounds = new Rectangle(x, y, width, height);
			}

			// Token: 0x0400075C RID: 1884
			[NonSerialized]
			internal ListViewItem owner;

			// Token: 0x0400075D RID: 1885
			private string text = string.Empty;

			// Token: 0x0400075E RID: 1886
			private string name;

			// Token: 0x0400075F RID: 1887
			private object userData;

			// Token: 0x04000760 RID: 1888
			private ListViewItem.ListViewSubItem.SubItemStyle style;

			// Token: 0x04000761 RID: 1889
			[NonSerialized]
			internal Rectangle bounds;

			// Token: 0x0200011C RID: 284
			[Serializable]
			private class SubItemStyle
			{
				// Token: 0x06000B52 RID: 2898 RVA: 0x00002A07 File Offset: 0x00000C07
				public SubItemStyle()
				{
				}

				// Token: 0x06000B53 RID: 2899 RVA: 0x00030B3B File Offset: 0x0002ED3B
				public SubItemStyle(Color foreColor, Color backColor, Font font)
				{
					this.foreColor = foreColor;
					this.backColor = backColor;
					this.font = font;
				}

				// Token: 0x04000763 RID: 1891
				public Color backColor;

				// Token: 0x04000764 RID: 1892
				public Color foreColor;

				// Token: 0x04000765 RID: 1893
				public Font font;
			}
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects stored in a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x0200011D RID: 285
		public class ListViewSubItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000B54 RID: 2900 RVA: 0x00030B58 File Offset: 0x0002ED58
			internal ListViewSubItemCollection(ListViewItem owner, string text)
			{
				this.owner = owner;
				this.list = new ArrayList();
				if (text != null)
				{
					this.Add(text);
				}
			}

			/// <summary>Gets the number of subitems in the collection.</summary>
			/// <returns>The number of subitems in the collection.</returns>
			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00030B7D File Offset: 0x0002ED7D
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the subitem at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x170002F1 RID: 753
			public ListViewItem.ListViewSubItem this[int index]
			{
				get
				{
					return (ListViewItem.ListViewSubItem)this.list[index];
				}
				set
				{
					value.owner = this.owner;
					this.list[index] = value;
					this.owner.Layout();
					this.owner.Invalidate();
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00030BCE File Offset: 0x0002EDCE
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.list.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00030BDB File Offset: 0x0002EDDB
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00030BE8 File Offset: 0x0002EDE8
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			/// <exception cref="T:System.ArgumentException">The object is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x170002F5 RID: 757
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is ListViewItem.ListViewSubItem))
					{
						throw new ArgumentException("Not of type ListViewSubItem", "value");
					}
					this[index] = (ListViewItem.ListViewSubItem)value;
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection. </param>
			// Token: 0x06000B5E RID: 2910 RVA: 0x00030C25 File Offset: 0x0002EE25
			public ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItem item)
			{
				this.AddSubItem(item);
				this.owner.Layout();
				this.owner.Invalidate();
				return item;
			}

			/// <summary>Adds a subitem to the collection with specified text.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="text">The text to display for the subitem. </param>
			// Token: 0x06000B5F RID: 2911 RVA: 0x00030C48 File Offset: 0x0002EE48
			public ListViewItem.ListViewSubItem Add(string text)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text);
				return this.Add(listViewSubItem);
			}

			/// <summary>Adds a subitem to the collection with specified text, foreground color, background color, and font settings.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="text">The text to display for the subitem. </param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in. </param>
			// Token: 0x06000B60 RID: 2912 RVA: 0x00030C6C File Offset: 0x0002EE6C
			public ListViewItem.ListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
				return this.Add(listViewSubItem);
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x00030C91 File Offset: 0x0002EE91
			private void AddSubItem(ListViewItem.ListViewSubItem subItem)
			{
				subItem.owner = this.owner;
				this.list.Add(subItem);
				subItem.UIATextChanged += this.OnUIASubItemTextChanged;
			}

			/// <summary>Removes all subitems and the parent <see cref="T:System.Windows.Forms.ListViewItem" /> from the collection.</summary>
			// Token: 0x06000B62 RID: 2914 RVA: 0x00030CBE File Offset: 0x0002EEBE
			public void Clear()
			{
				this.list.Clear();
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
			// Token: 0x06000B63 RID: 2915 RVA: 0x00030CCB File Offset: 0x0002EECB
			public bool Contains(ListViewItem.ListViewSubItem subItem)
			{
				return this.list.Contains(subItem);
			}

			/// <summary>Returns an enumerator to use to iterate through the subitem collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the subitem collection.</returns>
			// Token: 0x06000B64 RID: 2916 RVA: 0x00030CD9 File Offset: 0x0002EED9
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Copies the item and collection of subitems into an array.</summary>
			/// <param name="dest">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</param>
			/// <param name="index">The zero-based index in array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x06000B65 RID: 2917 RVA: 0x00030CE6 File Offset: 0x0002EEE6
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <returns>The zero-based index that indicates the location of the object that was added to the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x06000B66 RID: 2918 RVA: 0x00030CF8 File Offset: 0x0002EEF8
			int IList.Add(object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)item;
				listViewSubItem.owner = this.owner;
				listViewSubItem.UIATextChanged += this.OnUIASubItemTextChanged;
				return this.list.Add(listViewSubItem);
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			// Token: 0x06000B67 RID: 2919 RVA: 0x00030D4E File Offset: 0x0002EF4E
			bool IList.Contains(object subItem)
			{
				if (!(subItem is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "subItem");
				}
				return this.Contains((ListViewItem.ListViewSubItem)subItem);
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <returns>The zero-based index of the subitem if it is in the collection; otherwise, -1.</returns>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			// Token: 0x06000B68 RID: 2920 RVA: 0x00030D74 File Offset: 0x0002EF74
			int IList.IndexOf(object subItem)
			{
				if (!(subItem is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "subItem");
				}
				return this.IndexOf((ListViewItem.ListViewSubItem)subItem);
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">An object that represents the subitem to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x06000B69 RID: 2921 RVA: 0x00030D9A File Offset: 0x0002EF9A
			void IList.Insert(int index, object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				this.Insert(index, (ListViewItem.ListViewSubItem)item);
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x06000B6A RID: 2922 RVA: 0x00030DC1 File Offset: 0x0002EFC1
			void IList.Remove(object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				this.Remove((ListViewItem.ListViewSubItem)item);
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <returns>The zero-based index of the subitem's location in the collection. If the subitem is not located in the collection, the return value is negative one (-1).</returns>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
			// Token: 0x06000B6B RID: 2923 RVA: 0x00030DE7 File Offset: 0x0002EFE7
			public int IndexOf(ListViewItem.ListViewSubItem subItem)
			{
				return this.list.IndexOf(subItem);
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to insert into the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x06000B6C RID: 2924 RVA: 0x00030DF8 File Offset: 0x0002EFF8
			public void Insert(int index, ListViewItem.ListViewSubItem item)
			{
				item.owner = this.owner;
				this.list.Insert(index, item);
				this.owner.Layout();
				this.owner.Invalidate();
				item.UIATextChanged += this.OnUIASubItemTextChanged;
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x06000B6D RID: 2925 RVA: 0x00030E46 File Offset: 0x0002F046
			public void Remove(ListViewItem.ListViewSubItem item)
			{
				this.list.Remove(item);
				this.owner.Layout();
				this.owner.Invalidate();
				item.UIATextChanged -= this.OnUIASubItemTextChanged;
			}

			/// <summary>Removes the subitem at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the subitem to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x06000B6E RID: 2926 RVA: 0x00030E7C File Offset: 0x0002F07C
			public void RemoveAt(int index)
			{
				if (index >= 0 && index < this.list.Count)
				{
					((ListViewItem.ListViewSubItem)this.list[index]).UIATextChanged -= this.OnUIASubItemTextChanged;
				}
				this.list.RemoveAt(index);
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x00030EC9 File Offset: 0x0002F0C9
			private void OnUIASubItemTextChanged(object sender, EventArgs args)
			{
				this.owner.OnUIASubItemTextChanged(new LabelEditEventArgs(this.list.IndexOf(sender)));
			}

			// Token: 0x04000766 RID: 1894
			private ArrayList list;

			// Token: 0x04000767 RID: 1895
			internal ListViewItem owner;
		}
	}
}
