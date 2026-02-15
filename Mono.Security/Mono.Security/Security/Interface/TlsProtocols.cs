using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000047 RID: 71
	[Flags]
	public enum TlsProtocols
	{
		// Token: 0x040001EA RID: 490
		Zero = 0,
		// Token: 0x040001EB RID: 491
		Tls10Client = 128,
		// Token: 0x040001EC RID: 492
		Tls10Server = 64,
		// Token: 0x040001ED RID: 493
		Tls10 = 192,
		// Token: 0x040001EE RID: 494
		Tls11Client = 512,
		// Token: 0x040001EF RID: 495
		Tls11Server = 256,
		// Token: 0x040001F0 RID: 496
		Tls11 = 768,
		// Token: 0x040001F1 RID: 497
		Tls12Client = 2048,
		// Token: 0x040001F2 RID: 498
		Tls12Server = 1024,
		// Token: 0x040001F3 RID: 499
		Tls12 = 3072,
		// Token: 0x040001F4 RID: 500
		ClientMask = 2688,
		// Token: 0x040001F5 RID: 501
		ServerMask = 1344
	}
}
