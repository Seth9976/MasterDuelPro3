using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010DB RID: 4315
	public class CardPackBgActorContainer : ActorContainerBase<CardPackBgActorContainer>
	{
		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06008012 RID: 32786 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer scrollBgRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06008013 RID: 32787 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer scrollBgSubRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06008014 RID: 32788 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer bottomBgRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06008015 RID: 32789 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer bottomBgSubRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSmokeType(int smokeType)
		{
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackBgActorContainer Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0400B897 RID: 47255
		private readonly string k_ELabelBGSmoke01;

		// Token: 0x0400B898 RID: 47256
		private readonly string k_ELabelBGSmoke02;

		// Token: 0x0400B899 RID: 47257
		private readonly string k_ELabelBGSmoke03;

		// Token: 0x0400B89A RID: 47258
		private readonly string k_ELabelScrollBg;

		// Token: 0x0400B89B RID: 47259
		private readonly string k_ELabelScrollBgSub;

		// Token: 0x0400B89C RID: 47260
		private readonly string k_ELabelBottomBg;

		// Token: 0x0400B89D RID: 47261
		private readonly string k_ELabelBottomBgSub;

		// Token: 0x0400B89E RID: 47262
		private SpriteRenderer m_ScrollBgRenderer;

		// Token: 0x0400B89F RID: 47263
		private SpriteRenderer m_ScrollBgSubRenderer;

		// Token: 0x0400B8A0 RID: 47264
		private SpriteRenderer m_BottomBgRenderer;

		// Token: 0x0400B8A1 RID: 47265
		private SpriteRenderer m_BottomBgSubRenderer;
	}
}
