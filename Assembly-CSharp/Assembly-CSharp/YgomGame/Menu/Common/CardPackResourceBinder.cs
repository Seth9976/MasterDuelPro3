using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B25 RID: 2853
	public class CardPackResourceBinder : ResourceBinderBase, IItemCardTicketBinder
	{
		// Token: 0x0600532F RID: 21295 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public CardPackResourceBinder(string packTicket, string packTexPath)
		{
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPackTicketPath(int itemId = 0)
		{
			return null;
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindPackTicket(Image target, int packTicketId = 0, bool async = true)
		{
			return null;
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPackTexPath(string packImageName)
		{
			return null;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindCardPackImage(Image target, string packImageName, bool async = true)
		{
			return null;
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0000216A File Offset: 0x0000036A
		public Component BindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x04009113 RID: 37139
		public readonly string m_PackTicketPath;

		// Token: 0x04009114 RID: 37140
		public readonly string m_PackTexPath;
	}
}
