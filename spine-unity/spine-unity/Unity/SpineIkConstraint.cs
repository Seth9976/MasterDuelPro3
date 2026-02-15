using System;

namespace Spine.Unity
{
	// Token: 0x0200006E RID: 110
	public class SpineIkConstraint : SpineAttributeBase
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00012D82 File Offset: 0x00010F82
		public SpineIkConstraint(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}
	}
}
