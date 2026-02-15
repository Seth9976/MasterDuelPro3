using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000129 RID: 297
	public abstract class DeleteIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete index operation.</param>
		// Token: 0x060009CD RID: 2509 RVA: 0x000264EF File Offset: 0x000246EF
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindDeleteIndex(this, args);
		}

		/// <summary>Performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		// Token: 0x060009CE RID: 2510 RVA: 0x0002650F File Offset: 0x0002470F
		public DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackDeleteIndex(target, indexes, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060009CF RID: 2511
		public abstract DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);
	}
}
