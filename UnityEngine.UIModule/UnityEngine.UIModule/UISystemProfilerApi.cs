using System;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[IgnoredByDeepProfiler]
	[NativeHeader("Modules/UI/Canvas.h")]
	[StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
	public static class UISystemProfilerApi
	{
		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(UISystemProfilerApi.SampleType type);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(UISystemProfilerApi.SampleType type);

		// Token: 0x060000E0 RID: 224 RVA: 0x000035B8 File Offset: 0x000017B8
		public unsafe static void AddMarker(string name, Object obj)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UISystemProfilerApi.AddMarker_Injected(ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(obj));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddMarker_Injected(ref ManagedSpanWrapper name, IntPtr obj);

		// Token: 0x0200000C RID: 12
		public enum SampleType
		{
			// Token: 0x04000017 RID: 23
			Layout,
			// Token: 0x04000018 RID: 24
			Render
		}
	}
}
