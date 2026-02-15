using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents an in-memory cache of data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000019 RID: 25
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[XmlRoot("DataSet")]
	[DefaultProperty("DataSetName")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		// Token: 0x060001C7 RID: 455 RVA: 0x00005830 File Offset: 0x00003A30
		public DataSet()
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int>("<ds.DataSet.DataSet|API> {0}", this.ObjectID);
			this._tableCollection = new DataTableCollection(this);
			this._relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class with the given name.</summary>
		/// <param name="dataSetName">The name of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x060001C8 RID: 456 RVA: 0x000058D6 File Offset: 0x00003AD6
		public DataSet(string dataSetName)
			: this()
		{
			this.DataSetName = dataSetName;
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SerializationFormat" /> for the <see cref="T:System.Data.DataSet" /> used during remoting.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000058E5 File Offset: 0x00003AE5
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000058F0 File Offset: 0x00003AF0
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				this._remotingFormat = value;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RemotingFormat = value;
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000593A File Offset: 0x00003B3A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class that has the given serialization information and context.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x060001CC RID: 460 RVA: 0x0000593D File Offset: 0x00003B3D
		protected DataSet(SerializationInfo info, StreamingContext context)
			: this(info, context, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		/// <param name="ConstructSchema">The boolean value.</param>
		// Token: 0x060001CD RID: 461 RVA: 0x00005948 File Offset: 0x00003B48
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema)
			: this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "DataSet.RemotingFormat"))
				{
					if (name == "SchemaSerializationMode.DataSet")
					{
						schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					}
				}
				else
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			if (schemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (serializationFormat == SerializationFormat.Xml && !ConstructSchema)
			{
				return;
			}
			this.DeserializeDataSet(info, context, serializationFormat, schemaSerializationMode);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x060001CE RID: 462 RVA: 0x000059C8 File Offset: 0x00003BC8
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		/// <summary>Deserialize all of the tables data of the DataSet from the binary or XML stream.</summary>
		// Token: 0x060001CF RID: 463 RVA: 0x00003FD2 File Offset: 0x000021D2
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000059E8 File Offset: 0x00003BE8
		private void SerializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			info.AddValue("DataSet.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataSet.RemotingFormat", remotingFormat);
			}
			if (SchemaSerializationMode.IncludeSchema != this.SchemaSerializationMode)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				if (this.SchemaSerializationMode == SchemaSerializationMode.IncludeSchema)
				{
					this.SerializeDataSetProperties(info, context);
					info.AddValue("DataSet.Tables.Count", this.Tables.Count);
					for (int i = 0; i < this.Tables.Count; i++)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, this.Tables[i]);
						memoryStream.Position = 0L;
						info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), memoryStream.GetBuffer());
					}
					for (int j = 0; j < this.Tables.Count; j++)
					{
						this.Tables[j].SerializeConstraints(info, context, j, true);
					}
					this.SerializeRelations(info, context);
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].SerializeExpressionColumns(info, context, k);
					}
				}
				else
				{
					this.SerializeDataSetProperties(info, context);
				}
				for (int l = 0; l < this.Tables.Count; l++)
				{
					this.Tables[l].SerializeTableData(info, context, l);
				}
				return;
			}
			string xmlSchemaForRemoting = this.GetXmlSchemaForRemoting(null);
			info.AddValue("XmlSchema", xmlSchemaForRemoting);
			StringWriter stringWriter = new StringWriter(new StringBuilder(this.EstimatedXmlStringSize() * 2), CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			this.WriteXml(xmlTextWriter, XmlWriteMode.DiffGram);
			string text = stringWriter.ToString();
			info.AddValue("XmlDiffGram", text);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005BC6 File Offset: 0x00003DC6
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005BDC File Offset: 0x00003DDC
		private void DeserializeDataSetSchema(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			if (remotingFormat == SerializationFormat.Xml)
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				if (text != null)
				{
					this.ReadXmlSchema(new XmlTextReader(new StringReader(text)), true);
				}
				return;
			}
			if (schemaSerializationMode == SchemaSerializationMode.IncludeSchema)
			{
				this.DeserializeDataSetProperties(info, context);
				int @int = info.GetInt32("DataSet.Tables.Count");
				for (int i = 0; i < @int; i++)
				{
					MemoryStream memoryStream = new MemoryStream((byte[])info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), typeof(byte[])));
					memoryStream.Position = 0L;
					DataTable dataTable = (DataTable)new BinaryFormatter(null, new StreamingContext(context.State, false)).Deserialize(memoryStream);
					this.Tables.Add(dataTable);
				}
				for (int j = 0; j < @int; j++)
				{
					this.Tables[j].DeserializeConstraints(info, context, j, true);
				}
				this.DeserializeRelations(info, context);
				for (int k = 0; k < @int; k++)
				{
					this.Tables[k].DeserializeExpressionColumns(info, context, k);
				}
				return;
			}
			this.DeserializeDataSetProperties(info, context);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005D14 File Offset: 0x00003F14
		private void DeserializeDataSetData(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].DeserializeTableData(info, context, i);
				}
				return;
			}
			string text = (string)info.GetValue("XmlDiffGram", typeof(string));
			if (text != null)
			{
				this.ReadXml(new XmlTextReader(new StringReader(text)), XmlReadMode.DiffGram);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005D80 File Offset: 0x00003F80
		private void SerializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSet.DataSetName", this.DataSetName);
			info.AddValue("DataSet.Namespace", this.Namespace);
			info.AddValue("DataSet.Prefix", this.Prefix);
			info.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			info.AddValue("DataSet.ExtendedProperties", this.ExtendedProperties);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005E0C File Offset: 0x0000400C
		private void DeserializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			this._dataSetName = info.GetString("DataSet.DataSetName");
			this._namespaceURI = info.GetString("DataSet.Namespace");
			this._datasetPrefix = info.GetString("DataSet.Prefix");
			this._caseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			int num = (int)info.GetValue("DataSet.LocaleLCID", typeof(int));
			this._culture = new CultureInfo(num);
			this._cultureUserSet = true;
			this._enforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this._extendedProperties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005EBC File Offset: 0x000040BC
		private void SerializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				int[] array = new int[dataRelation.ParentColumns.Length + 1];
				array[0] = this.Tables.IndexOf(dataRelation.ParentTable);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = dataRelation.ParentColumns[i - 1].Ordinal;
				}
				int[] array2 = new int[dataRelation.ChildColumns.Length + 1];
				array2[0] = this.Tables.IndexOf(dataRelation.ChildTable);
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j] = dataRelation.ChildColumns[j - 1].Ordinal;
				}
				arrayList.Add(new ArrayList { dataRelation.RelationName, array, array2, dataRelation.Nested, dataRelation._extendedProperties });
			}
			info.AddValue("DataSet.Relations", arrayList);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006020 File Offset: 0x00004220
		private void DeserializeRelations(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in ((ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList))))
			{
				ArrayList arrayList = (ArrayList)obj;
				string text = (string)arrayList[0];
				int[] array = (int[])arrayList[1];
				int[] array2 = (int[])arrayList[2];
				bool flag = (bool)arrayList[3];
				PropertyCollection propertyCollection = (PropertyCollection)arrayList[4];
				DataColumn[] array3 = new DataColumn[array.Length - 1];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = this.Tables[array[0]].Columns[array[i + 1]];
				}
				DataColumn[] array4 = new DataColumn[array2.Length - 1];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = this.Tables[array2[0]].Columns[array2[j + 1]];
				}
				DataRelation dataRelation = new DataRelation(text, array3, array4, false);
				dataRelation.CheckMultipleNested = false;
				dataRelation.Nested = flag;
				dataRelation._extendedProperties = propertyCollection;
				this.Relations.Add(dataRelation);
				dataRelation.CheckMultipleNested = true;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000619C File Offset: 0x0000439C
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		/// <summary>Gets or sets a value indicating whether string comparisons within <see cref="T:System.Data.DataTable" /> objects are case-sensitive.</summary>
		/// <returns>true if string comparisons are case-sensitive; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000061AA File Offset: 0x000043AA
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000061B4 File Offset: 0x000043B4
		[DefaultValue(false)]
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					this._caseSensitive = value;
					if (!this.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					foreach (object obj in this.Tables)
					{
						((DataTable)obj).SetCaseSensitiveValue(value, false, true);
					}
				}
			}
		}

		/// <summary>Gets a custom view of the data contained in the <see cref="T:System.Data.DataSet" /> to allow filtering, searching, and navigating using a custom <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000623C File Offset: 0x0000443C
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this._defaultViewManager == null)
				{
					object defaultViewManagerLock = this._defaultViewManagerLock;
					lock (defaultViewManagerLock)
					{
						if (this._defaultViewManager == null)
						{
							this._defaultViewManager = new DataViewManager(this, true);
						}
					}
				}
				return this._defaultViewManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether constraint rules are followed when attempting any update operation.</summary>
		/// <returns>true if rules are enforced; otherwise false. The default is true.</returns>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enforced. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000062A4 File Offset: 0x000044A4
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this._enforceConstraints;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.set_EnforceConstraints|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._enforceConstraints != value)
					{
						if (value)
						{
							this.EnableConstraints();
						}
						this._enforceConstraints = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006300 File Offset: 0x00004500
		internal void RestoreEnforceConstraints(bool value)
		{
			this._enforceConstraints = value;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000630C File Offset: 0x0000450C
		internal void EnableConstraints()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.EnableConstraints|INFO> {0}", this.ObjectID);
			try
			{
				bool flag = false;
				ConstraintEnumerator constraintEnumerator = new ConstraintEnumerator(this);
				while (constraintEnumerator.GetNext())
				{
					Constraint constraint = constraintEnumerator.GetConstraint();
					flag |= constraint.IsConstraintViolated();
				}
				foreach (object obj in this.Tables)
				{
					foreach (object obj2 in ((DataTable)obj).Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (!dataColumn.AllowDBNull)
						{
							flag |= dataColumn.IsNotAllowDBNullViolated();
						}
						if (dataColumn.MaxLength >= 0)
						{
							flag |= dataColumn.IsMaxLengthViolated();
						}
					}
				}
				if (flag)
				{
					this.FailedEnableConstraints();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets or sets the name of the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006430 File Offset: 0x00004630
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00006438 File Offset: 0x00004638
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_DataSetName|API> {0}, '{1}'", this.ObjectID, value);
				if (value != this._dataSetName)
				{
					if (value == null || value.Length == 0)
					{
						throw ExceptionBuilder.SetDataSetNameToEmpty();
					}
					DataTable dataTable = this.Tables[value, this.Namespace];
					if (dataTable != null && !dataTable._fNestedInDataset)
					{
						throw ExceptionBuilder.SetDataSetNameConflicting(value);
					}
					this.RaisePropertyChanging("DataSetName");
					this._dataSetName = value;
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000064B1 File Offset: 0x000046B1
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000064BC File Offset: 0x000046BC
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				return this._namespaceURI;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this._namespaceURI)
				{
					this.RaisePropertyChanging("Namespace");
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						if (dataTable._tableNamespace == null && (dataTable.NestedParentRelations.Length == 0 || (dataTable.NestedParentRelations.Length == 1 && dataTable.NestedParentRelations[0].ChildTable == dataTable)))
						{
							if (this.Tables.Contains(dataTable.TableName, value, false, true))
							{
								throw ExceptionBuilder.DuplicateTableName2(dataTable.TableName, value);
							}
							dataTable.CheckCascadingNamespaceConflict(value);
							dataTable.DoRaiseNamespaceChange();
						}
					}
					this._namespaceURI = value;
					if (string.IsNullOrEmpty(value))
					{
						this._datasetPrefix = string.Empty;
					}
				}
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataSet" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000065C0 File Offset: 0x000047C0
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000065C8 File Offset: 0x000047C8
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._datasetPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				if (value != this._datasetPrefix)
				{
					this.RaisePropertyChanging("Prefix");
					this._datasetPrefix = value;
				}
			}
		}

		/// <summary>Gets the collection of customized user information associated with the DataSet.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> with all custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006624 File Offset: 0x00004824
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

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006649 File Offset: 0x00004849
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00006654 File Offset: 0x00004854
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.set_Locale|API> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						if (!this._culture.Equals(value))
						{
							this.SetLocaleValue(value, true);
						}
						this._cultureUserSet = true;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000066B8 File Offset: 0x000048B8
		internal void SetLocaleValue(CultureInfo value, bool userSet)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CultureInfo culture = this._culture;
			bool cultureUserSet = this._cultureUserSet;
			try
			{
				this._culture = value;
				this._cultureUserSet = userSet;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (!dataTable.ShouldSerializeLocale())
					{
						dataTable.SetLocaleValue(value, false, false);
					}
				}
				flag = this.ValidateLocaleConstraint();
				if (flag)
				{
					flag = false;
					foreach (object obj2 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						num++;
						if (!dataTable2.ShouldSerializeLocale())
						{
							dataTable2.SetLocaleValue(value, false, true);
						}
					}
					flag = true;
				}
			}
			catch
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (!flag)
				{
					this._culture = culture;
					this._cultureUserSet = cultureUserSet;
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						if (!dataTable3.ShouldSerializeLocale())
						{
							dataTable3.SetLocaleValue(culture, false, false);
						}
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (!this.Tables[i].ShouldSerializeLocale())
							{
								this.Tables[i].SetLocaleValue(culture, false, true);
							}
						}
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					if (!flag2)
					{
						throw ExceptionBuilder.CannotChangeCaseLocale(null);
					}
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000068C4 File Offset: 0x00004AC4
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000068CC File Offset: 0x00004ACC
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000068D4 File Offset: 0x00004AD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Tables.Count; i++)
						{
							if (this.Tables[i].Site != null)
							{
								container.Remove(this.Tables[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		/// <summary>Get the collection of relations that link tables and allow navigation from parent tables to child tables.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains a collection of <see cref="T:System.Data.DataRelation" /> objects. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000693A File Offset: 0x00004B3A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this._relationCollection;
			}
		}

		/// <summary>Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />. An empty collection is returned if no <see cref="T:System.Data.DataTable" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00006942 File Offset: 0x00004B42
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableCollection Tables
		{
			get
			{
				return this._tableCollection;
			}
		}

		/// <summary>Clears the <see cref="T:System.Data.DataSet" /> of any data by removing all rows in all tables.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001EF RID: 495 RVA: 0x0000694C File Offset: 0x00004B4C
		public void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clear|API> {0}", this.ObjectID);
			try
			{
				this.OnClearFunctionCalled(null);
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].Clear();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001F0 RID: 496 RVA: 0x000069D0 File Offset: 0x00004BD0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual DataSet Clone()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clone|API> {0}", this.ObjectID);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
				if (dataSet.Tables.Count > 0)
				{
					dataSet.Reset();
				}
				dataSet.DataSetName = this.DataSetName;
				dataSet.CaseSensitive = this.CaseSensitive;
				dataSet._culture = this._culture;
				dataSet._cultureUserSet = this._cultureUserSet;
				dataSet.EnforceConstraints = this.EnforceConstraints;
				dataSet.Namespace = this.Namespace;
				dataSet.Prefix = this.Prefix;
				dataSet.RemotingFormat = this.RemotingFormat;
				dataSet._fIsSchemaLoading = true;
				DataTableCollection tables = this.Tables;
				for (int i = 0; i < tables.Count; i++)
				{
					DataTable dataTable = tables[i].Clone(dataSet);
					dataTable._tableNamespace = tables[i].Namespace;
					dataSet.Tables.Add(dataTable);
				}
				for (int j = 0; j < tables.Count; j++)
				{
					ConstraintCollection constraints = tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (!(constraints[k] is UniqueConstraint))
						{
							ForeignKeyConstraint foreignKeyConstraint = constraints[k] as ForeignKeyConstraint;
							if (foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable)
							{
								dataSet.Tables[j].Constraints.Add(constraints[k].Clone(dataSet));
							}
						}
					}
				}
				DataRelationCollection relations = this.Relations;
				for (int l = 0; l < relations.Count; l++)
				{
					DataRelation dataRelation = relations[l].Clone(dataSet);
					dataRelation.CheckMultipleNested = false;
					dataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
				if (this._extendedProperties != null)
				{
					foreach (object obj in this._extendedProperties.Keys)
					{
						dataSet.ExtendedProperties[obj] = this._extendedProperties[obj];
					}
				}
				foreach (object obj2 in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj2;
					foreach (object obj3 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj3;
						if (dataColumn.Expression.Length != 0)
						{
							dataSet.Tables[dataTable2.TableName, dataTable2.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
						}
					}
				}
				for (int m = 0; m < tables.Count; m++)
				{
					dataSet.Tables[m]._tableNamespace = tables[m]._tableNamespace;
				}
				dataSet._fIsSchemaLoading = false;
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006D84 File Offset: 0x00004F84
		internal int EstimatedXmlStringSize()
		{
			int num = 100;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				int num2 = this.Tables[i].TableName.Length + 4 << 2;
				DataTable dataTable = this.Tables[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					num2 += dataTable.Columns[j].ColumnName.Length + 4 << 2;
					num2 += 20;
				}
				num += dataTable.Rows.Count * num2;
			}
			return num;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</returns>
		// Token: 0x060001F2 RID: 498 RVA: 0x00006E23 File Offset: 0x00005023
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006E2C File Offset: 0x0000502C
		internal string GetRemotingDiffGram(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				new NewDiffgramGen(table, false).Save(xmlTextWriter, table);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006E68 File Offset: 0x00005068
		internal string GetXmlSchemaForRemoting(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				if (table == null)
				{
					if (this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
					{
						new XmlTreeGen(SchemaFormat.RemotingSkipSchema).Save(this, xmlTextWriter);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Remoting).Save(this, xmlTextWriter);
					}
				}
				else
				{
					new XmlTreeGen(SchemaFormat.Remoting).Save(table, xmlTextWriter);
				}
			}
			return stringWriter.ToString();
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F5 RID: 501 RVA: 0x00006ECC File Offset: 0x000050CC
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006ED8 File Offset: 0x000050D8
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXmlSchema|INFO> {0}, reader, denyResolving={1}", this.ObjectID, denyResolving);
			try
			{
				int num2 = -1;
				if (reader != null)
				{
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						num2 = reader.Depth;
					}
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
						{
							this.ReadXDRSchema(reader);
						}
						else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
						{
							this.ReadXSDSchema(reader, denyResolving);
						}
						else
						{
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								XmlNode xmlNode = xmlDocument.ReadNode(reader);
								xmlElement.AppendChild(xmlNode);
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007164 File Offset: 0x00005364
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000719C File Offset: 0x0000539C
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000071C2 File Offset: 0x000053C2
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000071F8 File Offset: 0x000053F8
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			int num = 1;
			if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema" && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("schemafragmentcount", "urn:schemas-microsoft-com:xml-msdata");
				if (!string.IsNullOrEmpty(attribute))
				{
					num = int.Parse(attribute, null);
				}
			}
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema xmlSchema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(xmlSchema);
				this.ReadEndElement(reader);
				if (--num > 0)
				{
					DataSet.MoveToElement(reader);
				}
				while (reader.NodeType == XmlNodeType.Whitespace)
				{
					reader.Skip();
				}
			}
			xmlSchemaSet.Compile();
			new XSDSchema().LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000072CC File Offset: 0x000054CC
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007314 File Offset: 0x00005514
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaFormat>("<ds.DataSet.WriteXmlSchema|INFO> {0}, schemaFormat={1}", this.ObjectID, schemaFormat);
			try
			{
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen;
					if (schemaFormat == SchemaFormat.WebService && this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema && writer.WriteState == WriteState.Element)
					{
						xmlTreeGen = new XmlTreeGen(SchemaFormat.WebServiceSkipSchema);
					}
					else
					{
						xmlTreeGen = new XmlTreeGen(schemaFormat);
					}
					xmlTreeGen.Save(this, null, writer, false, multipleTargetConverter);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001FD RID: 509 RVA: 0x0000738C File Offset: 0x0000558C
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007398 File Offset: 0x00005598
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			IDisposable disposable = null;
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXml|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = -1;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					bool flag5 = false;
					bool flag6 = false;
					dsrowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						xmlReadMode2 = xmlReadMode;
					}
					else
					{
						if (this.Tables.Count == 0)
						{
							flag5 = true;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
							{
								this.ReadXmlDiffgram(reader);
								this.ReadEndElement(reader);
								return XmlReadMode.DiffGram;
							}
							if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
							{
								this.ReadXDRSchema(reader);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
							{
								this.ReadXSDSchema(reader, denyResolving);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							string value = reader.Value;
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									this.ReadXmlDiffgram(reader);
									xmlReadMode = XmlReadMode.DiffGram;
								}
								if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									flag2 = true;
									flag4 = true;
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									flag2 = true;
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
									{
										this.ReadXmlDiffgram(reader);
										flag3 = true;
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										while (!reader.EOF && reader.NodeType == XmlNodeType.Whitespace)
										{
											reader.Read();
										}
										if (reader.NodeType == XmlNodeType.Element)
										{
											flag = true;
											if (!flag2 && this.Tables.Count == 0)
											{
												XmlNode xmlNode = xmlDocument.ReadNode(reader);
												xmlElement.AppendChild(xmlNode);
											}
											else
											{
												if (xmlDataLoader == null)
												{
													xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
												}
												xmlDataLoader.LoadData(reader);
												flag6 = true;
												if (flag2)
												{
													xmlReadMode = XmlReadMode.ReadSchema;
												}
												else
												{
													xmlReadMode = XmlReadMode.IgnoreSchema;
												}
											}
										}
									}
								}
							}
							this.ReadEndElement(reader);
							bool flag7 = false;
							bool fTopLevelTable = this._fTopLevelTable;
							if (!flag2 && this.Tables.Count == 0 && !xmlElement.HasChildNodes)
							{
								this._fTopLevelTable = true;
								flag7 = true;
								if (value != null && value.Length > 0)
								{
									xmlElement.InnerText = value;
								}
							}
							if (!flag5 && value != null && value.Length > 0)
							{
								xmlElement.InnerText = value;
							}
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
							}
							if (!flag5 && !flag6)
							{
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement.ChildNodes.Count == 0 || (documentElement.ChildNodes.Count == 1 && documentElement.FirstChild.GetType() == typeof(XmlText)))
								{
									bool fTopLevelTable2 = this._fTopLevelTable;
									if (this.DataSetName != documentElement.Name && this._namespaceURI != documentElement.NamespaceURI && this.Tables.Contains(documentElement.Name, (documentElement.NamespaceURI.Length == 0) ? null : documentElement.NamespaceURI, false, true))
									{
										this._fTopLevelTable = true;
									}
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										this._fTopLevelTable = fTopLevelTable2;
									}
								}
							}
							if (!flag3)
							{
								if (!flag2 && this.Tables.Count == 0)
								{
									this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
									xmlReadMode = XmlReadMode.InferSchema;
									xmlDataLoader.FromInference = true;
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										xmlDataLoader.FromInference = false;
									}
								}
								if (flag7)
								{
									this._fTopLevelTable = fTopLevelTable;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
					}
				}
				finally
				{
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007968 File Offset: 0x00005B68
		internal void InferSchema(XmlDocument xdoc, string[] excludedNamespaces, XmlReadMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode>("<ds.DataSet.InferSchema|INFO> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				string namespaceURI = xdoc.DocumentElement.NamespaceURI;
				if (excludedNamespaces == null)
				{
					excludedNamespaces = Array.Empty<string>();
				}
				XmlNodeReader xmlNodeReader = new XmlIgnoreNamespaceReader(xdoc, excludedNamespaces);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaInference
				{
					Occurrence = XmlSchemaInference.InferenceOption.Relaxed,
					TypeInference = ((mode == XmlReadMode.InferTypedSchema) ? XmlSchemaInference.InferenceOption.Restricted : XmlSchemaInference.InferenceOption.Relaxed)
				}.InferSchema(xmlNodeReader);
				xmlSchemaSet.Compile();
				XSDSchema xsdschema = new XSDSchema();
				xsdschema.FromInference = true;
				try
				{
					xsdschema.LoadSchema(xmlSchemaSet, this);
				}
				finally
				{
					xsdschema.FromInference = false;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007A18 File Offset: 0x00005C18
		private bool IsEmpty()
		{
			using (IEnumerator enumerator = this.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Rows.Count > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007A80 File Offset: 0x00005C80
		private void ReadXmlDiffgram(XmlReader reader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ReadXmlDiffgram|INFO> {0}", this.ObjectID);
			try
			{
				int depth = reader.Depth;
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				bool flag = this.IsEmpty();
				DataSet dataSet;
				if (flag)
				{
					dataSet = this;
				}
				else
				{
					dataSet = this.Clone();
					dataSet.EnforceConstraints = false;
				}
				foreach (object obj in dataSet.Tables)
				{
					((DataTable)obj).Rows._nullInList = 0;
				}
				reader.MoveToContent();
				if (!(reader.LocalName != "diffgram") || !(reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					reader.Read();
					if (reader.NodeType == XmlNodeType.Whitespace)
					{
						this.MoveToElement(reader, reader.Depth - 1);
					}
					dataSet._fInLoadDiffgram = true;
					if (reader.Depth > depth)
					{
						if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
						{
							XmlElement xmlElement = new XmlDocument().CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							reader.Read();
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
							if (reader.Depth - 1 > depth)
							{
								new XmlDataLoader(dataSet, false, xmlElement, false)
								{
									_isDiffgram = true
								}.LoadData(reader);
							}
							this.ReadEndElement(reader);
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
						}
						if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
						{
							new XMLDiffLoader().LoadDiffGram(dataSet, reader);
						}
						while (reader.Depth > depth)
						{
							reader.Read();
						}
						this.ReadEndElement(reader);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						if (dataTable.Rows._nullInList > 0)
						{
							throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
						}
					}
					dataSet._fInLoadDiffgram = false;
					foreach (object obj3 in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj3;
						DataRelation[] nestedParentRelations = dataTable2.NestedParentRelations;
						DataRelation[] array = nestedParentRelations;
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ParentTable == dataTable2)
							{
								foreach (object obj4 in dataTable2.Rows)
								{
									DataRow dataRow = (DataRow)obj4;
									foreach (DataRelation dataRelation in nestedParentRelations)
									{
										dataRow.CheckForLoops(dataRelation);
									}
								}
							}
						}
					}
					if (!flag)
					{
						this.Merge(dataSet);
						if (this._dataSetName == "NewDataSet")
						{
							this._dataSetName = dataSet._dataSetName;
						}
						dataSet.EnforceConstraints = enforceConstraints;
					}
					this.EnforceConstraints = enforceConstraints;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000202 RID: 514 RVA: 0x00007E94 File Offset: 0x00006094
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007EA0 File Offset: 0x000060A0
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			IDisposable disposable = null;
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode, bool>("<ds.DataSet.ReadXml|INFO> {0}, mode={1}, denyResolving={2}", this.ObjectID, mode, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				XmlReadMode xmlReadMode = mode;
				if (reader == null)
				{
					xmlReadMode2 = xmlReadMode;
				}
				else if (mode == XmlReadMode.Auto)
				{
					xmlReadMode2 = this.ReadXml(reader);
				}
				else
				{
					DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
					try
					{
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						int num2 = -1;
						dsrowDiffIdUsageSection.Prepare(this);
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						reader.MoveToContent();
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										flag3 = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
										{
											XmlNode xmlNode = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(xmlNode);
										}
										else
										{
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, flag3, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag3, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
							{
								this.InferSchema(xmlDocument, null, mode);
								xmlReadMode = XmlReadMode.InferSchema;
								xmlDataLoader.FromInference = true;
								try
								{
									xmlDataLoader.LoadData(xmlDocument);
								}
								finally
								{
									xmlDataLoader.FromInference = false;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
					}
					finally
					{
					}
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000204 RID: 516 RVA: 0x00008354 File Offset: 0x00006554
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlWriteMode>("<ds.DataSet.WriteXml|API> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(writer);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(writer, mode == XmlWriteMode.WriteSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enabled. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000205 RID: 517 RVA: 0x000083BC File Offset: 0x000065BC
		public void Merge(DataSet dataSet)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			try
			{
				this.Merge(dataSet, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema with the current DataSet, preserving or discarding changes in the current DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000206 RID: 518 RVA: 0x00008414 File Offset: 0x00006614
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.ArgumentNull("dataSet");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeDataSet(dataSet);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Raises the <see cref="M:System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000207 RID: 519 RVA: 0x0000848C File Offset: 0x0000668C
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000084A0 File Offset: 0x000066A0
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.MergeFailed != null)
			{
				this.MergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000084C3 File Offset: 0x000066C3
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			this.OnMergeFailed(new MergeFailedEventArgs(table, conflict));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000084DD File Offset: 0x000066DD
		internal void OnDataRowCreated(DataRow row)
		{
			DataRowCreatedEventHandler dataRowCreated = this.DataRowCreated;
			if (dataRowCreated == null)
			{
				return;
			}
			dataRowCreated(this, row);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000084F1 File Offset: 0x000066F1
		internal void OnClearFunctionCalled(DataTable table)
		{
			DataSetClearEventhandler clearFunctionCalled = this.ClearFunctionCalled;
			if (clearFunctionCalled == null)
			{
				return;
			}
			clearFunctionCalled(this, table);
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is removed from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> being removed. </param>
		// Token: 0x0600020C RID: 524 RVA: 0x00003FD2 File Offset: 0x000021D2
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008508 File Offset: 0x00006708
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager defaultViewManager = this._defaultViewManager;
			if (defaultViewManager != null)
			{
				defaultViewManager.DataViewSettings.Remove(table);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRelation" /> object is removed from a <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> being removed. </param>
		// Token: 0x0600020E RID: 526 RVA: 0x00003FD2 File Offset: 0x000021D2
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000852B File Offset: 0x0000672B
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		/// <summary>Sends a notification that the specified <see cref="T:System.Data.DataSet" /> property is about to change.</summary>
		/// <param name="name">The name of the property that is about to change. </param>
		// Token: 0x06000210 RID: 528 RVA: 0x00008534 File Offset: 0x00006734
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008542 File Offset: 0x00006742
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000854C File Offset: 0x0000674C
		internal DataTable[] TopLevelTables(bool forSchema)
		{
			List<DataTable> list = new List<DataTable>();
			if (forSchema)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					if (dataTable.NestedParentsCount > 1 || dataTable.SelfNested)
					{
						list.Add(dataTable);
					}
				}
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				DataTable dataTable2 = this.Tables[j];
				if (dataTable2.NestedParentsCount == 0 && !list.Contains(dataTable2))
				{
					list.Add(dataTable2);
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return Array.Empty<DataTable>();
		}

		/// <summary>Clears all tables and removes all relations, foreign constraints, and tables from the <see cref="T:System.Data.DataSet" />. Subclasses should override <see cref="M:System.Data.DataSet.Reset" /> to restore a <see cref="T:System.Data.DataSet" /> to its original state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000213 RID: 531 RVA: 0x000085F0 File Offset: 0x000067F0
		public virtual void Reset()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Reset|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					ConstraintCollection constraints = this.Tables[i].Constraints;
					int j = 0;
					while (j < constraints.Count)
					{
						if (constraints[j] is ForeignKeyConstraint)
						{
							constraints.Remove(constraints[j]);
						}
						else
						{
							j++;
						}
					}
				}
				this.Clear();
				this.Relations.Clear();
				this.Tables.Clear();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000086A4 File Offset: 0x000068A4
		internal bool ValidateCaseConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateCaseConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.CaseSensitive != dataRelation.ParentTable.CaseSensitive)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.CaseSensitive != foreignKeyConstraint.RelatedTable.CaseSensitive)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000087B4 File Offset: 0x000069B4
		internal bool ValidateLocaleConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateLocaleConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.Locale.LCID != dataRelation.ParentTable.Locale.LCID)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.Locale.LCID != foreignKeyConstraint.RelatedTable.Locale.LCID)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000088DC File Offset: 0x00006ADC
		internal DataTable FindTable(DataTable baseTable, PropertyDescriptor[] props, int propStart)
		{
			if (props.Length < propStart + 1)
			{
				return baseTable;
			}
			PropertyDescriptor propertyDescriptor = props[propStart];
			if (baseTable == null)
			{
				if (propertyDescriptor is DataTablePropertyDescriptor)
				{
					return this.FindTable(((DataTablePropertyDescriptor)propertyDescriptor).Table, props, propStart + 1);
				}
				return null;
			}
			else
			{
				if (propertyDescriptor is DataRelationPropertyDescriptor)
				{
					return this.FindTable(((DataRelationPropertyDescriptor)propertyDescriptor).Relation.ChildTable, props, propStart + 1);
				}
				return null;
			}
		}

		/// <summary>Ignores attributes and returns an empty DataSet.</summary>
		/// <param name="reader">The specified XML reader. </param>
		// Token: 0x06000217 RID: 535 RVA: 0x00008940 File Offset: 0x00006B40
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this._useDataSetSchemaOnly = false;
			this._udtIsWrapped = false;
			if (reader.HasAttributes)
			{
				if (reader.MoveToAttribute("xsi:nil") && string.Equals(reader.GetAttribute("xsi:nil"), "true", StringComparison.Ordinal))
				{
					this.MoveToElement(reader, 1);
					return;
				}
				if (reader.MoveToAttribute("msdata:UseDataSetSchemaOnly"))
				{
					string attribute = reader.GetAttribute("msdata:UseDataSetSchemaOnly");
					if (string.Equals(attribute, "true", StringComparison.Ordinal) || string.Equals(attribute, "1", StringComparison.Ordinal))
					{
						this._useDataSetSchemaOnly = true;
					}
					else if (!string.Equals(attribute, "false", StringComparison.Ordinal) && !string.Equals(attribute, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UseDataSetSchemaOnly", attribute);
					}
				}
				if (reader.MoveToAttribute("msdata:UDTColumnValueWrapped"))
				{
					string attribute2 = reader.GetAttribute("msdata:UDTColumnValueWrapped");
					if (string.Equals(attribute2, "true", StringComparison.Ordinal) || string.Equals(attribute2, "1", StringComparison.Ordinal))
					{
						this._udtIsWrapped = true;
					}
					else if (!string.Equals(attribute2, "false", StringComparison.Ordinal) && !string.Equals(attribute2, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UDTColumnValueWrapped", attribute2);
					}
				}
			}
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		/// <summary>Gets a copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" /> for the DataSet.</summary>
		/// <returns>A copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">The specified schema set. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000218 RID: 536 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			if (DataSet.s_schemaTypeForWSDL == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaSequence.MaxOccurs = decimal.MaxValue;
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				DataSet.s_schemaTypeForWSDL = xmlSchemaComplexType;
			}
			return DataSet.s_schemaTypeForWSDL;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</returns>
		// Token: 0x06000219 RID: 537 RVA: 0x00008B10 File Offset: 0x00006D10
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x0600021A RID: 538 RVA: 0x00008B68 File Offset: 0x00006D68
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool flag = true;
			XmlTextReader xmlTextReader = null;
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			else
			{
				xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					flag = xmlTextReader.Normalization;
					xmlTextReader.Normalization = false;
				}
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
				return;
			}
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = flag;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x0600021B RID: 539 RVA: 0x00008BC7 File Offset: 0x00006DC7
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService, null);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008BDA File Offset: 0x00006DDA
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00008BE2 File Offset: 0x00006DE2
		internal string MainTableName
		{
			get
			{
				return this._mainTableName;
			}
			set
			{
				this._mainTableName = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008BEB File Offset: 0x00006DEB
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000033 RID: 51
		private DataViewManager _defaultViewManager;

		// Token: 0x04000034 RID: 52
		private readonly DataTableCollection _tableCollection;

		// Token: 0x04000035 RID: 53
		private readonly DataRelationCollection _relationCollection;

		// Token: 0x04000036 RID: 54
		internal PropertyCollection _extendedProperties;

		// Token: 0x04000037 RID: 55
		private string _dataSetName = "NewDataSet";

		// Token: 0x04000038 RID: 56
		private string _datasetPrefix = string.Empty;

		// Token: 0x04000039 RID: 57
		internal string _namespaceURI = string.Empty;

		// Token: 0x0400003A RID: 58
		private bool _enforceConstraints = true;

		// Token: 0x0400003B RID: 59
		private bool _caseSensitive;

		// Token: 0x0400003C RID: 60
		private CultureInfo _culture;

		// Token: 0x0400003D RID: 61
		private bool _cultureUserSet;

		// Token: 0x0400003E RID: 62
		internal bool _fInReadXml;

		// Token: 0x0400003F RID: 63
		internal bool _fInLoadDiffgram;

		// Token: 0x04000040 RID: 64
		internal bool _fTopLevelTable;

		// Token: 0x04000041 RID: 65
		internal bool _fInitInProgress;

		// Token: 0x04000042 RID: 66
		internal bool _fEnableCascading = true;

		// Token: 0x04000043 RID: 67
		internal bool _fIsSchemaLoading;

		// Token: 0x04000044 RID: 68
		internal string _mainTableName = string.Empty;

		// Token: 0x04000045 RID: 69
		private SerializationFormat _remotingFormat;

		// Token: 0x04000046 RID: 70
		private object _defaultViewManagerLock = new object();

		// Token: 0x04000047 RID: 71
		private static int s_objectTypeCount;

		// Token: 0x04000048 RID: 72
		private readonly int _objectID = Interlocked.Increment(ref DataSet.s_objectTypeCount);

		// Token: 0x04000049 RID: 73
		private static XmlSchemaComplexType s_schemaTypeForWSDL;

		// Token: 0x0400004A RID: 74
		internal bool _useDataSetSchemaOnly;

		// Token: 0x0400004B RID: 75
		internal bool _udtIsWrapped;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanging;

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		private MergeFailedEventHandler MergeFailed;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private DataRowCreatedEventHandler DataRowCreated;

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		private DataSetClearEventhandler ClearFunctionCalled;
	}
}
