using System;

namespace Spine.Unity
{
	// Token: 0x02000070 RID: 112
	public class SpinePathConstraint : SpineAttributeBase
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00012D82 File Offset: 0x00010F82
		public SpinePathConstraint(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}
	}
}
