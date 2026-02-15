using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E3 RID: 1251
	internal sealed class BinaryAssembly
	{
		// Token: 0x0600274A RID: 10058 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryAssembly()
		{
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0009E386 File Offset: 0x0009C586
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0009E396 File Offset: 0x0009C596
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x0009E3B8 File Offset: 0x0009C5B8
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x0400132F RID: 4911
		internal int assemId;

		// Token: 0x04001330 RID: 4912
		internal string assemblyString;
	}
}
