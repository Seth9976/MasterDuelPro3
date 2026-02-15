using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200015B RID: 347
	internal class SchemaObjectCache
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00053028 File Offset: 0x00051228
		private Hashtable Graph
		{
			get
			{
				if (this.graph == null)
				{
					this.graph = new Hashtable();
				}
				return this.graph;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00053043 File Offset: 0x00051243
		private Hashtable Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = new Hashtable();
				}
				return this.hash;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0005305E File Offset: 0x0005125E
		private Hashtable ObjectCache
		{
			get
			{
				if (this.objectCache == null)
				{
					this.objectCache = new Hashtable();
				}
				return this.objectCache;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00053079 File Offset: 0x00051279
		internal StringCollection Warnings
		{
			get
			{
				if (this.warnings == null)
				{
					this.warnings = new StringCollection();
				}
				return this.warnings;
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00053094 File Offset: 0x00051294
		internal XmlSchemaObject AddItem(XmlSchemaObject item, XmlQualifiedName qname, XmlSchemas schemas)
		{
			if (item == null)
			{
				return null;
			}
			if (qname == null || qname.IsEmpty)
			{
				return null;
			}
			string text = item.GetType().Name + ":" + qname.ToString();
			ArrayList arrayList = (ArrayList)this.ObjectCache[text];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.ObjectCache[text] = arrayList;
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[i];
				if (xmlSchemaObject == item)
				{
					return xmlSchemaObject;
				}
				if (this.Match(xmlSchemaObject, item, true))
				{
					return xmlSchemaObject;
				}
				this.Warnings.Add(Res.GetString("Warning: Cannot share {0} named '{1}' from '{2}' namespace. Several mismatched schema declarations were found.", new object[]
				{
					item.GetType().Name,
					qname.Name,
					qname.Namespace
				}));
				this.Warnings.Add("DEBUG:Cached item key:\r\n" + (string)this.looks[xmlSchemaObject] + "\r\nnew item key:\r\n" + (string)this.looks[item]);
			}
			arrayList.Add(item);
			return item;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000531B8 File Offset: 0x000513B8
		internal bool Match(XmlSchemaObject o1, XmlSchemaObject o2, bool shareTypes)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1.GetType() != o2.GetType())
			{
				return false;
			}
			if (this.Hash[o1] == null)
			{
				this.Hash[o1] = this.GetHash(o1);
			}
			int num = (int)this.Hash[o1];
			int num2 = this.GetHash(o2);
			return num == num2 && (!shareTypes || this.CompositeHash(o1, num) == this.CompositeHash(o2, num2));
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00053240 File Offset: 0x00051440
		private ArrayList GetDependencies(XmlSchemaObject o, ArrayList deps, Hashtable refs)
		{
			if (refs[o] == null)
			{
				refs[o] = o;
				deps.Add(o);
				ArrayList arrayList = this.Graph[o] as ArrayList;
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.GetDependencies((XmlSchemaObject)arrayList[i], deps, refs);
					}
				}
			}
			return deps;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000532A4 File Offset: 0x000514A4
		private int CompositeHash(XmlSchemaObject o, int hash)
		{
			ArrayList dependencies = this.GetDependencies(o, new ArrayList(), new Hashtable());
			double num = 0.0;
			for (int i = 0; i < dependencies.Count; i++)
			{
				object obj = this.Hash[dependencies[i]];
				if (obj is int)
				{
					num += (double)((int)obj / dependencies.Count);
				}
			}
			return (int)num;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0005330C File Offset: 0x0005150C
		internal void GenerateSchemaGraph(XmlSchemas schemas)
		{
			ArrayList items = new SchemaGraph(this.Graph, schemas).GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				this.GetHash((XmlSchemaObject)items[i]);
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00053350 File Offset: 0x00051550
		private int GetHash(XmlSchemaObject o)
		{
			object obj = this.Hash[o];
			if (obj != null && !(obj is XmlSchemaObject))
			{
				return (int)obj;
			}
			string text = this.ToString(o, new SchemaObjectWriter());
			this.looks[o] = text;
			int hashCode = text.GetHashCode();
			this.Hash[o] = hashCode;
			return hashCode;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000533B0 File Offset: 0x000515B0
		private string ToString(XmlSchemaObject o, SchemaObjectWriter writer)
		{
			return writer.WriteXmlSchemaObject(o);
		}

		// Token: 0x04000824 RID: 2084
		private Hashtable graph;

		// Token: 0x04000825 RID: 2085
		private Hashtable hash;

		// Token: 0x04000826 RID: 2086
		private Hashtable objectCache;

		// Token: 0x04000827 RID: 2087
		private StringCollection warnings;

		// Token: 0x04000828 RID: 2088
		internal Hashtable looks = new Hashtable();
	}
}
