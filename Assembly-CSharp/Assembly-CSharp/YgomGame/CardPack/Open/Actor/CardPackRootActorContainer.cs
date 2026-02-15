using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using YgomGame.CardPack.Open.Widget;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010E2 RID: 4322
	public class CardPackRootActorContainer : ActorContainerBase<CardPackRootActorContainer>
	{
		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06008090 RID: 32912 RVA: 0x0000216A File Offset: 0x0000036A
		public PlayableDirector playable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06008091 RID: 32913 RVA: 0x0000216A File Offset: 0x0000036A
		public ActorBindingRefs bindingRefs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06008092 RID: 32914 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008093 RID: 32915 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackBgActorContainer bgContainer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06008094 RID: 32916 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008095 RID: 32917 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackInfoActorContainer infoContainer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06008096 RID: 32918 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008097 RID: 32919 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackPackActorContainer packContainer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06008098 RID: 32920 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008099 RID: 32921 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackCardActorContainer cardContainer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600809A RID: 32922 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600809B RID: 32923 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackFoundKeyWidget foundKeyWidget
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600809C RID: 32924 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600809D RID: 32925 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPackCanvasActorContainer canvasContainer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600809E RID: 32926 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPlaingPlayable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x0600809F RID: 32927 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060080A0 RID: 32928 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isLoopPlayable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060080A1 RID: 32929 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060080A2 RID: 32930 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPausePlayable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060080A3 RID: 32931 RVA: 0x0000216A File Offset: 0x0000036A
		public Canvas reflectionRenderCanvas
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060080A4 RID: 32932 RVA: 0x0000216A File Offset: 0x0000036A
		public RawImage reflectionRenderImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackRootActorContainer Create(ElementObjectManager rootEom, ElementObjectManager root3DEom, ElementObjectManager rootUIEom)
		{
			return null;
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayPlayable(PlayableAsset playableAsset)
		{
		}

		// Token: 0x0400B8E9 RID: 47337
		private readonly string k_ELabelBgRoot;

		// Token: 0x0400B8EA RID: 47338
		private const string k_ELabelInfoGrp = "InfoGrp";

		// Token: 0x0400B8EB RID: 47339
		private readonly string k_ELabelPackGrp;

		// Token: 0x0400B8EC RID: 47340
		private readonly string k_ELabelCardGrp;

		// Token: 0x0400B8ED RID: 47341
		private readonly string k_ELabelReflectionRenderCanvas;

		// Token: 0x0400B8EE RID: 47342
		private readonly string k_ELabelReflectionRenderImage;

		// Token: 0x0400B8EF RID: 47343
		private readonly string k_ELabelFoundKeyTotal;

		// Token: 0x0400B8F0 RID: 47344
		[SerializeField]
		private ActorBindingRefs m_ActorBindingRefs;

		// Token: 0x0400B8F1 RID: 47345
		private PlayableDirector m_Playable;

		// Token: 0x0400B8F2 RID: 47346
		private ElementObjectManager m_Root3DEom;

		// Token: 0x0400B8F3 RID: 47347
		private ElementObjectManager m_RootUIEom;
	}
}
