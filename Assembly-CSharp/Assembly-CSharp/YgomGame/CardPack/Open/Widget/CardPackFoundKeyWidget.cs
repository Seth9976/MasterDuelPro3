using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack.Open.Widget
{
	// Token: 0x020010C2 RID: 4290
	public class CardPackFoundKeyWidget : ElementWidgetBehaviourBase<CardPackFoundKeyWidget>
	{
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06007F6F RID: 32623 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007F70 RID: 32624 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlayingPlayable
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06007F71 RID: 32625 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackFoundKeyWidget Create(ElementObjectManager eom, IReadOnlyList<GameObject> cardFoundKeys)
		{
			return null;
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFoundKeys(IReadOnlyList<ValueTuple<int, int>> foundKeys)
		{
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnObtainSecretKey(CardPackFoundKey obtainKey)
		{
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBeginPlayable(PlayableDirector playable)
		{
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEndPlayable(PlayableDirector playable)
		{
		}

		// Token: 0x0400B817 RID: 47127
		private readonly string k_ELabelNumText;

		// Token: 0x0400B818 RID: 47128
		private readonly string k_ELabelKeyIcon;

		// Token: 0x0400B819 RID: 47129
		private readonly string k_ELabelKeyIconBase;

		// Token: 0x0400B81A RID: 47130
		private int m_TotalFoundNum;

		// Token: 0x0400B81B RID: 47131
		private PlayableDirector m_Playable;

		// Token: 0x0400B81C RID: 47132
		private PlayableDirector m_KeyIconPlayable;

		// Token: 0x0400B81D RID: 47133
		private TextMeshPro m_NumText;

		// Token: 0x0400B81E RID: 47134
		private GameObject m_KeyIcon;

		// Token: 0x0400B81F RID: 47135
		private CardPackFoundKey[] m_FoundKeys;
	}
}
