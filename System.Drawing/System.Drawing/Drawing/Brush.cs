using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines objects used to fill the interiors of graphical shapes such as rectangles, ellipses, pies, polygons, and paths.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Brushes" />
	// Token: 0x0200000D RID: 13
	public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
	{
		/// <summary>When overridden in a derived class, creates an exact copy of this <see cref="T:System.Drawing.Brush" />.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Brush" /> that this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600002A RID: 42
		public abstract object Clone();

		/// <summary>In a derived class, sets a reference to a GDI+ brush object. </summary>
		/// <param name="brush">A pointer to the GDI+ brush object.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x00003511 File Offset: 0x00001711
		protected internal void SetNativeBrush(IntPtr brush)
		{
			this.SetNativeBrushInternal(brush);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000351A File Offset: 0x0000171A
		internal void SetNativeBrushInternal(IntPtr brush)
		{
			this._nativeBrush = brush;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003523 File Offset: 0x00001723
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal IntPtr NativeBrush
		{
			get
			{
				return this._nativeBrush;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600002E RID: 46 RVA: 0x0000352B File Offset: 0x0000172B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Brush" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600002F RID: 47 RVA: 0x0000353C File Offset: 0x0000173C
		protected virtual void Dispose(bool disposing)
		{
			if (this._nativeBrush != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDeleteBrush(new HandleRef(this, this._nativeBrush));
				}
				catch (Exception ex) when (!ClientUtils.IsSecurityOrCriticalException(ex))
				{
				}
				finally
				{
					this._nativeBrush = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000035B4 File Offset: 0x000017B4
		~Brush()
		{
			this.Dispose(false);
		}

		// Token: 0x040000B7 RID: 183
		private IntPtr _nativeBrush;
	}
}
