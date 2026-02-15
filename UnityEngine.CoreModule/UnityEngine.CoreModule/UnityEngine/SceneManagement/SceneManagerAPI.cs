using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200024F RID: 591
	public class SceneManagerAPI
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0002BD28 File Offset: 0x00029F28
		internal static SceneManagerAPI ActiveAPI
		{
			get
			{
				return SceneManagerAPI.overrideAPI ?? SceneManagerAPI.s_DefaultAPI;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0002BD38 File Offset: 0x00029F38
		public static SceneManagerAPI overrideAPI { get; }

		// Token: 0x060014BE RID: 5310 RVA: 0x000205EB File Offset: 0x0001E7EB
		protected internal SceneManagerAPI()
		{
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0002BD3F File Offset: 0x00029F3F
		protected internal virtual AsyncOperation LoadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0002144D File Offset: 0x0001F64D
		protected internal virtual AsyncOperation LoadFirstScene(bool mustLoadAsync)
		{
			return null;
		}

		// Token: 0x040007B4 RID: 1972
		private static SceneManagerAPI s_DefaultAPI = new SceneManagerAPI();
	}
}
