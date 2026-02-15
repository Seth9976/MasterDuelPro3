using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200050E RID: 1294
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x040014C3 RID: 5315
		NoArgs = 1,
		// Token: 0x040014C4 RID: 5316
		ArgsInline = 2,
		// Token: 0x040014C5 RID: 5317
		ArgsIsArray = 4,
		// Token: 0x040014C6 RID: 5318
		ArgsInArray = 8,
		// Token: 0x040014C7 RID: 5319
		NoContext = 16,
		// Token: 0x040014C8 RID: 5320
		ContextInline = 32,
		// Token: 0x040014C9 RID: 5321
		ContextInArray = 64,
		// Token: 0x040014CA RID: 5322
		MethodSignatureInArray = 128,
		// Token: 0x040014CB RID: 5323
		PropertyInArray = 256,
		// Token: 0x040014CC RID: 5324
		NoReturnValue = 512,
		// Token: 0x040014CD RID: 5325
		ReturnValueVoid = 1024,
		// Token: 0x040014CE RID: 5326
		ReturnValueInline = 2048,
		// Token: 0x040014CF RID: 5327
		ReturnValueInArray = 4096,
		// Token: 0x040014D0 RID: 5328
		ExceptionInArray = 8192,
		// Token: 0x040014D1 RID: 5329
		GenericMethod = 32768
	}
}
