using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020004F8 RID: 1272
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRendererUtility.h")]
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	internal class Utility
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002382 RID: 9090 RVA: 0x00082AAC File Offset: 0x00080CAC
		// (remove) Token: 0x06002383 RID: 9091 RVA: 0x00082AE0 File Offset: 0x00080CE0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> GraphicsResourcesRecreate;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002384 RID: 9092 RVA: 0x00082B14 File Offset: 0x00080D14
		// (remove) Token: 0x06002385 RID: 9093 RVA: 0x00082B48 File Offset: 0x00080D48
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action EngineUpdate;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002386 RID: 9094 RVA: 0x00082B7C File Offset: 0x00080D7C
		// (remove) Token: 0x06002387 RID: 9095 RVA: 0x00082BB0 File Offset: 0x00080DB0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action FlushPendingResources;

		// Token: 0x06002388 RID: 9096 RVA: 0x00082BE3 File Offset: 0x00080DE3
		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> graphicsResourcesRecreate = Utility.GraphicsResourcesRecreate;
			if (graphicsResourcesRecreate != null)
			{
				graphicsResourcesRecreate(recreate);
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00082BF8 File Offset: 0x00080DF8
		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.EngineUpdate();
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00082C1F File Offset: 0x00080E1F
		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action flushPendingResources = Utility.FlushPendingResources;
			if (flushPendingResources != null)
			{
				flushPendingResources();
			}
		}

		// Token: 0x0600238B RID: 9099
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		// Token: 0x0600238C RID: 9100
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeBuffer(IntPtr buffer);

		// Token: 0x0600238D RID: 9101
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		// Token: 0x0600238E RID: 9102 RVA: 0x00082C34 File Offset: 0x00080E34
		[ThreadSafe]
		public unsafe static IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes)
		{
			Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(vertexAttributes);
			IntPtr vertexDeclaration_Injected;
			fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				vertexDeclaration_Injected = Utility.GetVertexDeclaration_Injected(ref managedSpanWrapper);
			}
			return vertexDeclaration_Injected;
		}

		// Token: 0x0600238F RID: 9103
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		// Token: 0x06002390 RID: 9104 RVA: 0x00082C6C File Offset: 0x00080E6C
		[ThreadSafe]
		public static void SetPropertyBlock(MaterialPropertyBlock props)
		{
			Utility.SetPropertyBlock_Injected((props == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(props));
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00082C90 File Offset: 0x00080E90
		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		// Token: 0x06002392 RID: 9106
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableScissor();

		// Token: 0x06002393 RID: 9107 RVA: 0x00082CA4 File Offset: 0x00080EA4
		[ThreadSafe]
		public static IntPtr CreateStencilState(StencilState stencilState)
		{
			return Utility.CreateStencilState_Injected(ref stencilState);
		}

		// Token: 0x06002394 RID: 9108
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStencilState(IntPtr stencilState, int stencilRef);

		// Token: 0x06002395 RID: 9109
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasMappedBufferRange();

		// Token: 0x06002396 RID: 9110
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint InsertCPUFence();

		// Token: 0x06002397 RID: 9111
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CPUFencePassed(uint fence);

		// Token: 0x06002398 RID: 9112
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForCPUFencePassed(uint fence);

		// Token: 0x06002399 RID: 9113
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncRenderThread();

		// Token: 0x0600239A RID: 9114 RVA: 0x00082CB8 File Offset: 0x00080EB8
		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt rectInt;
			Utility.GetActiveViewport_Injected(out rectInt);
			return rectInt;
		}

		// Token: 0x0600239B RID: 9115
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainBegin();

		// Token: 0x0600239C RID: 9116
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainEnd();

		// Token: 0x0600239D RID: 9117
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		// Token: 0x0600239E RID: 9118 RVA: 0x00082CD0 File Offset: 0x00080ED0
		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetUnityProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x060023A0 RID: 9120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetVertexDeclaration_Injected(ref ManagedSpanWrapper vertexAttributes);

		// Token: 0x060023A1 RID: 9121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPropertyBlock_Injected(IntPtr props);

		// Token: 0x060023A2 RID: 9122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScissorRect_Injected([In] ref RectInt scissorRect);

		// Token: 0x060023A3 RID: 9123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateStencilState_Injected([In] ref StencilState stencilState);

		// Token: 0x060023A4 RID: 9124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		// Token: 0x060023A5 RID: 9125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x04001038 RID: 4152
		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		// Token: 0x020004F9 RID: 1273
		internal enum GPUBufferType
		{
			// Token: 0x0400103A RID: 4154
			Vertex,
			// Token: 0x0400103B RID: 4155
			Index
		}

		// Token: 0x020004FA RID: 1274
		public class GPUBuffer<T> : IDisposable where T : struct
		{
			// Token: 0x060023A6 RID: 9126 RVA: 0x00082CF6 File Offset: 0x00080EF6
			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			// Token: 0x060023A7 RID: 9127 RVA: 0x00082D28 File Offset: 0x00080F28
			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			// Token: 0x060023A8 RID: 9128 RVA: 0x00082D37 File Offset: 0x00080F37
			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00082D5C File Offset: 0x00080F5C
			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x060023AA RID: 9130 RVA: 0x00082D74 File Offset: 0x00080F74
			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x0400103C RID: 4156
			private IntPtr buffer;

			// Token: 0x0400103D RID: 4157
			private int elemCount;

			// Token: 0x0400103E RID: 4158
			private int elemStride;
		}
	}
}
