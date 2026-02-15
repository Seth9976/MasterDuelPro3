using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the convert dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000127 RID: 295
	public abstract class ConvertBinder : DynamicMetaObjectBinder
	{
		/// <summary>The type to convert to.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type to convert to.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00026489 File Offset: 0x00024689
		public Type Type { get; }

		/// <summary>Performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		// Token: 0x060009C7 RID: 2503 RVA: 0x00026491 File Offset: 0x00024691
		public DynamicMetaObject FallbackConvert(DynamicMetaObject target)
		{
			return this.FallbackConvert(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060009C8 RID: 2504
		public abstract DynamicMetaObject FallbackConvert(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic convert operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="args">An array of arguments of the dynamic convert operation.</param>
		// Token: 0x060009C9 RID: 2505 RVA: 0x0002649B File Offset: 0x0002469B
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindConvert(this);
		}
	}
}
