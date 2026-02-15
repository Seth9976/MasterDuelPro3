using System;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Test
{
	// Token: 0x020008B5 RID: 2229
	public class MonsterCutinTest : ViewController
	{
		// Token: 0x06004127 RID: 16679 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayCutin()
		{
		}

		// Token: 0x04007F7F RID: 32639
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04007F80 RID: 32640
		[SerializeField]
		private CardIndividualSetting setting;

		// Token: 0x04007F81 RID: 32641
		private int cardID;

		// Token: 0x04007F82 RID: 32642
		private bool useCardParameter;

		// Token: 0x04007F83 RID: 32643
		private bool isMyself;

		// Token: 0x04007F84 RID: 32644
		private Content.Attribute attribute;

		// Token: 0x04007F85 RID: 32645
		private string cardName;

		// Token: 0x04007F86 RID: 32646
		private MonsterCutinEffect.LevelType levelType;

		// Token: 0x04007F87 RID: 32647
		private int level;

		// Token: 0x04007F88 RID: 32648
		private int attack;

		// Token: 0x04007F89 RID: 32649
		private int defense;

		// Token: 0x04007F8A RID: 32650
		private bool isOcg;

		// Token: 0x04007F8B RID: 32651
		private MonsterCutinEffect monsterCutinEffect;

		// Token: 0x04007F8C RID: 32652
		private bool isPlaying;

		// Token: 0x04007F8D RID: 32653
		private ElementObjectManager ui;
	}
}
