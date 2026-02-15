using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000304 RID: 772
	internal class XmlSchemaSubstitutionGroup : XmlSchemaObject
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000C3104 File Offset: 0x000C1304
		[XmlIgnore]
		internal ArrayList Members
		{
			get
			{
				return this.membersList;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000C310C File Offset: 0x000C130C
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x000C3114 File Offset: 0x000C1314
		[XmlIgnore]
		internal XmlQualifiedName Examplar
		{
			get
			{
				return this.examplar;
			}
			set
			{
				this.examplar = value;
			}
		}

		// Token: 0x04000FF6 RID: 4086
		private ArrayList membersList = new ArrayList();

		// Token: 0x04000FF7 RID: 4087
		private XmlQualifiedName examplar = XmlQualifiedName.Empty;
	}
}
