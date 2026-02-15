using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x0200058C RID: 1420
	public class ContentSizeFilterForTMP : MonoBehaviour
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06002CDA RID: 11482 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002CDB RID: 11483 RVA: 0x0000216D File Offset: 0x0000036D
		public event UnityAction onResized
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04002B16 RID: 11030
		[SerializeField]
		private float minWidth;

		// Token: 0x04002B17 RID: 11031
		[SerializeField]
		private float minHeight;

		// Token: 0x04002B18 RID: 11032
		[SerializeField]
		private TextMeshProUGUI m_TargetTmp;

		// Token: 0x04002B19 RID: 11033
		private RectTransform rectTransform;

		// Token: 0x04002B1A RID: 11034
		private bool m_UpdateSize;

		// Token: 0x04002B1B RID: 11035
		public bool EnableWidthControl;

		// Token: 0x04002B1C RID: 11036
		public bool EnableHeightControl;
	}
}
