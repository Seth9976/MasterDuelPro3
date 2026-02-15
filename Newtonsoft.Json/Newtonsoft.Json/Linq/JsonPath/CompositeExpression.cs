using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001A0 RID: 416
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003F3F3 File Offset: 0x0003D5F3
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0003F3FB File Offset: 0x0003D5FB
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003F404 File Offset: 0x0003D604
		public CompositeExpression(QueryOperator @operator)
			: base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003F418 File Offset: 0x0003D618
		public override bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			QueryOperator @operator = this.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t, settings))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t, settings))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
