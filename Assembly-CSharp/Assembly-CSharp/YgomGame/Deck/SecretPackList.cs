using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Menu;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Deck
{
	// Token: 0x02001004 RID: 4100
	public class SecretPackList : BaseMenuViewController, IBokeSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06007B70 RID: 31600 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreateEntity(GameObject obj)
		{
		}

		// Token: 0x06007B74 RID: 31604 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject obj, int idx)
		{
		}

		// Token: 0x0400B315 RID: 45845
		private List<CardCollectionInfo.SecretPackInfo> packList;

		// Token: 0x0400B316 RID: 45846
		private InfinityScrollView infinityScroll;

		// Token: 0x0400B317 RID: 45847
		private Action<int, List<int>> decideCallback;

		// Token: 0x0400B318 RID: 45848
		public const string argsKeyPackList = "PackList";

		// Token: 0x0400B319 RID: 45849
		public const string argsKeyCallback = "DecideCallback";
	}
}
