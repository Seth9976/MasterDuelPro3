using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BBD RID: 3005
	public class MDMarkupPageWidgetBase : MDMarkupWidgetBase
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060055B9 RID: 21945 RVA: 0x0000216A File Offset: 0x0000036A
		private List<SelectionButton> YgomGame_002EMDMarkup_002EIMDMarkupPageWidget_002Ebuttons
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060055BA RID: 21946 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060055BB RID: 21947 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<bool> onFocusPageEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x000F4CC8 File Offset: 0x000F2EC8
		public MDMarkupPageWidgetBase(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x0000216D File Offset: 0x0000036D
		public override void BindContentData(IMDMarkupContent mdMarkupContent)
		{
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x0000216D File Offset: 0x0000036D
		public void OutputMarkupGraph(IMDMarkupContent mdMarkupContent, MDMarkupGraphFactory markupGraphFactory, Action onComplete)
		{
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual GlobalTextData GetCaptionText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual GlobalTextData GetText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual MDMarkupBannerContext GetBannerContext(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string GetResourcePath(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual List<URLSchemeButton> GetButtons(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual List<IMDMarkupContent> GetMarkupContents(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFocusPage(bool isFirst)
		{
		}

		// Token: 0x040092CB RID: 37579
		private readonly string k_ELabelCaption;

		// Token: 0x040092CC RID: 37580
		private readonly string k_ELabelText;

		// Token: 0x040092CD RID: 37581
		private readonly string k_ELabelImage;

		// Token: 0x040092CE RID: 37582
		private readonly string k_ELabelMarkupRoot;

		// Token: 0x040092CF RID: 37583
		private readonly string k_ELabelButtonFormat;

		// Token: 0x040092D0 RID: 37584
		public readonly GameObject m_ImageTarget;

		// Token: 0x040092D1 RID: 37585
		public readonly TextMeshProUGUI m_CaptionText;

		// Token: 0x040092D2 RID: 37586
		public readonly TextMeshProUGUI m_Text;

		// Token: 0x040092D3 RID: 37587
		public readonly Transform m_MarkupRoot;

		// Token: 0x040092D4 RID: 37588
		public readonly List<SelectionButton> buttons;
	}
}
