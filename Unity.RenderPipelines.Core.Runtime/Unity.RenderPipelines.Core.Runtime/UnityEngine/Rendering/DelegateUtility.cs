using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001BC RID: 444
	public static class DelegateUtility
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F7A0 File Offset: 0x0002D9A0
		public static Delegate Cast(Delegate source, Type type)
		{
			if (source == null)
			{
				return null;
			}
			Delegate[] delegates = source.GetInvocationList();
			if (delegates.Length == 1)
			{
				return Delegate.CreateDelegate(type, delegates[0].Target, delegates[0].Method);
			}
			Delegate[] delegatesDest = new Delegate[delegates.Length];
			for (int nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
			{
				delegatesDest[nDelegate] = Delegate.CreateDelegate(type, delegates[nDelegate].Target, delegates[nDelegate].Method);
			}
			return Delegate.Combine(delegatesDest);
		}
	}
}
