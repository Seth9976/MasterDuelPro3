using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000071 RID: 113
	internal sealed class ExprException
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x0001F767 File Offset: 0x0001D967
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001F776 File Offset: 0x0001D976
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001F785 File Offset: 0x0001D985
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001F794 File Offset: 0x0001D994
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001F794 File Offset: 0x0001D994
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001F7A3 File Offset: 0x0001D9A3
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument("Need a row or a table to Invoke DataFilter.");
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001F7AF File Offset: 0x0001D9AF
		public static Exception NYI(string moreinfo)
		{
			return ExprException._Expr(SR.Format("The feature not implemented. {0}.", moreinfo));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001F7C1 File Offset: 0x0001D9C1
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", Operators.ToString(before._op)));
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001F7DD File Offset: 0x0001D9DD
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", token));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001F7EF File Offset: 0x0001D9EF
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(SR.Format("Type mismatch in expression '{0}'.", expr));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001F801 File Offset: 0x0001DA01
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, SR.Format("{0}() argument is out of range.", func));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001F814 File Offset: 0x0001DA14
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval("Expression is too complex.");
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001F820 File Offset: 0x0001DA20
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(SR.Format("Cannot find column [{0}].", name));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001F832 File Offset: 0x0001DA32
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(SR.Format("The expression contains an invalid string constant: {0}.", str));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001F844 File Offset: 0x0001DA44
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(SR.Format("The expression contains undefined function call {0}().", name));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001F856 File Offset: 0x0001DA56
		public static Exception SyntaxError()
		{
			return ExprException._Syntax("Syntax error in the expression.");
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001F862 File Offset: 0x0001DA62
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(SR.Format("Invalid number of arguments: function {0}().", name));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001F874 File Offset: 0x0001DA74
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax("The expression is missing the closing parenthesis.");
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001F880 File Offset: 0x0001DA80
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(SR.Format("Cannot interpret token '{0}' at position {1}.", token, position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001F89E File Offset: 0x0001DA9E
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(SR.Format("Expected {0}, but actual token at the position {2} is {1}.", tokExpected.ToString(), tokCurr.ToString(), position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001F8D5 File Offset: 0x0001DAD5
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot convert from {0} to {1}.", type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001F8F2 File Offset: 0x0001DAF2
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(SR.Format("Cannot convert value '{0}' to Type: {1}.", value.ToString(), type.ToString()), innerException);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001F910 File Offset: 0x0001DB10
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(SR.Format("Invalid column name [{0}].", name));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001F922 File Offset: 0x0001DB22
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid date constant '{0}'.", date));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001F934 File Offset: 0x0001DB34
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval("Only constant expressions are allowed in the expression list for the IN operator.");
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001F940 File Offset: 0x0001DB40
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(SR.Format("Error in Like operator: the string pattern '{0}' is invalid.", pat));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001F952 File Offset: 0x0001DB52
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax("Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.");
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001F95E File Offset: 0x0001DB5E
		public static Exception InWithoutList()
		{
			return ExprException._Syntax("Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.");
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001F96A File Offset: 0x0001DB6A
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax("Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.");
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001F976 File Offset: 0x0001DB76
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(SR.Format("Value is either too large or too small for Type '{0}'.", type.Name));
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001F98D File Offset: 0x0001DB8D
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected {2}.", function, arg.ToString(CultureInfo.InvariantCulture), type.ToString()));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001F9B1 File Offset: 0x0001DBB1
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.", function, arg.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001F9CF File Offset: 0x0001DBCF
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot perform '{0}' operation on {1} and {2}.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001F9F2 File Offset: 0x0001DBF2
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001FA15 File Offset: 0x0001DC15
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(SR.Format("The expression contains unsupported operator '{0}'.", Operators.ToString(op)));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid name: '{0}'.", name));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001FA3E File Offset: 0x0001DC3E
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand before '{0}' operator.", op));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001FA50 File Offset: 0x0001DC50
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax("The expression has too many closing parentheses.");
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(SR.Format("The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.", name, expr));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001FA6F File Offset: 0x0001DC6F
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(SR.Format("Cannot find the parent relation '{0}'.", relationName));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001FA81 File Offset: 0x0001DC81
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax("Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.");
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001FA8D File Offset: 0x0001DC8D
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the aggregate expression '{0}'.", expr));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001FA9F File Offset: 0x0001DC9F
		public static Exception EvalNoContext()
		{
			return ExprException._Eval("Cannot evaluate non-constant expression without current row.");
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001FAAB File Offset: 0x0001DCAB
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the expression '{0}'.", expr));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001FABD File Offset: 0x0001DCBD
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(SR.Format("Cannot evaluate. Expression '{0}' is not an aggregate.", expr));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001FACF File Offset: 0x0001DCCF
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(SR.Format("Filter expression '{0}' does not evaluate to a Boolean term.", expr));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001FAE1 File Offset: 0x0001DCE1
		public static Exception LookupArgument()
		{
			return ExprException._Syntax("Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.");
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001FAED File Offset: 0x0001DCED
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(SR.Format("Invalid type name '{0}'.", typeName));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001FAFF File Offset: 0x0001DCFF
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval("'hours' argument is out of range. Value must be between -14 and +14.");
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001FB0B File Offset: 0x0001DD0B
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval("'minutes' argument is out of range. Value must be between -59 and +59.");
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001FB17 File Offset: 0x0001DD17
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval("Provided range for time one exceeds total of 14 hours.");
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001FB23 File Offset: 0x0001DD23
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval("Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.");
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001FB2F File Offset: 0x0001DD2F
		public static Exception UnsupportedDataType(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("A DataColumn of type '{0}' does not support expression.", type.FullName));
		}
	}
}
