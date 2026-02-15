using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004EF RID: 1263
	internal sealed class MemberPrimitiveUnTyped
	{
		// Token: 0x06002784 RID: 10116 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0009F767 File Offset: 0x0009D967
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009F777 File Offset: 0x0009D977
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0009F780 File Offset: 0x0009D980
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0009F794 File Offset: 0x0009D994
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x0400136D RID: 4973
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x0400136E RID: 4974
		internal object value;
	}
}
