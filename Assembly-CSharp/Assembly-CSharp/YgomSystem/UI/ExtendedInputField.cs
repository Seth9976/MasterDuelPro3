using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.LocalizedFont;

namespace YgomSystem.UI
{
	// Token: 0x02000590 RID: 1424
	public sealed class ExtendedInputField : InputField, ILocalizedFontOwner
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06002CEE RID: 11502 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x04002B27 RID: 11047
		[SerializeField]
		private LocalizedFontSettingsBase.FontType m_localizedFontType;

		// Token: 0x04002B28 RID: 11048
		[SerializeField]
		private int m_localizedFontMaterialIndex;

		// Token: 0x04002B29 RID: 11049
		private Event m_ProcessingEvent;

		// Token: 0x04002B2A RID: 11050
		private int preAnchor;

		// Token: 0x04002B2B RID: 11051
		private int preFocus;

		// Token: 0x04002B2C RID: 11052
		private string preText;
	}
}
