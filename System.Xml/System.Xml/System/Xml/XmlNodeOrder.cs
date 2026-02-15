using System;

namespace System.Xml
{
	/// <summary>Describes the document order of a node compared to a second node.</summary>
	// Token: 0x0200012D RID: 301
	public enum XmlNodeOrder
	{
		/// <summary>The current node of this navigator is before the current node of the supplied navigator.</summary>
		// Token: 0x04000760 RID: 1888
		Before,
		/// <summary>The current node of this navigator is after the current node of the supplied navigator.</summary>
		// Token: 0x04000761 RID: 1889
		After,
		/// <summary>The two navigators are positioned on the same node.</summary>
		// Token: 0x04000762 RID: 1890
		Same,
		/// <summary>The node positions cannot be determined in document order, relative to each other. This could occur if the two nodes reside in different trees.</summary>
		// Token: 0x04000763 RID: 1891
		Unknown
	}
}
