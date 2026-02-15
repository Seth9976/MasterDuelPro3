using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000074 RID: 116
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0001FCDF File Offset: 0x0001DEDF
		internal NameNode(DataTable table, char[] text, int start, int pos)
			: base(table)
		{
			this._name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		internal NameNode(DataTable table, string name)
			: base(table)
		{
			this._name = name;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001FD07 File Offset: 0x0001DF07
		internal override bool IsSqlColumn
		{
			get
			{
				return this._column.IsSqlType;
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001FD14 File Offset: 0x0001DF14
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			try
			{
				this._column = table.Columns[this._name];
			}
			catch (Exception ex)
			{
				this._found = false;
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ExprException.UnboundName(this._name);
			}
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			this._name = this._column.ColumnName;
			this._found = true;
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataColumn dataColumn = list[i];
				if (this._column == dataColumn)
				{
					break;
				}
			}
			if (i >= list.Count)
			{
				list.Add(this._column);
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001FC6A File Offset: 0x0001DE6A
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (!this._found)
			{
				throw ExprException.UnboundName(this._name);
			}
			if (row != null)
			{
				return this._column[row.GetRecordFromVersion(version)];
			}
			if (this.IsTableConstant())
			{
				return this._column.DataExpression.Evaluate();
			}
			throw ExprException.UnboundName(this._name);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00010A86 File Offset: 0x0000EC86
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001FE3B File Offset: 0x0001E03B
		internal override bool IsTableConstant()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.IsTableAggregate();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001FE64 File Offset: 0x0001E064
		internal override bool HasLocalAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001FE8D File Offset: 0x0001E08D
		internal override bool HasRemoteAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001FEB6 File Offset: 0x0001E0B6
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000207F File Offset: 0x0000027F
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		internal static string ParseName(char[] text, int start, int pos)
		{
			char c = '\0';
			string text2 = string.Empty;
			int num = start;
			int num2 = pos;
			checked
			{
				if (text[start] == '`')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "`";
				}
				else if (text[start] == '[')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "]\\";
				}
			}
			if (c != '\0')
			{
				int num3 = start;
				for (int i = start; i < pos; i++)
				{
					if (text[i] == c && i + 1 < pos && text2.IndexOf(text[i + 1]) >= 0)
					{
						i++;
					}
					text[num3] = text[i];
					num3++;
				}
				pos = num3;
			}
			if (pos == start)
			{
				throw ExprException.InvalidName(new string(text, num, num2 - num));
			}
			return new string(text, start, pos - start);
		}

		// Token: 0x04000272 RID: 626
		internal string _name;

		// Token: 0x04000273 RID: 627
		internal bool _found;

		// Token: 0x04000274 RID: 628
		internal DataColumn _column;
	}
}
