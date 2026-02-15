using System;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents a named parameter expression.</summary>
	// Token: 0x020000CE RID: 206
	[DebuggerTypeProxy(typeof(Expression.ParameterExpressionProxy))]
	public class ParameterExpression : Expression
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x00016B61 File Offset: 0x00014D61
		internal ParameterExpression(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00016B70 File Offset: 0x00014D70
		internal static ParameterExpression Make(Type type, string name, bool isByRef)
		{
			if (isByRef)
			{
				return new ByRefParameterExpression(type, name);
			}
			if (!type.IsEnum)
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.Object:
					if (type == typeof(object))
					{
						return new ParameterExpression(name);
					}
					if (type == typeof(Exception))
					{
						return new PrimitiveParameterExpression<Exception>(name);
					}
					if (type == typeof(object[]))
					{
						return new PrimitiveParameterExpression<object[]>(name);
					}
					break;
				case TypeCode.Boolean:
					return new PrimitiveParameterExpression<bool>(name);
				case TypeCode.Char:
					return new PrimitiveParameterExpression<char>(name);
				case TypeCode.SByte:
					return new PrimitiveParameterExpression<sbyte>(name);
				case TypeCode.Byte:
					return new PrimitiveParameterExpression<byte>(name);
				case TypeCode.Int16:
					return new PrimitiveParameterExpression<short>(name);
				case TypeCode.UInt16:
					return new PrimitiveParameterExpression<ushort>(name);
				case TypeCode.Int32:
					return new PrimitiveParameterExpression<int>(name);
				case TypeCode.UInt32:
					return new PrimitiveParameterExpression<uint>(name);
				case TypeCode.Int64:
					return new PrimitiveParameterExpression<long>(name);
				case TypeCode.UInt64:
					return new PrimitiveParameterExpression<ulong>(name);
				case TypeCode.Single:
					return new PrimitiveParameterExpression<float>(name);
				case TypeCode.Double:
					return new PrimitiveParameterExpression<double>(name);
				case TypeCode.Decimal:
					return new PrimitiveParameterExpression<decimal>(name);
				case TypeCode.DateTime:
					return new PrimitiveParameterExpression<DateTime>(name);
				case TypeCode.String:
					return new PrimitiveParameterExpression<string>(name);
				}
			}
			return new TypedParameterExpression(type, name);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ParameterExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00016CAA File Offset: 0x00014EAA
		public override Type Type
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00016CB6 File Offset: 0x00014EB6
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Parameter;
			}
		}

		/// <summary>Gets the name of the parameter or variable.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the parameter.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00016CBA File Offset: 0x00014EBA
		public string Name { get; }

		/// <summary>Indicates that this ParameterExpression is to be treated as a ByRef parameter.</summary>
		/// <returns>True if this ParameterExpression is a ByRef parameter, otherwise false.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00016CC2 File Offset: 0x00014EC2
		public bool IsByRef
		{
			get
			{
				return this.GetIsByRef();
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000B252 File Offset: 0x00009452
		internal virtual bool GetIsByRef()
		{
			return false;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000677 RID: 1655 RVA: 0x00016CCA File Offset: 0x00014ECA
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitParameter(this);
		}
	}
}
