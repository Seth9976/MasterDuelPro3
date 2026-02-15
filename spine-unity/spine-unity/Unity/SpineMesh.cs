using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000065 RID: 101
	public static class SpineMesh
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00012A2B File Offset: 0x00010C2B
		public static Mesh NewSkeletonMesh()
		{
			Mesh mesh = new Mesh();
			mesh.MarkDynamic();
			mesh.name = "Skeleton Mesh";
			mesh.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
			return mesh;
		}

		// Token: 0x04000204 RID: 516
		internal const HideFlags MeshHideflags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
	}
}
