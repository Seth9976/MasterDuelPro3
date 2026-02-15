using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F69 RID: 3945
	public class CommonDialogIconTextWidget : ContentWidgetBase<CommonDialogIconTextWidget, EntryIconTextData>
	{
		// Token: 0x06007428 RID: 29736 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogIconTextWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryIconTextData entryData)
		{
		}

		// Token: 0x0400AD39 RID: 44345
		private readonly string k_ELabelIcon;

		// Token: 0x0400AD3A RID: 44346
		private readonly string k_ELabelText;

		// Token: 0x0400AD3B RID: 44347
		private readonly string k_ELabelNumText;

		// Token: 0x0400AD3C RID: 44348
		[SerializeField]
		private Sprite m_GemIcon;

		// Token: 0x0400AD3D RID: 44349
		private TMP_Text m_Text;

		// Token: 0x0400AD3E RID: 44350
		private TMP_Text m_NumText;

		// Token: 0x02000F6A RID: 3946
		public enum IconType
		{
			// Token: 0x0400AD40 RID: 44352
			None,
			// Token: 0x0400AD41 RID: 44353
			Path,
			// Token: 0x0400AD42 RID: 44354
			Gem,
			// Token: 0x0400AD43 RID: 44355
			Item
		}
	}
}
