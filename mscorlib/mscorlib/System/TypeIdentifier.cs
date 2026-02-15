using System;

namespace System
{
	// Token: 0x020001EA RID: 490
	internal interface TypeIdentifier : TypeName, IEquatable<TypeName>
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001302 RID: 4866
		string InternalName { get; }
	}
}
