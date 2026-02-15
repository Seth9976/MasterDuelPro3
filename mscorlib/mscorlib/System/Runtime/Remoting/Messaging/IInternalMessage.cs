using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000489 RID: 1161
	internal interface IInternalMessage
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600255D RID: 9565
		// (set) Token: 0x0600255E RID: 9566
		Identity TargetIdentity { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600255F RID: 9567
		// (set) Token: 0x06002560 RID: 9568
		string Uri { get; set; }

		// Token: 0x06002561 RID: 9569
		bool HasProperties();
	}
}
