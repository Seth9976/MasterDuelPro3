using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.Collections
{
	// Token: 0x02000045 RID: 69
	[MovedFrom(true, "Unity.Networking.Transport", null, null)]
	[GenerateTestsForBurstCompatibility]
	public struct DataStreamReader
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000550A File Offset: 0x0000370A
		public DataStreamReader(NativeArray<byte> array)
		{
			DataStreamReader.Initialize(out this, array);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005513 File Offset: 0x00003713
		private unsafe static void Initialize(out DataStreamReader self, NativeArray<byte> array)
		{
			self.m_BufferPtr = (byte*)array.GetUnsafeReadOnlyPtr<byte>();
			self.m_Length = array.Length;
			self.m_Context = default(DataStreamReader.Context);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000553A File Offset: 0x0000373A
		public static bool IsLittleEndian
		{
			get
			{
				return DataStreamWriter.IsLittleEndian;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005541 File Offset: 0x00003741
		private static short ByteSwap(short val)
		{
			return (short)(((int)(val & 255) << 8) | ((val >> 8) & 255));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005557 File Offset: 0x00003757
		private static int ByteSwap(int val)
		{
			return ((val & 255) << 24) | ((val & 65280) << 8) | ((val >> 8) & 65280) | ((val >> 24) & 255);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005582 File Offset: 0x00003782
		public readonly bool HasFailedReads
		{
			get
			{
				return this.m_Context.m_FailedReads > 0;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005592 File Offset: 0x00003792
		public readonly int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000559A File Offset: 0x0000379A
		public readonly bool IsCreated
		{
			get
			{
				return this.m_BufferPtr != null;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000055AC File Offset: 0x000037AC
		private unsafe void ReadBytesInternal(byte* data, int length)
		{
			if (this.GetBytesRead() + length > this.m_Length)
			{
				this.m_Context.m_FailedReads = this.m_Context.m_FailedReads + 1;
				UnsafeUtility.MemClear((void*)data, (long)length);
				return;
			}
			this.Flush();
			UnsafeUtility.MemCpy((void*)data, (void*)(this.m_BufferPtr + this.m_Context.m_ReadByteIndex), (long)length);
			this.m_Context.m_ReadByteIndex = this.m_Context.m_ReadByteIndex + length;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005612 File Offset: 0x00003812
		public unsafe void ReadBytes(NativeArray<byte> array)
		{
			this.ReadBytesInternal((byte*)array.GetUnsafePtr<byte>(), array.Length);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005628 File Offset: 0x00003828
		public unsafe void ReadBytes(Span<byte> span)
		{
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				byte* ptr = pinnableReference;
				this.ReadBytesInternal(ptr, span.Length);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005651 File Offset: 0x00003851
		public int GetBytesRead()
		{
			return this.m_Context.m_ReadByteIndex - (this.m_Context.m_BitIndex >> 3);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000566C File Offset: 0x0000386C
		public int GetBitsRead()
		{
			return (this.m_Context.m_ReadByteIndex << 3) - this.m_Context.m_BitIndex;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005688 File Offset: 0x00003888
		public void SeekSet(int pos)
		{
			if (pos > this.m_Length)
			{
				this.m_Context.m_FailedReads = this.m_Context.m_FailedReads + 1;
				return;
			}
			this.m_Context.m_ReadByteIndex = pos;
			this.m_Context.m_BitIndex = 0;
			this.m_Context.m_BitBuffer = 0UL;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000056D4 File Offset: 0x000038D4
		public unsafe byte ReadByte()
		{
			byte data;
			this.ReadBytesInternal(&data, 1);
			return data;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000056EC File Offset: 0x000038EC
		public unsafe short ReadShort()
		{
			short data;
			this.ReadBytesInternal((byte*)(&data), 2);
			return data;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005704 File Offset: 0x00003904
		public unsafe ushort ReadUShort()
		{
			ushort data;
			this.ReadBytesInternal((byte*)(&data), 2);
			return data;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000571C File Offset: 0x0000391C
		public unsafe int ReadInt()
		{
			int data;
			this.ReadBytesInternal((byte*)(&data), 4);
			return data;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005734 File Offset: 0x00003934
		public unsafe uint ReadUInt()
		{
			uint data;
			this.ReadBytesInternal((byte*)(&data), 4);
			return data;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000574C File Offset: 0x0000394C
		public unsafe long ReadLong()
		{
			long data;
			this.ReadBytesInternal((byte*)(&data), 8);
			return data;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005764 File Offset: 0x00003964
		public unsafe ulong ReadULong()
		{
			ulong data;
			this.ReadBytesInternal((byte*)(&data), 8);
			return data;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000577C File Offset: 0x0000397C
		public void Flush()
		{
			this.m_Context.m_ReadByteIndex = this.m_Context.m_ReadByteIndex - (this.m_Context.m_BitIndex >> 3);
			this.m_Context.m_BitIndex = 0;
			this.m_Context.m_BitBuffer = 0UL;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000057B4 File Offset: 0x000039B4
		public unsafe short ReadShortNetworkByteOrder()
		{
			short data;
			this.ReadBytesInternal((byte*)(&data), 2);
			if (!DataStreamReader.IsLittleEndian)
			{
				return data;
			}
			return DataStreamReader.ByteSwap(data);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000057DA File Offset: 0x000039DA
		public ushort ReadUShortNetworkByteOrder()
		{
			return (ushort)this.ReadShortNetworkByteOrder();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000057E4 File Offset: 0x000039E4
		public unsafe int ReadIntNetworkByteOrder()
		{
			int data;
			this.ReadBytesInternal((byte*)(&data), 4);
			if (!DataStreamReader.IsLittleEndian)
			{
				return data;
			}
			return DataStreamReader.ByteSwap(data);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000580A File Offset: 0x00003A0A
		public uint ReadUIntNetworkByteOrder()
		{
			return (uint)this.ReadIntNetworkByteOrder();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005814 File Offset: 0x00003A14
		public float ReadFloat()
		{
			return new UIntFloat
			{
				intValue = (uint)this.ReadInt()
			}.floatValue;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000583C File Offset: 0x00003A3C
		public double ReadDouble()
		{
			return new UIntFloat
			{
				longValue = (ulong)this.ReadLong()
			}.doubleValue;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005864 File Offset: 0x00003A64
		public uint ReadPackedUInt(in StreamCompressionModel model)
		{
			return this.ReadPackedUIntInternal(6, in model);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005870 File Offset: 0x00003A70
		private unsafe uint ReadPackedUIntInternal(int maxSymbolLength, in StreamCompressionModel model)
		{
			this.FillBitBuffer();
			uint peekMask = (1U << maxSymbolLength) - 1U;
			uint peekBits = (uint)this.m_Context.m_BitBuffer & peekMask;
			ushort num = *((ref model.decodeTable.FixedElementField) + (IntPtr)peekBits * 2);
			int symbol = num >> 8;
			int length = (int)(num & 255);
			if (this.m_Context.m_BitIndex < length)
			{
				this.m_Context.m_FailedReads = this.m_Context.m_FailedReads + 1;
				return 0U;
			}
			this.m_Context.m_BitBuffer = this.m_Context.m_BitBuffer >> length;
			this.m_Context.m_BitIndex = this.m_Context.m_BitIndex - length;
			uint offset = *((ref model.bucketOffsets.FixedElementField) + (IntPtr)symbol * 4);
			byte bits = *((ref model.bucketSizes.FixedElementField) + symbol);
			return this.ReadRawBitsInternal((int)bits) + offset;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005928 File Offset: 0x00003B28
		private unsafe void FillBitBuffer()
		{
			while (this.m_Context.m_BitIndex <= 56 && this.m_Context.m_ReadByteIndex < this.m_Length)
			{
				ulong bitBuffer = this.m_Context.m_BitBuffer;
				int bufferPtr = this.m_BufferPtr;
				int readByteIndex = this.m_Context.m_ReadByteIndex;
				this.m_Context.m_ReadByteIndex = readByteIndex + 1;
				this.m_Context.m_BitBuffer = bitBuffer | ((ulong)(*(bufferPtr + readByteIndex)) << this.m_Context.m_BitIndex);
				this.m_Context.m_BitIndex = this.m_Context.m_BitIndex + 8;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000059A4 File Offset: 0x00003BA4
		private uint ReadRawBitsInternal(int numbits)
		{
			if (this.m_Context.m_BitIndex < numbits)
			{
				this.m_Context.m_FailedReads = this.m_Context.m_FailedReads + 1;
				return 0U;
			}
			uint num = (uint)(this.m_Context.m_BitBuffer & ((1UL << numbits) - 1UL));
			this.m_Context.m_BitBuffer = this.m_Context.m_BitBuffer >> numbits;
			this.m_Context.m_BitIndex = this.m_Context.m_BitIndex - numbits;
			return num;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005A0B File Offset: 0x00003C0B
		public uint ReadRawBits(int numbits)
		{
			this.FillBitBuffer();
			return this.ReadRawBitsInternal(numbits);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005A1C File Offset: 0x00003C1C
		public unsafe ulong ReadPackedULong(in StreamCompressionModel model)
		{
			ulong value;
			*(int*)(&value) = (int)this.ReadPackedUInt(in model);
			((int*)(&value))[1] = (int)this.ReadPackedUInt(in model);
			return value;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005A44 File Offset: 0x00003C44
		public int ReadPackedInt(in StreamCompressionModel model)
		{
			uint folded = this.ReadPackedUInt(in model);
			return (int)((folded >> 1) ^ -(int)(folded & 1U));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005A64 File Offset: 0x00003C64
		public long ReadPackedLong(in StreamCompressionModel model)
		{
			ulong folded = this.ReadPackedULong(in model);
			return (long)((folded >> 1) ^ -(long)(folded & 1UL));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005A82 File Offset: 0x00003C82
		public float ReadPackedFloat(in StreamCompressionModel model)
		{
			return this.ReadPackedFloatDelta(0f, in model);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005A90 File Offset: 0x00003C90
		public double ReadPackedDouble(in StreamCompressionModel model)
		{
			return this.ReadPackedDoubleDelta(0.0, in model);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public int ReadPackedIntDelta(int baseline, in StreamCompressionModel model)
		{
			int delta = this.ReadPackedInt(in model);
			return baseline - delta;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005ABC File Offset: 0x00003CBC
		public uint ReadPackedUIntDelta(uint baseline, in StreamCompressionModel model)
		{
			uint delta = (uint)this.ReadPackedInt(in model);
			return baseline - delta;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public long ReadPackedLongDelta(long baseline, in StreamCompressionModel model)
		{
			long delta = this.ReadPackedLong(in model);
			return baseline - delta;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005AEC File Offset: 0x00003CEC
		public ulong ReadPackedULongDelta(ulong baseline, in StreamCompressionModel model)
		{
			ulong delta = (ulong)this.ReadPackedLong(in model);
			return baseline - delta;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005B04 File Offset: 0x00003D04
		public float ReadPackedFloatDelta(float baseline, in StreamCompressionModel model)
		{
			this.FillBitBuffer();
			if (this.ReadRawBitsInternal(1) == 0U)
			{
				return baseline;
			}
			int bits = 32;
			return new UIntFloat
			{
				intValue = this.ReadRawBitsInternal(bits)
			}.floatValue;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005B44 File Offset: 0x00003D44
		public unsafe double ReadPackedDoubleDelta(double baseline, in StreamCompressionModel model)
		{
			this.FillBitBuffer();
			if (this.ReadRawBitsInternal(1) == 0U)
			{
				return baseline;
			}
			int bits = 32;
			UIntFloat uf = default(UIntFloat);
			uint* data = (uint*)(&uf.longValue);
			*data = this.ReadRawBitsInternal(bits);
			this.FillBitBuffer();
			data[1] |= this.ReadRawBitsInternal(bits);
			return uf.doubleValue;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005B9C File Offset: 0x00003D9C
		public unsafe FixedString32Bytes ReadFixedString32()
		{
			FixedString32Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadFixedStringInternal(data, str.Capacity);
			return str;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public unsafe FixedString64Bytes ReadFixedString64()
		{
			FixedString64Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadFixedStringInternal(data, str.Capacity);
			return str;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005BEC File Offset: 0x00003DEC
		public unsafe FixedString128Bytes ReadFixedString128()
		{
			FixedString128Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadFixedStringInternal(data, str.Capacity);
			return str;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005C14 File Offset: 0x00003E14
		public unsafe FixedString512Bytes ReadFixedString512()
		{
			FixedString512Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadFixedStringInternal(data, str.Capacity);
			return str;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005C3C File Offset: 0x00003E3C
		public unsafe FixedString4096Bytes ReadFixedString4096()
		{
			FixedString4096Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadFixedStringInternal(data, str.Capacity);
			return str;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005C62 File Offset: 0x00003E62
		public unsafe ushort ReadFixedString(NativeArray<byte> array)
		{
			return this.ReadFixedStringInternal((byte*)array.GetUnsafePtr<byte>(), array.Length);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005C78 File Offset: 0x00003E78
		private unsafe ushort ReadFixedStringInternal(byte* data, int maxLength)
		{
			ushort length = this.ReadUShort();
			if ((int)length > maxLength)
			{
				return 0;
			}
			this.ReadBytesInternal(data, (int)length);
			return length;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005C9C File Offset: 0x00003E9C
		public unsafe FixedString32Bytes ReadPackedFixedString32Delta(FixedString32Bytes baseline, in StreamCompressionModel model)
		{
			FixedString32Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadPackedFixedStringDeltaInternal(data, str.Capacity, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
			return str;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005CCC File Offset: 0x00003ECC
		public unsafe FixedString64Bytes ReadPackedFixedString64Delta(FixedString64Bytes baseline, in StreamCompressionModel model)
		{
			FixedString64Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadPackedFixedStringDeltaInternal(data, str.Capacity, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
			return str;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005CFC File Offset: 0x00003EFC
		public unsafe FixedString128Bytes ReadPackedFixedString128Delta(FixedString128Bytes baseline, in StreamCompressionModel model)
		{
			FixedString128Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadPackedFixedStringDeltaInternal(data, str.Capacity, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
			return str;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005D2C File Offset: 0x00003F2C
		public unsafe FixedString512Bytes ReadPackedFixedString512Delta(FixedString512Bytes baseline, in StreamCompressionModel model)
		{
			FixedString512Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadPackedFixedStringDeltaInternal(data, str.Capacity, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
			return str;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005D5C File Offset: 0x00003F5C
		public unsafe FixedString4096Bytes ReadPackedFixedString4096Delta(FixedString4096Bytes baseline, in StreamCompressionModel model)
		{
			FixedString4096Bytes str;
			byte* data = (byte*)(&str) + 2;
			*(short*)(&str) = (short)this.ReadPackedFixedStringDeltaInternal(data, str.Capacity, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
			return str;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005D8C File Offset: 0x00003F8C
		public unsafe ushort ReadPackedFixedStringDelta(NativeArray<byte> data, NativeArray<byte> baseData, in StreamCompressionModel model)
		{
			return this.ReadPackedFixedStringDeltaInternal((byte*)data.GetUnsafePtr<byte>(), data.Length, (byte*)baseData.GetUnsafePtr<byte>(), (ushort)baseData.Length, in model);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005DB0 File Offset: 0x00003FB0
		private unsafe ushort ReadPackedFixedStringDeltaInternal(byte* data, int maxLength, byte* baseData, ushort baseLength, in StreamCompressionModel model)
		{
			uint length = this.ReadPackedUIntDelta((uint)baseLength, in model);
			if (length > (uint)maxLength)
			{
				return 0;
			}
			if (length <= (uint)baseLength)
			{
				int i = 0;
				while ((long)i < (long)((ulong)length))
				{
					data[i] = (byte)this.ReadPackedUIntDelta((uint)baseData[i], in model);
					i++;
				}
			}
			else
			{
				for (int j = 0; j < (int)baseLength; j++)
				{
					data[j] = (byte)this.ReadPackedUIntDelta((uint)baseData[j], in model);
				}
				int k = (int)baseLength;
				while ((long)k < (long)((ulong)length))
				{
					data[k] = (byte)this.ReadPackedUInt(in model);
					k++;
				}
			}
			return (ushort)length;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal readonly void CheckRead()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005E30 File Offset: 0x00004030
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckBits(int numBits)
		{
			if (numBits < 0 || numBits > 32)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid number of bits specified: {0}! Valid range is (0, 32) inclusive.", numBits));
			}
		}

		// Token: 0x040000A8 RID: 168
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_BufferPtr;

		// Token: 0x040000A9 RID: 169
		private DataStreamReader.Context m_Context;

		// Token: 0x040000AA RID: 170
		private int m_Length;

		// Token: 0x02000046 RID: 70
		private struct Context
		{
			// Token: 0x040000AB RID: 171
			public int m_ReadByteIndex;

			// Token: 0x040000AC RID: 172
			public int m_BitIndex;

			// Token: 0x040000AD RID: 173
			public ulong m_BitBuffer;

			// Token: 0x040000AE RID: 174
			public int m_FailedReads;
		}
	}
}
