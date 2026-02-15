using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents an initializer for a single element of an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
	// Token: 0x02000094 RID: 148
	public sealed class ElementInit : IArgumentProvider
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x000132CB File Offset: 0x000114CB
		internal ElementInit(MethodInfo addMethod, ReadOnlyCollection<Expression> arguments)
		{
			this.AddMethod = addMethod;
			this.Arguments = arguments;
		}

		/// <summary>Gets the instance method that is used to add an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that adds an element to a collection.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000132E1 File Offset: 0x000114E1
		public MethodInfo AddMethod { get; }

		/// <summary>Gets the collection of arguments that are passed to a method that adds an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments for a method that adds an element to a collection.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000132E9 File Offset: 0x000114E9
		public ReadOnlyCollection<Expression> Arguments { get; }

		// Token: 0x06000454 RID: 1108 RVA: 0x000132F1 File Offset: 0x000114F1
		public Expression GetArgument(int index)
		{
			return this.Arguments[index];
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000132FF File Offset: 0x000114FF
		public int ArgumentCount
		{
			get
			{
				return this.Arguments.Count;
			}
		}

		/// <summary>Returns a textual representation of an <see cref="T:System.Linq.Expressions.ElementInit" /> object.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.ElementInit" /> object.</returns>
		// Token: 0x06000456 RID: 1110 RVA: 0x0001330C File Offset: 0x0001150C
		public override string ToString()
		{
			return ExpressionStringBuilder.ElementInitBindingToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property of the result.</param>
		// Token: 0x06000457 RID: 1111 RVA: 0x00013314 File Offset: 0x00011514
		public ElementInit Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			return Expression.ElementInit(this.AddMethod, arguments);
		}
	}
}
