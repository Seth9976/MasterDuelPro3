using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F72 RID: 3954
	public class CommonDialogTextWidget : ContentWidgetBase<CommonDialogTextWidget, EntryTextData>
	{
		// Token: 0x0600745B RID: 29787 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogTextWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600745D RID: 29789 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryTextData entryData)
		{
		}

		// Token: 0x0400AD6C RID: 44396
		private GameObject m_Base;

		// Token: 0x0400AD6D RID: 44397
		private TMP_Text m_Text;
	}
}
