using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200061E RID: 1566
	public class TweenCounterText : Tween
	{
		// Token: 0x060031BE RID: 12734 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetCurrentCounterText(float par)
		{
			return null;
		}

		// Token: 0x04002E36 RID: 11830
		[SerializeField]
		public string format;

		// Token: 0x04002E37 RID: 11831
		[SerializeField]
		public float from;

		// Token: 0x04002E38 RID: 11832
		[SerializeField]
		public float to;

		// Token: 0x04002E39 RID: 11833
		private MDText text;

		// Token: 0x04002E3A RID: 11834
		private TMP_Text tmpText;
	}
}
