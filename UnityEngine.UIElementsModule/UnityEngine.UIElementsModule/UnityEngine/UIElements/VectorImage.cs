using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	public sealed class VectorImage : ScriptableObject
	{
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0007FDBE File Offset: 0x0007DFBE
		public float width
		{
			get
			{
				return this.size.x;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0007FDCB File Offset: 0x0007DFCB
		public float height
		{
			get
			{
				return this.size.y;
			}
		}

		// Token: 0x04000F7F RID: 3967
		[SerializeField]
		internal int version = 0;

		// Token: 0x04000F80 RID: 3968
		[SerializeField]
		internal Texture2D atlas = null;

		// Token: 0x04000F81 RID: 3969
		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		// Token: 0x04000F82 RID: 3970
		[SerializeField]
		internal ushort[] indices = null;

		// Token: 0x04000F83 RID: 3971
		[SerializeField]
		internal GradientSettings[] settings = null;

		// Token: 0x04000F84 RID: 3972
		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
