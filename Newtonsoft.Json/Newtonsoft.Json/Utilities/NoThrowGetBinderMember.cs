using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BE RID: 190
	[NullableContext(1)]
	[Nullable(0)]
	internal class NoThrowGetBinderMember : GetMemberBinder
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001F8BF File Offset: 0x0001DABF
		public NoThrowGetBinderMember(GetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001F8DC File Offset: 0x0001DADC
		public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, [Nullable(2)] DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, CollectionUtils.ArrayEmpty<DynamicMetaObject>());
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x0400043A RID: 1082
		private readonly GetMemberBinder _innerBinder;
	}
}
