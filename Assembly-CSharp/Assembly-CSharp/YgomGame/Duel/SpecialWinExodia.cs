using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000F24 RID: 3876
	public class SpecialWinExodia : SpecialWinBase
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06007216 RID: 29206 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<string> seList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize()
		{
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Setup(PlayableDirector timeline)
		{
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnFinished()
		{
		}

		// Token: 0x0400ABC7 RID: 43975
		private List<string> _seList;

		// Token: 0x0400ABC8 RID: 43976
		private List<int> cardIDs;

		// Token: 0x0400ABC9 RID: 43977
		private List<Texture2D> cardTextures;

		// Token: 0x0400ABCA RID: 43978
		private string[] labels;

		// Token: 0x0400ABCB RID: 43979
		private string labelFront;

		// Token: 0x0400ABCC RID: 43980
		private GameObject cardPictureContainer;
	}
}
