using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F73 RID: 3955
	public class CommonDialogTitleWidget : ContentWidgetBase<CommonDialogTitleWidget, EntryTitleData>
	{
		// Token: 0x0600745F RID: 29791 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogTitleWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007461 RID: 29793 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryTitleData entryData)
		{
		}

		// Token: 0x0400AD6E RID: 44398
		private readonly string k_ELabelIcon;

		// Token: 0x0400AD6F RID: 44399
		private readonly string k_ELabelText;

		// Token: 0x0400AD70 RID: 44400
		[SerializeField]
		private Sprite m_NoticeIcon;

		// Token: 0x0400AD71 RID: 44401
		[SerializeField]
		private Sprite m_AlertIcon;

		// Token: 0x0400AD72 RID: 44402
		private TMP_Text m_Text;

		// Token: 0x02000F74 RID: 3956
		public enum IconType
		{
			// Token: 0x0400AD74 RID: 44404
			None,
			// Token: 0x0400AD75 RID: 44405
			Notice,
			// Token: 0x0400AD76 RID: 44406
			Alert
		}
	}
}
