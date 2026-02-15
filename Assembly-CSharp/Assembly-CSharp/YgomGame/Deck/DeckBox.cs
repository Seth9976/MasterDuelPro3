using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FC5 RID: 4037
	public class DeckBox : MonoBehaviour
	{
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060078C3 RID: 30915 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060078C4 RID: 30916 RVA: 0x0000216D File Offset: 0x0000036D
		public bool m_isSelected
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (set) Token: 0x060078C5 RID: 30917 RVA: 0x0000216D File Offset: 0x0000036D
		public string SoundLabelClick
		{
			set
			{
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060078C6 RID: 30918 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060078C7 RID: 30919 RVA: 0x0000216D File Offset: 0x0000036D
		public int deckID
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060078C8 RID: 30920 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060078C9 RID: 30921 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060078CA RID: 30922 RVA: 0x0000216D File Offset: 0x0000036D
		public string deckName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060078CB RID: 30923 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCaseWidget deckCaseWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060078CC RID: 30924 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060078CD RID: 30925 RVA: 0x0000216D File Offset: 0x0000036D
		public DeckSelectViewController2.DeckCondition m_Condition
		{
			[CompilerGenerated]
			get
			{
				return DeckSelectViewController2.DeckCondition.New;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTweenCallback()
		{
		}

		// Token: 0x060078D1 RID: 30929 RVA: 0x0000216A File Offset: 0x0000036A
		public IAsyncProgressContainer SetAsCreateButton()
		{
			return null;
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x0000216A File Offset: 0x0000036A
		public IAsyncProgressContainer SetAsDeck(int id, string name, int case_id, int protector_id, int[] pickup_ids, int[] pickup_decos, bool opened = false, bool setAsNewButton = false, bool isDeletemode = false, bool isSelected = false)
		{
			return null;
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x0000216A File Offset: 0x0000036A
		private IAsyncProgressContainer SetData(int id, string name, int case_id, int protector_id, int[] pickup_ids, int[] pickup_decos, bool opened = false, bool setAsNewButton = false, bool isDeletemode = false, bool isSelected = false)
		{
			return null;
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCurrentDeckIcon(bool disp)
		{
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetCurrentDeckIconDisp()
		{
			return false;
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisableDeckIcon(bool disp)
		{
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400B08C RID: 45196
		protected ElementObjectManager m_eom;

		// Token: 0x0400B08D RID: 45197
		protected ElementObjectManager m_body;

		// Token: 0x0400B08E RID: 45198
		protected TextMeshProUGUI m_DeckNameText;

		// Token: 0x0400B08F RID: 45199
		protected GameObject m_CreateDeckIcon;

		// Token: 0x0400B090 RID: 45200
		protected GameObject m_DisabledIcon;

		// Token: 0x0400B091 RID: 45201
		protected GameObject m_CurrentDeckIcon;

		// Token: 0x0400B092 RID: 45202
		protected SelectionButton m_button;

		// Token: 0x0400B093 RID: 45203
		private ElementObjectManager m_SelectionToggle;

		// Token: 0x0400B094 RID: 45204
		private GameObject m_ToggleOn;

		// Token: 0x0400B095 RID: 45205
		public Image m_regImage;

		// Token: 0x0400B096 RID: 45206
		protected GameObject m_deckCaseObj;

		// Token: 0x0400B097 RID: 45207
		protected UnityAction m_OnClickAction;

		// Token: 0x0400B098 RID: 45208
		protected int m_deckID;

		// Token: 0x0400B099 RID: 45209
		public Action onSelectedTweenCallback;

		// Token: 0x0400B09A RID: 45210
		public Action onDeSelectedTweenCallback;

		// Token: 0x0400B09B RID: 45211
		protected string m_deckName;

		// Token: 0x0400B09C RID: 45212
		protected DeckCaseWidget m_deckCaseWidget;
	}
}
