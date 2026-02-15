using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200029D RID: 669
	internal class SchemaNamespaceManager : XmlNamespaceManager
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x000B4937 File Offset: 0x000B2B37
		public SchemaNamespaceManager(XmlSchemaObject node)
		{
			this.node = node;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000B4948 File Offset: 0x000B2B48
		public override string LookupNamespace(string prefix)
		{
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					object obj = namespaces[prefix];
					if (obj != null)
					{
						return (string)obj;
					}
				}
			}
			if (prefix.Length != 0)
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000B49B4 File Offset: 0x000B2BB4
		public override string LookupPrefix(string ns)
		{
			if (ns == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					foreach (object obj in namespaces)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value.Equals(ns))
						{
							return (string)dictionaryEntry.Key;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000E38 RID: 3640
		private XmlSchemaObject node;
	}
}
