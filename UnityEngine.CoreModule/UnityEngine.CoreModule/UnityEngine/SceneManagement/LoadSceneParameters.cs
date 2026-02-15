using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000253 RID: 595
	[Serializable]
	public struct LoadSceneParameters
	{
		// Token: 0x060014D5 RID: 5333 RVA: 0x0002BF89 File Offset: 0x0002A189
		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		// Token: 0x040007C1 RID: 1985
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		// Token: 0x040007C2 RID: 1986
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
