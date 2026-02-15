using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x02000066 RID: 102
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		internal DataExpression(DataTable table, string expression)
			: this(table, expression, null)
		{
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this._originalExpression = expression;
			this._expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
				if (this._storageType == StorageType.BigInteger)
				{
					throw ExprException.UnsupportedDataType(type);
				}
				this._dataType = type;
				this._expr = expressionParser.Parse();
				this._parsed = true;
				if (this._expr != null && table != null)
				{
					this.Bind(table);
					return;
				}
				this._bound = false;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001DA6A File Offset: 0x0001BC6A
		internal string Expression
		{
			get
			{
				if (this._originalExpression == null)
				{
					return "";
				}
				return this._originalExpression;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001DA80 File Offset: 0x0001BC80
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001DA88 File Offset: 0x0001BC88
		internal bool HasValue
		{
			get
			{
				return this._expr != null;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001DA94 File Offset: 0x0001BC94
		internal void Bind(DataTable table)
		{
			this._table = table;
			if (table == null)
			{
				return;
			}
			if (this._expr != null)
			{
				List<DataColumn> list = new List<DataColumn>();
				this._expr.Bind(table, list);
				this._expr = this._expr.Optimize();
				this._table = table;
				this._bound = true;
				this._dependency = list.ToArray();
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001DAF2 File Offset: 0x0001BCF2
		internal bool DependsOn(DataColumn column)
		{
			return this._expr != null && this._expr.DependsOn(column);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001DB0A File Offset: 0x0001BD0A
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001DB18 File Offset: 0x0001BD18
		internal object Evaluate(DataRow row, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			object obj;
			if (this._expr != null)
			{
				obj = this._expr.Eval(row, version);
				if (obj == DBNull.Value && StorageType.Uri >= this._storageType)
				{
					return obj;
				}
				try
				{
					if (StorageType.Object != this._storageType)
					{
						obj = SqlConvert.ChangeType2(obj, this._storageType, this._dataType, this._table.FormatProvider);
					}
					return obj;
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(obj, this._dataType, ex);
				}
			}
			obj = null;
			return obj;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			if (this._expr == null)
			{
				return true;
			}
			if (row == null)
			{
				throw ExprException.InvokeArgument();
			}
			object obj = this._expr.Eval(row, version);
			bool flag;
			try
			{
				flag = DataExpression.ToBoolean(obj);
			}
			catch (EvaluateException)
			{
				throw ExprException.FilterConvertion(this.Expression);
			}
			return flag;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001DC24 File Offset: 0x0001BE24
		internal DataColumn[] GetDependency()
		{
			return this._dependency;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		internal bool IsTableAggregate()
		{
			return this._expr != null && this._expr.IsTableConstant();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001DC43 File Offset: 0x0001BE43
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001DC4B File Offset: 0x0001BE4B
		internal bool HasLocalAggregate()
		{
			return this._expr != null && this._expr.HasLocalAggregate();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001DC62 File Offset: 0x0001BE62
		internal bool HasRemoteAggregate()
		{
			return this._expr != null && this._expr.HasRemoteAggregate();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		internal static bool ToBoolean(object value)
		{
			if (DataExpression.IsUnknown(value))
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is SqlBoolean)
			{
				return ((SqlBoolean)value).IsTrue;
			}
			if (value is string)
			{
				try
				{
					return bool.Parse((string)value);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(value, typeof(bool), ex);
				}
			}
			throw ExprException.DatavalueConvertion(value, typeof(bool), null);
		}

		// Token: 0x04000223 RID: 547
		internal string _originalExpression;

		// Token: 0x04000224 RID: 548
		private bool _parsed;

		// Token: 0x04000225 RID: 549
		private bool _bound;

		// Token: 0x04000226 RID: 550
		private ExpressionNode _expr;

		// Token: 0x04000227 RID: 551
		private DataTable _table;

		// Token: 0x04000228 RID: 552
		private readonly StorageType _storageType;

		// Token: 0x04000229 RID: 553
		private readonly Type _dataType;

		// Token: 0x0400022A RID: 554
		private DataColumn[] _dependency = Array.Empty<DataColumn>();
	}
}
