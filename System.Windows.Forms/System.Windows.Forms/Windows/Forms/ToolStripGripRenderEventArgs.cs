using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C9 RID: 457
	public class ToolStripGripRenderEventArgs : ToolStripRenderEventArgs
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x00063730 File Offset: 0x00061930
		internal ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle gripBounds, ToolStripGripDisplayStyle displayStyle, ToolStripGripStyle gripStyle)
			: base(g, toolStrip)
		{
			this.grip_bounds = gripBounds;
			this.grip_display_style = displayStyle;
			this.grip_style = gripStyle;
		}

		/// <summary>Gets the rectangle representing the area in which to paint the move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area in which to paint the move handle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00063751 File Offset: 0x00061951
		public Rectangle GripBounds
		{
			get
			{
				return this.grip_bounds;
			}
		}

		/// <summary>Gets the style that indicates whether the move handle is displayed vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00063759 File Offset: 0x00061959
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return this.grip_display_style;
			}
		}

		/// <summary>Gets the style that indicates whether or not the move handle is visible.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00063761 File Offset: 0x00061961
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return this.grip_style;
			}
		}

		// Token: 0x04000BF0 RID: 3056
		private Rectangle grip_bounds;

		// Token: 0x04000BF1 RID: 3057
		private ToolStripGripDisplayStyle grip_display_style;

		// Token: 0x04000BF2 RID: 3058
		private ToolStripGripStyle grip_style;
	}
}
