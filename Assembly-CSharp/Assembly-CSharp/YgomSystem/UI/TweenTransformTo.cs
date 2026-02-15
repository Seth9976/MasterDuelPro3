using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000644 RID: 1604
	public class TweenTransformTo : Tween
	{
		// Token: 0x06003225 RID: 12837 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002EC5 RID: 11973
		private Vector3 fromPosition;

		// Token: 0x04002EC6 RID: 11974
		private Quaternion fromRotation;

		// Token: 0x04002EC7 RID: 11975
		private Vector3 fromScale;

		// Token: 0x04002EC8 RID: 11976
		[SerializeField]
		public Transform to;
	}
}
