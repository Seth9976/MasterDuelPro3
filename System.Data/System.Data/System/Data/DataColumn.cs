using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents the schema of a column in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000A RID: 10
	[DesignTimeVisible(false)]
	[DefaultProperty("ColumnName")]
	[ToolboxItem(false)]
	public class DataColumn : MarshalByValueComponent
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataColumn" /> class as type string.</summary>
		// Token: 0x06000011 RID: 17 RVA: 0x0000217B File Offset: 0x0000037B
		public DataColumn()
			: this(null, typeof(string), null, MappingType.Element)
		{
		}

		/// <summary>Inititalizes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified column name and data type.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000012 RID: 18 RVA: 0x00002190 File Offset: 0x00000390
		public DataColumn(string columnName, Type dataType)
			: this(columnName, dataType, null, MappingType.Element)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, expression, and value that determines whether the column is an attribute.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <param name="type">One of the <see cref="T:System.Data.MappingType" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000013 RID: 19 RVA: 0x0000219C File Offset: 0x0000039C
		public DataColumn(string columnName, Type dataType, string expr, MappingType type)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, string, string, MappingType>("<ds.DataColumn.DataColumn|API> {0}, columnName='{1}', expr='{2}', type={3}", this.ObjectID, columnName, expr, type);
			if (dataType == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataType");
			}
			StorageType storageType = DataStorage.GetStorageType(dataType);
			if (DataStorage.ImplementsINullableValue(storageType, dataType))
			{
				throw ExceptionBuilder.ColumnTypeNotSupported();
			}
			this._columnName = columnName ?? string.Empty;
			SimpleType simpleType = SimpleType.CreateSimpleType(storageType, dataType);
			if (simpleType != null)
			{
				this.SimpleType = simpleType;
			}
			this.UpdateColumnType(dataType, storageType);
			if (!string.IsNullOrEmpty(expr))
			{
				this.Expression = expr;
			}
			this._columnMapping = type;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002294 File Offset: 0x00000494
		private void UpdateColumnType(Type type, StorageType typeCode)
		{
			TypeLimiter.EnsureTypeIsAllowed(type, null);
			this._dataType = type;
			this._storageType = typeCode;
			if (StorageType.DateTime != typeCode)
			{
				this._dateTimeMode = DataSetDateTime.UnspecifiedLocal;
			}
			DataStorage.ImplementsInterfaces(typeCode, type, out this._isSqlType, out this._implementsINullable, out this._implementsIXMLSerializable, out this._implementsIChangeTracking, out this._implementsIRevertibleChangeTracking);
			if (!this._isSqlType && this._implementsINullable)
			{
				SqlUdtStorage.GetStaticNullForUdtType(type);
			}
		}

		/// <summary>Gets or sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</summary>
		/// <returns>true if null values values are allowed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022FE File Offset: 0x000004FE
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002308 File Offset: 0x00000508
		[DefaultValue(true)]
		public bool AllowDBNull
		{
			get
			{
				return this._allowNull;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataColumn.set_AllowDBNull|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._allowNull != value)
					{
						if (this._table != null && !value && this._table.EnforceConstraints)
						{
							this.CheckNotAllowNull();
						}
						this._allowNull = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column automatically increments the value of the column for new rows added to the table.</summary>
		/// <returns>true if the value of the column increments automatically; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002378 File Offset: 0x00000578
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002390 File Offset: 0x00000590
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoIncrement
		{
			get
			{
				return this._autoInc != null && this._autoInc.Auto;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataColumn.set_AutoIncrement|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrement != value)
				{
					if (value)
					{
						if (this._expression != null)
						{
							throw ExceptionBuilder.AutoIncrementAndExpression();
						}
						if (!this.DefaultValueIsNull)
						{
							throw ExceptionBuilder.AutoIncrementAndDefaultValue();
						}
						if (!DataColumn.IsAutoIncrementType(this.DataType))
						{
							if (this.HasData)
							{
								throw ExceptionBuilder.AutoIncrementCannotSetIfHasData(this.DataType.Name);
							}
							this.DataType = typeof(int);
						}
					}
					this.AutoInc.Auto = value;
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000241D File Offset: 0x0000061D
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000243E File Offset: 0x0000063E
		internal object AutoIncrementCurrent
		{
			get
			{
				if (this._autoInc == null)
				{
					return this.AutoIncrementSeed;
				}
				return this._autoInc.Current;
			}
			set
			{
				if (this.AutoIncrementSeed != BigIntegerStorage.ConvertToBigInteger(value, this.FormatProvider))
				{
					this.AutoInc.SetCurrent(value, this.FormatProvider);
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002470 File Offset: 0x00000670
		internal AutoIncrementValue AutoInc
		{
			get
			{
				AutoIncrementValue autoIncrementValue;
				if ((autoIncrementValue = this._autoInc) == null)
				{
					autoIncrementValue = (this._autoInc = ((this.DataType == typeof(BigInteger)) ? new AutoIncrementBigInteger() : new AutoIncrementInt64()));
				}
				return autoIncrementValue;
			}
		}

		/// <summary>Gets or sets the starting value for a column that has its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true. The default is 0.</summary>
		/// <returns>The starting value for the <see cref="P:System.Data.DataColumn.AutoIncrement" /> feature.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024B3 File Offset: 0x000006B3
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000024CB File Offset: 0x000006CB
		[DefaultValue(0L)]
		public long AutoIncrementSeed
		{
			get
			{
				if (this._autoInc == null)
				{
					return 0L;
				}
				return this._autoInc.Seed;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, long>("<ds.DataColumn.set_AutoIncrementSeed|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrementSeed != value)
				{
					this.AutoInc.Seed = value;
				}
			}
		}

		/// <summary>Gets or sets the increment used by a column with its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true.</summary>
		/// <returns>The number by which the value of the column is automatically incremented. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002510 File Offset: 0x00000710
		[DefaultValue(1L)]
		public long AutoIncrementStep
		{
			get
			{
				if (this._autoInc == null)
				{
					return 1L;
				}
				return this._autoInc.Step;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, long>("<ds.DataColumn.set_AutoIncrementStep|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrementStep != value)
				{
					this.AutoInc.Step = value;
				}
			}
		}

		/// <summary>Gets or sets the caption for the column.</summary>
		/// <returns>The caption of the column. If not set, returns the <see cref="P:System.Data.DataColumn.ColumnName" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000253D File Offset: 0x0000073D
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002554 File Offset: 0x00000754
		public string Caption
		{
			get
			{
				if (this._caption == null)
				{
					return this._columnName;
				}
				return this._caption;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._caption == null || string.Compare(this._caption, value, true, this.Locale) != 0)
				{
					this._caption = value;
				}
			}
		}

		/// <summary>Gets or sets the name of the column in the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The name of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to null or an empty string and the column belongs to a collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A column with the same name already exists in the collection. The name comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002584 File Offset: 0x00000784
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000258C File Offset: 0x0000078C
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataColumn.set_ColumnName|API> {0}, '{1}'", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = string.Empty;
					}
					if (string.Compare(this._columnName, value, true, this.Locale) != 0)
					{
						if (this._table != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.ColumnNameRequired();
							}
							this._table.Columns.RegisterColumnName(value, this);
							if (this._columnName.Length != 0)
							{
								this._table.Columns.UnregisterName(this._columnName);
							}
						}
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this._encodedColumnName = null;
						if (this._table != null)
						{
							this._table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
					else if (this._columnName != value)
					{
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this._encodedColumnName = null;
						if (this._table != null)
						{
							this._table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026BC File Offset: 0x000008BC
		internal string EncodedColumnName
		{
			get
			{
				if (this._encodedColumnName == null)
				{
					this._encodedColumnName = XmlConvert.EncodeLocalName(this.ColumnName);
				}
				return this._encodedColumnName;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026E0 File Offset: 0x000008E0
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002708 File Offset: 0x00000908
		internal CultureInfo Locale
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.Locale;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002723 File Offset: 0x00000923
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataTable" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000272B File Offset: 0x0000092B
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002734 File Offset: 0x00000934
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._columnPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataColumn.set_Prefix|API> {0}, '{1}'", this.ObjectID, value);
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				this._columnPrefix = value;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000278C File Offset: 0x0000098C
		internal string GetColumnValueAsString(DataRow row, DataRowVersion version)
		{
			object obj = this[row.GetRecordFromVersion(version)];
			if (DataStorage.IsObjectNull(obj))
			{
				return null;
			}
			return this.ConvertObjectToXml(obj);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027B8 File Offset: 0x000009B8
		internal bool Computed
		{
			get
			{
				return this._expression != null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027C3 File Offset: 0x000009C3
		internal DataExpression DataExpression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets or sets the type of data stored in the column.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the column data type.</returns>
		/// <exception cref="T:System.ArgumentException">The column already has data stored. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000027CB File Offset: 0x000009CB
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000027D4 File Offset: 0x000009D4
		[DefaultValue(typeof(string))]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ColumnTypeConverter))]
		public Type DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				if (this._dataType != value)
				{
					if (this.HasData)
					{
						throw ExceptionBuilder.CantChangeDataType();
					}
					if (value == null)
					{
						throw ExceptionBuilder.NullDataType();
					}
					StorageType storageType = DataStorage.GetStorageType(value);
					if (DataStorage.ImplementsINullableValue(storageType, value))
					{
						throw ExceptionBuilder.ColumnTypeNotSupported();
					}
					if (this._table != null && this.IsInRelation())
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
					if (storageType == StorageType.BigInteger && this._expression != null)
					{
						throw ExprException.UnsupportedDataType(value);
					}
					if (!this.DefaultValueIsNull)
					{
						try
						{
							if (this._defaultValue is BigInteger)
							{
								this._defaultValue = BigIntegerStorage.ConvertFromBigInteger((BigInteger)this._defaultValue, value, this.FormatProvider);
							}
							else if (typeof(BigInteger) == value)
							{
								this._defaultValue = BigIntegerStorage.ConvertToBigInteger(this._defaultValue, this.FormatProvider);
							}
							else if (typeof(string) == value)
							{
								this._defaultValue = this.DefaultValue.ToString();
							}
							else if (typeof(SqlString) == value)
							{
								this._defaultValue = SqlConvert.ConvertToSqlString(this.DefaultValue);
							}
							else if (typeof(object) != value)
							{
								this.DefaultValue = SqlConvert.ChangeTypeForDefaultValue(this.DefaultValue, value, this.FormatProvider);
							}
						}
						catch (InvalidCastException ex)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, ex);
						}
						catch (FormatException ex2)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, ex2);
						}
					}
					if (this.ColumnMapping == MappingType.SimpleContent && value == typeof(char))
					{
						throw ExceptionBuilder.CannotSetSimpleContentType(this.ColumnName, value);
					}
					this.SimpleType = SimpleType.CreateSimpleType(storageType, value);
					if (StorageType.String == storageType)
					{
						this._maxLength = -1;
					}
					this.UpdateColumnType(value, storageType);
					this.XmlDataType = null;
					if (this.AutoIncrement)
					{
						if (!DataColumn.IsAutoIncrementType(value))
						{
							this.AutoIncrement = false;
						}
						if (this._autoInc != null)
						{
							AutoIncrementValue autoInc = this._autoInc;
							this._autoInc = null;
							this.AutoInc.Auto = autoInc.Auto;
							this.AutoInc.Seed = autoInc.Seed;
							this.AutoInc.Step = autoInc.Step;
							if (this._autoInc.DataType == autoInc.DataType)
							{
								this._autoInc.Current = autoInc.Current;
								return;
							}
							if (autoInc.DataType == typeof(long))
							{
								this.AutoInc.Current = (long)autoInc.Current;
								return;
							}
							this.AutoInc.Current = (long)((BigInteger)autoInc.Current);
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the DateTimeMode for the column.</summary>
		/// <returns>The <see cref="T:System.Data.DataSetDateTime" /> for the specified column.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002AC4 File Offset: 0x00000CC4
		[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
		[RefreshProperties(RefreshProperties.All)]
		public DataSetDateTime DateTimeMode
		{
			get
			{
				return this._dateTimeMode;
			}
			set
			{
				if (this._dateTimeMode != value)
				{
					if (this.DataType != typeof(DateTime) && value != DataSetDateTime.UnspecifiedLocal)
					{
						throw ExceptionBuilder.CannotSetDateTimeModeForNonDateTimeColumns();
					}
					switch (value)
					{
					case DataSetDateTime.Local:
					case DataSetDateTime.Utc:
						if (this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					case DataSetDateTime.Unspecified:
					case DataSetDateTime.UnspecifiedLocal:
						if (this._dateTimeMode != DataSetDateTime.Unspecified && this._dateTimeMode != DataSetDateTime.UnspecifiedLocal && this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					default:
						throw ExceptionBuilder.InvalidDateTimeMode(value);
					}
					this._dateTimeMode = value;
				}
			}
		}

		/// <summary>Gets or sets the default value for the column when you are creating new rows.</summary>
		/// <returns>A value appropriate to the column's <see cref="P:System.Data.DataColumn.DataType" />.</returns>
		/// <exception cref="T:System.InvalidCastException">When you are adding a row, the default value is not an instance of the column's data type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002B64 File Offset: 0x00000D64
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002C00 File Offset: 0x00000E00
		[TypeConverter(typeof(DefaultValueTypeConverter))]
		public object DefaultValue
		{
			get
			{
				if (this._defaultValue == DBNull.Value && this._implementsINullable)
				{
					if (this._storage != null)
					{
						this._defaultValue = this._storage._nullValue;
					}
					else if (this._isSqlType)
					{
						this._defaultValue = SqlConvert.ChangeTypeForDefaultValue(this._defaultValue, this._dataType, this.FormatProvider);
					}
					else if (this._implementsINullable)
					{
						PropertyInfo property = this._dataType.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
						if (property != null)
						{
							this._defaultValue = property.GetValue(null, null);
						}
					}
				}
				return this._defaultValue;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataColumn.set_DefaultValue|API> {0}", this.ObjectID);
				if (this._defaultValue == null || !this.DefaultValue.Equals(value))
				{
					if (this.AutoIncrement)
					{
						throw ExceptionBuilder.DefaultValueAndAutoIncrement();
					}
					object obj = ((value == null) ? DBNull.Value : value);
					if (obj != DBNull.Value && this.DataType != typeof(object))
					{
						try
						{
							obj = SqlConvert.ChangeTypeForDefaultValue(obj, this.DataType, this.FormatProvider);
						}
						catch (InvalidCastException ex)
						{
							throw ExceptionBuilder.DefaultValueColumnDataType(this.ColumnName, obj.GetType(), this.DataType, ex);
						}
					}
					this._defaultValue = obj;
					this._defaultValueIsNull = obj == DBNull.Value || (this.ImplementsINullable && DataStorage.IsObjectSqlNull(obj));
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal bool DefaultValueIsNull
		{
			get
			{
				return this._defaultValueIsNull;
			}
		}

		/// <summary>Gets or sets the expression used to filter rows, calculate the values in a column, or create an aggregate column.</summary>
		/// <returns>An expression to calculate the value of a column, or create an aggregate column. The return type of an expression is determined by the <see cref="P:System.Data.DataColumn.DataType" /> of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Data.DataColumn.AutoIncrement" /> or <see cref="P:System.Data.DataColumn.Unique" /> property is set to true. </exception>
		/// <exception cref="T:System.FormatException">When you are using the CONVERT function, the expression evaluates to a string, but the string does not contain a representation that can be converted to the type parameter. </exception>
		/// <exception cref="T:System.InvalidCastException">When you are using the CONVERT function, the requested cast is not possible. See the Conversion function in the following section for detailed information about possible casts. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When you use the SUBSTRING function, the start argument is out of range.-Or- When you use the SUBSTRING function, the length argument is out of range. </exception>
		/// <exception cref="T:System.Exception">When you use the LEN function or the TRIM function, the expression does not evaluate to a string. This includes expressions that evaluate to <see cref="T:System.Char" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002D00 File Offset: 0x00000F00
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string Expression
		{
			get
			{
				if (this._expression != null)
				{
					return this._expression.Expression;
				}
				return "";
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataColumn.set_Expression|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				try
				{
					DataExpression dataExpression = null;
					if (value.Length > 0)
					{
						DataExpression dataExpression2 = new DataExpression(this._table, value, this._dataType);
						if (dataExpression2.HasValue)
						{
							dataExpression = dataExpression2;
						}
					}
					if (this._expression == null && dataExpression != null)
					{
						if (this.AutoIncrement || this.Unique)
						{
							throw ExceptionBuilder.ExpressionAndUnique();
						}
						if (this._table != null)
						{
							for (int i = 0; i < this._table.Constraints.Count; i++)
							{
								if (this._table.Constraints[i].ContainsColumn(this))
								{
									throw ExceptionBuilder.ExpressionAndConstraint(this, this._table.Constraints[i]);
								}
							}
						}
						bool readOnly = this.ReadOnly;
						try
						{
							this.ReadOnly = true;
						}
						catch (ReadOnlyException ex)
						{
							ExceptionBuilder.TraceExceptionForCapture(ex);
							this.ReadOnly = readOnly;
							throw ExceptionBuilder.ExpressionAndReadOnly();
						}
					}
					if (this._table != null)
					{
						if (dataExpression != null && dataExpression.DependsOn(this))
						{
							throw ExceptionBuilder.ExpressionCircular();
						}
						this.HandleDependentColumnList(this._expression, dataExpression);
						DataExpression expression = this._expression;
						this._expression = dataExpression;
						try
						{
							if (dataExpression == null)
							{
								for (int j = 0; j < this._table.RecordCapacity; j++)
								{
									this.InitializeRecord(j);
								}
							}
							else
							{
								this._table.EvaluateExpressions(this);
							}
							this._table.ResetInternalIndexes(this);
							this._table.EvaluateDependentExpressions(this);
							return;
						}
						catch (Exception ex2) when (ADP.IsCatchableExceptionType(ex2))
						{
							ExceptionBuilder.TraceExceptionForCapture(ex2);
							try
							{
								this._expression = expression;
								this.HandleDependentColumnList(dataExpression, this._expression);
								if (expression == null)
								{
									for (int k = 0; k < this._table.RecordCapacity; k++)
									{
										this.InitializeRecord(k);
									}
								}
								else
								{
									this._table.EvaluateExpressions(this);
								}
								this._table.ResetInternalIndexes(this);
								this._table.EvaluateDependentExpressions(this);
							}
							catch (Exception ex3) when (ADP.IsCatchableExceptionType(ex3))
							{
								ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
							}
							throw;
						}
					}
					this._expression = dataExpression;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets the collection of custom user information associated with a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002FAC File Offset: 0x000011AC
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002FD1 File Offset: 0x000011D1
		internal bool HasData
		{
			get
			{
				return this._storage != null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002FDC File Offset: 0x000011DC
		internal bool ImplementsINullable
		{
			get
			{
				return this._implementsINullable;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002FE4 File Offset: 0x000011E4
		internal bool ImplementsIChangeTracking
		{
			get
			{
				return this._implementsIChangeTracking;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002FEC File Offset: 0x000011EC
		internal bool ImplementsIRevertibleChangeTracking
		{
			get
			{
				return this._implementsIRevertibleChangeTracking;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002FF4 File Offset: 0x000011F4
		internal bool IsValueType
		{
			get
			{
				return this._storage._isValueType;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003001 File Offset: 0x00001201
		internal bool IsSqlType
		{
			get
			{
				return this._isSqlType;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000300C File Offset: 0x0000120C
		private void SetMaxLengthSimpleType()
		{
			if (this._simpleType != null)
			{
				this._simpleType.MaxLength = this._maxLength;
				if (this._simpleType.IsPlainString())
				{
					this._simpleType = null;
					return;
				}
				if (this._simpleType.Name != null && this.XmlDataType != null)
				{
					this._simpleType.ConvertToAnnonymousSimpleType();
					this.XmlDataType = null;
					return;
				}
			}
			else if (-1 < this._maxLength)
			{
				this.SimpleType = SimpleType.CreateLimitedStringType(this._maxLength);
			}
		}

		/// <summary>Gets or sets the maximum length of a text column.</summary>
		/// <returns>The maximum length of the column in characters. If the column has no maximum length, the value is –1 (default).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003089 File Offset: 0x00001289
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003094 File Offset: 0x00001294
		[DefaultValue(-1)]
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataColumn.set_MaxLength|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._maxLength != value)
					{
						if (this.ColumnMapping == MappingType.SimpleContent)
						{
							throw ExceptionBuilder.CannotSetMaxLength2(this);
						}
						if (this.DataType != typeof(string) && this.DataType != typeof(SqlString))
						{
							throw ExceptionBuilder.HasToBeStringType(this);
						}
						int maxLength = this._maxLength;
						this._maxLength = Math.Max(value, -1);
						if ((maxLength < 0 || value < maxLength) && this._table != null && this._table.EnforceConstraints && !this.CheckMaxLength())
						{
							this._maxLength = maxLength;
							throw ExceptionBuilder.CannotSetMaxLength(this, value);
						}
						this.SetMaxLengthSimpleType();
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003178 File Offset: 0x00001378
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000031AC File Offset: 0x000013AC
		public string Namespace
		{
			get
			{
				if (this._columnUri != null)
				{
					return this._columnUri;
				}
				if (this.Table != null && this._columnMapping != MappingType.Attribute)
				{
					return this.Table.Namespace;
				}
				return string.Empty;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataColumn.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (this._columnUri != value)
				{
					if (this._columnMapping != MappingType.SimpleContent)
					{
						this.RaisePropertyChanging("Namespace");
						this._columnUri = value;
						return;
					}
					if (value != this.Namespace)
					{
						throw ExceptionBuilder.CannotChangeNamespace(this.ColumnName);
					}
				}
			}
		}

		/// <summary>Gets the (zero-based) position of the column in the <see cref="T:System.Data.DataColumnCollection" /> collection.</summary>
		/// <returns>The position of the column. Gets -1 if the column is not a member of a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003213 File Offset: 0x00001413
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000321C File Offset: 0x0000141C
		internal void SetOrdinalInternal(int ordinal)
		{
			if (this._ordinal != ordinal)
			{
				if (this.Unique && this._ordinal != -1 && ordinal == -1)
				{
					UniqueConstraint uniqueConstraint = this._table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						this._table.Constraints.Remove(uniqueConstraint);
					}
				}
				if (this._sortIndex != null && -1 == ordinal)
				{
					this._sortIndex.RemoveRef();
					this._sortIndex.RemoveRef();
					this._sortIndex = null;
				}
				int ordinal2 = this._ordinal;
				this._ordinal = ordinal;
				if (ordinal2 == -1 && this._ordinal != -1 && this.Unique)
				{
					UniqueConstraint uniqueConstraint2 = new UniqueConstraint(this);
					this._table.Constraints.Add(uniqueConstraint2);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</summary>
		/// <returns>true if the column is read only; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to false on a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000032D4 File Offset: 0x000014D4
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000032DC File Offset: 0x000014DC
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataColumn.set_ReadOnly|API> {0}, {1}", this.ObjectID, value);
				if (this._readOnly != value)
				{
					if (!value && this._expression != null)
					{
						throw ExceptionBuilder.ReadOnlyAndExpression();
					}
					this._readOnly = value;
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003318 File Offset: 0x00001518
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Index SortIndex
		{
			get
			{
				if (this._sortIndex == null)
				{
					IndexField[] array = new IndexField[]
					{
						new IndexField(this, false)
					};
					this._sortIndex = this._table.GetIndex(array, DataViewRowState.CurrentRows, null);
					this._sortIndex.AddRef();
				}
				return this._sortIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the column belongs to.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataColumn" /> belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003368 File Offset: 0x00001568
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003370 File Offset: 0x00001570
		internal void SetTable(DataTable table)
		{
			if (this._table != table)
			{
				if (this.Computed && (table == null || (!table.fInitInProgress && (table.DataSet == null || (!table.DataSet._fIsSchemaLoading && !table.DataSet._fInitInProgress)))))
				{
					this.DataExpression.Bind(table);
				}
				if (this.Unique && this._table != null)
				{
					UniqueConstraint uniqueConstraint = table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						table.Constraints.CanRemove(uniqueConstraint, true);
					}
				}
				this._table = table;
				this._storage = null;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003403 File Offset: 0x00001603
		private DataRow GetDataRow(int index)
		{
			return this._table._recordManager[index];
		}

		// Token: 0x17000024 RID: 36
		internal object this[int record]
		{
			get
			{
				return this._storage.Get(record);
			}
			set
			{
				try
				{
					this._storage.Set(record, value);
				}
				catch (Exception ex)
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
				}
				if (this.AutoIncrement && !this._storage.IsNull(record))
				{
					this.AutoInc.SetCurrentAndIncrement(this._storage.Get(record));
				}
				if (this.Computed)
				{
					DataRow dataRow = this.GetDataRow(record);
					if (dataRow != null)
					{
						dataRow.LastChangedColumn = this;
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034B0 File Offset: 0x000016B0
		internal void InitializeRecord(int record)
		{
			this._storage.Set(record, this.DefaultValue);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000034C4 File Offset: 0x000016C4
		internal void SetValue(int record, object value)
		{
			try
			{
				this._storage.Set(record, value);
			}
			catch (Exception ex)
			{
				ExceptionBuilder.TraceExceptionForCapture(ex);
				throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
			}
			DataRow dataRow = this.GetDataRow(record);
			if (dataRow != null)
			{
				dataRow.LastChangedColumn = this;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000351C File Offset: 0x0000171C
		internal void FreeRecord(int record)
		{
			this._storage.Set(record, this._storage._nullValue);
		}

		/// <summary>Gets or sets a value that indicates whether the values in each row of the column must be unique.</summary>
		/// <returns>true if the value must be unique; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a calculated column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003535 File Offset: 0x00001735
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003540 File Offset: 0x00001740
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Unique
		{
			get
			{
				return this._unique;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataColumn.set_Unique|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._unique != value)
					{
						if (value && this._expression != null)
						{
							throw ExceptionBuilder.UniqueAndExpression();
						}
						UniqueConstraint uniqueConstraint = null;
						if (this._table != null)
						{
							if (value)
							{
								this.CheckUnique();
							}
							else
							{
								foreach (object obj in this.Table.Constraints)
								{
									UniqueConstraint uniqueConstraint2 = obj as UniqueConstraint;
									if (uniqueConstraint2 != null && uniqueConstraint2.ColumnsReference.Length == 1 && uniqueConstraint2.ColumnsReference[0] == this)
									{
										uniqueConstraint = uniqueConstraint2;
									}
								}
								this._table.Constraints.CanRemove(uniqueConstraint, true);
							}
						}
						this._unique = value;
						if (this._table != null)
						{
							if (value)
							{
								UniqueConstraint uniqueConstraint3 = new UniqueConstraint(this);
								this._table.Constraints.Add(uniqueConstraint3);
							}
							else
							{
								this._table.Constraints.Remove(uniqueConstraint);
							}
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003648 File Offset: 0x00001848
		internal void InternalUnique(bool value)
		{
			this._unique = value;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003651 File Offset: 0x00001851
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003659 File Offset: 0x00001859
		internal string XmlDataType { get; set; } = string.Empty;

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000366A File Offset: 0x0000186A
		internal SimpleType SimpleType
		{
			get
			{
				return this._simpleType;
			}
			set
			{
				this._simpleType = value;
				if (value != null && value.CanHaveMaxLength())
				{
					this._maxLength = this._simpleType.MaxLength;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.MappingType" /> of the column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MappingType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000368F File Offset: 0x0000188F
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003698 File Offset: 0x00001898
		[DefaultValue(MappingType.Element)]
		public virtual MappingType ColumnMapping
		{
			get
			{
				return this._columnMapping;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, MappingType>("<ds.DataColumn.set_ColumnMapping|API> {0}, {1}", this.ObjectID, value);
				if (value != this._columnMapping)
				{
					if (value == MappingType.SimpleContent && this._table != null)
					{
						int num = 0;
						if (this._columnMapping == MappingType.Element)
						{
							num = 1;
						}
						if (this._dataType == typeof(char))
						{
							throw ExceptionBuilder.CannotSetSimpleContent(this.ColumnName, this._dataType);
						}
						if (this._table.XmlText != null && this._table.XmlText != this)
						{
							throw ExceptionBuilder.CannotAddColumn3();
						}
						if (this._table.ElementColumnCount > num)
						{
							throw ExceptionBuilder.CannotAddColumn4(this.ColumnName);
						}
					}
					this.RaisePropertyChanging("ColumnMapping");
					if (this._table != null)
					{
						if (this._columnMapping == MappingType.SimpleContent)
						{
							this._table._xmlText = null;
						}
						if (value == MappingType.Element)
						{
							DataTable table = this._table;
							int num2 = table.ElementColumnCount;
							table.ElementColumnCount = num2 + 1;
						}
						else if (this._columnMapping == MappingType.Element)
						{
							DataTable table2 = this._table;
							int num2 = table2.ElementColumnCount;
							table2.ElementColumnCount = num2 - 1;
						}
					}
					this._columnMapping = value;
					if (value == MappingType.SimpleContent)
					{
						this._columnUri = null;
						if (this._table != null)
						{
							this._table.XmlText = this;
						}
						this.SimpleType = null;
					}
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000037D2 File Offset: 0x000019D2
		internal void CheckColumnConstraint(DataRow row, DataRowAction action)
		{
			if (this._table.UpdatingCurrent(row, action))
			{
				this.CheckNullable(row);
				this.CheckMaxLength(row);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037F4 File Offset: 0x000019F4
		internal bool CheckMaxLength()
		{
			if (0 <= this._maxLength && this.Table != null && 0 < this.Table.Rows.Count)
			{
				foreach (object obj in this.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.HasVersion(DataRowVersion.Current) && this._maxLength < this.GetStringLength(dataRow.GetCurrentRecordNo()))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003898 File Offset: 0x00001A98
		internal void CheckMaxLength(DataRow dr)
		{
			if (0 <= this._maxLength && this._maxLength < this.GetStringLength(dr.GetDefaultRecord()))
			{
				throw ExceptionBuilder.LongerThanMaxLength(this);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0600005B RID: 91 RVA: 0x000038C0 File Offset: 0x00001AC0
		protected internal void CheckNotAllowNull()
		{
			if (this._storage == null)
			{
				return;
			}
			if (this._sortIndex != null)
			{
				if (this._sortIndex.IsKeyInIndex(this._storage._nullValue))
				{
					throw ExceptionBuilder.NullKeyValues(this.ColumnName);
				}
			}
			else
			{
				foreach (object obj in this._table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						if (!this._implementsINullable)
						{
							if (dataRow[this] == DBNull.Value)
							{
								throw ExceptionBuilder.NullKeyValues(this.ColumnName);
							}
						}
						else if (DataStorage.IsObjectNull(dataRow[this]))
						{
							throw ExceptionBuilder.NullKeyValues(this.ColumnName);
						}
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003994 File Offset: 0x00001B94
		internal void CheckNullable(DataRow row)
		{
			if (!this.AllowDBNull && this._storage.IsNull(row.GetDefaultRecord()))
			{
				throw ExceptionBuilder.NullValues(this.ColumnName);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0600005D RID: 93 RVA: 0x000039BD File Offset: 0x00001BBD
		protected void CheckUnique()
		{
			if (!this.SortIndex.CheckUnique())
			{
				throw ExceptionBuilder.NonUniqueValues(this.ColumnName);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000039D8 File Offset: 0x00001BD8
		internal int Compare(int record1, int record2)
		{
			return this._storage.Compare(record1, record2);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000039E8 File Offset: 0x00001BE8
		internal bool CompareValueTo(int record1, object value, bool checkType)
		{
			if (this.CompareValueTo(record1, value) == 0)
			{
				Type type = value.GetType();
				Type type2 = this._storage.Get(record1).GetType();
				if (type == typeof(string) && type2 == typeof(string))
				{
					return string.CompareOrdinal((string)this._storage.Get(record1), (string)value) == 0;
				}
				if (type == type2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A6A File Offset: 0x00001C6A
		internal int CompareValueTo(int record1, object value)
		{
			return this._storage.CompareValueTo(record1, value);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003A79 File Offset: 0x00001C79
		internal object ConvertValue(object value)
		{
			return this._storage.ConvertValue(value);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A87 File Offset: 0x00001C87
		internal void Copy(int srcRecordNo, int dstRecordNo)
		{
			this._storage.Copy(srcRecordNo, dstRecordNo);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A98 File Offset: 0x00001C98
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal DataColumn Clone()
		{
			DataColumn dataColumn = (DataColumn)Activator.CreateInstance(base.GetType());
			dataColumn.SimpleType = this.SimpleType;
			dataColumn._allowNull = this._allowNull;
			if (this._autoInc != null)
			{
				dataColumn._autoInc = this._autoInc.Clone();
			}
			dataColumn._caption = this._caption;
			dataColumn.ColumnName = this.ColumnName;
			dataColumn._columnUri = this._columnUri;
			dataColumn._columnPrefix = this._columnPrefix;
			dataColumn.DataType = this.DataType;
			dataColumn._defaultValue = this._defaultValue;
			dataColumn._defaultValueIsNull = this._defaultValue == DBNull.Value || (dataColumn.ImplementsINullable && DataStorage.IsObjectSqlNull(this._defaultValue));
			dataColumn._columnMapping = this._columnMapping;
			dataColumn._readOnly = this._readOnly;
			dataColumn.MaxLength = this.MaxLength;
			dataColumn.XmlDataType = this.XmlDataType;
			dataColumn._dateTimeMode = this._dateTimeMode;
			if (this._extendedProperties != null)
			{
				foreach (object obj in this._extendedProperties.Keys)
				{
					dataColumn.ExtendedProperties[obj] = this._extendedProperties[obj];
				}
			}
			return dataColumn;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C00 File Offset: 0x00001E00
		internal object GetAggregateValue(int[] records, AggregateType kind)
		{
			if (this._storage != null)
			{
				return this._storage.Aggregate(records, kind);
			}
			if (kind != AggregateType.Count)
			{
				return DBNull.Value;
			}
			return 0;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003C29 File Offset: 0x00001E29
		private int GetStringLength(int record)
		{
			return this._storage.GetStringLength(record);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C38 File Offset: 0x00001E38
		internal void Init(int record)
		{
			if (this.AutoIncrement)
			{
				object obj = this._autoInc.Current;
				this._autoInc.MoveAfter();
				this._storage.Set(record, obj);
				return;
			}
			this[record] = this._defaultValue;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C80 File Offset: 0x00001E80
		internal static bool IsAutoIncrementType(Type dataType)
		{
			return dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short) || dataType == typeof(decimal) || dataType == typeof(BigInteger) || dataType == typeof(SqlInt32) || dataType == typeof(SqlInt64) || dataType == typeof(SqlInt16) || dataType == typeof(SqlDecimal);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003D32 File Offset: 0x00001F32
		internal bool IsCustomType
		{
			get
			{
				if (this._storage == null)
				{
					return DataStorage.IsTypeCustomType(this.DataType);
				}
				return this._storage._isCustomDefinedType;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D53 File Offset: 0x00001F53
		internal bool IsValueCustomTypeInstance(object value)
		{
			return DataStorage.IsTypeCustomType(value.GetType()) && !(value is Type);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003D70 File Offset: 0x00001F70
		internal bool ImplementsIXMLSerializable
		{
			get
			{
				return this._implementsIXMLSerializable;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003D78 File Offset: 0x00001F78
		internal bool IsNull(int record)
		{
			return this._storage.IsNull(record);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D88 File Offset: 0x00001F88
		internal bool IsInRelation()
		{
			DataRelationCollection dataRelationCollection = this._table.ParentRelations;
			for (int i = 0; i < dataRelationCollection.Count; i++)
			{
				if (dataRelationCollection[i].ChildKey.ContainsColumn(this))
				{
					return true;
				}
			}
			dataRelationCollection = this._table.ChildRelations;
			for (int j = 0; j < dataRelationCollection.Count; j++)
			{
				if (dataRelationCollection[j].ParentKey.ContainsColumn(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E04 File Offset: 0x00002004
		internal bool IsMaxLengthViolated()
		{
			if (this.MaxLength < 0)
			{
				return true;
			}
			bool flag = false;
			string text = null;
			foreach (object obj in this.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.HasVersion(DataRowVersion.Current))
				{
					object obj2 = dataRow[this];
					if (!this._isSqlType)
					{
						if (obj2 != null && obj2 != DBNull.Value && ((string)obj2).Length > this.MaxLength)
						{
							if (text == null)
							{
								text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
							}
							dataRow.RowError = text;
							dataRow.SetColumnError(this, text);
							flag = true;
						}
					}
					else if (!DataStorage.IsObjectNull(obj2) && ((SqlString)obj2).Value.Length > this.MaxLength)
					{
						if (text == null)
						{
							text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
						}
						dataRow.RowError = text;
						dataRow.SetColumnError(this, text);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F24 File Offset: 0x00002124
		internal bool IsNotAllowDBNullViolated()
		{
			Index sortIndex = this.SortIndex;
			DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(DBNull.Value));
			for (int i = 0; i < rows.Length; i++)
			{
				string text = ExceptionBuilder.NotAllowDBNullViolationText(this.ColumnName);
				rows[i].RowError = text;
				rows[i].SetColumnError(this, text);
			}
			return rows.Length != 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x0600006F RID: 111 RVA: 0x00003F7A File Offset: 0x0000217A
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x06000070 RID: 112 RVA: 0x00003F8E File Offset: 0x0000218E
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F9C File Offset: 0x0000219C
		private void InsureStorage()
		{
			if (this._storage == null)
			{
				this._storage = DataStorage.CreateStorage(this, this._dataType, this._storageType);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003FBE File Offset: 0x000021BE
		internal void SetCapacity(int capacity)
		{
			this.InsureStorage();
			this._storage.SetCapacity(capacity);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003FD2 File Offset: 0x000021D2
		internal void OnSetDataSet()
		{
		}

		/// <summary>Gets the <see cref="P:System.Data.DataColumn.Expression" /> of the column, if one exists.</summary>
		/// <returns>The <see cref="P:System.Data.DataColumn.Expression" /> value, if the property is set; otherwise, the <see cref="P:System.Data.DataColumn.ColumnName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000074 RID: 116 RVA: 0x00003FD4 File Offset: 0x000021D4
		public override string ToString()
		{
			if (this._expression != null)
			{
				return this.ColumnName + " + " + this.Expression;
			}
			return this.ColumnName;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003FFB File Offset: 0x000021FB
		internal object ConvertXmlToObject(string s)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(s);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000400F File Offset: 0x0000220F
		internal object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(xmlReader, xmlAttrib);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004024 File Offset: 0x00002224
		internal string ConvertObjectToXml(object value)
		{
			this.InsureStorage();
			return this._storage.ConvertObjectToXml(value);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004038 File Offset: 0x00002238
		internal void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			this._storage.ConvertObjectToXml(value, xmlWriter, xmlAttrib);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000404E File Offset: 0x0000224E
		internal object GetEmptyColumnStore(int recordCount)
		{
			this.InsureStorage();
			return this._storage.GetEmptyStorageInternal(recordCount);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004062 File Offset: 0x00002262
		internal void CopyValueIntoStore(int record, object store, BitArray nullbits, int storeIndex)
		{
			this._storage.CopyValueInternal(record, store, nullbits, storeIndex);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004074 File Offset: 0x00002274
		internal void SetStorage(object store, BitArray nullbits)
		{
			this.InsureStorage();
			this._storage.SetStorageInternal(store, nullbits);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004089 File Offset: 0x00002289
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns == null)
			{
				this._dependentColumns = new List<DataColumn>();
			}
			this._dependentColumns.Add(expressionColumn);
			this._table.AddDependentColumn(expressionColumn);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000040B6 File Offset: 0x000022B6
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns != null && this._dependentColumns.Contains(expressionColumn))
			{
				this._dependentColumns.Remove(expressionColumn);
			}
			this._table.RemoveDependentColumn(expressionColumn);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000040E8 File Offset: 0x000022E8
		internal void HandleDependentColumnList(DataExpression oldExpression, DataExpression newExpression)
		{
			if (oldExpression != null)
			{
				foreach (DataColumn dataColumn in oldExpression.GetDependency())
				{
					dataColumn.RemoveDependentColumn(this);
					if (dataColumn._table != this._table)
					{
						this._table.RemoveDependentColumn(this);
					}
				}
				this._table.RemoveDependentColumn(this);
			}
			if (newExpression != null)
			{
				foreach (DataColumn dataColumn2 in newExpression.GetDependency())
				{
					dataColumn2.AddDependentColumn(this);
					if (dataColumn2._table != this._table)
					{
						this._table.AddDependentColumn(this);
					}
				}
				this._table.AddDependentColumn(this);
			}
		}

		// Token: 0x04000009 RID: 9
		private bool _allowNull = true;

		// Token: 0x0400000A RID: 10
		private string _caption;

		// Token: 0x0400000B RID: 11
		private string _columnName;

		// Token: 0x0400000C RID: 12
		private Type _dataType;

		// Token: 0x0400000D RID: 13
		private StorageType _storageType;

		// Token: 0x0400000E RID: 14
		internal object _defaultValue = DBNull.Value;

		// Token: 0x0400000F RID: 15
		private DataSetDateTime _dateTimeMode = DataSetDateTime.UnspecifiedLocal;

		// Token: 0x04000010 RID: 16
		private DataExpression _expression;

		// Token: 0x04000011 RID: 17
		private int _maxLength = -1;

		// Token: 0x04000012 RID: 18
		private int _ordinal = -1;

		// Token: 0x04000013 RID: 19
		private bool _readOnly;

		// Token: 0x04000014 RID: 20
		internal Index _sortIndex;

		// Token: 0x04000015 RID: 21
		internal DataTable _table;

		// Token: 0x04000016 RID: 22
		private bool _unique;

		// Token: 0x04000017 RID: 23
		internal MappingType _columnMapping = MappingType.Element;

		// Token: 0x04000018 RID: 24
		internal int _hashCode;

		// Token: 0x04000019 RID: 25
		internal int _errors;

		// Token: 0x0400001A RID: 26
		private bool _isSqlType;

		// Token: 0x0400001B RID: 27
		private bool _implementsINullable;

		// Token: 0x0400001C RID: 28
		private bool _implementsIChangeTracking;

		// Token: 0x0400001D RID: 29
		private bool _implementsIRevertibleChangeTracking;

		// Token: 0x0400001E RID: 30
		private bool _implementsIXMLSerializable;

		// Token: 0x0400001F RID: 31
		private bool _defaultValueIsNull = true;

		// Token: 0x04000020 RID: 32
		internal List<DataColumn> _dependentColumns;

		// Token: 0x04000021 RID: 33
		internal PropertyCollection _extendedProperties;

		// Token: 0x04000022 RID: 34
		private DataStorage _storage;

		// Token: 0x04000023 RID: 35
		private AutoIncrementValue _autoInc;

		// Token: 0x04000024 RID: 36
		internal string _columnUri;

		// Token: 0x04000025 RID: 37
		private string _columnPrefix = string.Empty;

		// Token: 0x04000026 RID: 38
		internal string _encodedColumnName;

		// Token: 0x04000027 RID: 39
		internal SimpleType _simpleType;

		// Token: 0x04000028 RID: 40
		private static int s_objectTypeCount;

		// Token: 0x04000029 RID: 41
		private readonly int _objectID = Interlocked.Increment(ref DataColumn.s_objectTypeCount);

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanging;
	}
}
