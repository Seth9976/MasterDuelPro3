using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Hosts custom controls or Windows Forms controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BD RID: 445
	public class ToolStripControlHost : ToolStripItem
	{
		/// <summary>Gets the <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00061FCA File Offset: 0x000601CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gives the focus to a control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600132F RID: 4911 RVA: 0x00061FD2 File Offset: 0x000601D2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			this.control.Focus();
		}

		// Token: 0x04000BC0 RID: 3008
		private Control control;

		// Token: 0x04000BC1 RID: 3009
		private static object EnterEvent = new object();

		// Token: 0x04000BC2 RID: 3010
		private static object GotFocusEvent = new object();

		// Token: 0x04000BC3 RID: 3011
		private static object KeyDownEvent = new object();

		// Token: 0x04000BC4 RID: 3012
		private static object KeyPressEvent = new object();

		// Token: 0x04000BC5 RID: 3013
		private static object KeyUpEvent = new object();

		// Token: 0x04000BC6 RID: 3014
		private static object LeaveEvent = new object();

		// Token: 0x04000BC7 RID: 3015
		private static object LostFocusEvent = new object();

		// Token: 0x04000BC8 RID: 3016
		private static object ValidatedEvent = new object();

		// Token: 0x04000BC9 RID: 3017
		private static object ValidatingEvent = new object();
	}
}
