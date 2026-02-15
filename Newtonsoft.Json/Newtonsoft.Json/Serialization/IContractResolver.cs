using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200010D RID: 269
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x060007C1 RID: 1985
		JsonContract ResolveContract(Type type);
	}
}
