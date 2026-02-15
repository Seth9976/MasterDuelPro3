using System;

namespace AssetStudio
{
	// Token: 0x02000155 RID: 341
	public class ImportedVertex
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00015762 File Offset: 0x00013962
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0001576A File Offset: 0x0001396A
		public Vector3 Vertex { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00015773 File Offset: 0x00013973
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0001577B File Offset: 0x0001397B
		public Vector3 Normal { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0001578C File Offset: 0x0001398C
		public float[][] UV { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00015795 File Offset: 0x00013995
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001579D File Offset: 0x0001399D
		public Vector4 Tangent { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000157A6 File Offset: 0x000139A6
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000157AE File Offset: 0x000139AE
		public Color Color { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000157B7 File Offset: 0x000139B7
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000157BF File Offset: 0x000139BF
		public float[] Weights { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000157C8 File Offset: 0x000139C8
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000157D0 File Offset: 0x000139D0
		public int[] BoneIndices { get; set; }
	}
}
