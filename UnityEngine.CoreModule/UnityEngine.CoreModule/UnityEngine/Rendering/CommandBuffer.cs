using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Profiling;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000365 RID: 869
	[NativeType("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Export/Graphics/RenderingCommandBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingShader.h")]
	[UsedByNativeCode]
	public class CommandBuffer : IDisposable
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x0002F788 File Offset: 0x0002D988
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_1(src, callback, &data);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0002F7BC File Offset: 0x0002D9BC
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, &data);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_8(src, callback, &data);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0002F828 File Offset: 0x0002DA28
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, &data);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0002F860 File Offset: 0x0002DA60
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_3(src, callback, &data);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0002F894 File Offset: 0x0002DA94
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, &data);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &data);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0002F944 File Offset: 0x0002DB44
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &data);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0002F984 File Offset: 0x0002DB84
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0002F9D4 File Offset: 0x0002DBD4
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData data = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &data);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0002FA18 File Offset: 0x0002DC18
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_1([NotNull] ComputeBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_1_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0002FA70 File Offset: 0x0002DC70
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_2([NotNull] ComputeBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_2_Injected(intPtr, intPtr2, size, offset, callback, nativeArrayData);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_3([NotNull] Texture src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_3_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0002FB28 File Offset: 0x0002DD28
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_4([NotNull] Texture src, int mipIndex, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_4_Injected(intPtr, intPtr2, mipIndex, callback, nativeArrayData);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0002FB84 File Offset: 0x0002DD84
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_5([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_5_Injected(intPtr, intPtr2, mipIndex, dstFormat, callback, nativeArrayData);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0002FBE4 File Offset: 0x0002DDE4
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_6([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_6_Injected(intPtr, intPtr2, mipIndex, x, width, y, height, z, depth, callback, nativeArrayData);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_7([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_7_Injected(intPtr, intPtr2, mipIndex, x, width, y, height, z, depth, dstFormat, callback, nativeArrayData);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0002FCB8 File Offset: 0x0002DEB8
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_8([NotNull] GraphicsBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_8_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0002FD10 File Offset: 0x0002DF10
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_9([NotNull] GraphicsBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_9_Injected(intPtr, intPtr2, size, offset, callback, nativeArrayData);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0002FD70 File Offset: 0x0002DF70
		[NativeMethod("AddSetInvertCulling")]
		public void SetInvertCulling(bool invertCulling)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetInvertCulling_Injected(intPtr, invertCulling);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0002FD94 File Offset: 0x0002DF94
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetSinglePassStereo", HasExplicitThis = true)]
		private void Internal_SetSinglePassStereo(SinglePassStereoMode mode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_SetSinglePassStereo_Injected(intPtr, mode);
		}

		// Token: 0x060016B1 RID: 5809
		[FreeFunction("RenderingCommandBuffer_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer();

		// Token: 0x060016B2 RID: 5810 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
		[FreeFunction("RenderingCommandBuffer_Bindings::CreateGPUFence_Internal", HasExplicitThis = true)]
		private IntPtr CreateGPUFence_Internal(GraphicsFenceType fenceType, SynchronisationStageFlags stage)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CommandBuffer.CreateGPUFence_Internal_Injected(intPtr, fenceType, stage);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		[FreeFunction("RenderingCommandBuffer_Bindings::WaitOnGPUFence_Internal", HasExplicitThis = true)]
		private void WaitOnGPUFence_Internal(IntPtr fencePtr, SynchronisationStageFlags stage)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.WaitOnGPUFence_Internal_Injected(intPtr, fencePtr, stage);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0002FE00 File Offset: 0x0002E000
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseBuffer", HasExplicitThis = true, IsThreadSafe = true)]
		private void ReleaseBuffer()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ReleaseBuffer_Injected(intPtr);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0002FE24 File Offset: 0x0002E024
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeFloatParam", HasExplicitThis = true)]
		public void SetComputeFloatParam([NotNull] ComputeShader computeShader, int nameID, float val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeFloatParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0002FE6C File Offset: 0x0002E06C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeIntParam", HasExplicitThis = true)]
		public void SetComputeIntParam([NotNull] ComputeShader computeShader, int nameID, int val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeIntParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorParam", HasExplicitThis = true)]
		public void SetComputeVectorParam([NotNull] ComputeShader computeShader, int nameID, Vector4 val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeVectorParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0002FF00 File Offset: 0x0002E100
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorArrayParam", HasExplicitThis = true)]
		public unsafe void SetComputeVectorArrayParam([NotNull] ComputeShader computeShader, int nameID, Vector4[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetComputeVectorArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0002FF70 File Offset: 0x0002E170
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixParam", HasExplicitThis = true)]
		public void SetComputeMatrixParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4 val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeMatrixParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixArrayParam", HasExplicitThis = true)]
		public unsafe void SetComputeMatrixArrayParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetComputeMatrixArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0003002C File Offset: 0x0002E22C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeFloats", HasExplicitThis = true)]
		private unsafe void Internal_SetComputeFloats([NotNull] ComputeShader computeShader, int nameID, float[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetComputeFloats_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0003009C File Offset: 0x0002E29C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeInts", HasExplicitThis = true)]
		private unsafe void Internal_SetComputeInts([NotNull] ComputeShader computeShader, int nameID, int[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetComputeInts_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0003010C File Offset: 0x0002E30C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeTextureParam", HasExplicitThis = true)]
		private void Internal_SetComputeTextureParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ref RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeTextureParam_Injected(intPtr, intPtr2, kernelIndex, nameID, ref rt, mipLevel, element);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0003015C File Offset: 0x0002E35C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeBufferParam_Injected(intPtr, intPtr2, kernelIndex, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000301B8 File Offset: 0x0002E3B8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeGraphicsBufferHandleParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeGraphicsBufferHandleParam_Injected(intPtr, intPtr2, kernelIndex, nameID, ref bufferHandle);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00030204 File Offset: 0x0002E404
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeGraphicsBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeGraphicsBufferParam_Injected(intPtr, intPtr2, kernelIndex, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00030260 File Offset: 0x0002E460
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeConstantComputeBufferParam([NotNull] ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeConstantComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000302BC File Offset: 0x0002E4BC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeConstantGraphicsBufferParam([NotNull] ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeConstantGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00030318 File Offset: 0x0002E518
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchCompute", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchCompute([NotNull] ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchCompute_Injected(intPtr, intPtr2, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00030364 File Offset: 0x0002E564
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchComputeIndirect([NotNull] ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchComputeIndirect_Injected(intPtr, intPtr2, kernelIndex, (indirectBuffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(indirectBuffer), argsOffset);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000303BC File Offset: 0x0002E5BC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchComputeIndirectGraphicsBuffer([NotNull] ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchComputeIndirectGraphicsBuffer_Injected(intPtr, intPtr2, kernelIndex, (indirectBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indirectBuffer), argsOffset);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00030414 File Offset: 0x0002E614
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0003046C File Offset: 0x0002E66C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000304C4 File Offset: 0x0002E6C4
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingGraphicsBufferHandleParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingGraphicsBufferHandleParam_Injected(intPtr, intPtr2, nameID, ref bufferHandle);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00030510 File Offset: 0x0002E710
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingConstantComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingConstantComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0003056C File Offset: 0x0002E76C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingConstantGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingConstantGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000305C8 File Offset: 0x0002E7C8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingTextureParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingTextureParam([NotNull] RayTracingShader rayTracingShader, int nameID, ref RenderTargetIdentifier rt)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingTextureParam_Injected(intPtr, intPtr2, nameID, ref rt);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00030610 File Offset: 0x0002E810
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloatParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingFloatParam([NotNull] RayTracingShader rayTracingShader, int nameID, float val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingFloatParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00030658 File Offset: 0x0002E858
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingIntParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingIntParam([NotNull] RayTracingShader rayTracingShader, int nameID, int val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingIntParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000306A0 File Offset: 0x0002E8A0
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingVectorParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingVectorParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000306EC File Offset: 0x0002E8EC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorArrayParam", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingVectorArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingVectorArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0003075C File Offset: 0x0002E95C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingMatrixParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingMatrixParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000307A8 File Offset: 0x0002E9A8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixArrayParam", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingMatrixArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingMatrixArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00030818 File Offset: 0x0002EA18
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloats", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingFloats([NotNull] RayTracingShader rayTracingShader, int nameID, float[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingFloats_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00030888 File Offset: 0x0002EA88
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingInts", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingInts([NotNull] RayTracingShader rayTracingShader, int nameID, int[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingInts_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000308F8 File Offset: 0x0002EAF8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_BuildRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_BuildRayTracingAccelerationStructure([NotNull] RayTracingAccelerationStructure accelerationStructure, RayTracingAccelerationStructure.BuildSettings buildSettings)
		{
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_BuildRayTracingAccelerationStructure_Injected(intPtr, intPtr2, ref buildSettings);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00030940 File Offset: 0x0002EB40
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_SetRayTracingAccelerationStructure([NotNull] RayTracingShader rayTracingShader, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr3 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_SetRayTracingAccelerationStructure_Injected(intPtr, intPtr2, nameID, intPtr3);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000309AC File Offset: 0x0002EBAC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_SetComputeRayTracingAccelerationStructure([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr3 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_SetComputeRayTracingAccelerationStructure_Injected(intPtr, intPtr2, kernelIndex, nameID, intPtr3);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00030A1C File Offset: 0x0002EC1C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRays", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void Internal_DispatchRays([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, uint width, uint height, uint depth, Camera camera = null)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rayGenShaderName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rayGenShaderName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.Internal_DispatchRays_Injected(intPtr, intPtr2, ref managedSpanWrapper, width, height, depth, Object.MarshalledUnityObject.Marshal<Camera>(camera));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00030AB0 File Offset: 0x0002ECB0
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueCC(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueCC_Injected(intPtr, (src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00030AF4 File Offset: 0x0002ECF4
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueGC(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueGC_Injected(intPtr, (src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00030B38 File Offset: 0x0002ED38
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueCG(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueCG_Injected(intPtr, (src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00030B7C File Offset: 0x0002ED7C
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueGG(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueGG_Injected(intPtr, (src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x00030C00 File Offset: 0x0002EE00
		public unsafe string name
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					CommandBuffer.get_name_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					CommandBuffer.set_name_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00030C64 File Offset: 0x0002EE64
		public int sizeInBytes
		{
			[NativeMethod("GetBufferSize")]
			get
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CommandBuffer.get_sizeInBytes_Injected(intPtr);
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00030C88 File Offset: 0x0002EE88
		[NativeMethod("ClearCommands")]
		public void Clear()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Clear_Injected(intPtr);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00030CAC File Offset: 0x0002EEAC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMesh", HasExplicitThis = true)]
		private void Internal_DrawMesh([NotNull] Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			CommandBuffer.Internal_DrawMesh_Injected(intPtr, intPtr2, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), submeshIndex, shaderPass, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00030D10 File Offset: 0x0002EF10
		[NativeMethod("AddDrawMultipleMeshes")]
		private unsafe void Internal_DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<int> span2 = new Span<int>(subsetIndices);
				fixed (int* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					CommandBuffer.Internal_DrawMultipleMeshes_Injected(intPtr, ref managedSpanWrapper, meshes, ref managedSpanWrapper2, count, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
					ptr = null;
				}
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00030DA0 File Offset: 0x0002EFA0
		[NativeMethod("AddDrawRenderer")]
		private void Internal_DrawRenderer([NotNull] Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(renderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			CommandBuffer.Internal_DrawRenderer_Injected(intPtr, intPtr2, Object.MarshalledUnityObject.Marshal<Material>(material), submeshIndex, shaderPass);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00030DF0 File Offset: 0x0002EFF0
		[NativeMethod("AddDrawRendererList")]
		private void Internal_DrawRendererList(RendererList rendererList)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawRendererList_Injected(intPtr, ref rendererList);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00030E14 File Offset: 0x0002F014
		[NativeMethod("AddDrawProcedural")]
		private void Internal_DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProcedural_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, vertexCount, instanceCount, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00030E58 File Offset: 0x0002F058
		[NativeMethod("AddDrawProceduralIndexed")]
		private void Internal_DrawProceduralIndexed(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexed_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, indexCount, instanceCount, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00030EAC File Offset: 0x0002F0AC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndirect_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00030F00 File Offset: 0x0002F100
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexedIndirect_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00030F64 File Offset: 0x0002F164
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirectGraphicsBuffer(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndirectGraphicsBuffer_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0003101C File Offset: 0x0002F21C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstanced", HasExplicitThis = true)]
		private unsafe void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Mesh>(mesh);
			IntPtr intPtr3 = Object.MarshalledUnityObject.Marshal<Material>(material);
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_DrawMeshInstanced_Injected(intPtr, intPtr2, submeshIndex, intPtr3, shaderPass, ref managedSpanWrapper, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00031088 File Offset: 0x0002F288
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedProcedural", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedProcedural_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000310CC File Offset: 0x0002F2CC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedIndirect_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00031124 File Offset: 0x0002F324
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedIndirectGraphicsBuffer(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0003117C File Offset: 0x0002F37C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawOcclusionMesh", HasExplicitThis = true)]
		private void Internal_DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawOcclusionMesh_Injected(intPtr, ref normalizedCamViewport);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000311A0 File Offset: 0x0002F3A0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Texture", HasExplicitThis = true, ThrowsException = true)]
		private void SetRandomWriteTarget_Texture(int index, ref RenderTargetIdentifier rt)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRandomWriteTarget_Texture_Injected(intPtr, index, ref rt);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000311C4 File Offset: 0x0002F3C4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
		private void SetRandomWriteTarget_GraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRandomWriteTarget_GraphicsBuffer_Injected(intPtr, index, (uav == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(uav), preserveCounterValue);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000311F8 File Offset: 0x0002F3F8
		[FreeFunction("RenderingCommandBuffer_Bindings::ClearRandomWriteTargets", HasExplicitThis = true, ThrowsException = true)]
		public void ClearRandomWriteTargets()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ClearRandomWriteTargets_Injected(intPtr);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0003121C File Offset: 0x0002F41C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewport", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewport(Rect pixelRect)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetViewport_Injected(intPtr, ref pixelRect);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00031240 File Offset: 0x0002F440
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		public void EnableScissorRect(Rect scissor)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableScissorRect_Injected(intPtr, ref scissor);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00031264 File Offset: 0x0002F464
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		public void DisableScissorRect()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableScissorRect_Injected(intPtr);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00031288 File Offset: 0x0002F488
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Texture", HasExplicitThis = true)]
		private void Blit_Texture(Texture source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Blit_Texture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(source), ref dest, Object.MarshalledUnityObject.Marshal<Material>(mat), pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000312C4 File Offset: 0x0002F4C4
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Identifier", HasExplicitThis = true)]
		private void Blit_Identifier(ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Blit_Identifier_Injected(intPtr, ref source, ref dest, Object.MarshalledUnityObject.Marshal<Material>(mat), pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000312F8 File Offset: 0x0002F4F8
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTWithDescriptor", HasExplicitThis = true)]
		private void GetTemporaryRTWithDescriptor(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.GetTemporaryRTWithDescriptor_Injected(intPtr, nameID, ref desc, filter);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0003131E File Offset: 0x0002F51E
		public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			this.GetTemporaryRTWithDescriptor(nameID, desc, filter);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0003132C File Offset: 0x0002F52C
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseTemporaryRT", HasExplicitThis = true)]
		public void ReleaseTemporaryRT(int nameID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ReleaseTemporaryRT_Injected(intPtr, nameID);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0003134F File Offset: 0x0002F54F
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			this.ClearRenderTarget(clearDepth, clearColor, backgroundColor, 1f, 0U);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00031362 File Offset: 0x0002F562
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			this.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, 0U);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00031372 File Offset: 0x0002F572
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ClearRenderTargetSingle_Internal((clearColor ? RTClearFlags.Color : RTClearFlags.None) | (clearDepth ? RTClearFlags.DepthStencil : RTClearFlags.None), backgroundColor, depth, stencil);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00031399 File Offset: 0x0002F599
		public void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ClearRenderTargetSingle_Internal(clearFlags, backgroundColor, depth, stencil);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000313B4 File Offset: 0x0002F5B4
		public void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = backgroundColors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of clear colors must be at least 1, but is {0}", backgroundColors.Length));
			}
			bool flag2 = backgroundColors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of clear colors ({0}) exceeds the maximum supported number of render targets ({1})", backgroundColors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.ClearRenderTargetMulti_Internal(clearFlags, backgroundColors, depth, stencil);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0003142C File Offset: 0x0002F62C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloat", HasExplicitThis = true)]
		public void SetGlobalFloat(int nameID, float value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalFloat_Injected(intPtr, nameID, value);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00031450 File Offset: 0x0002F650
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInt", HasExplicitThis = true)]
		public void SetGlobalInt(int nameID, int value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalInt_Injected(intPtr, nameID, value);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00031474 File Offset: 0x0002F674
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInteger", HasExplicitThis = true)]
		public void SetGlobalInteger(int nameID, int value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalInteger_Injected(intPtr, nameID, value);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00031498 File Offset: 0x0002F698
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVector", HasExplicitThis = true)]
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalVector_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000314C0 File Offset: 0x0002F6C0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalColor", HasExplicitThis = true)]
		public void SetGlobalColor(int nameID, Color value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalColor_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000314E8 File Offset: 0x0002F6E8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrix", HasExplicitThis = true)]
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalMatrix_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00031510 File Offset: 0x0002F710
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
		public unsafe void EnableShaderKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.EnableShaderKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00031574 File Offset: 0x0002F774
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
		private void EnableGlobalKeyword(GlobalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableGlobalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00031598 File Offset: 0x0002F798
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableMaterialKeyword", HasExplicitThis = true)]
		private void EnableMaterialKeyword(Material material, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000315C4 File Offset: 0x0002F7C4
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableComputeKeyword", HasExplicitThis = true)]
		private void EnableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000315EE File Offset: 0x0002F7EE
		public void EnableKeyword(in GlobalKeyword keyword)
		{
			this.EnableGlobalKeyword(keyword);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000315FE File Offset: 0x0002F7FE
		public void EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.EnableMaterialKeyword(material, keyword);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0003160F File Offset: 0x0002F80F
		public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.EnableComputeKeyword(computeShader, keyword);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00031620 File Offset: 0x0002F820
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
		public unsafe void DisableShaderKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.DisableShaderKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00031684 File Offset: 0x0002F884
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
		private void DisableGlobalKeyword(GlobalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableGlobalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000316A8 File Offset: 0x0002F8A8
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableMaterialKeyword", HasExplicitThis = true)]
		private void DisableMaterialKeyword(Material material, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000316D4 File Offset: 0x0002F8D4
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableComputeKeyword", HasExplicitThis = true)]
		private void DisableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000316FE File Offset: 0x0002F8FE
		public void DisableKeyword(in GlobalKeyword keyword)
		{
			this.DisableGlobalKeyword(keyword);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0003170E File Offset: 0x0002F90E
		public void DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.DisableMaterialKeyword(material, keyword);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0003171F File Offset: 0x0002F91F
		public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.DisableComputeKeyword(computeShader, keyword);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00031730 File Offset: 0x0002F930
		[FreeFunction("RenderingCommandBuffer_Bindings::SetShaderKeyword", HasExplicitThis = true)]
		private void SetGlobalKeyword(GlobalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalKeyword_Injected(intPtr, ref keyword, value);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00031758 File Offset: 0x0002F958
		[FreeFunction("RenderingCommandBuffer_Bindings::SetMaterialKeyword", HasExplicitThis = true)]
		private void SetMaterialKeyword(Material material, LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword, value);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00031784 File Offset: 0x0002F984
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeKeyword", HasExplicitThis = true)]
		private void SetComputeKeyword(ComputeShader computeShader, LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword, value);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000317AF File Offset: 0x0002F9AF
		public void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.SetGlobalKeyword(keyword, value);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000317C0 File Offset: 0x0002F9C0
		public void SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.SetMaterialKeyword(material, keyword, value);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000317D2 File Offset: 0x0002F9D2
		public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.SetComputeKeyword(computeShader, keyword, value);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000317E4 File Offset: 0x0002F9E4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetViewProjectionMatrices_Injected(intPtr, ref view, ref proj);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0003180C File Offset: 0x0002FA0C
		[NativeMethod("AddSetGlobalDepthBias")]
		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalDepthBias_Injected(intPtr, bias, slopeBias);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00031830 File Offset: 0x0002FA30
		[FreeFunction("RenderingCommandBuffer_Bindings::SetExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		public void SetExecutionFlags(CommandBufferExecutionFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetExecutionFlags_Injected(intPtr, flags);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00031854 File Offset: 0x0002FA54
		[FreeFunction("RenderingCommandBuffer_Bindings::ValidateAgainstExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		private bool ValidateAgainstExecutionFlags(CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CommandBuffer.ValidateAgainstExecutionFlags_Injected(intPtr, requiredFlags, invalidFlags);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00031878 File Offset: 0x0002FA78
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalFloatArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalFloatArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0003189C File Offset: 0x0002FA9C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalVectorArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalVectorArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000318C0 File Offset: 0x0002FAC0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalMatrixArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalMatrixArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000318E4 File Offset: 0x0002FAE4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalFloatArray(int nameID, [NotNull] float[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalFloatArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0003193C File Offset: 0x0002FB3C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalVectorArray(int nameID, [NotNull] Vector4[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalVectorArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00031994 File Offset: 0x0002FB94
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalMatrixArray(int nameID, [NotNull] Matrix4x4[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalMatrixArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000319EC File Offset: 0x0002FBEC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetLateLatchProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetLateLatchProjectionMatrices([NotNull] Matrix4x4[] projectionMat)
		{
			if (projectionMat == null)
			{
				ThrowHelper.ThrowArgumentNullException(projectionMat, "projectionMat");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(projectionMat);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetLateLatchProjectionMatrices_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00031A44 File Offset: 0x0002FC44
		[FreeFunction("RenderingCommandBuffer_Bindings::MarkLateLatchMatrixShaderPropertyID", HasExplicitThis = true)]
		public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.MarkLateLatchMatrixShaderPropertyID_Injected(intPtr, matrixPropertyType, shaderPropertyID);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00031A68 File Offset: 0x0002FC68
		[FreeFunction("RenderingCommandBuffer_Bindings::UnmarkLateLatchMatrix", HasExplicitThis = true)]
		public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.UnmarkLateLatchMatrix_Injected(intPtr, matrixPropertyType);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00031A8C File Offset: 0x0002FC8C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalTexture_Impl", HasExplicitThis = true)]
		private void SetGlobalTexture_Impl(int nameID, ref RenderTargetIdentifier rt, RenderTextureSubElement element)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalTexture_Impl_Injected(intPtr, nameID, ref rt, element);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00031AB4 File Offset: 0x0002FCB4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		private void SetGlobalBufferInternal(int nameID, ComputeBuffer value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalBufferInternal_Injected(intPtr, nameID, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		private void SetGlobalGraphicsBufferInternal(int nameID, GraphicsBuffer value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalGraphicsBufferInternal_Injected(intPtr, nameID, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00031B1C File Offset: 0x0002FD1C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetShadowSamplingMode_Impl", HasExplicitThis = true)]
		private void SetShadowSamplingMode_Impl(ref RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetShadowSamplingMode_Impl_Injected(intPtr, ref shadowmap, mode);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00031B40 File Offset: 0x0002FD40
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventInternal", HasExplicitThis = true)]
		private void IssuePluginEventInternal(IntPtr callback, int eventID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginEventInternal_Injected(intPtr, callback, eventID);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00031B64 File Offset: 0x0002FD64
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample", HasExplicitThis = true)]
		public unsafe void BeginSample(string name)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
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
				CommandBuffer.BeginSample_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample", HasExplicitThis = true)]
		public unsafe void EndSample(string name)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
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
				CommandBuffer.EndSample_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00031C2C File Offset: 0x0002FE2C
		public void BeginSample(CustomSampler sampler)
		{
			this.BeginSample_CustomSampler(sampler);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00031C37 File Offset: 0x0002FE37
		public void EndSample(CustomSampler sampler)
		{
			this.EndSample_CustomSampler(sampler);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00031C44 File Offset: 0x0002FE44
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_CustomSampler", HasExplicitThis = true)]
		private void BeginSample_CustomSampler([NotNull] CustomSampler sampler)
		{
			if (sampler == null)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CustomSampler.BindingsMarshaller.ConvertToNative(sampler);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			CommandBuffer.BeginSample_CustomSampler_Injected(intPtr, intPtr2);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00031C8C File Offset: 0x0002FE8C
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample_CustomSampler", HasExplicitThis = true)]
		private void EndSample_CustomSampler([NotNull] CustomSampler sampler)
		{
			if (sampler == null)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CustomSampler.BindingsMarshaller.ConvertToNative(sampler);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			CommandBuffer.EndSample_CustomSampler_Injected(intPtr, intPtr2);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00031CD4 File Offset: 0x0002FED4
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataInternal", HasExplicitThis = true)]
		private void IssuePluginEventAndDataInternal(IntPtr callback, int eventID, IntPtr data)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginEventAndDataInternal_Injected(intPtr, callback, eventID, data);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00031CFC File Offset: 0x0002FEFC
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomBlitInternal", HasExplicitThis = true)]
		private void IssuePluginCustomBlitInternal(IntPtr callback, uint command, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginCustomBlitInternal_Injected(intPtr, callback, command, ref source, ref dest, commandParam, commandFlags);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00031D28 File Offset: 0x0002FF28
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomTextureUpdateInternal", HasExplicitThis = true)]
		private void IssuePluginCustomTextureUpdateInternal(IntPtr callback, Texture targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginCustomTextureUpdateInternal_Injected(intPtr, callback, Object.MarshalledUnityObject.Marshal<Texture>(targetTexture), userData, useNewUnityRenderingExtTextureUpdateParamsV2);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00031D54 File Offset: 0x0002FF54
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		private void SetGlobalConstantBufferInternal(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalConstantBufferInternal_Injected(intPtr, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), nameID, offset, size);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00031D8C File Offset: 0x0002FF8C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		private void SetGlobalConstantGraphicsBufferInternal(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalConstantGraphicsBufferInternal_Injected(intPtr, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), nameID, offset, size);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00031DC4 File Offset: 0x0002FFC4
		[FreeFunction("RenderingCommandBuffer_Bindings::IncrementUpdateCount", HasExplicitThis = true)]
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IncrementUpdateCount_Injected(intPtr, ref dest);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00031DE8 File Offset: 0x0002FFE8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetInstanceMultiplier", HasExplicitThis = true)]
		public void SetInstanceMultiplier(uint multiplier)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetInstanceMultiplier_Injected(intPtr, multiplier);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00031E0C File Offset: 0x0003000C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetFoveatedRenderingMode", HasExplicitThis = true)]
		public void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetFoveatedRenderingMode_Injected(intPtr, foveatedRenderingMode);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00031E30 File Offset: 0x00030030
		[FreeFunction("RenderingCommandBuffer_Bindings::SetWireframe", HasExplicitThis = true)]
		public void SetWireframe(bool enable)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetWireframe_Injected(intPtr, enable);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00031E54 File Offset: 0x00030054
		[FreeFunction("RenderingCommandBuffer_Bindings::ConfigureFoveatedRendering", HasExplicitThis = true)]
		public void ConfigureFoveatedRendering(IntPtr platformData)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ConfigureFoveatedRendering_Injected(intPtr, platformData);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00031E78 File Offset: 0x00030078
		private static void CheckThrowOnSetRenderTarget()
		{
			bool throwOnSetRenderTarget = CommandBuffer.ThrowOnSetRenderTarget;
			if (throwOnSetRenderTarget)
			{
				throw new Exception("Setrendertarget is not allowed in this context");
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00031E9A File Offset: 0x0003009A
		public void SetRenderTarget(RenderTargetIdentifier rt)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetSingle_Internal(rt, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00031EB8 File Offset: 0x000300B8
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = loadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, loadAction, storeAction, loadAction, storeAction);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00031EF4 File Offset: 0x000300F4
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00031F3C File Offset: 0x0003013C
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, CubemapFace.Unknown, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00031F88 File Offset: 0x00030188
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00031FD8 File Offset: 0x000301D8
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, depthSlice), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00032048 File Offset: 0x00030248
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetColorDepth_Internal(color, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00032068 File Offset: 0x00030268
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, CubemapFace.Unknown, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000320BC File Offset: 0x000302BC
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00032110 File Offset: 0x00030310
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, depthSlice), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00032184 File Offset: 0x00030384
		public void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetColorDepth_Internal(color, depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, RenderTargetFlags.None);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000321D0 File Offset: 0x000303D0
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMulti_Internal(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00032250 File Offset: 0x00030450
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMultiSubtarget(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000322D4 File Offset: 0x000304D4
		public void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepthSubtarget(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
			else
			{
				this.SetRenderTargetMultiSubtarget(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000324A0 File Offset: 0x000306A0
		public void SetRenderTarget(RenderTargetBinding binding)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepth_Internal(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, binding.flags);
			}
			else
			{
				this.SetRenderTargetMulti_Internal(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, binding.flags);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00032674 File Offset: 0x00030874
		private void ClearRenderTargetSingle_Internal(RTClearFlags clearFlags, Color color, float depth, uint stencil)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ClearRenderTargetSingle_Internal_Injected(intPtr, clearFlags, ref color, depth, stencil);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0003269C File Offset: 0x0003089C
		private unsafe void ClearRenderTargetMulti_Internal(RTClearFlags clearFlags, Color[] colors, float depth, uint stencil)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.ClearRenderTargetMulti_Internal_Injected(intPtr, clearFlags, ref managedSpanWrapper, depth, stencil);
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000326E8 File Offset: 0x000308E8
		private void SetRenderTargetSingle_Internal(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetSingle_Internal_Injected(intPtr, ref rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00032714 File Offset: 0x00030914
		private void SetRenderTargetColorDepth_Internal(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetColorDepth_Internal_Injected(intPtr, ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, flags);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00032744 File Offset: 0x00030944
		private unsafe void SetRenderTargetMulti_Internal(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderTargetIdentifier> span = new Span<RenderTargetIdentifier>(colors);
			fixed (RenderTargetIdentifier* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<RenderBufferLoadAction> span2 = new Span<RenderBufferLoadAction>(colorLoadActions);
				fixed (RenderBufferLoadAction* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<RenderBufferStoreAction> span3 = new Span<RenderBufferStoreAction>(colorStoreActions);
					fixed (RenderBufferStoreAction* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						CommandBuffer.SetRenderTargetMulti_Internal_Injected(intPtr, ref managedSpanWrapper, ref depth, ref managedSpanWrapper2, ref managedSpanWrapper3, depthLoadAction, depthStoreAction, flags);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000327E8 File Offset: 0x000309E8
		private void SetRenderTargetColorDepthSubtarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetColorDepthSubtarget_Injected(intPtr, ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003281C File Offset: 0x00030A1C
		private unsafe void SetRenderTargetMultiSubtarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderTargetIdentifier> span = new Span<RenderTargetIdentifier>(colors);
			fixed (RenderTargetIdentifier* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<RenderBufferLoadAction> span2 = new Span<RenderBufferLoadAction>(colorLoadActions);
				fixed (RenderBufferLoadAction* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<RenderBufferStoreAction> span3 = new Span<RenderBufferStoreAction>(colorStoreActions);
					fixed (RenderBufferStoreAction* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						CommandBuffer.SetRenderTargetMultiSubtarget_Injected(intPtr, ref managedSpanWrapper, ref depth, ref managedSpanWrapper2, ref managedSpanWrapper3, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000328C4 File Offset: 0x00030AC4
		public void SetBufferData(ComputeBuffer buffer, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetComputeBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003292C File Offset: 0x00030B2C
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0003299E File Offset: 0x00030B9E
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000329C8 File Offset: 0x00030BC8
		public void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00032A70 File Offset: 0x00030C70
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00032B24 File Offset: 0x00030D24
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00032B98 File Offset: 0x00030D98
		public void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue)
		{
			this.InternalSetComputeBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00032BA4 File Offset: 0x00030DA4
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetComputeBufferNativeData([NotNull] ComputeBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferNativeData_Injected(intPtr, intPtr2, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00032BF4 File Offset: 0x00030DF4
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetComputeBufferData([NotNull] ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferData_Injected(intPtr, intPtr2, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00032C44 File Offset: 0x00030E44
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
		private void InternalSetComputeBufferCounterValue([NotNull] ComputeBuffer buffer, uint counterValue)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferCounterValue_Injected(intPtr, intPtr2, counterValue);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00032C8C File Offset: 0x00030E8C
		public void SetBufferData(GraphicsBuffer buffer, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetGraphicsBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00032CF4 File Offset: 0x00030EF4
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00032D66 File Offset: 0x00030F66
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00032D90 File Offset: 0x00030F90
		public void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00032E38 File Offset: 0x00031038
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00032EEC File Offset: 0x000310EC
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00032F60 File Offset: 0x00031160
		public void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue)
		{
			this.InternalSetGraphicsBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00032F6C File Offset: 0x0003116C
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetGraphicsBufferNativeData([NotNull] GraphicsBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferNativeData_Injected(intPtr, intPtr2, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00032FBC File Offset: 0x000311BC
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetGraphicsBufferData([NotNull] GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferData_Injected(intPtr, intPtr2, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0003300C File Offset: 0x0003120C
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
		private void InternalSetGraphicsBufferCounterValue([NotNull] GraphicsBuffer buffer, uint counterValue)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferCounterValue_Injected(intPtr, intPtr2, counterValue);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00033054 File Offset: 0x00031254
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginRenderPass", HasExplicitThis = true)]
		private unsafe void BeginRenderPass_Internal(int width, int height, int volumeDepth, int samples, ReadOnlySpan<AttachmentDescriptor> attachments, int depthAttachmentIndex, ReadOnlySpan<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<AttachmentDescriptor> readOnlySpan = attachments;
			fixed (AttachmentDescriptor* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				ReadOnlySpan<SubPassDescriptor> readOnlySpan2 = subPasses;
				fixed (SubPassDescriptor* ptr2 = readOnlySpan2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					ReadOnlySpan<byte> readOnlySpan3 = debugNameUtf8;
					fixed (byte* pinnableReference = readOnlySpan3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan3.Length);
						CommandBuffer.BeginRenderPass_Internal_Injected(intPtr, width, height, volumeDepth, samples, ref managedSpanWrapper, depthAttachmentIndex, ref managedSpanWrapper2, ref managedSpanWrapper3);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000330E8 File Offset: 0x000312E8
		public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.BeginRenderPass_Internal(width, height, volumeDepth, samples, in attachments, depthAttachmentIndex, in subPasses, debugNameUtf8);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00033120 File Offset: 0x00031320
		[FreeFunction("RenderingCommandBuffer_Bindings::NextSubPass", HasExplicitThis = true)]
		private void NextSubPass_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.NextSubPass_Internal_Injected(intPtr);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00033142 File Offset: 0x00031342
		public void NextSubPass()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.NextSubPass_Internal();
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00033158 File Offset: 0x00031358
		[FreeFunction("RenderingCommandBuffer_Bindings::EndRenderPass", HasExplicitThis = true)]
		private void EndRenderPass_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EndRenderPass_Internal_Injected(intPtr);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0003317A File Offset: 0x0003137A
		public void EndRenderPass()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.EndRenderPass_Internal();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00033190 File Offset: 0x00031390
		[FreeFunction("RenderingCommandBuffer_Bindings::SetupCameraProperties", HasExplicitThis = true)]
		private void SetupCameraProperties_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			CommandBuffer.SetupCameraProperties_Internal_Injected(intPtr, intPtr2);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000331D6 File Offset: 0x000313D6
		public void SetupCameraProperties(Camera camera)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetupCameraProperties_Internal(camera);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000331EC File Offset: 0x000313EC
		[FreeFunction("RenderingCommandBuffer_Bindings::InvokeOnRenderObjectCallbacks", HasExplicitThis = true)]
		private void InvokeOnRenderObjectCallbacks_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.InvokeOnRenderObjectCallbacks_Internal_Injected(intPtr);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0003320E File Offset: 0x0003140E
		public void InvokeOnRenderObjectCallbacks()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.InvokeOnRenderObjectCallbacks_Internal();
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00033224 File Offset: 0x00031424
		~CommandBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00033258 File Offset: 0x00031458
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0003326A File Offset: 0x0003146A
		private void Dispose(bool disposing)
		{
			this.ReleaseBuffer();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0003327F File Offset: 0x0003147F
		public CommandBuffer()
		{
			this.m_Ptr = CommandBuffer.InitBuffer();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00033294 File Offset: 0x00031494
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000332A0 File Offset: 0x000314A0
		public GraphicsFence CreateAsyncGraphicsFence()
		{
			return this.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000332BC File Offset: 0x000314BC
		public GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, SynchronisationStageFlags stage)
		{
			GraphicsFence newFence = default(GraphicsFence);
			newFence.m_FenceType = fenceType;
			newFence.m_Ptr = this.CreateGPUFence_Internal(fenceType, stage);
			newFence.InitPostAllocation();
			newFence.Validate();
			return newFence;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000332FE File Offset: 0x000314FE
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			this.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.VertexProcessing);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0003330A File Offset: 0x0003150A
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStage stage)
		{
			this.WaitOnAsyncGraphicsFence(fence, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0003331C File Offset: 0x0003151C
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStageFlags stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Attempting to call WaitOnAsyncGPUFence on a fence that is not of GraphicsFenceType.AsyncQueueSynchronization");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				this.WaitOnGPUFence_Internal(fence.m_Ptr, stage);
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00033363 File Offset: 0x00031563
		public void SetComputeFloatParam(ComputeShader computeShader, string name, float val)
		{
			this.SetComputeFloatParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00033375 File Offset: 0x00031575
		public void SetComputeIntParam(ComputeShader computeShader, string name, int val)
		{
			this.SetComputeIntParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00033387 File Offset: 0x00031587
		public void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val)
		{
			this.SetComputeVectorParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00033399 File Offset: 0x00031599
		public void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values)
		{
			this.SetComputeVectorArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000333AB File Offset: 0x000315AB
		public void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val)
		{
			this.SetComputeMatrixParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000333BD File Offset: 0x000315BD
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values)
		{
			this.SetComputeMatrixArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000333CF File Offset: 0x000315CF
		public void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000333E1 File Offset: 0x000315E1
		public void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, nameID, values);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000333EE File Offset: 0x000315EE
		public void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00033400 File Offset: 0x00031600
		public void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, nameID, values);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0003340D File Offset: 0x0003160D
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00033423 File Offset: 0x00031623
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00033434 File Offset: 0x00031634
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0003344B File Offset: 0x0003164B
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0003345D File Offset: 0x0003165D
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, element);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00033475 File Offset: 0x00031675
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, element);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00033488 File Offset: 0x00031688
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00033497 File Offset: 0x00031697
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000334AB File Offset: 0x000316AB
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetComputeGraphicsBufferHandleParam(computeShader, kernelIndex, nameID, bufferHandle);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000334BA File Offset: 0x000316BA
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetComputeGraphicsBufferHandleParam(computeShader, kernelIndex, Shader.PropertyToID(name), bufferHandle);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000334CE File Offset: 0x000316CE
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000334DD File Offset: 0x000316DD
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000334F1 File Offset: 0x000316F1
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantComputeBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00033502 File Offset: 0x00031702
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantComputeBufferParam(computeShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00033518 File Offset: 0x00031718
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantGraphicsBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00033529 File Offset: 0x00031729
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantGraphicsBufferParam(computeShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0003353F File Offset: 0x0003173F
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			this.Internal_DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00033550 File Offset: 0x00031750
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			bool flag = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchComputeIndirect(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00033590 File Offset: 0x00031790
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			bool flag = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchComputeIndirectGraphicsBuffer(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000335D0 File Offset: 0x000317D0
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure)
		{
			RayTracingAccelerationStructure.BuildSettings buildSettings = new RayTracingAccelerationStructure.BuildSettings
			{
				buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace,
				relativeOrigin = Vector3.zero
			};
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00033608 File Offset: 0x00031808
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, Vector3 relativeOrigin)
		{
			RayTracingAccelerationStructure.BuildSettings buildSettings = new RayTracingAccelerationStructure.BuildSettings
			{
				buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace,
				relativeOrigin = relativeOrigin
			};
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0003363A File Offset: 0x0003183A
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, Shader.PropertyToID(name), rayTracingAccelerationStructure);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0003364C File Offset: 0x0003184C
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00033659 File Offset: 0x00031859
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetComputeRayTracingAccelerationStructure(computeShader, kernelIndex, Shader.PropertyToID(name), rayTracingAccelerationStructure);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0003366D File Offset: 0x0003186D
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetComputeRayTracingAccelerationStructure(computeShader, kernelIndex, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0003367C File Offset: 0x0003187C
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingComputeBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0003368E File Offset: 0x0003188E
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingComputeBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0003369B File Offset: 0x0003189B
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer)
		{
			this.Internal_SetRayTracingGraphicsBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000336AD File Offset: 0x000318AD
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetRayTracingGraphicsBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000336BA File Offset: 0x000318BA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetRayTracingGraphicsBufferHandleParam(rayTracingShader, Shader.PropertyToID(name), bufferHandle);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000336CC File Offset: 0x000318CC
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetRayTracingGraphicsBufferHandleParam(rayTracingShader, nameID, bufferHandle);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000336D9 File Offset: 0x000318D9
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantComputeBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x000336EA File Offset: 0x000318EA
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantComputeBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00033700 File Offset: 0x00031900
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantGraphicsBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00033711 File Offset: 0x00031911
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantGraphicsBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00033727 File Offset: 0x00031927
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, Shader.PropertyToID(name), ref rt);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0003373A File Offset: 0x0003193A
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, nameID, ref rt);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00033748 File Offset: 0x00031948
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0003375A File Offset: 0x0003195A
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00033767 File Offset: 0x00031967
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00033779 File Offset: 0x00031979
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, nameID, values);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00033786 File Offset: 0x00031986
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00033798 File Offset: 0x00031998
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000337A5 File Offset: 0x000319A5
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000337B7 File Offset: 0x000319B7
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, nameID, values);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000337C4 File Offset: 0x000319C4
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000337D6 File Offset: 0x000319D6
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000337E3 File Offset: 0x000319E3
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000337F5 File Offset: 0x000319F5
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00033802 File Offset: 0x00031A02
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00033814 File Offset: 0x00031A14
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00033821 File Offset: 0x00031A21
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00033833 File Offset: 0x00031A33
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00033840 File Offset: 0x00031A40
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera = null)
		{
			this.Internal_DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00033854 File Offset: 0x00031A54
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, [DefaultValue("0")] int submeshIndex, [DefaultValue("-1")] int shaderPass, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag2)
			{
				submeshIndex = Mathf.Clamp(submeshIndex, 0, mesh.subMeshCount - 1);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000338EB File Offset: 0x00031AEB
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, null);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000338FD File Offset: 0x00031AFD
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, -1);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0003390D File Offset: 0x00031B0D
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material)
		{
			this.DrawMesh(mesh, matrix, material, 0);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0003391C File Offset: 0x00031B1C
		[ExcludeFromDocs]
		public void DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = matrices.Length != meshes.Length || matrices.Length != subsetIndices.Length;
			if (flag)
			{
				throw new InvalidOperationException("matrices, meshes, subsetIndices must be of same length and must be valid");
			}
			bool flag2 = count < 1;
			if (flag2)
			{
				throw new InvalidOperationException("count must be atleast 1");
			}
			this.Internal_DrawMultipleMeshes(matrices, meshes, subsetIndices, count, material, shaderPass, properties);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00033978 File Offset: 0x00031B78
		public void DrawRenderer(Renderer renderer, Material material, [DefaultValue("0")] int submeshIndex, [DefaultValue("-1")] int shaderPass)
		{
			bool flag = renderer == null;
			if (flag)
			{
				throw new ArgumentNullException("renderer");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0;
			if (flag2)
			{
				submeshIndex = Mathf.Max(submeshIndex, 0);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000339F0 File Offset: 0x00031BF0
		[ExcludeFromDocs]
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.DrawRenderer(renderer, material, submeshIndex, -1);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000339FE File Offset: 0x00031BFE
		[ExcludeFromDocs]
		public void DrawRenderer(Renderer renderer, Material material)
		{
			this.DrawRenderer(renderer, material, 0);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00033A0B File Offset: 0x00031C0B
		public void DrawRendererList(RendererList rendererList)
		{
			this.Internal_DrawRendererList(rendererList);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00033A18 File Offset: 0x00031C18
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = material == null;
			if (flag)
			{
				throw new ArgumentNullException("material");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00033A57 File Offset: 0x00031C57
		[ExcludeFromDocs]
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, null);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00033A6B File Offset: 0x00031C6B
		[ExcludeFromDocs]
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, 1);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00033A80 File Offset: 0x00031C80
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawProceduralIndexed(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00033ACC File Offset: 0x00031CCC
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, null);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00033AED File Offset: 0x00031CED
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, 1);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00033B04 File Offset: 0x00031D04
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00033B6E File Offset: 0x00031D6E
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00033B82 File Offset: 0x00031D82
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00033B94 File Offset: 0x00031D94
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			bool flag4 = bufferWithArgs == null;
			if (flag4)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00033C0C File Offset: 0x00031E0C
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00033C2D File Offset: 0x00031E2D
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00033C44 File Offset: 0x00031E44
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirectGraphicsBuffer(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00033CAE File Offset: 0x00031EAE
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00033CC2 File Offset: 0x00031EC2
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00033CD4 File Offset: 0x00031ED4
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			bool flag4 = bufferWithArgs == null;
			if (flag4)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirectGraphicsBuffer(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00033D4C File Offset: 0x00031F4C
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00033D6D File Offset: 0x00031F6D
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00033D84 File Offset: 0x00031F84
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstanced is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag8 = count > 0;
			if (flag8)
			{
				this.Internal_DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
			}
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00033E9A File Offset: 0x0003209A
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, null);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00033EAE File Offset: 0x000320AE
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, matrices.Length);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00033EC4 File Offset: 0x000320C4
		public void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstancedProcedural is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag6 = count > 0;
			if (flag6)
			{
				this.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00033F7C File Offset: 0x0003217C
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00034035 File Offset: 0x00032235
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00034049 File Offset: 0x00032249
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0003405C File Offset: 0x0003225C
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00034115 File Offset: 0x00032315
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00034129 File Offset: 0x00032329
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0003413C File Offset: 0x0003233C
		public void DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.Internal_DrawOcclusionMesh(normalizedCamViewport);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00034147 File Offset: 0x00032347
		public void SetRandomWriteTarget(int index, RenderTargetIdentifier rt)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_Texture(index, ref rt);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0003415D File Offset: 0x0003235D
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer, bool preserveCounterValue)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_GraphicsBuffer(index, buffer, preserveCounterValue);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00034173 File Offset: 0x00032373
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer)
		{
			this.SetRandomWriteTarget(index, buffer, false);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00034180 File Offset: 0x00032380
		public void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0003418D File Offset: 0x0003238D
		public void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0003419A File Offset: 0x0003239A
		public void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000341A7 File Offset: 0x000323A7
		public void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000341B4 File Offset: 0x000323B4
		public void Blit(Texture source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000341FC File Offset: 0x000323FC
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00034244 File Offset: 0x00032444
		public void SetGlobalFloat(string name, float value)
		{
			this.SetGlobalFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00034255 File Offset: 0x00032455
		public void SetGlobalInt(string name, int value)
		{
			this.SetGlobalInt(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00034266 File Offset: 0x00032466
		public void SetGlobalInteger(string name, int value)
		{
			this.SetGlobalInteger(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00034277 File Offset: 0x00032477
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.SetGlobalVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00034288 File Offset: 0x00032488
		public void SetGlobalColor(string name, Color value)
		{
			this.SetGlobalColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00034299 File Offset: 0x00032499
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.SetGlobalMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000342AA File Offset: 0x000324AA
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000342BC File Offset: 0x000324BC
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalFloatArrayListImpl(nameID, values);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000342FE File Offset: 0x000324FE
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0003430F File Offset: 0x0003250F
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00034320 File Offset: 0x00032520
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalVectorArrayListImpl(nameID, values);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00034362 File Offset: 0x00032562
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00034373 File Offset: 0x00032573
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00034384 File Offset: 0x00032584
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalMatrixArrayListImpl(nameID, values);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000343C6 File Offset: 0x000325C6
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000343D7 File Offset: 0x000325D7
		public void SetGlobalTexture(string name, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, RenderTextureSubElement.Default);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000343E9 File Offset: 0x000325E9
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, RenderTextureSubElement.Default);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000343F7 File Offset: 0x000325F7
		public void SetGlobalTexture(string name, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00034409 File Offset: 0x00032609
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, element);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00034417 File Offset: 0x00032617
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00034428 File Offset: 0x00032628
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(nameID, value);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00034434 File Offset: 0x00032634
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00034445 File Offset: 0x00032645
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(nameID, value);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00034451 File Offset: 0x00032651
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00034460 File Offset: 0x00032660
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00034474 File Offset: 0x00032674
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00034483 File Offset: 0x00032683
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00034497 File Offset: 0x00032697
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetShadowSamplingMode_Impl(ref shadowmap, mode);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000344AD File Offset: 0x000326AD
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.Internal_SetSinglePassStereo(mode);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000344B8 File Offset: 0x000326B8
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.IssuePluginEventInternal(callback, eventID);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000344EC File Offset: 0x000326EC
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginEventAndDataInternal(callback, eventID, data);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00034527 File Offset: 0x00032727
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomBlitInternal(callback, command, ref source, ref dest, commandParam, commandFlags);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00034544 File Offset: 0x00032744
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, true);
		}

		// Token: 0x0600180A RID: 6154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_1_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x0600180B RID: 6155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_2_Injected(IntPtr _unity_self, IntPtr src, int size, int offset, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x0600180C RID: 6156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_3_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x0600180D RID: 6157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_4_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x0600180E RID: 6158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_5_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x0600180F RID: 6159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_6_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x06001810 RID: 6160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_7_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x06001811 RID: 6161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_8_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x06001812 RID: 6162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_9_Injected(IntPtr _unity_self, IntPtr src, int size, int offset, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x06001813 RID: 6163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInvertCulling_Injected(IntPtr _unity_self, bool invertCulling);

		// Token: 0x06001814 RID: 6164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSinglePassStereo_Injected(IntPtr _unity_self, SinglePassStereoMode mode);

		// Token: 0x06001815 RID: 6165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateGPUFence_Internal_Injected(IntPtr _unity_self, GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x06001816 RID: 6166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitOnGPUFence_Internal_Injected(IntPtr _unity_self, IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x06001817 RID: 6167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseBuffer_Injected(IntPtr _unity_self);

		// Token: 0x06001818 RID: 6168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeFloatParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, float val);

		// Token: 0x06001819 RID: 6169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeIntParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, int val);

		// Token: 0x0600181A RID: 6170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeVectorParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, [In] ref Vector4 val);

		// Token: 0x0600181B RID: 6171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeVectorArrayParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600181C RID: 6172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeMatrixParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x0600181D RID: 6173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeMatrixArrayParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600181E RID: 6174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeFloats_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600181F RID: 6175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeInts_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001820 RID: 6176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeTextureParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, ref RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element);

		// Token: 0x06001821 RID: 6177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x06001822 RID: 6178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeGraphicsBufferHandleParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, [In] ref GraphicsBufferHandle bufferHandle);

		// Token: 0x06001823 RID: 6179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x06001824 RID: 6180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeConstantComputeBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06001825 RID: 6181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeConstantGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06001826 RID: 6182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchCompute_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x06001827 RID: 6183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchComputeIndirect_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, IntPtr indirectBuffer, uint argsOffset);

		// Token: 0x06001828 RID: 6184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchComputeIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, IntPtr indirectBuffer, uint argsOffset);

		// Token: 0x06001829 RID: 6185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingComputeBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer);

		// Token: 0x0600182A RID: 6186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer);

		// Token: 0x0600182B RID: 6187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingGraphicsBufferHandleParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref GraphicsBufferHandle bufferHandle);

		// Token: 0x0600182C RID: 6188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingConstantComputeBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x0600182D RID: 6189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingConstantGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x0600182E RID: 6190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingTextureParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref RenderTargetIdentifier rt);

		// Token: 0x0600182F RID: 6191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingFloatParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, float val);

		// Token: 0x06001830 RID: 6192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingIntParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, int val);

		// Token: 0x06001831 RID: 6193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingVectorParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref Vector4 val);

		// Token: 0x06001832 RID: 6194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingVectorArrayParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001833 RID: 6195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingMatrixParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x06001834 RID: 6196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingMatrixArrayParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001835 RID: 6197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingFloats_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001836 RID: 6198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingInts_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001837 RID: 6199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BuildRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr accelerationStructure, [In] ref RayTracingAccelerationStructure.BuildSettings buildSettings);

		// Token: 0x06001838 RID: 6200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr accelerationStructure);

		// Token: 0x06001839 RID: 6201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr accelerationStructure);

		// Token: 0x0600183A RID: 6202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchRays_Injected(IntPtr _unity_self, IntPtr rayTracingShader, ref ManagedSpanWrapper rayGenShaderName, uint width, uint height, uint depth, IntPtr camera);

		// Token: 0x0600183B RID: 6203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueCC_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600183C RID: 6204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueGC_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600183D RID: 6205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueCG_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600183E RID: 6206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueGG_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600183F RID: 6207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_name_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06001840 RID: 6208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_name_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06001841 RID: 6209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sizeInBytes_Injected(IntPtr _unity_self);

		// Token: 0x06001842 RID: 6210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06001843 RID: 6211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMesh_Injected(IntPtr _unity_self, IntPtr mesh, [In] ref Matrix4x4 matrix, IntPtr material, int submeshIndex, int shaderPass, IntPtr properties);

		// Token: 0x06001844 RID: 6212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMultipleMeshes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper matrices, Mesh[] meshes, ref ManagedSpanWrapper subsetIndices, int count, IntPtr material, int shaderPass, IntPtr properties);

		// Token: 0x06001845 RID: 6213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawRenderer_Injected(IntPtr _unity_self, IntPtr renderer, IntPtr material, int submeshIndex, int shaderPass);

		// Token: 0x06001846 RID: 6214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawRendererList_Injected(IntPtr _unity_self, [In] ref RendererList rendererList);

		// Token: 0x06001847 RID: 6215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProcedural_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, IntPtr properties);

		// Token: 0x06001848 RID: 6216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexed_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, IntPtr properties);

		// Token: 0x06001849 RID: 6217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirect_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600184A RID: 6218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirect_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600184B RID: 6219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectGraphicsBuffer_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600184C RID: 6220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600184D RID: 6221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstanced_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, ref ManagedSpanWrapper matrices, int count, IntPtr properties);

		// Token: 0x0600184E RID: 6222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedProcedural_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, int count, IntPtr properties);

		// Token: 0x0600184F RID: 6223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirect_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x06001850 RID: 6224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x06001851 RID: 6225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawOcclusionMesh_Injected(IntPtr _unity_self, [In] ref RectInt normalizedCamViewport);

		// Token: 0x06001852 RID: 6226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRandomWriteTarget_Texture_Injected(IntPtr _unity_self, int index, ref RenderTargetIdentifier rt);

		// Token: 0x06001853 RID: 6227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRandomWriteTarget_GraphicsBuffer_Injected(IntPtr _unity_self, int index, IntPtr uav, bool preserveCounterValue);

		// Token: 0x06001854 RID: 6228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRandomWriteTargets_Injected(IntPtr _unity_self);

		// Token: 0x06001855 RID: 6229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewport_Injected(IntPtr _unity_self, [In] ref Rect pixelRect);

		// Token: 0x06001856 RID: 6230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableScissorRect_Injected(IntPtr _unity_self, [In] ref Rect scissor);

		// Token: 0x06001857 RID: 6231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableScissorRect_Injected(IntPtr _unity_self);

		// Token: 0x06001858 RID: 6232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit_Texture_Injected(IntPtr _unity_self, IntPtr source, ref RenderTargetIdentifier dest, IntPtr mat, int pass, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06001859 RID: 6233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit_Identifier_Injected(IntPtr _unity_self, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, IntPtr mat, int pass, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x0600185A RID: 6234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTemporaryRTWithDescriptor_Injected(IntPtr _unity_self, int nameID, [In] ref RenderTextureDescriptor desc, FilterMode filter);

		// Token: 0x0600185B RID: 6235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseTemporaryRT_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600185C RID: 6236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloat_Injected(IntPtr _unity_self, int nameID, float value);

		// Token: 0x0600185D RID: 6237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalInt_Injected(IntPtr _unity_self, int nameID, int value);

		// Token: 0x0600185E RID: 6238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalInteger_Injected(IntPtr _unity_self, int nameID, int value);

		// Token: 0x0600185F RID: 6239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVector_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 value);

		// Token: 0x06001860 RID: 6240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalColor_Injected(IntPtr _unity_self, int nameID, [In] ref Color value);

		// Token: 0x06001861 RID: 6241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrix_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 value);

		// Token: 0x06001862 RID: 6242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableShaderKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06001863 RID: 6243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword);

		// Token: 0x06001864 RID: 6244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword);

		// Token: 0x06001865 RID: 6245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword);

		// Token: 0x06001866 RID: 6246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableShaderKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06001867 RID: 6247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword);

		// Token: 0x06001868 RID: 6248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword);

		// Token: 0x06001869 RID: 6249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword);

		// Token: 0x0600186A RID: 6250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword, bool value);

		// Token: 0x0600186B RID: 6251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x0600186C RID: 6252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x0600186D RID: 6253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewProjectionMatrices_Injected(IntPtr _unity_self, [In] ref Matrix4x4 view, [In] ref Matrix4x4 proj);

		// Token: 0x0600186E RID: 6254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalDepthBias_Injected(IntPtr _unity_self, float bias, float slopeBias);

		// Token: 0x0600186F RID: 6255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetExecutionFlags_Injected(IntPtr _unity_self, CommandBufferExecutionFlags flags);

		// Token: 0x06001870 RID: 6256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateAgainstExecutionFlags_Injected(IntPtr _unity_self, CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags);

		// Token: 0x06001871 RID: 6257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x06001872 RID: 6258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x06001873 RID: 6259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x06001874 RID: 6260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001875 RID: 6261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001876 RID: 6262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06001877 RID: 6263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLateLatchProjectionMatrices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper projectionMat);

		// Token: 0x06001878 RID: 6264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkLateLatchMatrixShaderPropertyID_Injected(IntPtr _unity_self, CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID);

		// Token: 0x06001879 RID: 6265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnmarkLateLatchMatrix_Injected(IntPtr _unity_self, CameraLateLatchMatrixType matrixPropertyType);

		// Token: 0x0600187A RID: 6266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTexture_Impl_Injected(IntPtr _unity_self, int nameID, ref RenderTargetIdentifier rt, RenderTextureSubElement element);

		// Token: 0x0600187B RID: 6267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBufferInternal_Injected(IntPtr _unity_self, int nameID, IntPtr value);

		// Token: 0x0600187C RID: 6268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalGraphicsBufferInternal_Injected(IntPtr _unity_self, int nameID, IntPtr value);

		// Token: 0x0600187D RID: 6269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShadowSamplingMode_Impl_Injected(IntPtr _unity_self, ref RenderTargetIdentifier shadowmap, ShadowSamplingMode mode);

		// Token: 0x0600187E RID: 6270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventInternal_Injected(IntPtr _unity_self, IntPtr callback, int eventID);

		// Token: 0x0600187F RID: 6271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSample_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06001880 RID: 6272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSample_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06001881 RID: 6273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSample_CustomSampler_Injected(IntPtr _unity_self, IntPtr sampler);

		// Token: 0x06001882 RID: 6274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSample_CustomSampler_Injected(IntPtr _unity_self, IntPtr sampler);

		// Token: 0x06001883 RID: 6275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventAndDataInternal_Injected(IntPtr _unity_self, IntPtr callback, int eventID, IntPtr data);

		// Token: 0x06001884 RID: 6276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginCustomBlitInternal_Injected(IntPtr _unity_self, IntPtr callback, uint command, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

		// Token: 0x06001885 RID: 6277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginCustomTextureUpdateInternal_Injected(IntPtr _unity_self, IntPtr callback, IntPtr targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2);

		// Token: 0x06001886 RID: 6278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantBufferInternal_Injected(IntPtr _unity_self, IntPtr buffer, int nameID, int offset, int size);

		// Token: 0x06001887 RID: 6279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantGraphicsBufferInternal_Injected(IntPtr _unity_self, IntPtr buffer, int nameID, int offset, int size);

		// Token: 0x06001888 RID: 6280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementUpdateCount_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier dest);

		// Token: 0x06001889 RID: 6281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInstanceMultiplier_Injected(IntPtr _unity_self, uint multiplier);

		// Token: 0x0600188A RID: 6282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFoveatedRenderingMode_Injected(IntPtr _unity_self, FoveatedRenderingMode foveatedRenderingMode);

		// Token: 0x0600188B RID: 6283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWireframe_Injected(IntPtr _unity_self, bool enable);

		// Token: 0x0600188C RID: 6284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConfigureFoveatedRendering_Injected(IntPtr _unity_self, IntPtr platformData);

		// Token: 0x0600188D RID: 6285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRenderTargetSingle_Internal_Injected(IntPtr _unity_self, RTClearFlags clearFlags, [In] ref Color color, float depth, uint stencil);

		// Token: 0x0600188E RID: 6286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRenderTargetMulti_Internal_Injected(IntPtr _unity_self, RTClearFlags clearFlags, ref ManagedSpanWrapper colors, float depth, uint stencil);

		// Token: 0x0600188F RID: 6287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetSingle_Internal_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06001890 RID: 6288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetColorDepth_Internal_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier color, [In] ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags);

		// Token: 0x06001891 RID: 6289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetMulti_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, [In] ref RenderTargetIdentifier depth, ref ManagedSpanWrapper colorLoadActions, ref ManagedSpanWrapper colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags);

		// Token: 0x06001892 RID: 6290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetColorDepthSubtarget_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier color, [In] ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06001893 RID: 6291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetMultiSubtarget_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, [In] ref RenderTargetIdentifier depth, ref ManagedSpanWrapper colorLoadActions, ref ManagedSpanWrapper colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06001894 RID: 6292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferNativeData_Injected(IntPtr _unity_self, IntPtr buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001895 RID: 6293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferData_Injected(IntPtr _unity_self, IntPtr buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001896 RID: 6294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferCounterValue_Injected(IntPtr _unity_self, IntPtr buffer, uint counterValue);

		// Token: 0x06001897 RID: 6295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferNativeData_Injected(IntPtr _unity_self, IntPtr buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001898 RID: 6296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferData_Injected(IntPtr _unity_self, IntPtr buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001899 RID: 6297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferCounterValue_Injected(IntPtr _unity_self, IntPtr buffer, uint counterValue);

		// Token: 0x0600189A RID: 6298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal_Injected(IntPtr _unity_self, int width, int height, int volumeDepth, int samples, ref ManagedSpanWrapper attachments, int depthAttachmentIndex, ref ManagedSpanWrapper subPasses, ref ManagedSpanWrapper debugNameUtf8);

		// Token: 0x0600189B RID: 6299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NextSubPass_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0600189C RID: 6300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0600189D RID: 6301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x0600189E RID: 6302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeOnRenderObjectCallbacks_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000A31 RID: 2609
		public static bool ThrowOnSetRenderTarget;

		// Token: 0x04000A32 RID: 2610
		internal IntPtr m_Ptr;

		// Token: 0x02000366 RID: 870
		internal static class BindingsMarshaller
		{
			// Token: 0x0600189F RID: 6303 RVA: 0x0003455B File Offset: 0x0003275B
			public static IntPtr ConvertToNative(CommandBuffer commandBuffer)
			{
				return commandBuffer.m_Ptr;
			}
		}
	}
}
