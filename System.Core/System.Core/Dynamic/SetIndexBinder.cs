using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic set index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200013F RID: 319
	public abstract class SetIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="args">An array of arguments of the dynamic set index operation.</param>
		// Token: 0x06000A77 RID: 2679 RVA: 0x00028118 File Offset: 0x00026318
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length >= 2, "args");
			DynamicMetaObject dynamicMetaObject = args[args.Length - 1];
			DynamicMetaObject[] array = args.RemoveLast<DynamicMetaObject>();
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(array, "args");
			return target.BindSetIndex(this, array, dynamicMetaObject);
		}

		/// <summary>Performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		// Token: 0x06000A78 RID: 2680 RVA: 0x0002817C File Offset: 0x0002637C
		public DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			return this.FallbackSetIndex(target, indexes, value, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A79 RID: 2681
		public abstract DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value, DynamicMetaObject errorSuggestion);
	}
}
