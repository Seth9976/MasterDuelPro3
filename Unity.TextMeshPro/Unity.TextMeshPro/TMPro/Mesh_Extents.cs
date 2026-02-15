using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public struct Mesh_Extents
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0002EDB7 File Offset: 0x0002CFB7
		public Mesh_Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002EDC8 File Offset: 0x0002CFC8
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

		// Token: 0x040005D9 RID: 1497
		public Vector2 min;

		// Token: 0x040005DA RID: 1498
		public Vector2 max;
	}
}
