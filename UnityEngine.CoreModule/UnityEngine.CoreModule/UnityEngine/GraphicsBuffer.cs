using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F2 RID: 242
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	public sealed class GraphicsBuffer : IDisposable
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x00011760 File Offset: 0x0000F960
		~GraphicsBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00011794 File Offset: 0x0000F994
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000117A8 File Offset: 0x0000F9A8
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GraphicsBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000117F4 File Offset: 0x0000F9F4
		private static bool RequiresCompute(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target requiresComputeMask = GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.Raw | GraphicsBuffer.Target.Append | GraphicsBuffer.Target.Counter | GraphicsBuffer.Target.IndirectArguments;
			return (target & requiresComputeMask) > (GraphicsBuffer.Target)0;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00011814 File Offset: 0x0000FA14
		private static bool IsVertexIndexOrCopyOnly(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target mask = GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index | GraphicsBuffer.Target.CopySource | GraphicsBuffer.Target.CopyDestination;
			return (target & mask) == target;
		}

		// Token: 0x0600092B RID: 2347
		[FreeFunction("GraphicsBuffer_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride);

		// Token: 0x0600092C RID: 2348 RVA: 0x00011830 File Offset: 0x0000FA30
		[FreeFunction("GraphicsBuffer_Bindings::DestroyBuffer")]
		private static void DestroyBuffer(GraphicsBuffer buf)
		{
			GraphicsBuffer.DestroyBuffer_Injected((buf == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00011854 File Offset: 0x0000FA54
		public GraphicsBuffer(GraphicsBuffer.Target target, int count, int stride)
		{
			GraphicsBuffer.UsageFlags usageFlags = (((target & (GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index)) == target) ? GraphicsBuffer.UsageFlags.LockBufferForWrite : GraphicsBuffer.UsageFlags.None);
			this.InternalInitialization(target, usageFlags, count, stride);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00011883 File Offset: 0x0000FA83
		public GraphicsBuffer(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride)
		{
			this.InternalInitialization(target, usageFlags, count, stride);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001189C File Offset: 0x0000FA9C
		private void InternalInitialization(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride)
		{
			bool flag = GraphicsBuffer.RequiresCompute(target) && !SystemInfo.supportsComputeShaders;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a graphics buffer that requires compute shader support, but compute shaders are not supported on this platform. Target: " + target.ToString());
			}
			bool flag2 = count <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
			}
			bool flag3 = stride <= 0;
			if (flag3)
			{
				throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
			}
			bool flag4 = (target & GraphicsBuffer.Target.Index) != (GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
			if (flag4)
			{
				throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride.ToString(), "stride");
			}
			bool flag5 = !GraphicsBuffer.IsVertexIndexOrCopyOnly(target) && stride % 4 != 0;
			if (flag5)
			{
				throw new ArgumentException("Stride must be a multiple of 4 unless the buffer is only used as a vertex buffer and/or index buffer ", "stride");
			}
			long bufferSize = (long)count * (long)stride;
			long maxBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag6 = bufferSize > maxBufferSize;
			if (flag6)
			{
				throw new ArgumentException(string.Format("The total size of the graphics buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", bufferSize, maxBufferSize));
			}
			bool flag7 = (usageFlags & GraphicsBuffer.UsageFlags.LockBufferForWrite) != GraphicsBuffer.UsageFlags.None && (target & GraphicsBuffer.Target.CopyDestination) > (GraphicsBuffer.Target)0;
			if (flag7)
			{
				throw new ArgumentException("Attempting to create a LockBufferForWrite capable buffer that can be copied into. LockBufferForWrite buffers are read-only on the GPU.");
			}
			this.m_Ptr = GraphicsBuffer.InitBuffer(target, usageFlags, count, stride);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000119DF File Offset: 0x0000FBDF
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000119EC File Offset: 0x0000FBEC
		[FreeFunction("GraphicsBuffer_Bindings::IsValidBuffer")]
		private static bool IsValidBuffer(GraphicsBuffer buf)
		{
			return GraphicsBuffer.IsValidBuffer_Injected((buf == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00011A10 File Offset: 0x0000FC10
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero && GraphicsBuffer.IsValidBuffer(this);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00011A40 File Offset: 0x0000FC40
		public int count
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsBuffer.get_count_Injected(intPtr);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00011A64 File Offset: 0x0000FC64
		public int stride
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsBuffer.get_stride_Injected(intPtr);
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00011A88 File Offset: 0x0000FC88
		[FreeFunction(Name = "GraphicsBuffer_Bindings::GetUsageFlags", HasExplicitThis = true)]
		private GraphicsBuffer.UsageFlags GetUsageFlags()
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsBuffer.GetUsageFlags_Injected(intPtr);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public GraphicsBuffer.UsageFlags usageFlags
		{
			get
			{
				return this.GetUsageFlags();
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		public GraphicsBufferHandle bufferHandle
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsBufferHandle graphicsBufferHandle;
				GraphicsBuffer.get_bufferHandle_Injected(intPtr, out graphicsBufferHandle);
				return graphicsBufferHandle;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00011AE9 File Offset: 0x0000FCE9
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public void SetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00011C20 File Offset: 0x0000FE20
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.InternalSetNativeData_Injected(intPtr, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00011C4C File Offset: 0x0000FE4C
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.InternalSetData_Injected(intPtr, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00011C78 File Offset: 0x0000FE78
		private unsafe void* BeginBufferWrite(int offset = 0, int size = 0)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsBuffer.BeginBufferWrite_Injected(intPtr, offset, size);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00011C9C File Offset: 0x0000FE9C
		public unsafe NativeArray<T> LockBufferForWrite<T>(int bufferStartIndex, int count) where T : struct
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("LockBufferForWrite requires a valid GraphicsBuffer");
			}
			bool flag2 = (this.usageFlags & GraphicsBuffer.UsageFlags.LockBufferForWrite) == GraphicsBuffer.UsageFlags.None;
			if (flag2)
			{
				throw new InvalidOperationException("GraphicsBuffer must be created with usage mode UsageFlage.LockBufferForWrite to use LockBufferForWrite");
			}
			int elementSize = UnsafeUtility.SizeOf<T>();
			bool flag3 = bufferStartIndex < 0 || count < 0 || (bufferStartIndex + count) * elementSize > this.count * this.stride;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (bufferStartIndex:{0} count:{1} elementSize:{2}, this.count:{3}, this.stride{4})", new object[] { bufferStartIndex, count, elementSize, this.count, this.stride }));
			}
			void* ptr = this.BeginBufferWrite(bufferStartIndex * elementSize, count * elementSize);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, count, Allocator.Invalid);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00011D78 File Offset: 0x0000FF78
		private void EndBufferWrite(int bytesWritten = 0)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.EndBufferWrite_Injected(intPtr, bytesWritten);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public void UnlockBufferAfterWrite<T>(int countWritten) where T : struct
		{
			bool flag = countWritten < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (countWritten:{0})", countWritten));
			}
			int elementSize = UnsafeUtility.SizeOf<T>();
			this.EndBufferWrite(countWritten * elementSize);
		}

		// Token: 0x17000176 RID: 374
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		private unsafe void SetName(string name)
		{
			try
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GraphicsBuffer.SetName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000943 RID: 2371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer_Injected(IntPtr buf);

		// Token: 0x06000944 RID: 2372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidBuffer_Injected(IntPtr buf);

		// Token: 0x06000945 RID: 2373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_count_Injected(IntPtr _unity_self);

		// Token: 0x06000946 RID: 2374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_stride_Injected(IntPtr _unity_self);

		// Token: 0x06000947 RID: 2375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.UsageFlags GetUsageFlags_Injected(IntPtr _unity_self);

		// Token: 0x06000948 RID: 2376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bufferHandle_Injected(IntPtr _unity_self, out GraphicsBufferHandle ret);

		// Token: 0x06000949 RID: 2377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetNativeData_Injected(IntPtr _unity_self, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x0600094A RID: 2378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x0600094B RID: 2379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* BeginBufferWrite_Injected(IntPtr _unity_self, int offset, int size);

		// Token: 0x0600094C RID: 2380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndBufferWrite_Injected(IntPtr _unity_self, int bytesWritten);

		// Token: 0x0600094D RID: 2381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x040002BA RID: 698
		internal IntPtr m_Ptr;

		// Token: 0x020000F3 RID: 243
		[Flags]
		public enum Target
		{
			// Token: 0x040002BC RID: 700
			Vertex = 1,
			// Token: 0x040002BD RID: 701
			Index = 2,
			// Token: 0x040002BE RID: 702
			CopySource = 4,
			// Token: 0x040002BF RID: 703
			CopyDestination = 8,
			// Token: 0x040002C0 RID: 704
			Structured = 16,
			// Token: 0x040002C1 RID: 705
			Raw = 32,
			// Token: 0x040002C2 RID: 706
			Append = 64,
			// Token: 0x040002C3 RID: 707
			Counter = 128,
			// Token: 0x040002C4 RID: 708
			IndirectArguments = 256,
			// Token: 0x040002C5 RID: 709
			Constant = 512
		}

		// Token: 0x020000F4 RID: 244
		[Flags]
		public enum UsageFlags
		{
			// Token: 0x040002C7 RID: 711
			None = 0,
			// Token: 0x040002C8 RID: 712
			LockBufferForWrite = 1
		}

		// Token: 0x020000F5 RID: 245
		internal static class BindingsMarshaller
		{
			// Token: 0x0600094E RID: 2382 RVA: 0x00011E48 File Offset: 0x00010048
			public static IntPtr ConvertToNative(GraphicsBuffer graphicsBuffer)
			{
				return graphicsBuffer.m_Ptr;
			}
		}
	}
}
