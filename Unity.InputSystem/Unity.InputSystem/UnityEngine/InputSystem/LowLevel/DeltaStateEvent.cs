using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A9 RID: 425
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 29)]
	public struct DeltaStateEvent : IInputEventTypeInfo
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0004E3B1 File Offset: 0x0004C5B1
		public uint deltaStateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 28U;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0004E3C4 File Offset: 0x0004C5C4
		public unsafe void* deltaState
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0004E3DF File Offset: 0x0004C5DF
		public FourCC typeStatic
		{
			get
			{
				return 1145852993;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeltaStateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0004E404 File Offset: 0x0004C604
		public unsafe static DeltaStateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<DeltaStateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into DeltaStateEvent", ptr.type));
			}
			return DeltaStateEvent.FromUnchecked(ptr);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004E450 File Offset: 0x0004C650
		internal unsafe static DeltaStateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (DeltaStateEvent*)ptr.data;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004E45C File Offset: 0x0004C65C
		public unsafe static NativeArray<byte> From(InputControl control, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputDevice device = control.device;
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control), "control");
			}
			ref InputStateBlock deviceStateBlock = ref device.m_StateBlock;
			ref InputStateBlock controlStateBlock = ref control.m_StateBlock;
			FourCC stateFormat = deviceStateBlock.format;
			uint stateSize;
			if (controlStateBlock.bitOffset != 0U)
			{
				stateSize = (controlStateBlock.bitOffset + controlStateBlock.sizeInBits + 7U) / 8U;
			}
			else
			{
				stateSize = controlStateBlock.alignedSizeInBytes;
			}
			uint stateOffset = controlStateBlock.byteOffset;
			byte* statePtr = (byte*)control.currentStatePtr + stateOffset;
			uint eventSize = 28U + stateSize;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)eventSize.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			DeltaStateEvent* stateEventPtr = (DeltaStateEvent*)nativeArray.GetUnsafePtr<byte>();
			stateEventPtr->baseEvent = new InputEvent(1145852993, (int)eventSize, device.deviceId, InputRuntime.s_Instance.currentTime);
			stateEventPtr->stateFormat = stateFormat;
			stateEventPtr->stateOffset = controlStateBlock.byteOffset - deviceStateBlock.byteOffset;
			UnsafeUtility.MemCpy(stateEventPtr->deltaState, (void*)statePtr, (long)((ulong)stateSize));
			eventPtr = stateEventPtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x040009D7 RID: 2519
		public const int Type = 1145852993;

		// Token: 0x040009D8 RID: 2520
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040009D9 RID: 2521
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x040009DA RID: 2522
		[FieldOffset(24)]
		public uint stateOffset;

		// Token: 0x040009DB RID: 2523
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(28)]
		internal DeltaStateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x020001AA RID: 426
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x040009DC RID: 2524
			public byte FixedElementField;
		}
	}
}
