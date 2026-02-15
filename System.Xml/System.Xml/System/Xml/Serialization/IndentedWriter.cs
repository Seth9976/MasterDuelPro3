using System;
using System.IO;

namespace System.Xml.Serialization
{
	// Token: 0x020001F1 RID: 497
	internal class IndentedWriter
	{
		// Token: 0x06001979 RID: 6521 RVA: 0x00096E26 File Offset: 0x00095026
		internal IndentedWriter(TextWriter writer, bool compact)
		{
			this.writer = writer;
			this.compact = compact;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00096E3C File Offset: 0x0009503C
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x00096E44 File Offset: 0x00095044
		internal int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00096E4D File Offset: 0x0009504D
		internal void Write(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(s);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00096E69 File Offset: 0x00095069
		internal void Write(char c)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(c);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00096E85 File Offset: 0x00095085
		internal void WriteLine(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.WriteLine(s);
			this.needIndent = true;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00096EA8 File Offset: 0x000950A8
		internal void WriteLine()
		{
			this.writer.WriteLine();
			this.needIndent = true;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00096EBC File Offset: 0x000950BC
		internal void WriteIndent()
		{
			this.needIndent = false;
			if (!this.compact)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write("    ");
				}
			}
		}

		// Token: 0x04000ABD RID: 2749
		private TextWriter writer;

		// Token: 0x04000ABE RID: 2750
		private bool needIndent;

		// Token: 0x04000ABF RID: 2751
		private int indentLevel;

		// Token: 0x04000AC0 RID: 2752
		private bool compact;
	}
}
