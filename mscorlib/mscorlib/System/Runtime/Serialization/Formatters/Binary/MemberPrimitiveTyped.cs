using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004EB RID: 1259
	internal sealed class MemberPrimitiveTyped
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0009EF7C File Offset: 0x0009D17C
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0009EF8C File Offset: 0x0009D18C
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0009EFB4 File Offset: 0x0009D1B4
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001353 RID: 4947
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x04001354 RID: 4948
		internal object value;
	}
}
