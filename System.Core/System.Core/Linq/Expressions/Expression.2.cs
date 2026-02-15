using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;

namespace System.Linq.Expressions
{
	/// <summary>Represents a strongly typed lambda expression as a data structure in the form of an expression tree. This class cannot be inherited.</summary>
	/// <typeparam name="TDelegate">The type of the delegate that the <see cref="T:System.Linq.Expressions.Expression`1" /> represents.</typeparam>
	// Token: 0x020000AA RID: 170
	public class Expression<TDelegate> : LambdaExpression
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00015C87 File Offset: 0x00013E87
		internal Expression(Expression body)
			: base(body)
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00015C90 File Offset: 0x00013E90
		internal sealed override Type TypeCore
		{
			get
			{
				return typeof(TDelegate);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00015C9C File Offset: 0x00013E9C
		internal override Type PublicType
		{
			get
			{
				return typeof(Expression<TDelegate>);
			}
		}

		/// <summary>Compiles the lambda expression described by the expression tree into executable code and produces a delegate that represents the lambda expression.</summary>
		/// <returns>A delegate of type <paramref name="TDelegate" /> that represents the compiled lambda expression described by the <see cref="T:System.Linq.Expressions.Expression`1" />.</returns>
		// Token: 0x060005C0 RID: 1472 RVA: 0x00015CA8 File Offset: 0x00013EA8
		public TDelegate Compile()
		{
			return this.Compile(false);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015CB1 File Offset: 0x00013EB1
		public TDelegate Compile(bool preferInterpretation)
		{
			return (TDelegate)((object)LambdaCompiler.Compile(this));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00015CBE File Offset: 0x00013EBE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLambda<TDelegate>(this);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00015CC7 File Offset: 0x00013EC7
		internal override LambdaExpression Accept(StackSpiller spiller)
		{
			return spiller.Rewrite<TDelegate>(this);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015CD0 File Offset: 0x00013ED0
		internal static Expression<TDelegate> Create(Expression body, string name, bool tailCall, IReadOnlyList<ParameterExpression> parameters)
		{
			if (name != null || tailCall)
			{
				return new FullExpression<TDelegate>(body, name, tailCall, parameters);
			}
			switch (parameters.Count)
			{
			case 0:
				return new Expression0<TDelegate>(body);
			case 1:
				return new Expression1<TDelegate>(body, parameters[0]);
			case 2:
				return new Expression2<TDelegate>(body, parameters[0], parameters[1]);
			case 3:
				return new Expression3<TDelegate>(body, parameters[0], parameters[1], parameters[2]);
			default:
				return new ExpressionN<TDelegate>(body, parameters);
			}
		}
	}
}
