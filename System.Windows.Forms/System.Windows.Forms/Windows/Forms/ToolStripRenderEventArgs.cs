using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs)" />, <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs)" />, and <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs)" /> methods. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E8 RID: 488
	public class ToolStripRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		// Token: 0x060014C7 RID: 5319 RVA: 0x00068B32 File Offset: 0x00066D32
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip)
			: this(g, toolStrip, new Rectangle(0, 0, 100, 25), SystemColors.Control)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" />, using the specified <see cref="T:System.Drawing.Graphics" /> to paint the specified bounds with the specified <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
		/// <param name="affectedBounds">The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</param>
		// Token: 0x060014C8 RID: 5320 RVA: 0x00068B4C File Offset: 0x00066D4C
		public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle affectedBounds, Color backColor)
		{
			this.graphics = g;
			this.tool_strip = toolStrip;
			this.affected_bounds = affectedBounds;
			this.back_color = backColor;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted. </summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00068B71 File Offset: 0x00066D71
		public Rectangle AffectedBounds
		{
			get
			{
				return this.affected_bounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00068B79 File Offset: 0x00066D79
		public Rectangle ConnectedArea
		{
			get
			{
				return this.connected_area;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00068B81 File Offset: 0x00066D81
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00068B89 File Offset: 0x00066D89
		public ToolStrip ToolStrip
		{
			get
			{
				return this.tool_strip;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00068B91 File Offset: 0x00066D91
		internal Rectangle InternalConnectedArea
		{
			set
			{
				this.connected_area = value;
			}
		}

		// Token: 0x04000C8A RID: 3210
		private Rectangle affected_bounds;

		// Token: 0x04000C8B RID: 3211
		private Color back_color;

		// Token: 0x04000C8C RID: 3212
		private Rectangle connected_area;

		// Token: 0x04000C8D RID: 3213
		private Graphics graphics;

		// Token: 0x04000C8E RID: 3214
		private ToolStrip tool_strip;
	}
}
