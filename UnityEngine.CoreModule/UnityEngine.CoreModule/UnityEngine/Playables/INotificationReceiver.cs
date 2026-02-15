using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020002FF RID: 767
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		// Token: 0x0600153E RID: 5438
		[RequiredByNativeCode]
		void OnNotify(Playable origin, INotification notification, object context);
	}
}
