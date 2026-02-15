using System;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000B0 RID: 176
	public static class DOTweenExternalCommand
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000415 RID: 1045 RVA: 0x00011334 File Offset: 0x0000F534
		// (remove) Token: 0x06000416 RID: 1046 RVA: 0x00011368 File Offset: 0x0000F568
		public static event Action<PathOptions, Tween, Quaternion, Transform> SetOrientationOnPath;

		// Token: 0x06000417 RID: 1047 RVA: 0x0001139B File Offset: 0x0000F59B
		internal static void Dispatch_SetOrientationOnPath(PathOptions options, Tween t, Quaternion newRot, Transform trans)
		{
			if (DOTweenExternalCommand.SetOrientationOnPath != null)
			{
				DOTweenExternalCommand.SetOrientationOnPath.Invoke(options, t, newRot, trans);
			}
		}
	}
}
