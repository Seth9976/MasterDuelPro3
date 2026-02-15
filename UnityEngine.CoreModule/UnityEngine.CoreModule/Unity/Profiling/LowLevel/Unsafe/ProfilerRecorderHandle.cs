using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000032 RID: 50
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public readonly struct ProfilerRecorderHandle
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002EAB File Offset: 0x000010AB
		internal ProfilerRecorderHandle(ulong handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002EB5 File Offset: 0x000010B5
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && this.handle != ulong.MaxValue;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002ED0 File Offset: 0x000010D0
		public static ProfilerRecorderDescription GetDescription(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (flag)
			{
				throw new ArgumentException("ProfilerRecorderHandle is not initialized or is not available", "handle");
			}
			return ProfilerRecorderHandle.GetDescriptionInternal(handle);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F08 File Offset: 0x00001108
		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName__Unmanaged(ProfilerCategory category, byte* name, int nameLen)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName__Unmanaged_Injected(ref category, name, nameLen, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002F24 File Offset: 0x00001124
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerRecorderDescription GetDescriptionInternal(ProfilerRecorderHandle handle)
		{
			ProfilerRecorderDescription profilerRecorderDescription;
			ProfilerRecorderHandle.GetDescriptionInternal_Injected(ref handle, out profilerRecorderDescription);
			return profilerRecorderDescription;
		}

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName__Unmanaged_Injected([In] ref ProfilerCategory category, byte* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptionInternal_Injected([In] ref ProfilerRecorderHandle handle, out ProfilerRecorderDescription ret);

		// Token: 0x0400008D RID: 141
		[FieldOffset(0)]
		internal readonly ulong handle;
	}
}
