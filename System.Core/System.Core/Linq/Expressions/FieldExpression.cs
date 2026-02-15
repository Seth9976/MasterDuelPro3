using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000B7 RID: 183
	internal sealed class FieldExpression : MemberExpression
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0001617B File Offset: 0x0001437B
		public FieldExpression(Expression expression, FieldInfo member)
			: base(expression)
		{
			this._field = member;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001618B File Offset: 0x0001438B
		internal override MemberInfo GetMember()
		{
			return this._field;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00016193 File Offset: 0x00014393
		public sealed override Type Type
		{
			get
			{
				return this._field.FieldType;
			}
		}

		// Token: 0x040001D9 RID: 473
		private readonly FieldInfo _field;
	}
}
