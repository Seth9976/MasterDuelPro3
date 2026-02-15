using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Duel;
using YgomGame.Menu;

namespace YgomGame.CardBrowser
{
	// Token: 0x020010EB RID: 4331
	public class CardRelativeBrowserViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x060080F2 RID: 33010 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int mrk, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInitRoutine()
		{
			return null;
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400B972 RID: 47474
		private const string k_ArgKey_Mrk = "mrk";

		// Token: 0x0400B973 RID: 47475
		internal const string k_ArgKeySwap = "swap";

		// Token: 0x0400B974 RID: 47476
		internal const string k_ArgKey_SkipSwapTransition = "SkipSwapTransition";

		// Token: 0x0400B975 RID: 47477
		private readonly string k_ELabelCloseButton;

		// Token: 0x0400B976 RID: 47478
		private readonly string k_ELabelContentRoot;

		// Token: 0x0400B977 RID: 47479
		private int m_Mrk;

		// Token: 0x0400B978 RID: 47480
		private IEnumerator m_InitRoutine;

		// Token: 0x0400B979 RID: 47481
		private CardInfoDetailPool.RelativeList m_RelativeList;
	}
}
