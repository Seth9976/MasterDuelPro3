using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar button. Although <see cref="T:System.Windows.Forms.ToolStripButton" /> replaces and extends the <see cref="T:System.Windows.Forms.ToolBarButton" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBarButton" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B2 RID: 434
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.ToolBarButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class ToolBarButton : Component
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0005F44C File Offset: 0x0005D64C
		internal Image Image
		{
			get
			{
				if (this.Parent == null || this.Parent.ImageList == null)
				{
					return null;
				}
				ImageList imageList = this.Parent.ImageList;
				if (this.ImageIndex > -1 && this.ImageIndex < imageList.Images.Count)
				{
					return imageList.Images[this.ImageIndex];
				}
				if (!string.IsNullOrEmpty(this.image_key))
				{
					return imageList.Images[this.image_key];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the menu to be displayed in the drop-down toolbar button.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> to be displayed in the drop-down toolbar button. The default is null.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned object is not a <see cref="T:System.Windows.Forms.ContextMenu" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0005F4CA File Offset: 0x0005D6CA
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0005F4D2 File Offset: 0x0005D6D2
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		public Menu DropDownMenu
		{
			get
			{
				return this.menu;
			}
			set
			{
				if (value is ContextMenu)
				{
					this.menu = (ContextMenu)value;
					this.OnUIADropDownMenuChanged(EventArgs.Empty);
					return;
				}
				throw new ArgumentException("DropDownMenu must be of type ContextMenu.");
			}
		}

		/// <summary>Gets or sets a value indicating whether the button is enabled.</summary>
		/// <returns>true if the button is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0005F500 File Offset: 0x0005D700
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x0005F508 File Offset: 0x0005D708
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (value == this.enabled)
				{
					return;
				}
				this.enabled = value;
				this.Invalidate();
				this.OnUIAEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the index value of the image assigned to the button.</summary>
		/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0005F52C File Offset: 0x0005D72C
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0005F534 File Offset: 0x0005D734
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(-1)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[TypeConverter(typeof(ImageIndexConverter))]
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
					throw new ArgumentException("ImageIndex value must be above or equal to -1.");
				}
				if (value == this.image_index)
				{
					return;
				}
				bool flag = this.Parent != null && (value == -1 || this.image_index == -1);
				this.image_index = value;
				this.image_key = string.Empty;
				if (flag)
				{
					this.Parent.Redraw(true);
					return;
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets the toolbar control that the toolbar button is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBar" /> control that the <see cref="T:System.Windows.Forms.ToolBarButton" /> is assigned to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0005F59C File Offset: 0x0005D79C
		[Browsable(false)]
		public ToolBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is partially pushed.</summary>
		/// <returns>true if a toggle-style toolbar button is partially pushed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
		[DefaultValue(false)]
		public bool PartialPush
		{
			get
			{
				return this.partial_push;
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is currently in the pushed state.</summary>
		/// <returns>true if a toggle-style toolbar button is currently in the pushed state; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0005F5AC File Offset: 0x0005D7AC
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		[DefaultValue(false)]
		public bool Pushed
		{
			get
			{
				return this.pushed;
			}
			set
			{
				if (value == this.pushed)
				{
					return;
				}
				this.pushed = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets the bounding rectangle for a toolbar button.</summary>
		/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> for a toolbar button.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
		public Rectangle Rectangle
		{
			get
			{
				if (this.Visible && this.Parent != null && this.Parent.items != null)
				{
					foreach (ToolBarItem toolBarItem in this.Parent.items)
					{
						if (toolBarItem.Button == this)
						{
							return toolBarItem.Rectangle;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		/// <summary>Gets or sets the style of the toolbar button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. The default is ToolBarButtonStyle.PushButton.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0005F62D File Offset: 0x0005D82D
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0005F635 File Offset: 0x0005D835
		[DefaultValue(ToolBarButtonStyle.PushButton)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ToolBarButtonStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (value == this.style)
				{
					return;
				}
				this.style = value;
				if (this.parent != null)
				{
					this.parent.Redraw(true);
				}
				this.OnUIAStyleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the text displayed on the toolbar button.</summary>
		/// <returns>The text displayed on the toolbar button. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0005F667 File Offset: 0x0005D867
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets or sets the text that appears as a ToolTip for the button.</summary>
		/// <returns>The text that is displayed when the mouse pointer moves over the toolbar button. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0005F66F File Offset: 0x0005D86F
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTipText
		{
			get
			{
				return this.tooltip;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar button is visible.</summary>
		/// <returns>true if the toolbar button is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0005F677 File Offset: 0x0005D877
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0005F67F File Offset: 0x0005D87F
		internal void SetParent(ToolBar parent)
		{
			if (this.Parent == parent)
			{
				return;
			}
			if (this.Parent != null)
			{
				this.Parent.Buttons.Remove(this);
			}
			this.parent = parent;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0005F6AB File Offset: 0x0005D8AB
		internal void Invalidate()
		{
			if (this.Parent != null)
			{
				this.Parent.Invalidate(this.Rectangle);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x0005F6C8 File Offset: 0x0005D8C8
		internal bool UIAHasFocus
		{
			set
			{
				this.uiaHasFocus = value;
				EventHandler eventHandler = (EventHandler)(value ? base.Events[ToolBarButton.UIAGotFocusEvent] : base.Events[ToolBarButton.UIALostFocusEvent]);
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0005F718 File Offset: 0x0005D918
		private void OnUIAEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIAEnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0005F748 File Offset: 0x0005D948
		private void OnUIADropDownMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIADropDownMenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0005F778 File Offset: 0x0005D978
		private void OnUIAStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolBarButton.UIAStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBarButton" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001296 RID: 4758 RVA: 0x000057B1 File Offset: 0x000039B1
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBarButton" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ToolBarButton" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001297 RID: 4759 RVA: 0x0005F7A6 File Offset: 0x0005D9A6
		public override string ToString()
		{
			return string.Format("ToolBarButton: {0}, Style: {1}", this.text, this.style);
		}

		// Token: 0x04000B71 RID: 2929
		private bool enabled = true;

		// Token: 0x04000B72 RID: 2930
		private int image_index = -1;

		// Token: 0x04000B73 RID: 2931
		private ContextMenu menu;

		// Token: 0x04000B74 RID: 2932
		private ToolBar parent;

		// Token: 0x04000B75 RID: 2933
		private bool partial_push;

		// Token: 0x04000B76 RID: 2934
		private bool pushed;

		// Token: 0x04000B77 RID: 2935
		private ToolBarButtonStyle style = ToolBarButtonStyle.PushButton;

		// Token: 0x04000B78 RID: 2936
		private string text = "";

		// Token: 0x04000B79 RID: 2937
		private string tooltip = "";

		// Token: 0x04000B7A RID: 2938
		private bool visible = true;

		// Token: 0x04000B7B RID: 2939
		private string image_key = string.Empty;

		// Token: 0x04000B7C RID: 2940
		private bool uiaHasFocus;

		// Token: 0x04000B7D RID: 2941
		private static object UIAGotFocusEvent = new object();

		// Token: 0x04000B7E RID: 2942
		private static object UIALostFocusEvent = new object();

		// Token: 0x04000B7F RID: 2943
		private static object UIATextChangedEvent = new object();

		// Token: 0x04000B80 RID: 2944
		private static object UIAEnabledChangedEvent = new object();

		// Token: 0x04000B81 RID: 2945
		private static object UIADropDownMenuChangedEvent = new object();

		// Token: 0x04000B82 RID: 2946
		private static object UIAStyleChangedEvent = new object();
	}
}
