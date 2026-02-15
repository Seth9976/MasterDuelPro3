using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic create operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000128 RID: 296
	public abstract class CreateInstanceBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		// Token: 0x060009CA RID: 2506 RVA: 0x000264C4 File Offset: 0x000246C4
		public DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackCreateInstance(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060009CB RID: 2507
		public abstract DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic create operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">An array of arguments of the dynamic create operation.</param>
		// Token: 0x060009CC RID: 2508 RVA: 0x000264CF File Offset: 0x000246CF
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindCreateInstance(this, args);
		}
	}
}
