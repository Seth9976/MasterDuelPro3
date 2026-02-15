using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic binding and a binding logic of an object participating in the dynamic binding.</summary>
	// Token: 0x0200012B RID: 299
	public class DynamicMetaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		// Token: 0x060009D5 RID: 2517 RVA: 0x0002655D File Offset: 0x0002475D
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			this.Expression = expression;
			this.Restrictions = restrictions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		/// <param name="value">The runtime value represented by the <see cref="T:System.Dynamic.DynamicMetaObject" />.</param>
		// Token: 0x060009D6 RID: 2518 RVA: 0x00026594 File Offset: 0x00024794
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value)
			: this(expression, restrictions)
		{
			this._value = value;
		}

		/// <summary>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</summary>
		/// <returns>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000265A5 File Offset: 0x000247A5
		public Expression Expression { get; }

		/// <summary>The set of binding restrictions under which the binding is valid.</summary>
		/// <returns>The set of binding restrictions.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000265AD File Offset: 0x000247AD
		public BindingRestrictions Restrictions { get; }

		/// <summary>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000265B5 File Offset: 0x000247B5
		public object Value
		{
			get
			{
				if (!this.HasValue)
				{
					return null;
				}
				return this._value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value.</summary>
		/// <returns>True if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value, otherwise false.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000265C7 File Offset: 0x000247C7
		public bool HasValue
		{
			get
			{
				return this._value != DynamicMetaObject.s_noValueSentinel;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the runtime value or null if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has no value associated with it.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the runtime value or null.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000265DC File Offset: 0x000247DC
		public Type RuntimeType
		{
			get
			{
				if (!this.HasValue)
				{
					return null;
				}
				Type type = this.Expression.Type;
				if (type.IsValueType)
				{
					return type;
				}
				object value = this.Value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		/// <summary>Gets the limit type of the <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>
		///   <see cref="P:System.Dynamic.DynamicMetaObject.RuntimeType" /> if runtime value is available, a type of the <see cref="P:System.Dynamic.DynamicMetaObject.Expression" /> otherwise.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002661A File Offset: 0x0002481A
		public Type LimitType
		{
			get
			{
				return this.RuntimeType ?? this.Expression.Type;
			}
		}

		/// <summary>Performs the binding of the dynamic conversion operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.ConvertBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060009DD RID: 2525 RVA: 0x00026631 File Offset: 0x00024831
		public virtual DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackConvert(this);
		}

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetMemberBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060009DE RID: 2526 RVA: 0x00026645 File Offset: 0x00024845
		public virtual DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetMember(this);
		}

		/// <summary>Performs the binding of the dynamic set member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set member operation.</param>
		// Token: 0x060009DF RID: 2527 RVA: 0x00026659 File Offset: 0x00024859
		public virtual DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetMember(this, value);
		}

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteMemberBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002666E File Offset: 0x0002486E
		public virtual DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteMember(this);
		}

		/// <summary>Performs the binding of the dynamic get index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the get index operation.</param>
		// Token: 0x060009E1 RID: 2529 RVA: 0x00026682 File Offset: 0x00024882
		public virtual DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the set index operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set index operation.</param>
		// Token: 0x060009E2 RID: 2530 RVA: 0x00026697 File Offset: 0x00024897
		public virtual DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetIndex(this, indexes, value);
		}

		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the delete index operation.</param>
		// Token: 0x060009E3 RID: 2531 RVA: 0x000266AD File Offset: 0x000248AD
		public virtual DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke member operation.</param>
		// Token: 0x060009E4 RID: 2532 RVA: 0x000266C2 File Offset: 0x000248C2
		public virtual DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvokeMember(this, args);
		}

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke operation.</param>
		// Token: 0x060009E5 RID: 2533 RVA: 0x000266D7 File Offset: 0x000248D7
		public virtual DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvoke(this, args);
		}

		/// <summary>Performs the binding of the dynamic create instance operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.CreateInstanceBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the create instance operation.</param>
		// Token: 0x060009E6 RID: 2534 RVA: 0x000266EC File Offset: 0x000248EC
		public virtual DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackCreateInstance(this, args);
		}

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.UnaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		// Token: 0x060009E7 RID: 2535 RVA: 0x00026701 File Offset: 0x00024901
		public virtual DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackUnaryOperation(this);
		}

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="arg">An instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the right hand side of the binary operation.</param>
		// Token: 0x060009E8 RID: 2536 RVA: 0x00026715 File Offset: 0x00024915
		public virtual DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackBinaryOperation(this, arg);
		}

		/// <summary>Returns the enumeration of all dynamic member names.</summary>
		/// <returns>The list of dynamic member names.</returns>
		// Token: 0x060009E9 RID: 2537 RVA: 0x0002672A File Offset: 0x0002492A
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return Array.Empty<string>();
		}

		/// <summary>Creates a meta-object for the specified object.</summary>
		/// <returns>If the given object implements <see cref="T:System.Dynamic.IDynamicMetaObjectProvider" /> and is not a remote object from outside the current AppDomain, returns the object's specific meta-object returned by <see cref="M:System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression)" />. Otherwise a plain new meta-object with no restrictions is created and returned.</returns>
		/// <param name="value">The object to get a meta-object for.</param>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		// Token: 0x060009EA RID: 2538 RVA: 0x00026734 File Offset: 0x00024934
		public static DynamicMetaObject Create(object value, Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = value as IDynamicMetaObjectProvider;
			if (dynamicMetaObjectProvider == null)
			{
				return new DynamicMetaObject(expression, BindingRestrictions.Empty, value);
			}
			DynamicMetaObject metaObject = dynamicMetaObjectProvider.GetMetaObject(expression);
			if (metaObject == null || !metaObject.HasValue || metaObject.Value == null || metaObject.Expression != expression)
			{
				throw Error.InvalidMetaObjectCreated(dynamicMetaObjectProvider.GetType());
			}
			return metaObject;
		}

		/// <summary>Represents an empty array of type <see cref="T:System.Dynamic.DynamicMetaObject" />. This field is read only.</summary>
		// Token: 0x0400030B RID: 779
		public static readonly DynamicMetaObject[] EmptyMetaObjects = Array.Empty<DynamicMetaObject>();

		// Token: 0x0400030C RID: 780
		private static readonly object s_noValueSentinel = new object();

		// Token: 0x0400030D RID: 781
		private readonly object _value = DynamicMetaObject.s_noValueSentinel;
	}
}
