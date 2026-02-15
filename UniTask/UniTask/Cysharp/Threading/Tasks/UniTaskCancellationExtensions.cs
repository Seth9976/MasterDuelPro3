using System;
using System.Threading;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000074 RID: 116
	public static class UniTaskCancellationExtensions
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00005E6C File Offset: 0x0000406C
		public static CancellationToken GetCancellationTokenOnDestroy(this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.destroyCancellationToken;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005E74 File Offset: 0x00004074
		public static CancellationToken GetCancellationTokenOnDestroy(this GameObject gameObject)
		{
			return gameObject.GetAsyncDestroyTrigger().CancellationToken;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005E84 File Offset: 0x00004084
		public static CancellationToken GetCancellationTokenOnDestroy(this Component component)
		{
			MonoBehaviour mb = component as MonoBehaviour;
			if (mb != null)
			{
				return mb.destroyCancellationToken;
			}
			return component.GetAsyncDestroyTrigger().CancellationToken;
		}
	}
}
