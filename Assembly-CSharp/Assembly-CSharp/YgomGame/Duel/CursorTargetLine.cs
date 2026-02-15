using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D1F RID: 3359
	public class CursorTargetLine : MonoBehaviour
	{
		// Token: 0x06006128 RID: 24872 RVA: 0x0000216A File Offset: 0x0000036A
		public static CursorTargetLine Create()
		{
			return null;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x0000216D File Offset: 0x0000036D
		private void LineRendererUpdate()
		{
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBase(Vector3 pos1)
		{
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMoving(Vector3 pos2)
		{
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowLine()
		{
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideLine()
		{
		}

		// Token: 0x04009C44 RID: 40004
		private Vector3 point1;

		// Token: 0x04009C45 RID: 40005
		private Vector3 point2;

		// Token: 0x04009C46 RID: 40006
		private int middlePoints;

		// Token: 0x04009C47 RID: 40007
		private Vector3 controlPoint;

		// Token: 0x04009C48 RID: 40008
		private LineRenderer render;

		// Token: 0x04009C49 RID: 40009
		private bool isActive;
	}
}
