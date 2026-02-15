using System;
using System.Drawing.Drawing2D;

namespace System.Drawing
{
	/// <summary>Describes the interior of a graphics shape composed of rectangles and paths. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000059 RID: 89
	public sealed class Region : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" />.</summary>
		// Token: 0x0600032F RID: 815 RVA: 0x0000C119 File Offset: 0x0000A319
		public Region()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegion(out this.nativeRegion));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C13C File Offset: 0x0000A33C
		internal Region(IntPtr native)
		{
			this.nativeRegion = native;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />. </param>
		// Token: 0x06000331 RID: 817 RVA: 0x0000C156 File Offset: 0x0000A356
		public Region(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRectI(ref rect, out this.nativeRegion));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to unite with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000332 RID: 818 RVA: 0x0000C17B File Offset: 0x0000A37B
		public void Union(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Union));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to intersect with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000333 RID: 819 RVA: 0x0000C190 File Offset: 0x0000A390
		public void Intersect(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Intersect));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to exclude from this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000334 RID: 820 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		public void Exclude(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Exclude));
		}

		/// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that represents a rectangle that bounds this <see cref="T:System.Drawing.Region" /> on the drawing surface of a <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000335 RID: 821 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public RectangleF GetBounds(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			RectangleF rectangleF = default(Rectangle);
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionBounds(this.nativeRegion, g.NativeObject, ref rectangleF));
			return rectangleF;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an infinite interior on the specified drawing surface.</summary>
		/// <returns>true if the interior of this <see cref="T:System.Drawing.Region" /> is infinite when the transformation associated with <paramref name="g" /> is applied; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000336 RID: 822 RVA: 0x0000C210 File Offset: 0x0000A410
		public bool IsInfinite(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsInfiniteRegion(this.nativeRegion, g.NativeObject, out flag));
			return flag;
		}

		/// <summary>Initializes this <see cref="T:System.Drawing.Region" /> to an empty interior.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000337 RID: 823 RVA: 0x0000C244 File Offset: 0x0000A444
		public void MakeEmpty()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetEmpty(this.nativeRegion));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from a handle to the specified existing GDI region.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Region" />.</returns>
		/// <param name="hrgn">A handle to an existing <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000338 RID: 824 RVA: 0x0000C258 File Offset: 0x0000A458
		public static Region FromHrgn(IntPtr hrgn)
		{
			if (hrgn == IntPtr.Zero)
			{
				throw new ArgumentException("hrgn");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionHrgn(hrgn, out intPtr));
			return new Region(intPtr);
		}

		/// <summary>Returns a Windows handle to this <see cref="T:System.Drawing.Region" /> in the specified graphics context.</summary>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Region" />.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000339 RID: 825 RVA: 0x0000C290 File Offset: 0x0000A490
		public IntPtr GetHrgn(Graphics g)
		{
			if (g == null)
			{
				return this.nativeRegion;
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionHRgn(this.nativeRegion, g.NativeObject, ref zero));
			return zero;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Region" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600033A RID: 826 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
		public void Dispose()
		{
			this.DisposeHandle();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		private void DisposeHandle()
		{
			if (this.nativeRegion != IntPtr.Zero)
			{
				GDIPlus.GdipDeleteRegion(this.nativeRegion);
				this.nativeRegion = IntPtr.Zero;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C300 File Offset: 0x0000A500
		~Region()
		{
			this.DisposeHandle();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C32C File Offset: 0x0000A52C
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeRegion;
			}
		}

		// Token: 0x0400018F RID: 399
		private IntPtr nativeRegion = IntPtr.Zero;
	}
}
