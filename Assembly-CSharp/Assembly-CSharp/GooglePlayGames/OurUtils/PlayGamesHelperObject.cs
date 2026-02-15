using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x020011AE RID: 4526
	public class PlayGamesHelperObject : MonoBehaviour
	{
		// Token: 0x06008766 RID: 34662 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateObject()
		{
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x0000216D File Offset: 0x0000036D
		public void Awake()
		{
		}

		// Token: 0x06008768 RID: 34664 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDisable()
		{
		}

		// Token: 0x06008769 RID: 34665 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RunCoroutine(IEnumerator action)
		{
		}

		// Token: 0x0600876A RID: 34666 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RunOnGameThread(Action action)
		{
		}

		// Token: 0x0600876B RID: 34667 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x0600876C RID: 34668 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnApplicationFocus(bool focused)
		{
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnApplicationPause(bool paused)
		{
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddFocusCallback(Action<bool> callback)
		{
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool RemoveFocusCallback(Action<bool> callback)
		{
			return false;
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddPauseCallback(Action<bool> callback)
		{
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool RemovePauseCallback(Action<bool> callback)
		{
			return false;
		}

		// Token: 0x0400C1A1 RID: 49569
		private static PlayGamesHelperObject instance;

		// Token: 0x0400C1A2 RID: 49570
		private static bool sIsDummy;

		// Token: 0x0400C1A3 RID: 49571
		private static List<Action> sQueue;

		// Token: 0x0400C1A4 RID: 49572
		private List<Action> localQueue;

		// Token: 0x0400C1A5 RID: 49573
		private static bool sQueueEmpty;

		// Token: 0x0400C1A6 RID: 49574
		private static List<Action<bool>> sPauseCallbackList;

		// Token: 0x0400C1A7 RID: 49575
		private static List<Action<bool>> sFocusCallbackList;
	}
}
