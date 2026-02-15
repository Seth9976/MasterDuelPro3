using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D2 RID: 466
	[UsedByNativeCode]
	[NativeClass("GraphicsBuffer")]
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x0002629C File Offset: 0x0002449C
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000262D0 File Offset: 0x000244D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000262E4 File Offset: 0x000244E4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				ComputeBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of ComputeBuffer. Please use ComputeBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x060011D1 RID: 4561
		[FreeFunction("GraphicsBuffer_Bindings::InitComputeBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage);

		// Token: 0x060011D2 RID: 4562 RVA: 0x00026330 File Offset: 0x00024530
		[FreeFunction("GraphicsBuffer_Bindings::DestroyComputeBuffer")]
		private static void DestroyBuffer(ComputeBuffer buf)
		{
			ComputeBuffer.DestroyBuffer_Injected((buf == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00026351 File Offset: 0x00024551
		public ComputeBuffer(int count, int stride)
			: this(count, stride, ComputeBufferType.Default, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00026360 File Offset: 0x00024560
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
			: this(count, stride, type, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00026370 File Offset: 0x00024570
		private ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage, int stackDepth)
		{
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a zero length compute buffer", "count");
			}
			bool flag2 = stride <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a compute buffer with a negative or null stride", "stride");
			}
			long bufferSize = (long)count * (long)stride;
			long maxBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag3 = bufferSize > maxBufferSize;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The total size of the compute buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", bufferSize, maxBufferSize));
			}
			this.m_Ptr = ComputeBuffer.InitBuffer(count, stride, type, usage);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00026400 File Offset: 0x00024600
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0002640C File Offset: 0x0002460C
		public int count
		{
			get
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ComputeBuffer.get_count_Injected(intPtr);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00026430 File Offset: 0x00024630
		public int stride
		{
			get
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ComputeBuffer.get_stride_Injected(intPtr);
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00026454 File Offset: 0x00024654
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000264B9 File Offset: 0x000246B9
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000264DC File Offset: 0x000246DC
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, computeBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0002654C File Offset: 0x0002474C
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.InternalSetNativeData_Injected(intPtr, data, nativeBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00026578 File Offset: 0x00024778
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.InternalSetData_Injected(intPtr, data, managedBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x060011DE RID: 4574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer_Injected(IntPtr buf);

		// Token: 0x060011DF RID: 4575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_count_Injected(IntPtr _unity_self);

		// Token: 0x060011E0 RID: 4576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_stride_Injected(IntPtr _unity_self);

		// Token: 0x060011E1 RID: 4577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetNativeData_Injected(IntPtr _unity_self, IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x060011E2 RID: 4578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x040006B3 RID: 1715
		internal IntPtr m_Ptr;

		// Token: 0x020001D3 RID: 467
		internal static class BindingsMarshaller
		{
			// Token: 0x060011E3 RID: 4579 RVA: 0x000265A1 File Offset: 0x000247A1
			public static IntPtr ConvertToNative(ComputeBuffer computeBuffer)
			{
				return computeBuffer.m_Ptr;
			}
		}
	}
}
