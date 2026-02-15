using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the invoke dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200013D RID: 317
	public abstract class InvokeBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		// Token: 0x06000A6E RID: 2670 RVA: 0x000280B0 File Offset: 0x000262B0
		public DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvoke(target, args, null);
		}

		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A6F RID: 2671
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke operation.</param>
		// Token: 0x06000A70 RID: 2672 RVA: 0x000280BB File Offset: 0x000262BB
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvoke(this, args);
		}
	}
}
