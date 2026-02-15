using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x02000064 RID: 100
	internal sealed class LikeNode : BinaryNode
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0001D29F File Offset: 0x0001B49F
		internal LikeNode(DataTable table, int op, ExpressionNode left, ExpressionNode right)
			: base(table, op, left, right)
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			object obj = this._left.Eval(row, version);
			if (obj == DBNull.Value || (this._left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
			{
				return DBNull.Value;
			}
			string text2;
			if (this._pattern == null)
			{
				object obj2 = this._right.Eval(row, version);
				if (!(obj2 is string) && !(obj2 is SqlString))
				{
					base.SetTypeMismatchError(this._op, obj.GetType(), obj2.GetType());
				}
				if (obj2 == DBNull.Value || DataStorage.IsObjectSqlNull(obj2))
				{
					return DBNull.Value;
				}
				string text = (string)SqlConvert.ChangeType2(obj2, StorageType.String, typeof(string), base.FormatProvider);
				text2 = this.AnalyzePattern(text);
				if (this._right.IsConstant())
				{
					this._pattern = text2;
				}
			}
			else
			{
				text2 = this._pattern;
			}
			if (!(obj is string) && !(obj is SqlString))
			{
				base.SetTypeMismatchError(this._op, obj.GetType(), typeof(string));
			}
			char[] array = new char[] { ' ', '\u3000' };
			string text3;
			if (obj is SqlString)
			{
				text3 = ((SqlString)obj).Value;
			}
			else
			{
				text3 = (string)obj;
			}
			string text4 = text3.TrimEnd(array);
			switch (this._kind)
			{
			case 1:
				return base.table.IndexOf(text4, text2) == 0;
			case 2:
			{
				string text5 = text2.TrimEnd(array);
				return base.table.IsSuffix(text4, text5);
			}
			case 3:
				return 0 <= base.table.IndexOf(text4, text2);
			case 4:
				return base.table.Compare(text4, text2) == 0;
			case 5:
				return true;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001D48C File Offset: 0x0001B68C
		internal string AnalyzePattern(string pat)
		{
			int length = pat.Length;
			char[] array = new char[length + 1];
			pat.CopyTo(0, array, 0, length);
			array[length] = '\0';
			char[] array2 = new char[length + 1];
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < length)
			{
				if (array[i] != '*')
				{
					if (array[i] != '%')
					{
						if (array[i] != '[')
						{
							array2[num++] = array[i];
							i++;
							continue;
						}
						i++;
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						array2[num++] = array[i++];
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						if (array[i] != ']')
						{
							throw ExprException.InvalidPattern(pat);
						}
						i++;
						continue;
					}
				}
				while ((array[i] == '*' || array[i] == '%') && i < length)
				{
					i++;
				}
				if ((i < length && num > 0) || num2 >= 2)
				{
					throw ExprException.InvalidPattern(pat);
				}
				num2++;
			}
			string text = new string(array2, 0, num);
			if (num2 == 0)
			{
				this._kind = 4;
				return text;
			}
			if (num <= 0)
			{
				this._kind = 5;
				return text;
			}
			if (array[0] != '*' && array[0] != '%')
			{
				this._kind = 1;
				return text;
			}
			if (array[length - 1] == '*' || array[length - 1] == '%')
			{
				this._kind = 3;
				return text;
			}
			this._kind = 2;
			return text;
		}

		// Token: 0x04000220 RID: 544
		private int _kind;

		// Token: 0x04000221 RID: 545
		private string _pattern;
	}
}
