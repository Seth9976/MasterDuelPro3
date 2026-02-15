using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F6C RID: 3948
	public class CommonDialogItemContentWidget : ContentWidgetBase<CommonDialogItemContentWidget, EntryItemContentData>, IContentWidgetAsyncLoader
	{
		// Token: 0x06007432 RID: 29746 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogItemContentWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x0000216D File Offset: 0x0000036D
		public void AsyncBinding(IEntryData entryData, Action onComplete)
		{
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryItemContentData entryData)
		{
		}

		// Token: 0x0400AD45 RID: 44357
		private readonly string k_ELabelNameText;

		// Token: 0x0400AD46 RID: 44358
		private readonly string k_ELabelCategoryText;

		// Token: 0x0400AD47 RID: 44359
		private readonly string k_ELabelNumText;

		// Token: 0x0400AD48 RID: 44360
		private readonly string k_ELabelThumbHolder;

		// Token: 0x0400AD49 RID: 44361
		private readonly string k_ELabelBadgeLocator;

		// Token: 0x0400AD4A RID: 44362
		private readonly string k_ELabelButton;

		// Token: 0x0400AD4B RID: 44363
		private readonly string k_ELabelEffectRawImage;

		// Token: 0x0400AD4C RID: 44364
		private readonly string k_ALabelItemObtainEffect;

		// Token: 0x0400AD4D RID: 44365
		private TMP_Text m_NameText;

		// Token: 0x0400AD4E RID: 44366
		private TMP_Text m_CategoryText;

		// Token: 0x0400AD4F RID: 44367
		private TMP_Text m_Text;

		// Token: 0x0400AD50 RID: 44368
		private GameObject m_RenderTarget;

		// Token: 0x0400AD51 RID: 44369
		private int m_RtId;

		// Token: 0x0400AD52 RID: 44370
		private CommonDialogItemContentWidget.StrechRectTransformChanger structureCursorRect;

		// Token: 0x02000F6D RID: 3949
		public class StrechRectTransformChanger
		{
			// Token: 0x06007437 RID: 29751 RVA: 0x00002739 File Offset: 0x00000939
			public StrechRectTransformChanger(int left, int right, int top, int bottom)
			{
			}

			// Token: 0x06007438 RID: 29752 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplayRect(RectTransform targetRect)
			{
			}

			// Token: 0x0400AD53 RID: 44371
			private readonly int left;

			// Token: 0x0400AD54 RID: 44372
			private readonly int right;

			// Token: 0x0400AD55 RID: 44373
			private readonly int top;

			// Token: 0x0400AD56 RID: 44374
			private readonly int bottom;
		}
	}
}
