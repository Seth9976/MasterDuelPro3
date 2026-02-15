using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000205 RID: 517
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct StringConcat
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x00097CF8 File Offset: 0x00095EF8
		public void Clear()
		{
			this.idxStr = 0;
			this.delimiter = null;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00097D08 File Offset: 0x00095F08
		internal int Count
		{
			get
			{
				return this.idxStr;
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00097D10 File Offset: 0x00095F10
		public string GetResult()
		{
			switch (this.idxStr)
			{
			case 0:
				return string.Empty;
			case 1:
				return this.s1;
			case 2:
				return this.s1 + this.s2;
			case 3:
				return this.s1 + this.s2 + this.s3;
			case 4:
				return this.s1 + this.s2 + this.s3 + this.s4;
			default:
				return string.Concat(this.strList.ToArray());
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00097DA8 File Offset: 0x00095FA8
		internal void ConcatNoDelimiter(string s)
		{
			switch (this.idxStr)
			{
			case 0:
				this.s1 = s;
				goto IL_00A8;
			case 1:
				this.s2 = s;
				goto IL_00A8;
			case 2:
				this.s3 = s;
				goto IL_00A8;
			case 3:
				this.s4 = s;
				goto IL_00A8;
			case 4:
			{
				int num = ((this.strList == null) ? 8 : this.strList.Count);
				List<string> list = (this.strList = new List<string>(num));
				list.Add(this.s1);
				list.Add(this.s2);
				list.Add(this.s3);
				list.Add(this.s4);
				break;
			}
			}
			this.strList.Add(s);
			IL_00A8:
			this.idxStr++;
		}

		// Token: 0x04000AFF RID: 2815
		private string s1;

		// Token: 0x04000B00 RID: 2816
		private string s2;

		// Token: 0x04000B01 RID: 2817
		private string s3;

		// Token: 0x04000B02 RID: 2818
		private string s4;

		// Token: 0x04000B03 RID: 2819
		private string delimiter;

		// Token: 0x04000B04 RID: 2820
		private List<string> strList;

		// Token: 0x04000B05 RID: 2821
		private int idxStr;
	}
}
