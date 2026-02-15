using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000019 RID: 25
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h")]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000297C File Offset: 0x00000B7C
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem subsystem = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem inputSubsystem = subsystem as XRInputSubsystem;
			bool flag = inputSubsystem != null && inputSubsystem.trackingOriginUpdated != null;
			if (flag)
			{
				inputSubsystem.trackingOriginUpdated(inputSubsystem);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029B8 File Offset: 0x00000BB8
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem subsystem = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem inputSubsystem = subsystem as XRInputSubsystem;
			bool flag = inputSubsystem != null && inputSubsystem.boundaryChanged != null;
			if (flag)
			{
				inputSubsystem.boundaryChanged(inputSubsystem);
			}
		}

		// Token: 0x0400007C RID: 124
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<XRInputSubsystem> trackingOriginUpdated;

		// Token: 0x0400007D RID: 125
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<XRInputSubsystem> boundaryChanged;
	}
}
