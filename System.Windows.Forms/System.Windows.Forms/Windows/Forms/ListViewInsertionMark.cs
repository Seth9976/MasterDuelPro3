using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Used to indicate the expected drop location when an item is dragged to a new position in a <see cref="T:System.Windows.Forms.ListView" /> control. This functionality is available only on Windows XP and later.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000119 RID: 281
	public sealed class ListViewInsertionMark
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002EE5C File Offset: 0x0002D05C
		internal ListViewInsertionMark(ListView listview)
		{
			this.listview_owner = listview;
		}

		/// <summary>Gets the bounding rectangle of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the position and size of the insertion mark.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002EE6B File Offset: 0x0002D06B
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets or sets the color of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the color of the insertion mark. The default value is the value of the <see cref="P:System.Windows.Forms.ListView.ForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002EE73 File Offset: 0x0002D073
		public Color Color
		{
			get
			{
				if (this.color != null)
				{
					return this.color.Value;
				}
				return this.listview_owner.ForeColor;
			}
		}

		/// <summary>Gets or sets the index of the item next to which the insertion mark appears.</summary>
		/// <returns>The index of the item next to which the insertion mark appears or -1 when the insertion mark is hidden.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002EE99 File Offset: 0x0002D099
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
		internal PointF[] TopTriangle
		{
			get
			{
				PointF pointF = new PointF((float)this.bounds.X, (float)this.bounds.Y);
				PointF pointF2 = new PointF((float)this.bounds.Right, (float)this.bounds.Y);
				PointF pointF3 = new PointF((float)(this.bounds.X + (this.bounds.Right - this.bounds.X) / 2), (float)(this.bounds.Y + 5));
				return new PointF[] { pointF, pointF2, pointF3 };
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002EF48 File Offset: 0x0002D148
		internal PointF[] BottomTriangle
		{
			get
			{
				PointF pointF = new PointF((float)this.bounds.X, (float)this.bounds.Bottom);
				PointF pointF2 = new PointF((float)this.bounds.Right, (float)this.bounds.Bottom);
				PointF pointF3 = new PointF((float)(this.bounds.X + (this.bounds.Right - this.bounds.X) / 2), (float)(this.bounds.Bottom - 5));
				return new PointF[] { pointF, pointF2, pointF3 };
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002EFEC File Offset: 0x0002D1EC
		internal Rectangle Line
		{
			get
			{
				return new Rectangle(this.bounds.X + 2, this.bounds.Y + 2, 2, this.bounds.Height - 5);
			}
		}

		// Token: 0x0400073F RID: 1855
		private ListView listview_owner;

		// Token: 0x04000740 RID: 1856
		private Rectangle bounds;

		// Token: 0x04000741 RID: 1857
		private Color? color;

		// Token: 0x04000742 RID: 1858
		private int index;
	}
}
