using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000067 RID: 103
	internal abstract class ExpressionNode
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001DD24 File Offset: 0x0001BF24
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001DD34 File Offset: 0x0001BF34
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001DD64 File Offset: 0x0001BF64
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x060005F7 RID: 1527
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x060005F8 RID: 1528
		internal abstract object Eval();

		// Token: 0x060005F9 RID: 1529
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x060005FA RID: 1530
		internal abstract object Eval(int[] recordNos);

		// Token: 0x060005FB RID: 1531
		internal abstract bool IsConstant();

		// Token: 0x060005FC RID: 1532
		internal abstract bool IsTableConstant();

		// Token: 0x060005FD RID: 1533
		internal abstract bool HasLocalAggregate();

		// Token: 0x060005FE RID: 1534
		internal abstract bool HasRemoteAggregate();

		// Token: 0x060005FF RID: 1535
		internal abstract ExpressionNode Optimize();

		// Token: 0x06000600 RID: 1536 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001DD6D File Offset: 0x0001BF6D
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001DD95 File Offset: 0x0001BF95
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001DDD1 File Offset: 0x0001BFD1
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001DDED File Offset: 0x0001BFED
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001DE18 File Offset: 0x0001C018
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001DE2E File Offset: 0x0001C02E
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001DE49 File Offset: 0x0001C049
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001DE5B File Offset: 0x0001C05B
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001DE6D File Offset: 0x0001C06D
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001DE80 File Offset: 0x0001C080
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x0400022B RID: 555
		private DataTable _table;
	}
}
