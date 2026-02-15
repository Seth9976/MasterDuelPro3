using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000321 RID: 801
	[StaticAccessor("AsyncGPUReadbackManager::GetInstance()", StaticAccessorType.Dot)]
	public static class AsyncGPUReadback
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x0002E974 File Offset: 0x0002CB74
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest request = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, null);
			request.SetScriptingCallback(callback);
			return request;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0002E99C File Offset: 0x0002CB9C
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest request = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, null);
			request.SetScriptingCallback(callback);
			return request;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_1([NotNull] GraphicsBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_1_Injected(intPtr, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0002EA00 File Offset: 0x0002CC00
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_2([NotNull] GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_2_Injected(intPtr, size, offset, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001636 RID: 5686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_1_Injected(IntPtr buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001637 RID: 5687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_2_Injected(IntPtr src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);
	}
}
