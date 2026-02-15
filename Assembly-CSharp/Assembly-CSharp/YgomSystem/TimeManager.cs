using System;

namespace YgomSystem
{
	// Token: 0x020004E3 RID: 1251
	public static class TimeManager
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x0000216D File Offset: 0x0000036D
		public static float gameTimeScale
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float timeScale
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x040028AF RID: 10415
		private static float _gameTimeScale;
	}
}
