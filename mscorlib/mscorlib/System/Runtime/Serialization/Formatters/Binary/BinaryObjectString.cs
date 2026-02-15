using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E8 RID: 1256
	internal sealed class BinaryObjectString
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryObjectString()
		{
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0009EF09 File Offset: 0x0009D109
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0009EF19 File Offset: 0x0009D119
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0009EF3A File Offset: 0x0009D13A
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x0400134E RID: 4942
		internal int objectId;

		// Token: 0x0400134F RID: 4943
		internal string value;
	}
}
