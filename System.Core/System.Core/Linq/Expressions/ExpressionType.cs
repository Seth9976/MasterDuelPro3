using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the node types for the nodes of an expression tree.</summary>
	// Token: 0x02000097 RID: 151
	public enum ExpressionType
	{
		/// <summary>An addition operation, such as a + b, without overflow checking, for numeric operands.</summary>
		// Token: 0x0400014B RID: 331
		Add,
		/// <summary>An addition operation, such as (a + b), with overflow checking, for numeric operands.</summary>
		// Token: 0x0400014C RID: 332
		AddChecked,
		/// <summary>A bitwise or logical AND operation, such as (a &amp; b) in C# and (a And b) in Visual Basic.</summary>
		// Token: 0x0400014D RID: 333
		And,
		/// <summary>A conditional AND operation that evaluates the second operand only if the first operand evaluates to true. It corresponds to (a &amp;&amp; b) in C# and (a AndAlso b) in Visual Basic.</summary>
		// Token: 0x0400014E RID: 334
		AndAlso,
		/// <summary>An operation that obtains the length of a one-dimensional array, such as array.Length.</summary>
		// Token: 0x0400014F RID: 335
		ArrayLength,
		/// <summary>An indexing operation in a one-dimensional array, such as array[index] in C# or array(index) in Visual Basic.</summary>
		// Token: 0x04000150 RID: 336
		ArrayIndex,
		/// <summary>A method call, such as in the obj.sampleMethod() expression.</summary>
		// Token: 0x04000151 RID: 337
		Call,
		/// <summary>A node that represents a null coalescing operation, such as (a ?? b) in C# or If(a, b) in Visual Basic.</summary>
		// Token: 0x04000152 RID: 338
		Coalesce,
		/// <summary>A conditional operation, such as a &gt; b ? a : b in C# or If(a &gt; b, a, b) in Visual Basic.</summary>
		// Token: 0x04000153 RID: 339
		Conditional,
		/// <summary>A constant value.</summary>
		// Token: 0x04000154 RID: 340
		Constant,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value is too large for the destination type, no exception is thrown.</summary>
		// Token: 0x04000155 RID: 341
		Convert,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value does not fit the destination type, an exception is thrown.</summary>
		// Token: 0x04000156 RID: 342
		ConvertChecked,
		/// <summary>A division operation, such as (a / b), for numeric operands.</summary>
		// Token: 0x04000157 RID: 343
		Divide,
		/// <summary>A node that represents an equality comparison, such as (a == b) in C# or (a = b) in Visual Basic.</summary>
		// Token: 0x04000158 RID: 344
		Equal,
		/// <summary>A bitwise or logical XOR operation, such as (a ^ b) in C# or (a Xor b) in Visual Basic.</summary>
		// Token: 0x04000159 RID: 345
		ExclusiveOr,
		/// <summary>A "greater than" comparison, such as (a &gt; b).</summary>
		// Token: 0x0400015A RID: 346
		GreaterThan,
		/// <summary>A "greater than or equal to" comparison, such as (a &gt;= b).</summary>
		// Token: 0x0400015B RID: 347
		GreaterThanOrEqual,
		/// <summary>An operation that invokes a delegate or lambda expression, such as sampleDelegate.Invoke().</summary>
		// Token: 0x0400015C RID: 348
		Invoke,
		/// <summary>A lambda expression, such as a =&gt; a + a in C# or Function(a) a + a in Visual Basic.</summary>
		// Token: 0x0400015D RID: 349
		Lambda,
		/// <summary>A bitwise left-shift operation, such as (a &lt;&lt; b).</summary>
		// Token: 0x0400015E RID: 350
		LeftShift,
		/// <summary>A "less than" comparison, such as (a &lt; b).</summary>
		// Token: 0x0400015F RID: 351
		LessThan,
		/// <summary>A "less than or equal to" comparison, such as (a &lt;= b).</summary>
		// Token: 0x04000160 RID: 352
		LessThanOrEqual,
		/// <summary>An operation that creates a new <see cref="T:System.Collections.IEnumerable" /> object and initializes it from a list of elements, such as new List&lt;SampleType&gt;(){ a, b, c } in C# or Dim sampleList = { a, b, c } in Visual Basic.</summary>
		// Token: 0x04000161 RID: 353
		ListInit,
		/// <summary>An operation that reads from a field or property, such as obj.SampleProperty.</summary>
		// Token: 0x04000162 RID: 354
		MemberAccess,
		/// <summary>An operation that creates a new object and initializes one or more of its members, such as new Point { X = 1, Y = 2 } in C# or New Point With {.X = 1, .Y = 2} in Visual Basic.</summary>
		// Token: 0x04000163 RID: 355
		MemberInit,
		/// <summary>An arithmetic remainder operation, such as (a % b) in C# or (a Mod b) in Visual Basic.</summary>
		// Token: 0x04000164 RID: 356
		Modulo,
		/// <summary>A multiplication operation, such as (a * b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000165 RID: 357
		Multiply,
		/// <summary>An multiplication operation, such as (a * b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x04000166 RID: 358
		MultiplyChecked,
		/// <summary>An arithmetic negation operation, such as (-a). The object a should not be modified in place.</summary>
		// Token: 0x04000167 RID: 359
		Negate,
		/// <summary>A unary plus operation, such as (+a). The result of a predefined unary plus operation is the value of the operand, but user-defined implementations might have unusual results.</summary>
		// Token: 0x04000168 RID: 360
		UnaryPlus,
		/// <summary>An arithmetic negation operation, such as (-a), that has overflow checking. The object a should not be modified in place.</summary>
		// Token: 0x04000169 RID: 361
		NegateChecked,
		/// <summary>An operation that calls a constructor to create a new object, such as new SampleType().</summary>
		// Token: 0x0400016A RID: 362
		New,
		/// <summary>An operation that creates a new one-dimensional array and initializes it from a list of elements, such as new SampleType[]{a, b, c} in C# or New SampleType(){a, b, c} in Visual Basic.</summary>
		// Token: 0x0400016B RID: 363
		NewArrayInit,
		/// <summary>An operation that creates a new array, in which the bounds for each dimension are specified, such as new SampleType[dim1, dim2] in C# or New SampleType(dim1, dim2) in Visual Basic.</summary>
		// Token: 0x0400016C RID: 364
		NewArrayBounds,
		/// <summary>A bitwise complement or logical negation operation. In C#, it is equivalent to (~a) for integral types and to (!a) for Boolean values. In Visual Basic, it is equivalent to (Not a). The object a should not be modified in place.</summary>
		// Token: 0x0400016D RID: 365
		Not,
		/// <summary>An inequality comparison, such as (a != b) in C# or (a &lt;&gt; b) in Visual Basic.</summary>
		// Token: 0x0400016E RID: 366
		NotEqual,
		/// <summary>A bitwise or logical OR operation, such as (a | b) in C# or (a Or b) in Visual Basic.</summary>
		// Token: 0x0400016F RID: 367
		Or,
		/// <summary>A short-circuiting conditional OR operation, such as (a || b) in C# or (a OrElse b) in Visual Basic.</summary>
		// Token: 0x04000170 RID: 368
		OrElse,
		/// <summary>A reference to a parameter or variable that is defined in the context of the expression. For more information, see <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
		// Token: 0x04000171 RID: 369
		Parameter,
		/// <summary>A mathematical operation that raises a number to a power, such as (a ^ b) in Visual Basic.</summary>
		// Token: 0x04000172 RID: 370
		Power,
		/// <summary>An expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />. A <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> node can contain references to parameters that are defined in the context of the expression it represents.</summary>
		// Token: 0x04000173 RID: 371
		Quote,
		/// <summary>A bitwise right-shift operation, such as (a &gt;&gt; b).</summary>
		// Token: 0x04000174 RID: 372
		RightShift,
		/// <summary>A subtraction operation, such as (a - b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000175 RID: 373
		Subtract,
		/// <summary>An arithmetic subtraction operation, such as (a - b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x04000176 RID: 374
		SubtractChecked,
		/// <summary>An explicit reference or boxing conversion in which null is supplied if the conversion fails, such as (obj as SampleType) in C# or TryCast(obj, SampleType) in Visual Basic.</summary>
		// Token: 0x04000177 RID: 375
		TypeAs,
		/// <summary>A type test, such as obj is SampleType in C# or TypeOf obj is SampleType in Visual Basic.</summary>
		// Token: 0x04000178 RID: 376
		TypeIs,
		/// <summary>An assignment operation, such as (a = b).</summary>
		// Token: 0x04000179 RID: 377
		Assign,
		/// <summary>A block of expressions.</summary>
		// Token: 0x0400017A RID: 378
		Block,
		/// <summary>Debugging information.</summary>
		// Token: 0x0400017B RID: 379
		DebugInfo,
		/// <summary>A unary decrement operation, such as (a - 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x0400017C RID: 380
		Decrement,
		/// <summary>A dynamic operation.</summary>
		// Token: 0x0400017D RID: 381
		Dynamic,
		/// <summary>A default value.</summary>
		// Token: 0x0400017E RID: 382
		Default,
		/// <summary>An extension expression.</summary>
		// Token: 0x0400017F RID: 383
		Extension,
		/// <summary>A "go to" expression, such as goto Label in C# or GoTo Label in Visual Basic.</summary>
		// Token: 0x04000180 RID: 384
		Goto,
		/// <summary>A unary increment operation, such as (a + 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x04000181 RID: 385
		Increment,
		/// <summary>An index operation or an operation that accesses a property that takes arguments. </summary>
		// Token: 0x04000182 RID: 386
		Index,
		/// <summary>A label.</summary>
		// Token: 0x04000183 RID: 387
		Label,
		/// <summary>A list of run-time variables. For more information, see <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		// Token: 0x04000184 RID: 388
		RuntimeVariables,
		/// <summary>A loop, such as for or while.</summary>
		// Token: 0x04000185 RID: 389
		Loop,
		/// <summary>A switch operation, such as switch in C# or Select Case in Visual Basic.</summary>
		// Token: 0x04000186 RID: 390
		Switch,
		/// <summary>An operation that throws an exception, such as throw new Exception().</summary>
		// Token: 0x04000187 RID: 391
		Throw,
		/// <summary>A try-catch expression.</summary>
		// Token: 0x04000188 RID: 392
		Try,
		/// <summary>An unbox value type operation, such as unbox and unbox.any instructions in MSIL. </summary>
		// Token: 0x04000189 RID: 393
		Unbox,
		/// <summary>An addition compound assignment operation, such as (a += b), without overflow checking, for numeric operands.</summary>
		// Token: 0x0400018A RID: 394
		AddAssign,
		/// <summary>A bitwise or logical AND compound assignment operation, such as (a &amp;= b) in C#.</summary>
		// Token: 0x0400018B RID: 395
		AndAssign,
		/// <summary>An division compound assignment operation, such as (a /= b), for numeric operands.</summary>
		// Token: 0x0400018C RID: 396
		DivideAssign,
		/// <summary>A bitwise or logical XOR compound assignment operation, such as (a ^= b) in C#.</summary>
		// Token: 0x0400018D RID: 397
		ExclusiveOrAssign,
		/// <summary>A bitwise left-shift compound assignment, such as (a &lt;&lt;= b).</summary>
		// Token: 0x0400018E RID: 398
		LeftShiftAssign,
		/// <summary>An arithmetic remainder compound assignment operation, such as (a %= b) in C#.</summary>
		// Token: 0x0400018F RID: 399
		ModuloAssign,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000190 RID: 400
		MultiplyAssign,
		/// <summary>A bitwise or logical OR compound assignment, such as (a |= b) in C#.</summary>
		// Token: 0x04000191 RID: 401
		OrAssign,
		/// <summary>A compound assignment operation that raises a number to a power, such as (a ^= b) in Visual Basic.</summary>
		// Token: 0x04000192 RID: 402
		PowerAssign,
		/// <summary>A bitwise right-shift compound assignment operation, such as (a &gt;&gt;= b).</summary>
		// Token: 0x04000193 RID: 403
		RightShiftAssign,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000194 RID: 404
		SubtractAssign,
		/// <summary>An addition compound assignment operation, such as (a += b), with overflow checking, for numeric operands.</summary>
		// Token: 0x04000195 RID: 405
		AddAssignChecked,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x04000196 RID: 406
		MultiplyAssignChecked,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x04000197 RID: 407
		SubtractAssignChecked,
		/// <summary>A unary prefix increment, such as (++a). The object a should be modified in place.</summary>
		// Token: 0x04000198 RID: 408
		PreIncrementAssign,
		/// <summary>A unary prefix decrement, such as (--a). The object a should be modified in place.</summary>
		// Token: 0x04000199 RID: 409
		PreDecrementAssign,
		/// <summary>A unary postfix increment, such as (a++). The object a should be modified in place.</summary>
		// Token: 0x0400019A RID: 410
		PostIncrementAssign,
		/// <summary>A unary postfix decrement, such as (a--). The object a should be modified in place.</summary>
		// Token: 0x0400019B RID: 411
		PostDecrementAssign,
		/// <summary>An exact type test.</summary>
		// Token: 0x0400019C RID: 412
		TypeEqual,
		/// <summary>A ones complement operation, such as (~a) in C#.</summary>
		// Token: 0x0400019D RID: 413
		OnesComplement,
		/// <summary>A true condition value.</summary>
		// Token: 0x0400019E RID: 414
		IsTrue,
		/// <summary>A false condition value.</summary>
		// Token: 0x0400019F RID: 415
		IsFalse
	}
}
