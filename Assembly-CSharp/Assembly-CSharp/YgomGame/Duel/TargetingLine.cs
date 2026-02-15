using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F31 RID: 3889
	public class TargetingLine : MonoBehaviour
	{
		// Token: 0x06007280 RID: 29312 RVA: 0x0000216A File Offset: 0x0000036A
		public static TargetingLine Create(DuelGameObjectManager goManager, DuelEffectPool.Type effectType, bool usePrefabHeight = true, bool setOver3DLayer = true)
		{
			return null;
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(bool setOver3DLayer)
		{
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPosition(Vector3 tailPosition, Vector3 headPosition)
		{
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPositionMain(Vector3 tailPosition, Vector3 headPosition)
		{
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPositionSub(Vector3 tailPosition, Vector3 headPosition)
		{
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x000F6440 File Offset: 0x000F4640
		public Vector3 GetOriginHeadPosition()
		{
			return default(Vector3);
		}

		// Token: 0x06007287 RID: 29319 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRollover(bool active)
		{
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTiling(float tiling)
		{
		}

		// Token: 0x0400AC18 RID: 44056
		private SimpleEffect effect;

		// Token: 0x0400AC19 RID: 44057
		private LineRenderer lineRendererMain;

		// Token: 0x0400AC1A RID: 44058
		private LineRenderer lineRendererSub;

		// Token: 0x0400AC1B RID: 44059
		private Vector3[] originPositionsMain;

		// Token: 0x0400AC1C RID: 44060
		private Vector3[] originPositionsSub;

		// Token: 0x0400AC1D RID: 44061
		private bool usePrefabHeight;
	}
}
