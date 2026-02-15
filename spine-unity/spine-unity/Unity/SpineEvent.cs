using System;

namespace Spine.Unity
{
	// Token: 0x0200006D RID: 109
	public class SpineEvent : SpineAttributeBase
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00012E28 File Offset: 0x00011028
		public SpineEvent(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false, bool audioOnly = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
			this.audioOnly = audioOnly;
		}

		// Token: 0x0400021B RID: 539
		public bool audioOnly;
	}
}
