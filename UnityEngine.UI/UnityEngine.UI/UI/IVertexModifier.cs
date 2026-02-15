using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000084 RID: 132
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use IMeshModifier instead", true)]
	public interface IVertexModifier
	{
		// Token: 0x06000533 RID: 1331
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts)  instead", true)]
		void ModifyVertices(List<UIVertex> verts);
	}
}
