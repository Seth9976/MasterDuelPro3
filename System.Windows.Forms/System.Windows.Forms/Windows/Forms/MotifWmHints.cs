using System;

namespace System.Windows.Forms
{
	// Token: 0x0200026F RID: 623
	internal struct MotifWmHints
	{
		// Token: 0x06001740 RID: 5952 RVA: 0x00074990 File Offset: 0x00072B90
		public override string ToString()
		{
			return string.Format("MotifWmHints <flags={0}, functions={1}, decorations={2}, input_mode={3}, status={4}", new object[]
			{
				(MotifFlags)this.flags.ToInt32(),
				(MotifFunctions)this.functions.ToInt32(),
				(MotifDecorations)this.decorations.ToInt32(),
				(MotifInputMode)this.input_mode.ToInt32(),
				this.status.ToInt32()
			});
		}

		// Token: 0x0400108B RID: 4235
		internal IntPtr flags;

		// Token: 0x0400108C RID: 4236
		internal IntPtr functions;

		// Token: 0x0400108D RID: 4237
		internal IntPtr decorations;

		// Token: 0x0400108E RID: 4238
		internal IntPtr input_mode;

		// Token: 0x0400108F RID: 4239
		internal IntPtr status;
	}
}
