using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200021B RID: 539
	internal class SymbolsDictionary
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x0009A386 File Offset: 0x00098586
		public SymbolsDictionary()
		{
			this.names = new Hashtable();
			this.particles = new ArrayList();
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0009A3AB File Offset: 0x000985AB
		public int Count
		{
			get
			{
				return this.last + 1;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0009A3B5 File Offset: 0x000985B5
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0009A3BD File Offset: 0x000985BD
		public bool IsUpaEnforced
		{
			get
			{
				return this.isUpaEnforced;
			}
			set
			{
				this.isUpaEnforced = value;
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0009A3C8 File Offset: 0x000985C8
		public int AddName(XmlQualifiedName name, object particle)
		{
			object obj = this.names[name];
			if (obj != null)
			{
				int num = (int)obj;
				if (this.particles[num] != particle)
				{
					this.isUpaEnforced = false;
				}
				return num;
			}
			this.names.Add(name, this.last);
			this.particles.Add(particle);
			int num2 = this.last;
			this.last = num2 + 1;
			return num2;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0009A43C File Offset: 0x0009863C
		public void AddNamespaceList(NamespaceList list, object particle, bool allowLocal)
		{
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				this.particleLast = particle;
				return;
			case NamespaceList.ListType.Other:
				this.AddWildcard(list.Excluded, null);
				if (!allowLocal)
				{
					this.AddWildcard(string.Empty, null);
					return;
				}
				break;
			case NamespaceList.ListType.Set:
				foreach (object obj in list.Enumerate)
				{
					string text = (string)obj;
					this.AddWildcard(text, particle);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0009A4D8 File Offset: 0x000986D8
		private void AddWildcard(string wildcard, object particle)
		{
			if (this.wildcards == null)
			{
				this.wildcards = new Hashtable();
			}
			object obj = this.wildcards[wildcard];
			if (obj == null)
			{
				this.wildcards.Add(wildcard, this.last);
				this.particles.Add(particle);
				this.last++;
				return;
			}
			if (particle != null)
			{
				this.particles[(int)obj] = particle;
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0009A550 File Offset: 0x00098750
		public ICollection GetNamespaceListSymbols(NamespaceList list)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.names.Keys)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (xmlQualifiedName != XmlQualifiedName.Empty && list.Allows(xmlQualifiedName))
				{
					arrayList.Add(this.names[xmlQualifiedName]);
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards.Keys)
				{
					string text = (string)obj2;
					if (list.Allows(text))
					{
						arrayList.Add(this.wildcards[text]);
					}
				}
			}
			if (list.Type == NamespaceList.ListType.Any || list.Type == NamespaceList.ListType.Other)
			{
				arrayList.Add(this.last);
			}
			return arrayList;
		}

		// Token: 0x170005FD RID: 1533
		public int this[XmlQualifiedName name]
		{
			get
			{
				object obj = this.names[name];
				if (obj != null)
				{
					return (int)obj;
				}
				if (this.wildcards != null)
				{
					obj = this.wildcards[name.Namespace];
					if (obj != null)
					{
						return (int)obj;
					}
				}
				return this.last;
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0009A6B6 File Offset: 0x000988B6
		public bool Exists(XmlQualifiedName name)
		{
			return this.names[name] != null;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0009A6C9 File Offset: 0x000988C9
		public object GetParticle(int symbol)
		{
			if (symbol != this.last)
			{
				return this.particles[symbol];
			}
			return this.particleLast;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0009A6E8 File Offset: 0x000988E8
		public string NameOf(int symbol)
		{
			foreach (object obj in this.names)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if ((int)dictionaryEntry.Value == symbol)
				{
					return ((XmlQualifiedName)dictionaryEntry.Key).ToString();
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if ((int)dictionaryEntry2.Value == symbol)
					{
						return (string)dictionaryEntry2.Key + ":*";
					}
				}
			}
			return "##other:*";
		}

		// Token: 0x04000B5E RID: 2910
		private int last;

		// Token: 0x04000B5F RID: 2911
		private Hashtable names;

		// Token: 0x04000B60 RID: 2912
		private Hashtable wildcards;

		// Token: 0x04000B61 RID: 2913
		private ArrayList particles;

		// Token: 0x04000B62 RID: 2914
		private object particleLast;

		// Token: 0x04000B63 RID: 2915
		private bool isUpaEnforced = true;
	}
}
