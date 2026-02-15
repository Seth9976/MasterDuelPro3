using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FEF RID: 4079
	public class PublicDeckBox : MonoBehaviour
	{
		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06007B1C RID: 31516 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DeckID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06007B1D RID: 31517 RVA: 0x0000216A File Offset: 0x0000036A
		public PublicDeckCaseWidget publicDeckCaseWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06007B1E RID: 31518 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007B1F RID: 31519 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06007B20 RID: 31520 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x0000216A File Offset: 0x0000036A
		public IAsyncProgressContainer SetParams(int id, int pickup_id, int caseId)
		{
			return null;
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x0000216A File Offset: 0x0000036A
		private IAsyncProgressContainer SetData(int id, int pickup_id, int caseId)
		{
			return null;
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0400B24E RID: 45646
		private ElementObjectManager m_eom;

		// Token: 0x0400B24F RID: 45647
		private ElementObjectManager m_body;

		// Token: 0x0400B250 RID: 45648
		private GameObject m_CardImage;

		// Token: 0x0400B251 RID: 45649
		private GameObject m_DeckImage;

		// Token: 0x0400B252 RID: 45650
		private SelectionButton m_button;

		// Token: 0x0400B253 RID: 45651
		private GameObject m_publicDeckCaseObj;

		// Token: 0x0400B254 RID: 45652
		private UnityAction m_OnClickAction;

		// Token: 0x0400B255 RID: 45653
		private int m_deckID;

		// Token: 0x0400B256 RID: 45654
		private PublicDeckCaseWidget m_publicDeckCaseWidget;
	}
}
