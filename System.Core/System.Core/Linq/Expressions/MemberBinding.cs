using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent bindings that are used to initialize members of a newly created object derive.</summary>
	// Token: 0x020000B5 RID: 181
	public abstract class MemberBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.MemberBinding" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Linq.Expressions.MemberBindingType" /> that discriminates the type of binding that is represented.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to be initialized.</param>
		// Token: 0x060005F7 RID: 1527 RVA: 0x000160BA File Offset: 0x000142BA
		[Obsolete("Do not use this constructor. It will be removed in future releases.")]
		protected MemberBinding(MemberBindingType type, MemberInfo member)
		{
			this.BindingType = type;
			this.Member = member;
		}

		/// <summary>Gets the type of binding that is represented.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.MemberBindingType" /> values.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000160D0 File Offset: 0x000142D0
		public MemberBindingType BindingType { get; }

		/// <summary>Gets the field or property to be initialized.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be initialized.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000160D8 File Offset: 0x000142D8
		public MemberInfo Member { get; }

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</returns>
		// Token: 0x060005FA RID: 1530 RVA: 0x000160E0 File Offset: 0x000142E0
		public override string ToString()
		{
			return ExpressionStringBuilder.MemberBindingToString(this);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000160E8 File Offset: 0x000142E8
		internal virtual void ValidateAsDefinedHere(int index)
		{
			throw Error.UnknownBindingType(index);
		}
	}
}
