using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000023 RID: 35
	public abstract class JsonNameTable
	{
		// Token: 0x060000A0 RID: 160
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract string Get(char[] key, int start, int length);
	}
}
