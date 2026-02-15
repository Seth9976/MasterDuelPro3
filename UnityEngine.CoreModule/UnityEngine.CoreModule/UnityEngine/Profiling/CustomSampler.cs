using System;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x020001F2 RID: 498
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[NativeHeader("Runtime/Profiler/Marker.h")]
	public sealed class CustomSampler : Sampler
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x000294CE File Offset: 0x000276CE
		internal CustomSampler()
		{
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000294D8 File Offset: 0x000276D8
		private CustomSampler(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000294E4 File Offset: 0x000276E4
		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr nativeSampler = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.AvailabilityNonDevelopment | (collectGpuData ? MarkerFlags.SampleGPU : MarkerFlags.Default), 0);
			bool flag = nativeSampler == IntPtr.Zero;
			CustomSampler customSampler;
			if (flag)
			{
				customSampler = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				customSampler = new CustomSampler(nativeSampler);
			}
			return customSampler;
		}

		// Token: 0x04000720 RID: 1824
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();

		// Token: 0x020001F3 RID: 499
		internal static class BindingsMarshaller
		{
			// Token: 0x0600139B RID: 5019 RVA: 0x00029535 File Offset: 0x00027735
			public static IntPtr ConvertToNative(CustomSampler customSampler)
			{
				return customSampler.m_Ptr;
			}
		}
	}
}
