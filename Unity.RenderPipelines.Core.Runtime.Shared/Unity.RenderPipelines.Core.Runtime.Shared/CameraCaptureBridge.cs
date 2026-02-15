using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.RenderPipelines.Core.Runtime.Shared
{
	// Token: 0x02000004 RID: 4
	internal static class CameraCaptureBridge
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCachedCaptureActionsEnumerator(Camera camera)
		{
			return CameraCaptureBridge.GetCachedCaptureActionsEnumerator(camera);
		}
	}
}
