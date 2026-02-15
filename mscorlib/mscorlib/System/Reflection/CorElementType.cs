using System;

namespace System.Reflection
{
	// Token: 0x0200062E RID: 1582
	[Serializable]
	internal enum CorElementType : byte
	{
		// Token: 0x040017BC RID: 6076
		End,
		// Token: 0x040017BD RID: 6077
		Void,
		// Token: 0x040017BE RID: 6078
		Boolean,
		// Token: 0x040017BF RID: 6079
		Char,
		// Token: 0x040017C0 RID: 6080
		I1,
		// Token: 0x040017C1 RID: 6081
		U1,
		// Token: 0x040017C2 RID: 6082
		I2,
		// Token: 0x040017C3 RID: 6083
		U2,
		// Token: 0x040017C4 RID: 6084
		I4,
		// Token: 0x040017C5 RID: 6085
		U4,
		// Token: 0x040017C6 RID: 6086
		I8,
		// Token: 0x040017C7 RID: 6087
		U8,
		// Token: 0x040017C8 RID: 6088
		R4,
		// Token: 0x040017C9 RID: 6089
		R8,
		// Token: 0x040017CA RID: 6090
		String,
		// Token: 0x040017CB RID: 6091
		Ptr,
		// Token: 0x040017CC RID: 6092
		ByRef,
		// Token: 0x040017CD RID: 6093
		ValueType,
		// Token: 0x040017CE RID: 6094
		Class,
		// Token: 0x040017CF RID: 6095
		Var,
		// Token: 0x040017D0 RID: 6096
		Array,
		// Token: 0x040017D1 RID: 6097
		GenericInst,
		// Token: 0x040017D2 RID: 6098
		TypedByRef,
		// Token: 0x040017D3 RID: 6099
		I = 24,
		// Token: 0x040017D4 RID: 6100
		U,
		// Token: 0x040017D5 RID: 6101
		FnPtr = 27,
		// Token: 0x040017D6 RID: 6102
		Object,
		// Token: 0x040017D7 RID: 6103
		SzArray,
		// Token: 0x040017D8 RID: 6104
		MVar,
		// Token: 0x040017D9 RID: 6105
		CModReqd,
		// Token: 0x040017DA RID: 6106
		CModOpt,
		// Token: 0x040017DB RID: 6107
		Internal,
		// Token: 0x040017DC RID: 6108
		Max,
		// Token: 0x040017DD RID: 6109
		Modifier = 64,
		// Token: 0x040017DE RID: 6110
		Sentinel,
		// Token: 0x040017DF RID: 6111
		Pinned = 69,
		// Token: 0x040017E0 RID: 6112
		ELEMENT_TYPE_END = 0,
		// Token: 0x040017E1 RID: 6113
		ELEMENT_TYPE_VOID,
		// Token: 0x040017E2 RID: 6114
		ELEMENT_TYPE_BOOLEAN,
		// Token: 0x040017E3 RID: 6115
		ELEMENT_TYPE_CHAR,
		// Token: 0x040017E4 RID: 6116
		ELEMENT_TYPE_I1,
		// Token: 0x040017E5 RID: 6117
		ELEMENT_TYPE_U1,
		// Token: 0x040017E6 RID: 6118
		ELEMENT_TYPE_I2,
		// Token: 0x040017E7 RID: 6119
		ELEMENT_TYPE_U2,
		// Token: 0x040017E8 RID: 6120
		ELEMENT_TYPE_I4,
		// Token: 0x040017E9 RID: 6121
		ELEMENT_TYPE_U4,
		// Token: 0x040017EA RID: 6122
		ELEMENT_TYPE_I8,
		// Token: 0x040017EB RID: 6123
		ELEMENT_TYPE_U8,
		// Token: 0x040017EC RID: 6124
		ELEMENT_TYPE_R4,
		// Token: 0x040017ED RID: 6125
		ELEMENT_TYPE_R8,
		// Token: 0x040017EE RID: 6126
		ELEMENT_TYPE_STRING,
		// Token: 0x040017EF RID: 6127
		ELEMENT_TYPE_PTR,
		// Token: 0x040017F0 RID: 6128
		ELEMENT_TYPE_BYREF,
		// Token: 0x040017F1 RID: 6129
		ELEMENT_TYPE_VALUETYPE,
		// Token: 0x040017F2 RID: 6130
		ELEMENT_TYPE_CLASS,
		// Token: 0x040017F3 RID: 6131
		ELEMENT_TYPE_VAR,
		// Token: 0x040017F4 RID: 6132
		ELEMENT_TYPE_ARRAY,
		// Token: 0x040017F5 RID: 6133
		ELEMENT_TYPE_GENERICINST,
		// Token: 0x040017F6 RID: 6134
		ELEMENT_TYPE_TYPEDBYREF,
		// Token: 0x040017F7 RID: 6135
		ELEMENT_TYPE_I = 24,
		// Token: 0x040017F8 RID: 6136
		ELEMENT_TYPE_U,
		// Token: 0x040017F9 RID: 6137
		ELEMENT_TYPE_FNPTR = 27,
		// Token: 0x040017FA RID: 6138
		ELEMENT_TYPE_OBJECT,
		// Token: 0x040017FB RID: 6139
		ELEMENT_TYPE_SZARRAY,
		// Token: 0x040017FC RID: 6140
		ELEMENT_TYPE_MVAR,
		// Token: 0x040017FD RID: 6141
		ELEMENT_TYPE_CMOD_REQD,
		// Token: 0x040017FE RID: 6142
		ELEMENT_TYPE_CMOD_OPT,
		// Token: 0x040017FF RID: 6143
		ELEMENT_TYPE_INTERNAL,
		// Token: 0x04001800 RID: 6144
		ELEMENT_TYPE_MAX,
		// Token: 0x04001801 RID: 6145
		ELEMENT_TYPE_MODIFIER = 64,
		// Token: 0x04001802 RID: 6146
		ELEMENT_TYPE_SENTINEL,
		// Token: 0x04001803 RID: 6147
		ELEMENT_TYPE_PINNED = 69
	}
}
