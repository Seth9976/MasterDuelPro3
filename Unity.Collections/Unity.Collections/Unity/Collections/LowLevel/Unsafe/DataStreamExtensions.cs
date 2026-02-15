using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000100 RID: 256
	[GenerateTestsForBurstCompatibility]
	public static class DataStreamExtensions
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00021537 File Offset: 0x0001F737
		public unsafe static DataStreamWriter Create(byte* data, int length)
		{
			return new DataStreamWriter(NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, length, Allocator.None));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00021548 File Offset: 0x0001F748
		public unsafe static bool WriteBytesUnsafe(this DataStreamWriter writer, byte* data, int bytes)
		{
			NativeArray<byte> dataArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, bytes, Allocator.None);
			return writer.WriteBytes(dataArray);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00021568 File Offset: 0x0001F768
		public unsafe static void ReadBytesUnsafe(this DataStreamReader reader, byte* data, int length)
		{
			NativeArray<byte> dataArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, length, Allocator.None);
			reader.ReadBytes(dataArray);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00021588 File Offset: 0x0001F788
		public unsafe static ushort ReadFixedStringUnsafe(this DataStreamReader reader, byte* data, int maxLength)
		{
			NativeArray<byte> dataArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, maxLength, Allocator.Temp);
			return reader.ReadFixedString(dataArray);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public unsafe static ushort ReadPackedFixedStringDeltaUnsafe(this DataStreamReader reader, byte* data, int maxLength, byte* baseData, ushort baseLength, StreamCompressionModel model)
		{
			NativeArray<byte> current = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)data, maxLength, Allocator.Temp);
			NativeArray<byte> baseline = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)baseData, (int)baseLength, Allocator.Temp);
			return reader.ReadPackedFixedStringDelta(current, baseline, in model);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000215D2 File Offset: 0x0001F7D2
		public unsafe static void* GetUnsafeReadOnlyPtr(this DataStreamReader reader)
		{
			return (void*)reader.m_BufferPtr;
		}
	}
}
