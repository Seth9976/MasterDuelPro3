using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000065 RID: 101
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		internal ConstNode(DataTable table, ValueType type, object constant)
			: this(table, type, constant, true)
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		internal ConstNode(DataTable table, ValueType type, object constant, bool fParseQuotes)
			: base(table)
		{
			switch (type)
			{
			case ValueType.Null:
				this._val = DBNull.Value;
				return;
			case ValueType.Bool:
				this._val = Convert.ToBoolean(constant, CultureInfo.InvariantCulture);
				return;
			case ValueType.Numeric:
				this._val = this.SmallestNumeric(constant);
				return;
			case ValueType.Str:
				if (fParseQuotes)
				{
					this._val = ((string)constant).Replace("''", "'");
					return;
				}
				this._val = (string)constant;
				return;
			case ValueType.Float:
				this._val = Convert.ToDouble(constant, NumberFormatInfo.InvariantInfo);
				return;
			case ValueType.Decimal:
				this._val = this.SmallestDecimal(constant);
				return;
			case ValueType.Date:
				this._val = DateTime.Parse((string)constant, CultureInfo.InvariantCulture);
				return;
			}
			this._val = constant;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001D6C5 File Offset: 0x0001B8C5
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001D6CE File Offset: 0x0001B8CE
		internal override object Eval()
		{
			return this._val;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001D6D6 File Offset: 0x0001B8D6
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001D6D6 File Offset: 0x0001B8D6
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000593A File Offset: 0x00003B3A
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000593A File Offset: 0x00003B3A
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000207F File Offset: 0x0000027F
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		private object SmallestDecimal(object constant)
		{
			if (constant == null)
			{
				return 0.0;
			}
			string text = constant as string;
			if (text != null)
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToDecimal(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001D818 File Offset: 0x0001BA18
		private object SmallestNumeric(object constant)
		{
			if (constant == null)
			{
				return 0;
			}
			string text = constant as string;
			if (text != null)
			{
				int num;
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
				double num3;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num3))
				{
					return num3;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToInt32(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToInt64(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex9)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex9);
					}
					catch (FormatException ex10)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex10);
					}
					catch (InvalidCastException ex11)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex11);
					}
					catch (OverflowException ex12)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex12);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x04000222 RID: 546
		internal readonly object _val;
	}
}
