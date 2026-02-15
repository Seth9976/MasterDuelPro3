using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a graphic metafile. A metafile contains records that describe a sequence of graphics operations that can be recorded (constructed) and played back (displayed). This class is not inheritable.</summary>
	// Token: 0x02000090 RID: 144
	[Editor("System.Drawing.Design.MetafileEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[MonoTODO("Metafiles, both WMF and EMF formats, are only partially supported.")]
	[Serializable]
	public sealed class Metafile : Image
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000E654 File Offset: 0x0000C854
		internal Metafile.MetafileHolder AddMetafileHolder()
		{
			if (this._metafileHolder != null && !this._metafileHolder.Disposed)
			{
				return null;
			}
			this._metafileHolder = new Metafile.MetafileHolder();
			return this._metafileHolder;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00005FF4 File Offset: 0x000041F4
		internal Metafile(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00006003 File Offset: 0x00004203
		internal Metafile(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E680 File Offset: 0x0000C880
		protected override void Dispose(bool disposing)
		{
			if (this._metafileHolder != null && !this._metafileHolder.Disposed)
			{
				this._metafileHolder.MetafileDisposed(this.nativeObject);
				this._metafileHolder = null;
				this.nativeObject = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040002C9 RID: 713
		private Metafile.MetafileHolder _metafileHolder;

		// Token: 0x02000091 RID: 145
		internal sealed class MetafileHolder : IDisposable
		{
			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E6CC File Offset: 0x0000C8CC
			internal bool Disposed
			{
				get
				{
					return this._disposed;
				}
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
			internal MetafileHolder()
			{
				this._disposed = false;
				this._nativeImage = IntPtr.Zero;
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
			~MetafileHolder()
			{
				this.Dispose(false);
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x0000E720 File Offset: 0x0000C920
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060004A2 RID: 1186 RVA: 0x0000E730 File Offset: 0x0000C930
			internal void Dispose(bool disposing)
			{
				if (!this._disposed)
				{
					IntPtr nativeImage = this._nativeImage;
					this._nativeImage = IntPtr.Zero;
					this._disposed = true;
					if (nativeImage != IntPtr.Zero)
					{
						GDIPlus.CheckStatus(GDIPlus.GdipDisposeImage(nativeImage));
					}
				}
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x0000E776 File Offset: 0x0000C976
			internal void MetafileDisposed(IntPtr nativeImage)
			{
				this._nativeImage = nativeImage;
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x0000E77F File Offset: 0x0000C97F
			internal void GraphicsDisposed()
			{
				this.Dispose();
			}

			// Token: 0x040002CA RID: 714
			private bool _disposed;

			// Token: 0x040002CB RID: 715
			private IntPtr _nativeImage;
		}
	}
}
