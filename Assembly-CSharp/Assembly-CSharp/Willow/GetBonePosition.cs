using System;
using UnityEngine;

namespace Willow
{
	// Token: 0x0200154D RID: 5453
	[ExecuteAlways]
	public class GetBonePosition : MonoBehaviour
	{
		// Token: 0x06009E31 RID: 40497 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009E32 RID: 40498 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06009E33 RID: 40499 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRootPosProperty(Vector3 position)
		{
		}

		// Token: 0x0400DDC2 RID: 56770
		private static readonly int s_rootPosId;

		// Token: 0x0400DDC3 RID: 56771
		[SerializeField]
		private Transform m_targetRendererTrans;

		// Token: 0x0400DDC4 RID: 56772
		[SerializeField]
		private SkinnedMeshRenderer m_targetRenderer;

		// Token: 0x0400DDC5 RID: 56773
		[SerializeField]
		private Material m_drivenMaterial;
	}
}
