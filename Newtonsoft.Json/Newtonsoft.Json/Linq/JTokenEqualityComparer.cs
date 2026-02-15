using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000188 RID: 392
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x0003B0FA File Offset: 0x000392FA
		[NullableContext(2)]
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003B103 File Offset: 0x00039303
		[NullableContext(1)]
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
