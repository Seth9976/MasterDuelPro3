using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010E1 RID: 4321
	public class CardPackPackActorContainer : ActorContainerBase<CardPackPackActorContainer>
	{
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06008088 RID: 32904 RVA: 0x000029CC File Offset: 0x00000BCC
		public int locatorLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06008089 RID: 32905 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackPackActorContainer Create(ElementObjectManager eom, ActorBindingRefs bindingRefs)
		{
			return null;
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPackPackActor InsertActor(int idx, string packTexPath, ElementObjectManager pref)
		{
			return null;
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPackPackActor GetActor(int idx)
		{
			return null;
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x0000216A File Offset: 0x0000036A
		public Renderer GetPackHighlight(int idx)
		{
			return null;
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllActors()
		{
		}

		// Token: 0x0400B8E5 RID: 47333
		private readonly string k_ELabelLocatorFormat;

		// Token: 0x0400B8E6 RID: 47334
		private ActorBindingRefs m_BindingRefs;

		// Token: 0x0400B8E7 RID: 47335
		private Transform[] m_Locators;

		// Token: 0x0400B8E8 RID: 47336
		private readonly Dictionary<int, CardPackPackActor> m_ActorMap;
	}
}
