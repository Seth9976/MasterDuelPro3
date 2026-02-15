using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic get index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200013A RID: 314
	public abstract class GetIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the dynamic get index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="args">An array of arguments of the dynamic get index operation.</param>
		// Token: 0x06000A62 RID: 2658 RVA: 0x00028021 File Offset: 0x00026221
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindGetIndex(this, args);
		}

		/// <summary>Performs the binding of the dynamic get index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="indexes">The arguments of the dynamic get index operation.</param>
		// Token: 0x06000A63 RID: 2659 RVA: 0x00028041 File Offset: 0x00026241
		public DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackGetIndex(target, indexes, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic get index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="indexes">The arguments of the dynamic get index operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A64 RID: 2660
		public abstract DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);
	}
}
