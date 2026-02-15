using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class MemberReference
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal MemberReference()
		{
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0009F7A8 File Offset: 0x0009D9A8
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0009F7B1 File Offset: 0x0009D9B1
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0009F7C7 File Offset: 0x0009D9C7
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x0400136F RID: 4975
		internal int idRef;
	}
}
