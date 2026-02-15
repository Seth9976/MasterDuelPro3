using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000090 RID: 144
	public struct StringOptions : IPlugOptions
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public void Reset()
		{
			this.richTextEnabled = false;
			this.scrambleMode = ScrambleMode.None;
			this.scrambledChars = null;
			this.startValueStrippedLength = (this.changeValueStrippedLength = 0);
		}

		// Token: 0x0400018B RID: 395
		public bool richTextEnabled;

		// Token: 0x0400018C RID: 396
		public ScrambleMode scrambleMode;

		// Token: 0x0400018D RID: 397
		public char[] scrambledChars;

		// Token: 0x0400018E RID: 398
		internal int startValueStrippedLength;

		// Token: 0x0400018F RID: 399
		internal int changeValueStrippedLength;
	}
}
