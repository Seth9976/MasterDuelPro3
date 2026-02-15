using System;
using System.Text;

namespace System
{
	// Token: 0x020001F2 RID: 498
	internal interface ModifierSpec
	{
		// Token: 0x06001320 RID: 4896
		Type Resolve(Type type);

		// Token: 0x06001321 RID: 4897
		StringBuilder Append(StringBuilder sb);
	}
}
