using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000238 RID: 568
	public struct Vector3MagnitudeComparer : IComparer<Vector3>
	{
		// Token: 0x060014C7 RID: 5319 RVA: 0x0005EA04 File Offset: 0x0005CC04
		public int Compare(Vector3 x, Vector3 y)
		{
			float lenx = x.sqrMagnitude;
			float leny = y.sqrMagnitude;
			if (lenx < leny)
			{
				return -1;
			}
			if (lenx > leny)
			{
				return 1;
			}
			return 0;
		}
	}
}
