using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200062D RID: 1581
	public class TweenRotation : Tween
	{
		// Token: 0x060031E5 RID: 12773 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E67 RID: 11879
		public Vector3 from;

		// Token: 0x04002E68 RID: 11880
		public Vector3 to;

		// Token: 0x04002E69 RID: 11881
		public bool quaternionLerp;
	}
}
