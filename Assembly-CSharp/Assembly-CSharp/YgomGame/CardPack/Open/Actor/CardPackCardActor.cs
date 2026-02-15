using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010DD RID: 4317
	public class CardPackCardActor : ActorBase<CardPackCardActor>
	{
		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600803E RID: 32830 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600803F RID: 32831 RVA: 0x0000216D File Offset: 0x0000036D
		public int mrk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06008040 RID: 32832 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008041 RID: 32833 RVA: 0x0000216D File Offset: 0x0000036D
		public int premium
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06008042 RID: 32834 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008043 RID: 32835 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlayingPlayable
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06008044 RID: 32836 RVA: 0x0000216A File Offset: 0x0000036A
		public PlayableAsset currentPlayableAsset
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06008045 RID: 32837 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06008046 RID: 32838 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008047 RID: 32839 RVA: 0x0000216D File Offset: 0x0000036D
		public bool newIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06008048 RID: 32840 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008049 RID: 32841 RVA: 0x0000216D File Offset: 0x0000036D
		public bool pickupIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600804A RID: 32842 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600804B RID: 32843 RVA: 0x0000216D File Offset: 0x0000036D
		public bool frontCursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x0600804C RID: 32844 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600804D RID: 32845 RVA: 0x0000216D File Offset: 0x0000036D
		public bool backCursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x0600804E RID: 32846 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600804F RID: 32847 RVA: 0x0000216D File Offset: 0x0000036D
		public bool rarityFrameVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06008050 RID: 32848 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06008051 RID: 32849 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008052 RID: 32850 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor> onAcceptKeyEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06008053 RID: 32851 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008054 RID: 32852 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor> onDetailKeyEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06008055 RID: 32853 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008056 RID: 32854 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor, SelectionItem.DragStatus, Vector2> onDragEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06008057 RID: 32855 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008058 RID: 32856 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor> onPointerDownEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06008059 RID: 32857 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600805A RID: 32858 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor> onPointerUpEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x0600805B RID: 32859 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600805C RID: 32860 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<CardPackCardActor> onPointerClickEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackCardActor Create(ElementObjectManager eom, ActorBindingRefs bindingRefs)
		{
			return null;
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int mrk, int premium)
		{
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayPlayable(PlayableAsset playableAsset, DirectorWrapMode wrapMode = DirectorWrapMode.None)
		{
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBeginPlayable(PlayableDirector playable)
		{
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEndPlayable(PlayableDirector playable)
		{
		}

		// Token: 0x0400B8B2 RID: 47282
		private readonly string k_ELabelBackModel;

		// Token: 0x0400B8B3 RID: 47283
		private readonly string k_ELabelFrontModel;

		// Token: 0x0400B8B4 RID: 47284
		private readonly string k_ELabelFrontPremiereModel;

		// Token: 0x0400B8B5 RID: 47285
		private readonly string k_ELabelRareIconSprite;

		// Token: 0x0400B8B6 RID: 47286
		private readonly string k_ELabelNewIcon;

		// Token: 0x0400B8B7 RID: 47287
		private readonly string k_ELabelPickupIcon;

		// Token: 0x0400B8B8 RID: 47288
		private readonly string k_ELabelRarityFrame;

		// Token: 0x0400B8B9 RID: 47289
		private readonly string k_ELabelSelectCursor;

		// Token: 0x0400B8BA RID: 47290
		private readonly string k_ELabelSelectCursorFront;

		// Token: 0x0400B8BB RID: 47291
		private readonly string k_ELabelSelectCursorBack;

		// Token: 0x0400B8BC RID: 47292
		private ActorBindingRefs m_BindingRefs;

		// Token: 0x0400B8BD RID: 47293
		private PlayableDirector m_Playable;

		// Token: 0x0400B8BE RID: 47294
		private SelectionButton m_Button;

		// Token: 0x0400B8BF RID: 47295
		private MeshRenderer m_BackRenderer;

		// Token: 0x0400B8C0 RID: 47296
		private MeshRenderer m_FrontRenderer;

		// Token: 0x0400B8C1 RID: 47297
		private MeshRenderer m_FrontPremireRenderer;

		// Token: 0x0400B8C2 RID: 47298
		private SpriteRenderer m_RareIconRenderer;

		// Token: 0x0400B8C3 RID: 47299
		private SpriteRenderer m_NewIconRenderer;

		// Token: 0x0400B8C4 RID: 47300
		private SpriteRenderer m_PIckupIconRenderer;

		// Token: 0x0400B8C5 RID: 47301
		private GameObject m_RarityFrame;

		// Token: 0x0400B8C6 RID: 47302
		private GameObject m_SelectCursorBack;

		// Token: 0x0400B8C7 RID: 47303
		private GameObject m_SelectCursorFront;

		// Token: 0x0400B8C8 RID: 47304
		private int m_LoadingCnt;
	}
}
