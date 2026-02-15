using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004EA RID: 1258
	internal sealed class BinaryCrossAppDomainMap
	{
		// Token: 0x0600276C RID: 10092 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0009EF6E File Offset: 0x0009D16E
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001352 RID: 4946
		internal int crossAppDomainArrayIndex;
	}
}
