using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;

namespace System.Linq.Expressions
{
	/// <summary>Describes a lambda expression. This captures a block of code that is similar to a .NET method body.</summary>
	// Token: 0x020000A9 RID: 169
	[DebuggerTypeProxy(typeof(Expression.LambdaExpressionProxy))]
	public abstract class LambdaExpression : Expression, IParameterProvider
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00015C29 File Offset: 0x00013E29
		internal LambdaExpression(Expression body)
		{
			this._body = body;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LambdaExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015C38 File Offset: 0x00013E38
		public sealed override Type Type
		{
			get
			{
				return this.TypeCore;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005AD RID: 1453
		internal abstract Type TypeCore { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005AE RID: 1454
		internal abstract Type PublicType { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00015C40 File Offset: 0x00013E40
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Lambda;
			}
		}

		/// <summary>Gets the parameters of the lambda expression.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects that represent the parameters of the lambda expression.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00015C44 File Offset: 0x00013E44
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this.GetOrMakeParameters();
			}
		}

		/// <summary>Gets the name of the lambda expression.</summary>
		/// <returns>The name of the lambda expression.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015C4C File Offset: 0x00013E4C
		public string Name
		{
			get
			{
				return this.NameCore;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000C30F File Offset: 0x0000A50F
		internal virtual string NameCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the body of the lambda expression.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the body of the lambda expression.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00015C54 File Offset: 0x00013E54
		public Expression Body
		{
			get
			{
				return this._body;
			}
		}

		/// <summary>Gets the return type of the lambda expression.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the lambda expression.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00015C5C File Offset: 0x00013E5C
		public Type ReturnType
		{
			get
			{
				return this.Type.GetInvokeMethod().ReturnType;
			}
		}

		/// <summary>Gets the value that indicates if the lambda expression will be compiled with the tail call optimization.</summary>
		/// <returns>True if the lambda expression will be compiled with the tail call optimization, otherwise false.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00015C6E File Offset: 0x00013E6E
		public bool TailCall
		{
			get
			{
				return this.TailCallCore;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000B252 File Offset: 0x00009452
		internal virtual bool TailCallCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00015C76 File Offset: 0x00013E76
		[ExcludeFromCodeCoverage]
		ParameterExpression IParameterProvider.GetParameter(int index)
		{
			return this.GetParameter(index);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual ParameterExpression GetParameter(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00015C7F File Offset: 0x00013E7F
		[ExcludeFromCodeCoverage]
		int IParameterProvider.ParameterCount
		{
			get
			{
				return this.ParameterCount;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual int ParameterCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060005BC RID: 1468
		internal abstract LambdaExpression Accept(StackSpiller spiller);

		// Token: 0x040001C2 RID: 450
		private readonly Expression _body;
	}
}
