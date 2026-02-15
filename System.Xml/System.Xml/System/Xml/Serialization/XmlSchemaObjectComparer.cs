using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000185 RID: 389
	internal class XmlSchemaObjectComparer : IComparer
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x00057419 File Offset: 0x00055619
		public int Compare(object o1, object o2)
		{
			return this.comparer.Compare(XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o1), XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o2));
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0005743C File Offset: 0x0005563C
		internal static XmlQualifiedName NameOf(XmlSchemaObject o)
		{
			if (o is XmlSchemaAttribute)
			{
				return ((XmlSchemaAttribute)o).QualifiedName;
			}
			if (o is XmlSchemaAttributeGroup)
			{
				return ((XmlSchemaAttributeGroup)o).QualifiedName;
			}
			if (o is XmlSchemaComplexType)
			{
				return ((XmlSchemaComplexType)o).QualifiedName;
			}
			if (o is XmlSchemaSimpleType)
			{
				return ((XmlSchemaSimpleType)o).QualifiedName;
			}
			if (o is XmlSchemaElement)
			{
				return ((XmlSchemaElement)o).QualifiedName;
			}
			if (o is XmlSchemaGroup)
			{
				return ((XmlSchemaGroup)o).QualifiedName;
			}
			if (o is XmlSchemaGroupRef)
			{
				return ((XmlSchemaGroupRef)o).RefName;
			}
			if (o is XmlSchemaNotation)
			{
				return ((XmlSchemaNotation)o).QualifiedName;
			}
			if (o is XmlSchemaSequence)
			{
				XmlSchemaSequence xmlSchemaSequence = (XmlSchemaSequence)o;
				if (xmlSchemaSequence.Items.Count == 0)
				{
					return new XmlQualifiedName(".sequence", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaSequence.Items[0]);
			}
			else if (o is XmlSchemaAll)
			{
				XmlSchemaAll xmlSchemaAll = (XmlSchemaAll)o;
				if (xmlSchemaAll.Items.Count == 0)
				{
					return new XmlQualifiedName(".all", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaAll.Items);
			}
			else if (o is XmlSchemaChoice)
			{
				XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)o;
				if (xmlSchemaChoice.Items.Count == 0)
				{
					return new XmlQualifiedName(".choice", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaChoice.Items);
			}
			else
			{
				if (o is XmlSchemaAny)
				{
					return new XmlQualifiedName("*", SchemaObjectWriter.ToString(((XmlSchemaAny)o).NamespaceList));
				}
				if (o is XmlSchemaIdentityConstraint)
				{
					return ((XmlSchemaIdentityConstraint)o).QualifiedName;
				}
				return new XmlQualifiedName("?", XmlSchemaObjectComparer.Namespace(o));
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000575E4 File Offset: 0x000557E4
		internal static XmlQualifiedName NameOf(XmlSchemaObjectCollection items)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < items.Count; i++)
			{
				arrayList.Add(XmlSchemaObjectComparer.NameOf(items[i]));
			}
			arrayList.Sort(new QNameComparer());
			return (XmlQualifiedName)arrayList[0];
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00057632 File Offset: 0x00055832
		internal static string Namespace(XmlSchemaObject o)
		{
			while (o != null && !(o is XmlSchema))
			{
				o = o.Parent;
			}
			if (o != null)
			{
				return ((XmlSchema)o).TargetNamespace;
			}
			return "";
		}

		// Token: 0x040008B9 RID: 2233
		private QNameComparer comparer = new QNameComparer();
	}
}
