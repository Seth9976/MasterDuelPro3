using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000288 RID: 648
	internal class NamespaceList
	{
		// Token: 0x06001D49 RID: 7497 RVA: 0x00002127 File Offset: 0x00000327
		public NamespaceList()
		{
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000A6894 File Offset: 0x000A4A94
		public NamespaceList(string namespaces, string targetNamespace)
		{
			this.targetNamespace = targetNamespace;
			namespaces = namespaces.Trim();
			if (namespaces == "##any" || namespaces.Length == 0)
			{
				this.type = NamespaceList.ListType.Any;
				return;
			}
			if (namespaces == "##other")
			{
				this.type = NamespaceList.ListType.Other;
				return;
			}
			this.type = NamespaceList.ListType.Set;
			this.set = new Hashtable();
			string[] array = XmlConvert.SplitString(namespaces);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "##local")
				{
					this.set[string.Empty] = string.Empty;
				}
				else if (array[i] == "##targetNamespace")
				{
					this.set[targetNamespace] = targetNamespace;
				}
				else
				{
					XmlConvert.ToUri(array[i]);
					this.set[array[i]] = array[i];
				}
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000A6970 File Offset: 0x000A4B70
		public NamespaceList Clone()
		{
			NamespaceList namespaceList = (NamespaceList)base.MemberwiseClone();
			if (this.type == NamespaceList.ListType.Set)
			{
				namespaceList.set = (Hashtable)this.set.Clone();
			}
			return namespaceList;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x000A69A9 File Offset: 0x000A4BA9
		public NamespaceList.ListType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000A69B1 File Offset: 0x000A4BB1
		public string Excluded
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x000A69BC File Offset: 0x000A4BBC
		public ICollection Enumerate
		{
			get
			{
				NamespaceList.ListType listType = this.type;
				if (listType > NamespaceList.ListType.Other && listType == NamespaceList.ListType.Set)
				{
					return this.set.Keys;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000A69EC File Offset: 0x000A4BEC
		public virtual bool Allows(string ns)
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return true;
			case NamespaceList.ListType.Other:
				return ns != this.targetNamespace && ns.Length != 0;
			case NamespaceList.ListType.Set:
				return this.set[ns] != null;
			default:
				return false;
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000A6A41 File Offset: 0x000A4C41
		public bool Allows(XmlQualifiedName qname)
		{
			return this.Allows(qname.Namespace);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000A6A50 File Offset: 0x000A4C50
		public override string ToString()
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return "##any";
			case NamespaceList.ListType.Other:
				return "##other";
			case NamespaceList.ListType.Set:
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj in this.set.Keys)
				{
					string text = (string)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					if (text == this.targetNamespace)
					{
						stringBuilder.Append("##targetNamespace");
					}
					else if (text.Length == 0)
					{
						stringBuilder.Append("##local");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			default:
				return string.Empty;
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		public static bool IsSubset(NamespaceList sub, NamespaceList super)
		{
			if (super.type == NamespaceList.ListType.Any)
			{
				return true;
			}
			if (sub.type == NamespaceList.ListType.Other && super.type == NamespaceList.ListType.Other)
			{
				return super.targetNamespace == sub.targetNamespace;
			}
			if (sub.type != NamespaceList.ListType.Set)
			{
				return false;
			}
			if (super.type == NamespaceList.ListType.Other)
			{
				return !sub.set.Contains(super.targetNamespace);
			}
			foreach (object obj in sub.set.Keys)
			{
				string text = (string)obj;
				if (!super.set.Contains(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000A6C00 File Offset: 0x000A4E00
		public static NamespaceList Union(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					using (IEnumerator enumerator = o2.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							namespaceList.set[text] = text;
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o1.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o2.Clone();
						}
					}
					else if (o2.targetNamespace != string.Empty)
					{
						namespaceList = o1.CompareSetToOther(o2);
					}
					else if (o1.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o2.type == NamespaceList.ListType.Set && o1.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o2.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o1.Clone();
						}
					}
					else if (o1.targetNamespace != string.Empty)
					{
						namespaceList = o2.CompareSetToOther(o1);
					}
					else if (o2.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000A6E00 File Offset: 0x000A5000
		private NamespaceList CompareSetToOther(NamespaceList other)
		{
			NamespaceList namespaceList;
			if (this.set.Contains(other.targetNamespace))
			{
				if (this.set.Contains(string.Empty))
				{
					namespaceList = new NamespaceList();
				}
				else
				{
					namespaceList = new NamespaceList("##other", string.Empty);
				}
			}
			else if (this.set.Contains(string.Empty))
			{
				namespaceList = null;
			}
			else
			{
				namespaceList = other.Clone();
			}
			return namespaceList;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000A6E6C File Offset: 0x000A506C
		public static NamespaceList Intersection(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = o2.Clone();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = o1.Clone();
			}
			else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
			{
				namespaceList = o1.Clone();
				namespaceList.RemoveNamespace(o2.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Set)
			{
				namespaceList = o2.Clone();
				namespaceList.RemoveNamespace(o1.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					namespaceList = new NamespaceList();
					namespaceList.type = NamespaceList.ListType.Set;
					namespaceList.set = new Hashtable();
					using (IEnumerator enumerator = o1.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							if (o2.set.Contains(text))
							{
								namespaceList.set.Add(text, text);
							}
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
						return namespaceList;
					}
					if (!v1Compat)
					{
						if (o1.targetNamespace == string.Empty)
						{
							namespaceList = o2.Clone();
						}
						else if (o2.targetNamespace == string.Empty)
						{
							namespaceList = o1.Clone();
						}
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000A7018 File Offset: 0x000A5218
		private void RemoveNamespace(string tns)
		{
			if (this.set[tns] != null)
			{
				this.set.Remove(tns);
			}
		}

		// Token: 0x04000C94 RID: 3220
		private NamespaceList.ListType type;

		// Token: 0x04000C95 RID: 3221
		private Hashtable set;

		// Token: 0x04000C96 RID: 3222
		private string targetNamespace;

		// Token: 0x02000289 RID: 649
		public enum ListType
		{
			// Token: 0x04000C98 RID: 3224
			Any,
			// Token: 0x04000C99 RID: 3225
			Other,
			// Token: 0x04000C9A RID: 3226
			Set
		}
	}
}
