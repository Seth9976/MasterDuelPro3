using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004ED RID: 1261
	internal sealed class BinaryObjectWithMapTyped
	{
		// Token: 0x0600277A RID: 10106 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryObjectWithMapTyped()
		{
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0009F10A File Offset: 0x0009D30A
		internal BinaryObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0009F11C File Offset: 0x0009D31C
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, int assemId)
		{
			this.objectId = objectId;
			this.assemId = assemId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.memberAssemIds = memberAssemIds;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTypedAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTyped;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0009F184 File Offset: 0x0009D384
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
			for (int j = 0; j < this.numMembers; j++)
			{
				sout.WriteByte((byte)this.binaryTypeEnumA[j]);
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnumA[k], this.typeInformationA[k], this.memberAssemIds[k], sout);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0009F248 File Offset: 0x0009D448
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			this.binaryTypeEnumA = new BinaryTypeEnum[this.numMembers];
			this.typeInformationA = new object[this.numMembers];
			this.memberAssemIds = new int[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				this.binaryTypeEnumA[j] = (BinaryTypeEnum)input.ReadByte();
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				if (this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUrt && this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUser)
				{
					this.typeInformationA[k] = BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
				else
				{
					BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x0400135B RID: 4955
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x0400135C RID: 4956
		internal int objectId;

		// Token: 0x0400135D RID: 4957
		internal string name;

		// Token: 0x0400135E RID: 4958
		internal int numMembers;

		// Token: 0x0400135F RID: 4959
		internal string[] memberNames;

		// Token: 0x04001360 RID: 4960
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04001361 RID: 4961
		internal object[] typeInformationA;

		// Token: 0x04001362 RID: 4962
		internal int[] memberAssemIds;

		// Token: 0x04001363 RID: 4963
		internal int assemId;
	}
}
