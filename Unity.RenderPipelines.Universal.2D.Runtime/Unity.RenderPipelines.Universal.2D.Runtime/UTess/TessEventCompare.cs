using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B1 RID: 177
	internal struct TessEventCompare : IComparer<UEvent>
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
		public int Compare(UEvent a, UEvent b)
		{
			float f = a.a.x - b.a.x;
			if (0f != f)
			{
				if (f <= 0f)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				f = a.a.y - b.a.y;
				if (0f != f)
				{
					if (f <= 0f)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					int i = a.type - b.type;
					if (i != 0)
					{
						return i;
					}
					if (a.type != 0)
					{
						float o = ModuleHandle.OrientFast(a.a, a.b, b.b);
						if (0f != o)
						{
							if (o <= 0f)
							{
								return -1;
							}
							return 1;
						}
					}
					return a.idx - b.idx;
				}
			}
		}
	}
}
