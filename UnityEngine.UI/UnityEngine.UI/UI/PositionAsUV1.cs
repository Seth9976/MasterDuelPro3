using System;

namespace UnityEngine.UI
{
	// Token: 0x02000087 RID: 135
	[AddComponentMenu("UI/Effects/Position As UV1", 82)]
	public class PositionAsUV1 : BaseMeshEffect
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0001712D File Offset: 0x0001532D
		protected PositionAsUV1()
		{
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00017138 File Offset: 0x00015338
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vert = default(UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vert, i);
				vert.uv1 = new Vector2(vert.position.x, vert.position.y);
				vh.SetUIVertex(vert, i);
			}
		}
	}
}
