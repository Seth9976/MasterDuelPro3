using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001A RID: 26
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeConditional("ENABLE_XR")]
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h")]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
	}
}
