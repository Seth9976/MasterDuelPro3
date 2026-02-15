using System;
using System.Collections;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000515 RID: 1301
	public class CoroutineExecuter : MonoBehaviour
	{
		// Token: 0x060029BA RID: 10682 RVA: 0x0000216A File Offset: 0x0000036A
		public static Coroutine Run(IEnumerator coroutine)
		{
			return null;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Terminate(IEnumerator coroutine)
		{
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Terminate(Coroutine coroutine)
		{
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartCallbackCoroutine(Func<bool> func, Action callback)
		{
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator CallbackCoroutine(Func<bool> func, Action callback)
		{
			return null;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallDelay(Action callback, float delaySec, MonoBehaviour caller = null)
		{
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator CallDelayRoutine(Action callback, float delaySec)
		{
			return null;
		}

		// Token: 0x04002956 RID: 10582
		public static CoroutineExecuter Instance;
	}
}
