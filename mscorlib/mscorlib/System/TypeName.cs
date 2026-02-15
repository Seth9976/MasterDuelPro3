using System;

namespace System
{
	// Token: 0x020001E9 RID: 489
	internal interface TypeName : IEquatable<TypeName>
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001300 RID: 4864
		string DisplayName { get; }

		// Token: 0x06001301 RID: 4865
		TypeName NestedName(TypeIdentifier innerName);
	}
}
