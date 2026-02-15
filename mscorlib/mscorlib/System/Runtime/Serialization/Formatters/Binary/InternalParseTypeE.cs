using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004D7 RID: 1239
	internal enum InternalParseTypeE
	{
		// Token: 0x040012E6 RID: 4838
		Empty,
		// Token: 0x040012E7 RID: 4839
		SerializedStreamHeader,
		// Token: 0x040012E8 RID: 4840
		Object,
		// Token: 0x040012E9 RID: 4841
		Member,
		// Token: 0x040012EA RID: 4842
		ObjectEnd,
		// Token: 0x040012EB RID: 4843
		MemberEnd,
		// Token: 0x040012EC RID: 4844
		Headers,
		// Token: 0x040012ED RID: 4845
		HeadersEnd,
		// Token: 0x040012EE RID: 4846
		SerializedStreamHeaderEnd,
		// Token: 0x040012EF RID: 4847
		Envelope,
		// Token: 0x040012F0 RID: 4848
		EnvelopeEnd,
		// Token: 0x040012F1 RID: 4849
		Body,
		// Token: 0x040012F2 RID: 4850
		BodyEnd
	}
}
