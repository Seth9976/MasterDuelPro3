using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Displays a hierarchical collection of labeled items, each represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000201 RID: 513
	[ComVisible(true)]
	[Docking(DockingBehavior.Ask)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Nodes")]
	[DefaultEvent("AfterSelect")]
	[Designer("System.Windows.Forms.Design.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class TreeView : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeView" /> class.</summary>
		// Token: 0x060015B7 RID: 5559 RVA: 0x0006C9FC File Offset: 0x0006ABFC
		public TreeView()
		{
			this.vbar = new ImplicitVScrollBar();
			this.hbar = new ImplicitHScrollBar();
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.background_color = ThemeEngine.Current.ColorWindow;
			this.foreground_color = ThemeEngine.Current.ColorWindowText;
			this.draw_mode = TreeViewDrawMode.Normal;
			this.root_node = new TreeNode(this);
			this.root_node.Text = "ROOT NODE";
			this.nodes = new TreeNodeCollection(this.root_node);
			this.root_node.SetNodes(this.nodes);
			base.MouseDown += this.MouseDownHandler;
			base.MouseUp += this.MouseUpHandler;
			base.MouseMove += this.MouseMoveHandler;
			base.SizeChanged += this.SizeChangedHandler;
			base.FontChanged += this.FontChangedHandler;
			base.LostFocus += this.LostFocusHandler;
			base.GotFocus += this.GotFocusHandler;
			base.MouseWheel += this.MouseWheelHandler;
			base.VisibleChanged += this.VisibleChangedHandler;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			this.string_format = new StringFormat();
			this.string_format.LineAlignment = StringAlignment.Center;
			this.string_format.Alignment = StringAlignment.Center;
			this.vbar.Visible = false;
			this.hbar.Visible = false;
			this.vbar.ValueChanged += this.VScrollBarValueChanged;
			this.hbar.ValueChanged += this.HScrollBarValueChanged;
			base.SuspendLayout();
			base.Controls.AddImplicit(this.vbar);
			base.Controls.AddImplicit(this.hbar);
			base.ResumeLayout();
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0006CC3E File Offset: 0x0006AE3E
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.CreateDashPen();
				base.Invalidate();
			}
		}

		/// <summary>Gets or set the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00005B72 File Offset: 0x00003D72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>Gets or sets the border style of the tree view control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BD RID: 1469
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x0003B966 File Offset: 0x00039B66
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		public BorderStyle BorderStyle
		{
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the tree view control.</summary>
		/// <returns>true if a check box is displayed next to each tree node in the tree view control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0006CC53 File Offset: 0x0006AE53
		[DefaultValue(false)]
		public bool CheckBoxes
		{
			get
			{
				return this.checkboxes;
			}
		}

		/// <summary>The current foreground color for this control, which is the color the control uses to draw its text.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x0003B989 File Offset: 0x00039B89
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected tree node remains highlighted even when the tree view has lost the focus.</summary>
		/// <returns>true if the selected tree node is not highlighted when the tree view has lost the focus; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C0 RID: 1472
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x0006CC5B File Offset: 0x0006AE5B
		[DefaultValue(true)]
		public bool HideSelection
		{
			set
			{
				if (this.hide_selection == value)
				{
					return;
				}
				this.hide_selection = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it.</summary>
		/// <returns>true if a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C1 RID: 1473
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x0006CC74 File Offset: 0x0006AE74
		[DefaultValue(false)]
		public bool HotTracking
		{
			set
			{
				this.hot_tracking = value;
			}
		}

		/// <summary>Gets or sets the image-list index value of the default image that is displayed by the tree nodes.</summary>
		/// <returns>A zero-based index that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0006CC7D File Offset: 0x0006AE7D
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0006CC85 File Offset: 0x0006AE85
		[DefaultValue(-1)]
		[RelatedImageList("ImageList")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
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
				if (value < -1)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'. 'value' must be greater than or equal to 0.");
				}
				if (this.image_index == value)
				{
					return;
				}
				this.image_index = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects that are used by the tree nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects that are used by the tree nodes. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0006CCBD File Offset: 0x0006AEBD
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0006CCC5 File Offset: 0x0006AEC5
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				this.image_list = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the distance to indent each child tree node level.</summary>
		/// <returns>The distance, in pixels, to indent each child tree node level. The default value is 19.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0 (see Remarks).-or- The assigned value is greater than 32,000. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0006CCD4 File Offset: 0x0006AED4
		[Localizable(true)]
		public int Indent
		{
			get
			{
				return this.indent;
			}
		}

		/// <summary>Gets or sets the height of each tree node in the tree view control.</summary>
		/// <returns>The height, in pixels, of each tree node in the tree view.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than one.-or- The assigned value is greater than the <see cref="F:System.Int16.MaxValue" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0006CCDC File Offset: 0x0006AEDC
		public int ItemHeight
		{
			get
			{
				if (this.item_height == -1)
				{
					return base.FontHeight + 3;
				}
				return this.item_height;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0006CCF8 File Offset: 0x0006AEF8
		internal int ActualItemHeight
		{
			get
			{
				int num = this.ItemHeight;
				if (this.ImageList != null && this.ImageList.ImageSize.Height > num)
				{
					num = this.ImageList.ImageSize.Height;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the label text of the tree nodes can be edited.</summary>
		/// <returns>true if the label text of the tree nodes can be edited; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C7 RID: 1479
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0006CD3F File Offset: 0x0006AF3F
		[DefaultValue(false)]
		public bool LabelEdit
		{
			set
			{
				this.label_edit = value;
				this.OnUIALabelEditChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the collection of tree nodes that are assigned to the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0006CD53 File Offset: 0x0006AF53
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[Localizable(true)]
		public TreeNodeCollection Nodes
		{
			get
			{
				return this.nodes;
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when a tree node is selected.</summary>
		/// <returns>A zero-based index value that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <exception cref="T:System.ArgumentException">The index assigned value is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0006CD5B File Offset: 0x0006AF5B
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x0006CD63 File Offset: 0x0006AF63
		[DefaultValue(-1)]
		[RelatedImageList("ImageList")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public int SelectedImageIndex
		{
			get
			{
				return this.selected_image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'. 'value' must be greater than or equal to 0.");
				}
				this.UpdateNode(this.SelectedNode);
			}
		}

		/// <summary>Gets or sets the tree node that is currently selected in the tree view control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that is currently selected in the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0006CD90 File Offset: 0x0006AF90
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x0006CDA8 File Offset: 0x0006AFA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode SelectedNode
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.pre_selected_node;
				}
				return this.selected_node;
			}
			set
			{
				if (!base.IsHandleCreated)
				{
					this.pre_selected_node = value;
					return;
				}
				if (this.selected_node == value)
				{
					this.selection_action = TreeViewAction.Unknown;
					return;
				}
				if (value != null)
				{
					TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(value, false, this.selection_action);
					this.OnBeforeSelect(treeViewCancelEventArgs);
					if (treeViewCancelEventArgs.Cancel)
					{
						return;
					}
				}
				Rectangle rectangle = Rectangle.Empty;
				if (this.selected_node != null)
				{
					rectangle = this.Bloat(this.selected_node.Bounds);
				}
				if (this.focused_node != null)
				{
					rectangle = Rectangle.Union(rectangle, this.Bloat(this.focused_node.Bounds));
				}
				if (value != null)
				{
					rectangle = Rectangle.Union(rectangle, this.Bloat(value.Bounds));
				}
				this.highlighted_node = value;
				this.selected_node = value;
				this.focused_node = value;
				if (this.full_row_select || this.draw_mode != TreeViewDrawMode.Normal)
				{
					rectangle.X = 0;
					rectangle.Width = this.ViewportRectangle.Width;
				}
				if (rectangle != Rectangle.Empty)
				{
					base.Invalidate(rectangle);
				}
				if (this.selected_node != null)
				{
					this.selected_node.EnsureVisible();
				}
				if (value != null)
				{
					this.OnAfterSelect(new TreeViewEventArgs(value, TreeViewAction.Unknown));
				}
				this.selection_action = TreeViewAction.Unknown;
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0006CED0 File Offset: 0x0006B0D0
		private Rectangle Bloat(Rectangle rect)
		{
			int num = rect.Y;
			rect.Y = num - 1;
			num = rect.X;
			rect.X = num - 1;
			rect.Height += 2;
			rect.Width += 2;
			return rect;
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between tree nodes in the tree view control.</summary>
		/// <returns>true if lines are drawn between tree nodes in the tree view control; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CB RID: 1483
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x0006CF1E File Offset: 0x0006B11E
		[DefaultValue(true)]
		public bool ShowLines
		{
			set
			{
				if (this.show_lines == value)
				{
					return;
				}
				this.show_lines = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>true if ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0006CF37 File Offset: 0x0006B137
		[DefaultValue(false)]
		public bool ShowNodeToolTips
		{
			get
			{
				return this.show_node_tool_tips;
			}
		}

		/// <summary>Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to tree nodes that contain child tree nodes.</summary>
		/// <returns>true if plus sign and minus sign buttons are displayed next to tree nodes that contain child tree nodes; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CD RID: 1485
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x0006CF3F File Offset: 0x0006B13F
		[DefaultValue(true)]
		public bool ShowPlusMinus
		{
			set
			{
				if (this.show_plus_minus == value)
				{
					return;
				}
				this.show_plus_minus = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between the tree nodes that are at the root of the tree view.</summary>
		/// <returns>true if lines are drawn between the tree nodes that are at the root of the tree view; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0006CF58 File Offset: 0x0006B158
		[DefaultValue(true)]
		public bool ShowRootLines
		{
			get
			{
				return this.show_root_lines;
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree nodes in the tree view are sorted.</summary>
		/// <returns>true if the tree nodes in the tree view are sorted; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0006CF60 File Offset: 0x0006B160
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
		}

		/// <summary>Gets or sets the image list that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used for indicating the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0006CF68 File Offset: 0x0006B168
		[DefaultValue(null)]
		public ImageList StateImageList
		{
			get
			{
				return this.state_image_list;
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TreeView" />.</summary>
		/// <returns>Null in all cases.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000043BC File Offset: 0x000025BC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the first fully-visible tree node in the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the first fully-visible tree node in the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0006CF70 File Offset: 0x0006B170
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode TopNode
		{
			get
			{
				if (this.root_node.FirstNode == null)
				{
					return null;
				}
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.FirstNode);
				openTreeNodeEnumerator.MoveNext();
				for (int i = 0; i < this.skipped_nodes; i++)
				{
					openTreeNodeEnumerator.MoveNext();
				}
				return openTreeNodeEnumerator.CurrentNode;
			}
		}

		/// <summary>Gets or sets the implementation of <see cref="T:System.Collections.IComparer" /> to perform a custom sort of the <see cref="T:System.Windows.Forms.TreeView" /> nodes.</summary>
		/// <returns>The <see cref="T:System.Collections.IComparer" /> to perform the custom sort.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0006CFC2 File Offset: 0x0006B1C2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IComparer TreeViewNodeSorter
		{
			get
			{
				return this.tree_view_node_sorter;
			}
		}

		/// <summary>Gets the number of tree nodes that can be fully visible in the tree view control.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.TreeNode" /> items that can be fully visible in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0006CFCC File Offset: 0x0006B1CC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VisibleCount
		{
			get
			{
				return this.ViewportRectangle.Height / this.ActualItemHeight;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer. The <see cref="P:System.Windows.Forms.TreeView.DoubleBuffered" /> property does not affect the <see cref="T:System.Windows.Forms.TreeView" /> control. </summary>
		/// <returns>true if the control uses a secondary buffer; otherwise, false.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00027890 File Offset: 0x00025A90
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
		}

		/// <summary>Gets or sets the color of the lines connecting the nodes of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the lines connecting the tree nodes.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0006CFF0 File Offset: 0x0006B1F0
		[DefaultValue("Color [Black]")]
		public Color LineColor
		{
			get
			{
				if (this.line_color == Color.Empty)
				{
					Color color = ControlPaint.Dark(this.BackColor);
					if (color == this.BackColor)
					{
						color = ControlPaint.Light(this.BackColor);
					}
					return color;
				}
				return this.line_color;
			}
		}

		/// <summary>Gets or sets the key of the default image for each node in the <see cref="T:System.Windows.Forms.TreeView" /> control when it is in an unselected state.</summary>
		/// <returns>The key of the default image shown for each node <see cref="T:System.Windows.Forms.TreeView" /> control when the node is in an unselected state.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0006D03D File Offset: 0x0006B23D
		[Localizable(true)]
		[DefaultValue("")]
		[RelatedImageList("ImageList")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
		}

		/// <summary>Gets or sets the key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</summary>
		/// <returns>The key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0006D045 File Offset: 0x0006B245
		[Localizable(true)]
		[DefaultValue("")]
		[RelatedImageList("ImageList")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string SelectedImageKey
		{
			get
			{
				return this.selected_image_key;
			}
		}

		/// <summary>Gets or sets the layout of the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00005B82 File Offset: 0x00003D82
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005759C File Offset: 0x0005579C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		/// <summary>Disables any redrawing of the tree view.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E2 RID: 5602 RVA: 0x0006D04D File Offset: 0x0006B24D
		public void BeginUpdate()
		{
			this.update_stack++;
		}

		/// <summary>Enables the redrawing of the tree view.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E3 RID: 5603 RVA: 0x0006D060 File Offset: 0x0006B260
		public void EndUpdate()
		{
			if (this.update_stack > 1)
			{
				this.update_stack--;
				return;
			}
			this.update_stack = 0;
			if (this.update_needed)
			{
				this.RecalculateVisibleOrder(this.root_node);
				this.UpdateScrollBars(false);
				base.Invalidate(this.ViewportRectangle);
				this.update_needed = false;
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0006D0BC File Offset: 0x0006B2BC
		private void SetVScrollValue(int value)
		{
			if (value > this.vbar.Maximum)
			{
				value = this.vbar.Maximum;
			}
			else if (value < this.vbar.Minimum)
			{
				value = this.vbar.Minimum;
			}
			this.vbar.Value = value;
		}

		/// <summary>Retrieves the tree node that is at the specified point.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified point, in tree view (client) coordinates, or null if there is no node at that location.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to evaluate and retrieve the node from. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E5 RID: 5605 RVA: 0x0006D10D File Offset: 0x0006B30D
		public TreeNode GetNodeAt(Point pt)
		{
			return this.GetNodeAt(pt.Y);
		}

		/// <summary>Retrieves the tree node at the point with the specified coordinates.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified location, in tree view (client) coordinates, or null if there is no node at that location.</returns>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> position to evaluate and retrieve the node from. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> position to evaluate and retrieve the node from. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E6 RID: 5606 RVA: 0x0006D11C File Offset: 0x0006B31C
		public TreeNode GetNodeAt(int x, int y)
		{
			return this.GetNodeAt(y);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0006D128 File Offset: 0x0006B328
		private TreeNode GetNodeAtUseX(int x, int y)
		{
			TreeNode nodeAt = this.GetNodeAt(y);
			if (nodeAt == null || (!this.IsTextArea(nodeAt, x) && !this.full_row_select))
			{
				return null;
			}
			return nodeAt;
		}

		/// <summary>Overrides <see cref="M:System.ComponentModel.Component.ToString" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015E8 RID: 5608 RVA: 0x0006D158 File Offset: 0x0006B358
		public override string ToString()
		{
			int count = this.Nodes.Count;
			if (count <= 0)
			{
				return base.ToString() + ", Nodes.Count: 0";
			}
			return string.Concat(new object[]
			{
				base.ToString(),
				", Nodes.Count: ",
				count,
				", Nodes[0]: ",
				this.Nodes[0]
			});
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0006D1C2 File Offset: 0x0006B3C2
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.RecalculateVisibleOrder(this.root_node);
			this.UpdateScrollBars(false);
			if (this.pre_selected_node != null)
			{
				this.SelectedNode = this.pre_selected_node;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TreeView" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060015EA RID: 5610 RVA: 0x0006D1F1 File Offset: 0x0006B3F1
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.image_list = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the Keys values.</param>
		// Token: 0x060015EB RID: 5611 RVA: 0x0006D204 File Offset: 0x0006B404
		protected override bool IsInputKey(Keys keyData)
		{
			if (base.IsHandleCreated && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Return)
				{
					switch (keys)
					{
					case Keys.Escape:
					case Keys.PageUp:
					case Keys.PageDown:
					case Keys.End:
					case Keys.Home:
						break;
					case Keys.IMEConvert:
					case Keys.IMENonconvert:
					case Keys.IMEAceept:
					case Keys.IMEModeChange:
					case Keys.Space:
						goto IL_006D;
					case Keys.Left:
					case Keys.Up:
					case Keys.Right:
					case Keys.Down:
						return true;
					default:
						goto IL_006D;
					}
				}
				if (this.edit_node != null)
				{
					return true;
				}
			}
			IL_006D:
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060015EC RID: 5612 RVA: 0x0006D288 File Offset: 0x0006B488
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Keys keys = e.KeyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.PageUp:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					int visibleCount = this.VisibleCount;
					int num = 0;
					while (num < visibleCount && openTreeNodeEnumerator.MovePrevious())
					{
						num++;
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.PageDown:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					int visibleCount2 = this.VisibleCount;
					int num2 = 0;
					while (num2 < visibleCount2 && openTreeNodeEnumerator.MoveNext())
					{
						num2++;
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.End:
				if (this.root_node.Nodes.Count > 0)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.Nodes[0]);
					while (openTreeNodeEnumerator.MoveNext())
					{
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.Home:
				if (this.root_node.Nodes.Count > 0)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.Nodes[0]);
					if (openTreeNodeEnumerator.MoveNext())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			case Keys.Left:
				if (this.selected_node != null)
				{
					if (this.selected_node.IsExpanded && this.selected_node.Nodes.Count > 0)
					{
						this.selected_node.Collapse();
					}
					else
					{
						TreeNode parent = this.selected_node.Parent;
						if (parent != null)
						{
							this.selection_action = TreeViewAction.ByKeyboard;
							this.SelectedNode = parent;
						}
					}
				}
				break;
			case Keys.Up:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					if (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.MovePrevious())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			case Keys.Right:
				if (this.selected_node != null)
				{
					if (!this.selected_node.IsExpanded)
					{
						this.selected_node.Expand();
					}
					else
					{
						TreeNode firstNode = this.selected_node.FirstNode;
						if (firstNode != null)
						{
							this.SelectedNode = firstNode;
						}
					}
				}
				break;
			case Keys.Down:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			default:
				switch (keys)
				{
				case Keys.Multiply:
					if (this.selected_node != null)
					{
						this.selected_node.ExpandAll();
					}
					break;
				case Keys.Add:
					if (this.selected_node != null && this.selected_node.IsExpanded)
					{
						this.selected_node.Expand();
					}
					break;
				case Keys.Subtract:
					if (this.selected_node != null && this.selected_node.IsExpanded)
					{
						this.selected_node.Collapse();
					}
					break;
				}
				break;
			}
			base.OnKeyDown(e);
			if (!e.Handled && this.checkboxes && this.selected_node != null && (e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				this.selected_node.check_reason = TreeViewAction.ByKeyboard;
				this.selected_node.Checked = !this.selected_node.Checked;
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x060015ED RID: 5613 RVA: 0x0006D611 File Offset: 0x0006B811
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnKeyUp(System.Windows.Forms.KeyEventArgs)" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060015EE RID: 5614 RVA: 0x0006D62B File Offset: 0x0006B82B
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if ((e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060015EF RID: 5615 RVA: 0x0006D64C File Offset: 0x0006B84C
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			this.is_hovering = true;
			TreeNode nodeAt = this.GetNodeAt(base.PointToClient(Control.MousePosition));
			if (nodeAt != null)
			{
				this.MouseEnteredItem(nodeAt);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060015F0 RID: 5616 RVA: 0x0006D683 File Offset: 0x0006B883
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.is_hovering = false;
			if (this.tooltip_currently_showing != null)
			{
				this.MouseLeftItem(this.tooltip_currently_showing);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F1 RID: 5617 RVA: 0x0006D6A8 File Offset: 0x0006B8A8
		protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = (TreeNodeMouseClickEventHandler)base.Events[TreeView.NodeMouseClickEvent];
			if (treeNodeMouseClickEventHandler != null)
			{
				treeNodeMouseClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F2 RID: 5618 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = (TreeNodeMouseClickEventHandler)base.Events[TreeView.NodeMouseDoubleClickEvent];
			if (treeNodeMouseClickEventHandler != null)
			{
				treeNodeMouseClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event. </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> that contains the event data.</param>
		// Token: 0x060015F3 RID: 5619 RVA: 0x0006D708 File Offset: 0x0006B908
		protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
		{
			TreeNodeMouseHoverEventHandler treeNodeMouseHoverEventHandler = (TreeNodeMouseHoverEventHandler)base.Events[TreeView.NodeMouseHoverEvent];
			if (treeNodeMouseHoverEventHandler != null)
			{
				treeNodeMouseHoverEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F4 RID: 5620 RVA: 0x0006D738 File Offset: 0x0006B938
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			ItemDragEventHandler itemDragEventHandler = (ItemDragEventHandler)base.Events[TreeView.ItemDragEvent];
			if (itemDragEventHandler != null)
			{
				itemDragEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F5 RID: 5621 RVA: 0x0006D768 File Offset: 0x0006B968
		protected virtual void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			DrawTreeNodeEventHandler drawTreeNodeEventHandler = (DrawTreeNodeEventHandler)base.Events[TreeView.DrawNodeEvent];
			if (drawTreeNodeEventHandler != null)
			{
				drawTreeNodeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F6 RID: 5622 RVA: 0x0006D798 File Offset: 0x0006B998
		protected internal virtual void OnAfterCheck(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterCheckEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F7 RID: 5623 RVA: 0x0006D7C8 File Offset: 0x0006B9C8
		protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterCollapseEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F8 RID: 5624 RVA: 0x0006D7F8 File Offset: 0x0006B9F8
		protected internal virtual void OnAfterExpand(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterExpandEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060015F9 RID: 5625 RVA: 0x0006D828 File Offset: 0x0006BA28
		protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
		{
			NodeLabelEditEventHandler nodeLabelEditEventHandler = (NodeLabelEditEventHandler)base.Events[TreeView.AfterLabelEditEvent];
			if (nodeLabelEditEventHandler != null)
			{
				nodeLabelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FA RID: 5626 RVA: 0x0006D858 File Offset: 0x0006BA58
		protected virtual void OnAfterSelect(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterSelectEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FB RID: 5627 RVA: 0x0006D888 File Offset: 0x0006BA88
		protected internal virtual void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeCheckEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FC RID: 5628 RVA: 0x0006D8B8 File Offset: 0x0006BAB8
		protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeCollapseEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FD RID: 5629 RVA: 0x0006D8E8 File Offset: 0x0006BAE8
		protected internal virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeExpandEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FE RID: 5630 RVA: 0x0006D918 File Offset: 0x0006BB18
		protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
		{
			NodeLabelEditEventHandler nodeLabelEditEventHandler = (NodeLabelEditEventHandler)base.Events[TreeView.BeforeLabelEditEvent];
			if (nodeLabelEditEventHandler != null)
			{
				nodeLabelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060015FF RID: 5631 RVA: 0x0006D948 File Offset: 0x0006BB48
		protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeSelectEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001600 RID: 5632 RVA: 0x00004D0D File Offset: 0x00002F0D
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001601 RID: 5633 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06001602 RID: 5634 RVA: 0x0006D978 File Offset: 0x0006BB78
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_CONTEXTMENU)
			{
				if (msg == Msg.WM_LBUTTONDBLCLK)
				{
					int num = m.LParam.ToInt32();
					this.DoubleClickHandler(null, new MouseEventArgs(MouseButtons.Left, 2, num & 65535, (num >> 16) & 65535, 0));
				}
			}
			else if (this.WmContextMenu(ref m))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0006D9DF File Offset: 0x0006BBDF
		internal override void HandleClick(int clicks, MouseEventArgs me)
		{
			if (this.GetNodeAt(me.Location) != null)
			{
				if (clicks > 1 && base.GetStyle(ControlStyles.StandardDoubleClick))
				{
					this.OnDoubleClick(me);
					this.OnMouseDoubleClick(me);
					return;
				}
				this.OnClick(me);
				this.OnMouseClick(me);
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00006F54 File Offset: 0x00005154
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0006DA20 File Offset: 0x0006BC20
		internal Rectangle ViewportRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				if (this.vbar != null && this.vbar.Visible)
				{
					clientRectangle.Width -= this.vbar.Width;
				}
				if (this.hbar != null && this.hbar.Visible)
				{
					clientRectangle.Height -= this.hbar.Height;
				}
				return clientRectangle;
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0006DA94 File Offset: 0x0006BC94
		private TreeNode GetNodeAt(int y)
		{
			if (this.nodes.Count <= 0)
			{
				return null;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.TopNode);
			int num = y / this.ActualItemHeight;
			for (int i = -1; i < num; i++)
			{
				if (!openTreeNodeEnumerator.MoveNext())
				{
					return null;
				}
			}
			return openTreeNodeEnumerator.CurrentNode;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0006DAE4 File Offset: 0x0006BCE4
		private bool IsTextArea(TreeNode node, int x)
		{
			return node != null && node.Bounds.Left <= x && node.Bounds.Right >= x;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0006DB1C File Offset: 0x0006BD1C
		private bool IsSelectableArea(TreeNode node, int x)
		{
			if (node == null)
			{
				return false;
			}
			int num = node.Bounds.Left;
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width;
			}
			return num <= x && node.Bounds.Right >= x;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0006DB78 File Offset: 0x0006BD78
		private bool IsPlusMinusArea(TreeNode node, int x)
		{
			if (node.Nodes.Count == 0 || (node.parent == this.root_node && !this.show_root_lines))
			{
				return false;
			}
			int num = node.Bounds.Left + 5;
			if (this.show_root_lines || node.Parent != null)
			{
				num -= this.indent;
			}
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width + 3;
			}
			if (this.checkboxes)
			{
				num -= 19;
			}
			else if (node.StateImage != null)
			{
				num -= 19;
			}
			return x > num && x < num + 8;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0006DC1C File Offset: 0x0006BE1C
		private bool IsCheckboxArea(TreeNode node, int x)
		{
			int num = this.CheckBoxLeft(node);
			return x > num && x < num + 10;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0006DC40 File Offset: 0x0006BE40
		private int CheckBoxLeft(TreeNode node)
		{
			int num = node.Bounds.Left + 5;
			if (this.show_root_lines || node.Parent != null)
			{
				num -= this.indent;
			}
			if (!this.show_root_lines && node.Parent == null)
			{
				num -= this.indent;
			}
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width + 3;
			}
			return num;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0006DCB4 File Offset: 0x0006BEB4
		internal void RecalculateVisibleOrder(TreeNode start)
		{
			if (this.update_stack > 0)
			{
				return;
			}
			int num;
			if (start == null)
			{
				start = this.root_node;
				num = 0;
			}
			else
			{
				num = start.visible_order;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(start);
			while (openTreeNodeEnumerator.MoveNext())
			{
				openTreeNodeEnumerator.CurrentNode.visible_order = num;
				num++;
			}
			this.max_visible_order = num;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0006DD0C File Offset: 0x0006BF0C
		internal void SetTop(TreeNode node)
		{
			int num = 0;
			if (node != null)
			{
				num = Math.Max(0, node.visible_order - 1);
			}
			if (!this.vbar.is_visible)
			{
				this.skipped_nodes = num;
				return;
			}
			this.SetVScrollValue(Math.Min(num, this.vbar.Maximum - this.VisibleCount + 1));
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0006DD64 File Offset: 0x0006BF64
		internal void SetBottom(TreeNode node)
		{
			if (!this.vbar.is_visible)
			{
				return;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(node);
			int bottom = this.ViewportRectangle.Bottom;
			int num = 0;
			while (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.CurrentNode.Bounds.Bottom > bottom)
			{
				num++;
			}
			int num2 = this.vbar.Value + num;
			if (this.vbar.Value + num < this.vbar.Maximum)
			{
				this.SetVScrollValue(num2);
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0006DDEC File Offset: 0x0006BFEC
		internal void UpdateBelow(TreeNode node)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			if (node == this.root_node)
			{
				base.Invalidate(this.ViewportRectangle);
				return;
			}
			int num = Math.Max(node.Bounds.Top - 1, 0);
			Rectangle rectangle = new Rectangle(0, num, base.Width, base.Height - num);
			base.Invalidate(rectangle);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0006DE58 File Offset: 0x0006C058
		internal void UpdateNode(TreeNode node)
		{
			if (node == null)
			{
				return;
			}
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			if (node == this.root_node)
			{
				base.Invalidate();
				return;
			}
			Rectangle rectangle = new Rectangle(0, node.Bounds.Top - 1, base.Width, node.Bounds.Height + 1);
			base.Invalidate(rectangle);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0006DEBF File Offset: 0x0006C0BF
		internal override void OnPaintInternal(PaintEventArgs pe)
		{
			this.Draw(pe.ClipRectangle, pe.Graphics);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0006DED3 File Offset: 0x0006C0D3
		internal void CreateDashPen()
		{
			this.dash = new Pen(this.LineColor, 1f);
			this.dash.DashStyle = DashStyle.Dot;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0006DEF8 File Offset: 0x0006C0F8
		private void Draw(Rectangle clip, Graphics dc)
		{
			dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), clip);
			if (this.dash == null)
			{
				this.CreateDashPen();
			}
			Rectangle viewportRectangle = this.ViewportRectangle;
			Rectangle rectangle = clip;
			if (clip.Bottom > viewportRectangle.Bottom)
			{
				clip.Height = viewportRectangle.Bottom - clip.Top;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.TopNode);
			while (openTreeNodeEnumerator.MoveNext())
			{
				TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
				if (currentNode.GetY() + this.ActualItemHeight >= clip.Top)
				{
					if (currentNode.GetY() > clip.Bottom)
					{
						break;
					}
					this.DrawTreeNode(currentNode, dc, clip);
				}
			}
			if (this.hbar.Visible && this.vbar.Visible)
			{
				Rectangle rectangle2 = new Rectangle(this.hbar.Right, this.vbar.Bottom, this.vbar.Width, this.hbar.Height);
				if (rectangle.IntersectsWith(rectangle2))
				{
					dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorControl), rectangle2);
				}
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0006E024 File Offset: 0x0006C224
		private void DrawNodeState(TreeNode node, Graphics dc, int x, int y)
		{
			if (node.Checked)
			{
				if (this.StateImageList.Images[1] != null)
				{
					dc.DrawImage(this.StateImageList.Images[1], new Rectangle(x, y, 16, 16));
					return;
				}
			}
			else if (this.StateImageList.Images[0] != null)
			{
				dc.DrawImage(this.StateImageList.Images[0], new Rectangle(x, y, 16, 16));
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0006E0A8 File Offset: 0x0006C2A8
		private void DrawNodeCheckBox(TreeNode node, Graphics dc, int x, int middle)
		{
			Pen sizedPen = ThemeEngine.Current.ResPool.GetSizedPen(Color.Black, 2);
			dc.DrawRectangle(sizedPen, x + 3, middle - 4, 11, 11);
			if (node.Checked)
			{
				Pen pen = ThemeEngine.Current.ResPool.GetPen(Color.Black);
				int num = 5;
				int num2 = 3;
				Rectangle rectangle = new Rectangle(x + 4, middle - 3, num, num);
				for (int i = 0; i < num2; i++)
				{
					dc.DrawLine(pen, rectangle.Left + 1, rectangle.Top + num2 + i, rectangle.Left + 3, rectangle.Top + 5 + i);
					dc.DrawLine(pen, rectangle.Left + 3, rectangle.Top + 5 + i, rectangle.Left + 7, rectangle.Top + 1 + i);
				}
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0006E184 File Offset: 0x0006C384
		private void DrawNodeLines(TreeNode node, Graphics dc, Rectangle clip, Pen dash, int x, int y, int middle)
		{
			int num = 9;
			int num2 = 0;
			if (node.nodes.Count > 0 && this.show_plus_minus)
			{
				num = 13;
			}
			if (this.checkboxes)
			{
				num2 = 3;
			}
			if (this.show_root_lines || node.Parent != null)
			{
				dc.DrawLine(dash, x - this.indent + num, middle, x + num2, middle);
			}
			if (node.PrevNode != null || node.Parent != null)
			{
				num = 9;
				dc.DrawLine(dash, x - this.indent + num, node.Bounds.Top, x - this.indent + num, middle - ((this.show_plus_minus && node.Nodes.Count > 0) ? 4 : 0));
			}
			if (node.NextNode != null)
			{
				num = 9;
				dc.DrawLine(dash, x - this.indent + num, middle + ((this.show_plus_minus && node.Nodes.Count > 0) ? 4 : 0), x - this.indent + num, node.Bounds.Bottom);
			}
			num = 0;
			if (this.show_plus_minus)
			{
				num = 9;
			}
			for (TreeNode treeNode = node.Parent; treeNode != null; treeNode = treeNode.Parent)
			{
				if (treeNode.NextNode != null)
				{
					int num3 = treeNode.GetLinesX() - this.indent + num;
					dc.DrawLine(dash, num3, node.Bounds.Top, num3, node.Bounds.Bottom);
				}
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0006E2F8 File Offset: 0x0006C4F8
		private void DrawNodeImage(TreeNode node, Graphics dc, Rectangle clip, int x, int y)
		{
			if (!this.RectsIntersect(clip, x, y, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height))
			{
				return;
			}
			int image = node.Image;
			if (image > -1 && image < this.ImageList.Images.Count)
			{
				this.ImageList.Draw(dc, x, y, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height, image);
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0006E38E File Offset: 0x0006C58E
		private void LabelEditFinished(object sender, EventArgs e)
		{
			this.EndEdit(this.edit_node);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0006E39C File Offset: 0x0006C59C
		internal void BeginEdit(TreeNode node)
		{
			if (this.edit_node != null)
			{
				this.EndEdit(this.edit_node);
			}
			if (this.edit_text_box == null)
			{
				this.edit_text_box = new LabelEditTextBox();
				this.edit_text_box.BorderStyle = BorderStyle.FixedSingle;
				this.edit_text_box.Visible = false;
				this.edit_text_box.EditingCancelled += this.LabelEditCancelled;
				this.edit_text_box.EditingFinished += this.LabelEditFinished;
				this.edit_text_box.TextChanged += this.LabelTextChanged;
				base.Controls.Add(this.edit_text_box);
			}
			node.EnsureVisible();
			this.edit_text_box.Bounds = node.Bounds;
			this.edit_text_box.Text = node.Text;
			this.edit_text_box.Visible = true;
			this.edit_text_box.Focus();
			this.edit_text_box.SelectAll();
			this.edit_args = new NodeLabelEditEventArgs(node);
			this.OnBeforeLabelEdit(this.edit_args);
			this.edit_node = node;
			if (this.edit_args.CancelEdit)
			{
				this.edit_node = null;
				this.EndEdit(node);
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006E4C3 File Offset: 0x0006C6C3
		private void LabelEditCancelled(object sender, EventArgs e)
		{
			this.edit_args.SetLabel(null);
			this.EndEdit(this.edit_node);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0006E4E0 File Offset: 0x0006C6E0
		private void LabelTextChanged(object sender, EventArgs e)
		{
			int num = TextRenderer.MeasureTextInternal(this.edit_text_box.Text, this.edit_text_box.Font, false).Width + 4;
			this.edit_text_box.Width = num;
			if (this.edit_args != null)
			{
				this.edit_args.SetLabel(this.edit_text_box.Text);
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0006E540 File Offset: 0x0006C740
		internal void EndEdit(TreeNode node)
		{
			if (this.edit_text_box != null && this.edit_text_box.Visible)
			{
				this.edit_text_box.Visible = false;
				base.Focus();
			}
			Application.DoEvents();
			if (this.edit_node != null && this.edit_node == node)
			{
				this.edit_node = null;
				NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(this.edit_args.Node, this.edit_args.Label);
				this.OnAfterLabelEdit(nodeLabelEditEventArgs);
				if (nodeLabelEditEventArgs.CancelEdit)
				{
					return;
				}
				if (nodeLabelEditEventArgs.Label != null)
				{
					nodeLabelEditEventArgs.Node.Text = nodeLabelEditEventArgs.Label;
				}
			}
			this.edit_node = null;
			this.UpdateNode(node);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0006E5E8 File Offset: 0x0006C7E8
		internal void CancelEdit(TreeNode node)
		{
			this.edit_args.SetLabel(null);
			if (this.edit_text_box != null && this.edit_text_box.Visible)
			{
				this.edit_text_box.Visible = false;
				base.Focus();
			}
			this.edit_node = null;
			this.UpdateNode(node);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0006E638 File Offset: 0x0006C838
		internal int GetNodeWidth(TreeNode node)
		{
			Font font = node.NodeFont;
			if (node.NodeFont == null)
			{
				font = this.Font;
			}
			return (int)TextRenderer.MeasureString(node.Text, font, 0, this.string_format).Width + 3;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0006E67C File Offset: 0x0006C87C
		private void DrawSelectionAndFocus(TreeNode node, Graphics dc, Rectangle r)
		{
			if (this.Focused && this.focused_node == node && !this.full_row_select)
			{
				ControlPaint.DrawFocusRectangle(dc, r, this.ForeColor, this.BackColor);
			}
			if (this.draw_mode != TreeViewDrawMode.Normal)
			{
				return;
			}
			r.Inflate(-1, -1);
			if (this.Focused && node == this.highlighted_node)
			{
				Color color = ((node != this.selected_node && node.BackColor != Color.Empty) ? node.BackColor : ThemeEngine.Current.ColorHighlight);
				dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(color), r);
				return;
			}
			if (!this.hide_selection && node == this.highlighted_node)
			{
				dc.FillRectangle(SystemBrushes.Control, r);
				return;
			}
			Color color2 = ((node == this.selected_node) ? this.BackColor : node.BackColor);
			dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(color2), r);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0006E76C File Offset: 0x0006C96C
		private void DrawStaticNode(TreeNode node, Graphics dc)
		{
			if (!this.full_row_select || this.show_lines)
			{
				this.DrawSelectionAndFocus(node, dc, node.Bounds);
			}
			Font font = node.NodeFont;
			if (node.NodeFont == null)
			{
				font = this.Font;
			}
			Color color = ((this.Focused && node == this.highlighted_node) ? ThemeEngine.Current.ColorHighlightText : node.ForeColor);
			if (color.IsEmpty)
			{
				color = this.ForeColor;
			}
			dc.DrawString(node.Text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), node.Bounds, this.string_format);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0006E810 File Offset: 0x0006CA10
		private void DrawTreeNode(TreeNode node, Graphics dc, Rectangle clip)
		{
			int count = node.nodes.Count;
			int y = node.GetY();
			int num = y + this.ActualItemHeight / 2;
			if (this.full_row_select && !this.show_lines)
			{
				Rectangle rectangle = new Rectangle(1, y, this.ViewportRectangle.Width - 2, this.ActualItemHeight);
				this.DrawSelectionAndFocus(node, dc, rectangle);
			}
			if (this.draw_mode == TreeViewDrawMode.Normal || this.draw_mode == TreeViewDrawMode.OwnerDrawText)
			{
				if ((this.show_root_lines || node.Parent != null) && this.show_plus_minus && count > 0)
				{
					ThemeEngine.Current.TreeViewDrawNodePlusMinus(this, node, dc, node.GetLinesX() - this.Indent + 5, num);
				}
				if (this.checkboxes && this.state_image_list == null)
				{
					this.DrawNodeCheckBox(node, dc, this.CheckBoxLeft(node) - 3, num);
				}
				if (this.checkboxes && this.state_image_list != null)
				{
					this.DrawNodeState(node, dc, this.CheckBoxLeft(node) - 3, y);
				}
				if (!this.checkboxes && node.StateImage != null)
				{
					dc.DrawImage(node.StateImage, new Rectangle(this.CheckBoxLeft(node) - 3, y, 16, 16));
				}
				if (this.show_lines)
				{
					this.DrawNodeLines(node, dc, clip, this.dash, node.GetLinesX(), y, num);
				}
				if (this.ImageList != null)
				{
					this.DrawNodeImage(node, dc, clip, node.GetImageX(), y);
				}
			}
			if (this.draw_mode != TreeViewDrawMode.Normal)
			{
				dc.FillRectangle(Brushes.White, node.Bounds);
				TreeNodeStates treeNodeStates = TreeNodeStates.Default;
				if (node.IsSelected)
				{
					treeNodeStates = TreeNodeStates.Selected;
				}
				if (node.Checked)
				{
					treeNodeStates |= TreeNodeStates.Checked;
				}
				if (node == this.focused_node)
				{
					treeNodeStates |= TreeNodeStates.Focused;
				}
				Rectangle bounds = node.Bounds;
				if (this.draw_mode == TreeViewDrawMode.OwnerDrawText)
				{
					bounds.X += 3;
					bounds.Y++;
				}
				else
				{
					bounds.X = 0;
					bounds.Width = base.Width;
				}
				DrawTreeNodeEventArgs drawTreeNodeEventArgs = new DrawTreeNodeEventArgs(dc, node, bounds, treeNodeStates);
				this.OnDrawNode(drawTreeNodeEventArgs);
				if (!drawTreeNodeEventArgs.DrawDefault)
				{
					return;
				}
			}
			if (!node.IsEditing)
			{
				this.DrawStaticNode(node, dc);
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0006EA2C File Offset: 0x0006CC2C
		internal void UpdateScrollBars(bool force)
		{
			if (!force && (base.IsDisposed || this.update_stack > 0 || !base.IsHandleCreated || !base.Visible))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = -1;
			int actualItemHeight = this.ActualItemHeight;
			if (this.scrollable)
			{
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node);
				while (openTreeNodeEnumerator.MoveNext())
				{
					int right = openTreeNodeEnumerator.CurrentNode.Bounds.Right;
					if (right > num2)
					{
						num2 = right;
					}
					num += actualItemHeight;
				}
				num -= actualItemHeight;
				num2 += this.hbar_offset;
				if (num > base.ClientRectangle.Height)
				{
					flag = true;
					if (num2 > base.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth)
					{
						flag2 = true;
					}
				}
				else if (num2 > base.ClientRectangle.Width)
				{
					flag2 = true;
				}
				if (!flag && flag2 && num > base.ClientRectangle.Height - SystemInformation.HorizontalScrollBarHeight)
				{
					flag = true;
				}
			}
			if (flag)
			{
				int num3 = (flag2 ? (base.ClientRectangle.Height - this.hbar.Height) : base.ClientRectangle.Height);
				this.vbar.SetValues(Math.Max(0, this.max_visible_order - 2), num3 / this.ActualItemHeight);
				if (!this.vbar_bounds_set)
				{
					this.vbar.Bounds = new Rectangle(base.ClientRectangle.Width - this.vbar.Width, 0, this.vbar.Width, base.ClientRectangle.Height - (flag2 ? SystemInformation.VerticalScrollBarWidth : 0));
					this.vbar_bounds_set = true;
					this.hbar_bounds_set = false;
				}
				this.vbar.Visible = true;
				if (this.skipped_nodes > 0)
				{
					int num4 = Math.Min(this.skipped_nodes, this.vbar.Maximum - this.VisibleCount + 1);
					this.skipped_nodes = 0;
					this.vbar.SafeValueSet(num4);
					this.skipped_nodes = num4;
				}
			}
			else
			{
				this.skipped_nodes = 0;
				this.RecalculateVisibleOrder(this.root_node);
				this.vbar.Visible = false;
				this.SetVScrollValue(0);
				this.vbar_bounds_set = false;
			}
			if (flag2)
			{
				this.hbar.SetValues(num2 + 1, base.ClientRectangle.Width - (flag ? SystemInformation.VerticalScrollBarWidth : 0));
				if (!this.hbar_bounds_set)
				{
					this.hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - this.hbar.Height, base.ClientRectangle.Width - (flag ? SystemInformation.VerticalScrollBarWidth : 0), this.hbar.Height);
					this.hbar_bounds_set = true;
				}
				this.hbar.Visible = true;
				return;
			}
			this.hbar_offset = 0;
			this.hbar.Visible = false;
			this.hbar_bounds_set = false;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0006ED24 File Offset: 0x0006CF24
		private void SizeChangedHandler(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				if (this.max_visible_order == -1)
				{
					this.RecalculateVisibleOrder(this.root_node);
				}
				this.UpdateScrollBars(false);
			}
			if (this.vbar.Visible)
			{
				this.vbar.Bounds = new Rectangle(base.ClientRectangle.Width - this.vbar.Width, 0, this.vbar.Width, base.ClientRectangle.Height - (this.hbar.Visible ? SystemInformation.HorizontalScrollBarHeight : 0));
			}
			if (this.hbar.Visible)
			{
				this.hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - this.hbar.Height, base.ClientRectangle.Width - (this.vbar.Visible ? SystemInformation.VerticalScrollBarWidth : 0), this.hbar.Height);
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0006EE23 File Offset: 0x0006D023
		private void VScrollBarValueChanged(object sender, EventArgs e)
		{
			if (this.edit_node != null)
			{
				this.EndEdit(this.edit_node);
			}
			this.SetVScrollPos(this.vbar.Value, null);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0006EE4C File Offset: 0x0006D04C
		private void SetVScrollPos(int pos, TreeNode new_top)
		{
			if (!this.vbar.VisibleInternal)
			{
				return;
			}
			if (pos < 0)
			{
				pos = 0;
			}
			if (this.skipped_nodes == pos)
			{
				return;
			}
			int num = this.skipped_nodes - pos;
			this.skipped_nodes = pos;
			if (!base.IsHandleCreated)
			{
				return;
			}
			int num2 = num * this.ActualItemHeight;
			XplatUI.ScrollWindow(base.Handle, this.ViewportRectangle, 0, num2, false);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0006EEB0 File Offset: 0x0006D0B0
		private void HScrollBarValueChanged(object sender, EventArgs e)
		{
			if (this.edit_node != null)
			{
				this.EndEdit(this.edit_node);
			}
			int num = this.hbar_offset;
			this.hbar_offset = this.hbar.Value;
			if (this.hbar_offset < 0)
			{
				this.hbar_offset = 0;
			}
			XplatUI.ScrollWindow(base.Handle, this.ViewportRectangle, num - this.hbar_offset, 0, false);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0006EF14 File Offset: 0x0006D114
		internal void ExpandBelow(TreeNode node, int count_to_next)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			int num = ((node.Bounds.Bottom >= 0) ? node.Bounds.Bottom : 0);
			Rectangle rectangle = new Rectangle(0, num, this.ViewportRectangle.Width, this.ViewportRectangle.Height - num);
			int num2 = count_to_next * this.ActualItemHeight;
			if (num2 > 0)
			{
				XplatUI.ScrollWindow(base.Handle, rectangle, 0, num2, false);
			}
			if (this.show_plus_minus)
			{
				base.Invalidate(new Rectangle(0, node.GetY(), base.Width, this.ActualItemHeight));
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
		internal void CollapseBelow(TreeNode node, int count_to_next)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			Rectangle rectangle = new Rectangle(0, node.Bounds.Bottom, this.ViewportRectangle.Width, this.ViewportRectangle.Height - node.Bounds.Bottom);
			int num = count_to_next * this.ActualItemHeight;
			if (num > 0)
			{
				XplatUI.ScrollWindow(base.Handle, rectangle, 0, -num, false);
			}
			if (this.show_plus_minus)
			{
				base.Invalidate(new Rectangle(0, node.GetY(), base.Width, this.ActualItemHeight));
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0006F064 File Offset: 0x0006D264
		private void MouseWheelHandler(object sender, MouseEventArgs e)
		{
			if (this.vbar == null || !this.vbar.is_visible)
			{
				return;
			}
			if (e.Delta < 0)
			{
				this.SetVScrollValue(Math.Min(this.vbar.Value + SystemInformation.MouseWheelScrollLines, this.vbar.Maximum - this.VisibleCount + 1));
				return;
			}
			this.SetVScrollValue(Math.Max(0, this.vbar.Value - SystemInformation.MouseWheelScrollLines));
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0006F0DE File Offset: 0x0006D2DE
		private void VisibleChangedHandler(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				this.UpdateScrollBars(false);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0006F0F0 File Offset: 0x0006D2F0
		private void FontChangedHandler(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				TreeNode topNode = this.TopNode;
				this.InvalidateNodeWidthRecursive(this.root_node);
				this.SetTop(topNode);
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0006F120 File Offset: 0x0006D320
		private void InvalidateNodeWidthRecursive(TreeNode node)
		{
			node.InvalidateWidth();
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.InvalidateNodeWidthRecursive(treeNode);
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0006F180 File Offset: 0x0006D380
		private void GotFocusHandler(object sender, EventArgs e)
		{
			if (this.selected_node != null)
			{
				if (this.selected_node != null)
				{
					this.UpdateNode(this.selected_node);
				}
				return;
			}
			if (this.pre_selected_node != null)
			{
				this.SelectedNode = this.pre_selected_node;
				return;
			}
			this.SelectedNode = this.TopNode;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0006F1C0 File Offset: 0x0006D3C0
		private void LostFocusHandler(object sender, EventArgs e)
		{
			this.UpdateNode(this.SelectedNode);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0006F1D0 File Offset: 0x0006D3D0
		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				base.Focus();
			}
			TreeNode nodeAt = this.GetNodeAt(e.Y);
			if (nodeAt == null)
			{
				return;
			}
			this.mouse_click_node = nodeAt;
			if (this.show_plus_minus && this.IsPlusMinusArea(nodeAt, e.X) && e.Button == MouseButtons.Left)
			{
				nodeAt.Toggle();
				return;
			}
			if (this.checkboxes && this.IsCheckboxArea(nodeAt, e.X) && e.Button == MouseButtons.Left)
			{
				nodeAt.check_reason = TreeViewAction.ByMouse;
				nodeAt.Checked = !nodeAt.Checked;
				this.UpdateNode(nodeAt);
				return;
			}
			if (this.IsSelectableArea(nodeAt, e.X) || this.full_row_select)
			{
				TreeNode treeNode = this.highlighted_node;
				this.highlighted_node = nodeAt;
				if (this.label_edit && e.Clicks == 1 && this.highlighted_node == treeNode && e.Button == MouseButtons.Left)
				{
					this.BeginEdit(nodeAt);
				}
				else if (this.highlighted_node != this.focused_node)
				{
					Size dragSize = SystemInformation.DragSize;
					this.mouse_rect.X = e.X - dragSize.Width;
					this.mouse_rect.Y = e.Y - dragSize.Height;
					this.mouse_rect.Width = dragSize.Width * 2;
					this.mouse_rect.Height = dragSize.Height * 2;
					this.select_mmove = true;
				}
				base.Invalidate(this.highlighted_node.Bounds);
				if (treeNode != null)
				{
					base.Invalidate(this.Bloat(treeNode.Bounds));
				}
				this.drag_begin_x = e.X;
				this.drag_begin_y = e.Y;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0006F384 File Offset: 0x0006D584
		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.GetNodeAt(e.Y);
			if (nodeAt != null && nodeAt == this.mouse_click_node)
			{
				if (e.Clicks == 2)
				{
					this.OnNodeMouseDoubleClick(new TreeNodeMouseClickEventArgs(nodeAt, e.Button, e.Clicks, e.X, e.Y));
				}
				else
				{
					this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(nodeAt, e.Button, e.Clicks, e.X, e.Y));
				}
			}
			this.mouse_click_node = null;
			this.drag_begin_x = -1;
			this.drag_begin_y = -1;
			if (!this.select_mmove)
			{
				return;
			}
			this.select_mmove = false;
			if (e.Button == MouseButtons.Right && this.selected_node != null)
			{
				base.Invalidate(this.highlighted_node.Bounds);
				this.highlighted_node = this.selected_node;
				base.Invalidate(this.selected_node.Bounds);
				return;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.highlighted_node, false, TreeViewAction.ByMouse);
			this.OnBeforeSelect(treeViewCancelEventArgs);
			if (!treeViewCancelEventArgs.Cancel)
			{
				TreeNode treeNode = this.focused_node;
				TreeNode treeNode2 = this.highlighted_node;
				this.selected_node = this.highlighted_node;
				this.focused_node = this.highlighted_node;
				this.OnAfterSelect(new TreeViewEventArgs(this.selected_node, TreeViewAction.ByMouse));
				if (treeNode2 != null)
				{
					Rectangle rectangle;
					if (treeNode != null)
					{
						rectangle = Rectangle.Union(this.Bloat(treeNode.Bounds), this.Bloat(treeNode2.Bounds));
					}
					else
					{
						rectangle = this.Bloat(treeNode2.Bounds);
					}
					rectangle.X = 0;
					rectangle.Width = this.ViewportRectangle.Width;
					base.Invalidate(rectangle);
					return;
				}
			}
			else
			{
				if (this.highlighted_node != null)
				{
					base.Invalidate(this.highlighted_node.Bounds);
				}
				this.highlighted_node = this.focused_node;
				this.selected_node = this.focused_node;
				if (this.selected_node != null)
				{
					base.Invalidate(this.selected_node.Bounds);
				}
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0006F568 File Offset: 0x0006D768
		private void MouseMoveHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.GetNodeAt(e.Location);
			if (nodeAt != this.tooltip_currently_showing)
			{
				this.MouseLeftItem(this.tooltip_currently_showing);
			}
			if (nodeAt != null && nodeAt != this.tooltip_currently_showing)
			{
				this.MouseEnteredItem(nodeAt);
			}
			if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.drag_begin_x != -1 && this.drag_begin_y != -1)
			{
				double num = Math.Pow((double)(this.drag_begin_x - e.X), 2.0);
				double num2 = Math.Pow((double)(this.drag_begin_y - e.Y), 2.0);
				if (Math.Sqrt(num + num2) > 3.0)
				{
					TreeNode nodeAtUseX = this.GetNodeAtUseX(e.X, e.Y);
					if (nodeAtUseX != null)
					{
						this.OnItemDrag(new ItemDragEventArgs(e.Button, nodeAtUseX));
					}
					this.drag_begin_x = -1;
					this.drag_begin_y = -1;
				}
			}
			if (!this.select_mmove || this.mouse_rect.Contains(e.X, e.Y))
			{
				return;
			}
			base.Invalidate(this.highlighted_node.Bounds);
			if (this.selected_node != null)
			{
				base.Invalidate(this.selected_node.Bounds);
			}
			if (this.focused_node != null)
			{
				base.Invalidate(this.focused_node.Bounds);
			}
			this.highlighted_node = this.selected_node;
			this.focused_node = this.selected_node;
			this.select_mmove = false;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0006F6E4 File Offset: 0x0006D8E4
		private void DoubleClickHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAtUseX = this.GetNodeAtUseX(e.X, e.Y);
			if (nodeAtUseX != null && nodeAtUseX.Nodes.Count > 0)
			{
				nodeAtUseX.Toggle();
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0006F71B File Offset: 0x0006D91B
		private bool RectsIntersect(Rectangle r, int left, int top, int width, int height)
		{
			return r.Left <= left + width && r.Right >= left && r.Top <= top + height && r.Bottom >= top;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0006F750 File Offset: 0x0006D950
		private bool WmContextMenu(ref Message m)
		{
			Point point = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			TreeNode treeNode;
			if (point.X == -1 || point.Y == -1)
			{
				treeNode = this.SelectedNode;
				if (treeNode == null)
				{
					return false;
				}
				point = new Point(treeNode.Bounds.Left, treeNode.Bounds.Top + treeNode.Bounds.Height / 2);
			}
			else
			{
				point = base.PointToClient(point);
				treeNode = this.GetNodeAt(point);
				if (treeNode == null)
				{
					return false;
				}
			}
			if (treeNode.ContextMenu != null)
			{
				treeNode.ContextMenu.Show(this, point);
				return true;
			}
			if (treeNode.ContextMenuStrip != null)
			{
				treeNode.ContextMenuStrip.Show(this, point);
				return true;
			}
			return false;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0006F828 File Offset: 0x0006DA28
		private void MouseEnteredItem(TreeNode item)
		{
			this.tooltip_currently_showing = item;
			if (!this.is_hovering)
			{
				return;
			}
			if (this.ShowNodeToolTips && !string.IsNullOrEmpty(this.tooltip_currently_showing.ToolTipText))
			{
				this.ToolTipWindow.Present(this, this.tooltip_currently_showing.ToolTipText);
			}
			this.OnNodeMouseHover(new TreeNodeMouseHoverEventArgs(this.tooltip_currently_showing));
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0006F887 File Offset: 0x0006DA87
		private void MouseLeftItem(TreeNode item)
		{
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0006F89C File Offset: 0x0006DA9C
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
		internal void OnUIALabelEditChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TreeView.UIALabelEditChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0006F8E8 File Offset: 0x0006DAE8
		internal void OnUIANodeTextChanged(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.UIANodeTextChangedEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0006F918 File Offset: 0x0006DB18
		internal void OnUIACollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)base.Events[TreeView.UIACollectionChangedEvent];
			if (collectionChangeEventHandler != null)
			{
				if (sender == this.root_node)
				{
					sender = this;
				}
				collectionChangeEventHandler(sender, e);
			}
		}

		// Token: 0x04000D07 RID: 3335
		private string path_separator = "\\";

		// Token: 0x04000D08 RID: 3336
		private int item_height = -1;

		// Token: 0x04000D09 RID: 3337
		internal bool sorted;

		// Token: 0x04000D0A RID: 3338
		internal TreeNode root_node;

		// Token: 0x04000D0B RID: 3339
		private TreeNodeCollection nodes;

		// Token: 0x04000D0C RID: 3340
		private TreeViewAction selection_action;

		// Token: 0x04000D0D RID: 3341
		internal TreeNode selected_node;

		// Token: 0x04000D0E RID: 3342
		private TreeNode pre_selected_node;

		// Token: 0x04000D0F RID: 3343
		private TreeNode focused_node;

		// Token: 0x04000D10 RID: 3344
		internal TreeNode highlighted_node;

		// Token: 0x04000D11 RID: 3345
		private Rectangle mouse_rect;

		// Token: 0x04000D12 RID: 3346
		private bool select_mmove;

		// Token: 0x04000D13 RID: 3347
		private ImageList image_list;

		// Token: 0x04000D14 RID: 3348
		private int image_index = -1;

		// Token: 0x04000D15 RID: 3349
		private int selected_image_index = -1;

		// Token: 0x04000D16 RID: 3350
		private string image_key;

		// Token: 0x04000D17 RID: 3351
		private bool is_hovering;

		// Token: 0x04000D18 RID: 3352
		private TreeNode mouse_click_node;

		// Token: 0x04000D19 RID: 3353
		private string selected_image_key;

		// Token: 0x04000D1A RID: 3354
		private bool show_node_tool_tips;

		// Token: 0x04000D1B RID: 3355
		private ImageList state_image_list;

		// Token: 0x04000D1C RID: 3356
		private TreeNode tooltip_currently_showing;

		// Token: 0x04000D1D RID: 3357
		private ToolTip tooltip_window;

		// Token: 0x04000D1E RID: 3358
		private bool full_row_select;

		// Token: 0x04000D1F RID: 3359
		private bool hot_tracking;

		// Token: 0x04000D20 RID: 3360
		private int indent = 19;

		// Token: 0x04000D21 RID: 3361
		private NodeLabelEditEventArgs edit_args;

		// Token: 0x04000D22 RID: 3362
		private LabelEditTextBox edit_text_box;

		// Token: 0x04000D23 RID: 3363
		internal TreeNode edit_node;

		// Token: 0x04000D24 RID: 3364
		private bool checkboxes;

		// Token: 0x04000D25 RID: 3365
		private bool label_edit;

		// Token: 0x04000D26 RID: 3366
		private bool scrollable = true;

		// Token: 0x04000D27 RID: 3367
		private bool show_lines = true;

		// Token: 0x04000D28 RID: 3368
		private bool show_root_lines = true;

		// Token: 0x04000D29 RID: 3369
		private bool show_plus_minus = true;

		// Token: 0x04000D2A RID: 3370
		private bool hide_selection = true;

		// Token: 0x04000D2B RID: 3371
		private int max_visible_order = -1;

		// Token: 0x04000D2C RID: 3372
		internal VScrollBar vbar;

		// Token: 0x04000D2D RID: 3373
		internal HScrollBar hbar;

		// Token: 0x04000D2E RID: 3374
		private bool vbar_bounds_set;

		// Token: 0x04000D2F RID: 3375
		private bool hbar_bounds_set;

		// Token: 0x04000D30 RID: 3376
		internal int skipped_nodes;

		// Token: 0x04000D31 RID: 3377
		internal int hbar_offset;

		// Token: 0x04000D32 RID: 3378
		private int update_stack;

		// Token: 0x04000D33 RID: 3379
		private bool update_needed;

		// Token: 0x04000D34 RID: 3380
		private Pen dash;

		// Token: 0x04000D35 RID: 3381
		private Color line_color;

		// Token: 0x04000D36 RID: 3382
		private StringFormat string_format;

		// Token: 0x04000D37 RID: 3383
		private int drag_begin_x = -1;

		// Token: 0x04000D38 RID: 3384
		private int drag_begin_y = -1;

		// Token: 0x04000D39 RID: 3385
		private long handle_count = 1L;

		// Token: 0x04000D3A RID: 3386
		private TreeViewDrawMode draw_mode;

		// Token: 0x04000D3B RID: 3387
		private IComparer tree_view_node_sorter;

		// Token: 0x04000D3C RID: 3388
		private static object ItemDragEvent = new object();

		// Token: 0x04000D3D RID: 3389
		private static object AfterCheckEvent = new object();

		// Token: 0x04000D3E RID: 3390
		private static object AfterCollapseEvent = new object();

		// Token: 0x04000D3F RID: 3391
		private static object AfterExpandEvent = new object();

		// Token: 0x04000D40 RID: 3392
		private static object AfterLabelEditEvent = new object();

		// Token: 0x04000D41 RID: 3393
		private static object AfterSelectEvent = new object();

		// Token: 0x04000D42 RID: 3394
		private static object BeforeCheckEvent = new object();

		// Token: 0x04000D43 RID: 3395
		private static object BeforeCollapseEvent = new object();

		// Token: 0x04000D44 RID: 3396
		private static object BeforeExpandEvent = new object();

		// Token: 0x04000D45 RID: 3397
		private static object BeforeLabelEditEvent = new object();

		// Token: 0x04000D46 RID: 3398
		private static object BeforeSelectEvent = new object();

		// Token: 0x04000D47 RID: 3399
		private static object DrawNodeEvent = new object();

		// Token: 0x04000D48 RID: 3400
		private static object NodeMouseClickEvent = new object();

		// Token: 0x04000D49 RID: 3401
		private static object NodeMouseDoubleClickEvent = new object();

		// Token: 0x04000D4A RID: 3402
		private static object NodeMouseHoverEvent = new object();

		// Token: 0x04000D4B RID: 3403
		private static object RightToLeftLayoutChangedEvent = new object();

		// Token: 0x04000D4C RID: 3404
		private static object UIACheckBoxesChangedEvent = new object();

		// Token: 0x04000D4D RID: 3405
		private static object UIALabelEditChangedEvent = new object();

		// Token: 0x04000D4E RID: 3406
		private static object UIANodeTextChangedEvent = new object();

		// Token: 0x04000D4F RID: 3407
		private static object UIACollectionChangedEvent = new object();
	}
}
