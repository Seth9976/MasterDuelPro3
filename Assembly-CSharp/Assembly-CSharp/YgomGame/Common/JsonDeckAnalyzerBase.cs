using System;
using System.Collections.Generic;

namespace YgomGame.Common
{
	// Token: 0x02001018 RID: 4120
	public abstract class JsonDeckAnalyzerBase
	{
		// Token: 0x06007BE8 RID: 31720 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAccessoryBox(object deckData)
		{
			return 0;
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAccessorySleeve(object deckData)
		{
			return 0;
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetFocusCards(object deckData)
		{
			return null;
		}

		// Token: 0x06007BEB RID: 31723 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetMainCards(object deckData)
		{
			return null;
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetExtraCards(object deckData)
		{
			return null;
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetSideCards(object deckData)
		{
			return null;
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetAccessories(object deckData)
		{
			return null;
		}

		// Token: 0x06007BEF RID: 31727 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetPickupCards(object deckData)
		{
			return null;
		}
	}
}
