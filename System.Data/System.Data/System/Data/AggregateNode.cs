using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x02000061 RID: 97
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001AA8B File Offset: 0x00018C8B
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName)
			: this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001AA98 File Offset: 0x00018C98
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, bool local, string relationName)
			: base(table)
		{
			this._aggregate = (Aggregate)aggregateType;
			if (aggregateType == FunctionId.Sum)
			{
				this._type = AggregateType.Sum;
			}
			else if (aggregateType == FunctionId.Avg)
			{
				this._type = AggregateType.Mean;
			}
			else if (aggregateType == FunctionId.Min)
			{
				this._type = AggregateType.Min;
			}
			else if (aggregateType == FunctionId.Max)
			{
				this._type = AggregateType.Max;
			}
			else if (aggregateType == FunctionId.Count)
			{
				this._type = AggregateType.Count;
			}
			else if (aggregateType == FunctionId.Var)
			{
				this._type = AggregateType.Var;
			}
			else
			{
				if (aggregateType != FunctionId.StDev)
				{
					throw ExprException.UndefinedFunction(Function.s_functionName[(int)aggregateType]);
				}
				this._type = AggregateType.StDev;
			}
			this._local = local;
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001AB3C File Offset: 0x00018D3C
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (this._local)
			{
				this._relation = null;
			}
			else
			{
				DataRelationCollection childRelations = table.ChildRelations;
				if (this._relationName == null)
				{
					if (childRelations.Count > 1)
					{
						throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
					}
					if (childRelations.Count != 1)
					{
						throw ExprException.AggregateUnbound(this.ToString());
					}
					this._relation = childRelations[0];
				}
				else
				{
					this._relation = childRelations[this._relationName];
				}
			}
			this._childTable = ((this._relation == null) ? table : this._relation.ChildTable);
			this._column = this._childTable.Columns[this._columnName];
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

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001AC60 File Offset: 0x00018E60
		internal static void Bind(DataRelation relation, List<DataColumn> list)
		{
			if (relation != null)
			{
				foreach (DataColumn dataColumn in relation.ChildColumnsReference)
				{
					if (!list.Contains(dataColumn))
					{
						list.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in relation.ParentColumnsReference)
				{
					if (!list.Contains(dataColumn2))
					{
						list.Add(dataColumn2);
					}
				}
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010824 File Offset: 0x0000EA24
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			DataRow[] array;
			if (this._local)
			{
				array = new DataRow[this._childTable.Rows.Count];
				this._childTable.Rows.CopyTo(array, 0);
			}
			else
			{
				if (row == null)
				{
					throw ExprException.EvalNoContext();
				}
				if (this._relation == null)
				{
					throw ExprException.AggregateUnbound(this.ToString());
				}
				array = row.GetChildRows(this._relation, version);
			}
			if (version == DataRowVersion.Proposed)
			{
				version = DataRowVersion.Default;
			}
			List<int> list = new List<int>();
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].RowState == DataRowState.Deleted)
				{
					if (DataRowAction.Rollback == array[i]._action)
					{
						version = DataRowVersion.Original;
						goto IL_00BF;
					}
				}
				else if (DataRowAction.Rollback != array[i]._action || array[i].RowState != DataRowState.Added)
				{
					goto IL_00BF;
				}
				IL_00E1:
				i++;
				continue;
				IL_00BF:
				if (version != DataRowVersion.Original || array[i]._oldRecord != -1)
				{
					list.Add(array[i].GetRecordFromVersion(version));
					goto IL_00E1;
				}
				goto IL_00E1;
			}
			int[] array2 = list.ToArray();
			return this._column.GetAggregateValue(array2, this._type);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001ADD5 File Offset: 0x00018FD5
		internal override object Eval(int[] records)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (!this._local)
			{
				throw ExprException.ComputeNotAggregate(this.ToString());
			}
			return this._column.GetAggregateValue(records, this._type);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001AE11 File Offset: 0x00019011
		internal override bool IsTableConstant()
		{
			return this._local;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001AE11 File Offset: 0x00019011
		internal override bool HasLocalAggregate()
		{
			return this._local;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AE19 File Offset: 0x00019019
		internal override bool HasRemoteAggregate()
		{
			return !this._local;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AE24 File Offset: 0x00019024
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000207F File Offset: 0x0000027F
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x040001F2 RID: 498
		private readonly AggregateType _type;

		// Token: 0x040001F3 RID: 499
		private readonly Aggregate _aggregate;

		// Token: 0x040001F4 RID: 500
		private readonly bool _local;

		// Token: 0x040001F5 RID: 501
		private readonly string _relationName;

		// Token: 0x040001F6 RID: 502
		private readonly string _columnName;

		// Token: 0x040001F7 RID: 503
		private DataTable _childTable;

		// Token: 0x040001F8 RID: 504
		private DataColumn _column;

		// Token: 0x040001F9 RID: 505
		private DataRelation _relation;
	}
}
