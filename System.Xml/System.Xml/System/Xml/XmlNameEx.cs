using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000ED RID: 237
	internal sealed class XmlNameEx : XmlName
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0003F56C File Offset: 0x0003D76C
		internal XmlNameEx(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
			: base(prefix, localName, ns, hashCode, ownerDoc, next)
		{
			this.SetValidity(schemaInfo.Validity);
			this.SetIsDefault(schemaInfo.IsDefault);
			this.SetIsNil(schemaInfo.IsNil);
			this.memberType = schemaInfo.MemberType;
			this.schemaType = schemaInfo.SchemaType;
			this.decl = ((schemaInfo.SchemaElement != null) ? schemaInfo.SchemaElement : schemaInfo.SchemaAttribute);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		public override XmlSchemaValidity Validity
		{
			get
			{
				if (!this.ownerDoc.CanReportValidity)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return (XmlSchemaValidity)(this.flags & 3);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0003F601 File Offset: 0x0003D801
		public override bool IsDefault
		{
			get
			{
				return (this.flags & 4) > 0;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0003F60E File Offset: 0x0003D80E
		public override bool IsNil
		{
			get
			{
				return (this.flags & 8) > 0;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003F61B File Offset: 0x0003D81B
		public override XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003F623 File Offset: 0x0003D823
		public override XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0003F62B File Offset: 0x0003D82B
		public override XmlSchemaElement SchemaElement
		{
			get
			{
				return this.decl as XmlSchemaElement;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0003F638 File Offset: 0x0003D838
		public override XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.decl as XmlSchemaAttribute;
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0003F645 File Offset: 0x0003D845
		public void SetValidity(XmlSchemaValidity value)
		{
			this.flags = (byte)(((int)this.flags & -4) | (int)((byte)value));
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0003F65A File Offset: 0x0003D85A
		public void SetIsDefault(bool value)
		{
			if (value)
			{
				this.flags |= 4;
				return;
			}
			this.flags = (byte)((int)this.flags & -5);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0003F67F File Offset: 0x0003D87F
		public void SetIsNil(bool value)
		{
			if (value)
			{
				this.flags |= 8;
				return;
			}
			this.flags = (byte)((int)this.flags & -9);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0003F6A4 File Offset: 0x0003D8A4
		public override bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo != null && schemaInfo.Validity == (XmlSchemaValidity)(this.flags & 3) && schemaInfo.IsDefault == (this.flags & 4) > 0 && schemaInfo.IsNil == (this.flags & 8) > 0 && schemaInfo.MemberType == this.memberType && schemaInfo.SchemaType == this.schemaType && schemaInfo.SchemaElement == this.decl as XmlSchemaElement && schemaInfo.SchemaAttribute == this.decl as XmlSchemaAttribute;
		}

		// Token: 0x04000653 RID: 1619
		private byte flags;

		// Token: 0x04000654 RID: 1620
		private XmlSchemaSimpleType memberType;

		// Token: 0x04000655 RID: 1621
		private XmlSchemaType schemaType;

		// Token: 0x04000656 RID: 1622
		private object decl;
	}
}
