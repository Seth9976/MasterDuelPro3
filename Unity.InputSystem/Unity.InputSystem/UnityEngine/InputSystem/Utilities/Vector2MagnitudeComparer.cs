using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000237 RID: 567
	public struct Vector2MagnitudeComparer : IComparer<Vector2>
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		public int Compare(Vector2 x, Vector2 y)
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
