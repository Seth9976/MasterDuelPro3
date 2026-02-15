using System;

namespace TMPro
{
	// Token: 0x0200009D RID: 157
	public struct TMP_FontStyleStack
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0002BD44 File Offset: 0x00029F44
		public void Clear()
		{
			this.bold = 0;
			this.italic = 0;
			this.underline = 0;
			this.strikethrough = 0;
			this.highlight = 0;
			this.superscript = 0;
			this.subscript = 0;
			this.uppercase = 0;
			this.lowercase = 0;
			this.smallcaps = 0;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002BD98 File Offset: 0x00029F98
		public byte Add(FontStyles style)
		{
			if (style <= FontStyles.UpperCase)
			{
				switch (style)
				{
				case FontStyles.Bold:
					this.bold += 1;
					return this.bold;
				case FontStyles.Italic:
					this.italic += 1;
					return this.italic;
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
					this.underline += 1;
					return this.underline;
				default:
					if (style == FontStyles.LowerCase)
					{
						this.lowercase += 1;
						return this.lowercase;
					}
					if (style == FontStyles.UpperCase)
					{
						this.uppercase += 1;
						return this.uppercase;
					}
					break;
				}
			}
			else if (style <= FontStyles.Superscript)
			{
				if (style == FontStyles.Strikethrough)
				{
					this.strikethrough += 1;
					return this.strikethrough;
				}
				if (style == FontStyles.Superscript)
				{
					this.superscript += 1;
					return this.superscript;
				}
			}
			else
			{
				if (style == FontStyles.Subscript)
				{
					this.subscript += 1;
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					this.highlight += 1;
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0002BED8 File Offset: 0x0002A0D8
		public byte Remove(FontStyles style)
		{
			if (style <= FontStyles.UpperCase)
			{
				switch (style)
				{
				case FontStyles.Bold:
					if (this.bold > 1)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					return this.bold;
				case FontStyles.Italic:
					if (this.italic > 1)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					return this.italic;
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
					if (this.underline > 1)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					return this.underline;
				default:
					if (style == FontStyles.LowerCase)
					{
						if (this.lowercase > 1)
						{
							this.lowercase -= 1;
						}
						else
						{
							this.lowercase = 0;
						}
						return this.lowercase;
					}
					if (style == FontStyles.UpperCase)
					{
						if (this.uppercase > 1)
						{
							this.uppercase -= 1;
						}
						else
						{
							this.uppercase = 0;
						}
						return this.uppercase;
					}
					break;
				}
			}
			else if (style <= FontStyles.Superscript)
			{
				if (style == FontStyles.Strikethrough)
				{
					if (this.strikethrough > 1)
					{
						this.strikethrough -= 1;
					}
					else
					{
						this.strikethrough = 0;
					}
					return this.strikethrough;
				}
				if (style == FontStyles.Superscript)
				{
					if (this.superscript > 1)
					{
						this.superscript -= 1;
					}
					else
					{
						this.superscript = 0;
					}
					return this.superscript;
				}
			}
			else
			{
				if (style == FontStyles.Subscript)
				{
					if (this.subscript > 1)
					{
						this.subscript -= 1;
					}
					else
					{
						this.subscript = 0;
					}
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					if (this.highlight > 1)
					{
						this.highlight -= 1;
					}
					else
					{
						this.highlight = 0;
					}
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x0400056D RID: 1389
		public byte bold;

		// Token: 0x0400056E RID: 1390
		public byte italic;

		// Token: 0x0400056F RID: 1391
		public byte underline;

		// Token: 0x04000570 RID: 1392
		public byte strikethrough;

		// Token: 0x04000571 RID: 1393
		public byte highlight;

		// Token: 0x04000572 RID: 1394
		public byte superscript;

		// Token: 0x04000573 RID: 1395
		public byte subscript;

		// Token: 0x04000574 RID: 1396
		public byte uppercase;

		// Token: 0x04000575 RID: 1397
		public byte lowercase;

		// Token: 0x04000576 RID: 1398
		public byte smallcaps;
	}
}
