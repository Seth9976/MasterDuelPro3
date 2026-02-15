using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x0200063E RID: 1598
	internal abstract class RtFieldInfo : FieldInfo
	{
		// Token: 0x06002FAC RID: 12204
		internal abstract object UnsafeGetValue(object obj);

		// Token: 0x06002FAD RID: 12205
		internal abstract void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06002FAE RID: 12206
		internal abstract void CheckConsistency(object target);
	}
}
