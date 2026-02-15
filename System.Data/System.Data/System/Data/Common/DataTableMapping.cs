using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a description of a mapped relationship between a source table and a <see cref="T:System.Data.DataTable" />. This class is used by a <see cref="T:System.Data.Common.DataAdapter" /> when populating a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000EE RID: 238
	[TypeConverter(typeof(DataTableMapping.DataTableMappingConverter))]
	public sealed class DataTableMapping : MarshalByRefObject, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class.</summary>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00043059 File Offset: 0x00041259
		public DataTableMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class when given a source table name, a <see cref="T:System.Data.DataTable" /> name, and an array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source. </param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="columnMappings">An array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects. </param>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00044284 File Offset: 0x00042484
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
			if (columnMappings != null && columnMappings.Length != 0)
			{
				this.ColumnMappings.AddRange(columnMappings);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A data column mapping collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000442B0 File Offset: 0x000424B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnMappingCollection ColumnMappings
		{
			get
			{
				DataColumnMappingCollection dataColumnMappingCollection = this._columnMappings;
				if (dataColumnMappingCollection == null)
				{
					dataColumnMappingCollection = new DataColumnMappingCollection();
					this._columnMappings = dataColumnMappingCollection;
				}
				return dataColumnMappingCollection;
			}
		}

		/// <summary>Gets or sets the table name from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The table name from a <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000442D5 File Offset: 0x000424D5
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x000442E6 File Offset: 0x000424E6
		[DefaultValue("")]
		public string DataSetTable
		{
			get
			{
				return this._dataSetTableName ?? string.Empty;
			}
			set
			{
				this._dataSetTableName = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x000442EF File Offset: 0x000424EF
		internal DataTableMappingCollection Parent
		{
			get
			{
				return this._parent;
			}
		}

		/// <summary>Gets or sets the case-sensitive source table name from a data source.</summary>
		/// <returns>The case-sensitive source table name from a data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x000442F7 File Offset: 0x000424F7
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00044308 File Offset: 0x00042508
		[DefaultValue("")]
		public string SourceTable
		{
			get
			{
				return this._sourceTableName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceTableName, value) != 0)
				{
					this.Parent.ValidateSourceTable(-1, value);
				}
				this._sourceTableName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x06000CBC RID: 3260 RVA: 0x00044334 File Offset: 0x00042534
		object ICloneable.Clone()
		{
			DataTableMapping dataTableMapping = new DataTableMapping();
			dataTableMapping._dataSetTableName = this._dataSetTableName;
			dataTableMapping._sourceTableName = this._sourceTableName;
			if (this._columnMappings != null && 0 < this.ColumnMappings.Count)
			{
				DataColumnMappingCollection columnMappings = dataTableMapping.ColumnMappings;
				foreach (object obj in this.ColumnMappings)
				{
					ICloneable cloneable = (ICloneable)obj;
					columnMappings.Add(cloneable.Clone());
				}
			}
			return dataTableMapping;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataColumn" /> object for a given column name.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="dataType">The data type for <paramref name="sourceColumn" />.</param>
		/// <param name="dataTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000CBD RID: 3261 RVA: 0x000443D4 File Offset: 0x000425D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			return DataColumnMappingCollection.GetDataColumn(this._columnMappings, sourceColumn, dataType, dataTable, mappingAction, schemaAction);
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name, as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000CBE RID: 3262 RVA: 0x000443E8 File Offset: 0x000425E8
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x04000531 RID: 1329
		private DataTableMappingCollection _parent;

		// Token: 0x04000532 RID: 1330
		private DataColumnMappingCollection _columnMappings;

		// Token: 0x04000533 RID: 1331
		private string _dataSetTableName;

		// Token: 0x04000534 RID: 1332
		private string _sourceTableName;

		// Token: 0x020000EF RID: 239
		internal sealed class DataTableMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x06000CC0 RID: 3264 RVA: 0x000431FD File Offset: 0x000413FD
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x000443F0 File Offset: 0x000425F0
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataTableMapping)
				{
					DataTableMapping dataTableMapping = (DataTableMapping)value;
					DataColumnMapping[] array = new DataColumnMapping[dataTableMapping.ColumnMappings.Count];
					dataTableMapping.ColumnMappings.CopyTo(array, 0);
					object[] array2 = new object[] { dataTableMapping.SourceTable, dataTableMapping.DataSetTable, array };
					Type[] array3 = new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(DataColumnMapping[])
					};
					return new InstanceDescriptor(typeof(DataTableMapping).GetConstructor(array3), array2);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
