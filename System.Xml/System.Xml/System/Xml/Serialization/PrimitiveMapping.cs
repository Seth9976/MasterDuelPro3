using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000165 RID: 357
	internal class PrimitiveMapping : TypeMapping
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000540DB File Offset: 0x000522DB
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x000540E3 File Offset: 0x000522E3
		internal override bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x04000847 RID: 2119
		private bool isList;
	}
}
