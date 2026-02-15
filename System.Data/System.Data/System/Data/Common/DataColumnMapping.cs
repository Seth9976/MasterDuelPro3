using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a generic column mapping for an object that inherits from <see cref="T:System.Data.Common.DataAdapter" />. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000E7 RID: 231
	[TypeConverter(typeof(DataColumnMapping.DataColumnMappingConverter))]
	public sealed class DataColumnMapping : MarshalByRefObject, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class.</summary>
		// Token: 0x06000C38 RID: 3128 RVA: 0x00043059 File Offset: 0x00041259
		public DataColumnMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class with the specified source column name and <see cref="T:System.Data.DataSet" /> column name to map to.</summary>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		// Token: 0x06000C39 RID: 3129 RVA: 0x00043061 File Offset: 0x00041261
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DataSetColumn = dataSetColumn;
		}

		/// <summary>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</summary>
		/// <returns>The name of the column within the <see cref="T:System.Data.DataSet" /> to map to. The name is not case sensitive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00043077 File Offset: 0x00041277
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00043088 File Offset: 0x00041288
		[DefaultValue("")]
		public string DataSetColumn
		{
			get
			{
				return this._dataSetColumnName ?? string.Empty;
			}
			set
			{
				this._dataSetColumnName = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00043091 File Offset: 0x00041291
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x00043099 File Offset: 0x00041299
		internal DataColumnMappingCollection Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		/// <summary>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</summary>
		/// <returns>The case-sensitive name of the column in the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000430A2 File Offset: 0x000412A2
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000430B3 File Offset: 0x000412B3
		[DefaultValue("")]
		public string SourceColumn
		{
			get
			{
				return this._sourceColumnName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceColumnName, value) != 0)
				{
					this.Parent.ValidateSourceColumn(-1, value);
				}
				this._sourceColumnName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current object.</returns>
		// Token: 0x06000C40 RID: 3136 RVA: 0x000430DF File Offset: 0x000412DF
		object ICloneable.Clone()
		{
			return new DataColumnMapping
			{
				_sourceColumnName = this._sourceColumnName,
				_dataSetColumnName = this._dataSetColumnName
			};
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the given <see cref="T:System.Data.DataTable" /> using the <see cref="T:System.Data.MissingSchemaAction" /> and the <see cref="P:System.Data.Common.DataColumnMapping.DataSetColumn" /> property.</summary>
		/// <returns>A data column.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to get the column from.</param>
		/// <param name="dataType">The <see cref="T:System.Type" /> of the data column.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C41 RID: 3137 RVA: 0x000430FE File Offset: 0x000412FE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			return DataColumnMapping.GetDataColumnBySchemaAction(this.SourceColumn, this.DataSetColumn, dataTable, dataType, schemaAction);
		}

		/// <summary>A static version of <see cref="M:System.Data.Common.DataColumnMapping.GetDataColumnBySchemaAction(System.Data.DataTable,System.Type,System.Data.MissingSchemaAction)" /> that can be called without instantiating a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="schemaAction">Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C42 RID: 3138 RVA: 0x00043114 File Offset: 0x00041314
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable == null)
			{
				throw ADP.ArgumentNull("dataTable");
			}
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			DataColumnCollection columns = dataTable.Columns;
			int num = columns.IndexOf(dataSetColumn);
			if (0 > num || num >= columns.Count)
			{
				return DataColumnMapping.CreateDataColumnBySchemaAction(sourceColumn, dataSetColumn, dataTable, dataType, schemaAction);
			}
			DataColumn dataColumn = columns[num];
			if (!string.IsNullOrEmpty(dataColumn.Expression))
			{
				throw ADP.ColumnSchemaExpression(sourceColumn, dataSetColumn);
			}
			if (null == dataType || dataType.IsArray == dataColumn.DataType.IsArray)
			{
				return dataColumn;
			}
			throw ADP.ColumnSchemaMismatch(sourceColumn, dataType, dataColumn);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000431A4 File Offset: 0x000413A4
		internal static DataColumn CreateDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataColumn(dataSetColumn, dataType);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.ColumnSchemaMissing(dataSetColumn, dataTable.TableName, sourceColumn);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C44 RID: 3140 RVA: 0x000431F5 File Offset: 0x000413F5
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x040004EE RID: 1262
		private DataColumnMappingCollection _parent;

		// Token: 0x040004EF RID: 1263
		private string _dataSetColumnName;

		// Token: 0x040004F0 RID: 1264
		private string _sourceColumnName;

		// Token: 0x020000E8 RID: 232
		internal sealed class DataColumnMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x06000C46 RID: 3142 RVA: 0x000431FD File Offset: 0x000413FD
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x0004321C File Offset: 0x0004141C
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataColumnMapping)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)value;
					object[] array = new object[] { dataColumnMapping.SourceColumn, dataColumnMapping.DataSetColumn };
					Type[] array2 = new Type[]
					{
						typeof(string),
						typeof(string)
					};
					return new InstanceDescriptor(typeof(DataColumnMapping).GetConstructor(array2), array);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
