using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/Subsystems/SubsystemDescriptor.h")]
	[UsedByNativeCode("SubsystemDescriptor")]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystemDescriptor<TSubsystem> : IntegratedSubsystemDescriptor where TSubsystem : IntegratedSubsystem
	{
	}
}
