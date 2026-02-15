using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000166 RID: 358
	internal class NullableMapping : TypeMapping
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x000540F4 File Offset: 0x000522F4
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x000540FC File Offset: 0x000522FC
		internal TypeMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00054105 File Offset: 0x00052305
		internal override string DefaultElementName
		{
			get
			{
				return this.BaseMapping.DefaultElementName;
			}
		}

		// Token: 0x04000848 RID: 2120
		private TypeMapping baseMapping;
	}
}
