using System;

namespace Cinemachine
{
	// Token: 0x02000088 RID: 136
	[DocumentationSorting(DocumentationSortingAttribute.Level.Undoc)]
	public sealed class DocumentationSortingAttribute : Attribute
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00013896 File Offset: 0x00011A96
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001389E File Offset: 0x00011A9E
		public DocumentationSortingAttribute.Level Category { get; private set; }

		// Token: 0x06000327 RID: 807 RVA: 0x000138A7 File Offset: 0x00011AA7
		public DocumentationSortingAttribute(DocumentationSortingAttribute.Level category)
		{
			this.Category = category;
		}

		// Token: 0x02000089 RID: 137
		public enum Level
		{
			// Token: 0x040002DE RID: 734
			Undoc,
			// Token: 0x040002DF RID: 735
			API,
			// Token: 0x040002E0 RID: 736
			UserRef
		}
	}
}
