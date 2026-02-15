using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001A2 RID: 418
	[NullableContext(1)]
	[Nullable(0)]
	internal class QueryFilter : PathFilter
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0003F927 File Offset: 0x0003DB27
		public QueryFilter(QueryExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003F936 File Offset: 0x0003DB36
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken jtoken in current)
			{
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
				{
					if (this.Expression.IsMatch(root, jtoken2, settings))
					{
						yield return jtoken2;
					}
				}
				IEnumerator<JToken> enumerator2 = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040007BE RID: 1982
		internal QueryExpression Expression;
	}
}
