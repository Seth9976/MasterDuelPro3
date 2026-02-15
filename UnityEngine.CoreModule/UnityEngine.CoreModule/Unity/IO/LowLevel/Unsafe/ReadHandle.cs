using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000040 RID: 64
	public struct ReadHandle : IDisposable
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003354 File Offset: 0x00001554
		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003374 File Offset: 0x00001574
		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000033C4 File Offset: 0x000015C4
		public void Cancel()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Cancel cannot be called on a disposed ReadHandle");
			}
			ReadHandle.CancelInternal(this);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000033FC File Offset: 0x000015FC
		[FreeFunction("AsyncReadManagerManaged::CancelReadRequest")]
		private static void CancelInternal(ReadHandle handle)
		{
			ReadHandle.CancelInternal_Injected(ref handle);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003410 File Offset: 0x00001610
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000344C File Offset: 0x0000164C
		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003488 File Offset: 0x00001688
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000349C File Offset: 0x0000169C
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000034B0 File Offset: 0x000016B0
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000034C4 File Offset: 0x000016C4
		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle jobHandle;
			ReadHandle.GetJobHandle_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInternal_Injected([In] ref ReadHandle handle);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReadStatus GetReadStatus_Injected([In] ref ReadHandle handle);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseReadHandle_Injected([In] ref ReadHandle handle);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReadHandleValid_Injected([In] ref ReadHandle handle);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Injected([In] ref ReadHandle handle, out JobHandle ret);

		// Token: 0x040000BE RID: 190
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		// Token: 0x040000BF RID: 191
		internal int version;
	}
}
