using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001C RID: 28
	internal struct Extents
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003FB0 File Offset: 0x000021B0
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

		// Token: 0x04000086 RID: 134
		public Vector2 min;

		// Token: 0x04000087 RID: 135
		public Vector2 max;
	}
}
