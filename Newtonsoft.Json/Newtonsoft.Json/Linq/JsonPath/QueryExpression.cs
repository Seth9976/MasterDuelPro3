using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x0200019F RID: 415
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class QueryExpression
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x0003F3D9 File Offset: 0x0003D5D9
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
		public bool IsMatch(JToken root, JToken t)
		{
			return this.IsMatch(root, t, null);
		}

		// Token: 0x06000E57 RID: 3671
		public abstract bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings);

		// Token: 0x040007BA RID: 1978
		internal QueryOperator Operator;
	}
}
