using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000188 RID: 392
	internal class DisposeHelper
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x000380EC File Offset: 0x000362EC
		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}
	}
}
