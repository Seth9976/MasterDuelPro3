using System;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000050 RID: 80
	internal class AnimateEventArgs : EventArgs
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000B18B File Offset: 0x0000938B
		public AnimateEventArgs(Image image)
		{
			this.frameCount = image.GetFrameCount(FrameDimension.Time);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B1A4 File Offset: 0x000093A4
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000B1AC File Offset: 0x000093AC
		public Thread RunThread
		{
			get
			{
				return this.thread;
			}
			set
			{
				this.thread = value;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public int GetNextFrame()
		{
			if (this.activeFrame < this.frameCount - 1)
			{
				this.activeFrame++;
			}
			else
			{
				this.activeFrame = 0;
			}
			return this.activeFrame;
		}

		// Token: 0x0400017F RID: 383
		private int frameCount;

		// Token: 0x04000180 RID: 384
		private int activeFrame;

		// Token: 0x04000181 RID: 385
		private Thread thread;
	}
}
