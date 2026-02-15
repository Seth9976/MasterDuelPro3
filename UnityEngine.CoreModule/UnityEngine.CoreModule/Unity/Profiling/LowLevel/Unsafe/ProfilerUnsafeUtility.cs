using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerUnsafeUtility.bindings.h")]
	[IgnoredByDeepProfiler]
	[UsedByNativeCode]
	public static class ProfilerUnsafeUtility
	{
		// Token: 0x0600009B RID: 155
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern ushort CreateCategory__Unmanaged(byte* name, int nameLen, ProfilerCategoryColor colorIndex);

		// Token: 0x0600009C RID: 156 RVA: 0x00002F3C File Offset: 0x0000113C
		[ThreadSafe]
		public static ProfilerCategoryDescription GetCategoryDescription(ushort categoryId)
		{
			ProfilerCategoryDescription profilerCategoryDescription;
			ProfilerUnsafeUtility.GetCategoryDescription_Injected(categoryId, out profilerCategoryDescription);
			return profilerCategoryDescription;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002F54 File Offset: 0x00001154
		[ThreadSafe]
		public unsafe static IntPtr CreateMarker(string name, ushort categoryId, MarkerFlags flags, int metadataCount)
		{
			IntPtr intPtr;
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
				intPtr = ProfilerUnsafeUtility.CreateMarker_Injected(ref managedSpanWrapper, categoryId, flags, metadataCount);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x0600009E RID: 158
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateMarker__Unmanaged(byte* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x0600009F RID: 159
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetMarkerMetadata__Unmanaged(IntPtr markerPtr, int index, byte* name, int nameLen, byte type, byte unit);

		// Token: 0x060000A0 RID: 160
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(IntPtr markerPtr);

		// Token: 0x060000A1 RID: 161
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void BeginSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x060000A2 RID: 162
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(IntPtr markerPtr);

		// Token: 0x060000A3 RID: 163
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void* CreateCounterValue__Unmanaged(out IntPtr counterPtr, byte* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x060000A4 RID: 164 RVA: 0x00002FB0 File Offset: 0x000011B0
		internal unsafe static string Utf8ToString(byte* chars, int charsLen)
		{
			bool flag = chars == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				byte[] arr = new byte[charsLen];
				Marshal.Copy((IntPtr)((void*)chars), arr, 0, charsLen);
				text = Encoding.UTF8.GetString(arr, 0, charsLen);
			}
			return text;
		}

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryDescription_Injected(ushort categoryId, out ProfilerCategoryDescription ret);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateMarker_Injected(ref ManagedSpanWrapper name, ushort categoryId, MarkerFlags flags, int metadataCount);
	}
}
