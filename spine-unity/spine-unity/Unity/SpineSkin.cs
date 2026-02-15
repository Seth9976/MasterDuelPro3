using System;

namespace Spine.Unity
{
	// Token: 0x02000071 RID: 113
	public class SpineSkin : SpineAttributeBase
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00012E55 File Offset: 0x00011055
		public SpineSkin(string startsWith = "", string dataField = "", bool includeNone = false, bool fallbackToTextField = false, bool defaultAsEmptyString = false, bool avoidGenericMenu = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
			this.defaultAsEmptyString = defaultAsEmptyString;
			this.avoidGenericMenu = avoidGenericMenu;
		}

		// Token: 0x0400021C RID: 540
		public bool defaultAsEmptyString;
	}
}
