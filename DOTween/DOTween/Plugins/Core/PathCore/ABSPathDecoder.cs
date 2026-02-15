using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x0200009A RID: 154
	internal abstract class ABSPathDecoder
	{
		// Token: 0x0600038B RID: 907
		internal abstract void FinalizePath(Path p, Vector3[] wps, bool isClosedPath);

		// Token: 0x0600038C RID: 908
		internal abstract Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600038D RID: 909
		internal abstract int minInputWaypoints { get; }
	}
}
