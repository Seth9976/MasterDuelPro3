using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x02000076 RID: 118
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x000201B3 File Offset: 0x0001E3B3
		internal UnaryNode(DataTable table, int op, ExpressionNode right)
			: base(table)
		{
			this._op = op;
			this._right = right;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000201CA File Offset: 0x0001E3CA
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._right.Bind(table, list);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00010824 File Offset: 0x0000EA24
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000201E0 File Offset: 0x0001E3E0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this._op, this._right.Eval(row, version));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000201FB File Offset: 0x0001E3FB
		internal override object Eval(int[] recordNos)
		{
			return this._right.Eval(recordNos);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002020C File Offset: 0x0001E40C
		private object EvalUnaryOp(int op, object vl)
		{
			object value = DBNull.Value;
			if (DataExpression.IsUnknown(vl))
			{
				return DBNull.Value;
			}
			switch (op)
			{
			case 0:
				return vl;
			case 1:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					switch (storageType)
					{
					case StorageType.Byte:
						return (int)(-(int)((byte)vl));
					case StorageType.Int16:
						return (int)(-(int)((short)vl));
					case StorageType.UInt16:
					case StorageType.UInt32:
					case StorageType.UInt64:
						break;
					case StorageType.Int32:
						return -(int)vl;
					case StorageType.Int64:
						return -(long)vl;
					case StorageType.Single:
						return -(float)vl;
					case StorageType.Double:
						return -(double)vl;
					case StorageType.Decimal:
						return -(decimal)vl;
					default:
						switch (storageType)
						{
						case StorageType.SqlDecimal:
							return -(SqlDecimal)vl;
						case StorageType.SqlDouble:
							return -(SqlDouble)vl;
						case StorageType.SqlInt16:
							return -(SqlInt16)vl;
						case StorageType.SqlInt32:
							return -(SqlInt32)vl;
						case StorageType.SqlInt64:
							return -(SqlInt64)vl;
						case StorageType.SqlMoney:
							return -(SqlMoney)vl;
						case StorageType.SqlSingle:
							return -(SqlSingle)vl;
						}
						break;
					}
					return DBNull.Value;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 2:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					return vl;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 3:
				if (vl is SqlBoolean)
				{
					if (((SqlBoolean)vl).IsFalse)
					{
						return SqlBoolean.True;
					}
					if (((SqlBoolean)vl).IsTrue)
					{
						return SqlBoolean.False;
					}
					throw ExprException.UnsupportedOperator(op);
				}
				else
				{
					if (DataExpression.ToBoolean(vl))
					{
						return false;
					}
					return true;
				}
				break;
			default:
				throw ExprException.UnsupportedOperator(op);
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002045D File Offset: 0x0001E65D
		internal override bool IsConstant()
		{
			return this._right.IsConstant();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002046A File Offset: 0x0001E66A
		internal override bool IsTableConstant()
		{
			return this._right.IsTableConstant();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00020477 File Offset: 0x0001E677
		internal override bool HasLocalAggregate()
		{
			return this._right.HasLocalAggregate();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00020484 File Offset: 0x0001E684
		internal override bool HasRemoteAggregate()
		{
			return this._right.HasRemoteAggregate();
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00020491 File Offset: 0x0001E691
		internal override bool DependsOn(DataColumn column)
		{
			return this._right.DependsOn(column);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000204A0 File Offset: 0x0001E6A0
		internal override ExpressionNode Optimize()
		{
			this._right = this._right.Optimize();
			if (this.IsConstant())
			{
				object obj = this.Eval();
				return new ConstNode(base.table, ValueType.Object, obj, false);
			}
			return this;
		}

		// Token: 0x04000277 RID: 631
		internal readonly int _op;

		// Token: 0x04000278 RID: 632
		internal ExpressionNode _right;
	}
}
