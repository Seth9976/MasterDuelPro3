using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200017E RID: 382
	internal class NameTable : INameScope
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x00055F2C File Offset: 0x0005412C
		internal void Add(XmlQualifiedName qname, object value)
		{
			this.Add(qname.Name, qname.Namespace, value);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00055F44 File Offset: 0x00054144
		internal void Add(string name, string ns, object value)
		{
			NameKey nameKey = new NameKey(name, ns);
			this.table.Add(nameKey, value);
		}

		// Token: 0x17000440 RID: 1088
		internal object this[XmlQualifiedName qname]
		{
			get
			{
				return this.table[new NameKey(qname.Name, qname.Namespace)];
			}
			set
			{
				this.table[new NameKey(qname.Name, qname.Namespace)] = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		internal object this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		object INameScope.this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00055FCC File Offset: 0x000541CC
		internal ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00055FDC File Offset: 0x000541DC
		internal Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.table.Count);
			this.table.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0400089A RID: 2202
		private Hashtable table = new Hashtable();
	}
}
