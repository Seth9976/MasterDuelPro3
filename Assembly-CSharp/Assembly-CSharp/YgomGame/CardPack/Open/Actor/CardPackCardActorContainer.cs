using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010DE RID: 4318
	public class CardPackCardActorContainer : ActorContainerBase<CardPackCardActorContainer>
	{
		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06008064 RID: 32868 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<CardPackCardActor> allCardActors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06008065 RID: 32869 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem selectHeadCardItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06008066 RID: 32870 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject[] secretKeys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06008067 RID: 32871 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008068 RID: 32872 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cardSlideEffectVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06008069 RID: 32873 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600806A RID: 32874 RVA: 0x0000216D File Offset: 0x0000036D
		public bool pickUpGroupLabelVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x0600806B RID: 32875 RVA: 0x000029CC File Offset: 0x00000BCC
		public int LocatorLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackCardActorContainer Create(ElementObjectManager eom, ActorBindingRefs bindingRefs)
		{
			return null;
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x000029CC File Offset: 0x00000BCC
		public int IndexOf(CardPackCardActor cardActor)
		{
			return 0;
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardCentering(bool isModify, int cardCnt)
		{
		}

		// Token: 0x06008070 RID: 32880 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPackCardActor InsertActor(int idx, ElementObjectManager pref)
		{
			return null;
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x0000216A File Offset: 0x0000036A
		public Renderer GetFrontEffectRenderer(int idx)
		{
			return null;
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPackCardActor GetActorByIdx(int idx)
		{
			return null;
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllActors()
		{
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectHeadActor()
		{
			return false;
		}

		// Token: 0x0400B8C9 RID: 47305
		private readonly string k_ELabelLocatorFormat;

		// Token: 0x0400B8CA RID: 47306
		private readonly string k_ELabelLocatorFoundKey;

		// Token: 0x0400B8CB RID: 47307
		private readonly string k_ELabelLocatorFrontModel;

		// Token: 0x0400B8CC RID: 47308
		private readonly string k_ELabelCardSlideEffectSet;

		// Token: 0x0400B8CD RID: 47309
		private readonly string k_ELabelPickUpSet;

		// Token: 0x0400B8CE RID: 47310
		private readonly string k_ELabelSelectHeadCardItem;

		// Token: 0x0400B8CF RID: 47311
		private const string k_ELabelCardsCenteringLocator = "CardsCenteringLocator";

		// Token: 0x0400B8D0 RID: 47312
		private const string k_TLabelCardsCentering_Default = "Default";

		// Token: 0x0400B8D1 RID: 47313
		private const string k_TLabelCardsCentering_Modify = "Modify";

		// Token: 0x0400B8D2 RID: 47314
		private const string k_TLabelCardsCentering_Modify_01 = "Modify_01";

		// Token: 0x0400B8D3 RID: 47315
		private ActorBindingRefs m_BindingRefs;

		// Token: 0x0400B8D4 RID: 47316
		private Transform[] m_Locators;

		// Token: 0x0400B8D5 RID: 47317
		private GameObject[] m_SecretKeys;

		// Token: 0x0400B8D6 RID: 47318
		private GameObject m_CardSlideEffectSet;

		// Token: 0x0400B8D7 RID: 47319
		private GameObject m_PickUpGroupLabel;

		// Token: 0x0400B8D8 RID: 47320
		private readonly Dictionary<int, CardPackCardActor> m_ActorMap;
	}
}
