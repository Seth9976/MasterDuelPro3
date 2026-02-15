using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004EC RID: 1260
	internal sealed class BinaryObjectWithMap
	{
		// Token: 0x06002774 RID: 10100 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0009EFD4 File Offset: 0x0009D1D4
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0009EFE3 File Offset: 0x0009D1E3
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0009F020 File Offset: 0x0009D220
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0009F094 File Offset: 0x0009D294
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001355 RID: 4949
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04001356 RID: 4950
		internal int objectId;

		// Token: 0x04001357 RID: 4951
		internal string name;

		// Token: 0x04001358 RID: 4952
		internal int numMembers;

		// Token: 0x04001359 RID: 4953
		internal string[] memberNames;

		// Token: 0x0400135A RID: 4954
		internal int assemId;
	}
}
