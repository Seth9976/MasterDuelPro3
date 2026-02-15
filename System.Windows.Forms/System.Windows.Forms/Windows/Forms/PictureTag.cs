using System;
using System.Drawing;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	// Token: 0x0200019D RID: 413
	internal class PictureTag : LineTag
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x0004F31D File Offset: 0x0004D51D
		internal PictureTag(Line line, int start, Picture picture)
			: base(line, start)
		{
			this.picture = picture;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool IsTextTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004F32E File Offset: 0x0004D52E
		public override SizeF SizeOfPosition(Graphics dc, int pos)
		{
			return this.picture.Size;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004F33B File Offset: 0x0004D53B
		internal override int MaxHeight()
		{
			return (int)(this.picture.Height + 0.5f);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004F34F File Offset: 0x0004D54F
		public override string Text()
		{
			return "I";
		}

		// Token: 0x04000ADD RID: 2781
		internal Picture picture;
	}
}
