using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000EB2 RID: 3762
	public class ListCard : MonoBehaviour
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06006D81 RID: 28033 RVA: 0x000F6054 File Offset: 0x000F4254
		private Color m_OwnerColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06006D82 RID: 28034 RVA: 0x000029CC File Offset: 0x00000BCC
		public int RunListIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06006D83 RID: 28035 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06006D84 RID: 28036 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Isknown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06006D85 RID: 28037 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool HoldInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06006D86 RID: 28038 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CardId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06006D87 RID: 28039 RVA: 0x000029CC File Offset: 0x00000BCC
		public int UniqueID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06006D88 RID: 28040 RVA: 0x000029CC File Offset: 0x00000BCC
		public int TargetUid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (set) Token: 0x06006D89 RID: 28041 RVA: 0x0000216D File Offset: 0x0000036D
		public int Badge
		{
			set
			{
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06006D8A RID: 28042 RVA: 0x000029CC File Offset: 0x00000BCC
		public int textid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (set) Token: 0x06006D8B RID: 28043 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Selected
		{
			set
			{
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (set) Token: 0x06006D8C RID: 28044 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Targeted
		{
			set
			{
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (set) Token: 0x06006D8D RID: 28045 RVA: 0x0000216D File Offset: 0x0000036D
		public bool DarkMode
		{
			set
			{
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06006D8E RID: 28046 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Player
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06006D8F RID: 28047 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Position
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06006D90 RID: 28048 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Face
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06006D91 RID: 28049 RVA: 0x000029CC File Offset: 0x00000BCC
		public int highlightEffectTableIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponent()
		{
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDecideButton(bool use_decide_button, SelectionButton decide_button = null)
		{
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ListCardData data)
		{
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateListCard()
		{
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGroupPos(int index)
		{
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardArea()
		{
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetInfoArea()
		{
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLevelRankLink()
		{
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPendulumScale()
		{
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardPicture()
		{
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClick(UnityAction<ListCard> onClick)
		{
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDoubleClick(UnityAction<ListCard> onDoubleClick)
		{
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnHold(UnityAction<ListCard> onHold)
		{
		}

		// Token: 0x06006DA0 RID: 28064 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectionCursor()
		{
		}

		// Token: 0x06006DA1 RID: 28065 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform GetCardPictureTransform()
		{
			return null;
		}

		// Token: 0x0400A88F RID: 43151
		[SerializeField]
		private Color m_ColorMyself;

		// Token: 0x0400A890 RID: 43152
		[SerializeField]
		private Color m_ColorRIval;

		// Token: 0x0400A891 RID: 43153
		[SerializeField]
		private Color m_ColorDefault;

		// Token: 0x0400A892 RID: 43154
		private ListCardData m_CardData;

		// Token: 0x0400A893 RID: 43155
		private ElementObjectManager m_EOManager;

		// Token: 0x0400A894 RID: 43156
		private RawImage m_CardPicture;

		// Token: 0x0400A895 RID: 43157
		private RawImage m_DarkMask;

		// Token: 0x0400A896 RID: 43158
		private GameObject m_LinkMarker;

		// Token: 0x0400A897 RID: 43159
		private GameObject m_CardMask;

		// Token: 0x0400A898 RID: 43160
		private GameObject m_Badge;

		// Token: 0x0400A899 RID: 43161
		private GameObject m_PendulumInfo;

		// Token: 0x0400A89A RID: 43162
		private Image m_BgPanel;

		// Token: 0x0400A89B RID: 43163
		private Image m_SelectedEffect;

		// Token: 0x0400A89C RID: 43164
		private Image m_Star;

		// Token: 0x0400A89D RID: 43165
		private Image m_TargettedIcon;

		// Token: 0x0400A89E RID: 43166
		private Image m_ChainIcon;

		// Token: 0x0400A89F RID: 43167
		private Image m_FromExtraIcon;

		// Token: 0x0400A8A0 RID: 43168
		private ExtendedTextMeshProUGUI m_BadgeIndex;

		// Token: 0x0400A8A1 RID: 43169
		private ExtendedTextMeshProUGUI m_LvRkLkNum;

		// Token: 0x0400A8A2 RID: 43170
		private ExtendedTextMeshProUGUI m_ChainNum;

		// Token: 0x0400A8A3 RID: 43171
		private ExtendedTextMeshProUGUI m_PendulumScaleNum;

		// Token: 0x0400A8A4 RID: 43172
		private SelectionButton m_SelectionButton;

		// Token: 0x0400A8A5 RID: 43173
		private bool m_DarkMode;
	}
}
