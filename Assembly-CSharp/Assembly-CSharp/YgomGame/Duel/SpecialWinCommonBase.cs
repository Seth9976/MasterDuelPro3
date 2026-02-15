using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000F23 RID: 3875
	public abstract class SpecialWinCommonBase : SpecialWinBase
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06007210 RID: 29200
		protected abstract string[] labels { get; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06007211 RID: 29201 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool destroyOnWinStart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<string> seList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Setup(PlayableDirector timeline)
		{
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnFinished()
		{
		}

		// Token: 0x0400ABC2 RID: 43970
		protected List<Texture2D> cardTextures;

		// Token: 0x0400ABC3 RID: 43971
		private string labelFront;

		// Token: 0x0400ABC4 RID: 43972
		protected GameObject cardPictureContainer;

		// Token: 0x0400ABC5 RID: 43973
		protected List<int> cardIDs;

		// Token: 0x0400ABC6 RID: 43974
		private List<string> _seList;
	}
}
