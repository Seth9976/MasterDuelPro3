using System;
using UnityEngine;

namespace YgomGame.Card
{
	// Token: 0x020010FC RID: 4348
	public class CardIconSprites : ScriptableObject
	{
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600815F RID: 33119 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardIconSprites instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetAttributeIcon(Content.Attribute attr)
		{
			return null;
		}

		// Token: 0x06008161 RID: 33121 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetSpellTrapIcon(Content.Icon type)
		{
			return null;
		}

		// Token: 0x06008162 RID: 33122 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetTypeIcon(Content.Type type)
		{
			return null;
		}

		// Token: 0x06008163 RID: 33123 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action onLoaded)
		{
		}

		// Token: 0x0400B9E7 RID: 47591
		[SerializeField]
		private CardIconSprites.AttributeIconTable[] attrIcons;

		// Token: 0x0400B9E8 RID: 47592
		[SerializeField]
		private CardIconSprites.SpellTrapIconTable[] spelltrapIcons;

		// Token: 0x0400B9E9 RID: 47593
		[SerializeField]
		private CardIconSprites.TypeIconTable[] typeIcons;

		// Token: 0x0400B9EA RID: 47594
		private static CardIconSprites _instance;

		// Token: 0x0400B9EB RID: 47595
		private const string path = "Card/ScriptableObjects/CardIconSprites";

		// Token: 0x020010FD RID: 4349
		[Serializable]
		public class AttributeIconTable
		{
			// Token: 0x0400B9EC RID: 47596
			public Content.Attribute attr;

			// Token: 0x0400B9ED RID: 47597
			public Sprite icon;
		}

		// Token: 0x020010FE RID: 4350
		[Serializable]
		public class SpellTrapIconTable
		{
			// Token: 0x0400B9EE RID: 47598
			public Content.Icon type;

			// Token: 0x0400B9EF RID: 47599
			public Sprite icon;
		}

		// Token: 0x020010FF RID: 4351
		[Serializable]
		public class TypeIconTable
		{
			// Token: 0x0400B9F0 RID: 47600
			public Content.Type type;

			// Token: 0x0400B9F1 RID: 47601
			public Sprite icon;
		}
	}
}
