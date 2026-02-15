using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents the binary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200011E RID: 286
	public abstract class BinaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>The binary operation kind.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> object representing the kind of binary operation.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00026039 File Offset: 0x00024239
		public ExpressionType Operation { get; }

		/// <summary>Performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		// Token: 0x060009A9 RID: 2473 RVA: 0x00026041 File Offset: 0x00024241
		public DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg)
		{
			return this.FallbackBinaryOperation(target, arg, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		/// <param name="errorSuggestion">The binding result if the binding fails, or null.</param>
		// Token: 0x060009AA RID: 2474
		public abstract DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		// Token: 0x060009AB RID: 2475 RVA: 0x0002604C File Offset: 0x0002424C
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject dynamicMetaObject = args[0];
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			return target.BindBinaryOperation(this, dynamicMetaObject);
		}
	}
}
