using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000072 RID: 114
	public enum JsonToken
	{
		// Token: 0x0400029F RID: 671
		None,
		// Token: 0x040002A0 RID: 672
		StartObject,
		// Token: 0x040002A1 RID: 673
		StartArray,
		// Token: 0x040002A2 RID: 674
		StartConstructor,
		// Token: 0x040002A3 RID: 675
		PropertyName,
		// Token: 0x040002A4 RID: 676
		Comment,
		// Token: 0x040002A5 RID: 677
		Raw,
		// Token: 0x040002A6 RID: 678
		Integer,
		// Token: 0x040002A7 RID: 679
		Float,
		// Token: 0x040002A8 RID: 680
		String,
		// Token: 0x040002A9 RID: 681
		Boolean,
		// Token: 0x040002AA RID: 682
		Null,
		// Token: 0x040002AB RID: 683
		Undefined,
		// Token: 0x040002AC RID: 684
		EndObject,
		// Token: 0x040002AD RID: 685
		EndArray,
		// Token: 0x040002AE RID: 686
		EndConstructor,
		// Token: 0x040002AF RID: 687
		Date,
		// Token: 0x040002B0 RID: 688
		Bytes
	}
}
