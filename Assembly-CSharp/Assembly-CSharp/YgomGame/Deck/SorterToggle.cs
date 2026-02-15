using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02001008 RID: 4104
	public class SorterToggle : MonoBehaviour
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06007B86 RID: 31622 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager m_eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06007B87 RID: 31623 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedTextMeshProUGUI m_MethodLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06007B88 RID: 31624 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_AscImageOff
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06007B89 RID: 31625 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_AscImageOn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06007B8A RID: 31626 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_DescImageOff
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06007B8B RID: 31627 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_DescImageOn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06007B8C RID: 31628 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_AscButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06007B8D RID: 31629 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_DescButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(string label, bool isAscOn, bool isDescOn)
		{
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickAscCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDescCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDownTransitionItem(SelectionItem item)
		{
		}

		// Token: 0x0400B329 RID: 45865
		private UnityAction m_OnClickAscAction;

		// Token: 0x0400B32A RID: 45866
		private UnityAction m_OnClickDescAction;

		// Token: 0x0400B32B RID: 45867
		private const string LABEL_SBN_ASCBUTTON = "Button0";

		// Token: 0x0400B32C RID: 45868
		private const string LABEL_SBN_DESCBUTTON = "Button1";

		// Token: 0x0400B32D RID: 45869
		private const string LABEL_RT_ASCIMAGEOFF = "ImageOff0";

		// Token: 0x0400B32E RID: 45870
		private const string LABEL_RT_ASCIMAGEON = "ImageOn0";

		// Token: 0x0400B32F RID: 45871
		private const string LABEL_RT_DESCIMAGEOFF = "ImageOff1";

		// Token: 0x0400B330 RID: 45872
		private const string LABEL_RT_DESCIMAGEON = "ImageOn1";

		// Token: 0x0400B331 RID: 45873
		private const string LABEL_TXT_METHODTEXT = "Label";
	}
}
