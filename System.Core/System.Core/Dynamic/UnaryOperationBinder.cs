using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the unary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000141 RID: 321
	public abstract class UnaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic unary operation.</param>
		// Token: 0x06000A82 RID: 2690 RVA: 0x00028211 File Offset: 0x00026411
		public DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target)
		{
			return this.FallbackUnaryOperation(target, null);
		}

		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic unary operation.</param>
		/// <param name="errorSuggestion">The binding result in case the binding fails, or null.</param>
		// Token: 0x06000A83 RID: 2691
		public abstract DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0002821B File Offset: 0x0002641B
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindUnaryOperation(this);
		}
	}
}
