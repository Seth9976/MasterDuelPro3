using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the invoke member dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200013E RID: 318
	public abstract class InvokeMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Gets the name of the member to invoke.</summary>
		/// <returns>The name of the member to invoke.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000280DB File Offset: 0x000262DB
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x000280E3 File Offset: 0x000262E3
		public bool IgnoreCase { get; }

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke member operation.</param>
		// Token: 0x06000A73 RID: 2675 RVA: 0x000280EB File Offset: 0x000262EB
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvokeMember(this, args);
		}

		/// <summary>Performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		// Token: 0x06000A74 RID: 2676 RVA: 0x0002810B File Offset: 0x0002630B
		public DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvokeMember(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A75 RID: 2677
		public abstract DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A76 RID: 2678
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);
	}
}
