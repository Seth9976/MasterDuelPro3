using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000B8 RID: 184
	internal sealed class PropertyExpression : MemberExpression
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x000161A0 File Offset: 0x000143A0
		public PropertyExpression(Expression expression, PropertyInfo member)
			: base(expression)
		{
			this._property = member;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000161B0 File Offset: 0x000143B0
		internal override MemberInfo GetMember()
		{
			return this._property;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000161B8 File Offset: 0x000143B8
		public sealed override Type Type
		{
			get
			{
				return this._property.PropertyType;
			}
		}

		// Token: 0x040001DA RID: 474
		private readonly PropertyInfo _property;
	}
}
