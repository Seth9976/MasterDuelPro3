using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000636 RID: 1590
	public class TweenScroll : Tween
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x0000216A File Offset: 0x0000036A
		private ScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E7D RID: 11901
		[SerializeField]
		public float from;

		// Token: 0x04002E7E RID: 11902
		[SerializeField]
		public float to;

		// Token: 0x04002E7F RID: 11903
		[SerializeField]
		public TweenScroll.SCROLL_TYPE scrollType;

		// Token: 0x04002E80 RID: 11904
		[SerializeField]
		public bool normalize;

		// Token: 0x04002E81 RID: 11905
		private ScrollRect m_ScrollRect;

		// Token: 0x02000637 RID: 1591
		public enum SCROLL_TYPE
		{
			// Token: 0x04002E83 RID: 11907
			HORIZONTAL,
			// Token: 0x04002E84 RID: 11908
			VERTICAL
		}
	}
}
