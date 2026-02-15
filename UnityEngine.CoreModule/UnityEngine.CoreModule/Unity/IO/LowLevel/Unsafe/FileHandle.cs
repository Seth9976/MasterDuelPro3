using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200003F RID: 63
	public readonly struct FileHandle
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000032C0 File Offset: 0x000014C0
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(in this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.JobHandle cannot be called on a closed FileHandle");
				}
				return FileHandle.GetJobHandle_Internal(in this);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000032F0 File Offset: 0x000014F0
		public bool IsValid()
		{
			return FileHandle.IsFileHandleValid(in this);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003308 File Offset: 0x00001508
		public JobHandle Close(JobHandle dependency = default(JobHandle))
		{
			bool flag = !FileHandle.IsFileHandleValid(in this);
			if (flag)
			{
				throw new InvalidOperationException("FileHandle.Close cannot be called twice on the same FileHandle");
			}
			return AsyncReadManager.CloseFileAsync(in this, dependency);
		}

		// Token: 0x060000B3 RID: 179
		[FreeFunction("AsyncReadManagerManaged::IsFileHandleValid")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFileHandleValid(in FileHandle handle);

		// Token: 0x060000B4 RID: 180 RVA: 0x0000333C File Offset: 0x0000153C
		[FreeFunction("AsyncReadManagerManaged::GetJobFenceFromManagedHandle")]
		private static JobHandle GetJobHandle_Internal(in FileHandle handle)
		{
			JobHandle jobHandle;
			FileHandle.GetJobHandle_Internal_Injected(in handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Internal_Injected(in FileHandle handle, out JobHandle ret);

		// Token: 0x040000BC RID: 188
		[NativeDisableUnsafePtrRestriction]
		internal readonly IntPtr fileCommandPtr;

		// Token: 0x040000BD RID: 189
		internal readonly int version;
	}
}
