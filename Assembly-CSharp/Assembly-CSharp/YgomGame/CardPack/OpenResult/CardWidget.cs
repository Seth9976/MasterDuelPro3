using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010B7 RID: 4279
	public class CardWidget : CardWidget
	{
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06007F20 RID: 32544 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007F21 RID: 32545 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06007F22 RID: 32546 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007F23 RID: 32547 RVA: 0x0000216D File Offset: 0x0000036D
		public int idx
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

		// Token: 0x06007F24 RID: 32548 RVA: 0x000F6894 File Offset: 0x000F4A94
		public CardWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int mrk, int idx, int pRareType, IReadOnlyList<int> shopIds, bool isExpand = false)
		{
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClick()
		{
		}

		// Token: 0x0400B7B5 RID: 47029
		private readonly string k_ELabelSecretPulldown;

		// Token: 0x0400B7B6 RID: 47030
		public readonly SecretPulldownWidget secretPulldownWidget;

		// Token: 0x0400B7B7 RID: 47031
		public Action<CardWidget> onClickCardCallback;

		// Token: 0x0400B7B8 RID: 47032
		public Action<CardWidget> onClickPulldownCallback;

		// Token: 0x0400B7B9 RID: 47033
		public Action<CardWidget> onSelectedCardCallback;

		// Token: 0x0400B7BA RID: 47034
		public Action<CardWidget> onDeselectedCardCallback;

		// Token: 0x0400B7BB RID: 47035
		public Action<CardWidget> onSelectedPulldownCallback;

		// Token: 0x0400B7BC RID: 47036
		public Action<CardWidget> onDeselectedPulldownCallback;
	}
}
