using System;
using UnityEngine;
using YgomSystem.Effect;

namespace YgomSystem.UI
{
	// Token: 0x020005E4 RID: 1508
	public class SelectorCameraSetter : MonoBehaviour
	{
		// Token: 0x0600301A RID: 12314 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0000216A File Offset: 0x0000036A
		private Camera FindCamera()
		{
			return null;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0000216A File Offset: 0x0000036A
		public static Camera FindCameraByParentCanvas(GameObject target)
		{
			return null;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0000216A File Offset: 0x0000036A
		public static Camera FindCameraByScreenEffect(GameObject target, ScreenEffect screenEffect)
		{
			return null;
		}

		// Token: 0x04002CBE RID: 11454
		public SelectorCameraSetter.FindType findType;

		// Token: 0x04002CBF RID: 11455
		public ScreenEffect targetScreenEffect;

		// Token: 0x020005E5 RID: 1509
		public enum FindType
		{
			// Token: 0x04002CC1 RID: 11457
			ByParentCanvas,
			// Token: 0x04002CC2 RID: 11458
			ByScreenEffect
		}
	}
}
