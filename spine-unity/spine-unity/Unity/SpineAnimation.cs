using System;

namespace Spine.Unity
{
	// Token: 0x0200006C RID: 108
	public class SpineAnimation : SpineAttributeBase
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00012DFB File Offset: 0x00010FFB
		public SpineAnimation(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false, bool avoidGenericMenu = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
			this.avoidGenericMenu = avoidGenericMenu;
		}
	}
}
