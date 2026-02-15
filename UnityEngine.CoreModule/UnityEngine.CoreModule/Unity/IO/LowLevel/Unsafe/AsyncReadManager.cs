using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000041 RID: 65
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000034DC File Offset: 0x000016DC
		[FreeFunction("AsyncReadManagerManaged::GetFileInfo", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle GetFileInfoInternal(string filename, void* cmd)
		{
			ReadHandle readHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ReadHandle readHandle;
				AsyncReadManager.GetFileInfoInternal_Injected(ref managedSpanWrapper, cmd, out readHandle);
			}
			finally
			{
				char* ptr = null;
				ReadHandle readHandle;
				readHandle2 = readHandle;
			}
			return readHandle2;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003538 File Offset: 0x00001738
		public unsafe static ReadHandle GetFileInfo(string filename, FileInfoResult* result)
		{
			bool flag = result == null;
			if (flag)
			{
				throw new NullReferenceException("GetFileInfo must have a valid FileInfoResult to write into.");
			}
			return AsyncReadManager.GetFileInfoInternal(filename, (void*)result);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003568 File Offset: 0x00001768
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativeCopy", IsThreadSafe = true)]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativeCopy(in FileHandle fileHandle, void* readCmdArray)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadWithHandlesInternal_NativeCopy_Injected(in fileHandle, readCmdArray, out readHandle);
			return readHandle;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003580 File Offset: 0x00001780
		public static ReadHandle Read(in FileHandle fileHandle, ReadCommandArray readCmdArray)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativeCopy(in fileHandle, UnsafeUtility.AddressOf<ReadCommandArray>(ref readCmdArray));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000035B8 File Offset: 0x000017B8
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ScheduleOpenRequest", IsThreadSafe = true)]
		private unsafe static FileHandle OpenFileAsync_Internal(string fileName)
		{
			FileHandle fileHandle2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				FileHandle fileHandle;
				AsyncReadManager.OpenFileAsync_Internal_Injected(ref managedSpanWrapper, out fileHandle);
			}
			finally
			{
				char* ptr = null;
				FileHandle fileHandle;
				fileHandle2 = fileHandle;
			}
			return fileHandle2;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003614 File Offset: 0x00001814
		public static FileHandle OpenFileAsync(string fileName)
		{
			bool flag = fileName.Length == 0;
			if (flag)
			{
				throw new InvalidOperationException("FileName is empty");
			}
			return AsyncReadManager.OpenFileAsync_Internal(fileName);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003644 File Offset: 0x00001844
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		internal static JobHandle CloseFileAsync(in FileHandle fileHandle, JobHandle dependency)
		{
			JobHandle jobHandle;
			AsyncReadManager.CloseFileAsync_Injected(in fileHandle, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetFileInfoInternal_Injected(ref ManagedSpanWrapper filename, void* cmd, out ReadHandle ret);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadWithHandlesInternal_NativeCopy_Injected(in FileHandle fileHandle, void* readCmdArray, out ReadHandle ret);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OpenFileAsync_Internal_Injected(ref ManagedSpanWrapper fileName, out FileHandle ret);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CloseFileAsync_Injected(in FileHandle fileHandle, [In] ref JobHandle dependency, out JobHandle ret);
	}
}
