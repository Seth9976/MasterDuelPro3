using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D4 RID: 468
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingAccelerationStructure.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	public sealed class ComputeShader : Object
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x000265AC File Offset: 0x000247AC
		[RequiredByNativeCode]
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public unsafe int FindKernel(string name)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
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
				num = ComputeShader.FindKernel_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00026614 File Offset: 0x00024814
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		public void SetInt(int nameID, int val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetInt_Injected(intPtr, nameID, val);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00026638 File Offset: 0x00024838
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		private unsafe void SetIntArray(int nameID, int[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ComputeShader.SetIntArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00026684 File Offset: 0x00024884
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		private void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull] ComputeBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.Internal_SetBuffer_Injected(intPtr, kernelIndex, nameID, intPtr2);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000266CC File Offset: 0x000248CC
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		private void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull] GraphicsBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.Internal_SetGraphicsBuffer_Injected(intPtr, kernelIndex, nameID, intPtr2);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00026714 File Offset: 0x00024914
		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00026721 File Offset: 0x00024921
		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00026730 File Offset: 0x00024930
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		private void SetConstantComputeBuffer(int nameID, [NotNull] ComputeBuffer buffer, int offset, int size)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.SetConstantComputeBuffer_Injected(intPtr, nameID, intPtr2, offset, size);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0002677C File Offset: 0x0002497C
		[NativeName("DispatchComputeShader")]
		public void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.Dispatch_Injected(intPtr, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000267A4 File Offset: 0x000249A4
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LocalKeywordSpace localKeywordSpace;
				ComputeShader.get_keywordSpace_Injected(intPtr, out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000267CC File Offset: 0x000249CC
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		public unsafe void EnableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
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
				ComputeShader.EnableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00026830 File Offset: 0x00024A30
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		public unsafe void DisableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
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
				ComputeShader.DisableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00026894 File Offset: 0x00024A94
		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		private void SetShaderKeywords(string[] names)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetShaderKeywords_Injected(intPtr, names);
		}

		// Token: 0x170002B3 RID: 691
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x000268B7 File Offset: 0x00024AB7
		public string[] shaderKeywords
		{
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000268C2 File Offset: 0x00024AC2
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000268CE File Offset: 0x00024ACE
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x060011F4 RID: 4596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindKernel_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060011F5 RID: 4597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInt_Injected(IntPtr _unity_self, int nameID, int val);

		// Token: 0x060011F6 RID: 4598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x060011F7 RID: 4599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetBuffer_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x060011F8 RID: 4600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetGraphicsBuffer_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x060011F9 RID: 4601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantComputeBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x060011FA RID: 4602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispatch_Injected(IntPtr _unity_self, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x060011FB RID: 4603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_keywordSpace_Injected(IntPtr _unity_self, out LocalKeywordSpace ret);

		// Token: 0x060011FC RID: 4604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060011FD RID: 4605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060011FE RID: 4606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderKeywords_Injected(IntPtr _unity_self, string[] names);
	}
}
