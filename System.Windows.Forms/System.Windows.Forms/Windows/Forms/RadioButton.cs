using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Enables the user to select a single option from a group of choices when paired with other <see cref="T:System.Windows.Forms.RadioButton" /> controls.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200016D RID: 365
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[DefaultBindingProperty("Checked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Windows.Forms.Design.RadioButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class RadioButton : ButtonBase
	{
		/// <summary>Gets or sets the location of the check box portion of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
		/// <returns>One of the valid <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003F1EF File Offset: 0x0003D3EF
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.radiobutton_alignment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is checked.</summary>
		/// <returns>true if the check box is checked; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0003F1F7 File Offset: 0x0003D3F7
		[DefaultValue(false)]
		[SettingsBindable(true)]
		[Bindable(true, BindingDirection.OneWay)]
		public bool Checked
		{
			get
			{
				return this.check_state != CheckState.Unchecked;
			}
		}

		// Token: 0x040008E5 RID: 2277
		internal ContentAlignment radiobutton_alignment;

		// Token: 0x040008E6 RID: 2278
		internal CheckState check_state;

		// Token: 0x040008E7 RID: 2279
		private static object AppearanceChangedEvent = new object();

		// Token: 0x040008E8 RID: 2280
		private static object CheckedChangedEvent = new object();
	}
}
