using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004D3 RID: 1235
	internal enum BinaryHeaderEnum
	{
		// Token: 0x040012BB RID: 4795
		SerializedStreamHeader,
		// Token: 0x040012BC RID: 4796
		Object,
		// Token: 0x040012BD RID: 4797
		ObjectWithMap,
		// Token: 0x040012BE RID: 4798
		ObjectWithMapAssemId,
		// Token: 0x040012BF RID: 4799
		ObjectWithMapTyped,
		// Token: 0x040012C0 RID: 4800
		ObjectWithMapTypedAssemId,
		// Token: 0x040012C1 RID: 4801
		ObjectString,
		// Token: 0x040012C2 RID: 4802
		Array,
		// Token: 0x040012C3 RID: 4803
		MemberPrimitiveTyped,
		// Token: 0x040012C4 RID: 4804
		MemberReference,
		// Token: 0x040012C5 RID: 4805
		ObjectNull,
		// Token: 0x040012C6 RID: 4806
		MessageEnd,
		// Token: 0x040012C7 RID: 4807
		Assembly,
		// Token: 0x040012C8 RID: 4808
		ObjectNullMultiple256,
		// Token: 0x040012C9 RID: 4809
		ObjectNullMultiple,
		// Token: 0x040012CA RID: 4810
		ArraySinglePrimitive,
		// Token: 0x040012CB RID: 4811
		ArraySingleObject,
		// Token: 0x040012CC RID: 4812
		ArraySingleString,
		// Token: 0x040012CD RID: 4813
		CrossAppDomainMap,
		// Token: 0x040012CE RID: 4814
		CrossAppDomainString,
		// Token: 0x040012CF RID: 4815
		CrossAppDomainAssembly,
		// Token: 0x040012D0 RID: 4816
		MethodCall,
		// Token: 0x040012D1 RID: 4817
		MethodReturn
	}
}
