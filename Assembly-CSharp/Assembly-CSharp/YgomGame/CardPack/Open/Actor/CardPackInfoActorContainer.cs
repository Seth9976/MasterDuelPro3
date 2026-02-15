using System;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010DF RID: 4319
	public class CardPackInfoActorContainer : ActorContainerBase<CardPackInfoActorContainer>
	{
		// Token: 0x06008076 RID: 32886 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackInfoActorContainer Create(ElementObjectManager eom, ActorBindingRefs bindingRefs)
		{
			return null;
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel(int bandStyle, string labelText)
		{
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabelTween(string tweenKey)
		{
		}

		// Token: 0x0400B8D9 RID: 47321
		private readonly string k_ELabelLabelRoot;

		// Token: 0x0400B8DA RID: 47322
		private readonly string k_ELabelLabelRoot_LabelBand;

		// Token: 0x0400B8DB RID: 47323
		private readonly string k_ELabelLabelRoot_LabelText;

		// Token: 0x0400B8DC RID: 47324
		internal const string k_TLabelShow = "Show";

		// Token: 0x0400B8DD RID: 47325
		internal const string k_TLabelHide = "Hide";

		// Token: 0x0400B8DE RID: 47326
		private ActorBindingRefs m_BindingRefs;

		// Token: 0x0400B8DF RID: 47327
		private ElementObjectManager m_LabelRootEom;
	}
}
