using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Prize.TurnOverPrize
{
	// Token: 0x02000A18 RID: 2584
	public class PackActor : ElementWidgetBase
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer packSprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer coverSprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06004AFF RID: 19199 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer arrowSprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public PackActor(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayInConfirm()
		{
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayInConfirm()
		{
			return null;
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayOutConfirm()
		{
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingOut()
		{
			return false;
		}

		// Token: 0x04008921 RID: 35105
		private const string k_ELabelPackSprite = "PackSprite";

		// Token: 0x04008922 RID: 35106
		private const string k_ELabelCoverSprite = "CoverSprite";

		// Token: 0x04008923 RID: 35107
		private const string k_ELabelArrowSprite = "ArrowSprite";

		// Token: 0x04008924 RID: 35108
		private const string k_TLabelIn = "In";

		// Token: 0x04008925 RID: 35109
		private const string k_TLabelLoop = "Loop";

		// Token: 0x04008926 RID: 35110
		private const string k_TLabelOut = "Out";

		// Token: 0x04008927 RID: 35111
		private readonly PlayableDirector m_PlayableDirector;

		// Token: 0x04008928 RID: 35112
		private readonly LabeledPlayableController m_LabeledController;
	}
}
