using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009D6 RID: 2518
	public class ScenarioLoadGroupContainer : MonoBehaviour
	{
		// Token: 0x0600492C RID: 18732 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScenarioLoadGroupContainer Create(GameObject owner, List<ScenarioBehaviour> allBehaviours)
		{
			return null;
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0000216D File Offset: 0x0000036D
		private void Clear()
		{
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool LoadGroup(ScenarioBehaviour_LoadGroup_Begin beginBehaviour)
		{
			return false;
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLoading()
		{
			return false;
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UnloadGroup(ScenarioBehaviour_LoadGroup_End beginBehaviour)
		{
			return false;
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadAll()
		{
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddLoadingCount()
		{
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x0000216D File Offset: 0x0000036D
		private void DecLoadingCount()
		{
		}

		// Token: 0x0400870A RID: 34570
		private List<ScenarioBehaviour> m_AllBehaviours;

		// Token: 0x0400870B RID: 34571
		private int m_LoadingCount;

		// Token: 0x0400870C RID: 34572
		private List<ValueTuple<string, Type>> m_LoadPaths;

		// Token: 0x0400870D RID: 34573
		private List<int> m_LoadMrks;

		// Token: 0x0400870E RID: 34574
		private ScenarioBehaviour_LoadGroup_Begin m_CurrentBeginBehaviour;

		// Token: 0x0400870F RID: 34575
		private ScenarioBehaviour_LoadGroup_End m_CurrentEndBehaviour;
	}
}
