using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Scenario
{
	// Token: 0x020009D0 RID: 2512
	public class ScenarioCardPopActor : ElementWidgetBase
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004903 RID: 18691 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004905 RID: 18693 RVA: 0x0000216D File Offset: 0x0000036D
		public int subMrk
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

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ready
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ScenarioCardPopActor(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardPopPath(int mrk)
		{
			return null;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Clear()
		{
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearSub()
		{
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureSub()
		{
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int mrk)
		{
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideFront()
		{
		}

		// Token: 0x040086F5 RID: 34549
		private readonly string k_ELabelSprite;

		// Token: 0x040086F6 RID: 34550
		private readonly string k_ELabelSubSprite;

		// Token: 0x040086F7 RID: 34551
		private const string k_PopResourceFormat = "Scenarios/CardPop/<_CARD_ILLUST_>/CardPop{0:D4}";

		// Token: 0x040086F8 RID: 34552
		public readonly SpriteRenderer spriteRenderer;

		// Token: 0x040086F9 RID: 34553
		public readonly SpriteRenderer subSpriteRenderer;

		// Token: 0x040086FA RID: 34554
		private int m_LoadingCnt;
	}
}
