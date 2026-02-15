using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.CardPack.Open.Widget
{
	// Token: 0x020010C3 RID: 4291
	[ExecuteInEditMode]
	public class ChildrenSpriteColorSetter : MonoBehaviour
	{
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x000F68B0 File Offset: 0x000F4AB0
		// (set) Token: 0x06007F79 RID: 32633 RVA: 0x0000216D File Offset: 0x0000036D
		public Color color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateColor()
		{
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateChildren()
		{
		}

		// Token: 0x0400B820 RID: 47136
		[SerializeField]
		private Color m_Color;

		// Token: 0x0400B821 RID: 47137
		[SerializeField]
		private bool m_UpdateEveryFrame;

		// Token: 0x0400B822 RID: 47138
		private Color m_LastColor;

		// Token: 0x0400B823 RID: 47139
		private bool m_Dirty;

		// Token: 0x0400B824 RID: 47140
		private List<SpriteRenderer> m_Children;

		// Token: 0x0400B825 RID: 47141
		private int m_LastChildCount;
	}
}
