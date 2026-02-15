using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015E RID: 350
	public class PaintEventArgs : EventArgs, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaintEventArgs" /> class with the specified graphics and clipping rectangle.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item. </param>
		/// <param name="clipRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint. </param>
		// Token: 0x06000D8D RID: 3469 RVA: 0x0003B5A8 File Offset: 0x000397A8
		public PaintEventArgs(Graphics graphics, Rectangle clipRect)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.graphics = graphics;
			this.clip_rectangle = clipRect;
		}

		/// <summary>Gets the rectangle in which to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> in which to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003B5CC File Offset: 0x000397CC
		public Rectangle ClipRectangle
		{
			get
			{
				return this.clip_rectangle;
			}
		}

		/// <summary>Gets the graphics used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> object used to paint. The <see cref="T:System.Drawing.Graphics" /> object provides methods for drawing objects on the display device.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003B5D4 File Offset: 0x000397D4
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D90 RID: 3472 RVA: 0x0003B5DC File Offset: 0x000397DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003B5EB File Offset: 0x000397EB
		internal Graphics SetGraphics(Graphics g)
		{
			Graphics graphics = this.graphics;
			this.graphics = g;
			return graphics;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003B5FA File Offset: 0x000397FA
		internal void SetClip(Rectangle clip)
		{
			this.clip_rectangle = clip;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003B604 File Offset: 0x00039804
		~PaintEventArgs()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x0003B634 File Offset: 0x00039834
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x0400087F RID: 2175
		private Graphics graphics;

		// Token: 0x04000880 RID: 2176
		private Rectangle clip_rectangle;

		// Token: 0x04000881 RID: 2177
		internal bool Handled;

		// Token: 0x04000882 RID: 2178
		private bool disposed;
	}
}
