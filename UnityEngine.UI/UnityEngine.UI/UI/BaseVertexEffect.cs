using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000082 RID: 130
	[Obsolete("Use BaseMeshEffect instead", true)]
	public abstract class BaseVertexEffect
	{
		// Token: 0x0600052A RID: 1322
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use BaseMeshEffect.ModifyMeshes instead", true)]
		public abstract void ModifyVertices(List<UIVertex> vertices);
	}
}
