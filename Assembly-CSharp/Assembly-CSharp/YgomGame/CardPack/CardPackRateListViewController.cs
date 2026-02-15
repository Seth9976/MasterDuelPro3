using System;
using System.Collections.Generic;
using YgomGame.CardPack.RateMMAData;
using YgomGame.Menu;

namespace YgomGame.CardPack
{
	// Token: 0x020010A8 RID: 4264
	public class CardPackRateListViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06007EC5 RID: 32453 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int packId, int shopId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x0000216D File Offset: 0x0000036D
		private void LaunchMDMarkupAsset()
		{
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x0000216A File Offset: 0x0000036A
		private IMMAData CreateMMAData(Dictionary<string, object> mmaData)
		{
			return null;
		}

		// Token: 0x0400B77C RID: 46972
		private const string k_ArgPackId = "packId";

		// Token: 0x0400B77D RID: 46973
		private const string k_ArgShopId = "shopId";
	}
}
