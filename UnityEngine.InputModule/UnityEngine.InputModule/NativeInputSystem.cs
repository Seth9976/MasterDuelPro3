using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/Input/Private/InputModuleBindings.h")]
	[NativeHeader("Modules/Input/Private/InputInternal.h")]
	internal class NativeInputSystem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		public static Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.s_OnDeviceDiscoveredCallback;
			}
			set
			{
				NativeInputSystem.s_OnDeviceDiscoveredCallback = value;
				NativeInputSystem.hasDeviceDiscoveredCallback = NativeInputSystem.s_OnDeviceDiscoveredCallback != null;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		[RequiredByNativeCode]
		internal static void NotifyBeforeUpdate(NativeInputUpdateType updateType)
		{
			Action<NativeInputUpdateType> callback = NativeInputSystem.onBeforeUpdate;
			bool flag = callback != null;
			if (flag)
			{
				callback(updateType);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020AC File Offset: 0x000002AC
		[RequiredByNativeCode]
		internal unsafe static void NotifyUpdate(NativeInputUpdateType updateType, IntPtr eventBuffer)
		{
			NativeUpdateCallback callback = NativeInputSystem.onUpdate;
			NativeInputEventBuffer* eventBufferPtr = (NativeInputEventBuffer*)eventBuffer.ToPointer();
			bool flag = callback == null;
			if (flag)
			{
				eventBufferPtr->eventCount = 0;
				eventBufferPtr->sizeInBytes = 0;
			}
			else
			{
				callback(updateType, eventBufferPtr);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F0 File Offset: 0x000002F0
		[RequiredByNativeCode]
		internal static void NotifyDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			Action<int, string> callback = NativeInputSystem.s_OnDeviceDiscoveredCallback;
			bool flag = callback != null;
			if (flag)
			{
				callback(deviceId, deviceDescriptor);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		[RequiredByNativeCode]
		internal static void ShouldRunUpdate(NativeInputUpdateType updateType, out bool retval)
		{
			Func<NativeInputUpdateType, bool> callback = NativeInputSystem.onShouldRunUpdate;
			retval = callback == null || callback(updateType);
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x0600000A RID: 10
		internal static extern bool hasDeviceDiscoveredCallback
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		} = false;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11
		[NativeProperty(IsThreadSafe = true)]
		public static extern double currentTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12
		[NativeProperty(IsThreadSafe = true)]
		public static extern double currentTimeOffsetToRealtimeSinceStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600000D RID: 13
		[FreeFunction("AllocateInputDeviceId")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AllocateDeviceId();

		// Token: 0x0600000E RID: 14
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void QueueInputEvent(IntPtr inputEvent);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long IOCTL(int deviceId, int code, IntPtr data, int sizeInBytes);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPollingFrequency(float hertz);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update(NativeInputUpdateType updateType);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		[NativeProperty("NormalizeScrollWheelDelta")]
		internal static extern bool normalizeScrollWheelDelta
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetScrollWheelDeltaPerTick();

		// Token: 0x04000017 RID: 23
		public static NativeUpdateCallback onUpdate;

		// Token: 0x04000018 RID: 24
		public static Action<NativeInputUpdateType> onBeforeUpdate;

		// Token: 0x04000019 RID: 25
		public static Func<NativeInputUpdateType, bool> onShouldRunUpdate;

		// Token: 0x0400001A RID: 26
		private static Action<int, string> s_OnDeviceDiscoveredCallback;
	}
}
