using System;

namespace Spine.Unity
{
	// Token: 0x0200006F RID: 111
	public class SpineTransformConstraint : SpineAttributeBase
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00012D82 File Offset: 0x00010F82
		public SpineTransformConstraint(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}
	}
}
