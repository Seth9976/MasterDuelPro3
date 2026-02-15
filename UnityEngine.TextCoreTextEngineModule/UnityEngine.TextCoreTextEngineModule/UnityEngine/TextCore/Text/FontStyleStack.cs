using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000063 RID: 99
	internal struct FontStyleStack
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0002EE40 File Offset: 0x0002D040
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

		// Token: 0x060002C1 RID: 705 RVA: 0x0002EE94 File Offset: 0x0002D094
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

		// Token: 0x060002C2 RID: 706 RVA: 0x0002F000 File Offset: 0x0002D200
		public byte Remove(FontStyles style)
		{
			if (style <= FontStyles.UpperCase)
			{
				switch (style)
				{
				case FontStyles.Bold:
				{
					bool flag = this.bold > 1;
					if (flag)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					return this.bold;
				}
				case FontStyles.Italic:
				{
					bool flag2 = this.italic > 1;
					if (flag2)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					return this.italic;
				}
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
				{
					bool flag3 = this.underline > 1;
					if (flag3)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					return this.underline;
				}
				default:
					if (style == FontStyles.LowerCase)
					{
						bool flag4 = this.lowercase > 1;
						if (flag4)
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
						bool flag5 = this.uppercase > 1;
						if (flag5)
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
					bool flag6 = this.strikethrough > 1;
					if (flag6)
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
					bool flag7 = this.superscript > 1;
					if (flag7)
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
					bool flag8 = this.subscript > 1;
					if (flag8)
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
					bool flag9 = this.highlight > 1;
					if (flag9)
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

		// Token: 0x04000439 RID: 1081
		public byte bold;

		// Token: 0x0400043A RID: 1082
		public byte italic;

		// Token: 0x0400043B RID: 1083
		public byte underline;

		// Token: 0x0400043C RID: 1084
		public byte strikethrough;

		// Token: 0x0400043D RID: 1085
		public byte highlight;

		// Token: 0x0400043E RID: 1086
		public byte superscript;

		// Token: 0x0400043F RID: 1087
		public byte subscript;

		// Token: 0x04000440 RID: 1088
		public byte uppercase;

		// Token: 0x04000441 RID: 1089
		public byte lowercase;

		// Token: 0x04000442 RID: 1090
		public byte smallcaps;
	}
}
