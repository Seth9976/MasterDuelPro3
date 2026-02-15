using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200068D RID: 1677
	public class ElementWidgetBase : IAsyncProgressContainer
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IAsyncProgressContent> asyncProgressContents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject gameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform transform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x00002739 File Offset: 0x00000939
		public ElementWidgetBase(ElementObjectManager eom)
		{
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Clear()
		{
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000029CC File Offset: 0x00000BCC
		public static implicit operator bool(ElementWidgetBase exists)
		{
			return false;
		}

		// Token: 0x0400301C RID: 12316
		private List<IAsyncProgressContent> m_AsyncProgressContents;

		// Token: 0x0400301D RID: 12317
		public readonly ElementObjectManager eom;
	}
}
