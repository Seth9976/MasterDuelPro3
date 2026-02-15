using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000305 RID: 773
	internal class XmlSchemaSubstitutionGroupV1Compat : XmlSchemaSubstitutionGroup
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000C313B File Offset: 0x000C133B
		[XmlIgnore]
		internal XmlSchemaChoice Choice
		{
			get
			{
				return this.choice;
			}
		}

		// Token: 0x04000FF8 RID: 4088
		private XmlSchemaChoice choice = new XmlSchemaChoice();
	}
}
