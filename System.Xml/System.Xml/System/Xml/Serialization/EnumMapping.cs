using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000168 RID: 360
	internal class EnumMapping : PrimitiveMapping
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000541B3 File Offset: 0x000523B3
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000541BB File Offset: 0x000523BB
		internal bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
			set
			{
				this.isFlags = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000541C4 File Offset: 0x000523C4
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000541CC File Offset: 0x000523CC
		internal ConstantMapping[] Constants
		{
			get
			{
				return this.constants;
			}
			set
			{
				this.constants = value;
			}
		}

		// Token: 0x0400084D RID: 2125
		private ConstantMapping[] constants;

		// Token: 0x0400084E RID: 2126
		private bool isFlags;
	}
}
