using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000020 RID: 32
	internal class CharEntityEncoderFallback : EncoderFallback
	{
		// Token: 0x06000121 RID: 289 RVA: 0x0000A2D8 File Offset: 0x000084D8
		internal CharEntityEncoderFallback()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			if (this.fallbackBuffer == null)
			{
				this.fallbackBuffer = new CharEntityEncoderFallbackBuffer(this);
			}
			return this.fallbackBuffer;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000A2FC File Offset: 0x000084FC
		public override int MaxCharCount
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000A300 File Offset: 0x00008500
		internal int StartOffset
		{
			set
			{
				this.startOffset = value;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A309 File Offset: 0x00008509
		internal void Reset(int[] textContentMarks, int endMarkPos)
		{
			this.textContentMarks = textContentMarks;
			this.endMarkPos = endMarkPos;
			this.curMarkPos = 0;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A320 File Offset: 0x00008520
		internal bool CanReplaceAt(int index)
		{
			int num = this.curMarkPos;
			int num2 = this.startOffset + index;
			while (num < this.endMarkPos && num2 >= this.textContentMarks[num + 1])
			{
				num++;
			}
			this.curMarkPos = num;
			return (num & 1) != 0;
		}

		// Token: 0x040000F5 RID: 245
		private CharEntityEncoderFallbackBuffer fallbackBuffer;

		// Token: 0x040000F6 RID: 246
		private int[] textContentMarks;

		// Token: 0x040000F7 RID: 247
		private int endMarkPos;

		// Token: 0x040000F8 RID: 248
		private int curMarkPos;

		// Token: 0x040000F9 RID: 249
		private int startOffset;
	}
}
