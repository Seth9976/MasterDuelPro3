using System;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method, AllowMultiple = false)]
	[RequiredByNativeCode]
	public sealed class IgnoredByDeepProfilerAttribute : Attribute
	{
	}
}
