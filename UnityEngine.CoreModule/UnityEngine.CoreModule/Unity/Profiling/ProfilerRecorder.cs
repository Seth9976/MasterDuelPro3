using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200002A RID: 42
	[DebuggerTypeProxy(typeof(ProfilerRecorderDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerRecorder.bindings.h")]
	public struct ProfilerRecorder : IDisposable
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002C92 File Offset: 0x00000E92
		public ProfilerRecorder(ProfilerRecorderHandle statHandle, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(statHandle, capacity, options);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CA3 File Offset: 0x00000EA3
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && ProfilerRecorder.GetValid(this);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CBB File Offset: 0x00000EBB
		public void Start()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Start);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public void Stop()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Stop);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002CEC File Offset: 0x00000EEC
		public long LastValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValue(this);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002D10 File Offset: 0x00000F10
		public int Count
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.Count);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002D38 File Offset: 0x00000F38
		public bool IsRunning
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetRunning(this);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002D5C File Offset: 0x00000F5C
		public ProfilerRecorderSample GetSample(int index)
		{
			this.CheckInitializedAndThrow();
			return ProfilerRecorder.GetSampleInternal(this, index);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002D84 File Offset: 0x00000F84
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorder Create(ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options)
		{
			ProfilerRecorder profilerRecorder;
			ProfilerRecorder.Create_Injected(ref statHandle, maxSampleCount, options, out profilerRecorder);
			return profilerRecorder;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static void Control(ProfilerRecorder handle, ProfilerRecorder.ControlOptions options)
		{
			ProfilerRecorder.Control_Injected(ref handle, options);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002DB8 File Offset: 0x00000FB8
		[NativeMethod(IsThreadSafe = true)]
		private static long GetLastValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValue_Injected(ref handle);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002DCC File Offset: 0x00000FCC
		[NativeMethod(IsThreadSafe = true)]
		private static int GetCount(ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions)
		{
			return ProfilerRecorder.GetCount_Injected(ref handle, countOptions);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002DE4 File Offset: 0x00000FE4
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetValid(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValid_Injected(ref handle);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetRunning(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetRunning_Injected(ref handle);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002E0C File Offset: 0x0000100C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorderSample GetSampleInternal(ProfilerRecorder handle, int index)
		{
			ProfilerRecorderSample profilerRecorderSample;
			ProfilerRecorder.GetSampleInternal_Injected(ref handle, index, out profilerRecorderSample);
			return profilerRecorderSample;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002E24 File Offset: 0x00001024
		public void Dispose()
		{
			bool flag = this.handle == 0UL;
			if (!flag)
			{
				ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Release);
				this.handle = 0UL;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002E58 File Offset: 0x00001058
		[BurstDiscard]
		private void CheckInitializedAndThrow()
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
		}

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected([In] ref ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options, out ProfilerRecorder ret);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Control_Injected([In] ref ProfilerRecorder handle, ProfilerRecorder.ControlOptions options);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLastValue_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCount_Injected([In] ref ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetValid_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRunning_Injected([In] ref ProfilerRecorder handle);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSampleInternal_Injected([In] ref ProfilerRecorder handle, int index, out ProfilerRecorderSample ret);

		// Token: 0x04000061 RID: 97
		internal ulong handle;

		// Token: 0x04000062 RID: 98
		internal const ProfilerRecorderOptions SharedRecorder = (ProfilerRecorderOptions)128;

		// Token: 0x0200002B RID: 43
		internal enum ControlOptions
		{
			// Token: 0x04000064 RID: 100
			Start,
			// Token: 0x04000065 RID: 101
			Stop,
			// Token: 0x04000066 RID: 102
			Reset,
			// Token: 0x04000067 RID: 103
			Release = 4,
			// Token: 0x04000068 RID: 104
			SetFilterToCurrentThread,
			// Token: 0x04000069 RID: 105
			SetToCollectFromAllThreads
		}

		// Token: 0x0200002C RID: 44
		internal enum CountOptions
		{
			// Token: 0x0400006B RID: 107
			Count,
			// Token: 0x0400006C RID: 108
			MaxCount
		}
	}
}
