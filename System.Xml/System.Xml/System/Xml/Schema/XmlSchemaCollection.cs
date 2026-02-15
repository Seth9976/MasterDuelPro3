using System;
using System.Collections;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) and XML-Data Reduced (XDR) schemas. The <see cref="T:System.Xml.Schema.XmlSchemaCollection" /> class class is obsolete. Use <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instead.</summary>
	// Token: 0x020002BF RID: 703
	[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. https://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XmlSchemaCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the XmlSchemaCollection class with the specified <see cref="T:System.Xml.XmlNameTable" />. The XmlNameTable is used when loading schemas.</summary>
		/// <param name="nametable">The XmlNameTable to use. </param>
		// Token: 0x06002049 RID: 8265 RVA: 0x000BE7BC File Offset: 0x000BC9BC
		public XmlSchemaCollection(XmlNameTable nametable)
		{
			if (nametable == null)
			{
				throw new ArgumentNullException("nametable");
			}
			this.nameTable = nametable;
			this.collection = Hashtable.Synchronized(new Hashtable());
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.isThreadSafe = true;
			if (this.isThreadSafe)
			{
				this.wLock = new ReaderWriterLock();
			}
		}

		/// <summary>Gets the number of namespaces defined in this collection.</summary>
		/// <returns>The number of namespaces defined in this collection.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x000BE827 File Offset: 0x000BCA27
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		/// <summary>Gets the default XmlNameTable used by the XmlSchemaCollection when loading new schemas.</summary>
		/// <returns>An XmlNameTable.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x000BE834 File Offset: 0x000BCA34
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x000BE83C File Offset: 0x000BCA3C
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchema" /> associated with the given namespace URI.</summary>
		/// <returns>The XmlSchema associated with the namespace URI; null if there is no loaded schema associated with the given namespace or if the namespace is associated with an XDR schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema you want to return. This will typically be the targetNamespace of the schema. </param>
		// Token: 0x170007AD RID: 1965
		public XmlSchema this[string ns]
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
				if (xmlSchemaCollectionNode == null)
				{
					return null;
				}
				return xmlSchemaCollectionNode.Schema;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.GetEnumerator" />.</summary>
		/// <returns>Returns the <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x0600204E RID: 8270 RVA: 0x000BE87C File Offset: 0x000BCA7C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		/// <summary>Provides support for the "for each" style iteration over the collection of schemas.</summary>
		/// <returns>An enumerator for iterating over all schemas in the current collection.</returns>
		// Token: 0x0600204F RID: 8271 RVA: 0x000BE87C File Offset: 0x000BCA7C
		public XmlSchemaCollectionEnumerator GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.CopyTo(System.Xml.Schema.XmlSchema[],System.Int32)" />.</summary>
		/// <param name="array">The array to copy the objects to. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying will begin. </param>
		// Token: 0x06002050 RID: 8272 RVA: 0x000BE88C File Offset: 0x000BCA8C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XmlSchemaCollectionEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (index == array.Length && array.IsFixedSize)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				array.SetValue(enumerator.Current, index++);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>Returns true if the collection is synchronized, otherwise false.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Returns a <see cref="T:System.Collections.ICollection.SyncRoot" /> object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x00035F33 File Offset: 0x00034133
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.Count" />.</summary>
		/// <returns>Returns the count of the items in the collection.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000BE827 File Offset: 0x000BCA27
		int ICollection.Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000BE8F8 File Offset: 0x000BCAF8
		internal SchemaInfo GetSchemaInfo(string ns)
		{
			XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
			if (xmlSchemaCollectionNode == null)
			{
				return null;
			}
			return xmlSchemaCollectionNode.SchemaInfo;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000BE92C File Offset: 0x000BCB2C
		internal SchemaNames GetSchemaNames(XmlNameTable nt)
		{
			if (this.nameTable != nt)
			{
				return new SchemaNames(nt);
			}
			if (this.schemaNames == null)
			{
				this.schemaNames = new SchemaNames(this.nameTable);
			}
			return this.schemaNames;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000BE95D File Offset: 0x000BCB5D
		internal XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile)
		{
			return this.Add(ns, schemaInfo, schema, compile, this.xmlResolver);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000BE970 File Offset: 0x000BCB70
		private XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile, XmlResolver resolver)
		{
			int num = 0;
			if (schema != null)
			{
				if (schema.ErrorCount == 0 && compile)
				{
					if (!schema.CompileSchema(this, resolver, schemaInfo, ns, this.validationEventHandler, this.nameTable, true))
					{
						num = 1;
					}
					ns = ((schema.TargetNamespace == null) ? string.Empty : schema.TargetNamespace);
				}
				num += schema.ErrorCount;
			}
			else
			{
				num += schemaInfo.ErrorCount;
				ns = this.NameTable.Add(ns);
			}
			if (num == 0)
			{
				this.Add(ns, new XmlSchemaCollectionNode
				{
					NamespaceURI = ns,
					SchemaInfo = schemaInfo,
					Schema = schema
				});
				return schema;
			}
			return null;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000BEA10 File Offset: 0x000BCC10
		private void Add(string ns, XmlSchemaCollectionNode node)
		{
			if (this.isThreadSafe)
			{
				this.wLock.AcquireWriterLock(this.timeout);
			}
			try
			{
				if (this.collection[ns] != null)
				{
					this.collection.Remove(ns);
				}
				this.collection.Add(ns, node);
			}
			finally
			{
				if (this.isThreadSafe)
				{
					this.wLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000BEA84 File Offset: 0x000BCC84
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000BEA8C File Offset: 0x000BCC8C
		internal ValidationEventHandler EventHandler
		{
			get
			{
				return this.validationEventHandler;
			}
			set
			{
				this.validationEventHandler = value;
			}
		}

		// Token: 0x04000F17 RID: 3863
		private Hashtable collection;

		// Token: 0x04000F18 RID: 3864
		private XmlNameTable nameTable;

		// Token: 0x04000F19 RID: 3865
		private SchemaNames schemaNames;

		// Token: 0x04000F1A RID: 3866
		private ReaderWriterLock wLock;

		// Token: 0x04000F1B RID: 3867
		private int timeout = -1;

		// Token: 0x04000F1C RID: 3868
		private bool isThreadSafe = true;

		// Token: 0x04000F1D RID: 3869
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04000F1E RID: 3870
		private XmlResolver xmlResolver;
	}
}
