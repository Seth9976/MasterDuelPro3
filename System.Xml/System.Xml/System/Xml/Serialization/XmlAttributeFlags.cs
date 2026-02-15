using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001A8 RID: 424
	internal enum XmlAttributeFlags
	{
		// Token: 0x0400094A RID: 2378
		Enum = 1,
		// Token: 0x0400094B RID: 2379
		Array,
		// Token: 0x0400094C RID: 2380
		Text = 4,
		// Token: 0x0400094D RID: 2381
		ArrayItems = 8,
		// Token: 0x0400094E RID: 2382
		Elements = 16,
		// Token: 0x0400094F RID: 2383
		Attribute = 32,
		// Token: 0x04000950 RID: 2384
		Root = 64,
		// Token: 0x04000951 RID: 2385
		Type = 128,
		// Token: 0x04000952 RID: 2386
		AnyElements = 256,
		// Token: 0x04000953 RID: 2387
		AnyAttribute = 512,
		// Token: 0x04000954 RID: 2388
		ChoiceIdentifier = 1024,
		// Token: 0x04000955 RID: 2389
		XmlnsDeclarations = 2048
	}
}
