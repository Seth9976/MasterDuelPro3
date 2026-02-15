using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic get member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200013B RID: 315
	public abstract class GetMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.GetMemberBinder" />.</summary>
		/// <param name="name">The name of the member to obtain.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002804C File Offset: 0x0002624C
		protected GetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00016CAA File Offset: 0x00014EAA
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to obtain.</summary>
		/// <returns>The name of the member to obtain.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002806D File Offset: 0x0002626D
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00028075 File Offset: 0x00026275
		public bool IgnoreCase { get; }

		/// <summary>Performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		// Token: 0x06000A69 RID: 2665 RVA: 0x0002807D File Offset: 0x0002627D
		public DynamicMetaObject FallbackGetMember(DynamicMetaObject target)
		{
			return this.FallbackGetMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x06000A6A RID: 2666
		public abstract DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="args">An array of arguments of the dynamic get member operation.</param>
		// Token: 0x06000A6B RID: 2667 RVA: 0x00028087 File Offset: 0x00026287
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindGetMember(this);
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00009F9F File Offset: 0x0000819F
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}
	}
}
