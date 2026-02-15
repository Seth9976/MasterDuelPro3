using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.Collections
{
	// Token: 0x02000047 RID: 71
	[MovedFrom(true, "Unity.Networking.Transport", "Unity.Networking.Transport", null)]
	[GenerateTestsForBurstCompatibility]
	public struct DataStreamWriter
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005E54 File Offset: 0x00004054
		public unsafe static bool IsLittleEndian
		{
			get
			{
				uint test = 1U;
				byte* testPtr = (byte*)(&test);
				return *testPtr == 1;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005E6C File Offset: 0x0000406C
		public DataStreamWriter(int length, AllocatorManager.AllocatorHandle allocator)
		{
			DataStreamWriter.Initialize(out this, CollectionHelper.CreateNativeArray<byte>(length, allocator, NativeArrayOptions.ClearMemory));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005E7C File Offset: 0x0000407C
		public DataStreamWriter(NativeArray<byte> data)
		{
			DataStreamWriter.Initialize(out this, data);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005E88 File Offset: 0x00004088
		public unsafe DataStreamWriter(byte* data, int length)
		{
			NativeArray<byte> na = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, length, Allocator.Invalid);
			DataStreamWriter.Initialize(out this, na);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005EA5 File Offset: 0x000040A5
		public unsafe NativeArray<byte> AsNativeArray()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)this.m_Data.buffer, this.Length, Allocator.Invalid);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005EC0 File Offset: 0x000040C0
		private unsafe static void Initialize(out DataStreamWriter self, NativeArray<byte> data)
		{
			self.m_SendHandleData = IntPtr.Zero;
			self.m_Data.capacity = data.Length;
			self.m_Data.length = 0;
			self.m_Data.buffer = (byte*)data.GetUnsafePtr<byte>();
			self.m_Data.bitBuffer = 0UL;
			self.m_Data.bitIndex = 0;
			self.m_Data.failedWrites = 0;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005541 File Offset: 0x00003741
		private static short ByteSwap(short val)
		{
			return (short)(((int)(val & 255) << 8) | ((val >> 8) & 255));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005557 File Offset: 0x00003757
		private static int ByteSwap(int val)
		{
			return ((val & 255) << 24) | ((val & 65280) << 8) | ((val >> 8) & 65280) | ((val >> 24) & 255);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005F2C File Offset: 0x0000412C
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.buffer != null;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005F40 File Offset: 0x00004140
		public readonly bool HasFailedWrites
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.failedWrites > 0;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005F50 File Offset: 0x00004150
		public readonly int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.capacity;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005F5D File Offset: 0x0000415D
		public int Length
		{
			get
			{
				this.SyncBitData();
				return this.m_Data.length + (this.m_Data.bitIndex + 7 >> 3);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005F80 File Offset: 0x00004180
		public int LengthInBits
		{
			get
			{
				this.SyncBitData();
				return this.m_Data.length * 8 + this.m_Data.bitIndex;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005FA4 File Offset: 0x000041A4
		private unsafe void SyncBitData()
		{
			int bitIndex = this.m_Data.bitIndex;
			if (bitIndex <= 0)
			{
				return;
			}
			ulong bitBuffer = this.m_Data.bitBuffer;
			int offset = 0;
			while (bitIndex > 0)
			{
				this.m_Data.buffer[this.m_Data.length + offset] = (byte)bitBuffer;
				bitIndex -= 8;
				bitBuffer >>= 8;
				offset++;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006000 File Offset: 0x00004200
		public unsafe void Flush()
		{
			while (this.m_Data.bitIndex > 0)
			{
				ref byte buffer = ref *this.m_Data.buffer;
				int length = this.m_Data.length;
				this.m_Data.length = length + 1;
				*((ref buffer) + length) = (byte)this.m_Data.bitBuffer;
				this.m_Data.bitIndex = this.m_Data.bitIndex - 8;
				this.m_Data.bitBuffer = this.m_Data.bitBuffer >> 8;
			}
			this.m_Data.bitIndex = 0;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006078 File Offset: 0x00004278
		private unsafe bool WriteBytesInternal(byte* data, int bytes)
		{
			if (this.m_Data.length + (this.m_Data.bitIndex + 7 >> 3) + bytes > this.m_Data.capacity)
			{
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
				return false;
			}
			this.Flush();
			UnsafeUtility.MemCpy((void*)(this.m_Data.buffer + this.m_Data.length), (void*)data, (long)bytes);
			this.m_Data.length = this.m_Data.length + bytes;
			return true;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000060F7 File Offset: 0x000042F7
		public unsafe bool WriteByte(byte value)
		{
			return this.WriteBytesInternal(&value, 1);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006103 File Offset: 0x00004303
		public unsafe bool WriteBytes(NativeArray<byte> value)
		{
			return this.WriteBytesInternal((byte*)value.GetUnsafeReadOnlyPtr<byte>(), value.Length);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006118 File Offset: 0x00004318
		public unsafe bool WriteBytes(Span<byte> value)
		{
			fixed (byte* pinnableReference = value.GetPinnableReference())
			{
				byte* data = pinnableReference;
				return this.WriteBytesInternal(data, value.Length);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000613E File Offset: 0x0000433E
		public unsafe bool WriteShort(short value)
		{
			return this.WriteBytesInternal((byte*)(&value), 2);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000613E File Offset: 0x0000433E
		public unsafe bool WriteUShort(ushort value)
		{
			return this.WriteBytesInternal((byte*)(&value), 2);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000614A File Offset: 0x0000434A
		public unsafe bool WriteInt(int value)
		{
			return this.WriteBytesInternal((byte*)(&value), 4);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000614A File Offset: 0x0000434A
		public unsafe bool WriteUInt(uint value)
		{
			return this.WriteBytesInternal((byte*)(&value), 4);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006156 File Offset: 0x00004356
		public unsafe bool WriteLong(long value)
		{
			return this.WriteBytesInternal((byte*)(&value), 8);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006156 File Offset: 0x00004356
		public unsafe bool WriteULong(ulong value)
		{
			return this.WriteBytesInternal((byte*)(&value), 8);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006164 File Offset: 0x00004364
		public unsafe bool WriteShortNetworkByteOrder(short value)
		{
			short netValue = (DataStreamWriter.IsLittleEndian ? DataStreamWriter.ByteSwap(value) : value);
			return this.WriteBytesInternal((byte*)(&netValue), 2);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000618C File Offset: 0x0000438C
		public bool WriteUShortNetworkByteOrder(ushort value)
		{
			return this.WriteShortNetworkByteOrder((short)value);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006198 File Offset: 0x00004398
		public unsafe bool WriteIntNetworkByteOrder(int value)
		{
			int netValue = (DataStreamWriter.IsLittleEndian ? DataStreamWriter.ByteSwap(value) : value);
			return this.WriteBytesInternal((byte*)(&netValue), 4);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000061C0 File Offset: 0x000043C0
		public bool WriteUIntNetworkByteOrder(uint value)
		{
			return this.WriteIntNetworkByteOrder((int)value);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000061CC File Offset: 0x000043CC
		public bool WriteFloat(float value)
		{
			return this.WriteInt((int)new UIntFloat
			{
				floatValue = value
			}.intValue);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000061F8 File Offset: 0x000043F8
		public bool WriteDouble(double value)
		{
			return this.WriteLong((long)new UIntFloat
			{
				doubleValue = value
			}.longValue);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006224 File Offset: 0x00004424
		private unsafe void FlushBits()
		{
			while (this.m_Data.bitIndex >= 8)
			{
				ref byte buffer = ref *this.m_Data.buffer;
				int length = this.m_Data.length;
				this.m_Data.length = length + 1;
				*((ref buffer) + length) = (byte)this.m_Data.bitBuffer;
				this.m_Data.bitIndex = this.m_Data.bitIndex - 8;
				this.m_Data.bitBuffer = this.m_Data.bitBuffer >> 8;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000628D File Offset: 0x0000448D
		private void WriteRawBitsInternal(uint value, int numbits)
		{
			this.m_Data.bitBuffer = this.m_Data.bitBuffer | ((ulong)value << this.m_Data.bitIndex);
			this.m_Data.bitIndex = this.m_Data.bitIndex + numbits;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000062C0 File Offset: 0x000044C0
		public bool WriteRawBits(uint value, int numbits)
		{
			if (this.m_Data.length + (this.m_Data.bitIndex + numbits + 7 >> 3) > this.m_Data.capacity)
			{
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
				return false;
			}
			this.WriteRawBitsInternal(value, numbits);
			this.FlushBits();
			return true;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006318 File Offset: 0x00004518
		public unsafe bool WritePackedUInt(uint value, in StreamCompressionModel model)
		{
			int bucket = model.CalculateBucket(value);
			uint offset = *((ref model.bucketOffsets.FixedElementField) + (IntPtr)bucket * 4);
			int bits = (int)(*((ref model.bucketSizes.FixedElementField) + bucket));
			ushort encodeEntry = *((ref model.encodeTable.FixedElementField) + (IntPtr)bucket * 2);
			if (this.m_Data.length + (this.m_Data.bitIndex + (int)(encodeEntry & 255) + bits + 7 >> 3) > this.m_Data.capacity)
			{
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
				return false;
			}
			this.WriteRawBitsInternal((uint)(encodeEntry >> 8), (int)(encodeEntry & 255));
			this.WriteRawBitsInternal(value - offset, bits);
			this.FlushBits();
			return true;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000063C8 File Offset: 0x000045C8
		public unsafe bool WritePackedULong(ulong value, in StreamCompressionModel model)
		{
			uint* data = (uint*)(&value);
			return this.WritePackedUInt(*data, in model) & this.WritePackedUInt(data[1], in model);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000063F0 File Offset: 0x000045F0
		public bool WritePackedInt(int value, in StreamCompressionModel model)
		{
			uint interleaved = (uint)((value >> 31) ^ (value << 1));
			return this.WritePackedUInt(interleaved, in model);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006410 File Offset: 0x00004610
		public bool WritePackedLong(long value, in StreamCompressionModel model)
		{
			ulong interleaved = (ulong)((value >> 63) ^ (value << 1));
			return this.WritePackedULong(interleaved, in model);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000642E File Offset: 0x0000462E
		public bool WritePackedFloat(float value, in StreamCompressionModel model)
		{
			return this.WritePackedFloatDelta(value, 0f, in model);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000643D File Offset: 0x0000463D
		public bool WritePackedDouble(double value, in StreamCompressionModel model)
		{
			return this.WritePackedDoubleDelta(value, 0.0, in model);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006450 File Offset: 0x00004650
		public bool WritePackedUIntDelta(uint value, uint baseline, in StreamCompressionModel model)
		{
			int diff = (int)(baseline - value);
			return this.WritePackedInt(diff, in model);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000646C File Offset: 0x0000466C
		public bool WritePackedIntDelta(int value, int baseline, in StreamCompressionModel model)
		{
			int diff = baseline - value;
			return this.WritePackedInt(diff, in model);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006488 File Offset: 0x00004688
		public bool WritePackedLongDelta(long value, long baseline, in StreamCompressionModel model)
		{
			long diff = baseline - value;
			return this.WritePackedLong(diff, in model);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000064A4 File Offset: 0x000046A4
		public bool WritePackedULongDelta(ulong value, ulong baseline, in StreamCompressionModel model)
		{
			long diff = (long)(baseline - value);
			return this.WritePackedLong(diff, in model);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000064C0 File Offset: 0x000046C0
		public bool WritePackedFloatDelta(float value, float baseline, in StreamCompressionModel model)
		{
			int bits = 0;
			if (value != baseline)
			{
				bits = 32;
			}
			if (this.m_Data.length + (this.m_Data.bitIndex + 1 + bits + 7 >> 3) > this.m_Data.capacity)
			{
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
				return false;
			}
			if (bits == 0)
			{
				this.WriteRawBitsInternal(0U, 1);
			}
			else
			{
				this.WriteRawBitsInternal(1U, 1);
				this.WriteRawBitsInternal(new UIntFloat
				{
					floatValue = value
				}.intValue, bits);
			}
			this.FlushBits();
			return true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006550 File Offset: 0x00004750
		public unsafe bool WritePackedDoubleDelta(double value, double baseline, in StreamCompressionModel model)
		{
			int bits = 0;
			if (value != baseline)
			{
				bits = 64;
			}
			if (this.m_Data.length + (this.m_Data.bitIndex + 1 + bits + 7 >> 3) > this.m_Data.capacity)
			{
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
				return false;
			}
			if (bits == 0)
			{
				this.WriteRawBitsInternal(0U, 1);
			}
			else
			{
				this.WriteRawBitsInternal(1U, 1);
				UIntFloat uf = default(UIntFloat);
				uf.doubleValue = value;
				uint* data = (uint*)(&uf.longValue);
				this.WriteRawBitsInternal(*data, 32);
				this.FlushBits();
				this.WriteRawBitsInternal(data[1], 32);
			}
			this.FlushBits();
			return true;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000065F8 File Offset: 0x000047F8
		public unsafe bool WriteFixedString32(FixedString32Bytes str)
		{
			int length = (int)(*(ushort*)(&str) + 2);
			byte* data = (byte*)(&str);
			return this.WriteBytesInternal(data, length);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006618 File Offset: 0x00004818
		public unsafe bool WriteFixedString64(FixedString64Bytes str)
		{
			int length = (int)(*(ushort*)(&str) + 2);
			byte* data = (byte*)(&str);
			return this.WriteBytesInternal(data, length);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006638 File Offset: 0x00004838
		public unsafe bool WriteFixedString128(FixedString128Bytes str)
		{
			int length = (int)(*(ushort*)(&str) + 2);
			byte* data = (byte*)(&str);
			return this.WriteBytesInternal(data, length);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006658 File Offset: 0x00004858
		public unsafe bool WriteFixedString512(FixedString512Bytes str)
		{
			int length = (int)(*(ushort*)(&str) + 2);
			byte* data = (byte*)(&str);
			return this.WriteBytesInternal(data, length);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006678 File Offset: 0x00004878
		public unsafe bool WriteFixedString4096(FixedString4096Bytes str)
		{
			int length = (int)(*(ushort*)(&str) + 2);
			byte* data = (byte*)(&str);
			return this.WriteBytesInternal(data, length);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006698 File Offset: 0x00004898
		public unsafe bool WritePackedFixedString32Delta(FixedString32Bytes str, FixedString32Bytes baseline, in StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return this.WritePackedFixedStringDelta(data, (uint)length, (byte*)(&baseline) + 2, (uint)(*(ushort*)(&baseline)), in model);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000066C4 File Offset: 0x000048C4
		public unsafe bool WritePackedFixedString64Delta(FixedString64Bytes str, FixedString64Bytes baseline, in StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return this.WritePackedFixedStringDelta(data, (uint)length, (byte*)(&baseline) + 2, (uint)(*(ushort*)(&baseline)), in model);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000066F0 File Offset: 0x000048F0
		public unsafe bool WritePackedFixedString128Delta(FixedString128Bytes str, FixedString128Bytes baseline, in StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return this.WritePackedFixedStringDelta(data, (uint)length, (byte*)(&baseline) + 2, (uint)(*(ushort*)(&baseline)), in model);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000671C File Offset: 0x0000491C
		public unsafe bool WritePackedFixedString512Delta(FixedString512Bytes str, FixedString512Bytes baseline, in StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return this.WritePackedFixedStringDelta(data, (uint)length, (byte*)(&baseline) + 2, (uint)(*(ushort*)(&baseline)), in model);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006748 File Offset: 0x00004948
		public unsafe bool WritePackedFixedString4096Delta(FixedString4096Bytes str, FixedString4096Bytes baseline, in StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return this.WritePackedFixedStringDelta(data, (uint)length, (byte*)(&baseline) + 2, (uint)(*(ushort*)(&baseline)), in model);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006774 File Offset: 0x00004974
		private unsafe bool WritePackedFixedStringDelta(byte* data, uint length, byte* baseData, uint baseLength, in StreamCompressionModel model)
		{
			DataStreamWriter.StreamData oldData = this.m_Data;
			if (!this.WritePackedUIntDelta(length, baseLength, in model))
			{
				return false;
			}
			bool didFailWrite = false;
			if (length <= baseLength)
			{
				for (uint i = 0U; i < length; i += 1U)
				{
					didFailWrite |= !this.WritePackedUIntDelta((uint)data[i], (uint)baseData[i], in model);
				}
			}
			else
			{
				for (uint j = 0U; j < baseLength; j += 1U)
				{
					didFailWrite |= !this.WritePackedUIntDelta((uint)data[j], (uint)baseData[j], in model);
				}
				for (uint k = baseLength; k < length; k += 1U)
				{
					didFailWrite |= !this.WritePackedUInt((uint)data[k], in model);
				}
			}
			if (didFailWrite)
			{
				this.m_Data = oldData;
				this.m_Data.failedWrites = this.m_Data.failedWrites + 1;
			}
			return !didFailWrite;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000682C File Offset: 0x00004A2C
		public void Clear()
		{
			this.m_Data.length = 0;
			this.m_Data.bitIndex = 0;
			this.m_Data.bitBuffer = 0UL;
			this.m_Data.failedWrites = 0;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000685F File Offset: 0x00004A5F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAllocator(AllocatorManager.AllocatorHandle allocator)
		{
			if (allocator.ToAllocator != Allocator.Temp)
			{
				throw new InvalidOperationException("DataStreamWriters can only be created with temp memory");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006878 File Offset: 0x00004A78
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckBits(uint value, int numBits)
		{
			if (numBits < 0 || numBits > 32)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid number of bits specified: {0}! Valid range is (0, 32) inclusive.", numBits));
			}
			ulong errValue = 1UL << numBits;
			if ((ulong)value >= errValue)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} does not fit in the specified number of bits: {1}! Range (inclusive) is (0, {2})!", value, numBits, errValue - 1UL));
			}
		}

		// Token: 0x040000AF RID: 175
		[NativeDisableUnsafePtrRestriction]
		private DataStreamWriter.StreamData m_Data;

		// Token: 0x040000B0 RID: 176
		public IntPtr m_SendHandleData;

		// Token: 0x02000048 RID: 72
		private struct StreamData
		{
			// Token: 0x040000B1 RID: 177
			public unsafe byte* buffer;

			// Token: 0x040000B2 RID: 178
			public int length;

			// Token: 0x040000B3 RID: 179
			public int capacity;

			// Token: 0x040000B4 RID: 180
			public ulong bitBuffer;

			// Token: 0x040000B5 RID: 181
			public int bitIndex;

			// Token: 0x040000B6 RID: 182
			public int failedWrites;
		}
	}
}
