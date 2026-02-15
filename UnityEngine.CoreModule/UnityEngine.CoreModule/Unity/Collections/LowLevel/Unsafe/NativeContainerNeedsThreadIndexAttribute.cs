using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200006D RID: 109
	[RequiredByNativeCode]
	[Obsolete("Use NativeSetThreadIndexAttribute instead")]
	[AttributeUsage(AttributeTargets.Struct)]
	public sealed class NativeContainerNeedsThreadIndexAttribute : Attribute
	{
	}
}
