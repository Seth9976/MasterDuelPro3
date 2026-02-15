using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000111 RID: 273
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x060007CA RID: 1994
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x060007CB RID: 1995
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
