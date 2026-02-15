using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h")]
	[NativeConditional("ENABLE_VR")]
	[StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot)]
	public static class InputTracking
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(InputTracking.TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			XRNodeState callbackParam = default(XRNodeState);
			callbackParam.uniqueID = (ulong)uniqueID;
			callbackParam.nodeType = nodeType;
			callbackParam.tracked = tracked;
			Action<XRNodeState> callback;
			switch (eventType)
			{
			case InputTracking.TrackingStateEventType.NodeAdded:
				callback = InputTracking.nodeAdded;
				break;
			case InputTracking.TrackingStateEventType.NodeRemoved:
				callback = InputTracking.nodeRemoved;
				break;
			case InputTracking.TrackingStateEventType.TrackingAcquired:
				callback = InputTracking.trackingAcquired;
				break;
			case InputTracking.TrackingStateEventType.TrackingLost:
				callback = InputTracking.trackingLost;
				break;
			default:
				throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType.ToString());
			}
			bool flag = callback != null;
			if (flag)
			{
				callback(callbackParam);
			}
		}

		// Token: 0x04000001 RID: 1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<XRNodeState> trackingAcquired;

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<XRNodeState> trackingLost;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<XRNodeState> nodeAdded;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<XRNodeState> nodeRemoved;

		// Token: 0x02000003 RID: 3
		private enum TrackingStateEventType
		{
			// Token: 0x04000006 RID: 6
			NodeAdded,
			// Token: 0x04000007 RID: 7
			NodeRemoved,
			// Token: 0x04000008 RID: 8
			TrackingAcquired,
			// Token: 0x04000009 RID: 9
			TrackingLost
		}
	}
}
