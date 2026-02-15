using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Dynamic.Utils
{
	// Token: 0x02000148 RID: 328
	internal static class ExpressionUtils
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		public static ReadOnlyCollection<ParameterExpression> ReturnReadOnly(IParameterProvider provider, ref object collection)
		{
			ParameterExpression parameterExpression = collection as ParameterExpression;
			if (parameterExpression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<ParameterExpression>(new ListParameterProvider(provider, parameterExpression)), parameterExpression);
			}
			return (ReadOnlyCollection<ParameterExpression>)collection;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		public static ReadOnlyCollection<T> ReturnReadOnly<T>(ref IReadOnlyList<T> collection)
		{
			IReadOnlyList<T> readOnlyList = collection;
			ReadOnlyCollection<T> readOnlyCollection = readOnlyList as ReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			Interlocked.CompareExchange<IReadOnlyList<T>>(ref collection, readOnlyList.ToReadOnly<T>(), readOnlyList);
			return (ReadOnlyCollection<T>)collection;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002A928 File Offset: 0x00028B28
		public static T ReturnObject<T>(object collectionOrT) where T : class
		{
			T t = collectionOrT as T;
			if (t != null)
			{
				return t;
			}
			return ((ReadOnlyCollection<T>)collectionOrT)[0];
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002A958 File Offset: 0x00028B58
		public static void ValidateArgumentTypes(MethodBase method, ExpressionType nodeKind, ref ReadOnlyCollection<Expression> arguments, string methodParamName)
		{
			ParameterInfo[] parametersForValidation = ExpressionUtils.GetParametersForValidation(method, nodeKind);
			ExpressionUtils.ValidateArgumentCount(method, nodeKind, arguments.Count, parametersForValidation);
			Expression[] array = null;
			int i = 0;
			int num = parametersForValidation.Length;
			while (i < num)
			{
				Expression expression = arguments[i];
				ParameterInfo parameterInfo = parametersForValidation[i];
				expression = ExpressionUtils.ValidateOneArgument(method, nodeKind, expression, parameterInfo, methodParamName, "arguments", i);
				if (array == null && expression != arguments[i])
				{
					array = new Expression[arguments.Count];
					for (int j = 0; j < i; j++)
					{
						array[j] = arguments[j];
					}
				}
				if (array != null)
				{
					array[i] = expression;
				}
				i++;
			}
			if (array != null)
			{
				arguments = new TrueReadOnlyCollection<Expression>(array);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002AA00 File Offset: 0x00028C00
		public static void ValidateArgumentCount(MethodBase method, ExpressionType nodeKind, int count, ParameterInfo[] pis)
		{
			if (pis.Length != count)
			{
				if (nodeKind <= ExpressionType.Invoke)
				{
					if (nodeKind != ExpressionType.Call)
					{
						if (nodeKind != ExpressionType.Invoke)
						{
							goto IL_003A;
						}
						throw Error.IncorrectNumberOfLambdaArguments();
					}
				}
				else
				{
					if (nodeKind == ExpressionType.New)
					{
						throw Error.IncorrectNumberOfConstructorArguments();
					}
					if (nodeKind != ExpressionType.Dynamic)
					{
						goto IL_003A;
					}
				}
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
				IL_003A:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002AA50 File Offset: 0x00028C50
		public static Expression ValidateOneArgument(MethodBase method, ExpressionType nodeKind, Expression arguments, ParameterInfo pi, string methodParamName, string argumentParamName, int index = -1)
		{
			ExpressionUtils.RequiresCanRead(arguments, argumentParamName, index);
			Type type = pi.ParameterType;
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			TypeUtils.ValidateType(type, methodParamName, true, true);
			if (!TypeUtils.AreReferenceAssignable(type, arguments.Type) && !ExpressionUtils.TryQuote(type, ref arguments))
			{
				if (nodeKind <= ExpressionType.Invoke)
				{
					if (nodeKind != ExpressionType.Call)
					{
						if (nodeKind != ExpressionType.Invoke)
						{
							goto IL_0092;
						}
						throw Error.ExpressionTypeDoesNotMatchParameter(arguments.Type, type, argumentParamName, index);
					}
				}
				else
				{
					if (nodeKind == ExpressionType.New)
					{
						throw Error.ExpressionTypeDoesNotMatchConstructorParameter(arguments.Type, type, argumentParamName, index);
					}
					if (nodeKind != ExpressionType.Dynamic)
					{
						goto IL_0092;
					}
				}
				throw Error.ExpressionTypeDoesNotMatchMethodParameter(arguments.Type, type, method, argumentParamName, index);
				IL_0092:
				throw ContractUtils.Unreachable;
			}
			return arguments;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002AAF6 File Offset: 0x00028CF6
		public static void RequiresCanRead(Expression expression, string paramName)
		{
			ExpressionUtils.RequiresCanRead(expression, paramName, -1);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002AB00 File Offset: 0x00028D00
		public static void RequiresCanRead(Expression expression, string paramName, int idx)
		{
			ContractUtils.RequiresNotNull(expression, paramName, idx);
			ExpressionType nodeType = expression.NodeType;
			if (nodeType != ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.Index)
				{
					IndexExpression indexExpression = (IndexExpression)expression;
					if (indexExpression.Indexer != null && !indexExpression.Indexer.CanRead)
					{
						throw Error.ExpressionMustBeReadable(paramName, idx);
					}
				}
			}
			else
			{
				PropertyInfo propertyInfo = ((MemberExpression)expression).Member as PropertyInfo;
				if (propertyInfo != null && !propertyInfo.CanRead)
				{
					throw Error.ExpressionMustBeReadable(paramName, idx);
				}
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002AB7A File Offset: 0x00028D7A
		public static bool TryQuote(Type parameterType, ref Expression argument)
		{
			if (TypeUtils.IsSameOrSubclass(typeof(LambdaExpression), parameterType) && parameterType.IsInstanceOfType(argument))
			{
				argument = Expression.Quote(argument);
				return true;
			}
			return false;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		internal static ParameterInfo[] GetParametersForValidation(MethodBase method, ExpressionType nodeKind)
		{
			ParameterInfo[] array = method.GetParametersCached();
			if (nodeKind == ExpressionType.Dynamic)
			{
				array = array.RemoveFirst<ParameterInfo>();
			}
			return array;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		internal static bool SameElements<T>(ref IEnumerable<T> replacement, IReadOnlyList<T> current) where T : class
		{
			if (replacement == current)
			{
				return true;
			}
			if (replacement == null)
			{
				return current.Count == 0;
			}
			ICollection<T> collection = replacement as ICollection<T>;
			if (collection == null)
			{
				replacement = (collection = replacement.ToReadOnly<T>());
			}
			return ExpressionUtils.SameElementsInCollection<T>(collection, current);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002AC08 File Offset: 0x00028E08
		private static bool SameElementsInCollection<T>(ICollection<T> replacement, IReadOnlyList<T> current) where T : class
		{
			int count = current.Count;
			if (replacement.Count != count)
			{
				return false;
			}
			if (count != 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = replacement.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current != current[num])
						{
							return false;
						}
						num++;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002AC80 File Offset: 0x00028E80
		public static void ValidateArgumentCount(this LambdaExpression lambda)
		{
			if (((IParameterProvider)lambda).ParameterCount >= 65535)
			{
				throw Error.InvalidProgram();
			}
		}
	}
}
