using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x020000BC RID: 188
	[MovedFrom(true, "UnityEditor", "UnityEditor", null)]
	public class ExpressionEvaluator
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00009534 File Offset: 0x00007734
		internal static bool Evaluate<T>(string expression, out T value, out ExpressionEvaluator.Expression delayed)
		{
			value = default(T);
			delayed = null;
			bool flag = ExpressionEvaluator.TryParse<T>(expression, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				ExpressionEvaluator.Expression expr = new ExpressionEvaluator.Expression(expression);
				bool hasVariables = expr.hasVariables;
				if (hasVariables)
				{
					value = default(T);
					delayed = expr;
					flag2 = false;
				}
				else
				{
					flag2 = ExpressionEvaluator.EvaluateTokens<T>(expr.rpnTokens, ref value, 0, 1);
				}
			}
			return flag2;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00009590 File Offset: 0x00007790
		private unsafe static bool EvaluateTokens<T>(string[] tokens, ref T value, int index, int count)
		{
			bool res = false;
			bool flag = typeof(T) == typeof(float);
			if (flag)
			{
				double v = (double)(*UnsafeUtility.As<T, float>(ref value));
				res = ExpressionEvaluator.EvaluateDouble(tokens, ref v, index, count);
				float outValue = (float)v;
				value = *UnsafeUtility.As<float, T>(ref outValue);
			}
			else
			{
				bool flag2 = typeof(T) == typeof(int);
				if (flag2)
				{
					double v2 = (double)(*UnsafeUtility.As<T, int>(ref value));
					res = ExpressionEvaluator.EvaluateDouble(tokens, ref v2, index, count);
					int outValue2 = (int)v2;
					value = *UnsafeUtility.As<int, T>(ref outValue2);
				}
				else
				{
					bool flag3 = typeof(T) == typeof(long);
					if (flag3)
					{
						double v3 = (double)(*UnsafeUtility.As<T, long>(ref value));
						res = ExpressionEvaluator.EvaluateDouble(tokens, ref v3, index, count);
						long outValue3 = (long)v3;
						value = *UnsafeUtility.As<long, T>(ref outValue3);
					}
					else
					{
						bool flag4 = typeof(T) == typeof(ulong);
						if (flag4)
						{
							double v4 = *UnsafeUtility.As<T, ulong>(ref value);
							res = ExpressionEvaluator.EvaluateDouble(tokens, ref v4, index, count);
							bool flag5 = v4 < 0.0;
							if (flag5)
							{
								v4 = 0.0;
							}
							ulong outValue4 = (ulong)v4;
							value = *UnsafeUtility.As<ulong, T>(ref outValue4);
						}
						else
						{
							bool flag6 = typeof(T) == typeof(double);
							if (flag6)
							{
								double v5 = *UnsafeUtility.As<T, double>(ref value);
								res = ExpressionEvaluator.EvaluateDouble(tokens, ref v5, index, count);
								value = *UnsafeUtility.As<double, T>(ref v5);
							}
						}
					}
				}
			}
			return res;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00009750 File Offset: 0x00007950
		private static bool EvaluateDouble(string[] tokens, ref double value, int index, int count)
		{
			Stack<string> stack = new Stack<string>();
			foreach (string token in tokens)
			{
				bool flag = ExpressionEvaluator.IsOperator(token);
				if (flag)
				{
					ExpressionEvaluator.Operator oper = ExpressionEvaluator.TokenToOperator(token);
					List<double> values = new List<double>();
					bool parsed = true;
					while (stack.Count > 0 && !ExpressionEvaluator.IsCommand(stack.Peek()) && values.Count < oper.inputs)
					{
						double newValue;
						parsed &= ExpressionEvaluator.TryParse<double>(stack.Pop(), out newValue);
						values.Add(newValue);
					}
					values.Reverse();
					bool flag2 = parsed && values.Count == oper.inputs;
					if (!flag2)
					{
						return false;
					}
					stack.Push(ExpressionEvaluator.EvaluateOp(values.ToArray(), oper.op, index, count).ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					bool flag3 = ExpressionEvaluator.IsVariable(token);
					if (flag3)
					{
						stack.Push((token == "#") ? index.ToString() : value.ToString(CultureInfo.InvariantCulture));
					}
					else
					{
						stack.Push(token);
					}
				}
			}
			bool flag4 = stack.Count == 1;
			if (flag4)
			{
				bool flag5 = ExpressionEvaluator.TryParse<double>(stack.Pop(), out value);
				if (flag5)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000098C0 File Offset: 0x00007AC0
		private static string[] InfixToRPN(string[] tokens)
		{
			Stack<string> operatorStack = new Stack<string>();
			Queue<string> outputQueue = new Queue<string>();
			foreach (string token in tokens)
			{
				bool flag = ExpressionEvaluator.IsCommand(token);
				if (flag)
				{
					char command = token[0];
					bool flag2 = command == '(';
					if (flag2)
					{
						operatorStack.Push(token);
					}
					else
					{
						bool flag3 = command == ')';
						if (flag3)
						{
							while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
							{
								outputQueue.Enqueue(operatorStack.Pop());
							}
							bool flag4 = operatorStack.Count > 0;
							if (flag4)
							{
								operatorStack.Pop();
							}
							bool flag5 = operatorStack.Count > 0 && ExpressionEvaluator.IsDelayedFunction(operatorStack.Peek());
							if (flag5)
							{
								outputQueue.Enqueue(operatorStack.Pop());
							}
						}
						else
						{
							bool flag6 = command == ',';
							if (flag6)
							{
								while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
								{
									outputQueue.Enqueue(operatorStack.Pop());
								}
							}
							else
							{
								ExpressionEvaluator.Operator o = ExpressionEvaluator.TokenToOperator(token);
								while (ExpressionEvaluator.NeedToPop(operatorStack, o))
								{
									outputQueue.Enqueue(operatorStack.Pop());
								}
								operatorStack.Push(token);
							}
						}
					}
				}
				else
				{
					bool flag7 = ExpressionEvaluator.IsDelayedFunction(token);
					if (flag7)
					{
						operatorStack.Push(token);
					}
					else
					{
						outputQueue.Enqueue(token);
					}
				}
			}
			while (operatorStack.Count > 0)
			{
				outputQueue.Enqueue(operatorStack.Pop());
			}
			return outputQueue.ToArray();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00009A74 File Offset: 0x00007C74
		private static bool NeedToPop(Stack<string> operatorStack, ExpressionEvaluator.Operator newOperator)
		{
			bool flag = operatorStack.Count > 0 && newOperator != null;
			if (flag)
			{
				ExpressionEvaluator.Operator topOfStack = ExpressionEvaluator.TokenToOperator(operatorStack.Peek());
				bool flag2 = topOfStack != null;
				if (flag2)
				{
					bool flag3 = (newOperator.associativity == ExpressionEvaluator.Associativity.Left && newOperator.precedence <= topOfStack.precedence) || (newOperator.associativity == ExpressionEvaluator.Associativity.Right && newOperator.precedence < topOfStack.precedence);
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00009AF4 File Offset: 0x00007CF4
		private static string[] ExpressionToTokens(string expression, out bool hasVariables)
		{
			hasVariables = false;
			List<string> result = new List<string>();
			string currentString = "";
			foreach (char currentChar in expression)
			{
				bool flag = ExpressionEvaluator.IsCommand(currentChar.ToString());
				if (flag)
				{
					bool flag2 = currentString.Length > 0;
					if (flag2)
					{
						result.Add(currentString);
					}
					result.Add(currentChar.ToString());
					currentString = "";
				}
				else
				{
					bool flag3 = currentChar != ' ';
					if (flag3)
					{
						currentString += currentChar.ToString();
					}
					else
					{
						bool flag4 = currentString.Length > 0;
						if (flag4)
						{
							result.Add(currentString);
						}
						currentString = "";
					}
				}
			}
			bool flag5 = currentString.Length > 0;
			if (flag5)
			{
				result.Add(currentString);
			}
			hasVariables = result.Any((string f) => ExpressionEvaluator.IsVariable(f) || ExpressionEvaluator.IsDelayedFunction(f));
			return result.ToArray();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00009C04 File Offset: 0x00007E04
		private static bool IsCommand(string token)
		{
			bool flag = token.Length == 1;
			if (flag)
			{
				char c = token[0];
				bool flag2 = c == '(' || c == ')' || c == ',';
				if (flag2)
				{
					return true;
				}
			}
			return ExpressionEvaluator.IsOperator(token);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00009C50 File Offset: 0x00007E50
		private static bool IsVariable(string token)
		{
			bool flag = token.Length == 1;
			bool flag2;
			if (flag)
			{
				char c = token[0];
				flag2 = c == 'x' || c == 'v' || c == 'f' || c == '#';
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00009C94 File Offset: 0x00007E94
		private static bool IsDelayedFunction(string token)
		{
			ExpressionEvaluator.Operator op = ExpressionEvaluator.TokenToOperator(token);
			bool flag = op != null;
			if (flag)
			{
				bool flag2 = op.op == ExpressionEvaluator.Op.Rand || op.op == ExpressionEvaluator.Op.Linear;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private static bool IsOperator(string token)
		{
			return ExpressionEvaluator.s_Operators.ContainsKey(token);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00009CF8 File Offset: 0x00007EF8
		private static ExpressionEvaluator.Operator TokenToOperator(string token)
		{
			ExpressionEvaluator.Operator op;
			return ExpressionEvaluator.s_Operators.TryGetValue(token, out op) ? op : null;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00009D20 File Offset: 0x00007F20
		private static string PreFormatExpression(string expression)
		{
			string result = expression.Trim();
			bool flag = result.Length == 0;
			string text;
			if (flag)
			{
				text = result;
			}
			else
			{
				char lastChar = result[result.Length - 1];
				bool flag2 = ExpressionEvaluator.IsOperator(lastChar.ToString());
				if (flag2)
				{
					result = result.TrimEnd(lastChar);
				}
				bool flag3 = result.Length >= 2 && result[1] == '=';
				if (flag3)
				{
					char op = result[0];
					string expr = result.Substring(2);
					bool flag4 = op == '+';
					if (flag4)
					{
						result = "x+(" + expr + ")";
					}
					bool flag5 = op == '-';
					if (flag5)
					{
						result = "x-(" + expr + ")";
					}
					bool flag6 = op == '*';
					if (flag6)
					{
						result = "x*(" + expr + ")";
					}
					bool flag7 = op == '/';
					if (flag7)
					{
						result = "x/(" + expr + ")";
					}
				}
				text = result;
			}
			return text;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00009E28 File Offset: 0x00008028
		private static string[] FixUnaryOperators(string[] tokens)
		{
			bool flag = tokens.Length == 0;
			string[] array;
			if (flag)
			{
				array = tokens;
			}
			else
			{
				bool flag2 = tokens[0] == "-";
				if (flag2)
				{
					tokens[0] = "_";
				}
				for (int i = 1; i < tokens.Length - 1; i++)
				{
					string token = tokens[i];
					string previousToken = tokens[i - 1];
					bool flag3 = token == "-" && ExpressionEvaluator.IsCommand(previousToken) && previousToken != ")";
					if (flag3)
					{
						tokens[i] = "_";
					}
				}
				array = tokens;
			}
			return array;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00009EBC File Offset: 0x000080BC
		private static double EvaluateOp(double[] values, ExpressionEvaluator.Op op, int index, int count)
		{
			double a = ((values.Length >= 1) ? values[0] : 0.0);
			double b = ((values.Length >= 2) ? values[1] : 0.0);
			double num;
			switch (op)
			{
			case ExpressionEvaluator.Op.Add:
				num = a + b;
				break;
			case ExpressionEvaluator.Op.Sub:
				num = a - b;
				break;
			case ExpressionEvaluator.Op.Mul:
				num = a * b;
				break;
			case ExpressionEvaluator.Op.Div:
				num = a / b;
				break;
			case ExpressionEvaluator.Op.Mod:
				num = a % b;
				break;
			case ExpressionEvaluator.Op.Neg:
				num = -a;
				break;
			case ExpressionEvaluator.Op.Pow:
				num = Math.Pow(a, b);
				break;
			case ExpressionEvaluator.Op.Sqrt:
				num = ((a <= 0.0) ? 0.0 : Math.Sqrt(a));
				break;
			case ExpressionEvaluator.Op.Sin:
				num = Math.Sin(a);
				break;
			case ExpressionEvaluator.Op.Cos:
				num = Math.Cos(a);
				break;
			case ExpressionEvaluator.Op.Tan:
				num = Math.Tan(a);
				break;
			case ExpressionEvaluator.Op.Floor:
				num = Math.Floor(a);
				break;
			case ExpressionEvaluator.Op.Ceil:
				num = Math.Ceiling(a);
				break;
			case ExpressionEvaluator.Op.Round:
				num = Math.Round(a);
				break;
			case ExpressionEvaluator.Op.Rand:
			{
				uint r = ExpressionEvaluator.s_Random.GetUInt() & 16777215U;
				double f = r / 16777215.0;
				num = a + f * (b - a);
				break;
			}
			case ExpressionEvaluator.Op.Linear:
			{
				bool flag = count < 1;
				if (flag)
				{
					count = 1;
				}
				double f2 = ((count < 2) ? 0.5 : ((double)index / (double)(count - 1)));
				num = a + f2 * (b - a);
				break;
			}
			default:
				num = 0.0;
				break;
			}
			return num;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000A068 File Offset: 0x00008268
		private static bool TryParse<T>(string expression, out T result)
		{
			expression = expression.Replace(',', '.');
			string expressionLowerCase = expression.ToLowerInvariant();
			bool flag = expressionLowerCase.Length > 1 && char.IsDigit(expressionLowerCase[expressionLowerCase.Length - 2]);
			if (flag)
			{
				char[] numberDesignator = new char[] { 'f', 'd', 'l' };
				expressionLowerCase = expressionLowerCase.TrimEnd(numberDesignator);
			}
			bool success = false;
			result = default(T);
			bool flag2 = expressionLowerCase.Length == 0;
			bool flag3;
			if (flag2)
			{
				flag3 = true;
			}
			else
			{
				bool flag4 = typeof(T) == typeof(float);
				if (flag4)
				{
					bool flag5 = expressionLowerCase == "pi";
					if (flag5)
					{
						success = true;
						result = (T)((object)3.1415927f);
					}
					else
					{
						float temp;
						success = float.TryParse(expressionLowerCase, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out temp);
						result = (T)((object)temp);
					}
				}
				else
				{
					bool flag6 = typeof(T) == typeof(int);
					if (flag6)
					{
						int temp2;
						success = int.TryParse(expressionLowerCase, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out temp2);
						result = (T)((object)temp2);
					}
					else
					{
						bool flag7 = typeof(T) == typeof(double);
						if (flag7)
						{
							bool flag8 = expressionLowerCase == "pi";
							if (flag8)
							{
								success = true;
								result = (T)((object)3.141592653589793);
							}
							else
							{
								double temp3;
								success = double.TryParse(expressionLowerCase, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out temp3);
								result = (T)((object)temp3);
							}
						}
						else
						{
							bool flag9 = typeof(T) == typeof(long);
							if (flag9)
							{
								long temp4;
								success = long.TryParse(expressionLowerCase, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out temp4);
								result = (T)((object)temp4);
							}
							else
							{
								bool flag10 = typeof(T) == typeof(ulong);
								if (flag10)
								{
									ulong temp5;
									success = ulong.TryParse(expressionLowerCase, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out temp5);
									result = (T)((object)temp5);
								}
							}
						}
					}
				}
				flag3 = success;
			}
			return flag3;
		}

		// Token: 0x04000243 RID: 579
		private static ExpressionEvaluator.PcgRandom s_Random = new ExpressionEvaluator.PcgRandom(0UL, 0UL);

		// Token: 0x04000244 RID: 580
		private static Dictionary<string, ExpressionEvaluator.Operator> s_Operators = new Dictionary<string, ExpressionEvaluator.Operator>
		{
			{
				"-",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sub, 2, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"+",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Add, 2, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"/",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Div, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"*",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Mul, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"%",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Mod, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"^",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Pow, 5, 2, ExpressionEvaluator.Associativity.Right)
			},
			{
				"_",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Neg, 5, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"sqrt",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sqrt, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"cos",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Cos, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"sin",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sin, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"tan",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Tan, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"floor",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Floor, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"ceil",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Ceil, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"round",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Round, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"R",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Rand, 4, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"L",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Linear, 4, 2, ExpressionEvaluator.Associativity.Left)
			}
		};

		// Token: 0x020000BD RID: 189
		internal class Expression
		{
			// Token: 0x060004AD RID: 1197 RVA: 0x0000A448 File Offset: 0x00008648
			internal Expression(string expression)
			{
				expression = ExpressionEvaluator.PreFormatExpression(expression);
				string[] infixTokens = ExpressionEvaluator.ExpressionToTokens(expression, out this.hasVariables);
				infixTokens = ExpressionEvaluator.FixUnaryOperators(infixTokens);
				this.rpnTokens = ExpressionEvaluator.InfixToRPN(infixTokens);
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x0000A488 File Offset: 0x00008688
			public bool Evaluate<T>(ref T value, int index = 0, int count = 1)
			{
				return ExpressionEvaluator.EvaluateTokens<T>(this.rpnTokens, ref value, index, count);
			}

			// Token: 0x04000245 RID: 581
			internal readonly string[] rpnTokens;

			// Token: 0x04000246 RID: 582
			internal readonly bool hasVariables;
		}

		// Token: 0x020000BE RID: 190
		private struct PcgRandom
		{
			// Token: 0x060004AF RID: 1199 RVA: 0x0000A4A8 File Offset: 0x000086A8
			public PcgRandom(ulong state = 0UL, ulong sequence = 0UL)
			{
				this.increment = (sequence << 1) | 1UL;
				this.state = 0UL;
				this.Step();
				this.state += state;
				this.Step();
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0000A4DC File Offset: 0x000086DC
			public uint GetUInt()
			{
				ulong prevState = this.state;
				this.Step();
				return ExpressionEvaluator.PcgRandom.XshRr(prevState);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x0000A502 File Offset: 0x00008702
			private static uint RotateRight(uint v, int rot)
			{
				return (v >> rot) | (v << -rot);
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x0000A515 File Offset: 0x00008715
			private static uint XshRr(ulong s)
			{
				return ExpressionEvaluator.PcgRandom.RotateRight((uint)(((s >> 18) ^ s) >> 27), (int)(s >> 59));
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0000A52B File Offset: 0x0000872B
			private void Step()
			{
				this.state = this.state * 6364136223846793005UL + this.increment;
			}

			// Token: 0x04000247 RID: 583
			private readonly ulong increment;

			// Token: 0x04000248 RID: 584
			private ulong state;
		}

		// Token: 0x020000BF RID: 191
		private enum Op
		{
			// Token: 0x0400024A RID: 586
			Add,
			// Token: 0x0400024B RID: 587
			Sub,
			// Token: 0x0400024C RID: 588
			Mul,
			// Token: 0x0400024D RID: 589
			Div,
			// Token: 0x0400024E RID: 590
			Mod,
			// Token: 0x0400024F RID: 591
			Neg,
			// Token: 0x04000250 RID: 592
			Pow,
			// Token: 0x04000251 RID: 593
			Sqrt,
			// Token: 0x04000252 RID: 594
			Sin,
			// Token: 0x04000253 RID: 595
			Cos,
			// Token: 0x04000254 RID: 596
			Tan,
			// Token: 0x04000255 RID: 597
			Floor,
			// Token: 0x04000256 RID: 598
			Ceil,
			// Token: 0x04000257 RID: 599
			Round,
			// Token: 0x04000258 RID: 600
			Rand,
			// Token: 0x04000259 RID: 601
			Linear
		}

		// Token: 0x020000C0 RID: 192
		private enum Associativity
		{
			// Token: 0x0400025B RID: 603
			Left,
			// Token: 0x0400025C RID: 604
			Right
		}

		// Token: 0x020000C1 RID: 193
		private class Operator
		{
			// Token: 0x060004B4 RID: 1204 RVA: 0x0000A54B File Offset: 0x0000874B
			public Operator(ExpressionEvaluator.Op op, int precedence, int inputs, ExpressionEvaluator.Associativity associativity)
			{
				this.op = op;
				this.precedence = precedence;
				this.inputs = inputs;
				this.associativity = associativity;
			}

			// Token: 0x0400025D RID: 605
			public readonly ExpressionEvaluator.Op op;

			// Token: 0x0400025E RID: 606
			public readonly int precedence;

			// Token: 0x0400025F RID: 607
			public readonly ExpressionEvaluator.Associativity associativity;

			// Token: 0x04000260 RID: 608
			public readonly int inputs;
		}
	}
}
