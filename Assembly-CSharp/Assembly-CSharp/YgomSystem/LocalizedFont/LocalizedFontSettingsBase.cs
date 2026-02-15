using System;
using UnityEngine;

namespace YgomSystem.LocalizedFont
{
	// Token: 0x02000739 RID: 1849
	public abstract class LocalizedFontSettingsBase : ScriptableObject
	{
		// Token: 0x0200073A RID: 1850
		public enum Locale
		{
			// Token: 0x040033F8 RID: 13304
			Other,
			// Token: 0x040033F9 RID: 13305
			Japanese,
			// Token: 0x040033FA RID: 13306
			Korean,
			// Token: 0x040033FB RID: 13307
			TraditionalChinese,
			// Token: 0x040033FC RID: 13308
			SimplifiedChinese
		}

		// Token: 0x0200073B RID: 1851
		public enum FontType
		{
			// Token: 0x040033FE RID: 13310
			Other,
			// Token: 0x040033FF RID: 13311
			Normal,
			// Token: 0x04003400 RID: 13312
			Card,
			// Token: 0x04003401 RID: 13313
			Bold,
			// Token: 0x04003402 RID: 13314
			Story,
			// Token: 0x04003403 RID: 13315
			BigMenu
		}
	}
}
