using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200031F RID: 799
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[UsedByNativeCode]
	public struct AsyncGPUReadbackRequest
	{
		// Token: 0x06001624 RID: 5668
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WaitForCompletion();

		// Token: 0x06001625 RID: 5669 RVA: 0x0002E814 File Offset: 0x0002CA14
		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int stride = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / stride, Allocator.None);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0002E8BC File Offset: 0x0002CABC
		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		// Token: 0x0600162A RID: 5674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsDone();

		// Token: 0x0600162B RID: 5675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasError();

		// Token: 0x0600162C RID: 5676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLayerCount();

		// Token: 0x0600162D RID: 5677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLayerDataSize();

		// Token: 0x0600162E RID: 5678
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback);

		// Token: 0x0600162F RID: 5679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetDataRaw(int layer);

		// Token: 0x06001630 RID: 5680 RVA: 0x0002E904 File Offset: 0x0002CB04
		[RequiredByNativeCode]
		private static void InvokeCallback(Action<AsyncGPUReadbackRequest> callback, AsyncGPUReadbackRequest obj)
		{
			callback(obj);
		}

		// Token: 0x04000855 RID: 2133
		internal IntPtr m_Ptr;

		// Token: 0x04000856 RID: 2134
		internal int m_Version;
	}
}
