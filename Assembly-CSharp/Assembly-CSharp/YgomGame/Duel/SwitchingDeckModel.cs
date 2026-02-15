using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F2C RID: 3884
	public class SwitchingDeckModel : MonoBehaviour
	{
		// Token: 0x0600725F RID: 29279 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTopTexture(Texture texture)
		{
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartSwitching(SwitchingDeckModel.MoveType moveType, Action onFinished)
		{
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitOutgoing(Vector3 pos, Action onFinished)
		{
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitOutgoingStep()
		{
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitIncoming(Vector3 pos, Action onFinished)
		{
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitIncomingStep()
		{
		}

		// Token: 0x0400ABFB RID: 44027
		private SwitchingDeckModel.Step step;

		// Token: 0x0400ABFC RID: 44028
		private float time;

		// Token: 0x0400ABFD RID: 44029
		private MeshAlphaFader alphaFader;

		// Token: 0x0400ABFE RID: 44030
		private Vector3 posFrom;

		// Token: 0x0400ABFF RID: 44031
		private Vector3 posTo;

		// Token: 0x0400AC00 RID: 44032
		private Material frontMtrl;

		// Token: 0x0400AC01 RID: 44033
		private const float dulation = 0.5f;

		// Token: 0x0400AC02 RID: 44034
		private const float sideOffset = 5.9f;

		// Token: 0x02000F2D RID: 3885
		public enum MoveType
		{
			// Token: 0x0400AC04 RID: 44036
			Outgoing,
			// Token: 0x0400AC05 RID: 44037
			Incoming
		}

		// Token: 0x02000F2E RID: 3886
		private enum Step
		{
			// Token: 0x0400AC07 RID: 44039
			Idle,
			// Token: 0x0400AC08 RID: 44040
			WaitOutgoing,
			// Token: 0x0400AC09 RID: 44041
			WaitIncoming
		}
	}
}
