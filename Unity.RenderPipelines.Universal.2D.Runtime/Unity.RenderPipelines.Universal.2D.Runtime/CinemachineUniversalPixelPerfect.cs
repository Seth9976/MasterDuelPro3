using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("")]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	public class CinemachineUniversalPixelPerfect : MonoBehaviour
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021F4 File Offset: 0x000003F4
		private void OnEnable()
		{
			Debug.LogError("CinemachineUniversalPixelPerfect is now deprecated and doesn't function properly. Instead, use the one from Cinemachine v2.4.0 or newer.");
		}
	}
}
