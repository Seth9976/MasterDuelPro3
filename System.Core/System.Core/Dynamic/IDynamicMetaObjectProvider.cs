using System;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents a dynamic object, that can have its operations bound at runtime.</summary>
	// Token: 0x0200013C RID: 316
	public interface IDynamicMetaObjectProvider
	{
		/// <summary>Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.</returns>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		// Token: 0x06000A6D RID: 2669
		DynamicMetaObject GetMetaObject(Expression parameter);
	}
}
