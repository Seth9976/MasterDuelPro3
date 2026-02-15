using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EB3 RID: 3763
	public class ListCardBase : MonoBehaviour
	{
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06006DA3 RID: 28067 RVA: 0x000029CC File Offset: 0x00000BCC
		public int cardid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06006DA4 RID: 28068 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton sbtn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006DA5 RID: 28069 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponent()
		{
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(UnityAction onSelected, UnityAction onClick)
		{
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ListCardBaseData data)
		{
		}

		// Token: 0x0400A8A6 RID: 43174
		private ListCardBaseData m_CardData;

		// Token: 0x0400A8A7 RID: 43175
		private ElementObjectManager m_EOManager;

		// Token: 0x0400A8A8 RID: 43176
		private RawImage m_CardPicture;

		// Token: 0x0400A8A9 RID: 43177
		private SelectionButton m_Sbtn;
	}
}
