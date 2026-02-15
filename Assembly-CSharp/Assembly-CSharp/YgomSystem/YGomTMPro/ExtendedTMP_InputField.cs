using System;
using TMPro;
using UnityEngine;
using YgomSystem.LocalizedFont;

namespace YgomSystem.YGomTMPro
{
	// Token: 0x020004EA RID: 1258
	public class ExtendedTMP_InputField : TMP_InputField, ILocalizedFontOwner
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x0000216D File Offset: 0x0000036D
		public LocalizedFontSettingsBase.FontType localizedFontType
		{
			get
			{
				return LocalizedFontSettingsBase.FontType.Other;
			}
			set
			{
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x0000216D File Offset: 0x0000036D
		public int localizedFontMaterialIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x040028B9 RID: 10425
		[SerializeField]
		private LocalizedFontSettingsBase.FontType m_localizedFontType;

		// Token: 0x040028BA RID: 10426
		[SerializeField]
		private int m_localizedFontMaterialIndex;
	}
}
