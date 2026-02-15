using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000146 RID: 326
	internal class Matchlet
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00039B2F File Offset: 0x00037D2F
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00039B26 File Offset: 0x00037D26
		public byte[] ByteValue
		{
			get
			{
				return this.byteValue;
			}
			set
			{
				this.byteValue = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00039B40 File Offset: 0x00037D40
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00039B37 File Offset: 0x00037D37
		public byte[] Mask
		{
			get
			{
				return this.mask;
			}
			set
			{
				this.mask = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00039B51 File Offset: 0x00037D51
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00039B48 File Offset: 0x00037D48
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00039B62 File Offset: 0x00037D62
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00039B59 File Offset: 0x00037D59
		public int OffsetLength
		{
			get
			{
				return this.offsetLength;
			}
			set
			{
				this.offsetLength = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00039B6A File Offset: 0x00037D6A
		public ArrayList Matchlets
		{
			get
			{
				return this.matchlets;
			}
		}

		// Token: 0x04000840 RID: 2112
		private byte[] byteValue;

		// Token: 0x04000841 RID: 2113
		private byte[] mask;

		// Token: 0x04000842 RID: 2114
		private int offset;

		// Token: 0x04000843 RID: 2115
		private int offsetLength;

		// Token: 0x04000844 RID: 2116
		private int wordSize = 1;

		// Token: 0x04000845 RID: 2117
		private ArrayList matchlets = new ArrayList();
	}
}
