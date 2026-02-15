using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000460 RID: 1120
	internal interface IUIElementsUtility
	{
		// Token: 0x0600210B RID: 8459
		bool TakeCapture();

		// Token: 0x0600210C RID: 8460
		bool ReleaseCapture();

		// Token: 0x0600210D RID: 8461
		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		// Token: 0x0600210E RID: 8462
		bool CleanupRoots();

		// Token: 0x0600210F RID: 8463
		bool EndContainerGUIFromException(Exception exception);

		// Token: 0x06002110 RID: 8464
		bool MakeCurrentIMGUIContainerDirty();
	}
}
