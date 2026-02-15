using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.YGomTMPro
{
	// Token: 0x020004EE RID: 1262
	public class TextMeshProAutoSizer : MonoBehaviour
	{
		// Token: 0x060027F7 RID: 10231 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0000216D File Offset: 0x0000036D
		public void Apply()
		{
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float ApplyFontSize(TMP_Text target, float fitRectHeight, float fontSizeMin, float fontSizeMax = -1f, float sizeDelta = 1f, bool maxToMin = true)
		{
			return 0f;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000029C5 File Offset: 0x00000BC5
		private static float ApplyFontSizeMaxToMin(TMP_Text target, float fitRectHeight, float fontSizeMin, float fontSizeMax, float sizeDelta)
		{
			return 0f;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000029C5 File Offset: 0x00000BC5
		private static float ApplyFontSizeMinToMax(TMP_Text target, float fitRectHeight, float fontSizeMin, float fontSizeMax, float sizeDelta)
		{
			return 0f;
		}

		// Token: 0x040028BB RID: 10427
		[SerializeField]
		private bool autoUpdate;

		// Token: 0x040028BC RID: 10428
		[SerializeField]
		private float checkSizeDelta;

		// Token: 0x040028BD RID: 10429
		private TMP_Text tmpText;

		// Token: 0x040028BE RID: 10430
		private string currentText;

		// Token: 0x040028BF RID: 10431
		private RectTransform rectTransform;
	}
}
