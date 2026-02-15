using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack
{
	// Token: 0x020010AB RID: 4267
	public class CardWidget : ElementWidgetBase
	{
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06007EE2 RID: 32482 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007EE3 RID: 32483 RVA: 0x0000216D File Offset: 0x0000036D
		public bool highlightVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06007EE4 RID: 32484 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007EE5 RID: 32485 RVA: 0x0000216D File Offset: 0x0000036D
		public bool newIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06007EE6 RID: 32486 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007EE7 RID: 32487 RVA: 0x0000216D File Offset: 0x0000036D
		public bool limitIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06007EE8 RID: 32488 RVA: 0x0000216A File Offset: 0x0000036A
		public Image limitIconImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06007EE9 RID: 32489 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject innerTextArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06007EEA RID: 32490 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text innerText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06007EEB RID: 32491 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06007EEC RID: 32492 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingCardMaterial bindingCardMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public CardWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Binding(int mrk, int styleId = 1)
		{
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnClick()
		{
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenDetail()
		{
		}

		// Token: 0x0400B77E RID: 46974
		private readonly string k_ECardLabelButton;

		// Token: 0x0400B77F RID: 46975
		private readonly string k_ECardLabelHighlight;

		// Token: 0x0400B780 RID: 46976
		private readonly string k_ECardLabelIconRarity;

		// Token: 0x0400B781 RID: 46977
		private readonly string k_ECardLabelLimitIcon;

		// Token: 0x0400B782 RID: 46978
		private readonly string k_ECardLabelNumTextArea;

		// Token: 0x0400B783 RID: 46979
		private readonly string k_ECardLabelNumText;

		// Token: 0x0400B784 RID: 46980
		private readonly string k_ECardLabelNewIcon;

		// Token: 0x0400B785 RID: 46981
		private int m_Mrk;

		// Token: 0x0400B786 RID: 46982
		private int m_StyleId;

		// Token: 0x0400B787 RID: 46983
		private readonly RawImage m_CardRawImage;

		// Token: 0x0400B788 RID: 46984
		private BindingCardMaterial m_BindingCardMaterial;

		// Token: 0x0400B789 RID: 46985
		public int regurationId;
	}
}
