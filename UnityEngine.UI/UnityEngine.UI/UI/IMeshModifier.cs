using System;

namespace UnityEngine.UI
{
	// Token: 0x02000085 RID: 133
	public interface IMeshModifier
	{
		// Token: 0x06000534 RID: 1332
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(Mesh mesh);

		// Token: 0x06000535 RID: 1333
		void ModifyMesh(VertexHelper verts);
	}
}
