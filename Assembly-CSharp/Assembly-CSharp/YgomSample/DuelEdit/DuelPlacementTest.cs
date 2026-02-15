using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;

namespace YgomSample.DuelEdit
{
	// Token: 0x0200079D RID: 1949
	public class DuelPlacementTest : MonoBehaviour
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06003C9E RID: 15518 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003C9F RID: 15519 RVA: 0x0000216D File Offset: 0x0000036D
		public List<GameObject> cards
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupObjects()
		{
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearObjects()
		{
		}

		// Token: 0x04003515 RID: 13589
		[SerializeField]
		private CameraViewSetting cameraViewSetting;

		// Token: 0x04003516 RID: 13590
		[SerializeField]
		private Transform objectsParent;

		// Token: 0x04003517 RID: 13591
		[SerializeField]
		private GameObject cardPrefab;

		// Token: 0x04003518 RID: 13592
		[SerializeField]
		private GameObject nearFieldPrefab;

		// Token: 0x04003519 RID: 13593
		[SerializeField]
		private GameObject farFieldPrefab;

		// Token: 0x0400351A RID: 13594
		[SerializeField]
		private float matOffset;

		// Token: 0x0400351B RID: 13595
		private GameObject nearMat;

		// Token: 0x0400351C RID: 13596
		private GameObject farMat;
	}
}
