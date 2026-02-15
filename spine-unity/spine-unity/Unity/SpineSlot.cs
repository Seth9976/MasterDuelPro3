using System;

namespace Spine.Unity
{
	// Token: 0x0200006B RID: 107
	public class SpineSlot : SpineAttributeBase
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00012DCE File Offset: 0x00010FCE
		public SpineSlot(string startsWith = "", string dataField = "", bool containsBoundingBoxes = false, bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.containsBoundingBoxes = containsBoundingBoxes;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}

		// Token: 0x0400021A RID: 538
		public bool containsBoundingBoxes;
	}
}
