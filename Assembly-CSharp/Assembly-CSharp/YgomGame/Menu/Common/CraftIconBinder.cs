using System;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B2A RID: 2858
	public class CraftIconBinder : ResourceBinderBase
	{
		// Token: 0x06005349 RID: 21321 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public CraftIconBinder(string[] craftIconPath)
		{
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetCraftIconPath(int id)
		{
			return null;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindCraftIcon(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x0400911B RID: 37147
		public readonly string[] m_CraftIconPath;
	}
}
