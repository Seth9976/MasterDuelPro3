using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000163 RID: 355
	internal abstract class Mapping
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00002127 File Offset: 0x00000327
		internal Mapping()
		{
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00053FEB File Offset: 0x000521EB
		protected Mapping(Mapping mapping)
		{
			this.isSoap = mapping.isSoap;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00053FFF File Offset: 0x000521FF
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00054007 File Offset: 0x00052207
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x0400083F RID: 2111
		private bool isSoap;
	}
}
