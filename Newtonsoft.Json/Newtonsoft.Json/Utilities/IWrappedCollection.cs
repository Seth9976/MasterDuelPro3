using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009C RID: 156
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000518 RID: 1304
		[Nullable(1)]
		object UnderlyingCollection
		{
			[NullableContext(1)]
			get;
		}
	}
}
