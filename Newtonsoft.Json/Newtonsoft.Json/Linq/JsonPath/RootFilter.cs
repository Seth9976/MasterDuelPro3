using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001A6 RID: 422
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000E7B RID: 3707 RVA: 0x0003D0D2 File Offset: 0x0003B2D2
		private RootFilter()
		{
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003FE53 File Offset: 0x0003E053
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[] { root };
		}

		// Token: 0x040007D8 RID: 2008
		public static readonly RootFilter Instance = new RootFilter();
	}
}
