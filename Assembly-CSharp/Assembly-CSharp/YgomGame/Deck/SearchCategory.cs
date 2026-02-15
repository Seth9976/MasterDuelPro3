using System;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FF2 RID: 4082
	public class SearchCategory : MonoBehaviour
	{
		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06007B2F RID: 31535 RVA: 0x0000216A File Offset: 0x0000036A
		public SearchCategoryWidget searchCategoryWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x0000216A File Offset: 0x0000036A
		public IAsyncProgressContainer SetParams(int id, string name, bool isSelected)
		{
			return null;
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x0000216A File Offset: 0x0000036A
		private IAsyncProgressContainer SetData(int id, string name)
		{
			return null;
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnClick()
		{
			return false;
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0400B260 RID: 45664
		private ElementObjectManager m_eom;

		// Token: 0x0400B261 RID: 45665
		private GameObject m_ImageOn;

		// Token: 0x0400B262 RID: 45666
		private GameObject m_ImageOff;

		// Token: 0x0400B263 RID: 45667
		private SelectionButton m_button;

		// Token: 0x0400B264 RID: 45668
		private GameObject m_templateObj;

		// Token: 0x0400B265 RID: 45669
		private UnityAction m_OnClickAction;

		// Token: 0x0400B266 RID: 45670
		private SearchCategoryWidget m_SearchCategoryWidget;
	}
}
