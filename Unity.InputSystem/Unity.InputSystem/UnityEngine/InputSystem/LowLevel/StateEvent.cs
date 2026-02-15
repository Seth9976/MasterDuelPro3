using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001C2 RID: 450
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 25)]
	public struct StateEvent : IInputEventTypeInfo
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000510BC File Offset: 0x0004F2BC
		public uint stateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 24U;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000510CC File Offset: 0x0004F2CC
		public unsafe void* state
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (StateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x000510FE File Offset: 0x0004F2FE
		public FourCC typeStatic
		{
			get
			{
				return 1398030676;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0005110C File Offset: 0x0004F30C
		public TState GetState<TState>() where TState : struct, IInputStateTypeInfo
		{
			TState result = default(TState);
			if (this.stateFormat != result.format)
			{
				throw new InvalidOperationException(string.Format("Expected state format '{0}' but got '{1}' instead", result.format, this.stateFormat));
			}
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TState>(ref result), this.state, Math.Min((long)((ulong)this.stateSizeInBytes), (long)UnsafeUtility.SizeOf<TState>()));
			return result;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0005118D File Offset: 0x0004F38D
		public unsafe static TState GetState<TState>(InputEventPtr ptr) where TState : struct, IInputStateTypeInfo
		{
			return StateEvent.From(ptr)->GetState<TState>();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0005119A File Offset: 0x0004F39A
		public static int GetEventSizeWithPayload<TState>() where TState : struct
		{
			return UnsafeUtility.SizeOf<TState>() + 20 + 4;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000511A8 File Offset: 0x0004F3A8
		public unsafe static StateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<StateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into StateEvent", ptr.type));
			}
			return StateEvent.FromUnchecked(ptr);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004E450 File Offset: 0x0004C650
		internal unsafe static StateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (StateEvent*)ptr.data;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, false);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000511FF File Offset: 0x0004F3FF
		public static NativeArray<byte> FromDefaultStateFor(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, true);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005120C File Offset: 0x0004F40C
		private unsafe static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator, bool useDefaultState)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device '{0}' has not been added to system", device), "device");
			}
			FourCC stateFormat = device.m_StateBlock.format;
			uint stateSize = device.m_StateBlock.alignedSizeInBytes;
			uint stateOffset = device.m_StateBlock.byteOffset;
			byte* statePtr = (byte*)((useDefaultState ? device.defaultStatePtr : device.currentStatePtr) + stateOffset);
			uint eventSize = 24U + stateSize;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)eventSize.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			StateEvent* stateEventPtr = (StateEvent*)nativeArray.GetUnsafePtr<byte>();
			stateEventPtr->baseEvent = new InputEvent(1398030676, (int)eventSize, device.deviceId, InputRuntime.s_Instance.currentTime);
			stateEventPtr->stateFormat = stateFormat;
			UnsafeUtility.MemCpy(stateEventPtr->state, (void*)statePtr, (long)((ulong)stateSize));
			eventPtr = stateEventPtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x04000A3B RID: 2619
		public const int Type = 1398030676;

		// Token: 0x04000A3C RID: 2620
		internal const int kStateDataSizeToSubtract = 1;

		// Token: 0x04000A3D RID: 2621
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x04000A3E RID: 2622
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x04000A3F RID: 2623
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(24)]
		internal StateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x020001C3 RID: 451
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x04000A40 RID: 2624
			public byte FixedElementField;
		}
	}
}
