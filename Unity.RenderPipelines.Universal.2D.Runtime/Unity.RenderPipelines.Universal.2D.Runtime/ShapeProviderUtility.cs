using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000097 RID: 151
	internal class ShapeProviderUtility
	{
		// Token: 0x0600037A RID: 890 RVA: 0x000192D0 File Offset: 0x000174D0
		public static void CallOnBeforeRender(ShadowShape2DProvider shapeProvider, Component component, ShadowMesh2D shadowMesh, Bounds bounds)
		{
			if (component != null)
			{
				if (shapeProvider != null && component.gameObject.activeInHierarchy)
				{
					shapeProvider.OnBeforeRender(component, bounds, shadowMesh);
					return;
				}
			}
			else if (shadowMesh != null && shadowMesh.mesh != null)
			{
				shadowMesh.mesh.Clear();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001931C File Offset: 0x0001751C
		public static void PersistantDataCreated(ShadowShape2DProvider shapeProvider, Component component, ShadowMesh2D shadowMesh)
		{
			if (component != null && shapeProvider != null)
			{
				shapeProvider.OnPersistantDataCreated(component, shadowMesh);
			}
		}
	}
}
