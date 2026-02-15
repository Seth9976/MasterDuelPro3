using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000B0 RID: 176
	public struct Extents
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0002ECC5 File Offset: 0x0002CEC5
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002ECD8 File Offset: 0x0002CED8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Min (",
				this.min.x.ToString("f2"),
				", ",
				this.min.y.ToString("f2"),
				")   Max (",
				this.max.x.ToString("f2"),
				", ",
				this.max.y.ToString("f2"),
				")"
			});
		}

		// Token: 0x040005D5 RID: 1493
		internal static Extents zero = new Extents(Vector2.zero, Vector2.zero);

		// Token: 0x040005D6 RID: 1494
		internal static Extents uninitialized = new Extents(new Vector2(32767f, 32767f), new Vector2(-32767f, -32767f));

		// Token: 0x040005D7 RID: 1495
		public Vector2 min;

		// Token: 0x040005D8 RID: 1496
		public Vector2 max;
	}
}
