using System;

namespace Spine
{
	// Token: 0x02000077 RID: 119
	public class Polygon
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015EF9 File Offset: 0x000140F9
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015F01 File Offset: 0x00014101
		public float[] Vertices { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00015F0A File Offset: 0x0001410A
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00015F12 File Offset: 0x00014112
		public int Count { get; set; }

		// Token: 0x06000459 RID: 1113 RVA: 0x00015F1B File Offset: 0x0001411B
		public Polygon()
		{
			this.Vertices = new float[16];
		}
	}
}
