using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A7 RID: 167
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000577 RID: 1399
		[Nullable(1)]
		object UnderlyingDictionary
		{
			[NullableContext(1)]
			get;
		}
	}
}
