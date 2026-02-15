using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D10 RID: 3344
	public class CardStatus
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600604B RID: 24651 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsShowing
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

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600604D RID: 24653 RVA: 0x0000216D File Offset: 0x0000036D
		public bool ShowFullStatus
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTerminated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006050 RID: 24656 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot cardRoot
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

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x0000216A File Offset: 0x0000036A
		private DuelEffectPool pool
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06006052 RID: 24658 RVA: 0x0000216A File Offset: 0x0000036A
		private ICardStatusIconAnchor dynamicAnchor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x0000216A File Offset: 0x0000036A
		private ICardStatusIconAnchor cardAnchor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardStatus Create(CardRoot cardRoot)
		{
			return null;
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStatusLabel3d()
		{
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x000F534C File Offset: 0x000F354C
		private Vector2 World2ScreenPos(Vector3 pos)
		{
			return default(Vector2);
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartStatusLabel3D(bool immediate = false)
		{
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndStatusLabel3D(bool immediate = false)
		{
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitParameters()
		{
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x0000216D File Offset: 0x0000036D
		public void AtkToDefEffect()
		{
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x0000216D File Offset: 0x0000036D
		public void DefToAtkEffect()
		{
		}

		// Token: 0x04009B80 RID: 39808
		private List<CardStatusEffect> dynamicAnchorEffs;

		// Token: 0x04009B81 RID: 39809
		private CardStatusEffect chainOrderEff;

		// Token: 0x04009B82 RID: 39810
		private CardStatusEffect sacrificeTgtEff;

		// Token: 0x04009B83 RID: 39811
		private CardStatusLabel3D statusLabel3d;
	}
}
