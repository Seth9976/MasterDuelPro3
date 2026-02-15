using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000199 RID: 409
	internal enum TypeFlags
	{
		// Token: 0x040008FC RID: 2300
		None,
		// Token: 0x040008FD RID: 2301
		Abstract,
		// Token: 0x040008FE RID: 2302
		Reference,
		// Token: 0x040008FF RID: 2303
		Special = 4,
		// Token: 0x04000900 RID: 2304
		CanBeAttributeValue = 8,
		// Token: 0x04000901 RID: 2305
		CanBeTextValue = 16,
		// Token: 0x04000902 RID: 2306
		CanBeElementValue = 32,
		// Token: 0x04000903 RID: 2307
		HasCustomFormatter = 64,
		// Token: 0x04000904 RID: 2308
		AmbiguousDataType = 128,
		// Token: 0x04000905 RID: 2309
		IgnoreDefault = 512,
		// Token: 0x04000906 RID: 2310
		HasIsEmpty = 1024,
		// Token: 0x04000907 RID: 2311
		HasDefaultConstructor = 2048,
		// Token: 0x04000908 RID: 2312
		XmlEncodingNotRequired = 4096,
		// Token: 0x04000909 RID: 2313
		UseReflection = 16384,
		// Token: 0x0400090A RID: 2314
		CollapseWhitespace = 32768,
		// Token: 0x0400090B RID: 2315
		OptionalValue = 65536,
		// Token: 0x0400090C RID: 2316
		CtorInaccessible = 131072,
		// Token: 0x0400090D RID: 2317
		UsePrivateImplementation = 262144,
		// Token: 0x0400090E RID: 2318
		GenericInterface = 524288,
		// Token: 0x0400090F RID: 2319
		Unsupported = 1048576
	}
}
