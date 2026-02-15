using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality of a scroll bar control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000179 RID: 377
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Scroll")]
	[DefaultProperty("Value")]
	public abstract class ScrollBar : Control
	{
		/// <summary>Occurs when the scroll box has been moved by either a mouse or keyboard action.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000E34 RID: 3636 RVA: 0x00040C4A File Offset: 0x0003EE4A
		// (remove) Token: 0x06000E35 RID: 3637 RVA: 0x00040C5D File Offset: 0x0003EE5D
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollBar.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property is changed, either by a <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event or programmatically.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000E36 RID: 3638 RVA: 0x00040C70 File Offset: 0x0003EE70
		// (remove) Token: 0x06000E37 RID: 3639 RVA: 0x00040C83 File Offset: 0x0003EE83
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(ScrollBar.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.ValueChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollBar" /> class.</summary>
		// Token: 0x06000E38 RID: 3640 RVA: 0x00040C98 File Offset: 0x0003EE98
		public ScrollBar()
		{
			this.position = 0;
			this.minimum = 0;
			this.maximum = 100;
			this.large_change = 10;
			this.small_change = 1;
			this.timer.Tick += this.OnTimer;
			base.MouseEnter += this.OnMouseEnter;
			base.MouseLeave += this.OnMouseLeave;
			base.KeyDown += this.OnKeyDownSB;
			base.MouseDown += this.OnMouseDownSB;
			base.MouseUp += this.OnMouseUpSB;
			base.MouseMove += this.OnMouseMoveSB;
			base.Resize += this.OnResizeSB;
			base.TabStop = false;
			base.Cursor = Cursors.Default;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00040D96 File Offset: 0x0003EF96
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00040D9E File Offset: 0x0003EF9E
		internal Rectangle FirstArrowArea
		{
			get
			{
				return this.first_arrow_area;
			}
			set
			{
				this.first_arrow_area = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00040DA7 File Offset: 0x0003EFA7
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00040DAF File Offset: 0x0003EFAF
		internal Rectangle SecondArrowArea
		{
			get
			{
				return this.second_arrow_area;
			}
			set
			{
				this.second_arrow_area = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00040DB8 File Offset: 0x0003EFB8
		private int MaximumAllowed
		{
			get
			{
				if (!this.use_manual_thumb_size)
				{
					return this.maximum - this.LargeChange + 1;
				}
				return this.maximum - this.manual_thumb_size + 1;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00040DE1 File Offset: 0x0003EFE1
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00040DE9 File Offset: 0x0003EFE9
		internal Rectangle ThumbPos
		{
			get
			{
				return this.thumb_pos;
			}
			set
			{
				this.thumb_pos = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00040DF2 File Offset: 0x0003EFF2
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00040DFA File Offset: 0x0003EFFA
		internal bool FirstButtonEntered
		{
			get
			{
				return this.first_button_entered;
			}
			private set
			{
				if (this.first_button_entered == value)
				{
					return;
				}
				this.first_button_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.first_arrow_area);
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00040E25 File Offset: 0x0003F025
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00040E2D File Offset: 0x0003F02D
		internal bool SecondButtonEntered
		{
			get
			{
				return this.second_button_entered;
			}
			private set
			{
				if (this.second_button_entered == value)
				{
					return;
				}
				this.second_button_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.second_arrow_area);
				}
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00040E58 File Offset: 0x0003F058
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00040E60 File Offset: 0x0003F060
		internal bool ThumbEntered
		{
			get
			{
				return this.thumb_entered;
			}
			private set
			{
				if (this.thumb_entered == value)
				{
					return;
				}
				this.thumb_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.thumb_pos);
				}
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00040E8B File Offset: 0x0003F08B
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x00040E93 File Offset: 0x0003F093
		internal bool ThumbPressed
		{
			get
			{
				return this.thumb_pressed;
			}
			private set
			{
				if (this.thumb_pressed == value)
				{
					return;
				}
				this.thumb_pressed = value;
				if (ThemeEngine.Current.ScrollBarHasPressedThumbStyle)
				{
					base.Invalidate(this.thumb_pos);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBar" /> is automatically resized to fit its contents.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ScrollBar" /> should be automatically resized to fit its contents; otherwise, false.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x000042C5 File Offset: 0x000024C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00005B54 File Offset: 0x00003D54
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.BackColor == value)
				{
					return;
				}
				base.BackColor = value;
				this.Refresh();
			}
		}

		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00005B72 File Offset: 0x00003D72
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00005B82 File Offset: 0x00003D82
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default distance between the <see cref="T:System.Windows.Forms.ScrollBar" /> control edges and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00040EBE File Offset: 0x0003F0BE
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00040EC5 File Offset: 0x0003F0C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.Font.Equals(value))
				{
					return;
				}
				base.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the scroll bar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color for this scroll bar control. The default is the foreground color of the parent control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00005E67 File Offset: 0x00004067
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.ForeColor == value)
				{
					return;
				}
				base.ForeColor = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a large distance.</summary>
		/// <returns>A numeric value. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00040EDD File Offset: 0x0003F0DD
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00040EFC File Offset: 0x0003F0FC
		[DefaultValue(10)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFDescription("Scroll amount when clicking in the scroll area")]
		[MWFCategory("Behaviour")]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.large_change, this.maximum - this.minimum + 1);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LargeChange", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				if (this.large_change != value)
				{
					this.large_change = value;
					this.CalcThumbArea();
					this.UpdatePos(this.Value, true);
					this.InvalidateDirty();
					this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.LargeIncrement, value));
				}
			}
		}

		/// <summary>Gets or sets the upper limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 100.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00040F5E File Offset: 0x0003F15E
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00040F68 File Offset: 0x0003F168
		[DefaultValue(100)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFDescription("Highest value for scrollbar")]
		[MWFCategory("Behaviour")]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.maximum == value)
				{
					return;
				}
				this.maximum = value;
				this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.Last, value));
				if (this.maximum < this.minimum)
				{
					this.minimum = this.maximum;
				}
				if (this.Value > this.maximum)
				{
					this.Value = this.maximum;
				}
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00040FE0 File Offset: 0x0003F1E0
		internal void SetValues(int maximum, int large_change)
		{
			this.SetValues(-1, maximum, -1, large_change);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00040FEC File Offset: 0x0003F1EC
		internal void SetValues(int minimum, int maximum, int small_change, int large_change)
		{
			bool flag = false;
			if (-1 != minimum && this.minimum != minimum)
			{
				this.minimum = minimum;
				if (minimum > this.maximum)
				{
					this.maximum = minimum;
				}
				flag = true;
				this.position = Math.Max(this.position, minimum);
			}
			if (-1 != maximum && this.maximum != maximum)
			{
				this.maximum = maximum;
				if (maximum < this.minimum)
				{
					this.minimum = maximum;
				}
				flag = true;
				this.position = Math.Min(this.position, maximum);
			}
			if (-1 != small_change && this.small_change != small_change)
			{
				this.small_change = small_change;
			}
			if (this.large_change != large_change)
			{
				this.large_change = large_change;
				flag = true;
			}
			if (flag)
			{
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		/// <summary>Gets or sets the lower limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x000410AF File Offset: 0x0003F2AF
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x000410B8 File Offset: 0x0003F2B8
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFDescription("Smallest value for scrollbar")]
		[MWFCategory("Behaviour")]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.minimum == value)
				{
					return;
				}
				this.minimum = value;
				this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.First, value));
				if (this.minimum > this.maximum)
				{
					this.maximum = this.minimum;
				}
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		/// <summary>Gets or sets the value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a small distance.</summary>
		/// <returns>A numeric value. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00041116 File Offset: 0x0003F316
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00041134 File Offset: 0x0003F334
		[DefaultValue(1)]
		[MWFDescription("Scroll amount when clicking scroll arrows")]
		[MWFCategory("Behaviour")]
		public int SmallChange
		{
			get
			{
				if (this.small_change <= this.LargeChange)
				{
					return this.small_change;
				}
				return this.LargeChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SmallChange", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				if (this.small_change != value)
				{
					this.small_change = value;
					this.UpdatePos(this.Value, true);
					this.InvalidateDirty();
					this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.SmallIncrement, value));
				}
			}
		}

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x000043BC File Offset: 0x000025BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the scroll bar control.</summary>
		/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> and <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> range. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> property value.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> property value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00041190 File Offset: 0x0003F390
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00041198 File Offset: 0x0003F398
		[Bindable(true)]
		[DefaultValue(0)]
		[MWFDescription("Current value for scrollbar")]
		[MWFCategory("Behaviour")]
		public int Value
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value < this.minimum || value > this.maximum)
				{
					throw new ArgumentOutOfRangeException("Value", string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
				}
				if (this.position != value)
				{
					this.position = value;
					this.OnValueChanged(EventArgs.Empty);
					if (base.IsHandleCreated)
					{
						Rectangle rectangle = this.thumb_pos;
						this.UpdateThumbPos((this.vert ? this.thumb_area.Y : this.thumb_area.X) + (int)((float)(this.position - this.minimum) * this.pixel_per_pos), false, false);
						this.MoveThumb(rectangle, this.vert ? this.thumb_pos.Y : this.thumb_pos.X);
					}
				}
			}
		}

		/// <summary>Returns the bounds to use when the <see cref="T:System.Windows.Forms.ScrollBar" /> is scaled by a specified amount.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> specifying the scaled bounds.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the initial bounds.</param>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> that indicates the amount the current bounds should be increased by.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicate the how to define the control's size and position returned by <see cref="M:System.Windows.Forms.ScrollBar.GetScaledBounds(System.Drawing.Rectangle,System.Drawing.SizeF,System.Windows.Forms.BoundsSpecified)" />. </param>
		// Token: 0x06000E63 RID: 3683 RVA: 0x00041265 File Offset: 0x0003F465
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.vert)
			{
				return base.GetScaledBounds(bounds, factor, (specified & BoundsSpecified.Height) | (specified & BoundsSpecified.Location));
			}
			return base.GetScaledBounds(bounds, factor, (specified & BoundsSpecified.Width) | (specified & BoundsSpecified.Location));
		}

		/// <param name="e">The event data.</param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x00041290 File Offset: 0x0003F490
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (base.Enabled)
			{
				this.firstbutton_state = (this.secondbutton_state = ButtonState.Normal);
			}
			else
			{
				this.firstbutton_state = (this.secondbutton_state = ButtonState.Inactive);
			}
			this.Refresh();
		}

		/// <param name="e">The event data.</param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x000412D8 File Offset: 0x0003F4D8
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CalcButtonSizes();
			this.CalcThumbArea();
			this.UpdateThumbPos(this.thumb_area.Y + (int)((float)(this.position - this.minimum) * this.pixel_per_pos), true, false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06000E66 RID: 3686 RVA: 0x00041318 File Offset: 0x0003F518
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.ScrollEvent];
			if (scrollEventHandler == null)
			{
				return;
			}
			if (se.NewValue < this.Minimum)
			{
				se.NewValue = this.Minimum;
			}
			if (se.NewValue > this.Maximum)
			{
				se.NewValue = this.Maximum;
			}
			scrollEventHandler(this, se);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0004137C File Offset: 0x0003F57C
		private void SendWMScroll(ScrollBarCommands cmd)
		{
			if (base.Parent != null && base.Parent.IsHandleCreated)
			{
				if (this.vert)
				{
					XplatUI.SendMessage(base.Parent.Handle, Msg.WM_VSCROLL, (IntPtr)((int)cmd), this.implicit_control ? IntPtr.Zero : base.Handle);
					return;
				}
				XplatUI.SendMessage(base.Parent.Handle, Msg.WM_HSCROLL, (IntPtr)((int)cmd), this.implicit_control ? IntPtr.Zero : base.Handle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000E68 RID: 3688 RVA: 0x0004140C File Offset: 0x0003F60C
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ScrollBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ScrollBar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E69 RID: 3689 RVA: 0x0004143C File Offset: 0x0003F63C
		public override string ToString()
		{
			return string.Format("{0}, Minimum: {1}, Maximum: {2}, Value: {3}", new object[]
			{
				base.GetType().FullName,
				this.minimum,
				this.maximum,
				this.position
			});
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">A Windows Message object.</param>
		// Token: 0x06000E6A RID: 3690 RVA: 0x00020975 File Offset: 0x0001EB75
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00041494 File Offset: 0x0003F694
		private void CalcButtonSizes()
		{
			if (this.vert)
			{
				if (base.Height < ThemeEngine.Current.ScrollBarButtonSize * 2)
				{
					this.scrollbutton_height = base.Height / 2;
					return;
				}
				this.scrollbutton_height = ThemeEngine.Current.ScrollBarButtonSize;
				return;
			}
			else
			{
				if (base.Width < ThemeEngine.Current.ScrollBarButtonSize * 2)
				{
					this.scrollbutton_width = base.Width / 2;
					return;
				}
				this.scrollbutton_width = ThemeEngine.Current.ScrollBarButtonSize;
				return;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00041510 File Offset: 0x0003F710
		private void CalcThumbArea()
		{
			int num = (this.use_manual_thumb_size ? this.manual_thumb_size : this.LargeChange);
			if (this.vert)
			{
				this.thumb_area.Height = base.Height - this.scrollbutton_height - this.scrollbutton_height;
				this.thumb_area.X = 0;
				this.thumb_area.Y = this.scrollbutton_height;
				this.thumb_area.Width = base.Width;
				if (base.Height < 40)
				{
					this.thumb_size = 0;
				}
				else
				{
					double num2 = (double)num / (double)(1 + this.maximum - this.minimum);
					this.thumb_size = 1 + (int)((double)this.thumb_area.Height * num2);
					if (this.thumb_size < 8)
					{
						this.thumb_size = 8;
					}
					if (this.LargeChange == 0)
					{
						this.thumb_size = 17;
					}
				}
				this.pixel_per_pos = (float)(this.thumb_area.Height - this.thumb_size) / (float)(this.maximum - this.minimum - num + 1);
				return;
			}
			this.thumb_area.Y = 0;
			this.thumb_area.X = this.scrollbutton_width;
			this.thumb_area.Height = base.Height;
			this.thumb_area.Width = base.Width - this.scrollbutton_width - this.scrollbutton_width;
			if (base.Width < 40)
			{
				this.thumb_size = 0;
			}
			else
			{
				double num3 = (double)num / (double)(1 + this.maximum - this.minimum);
				this.thumb_size = 1 + (int)((double)this.thumb_area.Width * num3);
				if (this.thumb_size < 8)
				{
					this.thumb_size = 8;
				}
				if (this.LargeChange == 0)
				{
					this.thumb_size = 17;
				}
			}
			this.pixel_per_pos = (float)(this.thumb_area.Width - this.thumb_size) / (float)(this.maximum - this.minimum - num + 1);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000416F0 File Offset: 0x0003F8F0
		private void LargeIncrement()
		{
			int num = Math.Min(this.MaximumAllowed, this.position + this.large_change);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.LargeIncrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.LargeIncrement, this.Value));
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00041764 File Offset: 0x0003F964
		private void LargeDecrement()
		{
			int num = Math.Max(this.Minimum, this.position - this.large_change);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.LargeDecrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.LargeDecrement, this.Value));
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000417D7 File Offset: 0x0003F9D7
		private void OnResizeSB(object o, EventArgs e)
		{
			if (base.Width <= 0 || base.Height <= 0)
			{
				return;
			}
			this.CalcButtonSizes();
			this.CalcThumbArea();
			this.UpdatePos(this.position, true);
			this.Refresh();
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0004180B File Offset: 0x0003FA0B
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawScrollBar(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00041824 File Offset: 0x0003FA24
		private void OnTimer(object source, EventArgs e)
		{
			this.ClearDirty();
			switch (this.timer_type)
			{
			case ScrollBar.TimerType.HoldButton:
				this.SetRepeatButtonTimer();
				break;
			case ScrollBar.TimerType.RepeatButton:
				if ((this.firstbutton_state & ButtonState.Pushed) == ButtonState.Pushed && this.position != this.Minimum)
				{
					this.SmallDecrement();
					this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				}
				if ((this.secondbutton_state & ButtonState.Pushed) == ButtonState.Pushed && this.position != this.Maximum)
				{
					this.SmallIncrement();
					this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				}
				break;
			case ScrollBar.TimerType.HoldThumbArea:
				this.SetRepeatThumbAreaTimer();
				break;
			case ScrollBar.TimerType.RepeatThumbArea:
			{
				Rectangle rectangle = this.thumb_area;
				Point point = base.PointToScreen(new Point(this.thumb_area.X, this.thumb_area.Y));
				rectangle.X = point.X;
				rectangle.Y = point.Y;
				if (!rectangle.Contains(Control.MousePosition))
				{
					this.timer.Enabled = false;
					this.thumb_moving = ScrollBar.ThumbMoving.None;
					this.DirtyThumbArea();
					this.InvalidateDirty();
				}
				Point point2 = base.PointToClient(Control.MousePosition);
				if (this.vert)
				{
					this.lastclick_pos = point2.Y;
				}
				else
				{
					this.lastclick_pos = point2.X;
				}
				if (this.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					if ((this.vert && this.thumb_pos.Y + this.thumb_size > this.lastclick_pos) || (!this.vert && this.thumb_pos.X + this.thumb_size > this.lastclick_pos) || !this.thumb_area.Contains(point2))
					{
						this.timer.Enabled = false;
						this.thumb_moving = ScrollBar.ThumbMoving.None;
						this.Refresh();
						return;
					}
					this.LargeIncrement();
					this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
				}
				else if ((this.vert && this.thumb_pos.Y < this.lastclick_pos) || (!this.vert && this.thumb_pos.X < this.lastclick_pos))
				{
					this.timer.Enabled = false;
					this.thumb_moving = ScrollBar.ThumbMoving.None;
					this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
					this.Refresh();
				}
				else
				{
					this.LargeDecrement();
					this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
				}
				break;
			}
			}
			this.InvalidateDirty();
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00041A68 File Offset: 0x0003FC68
		private void MoveThumb(Rectangle original_thumbpos, int value)
		{
			if (this.vert)
			{
				int num = value - original_thumbpos.Y;
				if (num < 0)
				{
					original_thumbpos.Y += num;
					original_thumbpos.Height -= num;
				}
				else
				{
					original_thumbpos.Height += num;
				}
				XplatUI.ScrollWindow(base.Handle, original_thumbpos, 0, num, false);
			}
			else
			{
				int num = value - original_thumbpos.X;
				if (num < 0)
				{
					original_thumbpos.X += num;
					original_thumbpos.Width -= num;
				}
				else
				{
					original_thumbpos.Width += num;
				}
				XplatUI.ScrollWindow(base.Handle, original_thumbpos, num, 0, false);
			}
			base.Update();
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00041B20 File Offset: 0x0003FD20
		private void OnMouseMoveSB(object sender, MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.FirstButtonEntered = this.first_arrow_area.Contains(e.Location);
			this.SecondButtonEntered = this.second_arrow_area.Contains(e.Location);
			if (this.thumb_size == 0)
			{
				return;
			}
			this.ThumbEntered = this.thumb_pos.Contains(e.Location);
			if (this.firstbutton_pressed)
			{
				if (!this.first_arrow_area.Contains(e.X, e.Y) && (this.firstbutton_state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					this.firstbutton_state = ButtonState.Normal;
					base.Invalidate(this.first_arrow_area);
					base.Update();
					return;
				}
				if (this.first_arrow_area.Contains(e.X, e.Y) && (this.firstbutton_state & ButtonState.Normal) == ButtonState.Normal)
				{
					this.firstbutton_state = ButtonState.Pushed;
					base.Invalidate(this.first_arrow_area);
					base.Update();
					return;
				}
			}
			else if (this.secondbutton_pressed)
			{
				if (!this.second_arrow_area.Contains(e.X, e.Y) && (this.secondbutton_state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					this.secondbutton_state = ButtonState.Normal;
					base.Invalidate(this.second_arrow_area);
					base.Update();
					return;
				}
				if (this.second_arrow_area.Contains(e.X, e.Y) && (this.secondbutton_state & ButtonState.Normal) == ButtonState.Normal)
				{
					this.secondbutton_state = ButtonState.Pushed;
					base.Invalidate(this.second_arrow_area);
					base.Update();
					return;
				}
			}
			else if (this.thumb_pressed)
			{
				if (this.vert)
				{
					int num = e.Y - this.thumbclick_offset;
					if (num < this.thumb_area.Y)
					{
						num = this.thumb_area.Y;
					}
					else if (num > this.thumb_area.Bottom - this.thumb_size)
					{
						num = this.thumb_area.Bottom - this.thumb_size;
					}
					if (num != this.thumb_pos.Y)
					{
						Rectangle rectangle = this.thumb_pos;
						this.UpdateThumbPos(num, false, true);
						this.MoveThumb(rectangle, this.thumb_pos.Y);
						this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, this.position));
					}
					this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
					return;
				}
				int num2 = e.X - this.thumbclick_offset;
				if (num2 < this.thumb_area.X)
				{
					num2 = this.thumb_area.X;
				}
				else if (num2 > this.thumb_area.Right - this.thumb_size)
				{
					num2 = this.thumb_area.Right - this.thumb_size;
				}
				if (num2 != this.thumb_pos.X)
				{
					Rectangle rectangle2 = this.thumb_pos;
					this.UpdateThumbPos(num2, false, true);
					this.MoveThumb(rectangle2, this.thumb_pos.X);
					this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, this.position));
				}
				this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00041E04 File Offset: 0x00040004
		private void OnMouseDownSB(object sender, MouseEventArgs e)
		{
			this.ClearDirty();
			if (!base.Enabled || (e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			if (this.firstbutton_state != ButtonState.Inactive && this.first_arrow_area.Contains(e.X, e.Y))
			{
				this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				this.firstbutton_state = ButtonState.Pushed;
				this.firstbutton_pressed = true;
				base.Invalidate(this.first_arrow_area);
				base.Update();
				if (!this.timer.Enabled)
				{
					this.SetHoldButtonClickTimer();
					this.timer.Enabled = true;
				}
			}
			if (this.secondbutton_state != ButtonState.Inactive && this.second_arrow_area.Contains(e.X, e.Y))
			{
				this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				this.secondbutton_state = ButtonState.Pushed;
				this.secondbutton_pressed = true;
				base.Invalidate(this.second_arrow_area);
				base.Update();
				if (!this.timer.Enabled)
				{
					this.SetHoldButtonClickTimer();
					this.timer.Enabled = true;
				}
			}
			if (this.thumb_size <= 0 || !this.thumb_pos.Contains(e.X, e.Y))
			{
				if (this.thumb_size > 0 && this.thumb_area.Contains(e.X, e.Y))
				{
					if (this.vert)
					{
						this.lastclick_pos = e.Y;
						if (e.Y > this.thumb_pos.Y + this.thumb_pos.Height)
						{
							this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
							this.LargeIncrement();
							this.thumb_moving = ScrollBar.ThumbMoving.Forward;
							this.Dirty(new Rectangle(0, this.thumb_pos.Y + this.thumb_pos.Height, base.ClientRectangle.Width, base.ClientRectangle.Height - (this.thumb_pos.Y + this.thumb_pos.Height) - this.scrollbutton_height));
						}
						else
						{
							this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
							this.LargeDecrement();
							this.thumb_moving = ScrollBar.ThumbMoving.Backwards;
							this.Dirty(new Rectangle(0, this.scrollbutton_height, base.ClientRectangle.Width, this.thumb_pos.Y - this.scrollbutton_height));
						}
					}
					else
					{
						this.lastclick_pos = e.X;
						if (e.X > this.thumb_pos.X + this.thumb_pos.Width)
						{
							this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
							this.thumb_moving = ScrollBar.ThumbMoving.Forward;
							this.LargeIncrement();
							this.Dirty(new Rectangle(this.thumb_pos.X + this.thumb_pos.Width, 0, base.ClientRectangle.Width - (this.thumb_pos.X + this.thumb_pos.Width) - this.scrollbutton_width, base.ClientRectangle.Height));
						}
						else
						{
							this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
							this.thumb_moving = ScrollBar.ThumbMoving.Backwards;
							this.LargeDecrement();
							this.Dirty(new Rectangle(this.scrollbutton_width, 0, this.thumb_pos.X - this.scrollbutton_width, base.ClientRectangle.Height));
						}
					}
					this.SetHoldThumbAreaTimer();
					this.timer.Enabled = true;
					this.InvalidateDirty();
				}
				return;
			}
			this.ThumbPressed = true;
			this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
			if (this.vert)
			{
				this.thumbclick_offset = e.Y - this.thumb_pos.Y;
				this.lastclick_pos = e.Y;
				return;
			}
			this.thumbclick_offset = e.X - this.thumb_pos.X;
			this.lastclick_pos = e.X;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000421B0 File Offset: 0x000403B0
		private void OnMouseUpSB(object sender, MouseEventArgs e)
		{
			this.ClearDirty();
			if (!base.Enabled)
			{
				return;
			}
			this.timer.Enabled = false;
			if (this.thumb_moving != ScrollBar.ThumbMoving.None)
			{
				this.DirtyThumbArea();
				this.thumb_moving = ScrollBar.ThumbMoving.None;
			}
			if (this.firstbutton_pressed)
			{
				this.firstbutton_state = ButtonState.Normal;
				if (this.first_arrow_area.Contains(e.X, e.Y))
				{
					this.SmallDecrement();
				}
				this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				this.firstbutton_pressed = false;
				this.Dirty(this.first_arrow_area);
			}
			else if (this.secondbutton_pressed)
			{
				this.secondbutton_state = ButtonState.Normal;
				if (this.second_arrow_area.Contains(e.X, e.Y))
				{
					this.SmallIncrement();
				}
				this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				this.Dirty(this.second_arrow_area);
				this.secondbutton_pressed = false;
			}
			else if (this.thumb_pressed)
			{
				this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, this.position));
				this.OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, this.position));
				this.SendWMScroll(ScrollBarCommands.SB_THUMBPOSITION);
				this.ThumbPressed = false;
				return;
			}
			this.InvalidateDirty();
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000422C8 File Offset: 0x000404C8
		private void OnKeyDownSB(object o, KeyEventArgs key)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.ClearDirty();
			switch (key.KeyCode)
			{
			case Keys.PageUp:
				this.LargeDecrement();
				break;
			case Keys.PageDown:
				this.LargeIncrement();
				break;
			case Keys.End:
				this.SetEndPosition();
				break;
			case Keys.Home:
				this.SetHomePosition();
				break;
			case Keys.Up:
				this.SmallDecrement();
				break;
			case Keys.Down:
				this.SmallIncrement();
				break;
			}
			this.InvalidateDirty();
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0004234A File Offset: 0x0004054A
		internal void SafeValueSet(int value)
		{
			value = Math.Min(value, this.maximum);
			value = Math.Max(value, this.minimum);
			this.Value = value;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00042370 File Offset: 0x00040570
		private void SetEndPosition()
		{
			int num = this.MaximumAllowed;
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.Last, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			this.SetValue(num);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000423B8 File Offset: 0x000405B8
		private void SetHomePosition()
		{
			int num = this.Minimum;
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.First, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			this.SetValue(num);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00042400 File Offset: 0x00040600
		private void SmallIncrement()
		{
			int num = Math.Min(this.MaximumAllowed, this.position + this.SmallChange);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.SmallIncrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Value));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00042474 File Offset: 0x00040674
		private void SmallDecrement()
		{
			int num = Math.Max(this.Minimum, this.position - this.SmallChange);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.SmallDecrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Value));
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000424E7 File Offset: 0x000406E7
		private void SetHoldButtonClickTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 200;
			this.timer_type = ScrollBar.TimerType.HoldButton;
			this.timer.Enabled = true;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00042518 File Offset: 0x00040718
		private void SetRepeatButtonTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 50;
			this.timer_type = ScrollBar.TimerType.RepeatButton;
			this.timer.Enabled = true;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00042546 File Offset: 0x00040746
		private void SetHoldThumbAreaTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 200;
			this.timer_type = ScrollBar.TimerType.HoldThumbArea;
			this.timer.Enabled = true;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00042577 File Offset: 0x00040777
		private void SetRepeatThumbAreaTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 50;
			this.timer_type = ScrollBar.TimerType.RepeatThumbArea;
			this.timer.Enabled = true;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000425A8 File Offset: 0x000407A8
		private void UpdatePos(int newPos, bool update_thumbpos)
		{
			int num;
			if (newPos < this.minimum)
			{
				num = this.minimum;
			}
			else if (newPos > this.MaximumAllowed)
			{
				num = this.MaximumAllowed;
			}
			else
			{
				num = newPos;
			}
			if (num < this.minimum)
			{
				num = this.minimum;
			}
			if (num > this.maximum)
			{
				num = this.maximum;
			}
			if (update_thumbpos)
			{
				if (this.vert)
				{
					this.UpdateThumbPos(this.thumb_area.Y + (int)((float)(num - this.minimum) * this.pixel_per_pos), true, false);
				}
				else
				{
					this.UpdateThumbPos(this.thumb_area.X + (int)((float)(num - this.minimum) * this.pixel_per_pos), true, false);
				}
				this.SetValue(num);
				return;
			}
			this.position = num;
			EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00042688 File Offset: 0x00040888
		private void UpdateThumbPos(int pixel, bool dirty, bool update_value)
		{
			float num;
			if (this.vert)
			{
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
				if (pixel < this.thumb_area.Y)
				{
					this.thumb_pos.Y = this.thumb_area.Y;
				}
				else if (pixel > this.thumb_area.Bottom - this.thumb_size)
				{
					this.thumb_pos.Y = this.thumb_area.Bottom - this.thumb_size;
				}
				else
				{
					this.thumb_pos.Y = pixel;
				}
				this.thumb_pos.X = 0;
				this.thumb_pos.Width = ThemeEngine.Current.ScrollBarButtonSize;
				this.thumb_pos.Height = this.thumb_size;
				num = (float)(this.thumb_pos.Y - this.thumb_area.Y);
				num /= this.pixel_per_pos;
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
			}
			else
			{
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
				if (pixel < this.thumb_area.X)
				{
					this.thumb_pos.X = this.thumb_area.X;
				}
				else if (pixel > this.thumb_area.Right - this.thumb_size)
				{
					this.thumb_pos.X = this.thumb_area.Right - this.thumb_size;
				}
				else
				{
					this.thumb_pos.X = pixel;
				}
				this.thumb_pos.Y = 0;
				this.thumb_pos.Width = this.thumb_size;
				this.thumb_pos.Height = ThemeEngine.Current.ScrollBarButtonSize;
				num = (float)(this.thumb_pos.X - this.thumb_area.X);
				num /= this.pixel_per_pos;
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
			}
			if (update_value)
			{
				this.UpdatePos((int)num + this.minimum, false);
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00042874 File Offset: 0x00040A74
		private void SetValue(int value)
		{
			if (value < this.minimum || value > this.maximum)
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
			}
			if (this.position != value)
			{
				this.position = value;
				this.OnValueChanged(EventArgs.Empty);
				this.UpdatePos(value, true);
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000428CC File Offset: 0x00040ACC
		private void ClearDirty()
		{
			this.dirty = Rectangle.Empty;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000428D9 File Offset: 0x00040AD9
		private void Dirty(Rectangle r)
		{
			if (this.dirty == Rectangle.Empty)
			{
				this.dirty = r;
				return;
			}
			this.dirty = Rectangle.Union(this.dirty, r);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00042908 File Offset: 0x00040B08
		private void DirtyThumbArea()
		{
			if (this.thumb_moving != ScrollBar.ThumbMoving.Forward)
			{
				if (this.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					if (this.vert)
					{
						this.Dirty(new Rectangle(0, this.scrollbutton_height, base.ClientRectangle.Width, this.thumb_pos.Y - this.scrollbutton_height));
						return;
					}
					this.Dirty(new Rectangle(this.scrollbutton_width, 0, this.thumb_pos.X - this.scrollbutton_width, base.ClientRectangle.Height));
				}
				return;
			}
			if (this.vert)
			{
				this.Dirty(new Rectangle(0, this.thumb_pos.Y + this.thumb_pos.Height, base.ClientRectangle.Width, base.ClientRectangle.Height - (this.thumb_pos.Y + this.thumb_pos.Height) - this.scrollbutton_height));
				return;
			}
			this.Dirty(new Rectangle(this.thumb_pos.X + this.thumb_pos.Width, 0, base.ClientRectangle.Width - (this.thumb_pos.X + this.thumb_pos.Width) - this.scrollbutton_width, base.ClientRectangle.Height));
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00042A5D File Offset: 0x00040C5D
		private void InvalidateDirty()
		{
			base.Invalidate(this.dirty);
			base.Update();
			this.dirty = Rectangle.Empty;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00042A7C File Offset: 0x00040C7C
		private void OnMouseEnter(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.ScrollBarHasHoverArrowButtonStyle)
			{
				Region region = new Region(this.first_arrow_area);
				region.Union(this.second_arrow_area);
				base.Invalidate(region);
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00042AB4 File Offset: 0x00040CB4
		private void OnMouseLeave(object sender, EventArgs e)
		{
			Region region = new Region();
			region.MakeEmpty();
			bool flag = false;
			if (ThemeEngine.Current.ScrollBarHasHoverArrowButtonStyle)
			{
				region.Union(this.first_arrow_area);
				region.Union(this.second_arrow_area);
				flag = true;
			}
			else if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
			{
				if (this.first_button_entered)
				{
					region.Union(this.first_arrow_area);
					flag = true;
				}
				else if (this.second_button_entered)
				{
					region.Union(this.second_arrow_area);
					flag = true;
				}
			}
			if (ThemeEngine.Current.ScrollBarHasHotElementStyles && this.thumb_entered)
			{
				region.Union(this.thumb_pos);
				flag = true;
			}
			this.first_button_entered = false;
			this.second_button_entered = false;
			this.thumb_entered = false;
			if (flag)
			{
				base.Invalidate(region);
			}
			region.Dispose();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /></param>
		// Token: 0x06000E89 RID: 3721 RVA: 0x00042B78 File Offset: 0x00040D78
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00042B84 File Offset: 0x00040D84
		internal void OnUIAScroll(ScrollEventArgs args)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.UIAScrollEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, args);
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00042BB4 File Offset: 0x00040DB4
		internal void OnUIAValueChanged(ScrollEventArgs args)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.UIAValueChangeEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, args);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00042BE2 File Offset: 0x00040DE2
		// Note: this type is marked as 'beforefieldinit'.
		static ScrollBar()
		{
			ScrollBar.ScrollEvent = new object();
			ScrollBar.ValueChangedEvent = new object();
			ScrollBar.UIAScrollEvent = new object();
			ScrollBar.UIAValueChangeEvent = new object();
		}

		// Token: 0x0400091A RID: 2330
		private int position;

		// Token: 0x0400091B RID: 2331
		private int minimum;

		// Token: 0x0400091C RID: 2332
		private int maximum;

		// Token: 0x0400091D RID: 2333
		private int large_change;

		// Token: 0x0400091E RID: 2334
		private int small_change;

		// Token: 0x0400091F RID: 2335
		internal int scrollbutton_height;

		// Token: 0x04000920 RID: 2336
		internal int scrollbutton_width;

		// Token: 0x04000921 RID: 2337
		private Rectangle first_arrow_area;

		// Token: 0x04000922 RID: 2338
		private Rectangle second_arrow_area;

		// Token: 0x04000923 RID: 2339
		private Rectangle thumb_pos;

		// Token: 0x04000924 RID: 2340
		private Rectangle thumb_area;

		// Token: 0x04000925 RID: 2341
		internal ButtonState firstbutton_state;

		// Token: 0x04000926 RID: 2342
		internal ButtonState secondbutton_state;

		// Token: 0x04000927 RID: 2343
		private bool firstbutton_pressed;

		// Token: 0x04000928 RID: 2344
		private bool secondbutton_pressed;

		// Token: 0x04000929 RID: 2345
		private bool thumb_pressed;

		// Token: 0x0400092A RID: 2346
		private float pixel_per_pos;

		// Token: 0x0400092B RID: 2347
		private Timer timer = new Timer();

		// Token: 0x0400092C RID: 2348
		private ScrollBar.TimerType timer_type;

		// Token: 0x0400092D RID: 2349
		private int thumb_size = 40;

		// Token: 0x0400092E RID: 2350
		internal bool use_manual_thumb_size;

		// Token: 0x0400092F RID: 2351
		internal int manual_thumb_size;

		// Token: 0x04000930 RID: 2352
		internal bool vert;

		// Token: 0x04000931 RID: 2353
		internal bool implicit_control;

		// Token: 0x04000932 RID: 2354
		private int lastclick_pos;

		// Token: 0x04000933 RID: 2355
		private int thumbclick_offset;

		// Token: 0x04000934 RID: 2356
		private Rectangle dirty;

		// Token: 0x04000935 RID: 2357
		internal ScrollBar.ThumbMoving thumb_moving;

		// Token: 0x04000936 RID: 2358
		private bool first_button_entered;

		// Token: 0x04000937 RID: 2359
		private bool second_button_entered;

		// Token: 0x04000938 RID: 2360
		private bool thumb_entered;

		// Token: 0x0400093B RID: 2363
		private static object UIAScrollEvent;

		// Token: 0x0400093C RID: 2364
		private static object UIAValueChangeEvent;

		// Token: 0x0200017A RID: 378
		private enum TimerType
		{
			// Token: 0x0400093E RID: 2366
			HoldButton,
			// Token: 0x0400093F RID: 2367
			RepeatButton,
			// Token: 0x04000940 RID: 2368
			HoldThumbArea,
			// Token: 0x04000941 RID: 2369
			RepeatThumbArea
		}

		// Token: 0x0200017B RID: 379
		internal enum ThumbMoving
		{
			// Token: 0x04000943 RID: 2371
			None,
			// Token: 0x04000944 RID: 2372
			Forward,
			// Token: 0x04000945 RID: 2373
			Backwards
		}
	}
}
