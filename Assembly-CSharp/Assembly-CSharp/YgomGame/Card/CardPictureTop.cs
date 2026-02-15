using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem;

namespace YgomGame.Card
{
	// Token: 0x0200110F RID: 4367
	public class CardPictureTop : MonoBehaviour
	{
		// Token: 0x060081ED RID: 33261 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPictureTop Create(Transform parent)
		{
			return null;
		}

		// Token: 0x060081EE RID: 33262 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTopArea(int cardid, bool maskmode)
		{
		}

		// Token: 0x060081EF RID: 33263 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardName(int cardid, bool maskmode)
		{
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLevelRank(int cardid, bool maskmode)
		{
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLevelIcon(int level, bool maskmode)
		{
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRankIcon(int rank, bool maskmode)
		{
		}

		// Token: 0x0400BA51 RID: 47697
		private const int MAXLEVEL = 12;

		// Token: 0x0400BA52 RID: 47698
		private const string prefabPath = "Prefabs/Duel/CardPictureTop";

		// Token: 0x0400BA53 RID: 47699
		[SerializeField]
		private RubyRoot rubyRoot;

		// Token: 0x0400BA54 RID: 47700
		[SerializeField]
		private GameObject LevelRoot;

		// Token: 0x0400BA55 RID: 47701
		[SerializeField]
		private Sprite[] LevelSprites;

		// Token: 0x0400BA56 RID: 47702
		[SerializeField]
		private Image[] LevelIcons;

		// Token: 0x0400BA57 RID: 47703
		[SerializeField]
		private int nameFontSize;

		// Token: 0x0400BA58 RID: 47704
		[SerializeField]
		private int levelIconWidth;

		// Token: 0x0400BA59 RID: 47705
		[SerializeField]
		private Color monsterNameColor;

		// Token: 0x0400BA5A RID: 47706
		[SerializeField]
		private Color spellTrapNameColor;
	}
}
