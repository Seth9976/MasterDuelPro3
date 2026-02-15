using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FED RID: 4077
	public class LootSourceItem : MonoBehaviour
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06007AF1 RID: 31473 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007AF2 RID: 31474 RVA: 0x0000216D File Offset: 0x0000036D
		public LootSourceInfo.LootCategory m_Category
		{
			[CompilerGenerated]
			get
			{
				return (LootSourceInfo.LootCategory)0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06007AF3 RID: 31475 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007AF4 RID: 31476 RVA: 0x0000216D File Offset: 0x0000036D
		public string m_StringID
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06007AF5 RID: 31477 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007AF6 RID: 31478 RVA: 0x0000216D File Offset: 0x0000036D
		public bool m_IsAvailable
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06007AF7 RID: 31479 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager m_eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06007AF8 RID: 31480 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06007AF9 RID: 31481 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedTextMeshProUGUI m_SourceText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06007AFA RID: 31482 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedTextMeshProUGUI m_TitleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06007AFB RID: 31483 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_ImageSummary
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06007AFC RID: 31484 RVA: 0x0000216A File Offset: 0x0000036A
		private Image m_ImageSummaryIMG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06007AFD RID: 31485 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_On
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06007AFE RID: 31486 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_Off
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06007AFF RID: 31487 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_Mask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(LootSourceInfo.LootCategory cat, string title, string source, int param, bool available, int iconType = 0, string iconData = null, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetImageSummary(LootSourceInfo.LootCategory cat, int param = 0, string iconData = null)
		{
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleAvailability(bool b)
		{
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleAllow()
		{
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDeselectedCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnRightClickCallback(UnityAction<bool> callback)
		{
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeySub1Callback(UnityAction callback)
		{
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyLeftCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyL2Callback(UnityAction callback)
		{
		}

		// Token: 0x0400B237 RID: 45623
		private const string LABEL_SBN_BUTTON = "Button";

		// Token: 0x0400B238 RID: 45624
		private const string LABEL_TXT_SOURCETEXT = "TextSource";

		// Token: 0x0400B239 RID: 45625
		private const string LABEL_TXT_TITLETEXT = "TextTitle";

		// Token: 0x0400B23A RID: 45626
		private const string LABEL_RT_IMAGESUMMARY = "ImageSummary";

		// Token: 0x0400B23B RID: 45627
		private const string LABEL_IMG_IMAGESUMMARY = "ImageSummary";

		// Token: 0x0400B23C RID: 45628
		private const string LABEL_RT_ON = "On";

		// Token: 0x0400B23D RID: 45629
		private const string LABEL_RT_OFF = "Off";

		// Token: 0x0400B23E RID: 45630
		private const string LABEL_RT_MASK = "Mask";

		// Token: 0x0400B23F RID: 45631
		protected UnityAction m_OnClickAction;

		// Token: 0x0400B240 RID: 45632
		protected UnityAction m_OnSelectedAction;

		// Token: 0x0400B241 RID: 45633
		protected UnityAction m_OnDeselectedAction;

		// Token: 0x0400B242 RID: 45634
		protected UnityAction<bool> m_OnRightClickAction;

		// Token: 0x0400B243 RID: 45635
		protected UnityAction m_SelectedKeySub1Action;

		// Token: 0x0400B244 RID: 45636
		protected UnityAction m_SelectedKeyLeftAction;

		// Token: 0x0400B245 RID: 45637
		protected UnityAction m_SelectedKeyL2Action;
	}
}
