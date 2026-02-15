using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x02000073 RID: 115
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001FB46 File Offset: 0x0001DD46
		internal LookupNode(DataTable table, string columnName, string relationName)
			: base(table)
		{
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001FB60 File Offset: 0x0001DD60
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._column = null;
			this._relation = null;
			if (table == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			if (this._relationName == null)
			{
				if (parentRelations.Count > 1)
				{
					throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
				}
				this._relation = parentRelations[0];
			}
			else
			{
				this._relation = parentRelations[this._relationName];
			}
			if (this._relation == null)
			{
				throw ExprException.BindFailure(this._relationName);
			}
			DataTable parentTable = this._relation.ParentTable;
			this._column = parentTable.Columns[this._columnName];
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._columnName);
			}
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
			AggregateNode.Bind(this._relation, list);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001FC6A File Offset: 0x0001DE6A
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001FC74 File Offset: 0x0001DE74
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._column == null || this._relation == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRow parentRow = row.GetParentRow(this._relation, version);
			if (parentRow == null)
			{
				return DBNull.Value;
			}
			return parentRow[this._column, parentRow.HasVersion(version) ? version : DataRowVersion.Current];
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00010A86 File Offset: 0x0000EC86
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001FCD1 File Offset: 0x0001DED1
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000207F File Offset: 0x0000027F
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400026E RID: 622
		private readonly string _relationName;

		// Token: 0x0400026F RID: 623
		private readonly string _columnName;

		// Token: 0x04000270 RID: 624
		private DataColumn _column;

		// Token: 0x04000271 RID: 625
		private DataRelation _relation;
	}
}
