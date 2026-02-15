using System;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x00020CFB File Offset: 0x0001EEFB
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00020D13 File Offset: 0x0001EF13
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x04000619 RID: 1561
		private string m_AddComponentMenu;

		// Token: 0x0400061A RID: 1562
		private int m_Ordering;
	}
}
