using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/NavMeshManager.h")]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public static class NavMesh
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearPreUpdateListeners()
		{
			NavMesh.onPreUpdate = null;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			bool flag = NavMesh.onPreUpdate != null;
			if (flag)
			{
				NavMesh.onPreUpdate();
			}
		}

		// Token: 0x04000001 RID: 1
		public static NavMesh.OnNavMeshPreUpdate onPreUpdate;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000004 RID: 4
		public delegate void OnNavMeshPreUpdate();
	}
}
