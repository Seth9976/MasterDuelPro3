using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000145 RID: 325
	internal class Match
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00039AFA File Offset: 0x00037CFA
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00039AF1 File Offset: 0x00037CF1
		public string MimeType
		{
			get
			{
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00039B02 File Offset: 0x00037D02
		public int Priority
		{
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00039B0B File Offset: 0x00037D0B
		public ArrayList Matchlets
		{
			get
			{
				return this.matchlets;
			}
		}

		// Token: 0x0400083D RID: 2109
		private string mimeType;

		// Token: 0x0400083E RID: 2110
		private int priority;

		// Token: 0x0400083F RID: 2111
		private ArrayList matchlets = new ArrayList();
	}
}
