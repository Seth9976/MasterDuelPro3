using System;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x0200029F RID: 671
	public class StageRuntimeInterface
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x000443E4 File Offset: 0x000425E4
		public StageRuntimeInterface(Func<bool, GameObject> AddGameObject, Func<Camera> GetCamera, Func<Light> GetSunLight)
		{
			this.m_AddGameObject = AddGameObject;
			this.m_GetCamera = GetCamera;
			this.m_GetSunLight = GetSunLight;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00044401 File Offset: 0x00042601
		public GameObject AddGameObject(bool persistent = false)
		{
			Func<bool, GameObject> addGameObject = this.m_AddGameObject;
			if (addGameObject == null)
			{
				return null;
			}
			return addGameObject(persistent);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00044415 File Offset: 0x00042615
		public Camera camera
		{
			get
			{
				Func<Camera> getCamera = this.m_GetCamera;
				if (getCamera == null)
				{
					return null;
				}
				return getCamera();
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00044428 File Offset: 0x00042628
		public Light sunLight
		{
			get
			{
				Func<Light> getSunLight = this.m_GetSunLight;
				if (getSunLight == null)
				{
					return null;
				}
				return getSunLight();
			}
		}

		// Token: 0x04000BFE RID: 3070
		private Func<bool, GameObject> m_AddGameObject;

		// Token: 0x04000BFF RID: 3071
		private Func<Camera> m_GetCamera;

		// Token: 0x04000C00 RID: 3072
		private Func<Light> m_GetSunLight;

		// Token: 0x04000C01 RID: 3073
		public object SRPData;
	}
}
