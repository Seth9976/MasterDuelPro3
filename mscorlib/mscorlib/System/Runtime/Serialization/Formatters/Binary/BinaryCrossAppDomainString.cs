using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E9 RID: 1257
	internal sealed class BinaryCrossAppDomainString
	{
		// Token: 0x06002769 RID: 10089 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0009EF54 File Offset: 0x0009D154
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001350 RID: 4944
		internal int objectId;

		// Token: 0x04001351 RID: 4945
		internal int value;
	}
}
