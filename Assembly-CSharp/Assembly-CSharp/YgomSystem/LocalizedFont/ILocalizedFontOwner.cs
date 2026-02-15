using System;

namespace YgomSystem.LocalizedFont
{
	// Token: 0x02000733 RID: 1843
	public interface ILocalizedFontOwner
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06003961 RID: 14689
		// (set) Token: 0x06003962 RID: 14690
		LocalizedFontSettingsBase.FontType localizedFontType { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06003963 RID: 14691
		// (set) Token: 0x06003964 RID: 14692
		int localizedFontMaterialIndex { get; set; }
	}
}
