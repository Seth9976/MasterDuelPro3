using System;
using Cinemachine;
using UnityEngine;

namespace Willow
{
	// Token: 0x02001548 RID: 5448
	public class CameraSync : MonoBehaviour
	{
		// Token: 0x06009DE0 RID: 40416 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06009DE1 RID: 40417 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06009DE2 RID: 40418 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCamera()
		{
		}

		// Token: 0x0400DDA4 RID: 56740
		[SerializeField]
		private Camera m_camera;

		// Token: 0x0400DDA5 RID: 56741
		[SerializeField]
		private CinemachineVirtualCamera m_vcam;
	}
}
