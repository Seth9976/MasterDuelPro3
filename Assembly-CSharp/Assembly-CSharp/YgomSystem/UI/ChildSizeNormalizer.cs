using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200057C RID: 1404
	[ExecuteInEditMode]
	public class ChildSizeNormalizer : MonoBehaviour
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform rectTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x0000216D File Offset: 0x0000036D
		private void Reset()
		{
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x0000216D File Offset: 0x0000036D
		public void NormalizeChildSize()
		{
		}

		// Token: 0x04002ACE RID: 10958
		[SerializeField]
		private RectTransform.Axis _mode;

		// Token: 0x04002ACF RID: 10959
		[SerializeField]
		private float _spacing;

		// Token: 0x04002AD0 RID: 10960
		private int childCount;

		// Token: 0x04002AD1 RID: 10961
		private float spacing;

		// Token: 0x04002AD2 RID: 10962
		private RectTransform _rectTransform;

		// Token: 0x04002AD3 RID: 10963
		private RectTransform.Axis mode;

		// Token: 0x04002AD4 RID: 10964
		private Rect rect;
	}
}
