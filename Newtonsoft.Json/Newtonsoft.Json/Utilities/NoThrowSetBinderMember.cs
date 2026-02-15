using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	internal class NoThrowSetBinderMember : SetMemberBinder
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001F916 File Offset: 0x0001DB16
		public NoThrowSetBinderMember(SetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001F934 File Offset: 0x0001DB34
		public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, [Nullable(2)] DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, new DynamicMetaObject[] { value });
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x0400043B RID: 1083
		private readonly SetMemberBinder _innerBinder;
	}
}
