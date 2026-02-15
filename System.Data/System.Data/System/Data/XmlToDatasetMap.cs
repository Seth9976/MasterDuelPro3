using System;
using System.Collections;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000B0 RID: 176
	internal sealed class XmlToDatasetMap
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00030900 File Offset: 0x0002EB00
		public XmlToDatasetMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataSet, nameTable);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00030910 File Offset: 0x0002EB10
		public XmlToDatasetMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.BuildIdentityMap(nameTable, dataSet);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00030920 File Offset: 0x0002EB20
		public XmlToDatasetMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataTable, nameTable);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00030930 File Offset: 0x0002EB30
		public XmlToDatasetMap(XmlNameTable nameTable, DataTable dataTable)
		{
			this.BuildIdentityMap(nameTable, dataTable);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00030940 File Offset: 0x0002EB40
		internal static bool IsMappedColumn(DataColumn c)
		{
			return c.ColumnMapping != MappingType.Hidden;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00030950 File Offset: 0x0002EB50
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(DataTable table, XmlNameTable nameTable)
		{
			string text = nameTable.Get(table.EncodedTableName);
			string text2 = nameTable.Get(table.Namespace);
			if (text == null)
			{
				return null;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00030998 File Offset: 0x0002EB98
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(XmlNameTable nameTable, DataTable table)
		{
			string encodedTableName = table.EncodedTableName;
			string text = nameTable.Get(encodedTableName);
			if (text == null)
			{
				text = nameTable.Add(encodedTableName);
			}
			table._encodedTableName = text;
			string text2 = nameTable.Get(table.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(table.Namespace);
			}
			else if (table._tableNamespace != null)
			{
				table._tableNamespace = text2;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00030A10 File Offset: 0x0002EC10
		private bool AddColumnSchema(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = nameTable.Get(col.EncodedColumnName);
			string text2 = nameTable.Get(col.Namespace);
			if (text == null)
			{
				return false;
			}
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[xmlNodeIdentety] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00030A6C File Offset: 0x0002EC6C
		private bool AddColumnSchema(XmlNameTable nameTable, DataColumn col, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = XmlConvert.EncodeLocalName(col.ColumnName);
			string text2 = nameTable.Get(text);
			if (text2 == null)
			{
				text2 = nameTable.Add(text);
			}
			col._encodedColumnName = text2;
			string text3 = nameTable.Get(col.Namespace);
			if (text3 == null)
			{
				text3 = nameTable.Add(col.Namespace);
			}
			else if (col._columnUri != null)
			{
				col._columnUri = text3;
			}
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
			columns[xmlNodeIdentety] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00030AFC File Offset: 0x0002ECFC
		private void BuildIdentityMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00030BD4 File Offset: 0x0002EDD4
		private void BuildIdentityMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			string text = nameTable.Get(dataSet.Namespace);
			if (text == null)
			{
				text = nameTable.Add(dataSet.Namespace);
			}
			dataSet._namespaceURI = text;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string text2 = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text3 = nameTable.Get(text2);
							if (text3 == null)
							{
								text3 = nameTable.Add(text2);
							}
							string text4 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text4 == null)
							{
								text4 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text3, text4);
							tableSchemaInfo.ColumnsSchemaMap[xmlNodeIdentety] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00030DCC File Offset: 0x0002EFCC
		private void BuildIdentityMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(1);
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
			if (tableSchemaInfo != null)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (XmlToDatasetMap.IsMappedColumn(dataColumn))
					{
						this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
					}
				}
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00030E50 File Offset: 0x0002F050
		private void BuildIdentityMap(XmlNameTable nameTable, DataTable dataTable)
		{
			ArrayList selfAndDescendants = this.GetSelfAndDescendants(dataTable);
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(selfAndDescendants.Count);
			foreach (object obj in selfAndDescendants)
			{
				DataTable dataTable2 = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable2);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable2.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string text = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text2 = nameTable.Get(text);
							if (text2 == null)
							{
								text2 = nameTable.Add(text);
							}
							string text3 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text3 == null)
							{
								text3 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
							tableSchemaInfo.ColumnsSchemaMap[xmlNodeIdentety] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00031024 File Offset: 0x0002F224
		private ArrayList GetSelfAndDescendants(DataTable dt)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(dt);
			for (int i = 0; i < arrayList.Count; i++)
			{
				foreach (object obj in ((DataTable)arrayList[i]).ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (!arrayList.Contains(dataRelation.ChildTable))
					{
						arrayList.Add(dataRelation.ChildTable);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000310C0 File Offset: 0x0002F2C0
		public object GetColumnSchema(XmlNode node, bool fIgnoreNamespace)
		{
			XmlNode xmlNode = ((node.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)node).OwnerElement : node.ParentNode);
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
			{
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[xmlNode.LocalName] : this._tableSchemaMap[xmlNode]);
				xmlNode = xmlNode.ParentNode;
				if (tableSchemaInfo != null)
				{
					if (fIgnoreNamespace)
					{
						return tableSchemaInfo.ColumnsSchemaMap[node.LocalName];
					}
					return tableSchemaInfo.ColumnsSchemaMap[node];
				}
			}
			return null;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00031150 File Offset: 0x0002F350
		public object GetColumnSchema(DataTable table, XmlReader dataReader, bool fIgnoreNamespace)
		{
			if (this._lastTableSchemaInfo == null || this._lastTableSchemaInfo.TableSchema != table)
			{
				this._lastTableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[table.EncodedTableName] : this._tableSchemaMap[table]);
			}
			if (fIgnoreNamespace)
			{
				return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader.LocalName];
			}
			return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader];
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000311CC File Offset: 0x0002F3CC
		public object GetSchemaForNode(XmlNode node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = null;
			if (node.NodeType == XmlNodeType.Element)
			{
				tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			}
			if (tableSchemaInfo != null)
			{
				return tableSchemaInfo.TableSchema;
			}
			return this.GetColumnSchema(node, fIgnoreNamespace);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00031220 File Offset: 0x0002F420
		public DataTable GetTableForNode(XmlReader node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			if (tableSchemaInfo != null)
			{
				this._lastTableSchemaInfo = tableSchemaInfo;
				return this._lastTableSchemaInfo.TableSchema;
			}
			return null;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003126C File Offset: 0x0002F46C
		private void HandleSpecialColumn(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text;
			if ('x' == col.ColumnName[0])
			{
				text = "_x0078_";
			}
			else
			{
				text = "_x0058_";
			}
			text += col.ColumnName.Substring(1);
			if (nameTable.Get(text) == null)
			{
				nameTable.Add(text);
			}
			string text2 = nameTable.Get(col.Namespace);
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[xmlNodeIdentety] = col;
		}

		// Token: 0x04000380 RID: 896
		private XmlToDatasetMap.XmlNodeIdHashtable _tableSchemaMap;

		// Token: 0x04000381 RID: 897
		private XmlToDatasetMap.TableSchemaInfo _lastTableSchemaInfo;

		// Token: 0x020000B1 RID: 177
		private sealed class XmlNodeIdentety
		{
			// Token: 0x06000891 RID: 2193 RVA: 0x000312D8 File Offset: 0x0002F4D8
			public XmlNodeIdentety(string localName, string namespaceURI)
			{
				this.LocalName = localName;
				this.NamespaceURI = namespaceURI;
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x000312EE File Offset: 0x0002F4EE
			public override int GetHashCode()
			{
				return this.LocalName.GetHashCode();
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x000312FC File Offset: 0x0002F4FC
			public override bool Equals(object obj)
			{
				XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = (XmlToDatasetMap.XmlNodeIdentety)obj;
				return string.Equals(this.LocalName, xmlNodeIdentety.LocalName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.NamespaceURI, xmlNodeIdentety.NamespaceURI, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000382 RID: 898
			public string LocalName;

			// Token: 0x04000383 RID: 899
			public string NamespaceURI;
		}

		// Token: 0x020000B2 RID: 178
		internal sealed class XmlNodeIdHashtable : Hashtable
		{
			// Token: 0x06000894 RID: 2196 RVA: 0x00031338 File Offset: 0x0002F538
			public XmlNodeIdHashtable(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x17000149 RID: 329
			public object this[XmlNode node]
			{
				get
				{
					this._id.LocalName = node.LocalName;
					this._id.NamespaceURI = node.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x1700014A RID: 330
			public object this[XmlReader dataReader]
			{
				get
				{
					this._id.LocalName = dataReader.LocalName;
					this._id.NamespaceURI = dataReader.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x1700014B RID: 331
			public object this[DataTable table]
			{
				get
				{
					this._id.LocalName = table.EncodedTableName;
					this._id.NamespaceURI = table.Namespace;
					return this[this._id];
				}
			}

			// Token: 0x1700014C RID: 332
			public object this[string name]
			{
				get
				{
					this._id.LocalName = name;
					this._id.NamespaceURI = string.Empty;
					return this[this._id];
				}
			}

			// Token: 0x04000384 RID: 900
			private XmlToDatasetMap.XmlNodeIdentety _id = new XmlToDatasetMap.XmlNodeIdentety(string.Empty, string.Empty);
		}

		// Token: 0x020000B3 RID: 179
		private sealed class TableSchemaInfo
		{
			// Token: 0x06000899 RID: 2201 RVA: 0x00031410 File Offset: 0x0002F610
			public TableSchemaInfo(DataTable tableSchema)
			{
				this.TableSchema = tableSchema;
				this.ColumnsSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(tableSchema.Columns.Count);
			}

			// Token: 0x04000385 RID: 901
			public DataTable TableSchema;

			// Token: 0x04000386 RID: 902
			public XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;
		}
	}
}
