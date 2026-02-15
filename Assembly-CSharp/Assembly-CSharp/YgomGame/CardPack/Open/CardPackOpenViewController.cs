using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.CardPack.Open.Actor;
using YgomGame.CardPack.Open.Sequence;
using YgomGame.HeaderFooter;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.CardPack.Open
{
	// Token: 0x020010BE RID: 4286
	public class CardPackOpenViewController : BaseMenuViewController
	{
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06007F57 RID: 32599 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06007F58 RID: 32600 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewController swapTarget = null)
		{
		}

		// Token: 0x06007F5A RID: 32602 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007F5B RID: 32603 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007F5D RID: 32605 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateSequence()
		{
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToResult()
		{
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x0000216D File Offset: 0x0000036D
		private void Skip()
		{
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSkip()
		{
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickOk()
		{
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B7F4 RID: 47092
		private const string k_PrefPath = "CardPack/CardPackOpen";

		// Token: 0x0400B7F5 RID: 47093
		private readonly string k_ELabelRoot3D;

		// Token: 0x0400B7F6 RID: 47094
		private readonly string k_ELabelRootCanvas;

		// Token: 0x0400B7F7 RID: 47095
		private readonly string k_ELabelRootUI;

		// Token: 0x0400B7F8 RID: 47096
		private readonly string k_ELabelRenderTextureCamera;

		// Token: 0x0400B7F9 RID: 47097
		private readonly string k_ELabelBackKey;

		// Token: 0x0400B7FA RID: 47098
		private Dictionary<string, object> m_GachaDrawInfoWork;

		// Token: 0x0400B7FB RID: 47099
		private int m_DrawPackTotal;

		// Token: 0x0400B7FC RID: 47100
		private CardPackRootActorContainer m_RootActorContainer;

		// Token: 0x0400B7FD RID: 47101
		private OutGameFooter m_Footer;

		// Token: 0x0400B7FE RID: 47102
		private SequenceController m_SequenceController;

		// Token: 0x0400B7FF RID: 47103
		private Selector m_Selector3D;

		// Token: 0x0400B800 RID: 47104
		private int m_OverridedCullingMaskBefore;

		// Token: 0x0400B801 RID: 47105
		private Camera m_OverridedCullingMaskCamera;
	}
}
