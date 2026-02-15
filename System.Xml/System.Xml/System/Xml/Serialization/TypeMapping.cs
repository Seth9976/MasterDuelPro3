using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000164 RID: 356
	internal abstract class TypeMapping : Mapping
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00054010 File Offset: 0x00052210
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00054018 File Offset: 0x00052218
		internal bool ReferencedByTopLevelElement
		{
			get
			{
				return this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByTopLevelElement = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00054021 File Offset: 0x00052221
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00054033 File Offset: 0x00052233
		internal bool ReferencedByElement
		{
			get
			{
				return this.referencedByElement || this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByElement = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0005403C File Offset: 0x0005223C
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00054044 File Offset: 0x00052244
		internal string Namespace
		{
			get
			{
				return this.typeNs;
			}
			set
			{
				this.typeNs = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0005404D File Offset: 0x0005224D
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00054055 File Offset: 0x00052255
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005405E File Offset: 0x0005225E
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00054066 File Offset: 0x00052266
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0005406F File Offset: 0x0005226F
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00054077 File Offset: 0x00052277
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual bool IsList
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00054080 File Offset: 0x00052280
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x00054088 File Offset: 0x00052288
		internal bool IsReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00054091 File Offset: 0x00052291
		internal bool IsAnonymousType
		{
			get
			{
				return this.typeName == null || this.typeName.Length == 0;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000540AB File Offset: 0x000522AB
		internal virtual string DefaultElementName
		{
			get
			{
				if (!this.IsAnonymousType)
				{
					return this.typeName;
				}
				return XmlConvert.EncodeLocalName(this.typeDesc.Name);
			}
		}

		// Token: 0x04000840 RID: 2112
		private TypeDesc typeDesc;

		// Token: 0x04000841 RID: 2113
		private string typeNs;

		// Token: 0x04000842 RID: 2114
		private string typeName;

		// Token: 0x04000843 RID: 2115
		private bool referencedByElement;

		// Token: 0x04000844 RID: 2116
		private bool referencedByTopLevelElement;

		// Token: 0x04000845 RID: 2117
		private bool includeInSchema = true;

		// Token: 0x04000846 RID: 2118
		private bool reference;
	}
}
