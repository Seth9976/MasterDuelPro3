using System;

namespace System.Resources
{
	// Token: 0x020005DB RID: 1499
	internal class NameOrId
	{
		// Token: 0x06002C5D RID: 11357 RVA: 0x000B0BA7 File Offset: 0x000AEDA7
		public NameOrId(string name)
		{
			this.name = name;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000B0BB6 File Offset: 0x000AEDB6
		public NameOrId(int id)
		{
			this.id = id;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000B0BC5 File Offset: 0x000AEDC5
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000B0BD0 File Offset: 0x000AEDD0
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000B0BD8 File Offset: 0x000AEDD8
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id.ToString() + ")";
		}

		// Token: 0x0400169E RID: 5790
		private string name;

		// Token: 0x0400169F RID: 5791
		private int id;
	}
}
