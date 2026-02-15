using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200005F RID: 95
	public struct MeshGeneratorBuffers
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F5BA File Offset: 0x0000D7BA
		public Vector2[] uv2Buffer
		{
			get
			{
				return this.meshGenerator.UV2;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
		public Vector2[] uv3Buffer
		{
			get
			{
				return this.meshGenerator.UV3;
			}
		}

		// Token: 0x040001D6 RID: 470
		public int vertexCount;

		// Token: 0x040001D7 RID: 471
		public Vector3[] vertexBuffer;

		// Token: 0x040001D8 RID: 472
		public Vector2[] uvBuffer;

		// Token: 0x040001D9 RID: 473
		public Color32[] colorBuffer;

		// Token: 0x040001DA RID: 474
		public MeshGenerator meshGenerator;
	}
}
