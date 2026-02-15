using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Friend
{
	// Token: 0x02000C16 RID: 3094
	public class PlayerContextCollection
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0000216A File Offset: 0x0000036A
		public IPlayerContext Item
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06005847 RID: 22599 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator<IPlayerContext> GetEnumerator()
		{
			return null;
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
		{
			return null;
		}

		// Token: 0x040094A6 RID: 38054
		protected readonly List<IPlayerContext> m_PlayerContexts;
	}
}
