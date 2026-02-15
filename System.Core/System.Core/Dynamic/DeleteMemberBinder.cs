using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200012A RID: 298
	public abstract class DeleteMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Gets the name of the member to delete.</summary>
		/// <returns>The name of the member to delete.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002651A File Offset: 0x0002471A
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the string comparison should ignore the case, otherwise false.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00026522 File Offset: 0x00024722
		public bool IgnoreCase { get; }

		/// <summary>Performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		// Token: 0x060009D2 RID: 2514 RVA: 0x0002652A File Offset: 0x0002472A
		public DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target)
		{
			return this.FallbackDeleteMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060009D3 RID: 2515
		public abstract DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete member operation.</param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x00026534 File Offset: 0x00024734
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindDeleteMember(this);
		}
	}
}
