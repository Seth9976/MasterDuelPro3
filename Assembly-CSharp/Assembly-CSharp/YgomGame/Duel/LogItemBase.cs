using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EBD RID: 3773
	public class LogItemBase : MonoBehaviour
	{
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06006E0A RID: 28170 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006E0B RID: 28171 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem selectionItemL
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06006E0C RID: 28172 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006E0D RID: 28173 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem selectionItemR
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnCreated(DuelClient host)
		{
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnAdded()
		{
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnRemoved()
		{
		}

		// Token: 0x0400A8C7 RID: 43207
		protected DuelClient m_Host;
	}
}
