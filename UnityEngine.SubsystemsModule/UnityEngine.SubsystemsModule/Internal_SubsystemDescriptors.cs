using System;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	internal static class Internal_SubsystemDescriptors
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002124 File Offset: 0x00000324
		[RequiredByNativeCode]
		internal static void Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDeprecatedDescriptor(descriptor);
		}
	}
}
