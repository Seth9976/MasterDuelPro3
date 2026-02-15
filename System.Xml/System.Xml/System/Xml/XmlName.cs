using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000EC RID: 236
	internal class XmlName : IXmlSchemaInfo
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		public static XmlName Create(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
		{
			if (schemaInfo == null)
			{
				return new XmlName(prefix, localName, ns, hashCode, ownerDoc, next);
			}
			return new XmlNameEx(prefix, localName, ns, hashCode, ownerDoc, next, schemaInfo);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003F3F3 File Offset: 0x0003D5F3
		internal XmlName(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.name = null;
			this.hashCode = hashCode;
			this.ownerDoc = ownerDoc;
			this.next = next;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0003F42F File Offset: 0x0003D62F
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0003F437 File Offset: 0x0003D637
		public string NamespaceURI
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0003F43F File Offset: 0x0003D63F
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0003F447 File Offset: 0x0003D647
		public int HashCode
		{
			get
			{
				return this.hashCode;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0003F44F File Offset: 0x0003D64F
		public XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDoc;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0003F458 File Offset: 0x0003D658
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					if (this.prefix.Length > 0)
					{
						if (this.localName.Length > 0)
						{
							string text = this.prefix + ":" + this.localName;
							XmlNameTable nameTable = this.ownerDoc.NameTable;
							lock (nameTable)
							{
								if (this.name == null)
								{
									this.name = this.ownerDoc.NameTable.Add(text);
								}
								goto IL_0099;
							}
						}
						this.name = this.prefix;
					}
					else
					{
						this.name = this.localName;
					}
				}
				IL_0099:
				return this.name;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003F514 File Offset: 0x0003D714
		public virtual bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo == null;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003F51C File Offset: 0x0003D71C
		public static int GetHashCode(string name)
		{
			int num = 0;
			if (name != null)
			{
				for (int i = name.Length - 1; i >= 0; i--)
				{
					char c = name[i];
					if (c == ':')
					{
						break;
					}
					num += (num << 7) ^ (int)c;
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
			}
			return num;
		}

		// Token: 0x0400064C RID: 1612
		private string prefix;

		// Token: 0x0400064D RID: 1613
		private string localName;

		// Token: 0x0400064E RID: 1614
		private string ns;

		// Token: 0x0400064F RID: 1615
		private string name;

		// Token: 0x04000650 RID: 1616
		private int hashCode;

		// Token: 0x04000651 RID: 1617
		internal XmlDocument ownerDoc;

		// Token: 0x04000652 RID: 1618
		internal XmlName next;
	}
}
