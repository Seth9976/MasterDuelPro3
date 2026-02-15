using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E4 RID: 1252
	internal sealed class BinaryCrossAppDomainAssembly
	{
		// Token: 0x0600274F RID: 10063 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0009E3D2 File Offset: 0x0009C5D2
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001331 RID: 4913
		internal int assemId;

		// Token: 0x04001332 RID: 4914
		internal int assemblyIndex;
	}
}
