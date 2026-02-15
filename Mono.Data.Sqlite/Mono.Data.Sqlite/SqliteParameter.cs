using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000022 RID: 34
	public sealed class SqliteParameter : DbParameter, ICloneable
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000C884 File Offset: 0x0000AA84
		public SqliteParameter()
			: this(null, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C895 File Offset: 0x0000AA95
		public SqliteParameter(string parameterName, object value)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
			this.Value = value;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
		{
			this._parameterName = parameterName;
			this._dbType = (int)parameterType;
			this._sourceColumn = sourceColumn;
			this._rowVersion = rowVersion;
			this._objValue = null;
			this._dataSize = parameterSize;
			this._nullMapping = false;
			this._nullable = true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C900 File Offset: 0x0000AB00
		private SqliteParameter(SqliteParameter source)
			: this(source.ParameterName, (DbType)source._dbType, 0, source.Direction, source.IsNullable, 0, 0, source.SourceColumn, source.SourceVersion, source.Value)
		{
			this._nullMapping = source._nullMapping;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C94C File Offset: 0x0000AB4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Value = value;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000C97E File Offset: 0x0000AB7E
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000C986 File Offset: 0x0000AB86
		public override bool IsNullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000C990 File Offset: 0x0000AB90
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000C9DE File Offset: 0x0000ABDE
		[DbProviderSpecificTypeProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override DbType DbType
		{
			get
			{
				if (this._dbType != -1)
				{
					return (DbType)this._dbType;
				}
				if (this._objValue != null && this._objValue != DBNull.Value)
				{
					return SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
				return DbType.String;
			}
			set
			{
				this._dbType = (int)value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00003A43 File Offset: 0x00001C43
		public override ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (value != ParameterDirection.Input)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000C9E7 File Offset: 0x0000ABE7
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		public override string ParameterName
		{
			get
			{
				return this._parameterName;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		[DefaultValue(0)]
		public override int Size
		{
			set
			{
				this._dataSize = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000CA01 File Offset: 0x0000AC01
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000CA09 File Offset: 0x0000AC09
		public override string SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000CA12 File Offset: 0x0000AC12
		public override bool SourceColumnNullMapping
		{
			set
			{
				this._nullMapping = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000CA23 File Offset: 0x0000AC23
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this._rowVersion;
			}
			set
			{
				this._rowVersion = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000CA34 File Offset: 0x0000AC34
		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public override object Value
		{
			get
			{
				return this._objValue;
			}
			set
			{
				this._objValue = value;
				if (this._dbType == -1 && this._objValue != null && this._objValue != DBNull.Value)
				{
					this._dbType = (int)SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public object Clone()
		{
			return new SqliteParameter(this);
		}

		// Token: 0x040000A7 RID: 167
		internal int _dbType;

		// Token: 0x040000A8 RID: 168
		private DataRowVersion _rowVersion;

		// Token: 0x040000A9 RID: 169
		private object _objValue;

		// Token: 0x040000AA RID: 170
		private string _sourceColumn;

		// Token: 0x040000AB RID: 171
		private string _parameterName;

		// Token: 0x040000AC RID: 172
		private int _dataSize;

		// Token: 0x040000AD RID: 173
		private bool _nullable;

		// Token: 0x040000AE RID: 174
		private bool _nullMapping;
	}
}
