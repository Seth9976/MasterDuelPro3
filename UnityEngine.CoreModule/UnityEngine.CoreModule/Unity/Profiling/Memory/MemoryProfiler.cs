using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.Memory
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Runtime/Profiler/Runtime/MemorySnapshotManager.h")]
	public static class MemoryProfiler
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003014 File Offset: 0x00001214
		[RequiredByNativeCode]
		private unsafe static byte[] PrepareMetadata()
		{
			bool flag = MemoryProfiler.CreatingMetadata == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				MemorySnapshotMetadata data = new MemorySnapshotMetadata();
				data.Description = string.Empty;
				MemoryProfiler.CreatingMetadata(data);
				bool flag2 = data.Description == null;
				if (flag2)
				{
					data.Description = "";
				}
				int contentLength = 2 * data.Description.Length;
				int dataLength = ((data.Data == null) ? 0 : data.Data.Length);
				int metaDataSize = contentLength + dataLength + 12;
				byte[] metaDataBytes = new byte[metaDataSize];
				int offset = 0;
				offset = MemoryProfiler.WriteIntToByteArray(metaDataBytes, offset, data.Description.Length);
				offset = MemoryProfiler.WriteStringToByteArray(metaDataBytes, offset, data.Description);
				offset = MemoryProfiler.WriteIntToByteArray(metaDataBytes, offset, dataLength);
				byte[] array2;
				byte* src;
				if ((array2 = data.Data) == null || array2.Length == 0)
				{
					src = null;
				}
				else
				{
					src = &array2[0];
				}
				byte[] array3;
				byte* dst;
				if ((array3 = metaDataBytes) == null || array3.Length == 0)
				{
					dst = null;
				}
				else
				{
					dst = &array3[0];
				}
				byte* start = dst + offset;
				UnsafeUtility.MemCpy((void*)start, (void*)src, (long)dataLength);
				array2 = null;
				array3 = null;
				array = metaDataBytes;
			}
			return array;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003144 File Offset: 0x00001344
		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* pi = (byte*)(&value);
			array[offset++] = *pi;
			array[offset++] = pi[1];
			array[offset++] = pi[2];
			array[offset++] = pi[3];
			return offset;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000318C File Offset: 0x0000138C
		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			bool flag = value.Length != 0;
			if (flag)
			{
				fixed (string text = value)
				{
					char* p = text;
					if (p != null)
					{
						p += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* begin = p;
					char* end = p + value.Length;
					while (begin != end)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = *(byte*)(begin + i / 2);
						}
						begin++;
					}
				}
			}
			return offset;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003210 File Offset: 0x00001410
		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Action<string, bool> onSnapshotFinished = MemoryProfiler.m_SnapshotFinished;
				MemoryProfiler.m_SnapshotFinished = null;
				onSnapshotFinished(path, result);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003244 File Offset: 0x00001444
		[RequiredByNativeCode]
		private static void SaveScreenshotToDisk(string path, bool result, IntPtr pixelsPtr, int pixelsCount, TextureFormat format, int width, int height)
		{
			bool flag = MemoryProfiler.m_SaveScreenshotToDisk != null;
			if (flag)
			{
				Action<string, bool, DebugScreenCapture> saveScreenshotToDisk = MemoryProfiler.m_SaveScreenshotToDisk;
				MemoryProfiler.m_SaveScreenshotToDisk = null;
				DebugScreenCapture debugScreenCapture = default(DebugScreenCapture);
				if (result)
				{
					NativeArray<byte> nonOwningNativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(pixelsPtr.ToPointer(), pixelsCount, Allocator.Persistent);
					debugScreenCapture.RawImageDataReference = nonOwningNativeArray;
					debugScreenCapture.Height = height;
					debugScreenCapture.Width = width;
					debugScreenCapture.ImageFormat = format;
				}
				saveScreenshotToDisk(path, result, debugScreenCapture);
			}
		}

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, bool> m_SnapshotFinished;

		// Token: 0x0400009C RID: 156
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, bool, DebugScreenCapture> m_SaveScreenshotToDisk;

		// Token: 0x0400009D RID: 157
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<MemorySnapshotMetadata> CreatingMetadata;
	}
}
