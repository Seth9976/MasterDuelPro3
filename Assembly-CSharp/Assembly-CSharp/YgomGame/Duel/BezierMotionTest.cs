using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CAA RID: 3242
	public class BezierMotionTest : MonoBehaviour
	{
		// Token: 0x06005C6B RID: 23659 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x040097EB RID: 38891
		[SerializeField]
		private BezierMotionSetting[] setting;

		// Token: 0x040097EC RID: 38892
		[SerializeField]
		private CameraViewSetting cameraViewSetting;

		// Token: 0x040097ED RID: 38893
		[SerializeField]
		private GameObject originPositionObj;

		// Token: 0x040097EE RID: 38894
		[SerializeField]
		private GameObject targetPositionObj;

		// Token: 0x040097EF RID: 38895
		[SerializeField]
		private GameObject startObj;

		// Token: 0x040097F0 RID: 38896
		[SerializeField]
		private GameObject viaObj;

		// Token: 0x040097F1 RID: 38897
		[SerializeField]
		private GameObject endObj;

		// Token: 0x040097F2 RID: 38898
		[SerializeField]
		private GameObject motionObj;

		// Token: 0x040097F3 RID: 38899
		[SerializeField]
		private float timer;

		// Token: 0x040097F4 RID: 38900
		[SerializeField]
		private bool autoUpdateTimer;

		// Token: 0x040097F5 RID: 38901
		[SerializeField]
		private GameObject[] prefabBg;

		// Token: 0x040097F6 RID: 38902
		private ChainedBezierMotion motion;

		// Token: 0x040097F7 RID: 38903
		[SerializeField]
		private BezierMotionTest.CameraMode cameraMode;

		// Token: 0x040097F8 RID: 38904
		private BezierMotionTest.CameraMode preCameraMode;

		// Token: 0x040097F9 RID: 38905
		[SerializeField]
		private bool loop;

		// Token: 0x02000CAB RID: 3243
		private enum CameraMode
		{
			// Token: 0x040097FB RID: 38907
			Top,
			// Token: 0x040097FC RID: 38908
			Motion
		}
	}
}
