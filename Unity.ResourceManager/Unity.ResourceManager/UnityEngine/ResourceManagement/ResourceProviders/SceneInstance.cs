using System;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000058 RID: 88
	public struct SceneInstance
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008EE6 File Offset: 0x000070E6
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00008EEE File Offset: 0x000070EE
		public Scene Scene
		{
			get
			{
				return this.m_Scene;
			}
			internal set
			{
				this.m_Scene = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008EF7 File Offset: 0x000070F7
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00008EFF File Offset: 0x000070FF
		internal bool ReleaseSceneOnSceneUnloaded
		{
			get
			{
				return this.m_ReleaseOnSceneUnloaded;
			}
			set
			{
				this.m_ReleaseOnSceneUnloaded = value;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008F08 File Offset: 0x00007108
		public AsyncOperation ActivateAsync()
		{
			this.m_Operation.allowSceneActivation = true;
			return this.m_Operation;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008F1C File Offset: 0x0000711C
		public override int GetHashCode()
		{
			return this.Scene.GetHashCode();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008F40 File Offset: 0x00007140
		public override bool Equals(object obj)
		{
			return obj is SceneInstance && this.Scene.Equals(((SceneInstance)obj).Scene);
		}

		// Token: 0x040000ED RID: 237
		private Scene m_Scene;

		// Token: 0x040000EE RID: 238
		private bool m_ReleaseOnSceneUnloaded;

		// Token: 0x040000EF RID: 239
		internal AsyncOperation m_Operation;
	}
}
