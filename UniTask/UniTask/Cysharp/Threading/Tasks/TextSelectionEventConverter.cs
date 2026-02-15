using System;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200017B RID: 379
	internal class TextSelectionEventConverter : UnityEvent<ValueTuple<string, int, int>>, IDisposable
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00028912 File Offset: 0x00026B12
		public TextSelectionEventConverter(UnityEvent<string, int, int> unityEvent)
		{
			this.innerEvent = unityEvent;
			this.invokeDelegate = new UnityAction<string, int, int>(this.InvokeCore);
			this.innerEvent.AddListener(this.invokeDelegate);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00028944 File Offset: 0x00026B44
		private void InvokeCore(string item1, int item2, int item3)
		{
			base.Invoke(new ValueTuple<string, int, int>(item1, item2, item3));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00028954 File Offset: 0x00026B54
		public void Dispose()
		{
			this.innerEvent.RemoveListener(this.invokeDelegate);
		}

		// Token: 0x040005CF RID: 1487
		private readonly UnityEvent<string, int, int> innerEvent;

		// Token: 0x040005D0 RID: 1488
		private readonly UnityAction<string, int, int> invokeDelegate;
	}
}
