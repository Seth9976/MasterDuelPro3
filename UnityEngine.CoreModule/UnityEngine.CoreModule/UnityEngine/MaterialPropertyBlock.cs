using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000EB RID: 235
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Math/SphericalHarmonicsL2.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ShaderPropertySheet.h")]
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0000CE98 File Offset: 0x0000B098
		[ThreadSafe]
		[NativeName("SetIntFromScript")]
		private void SetIntImpl(int name, int value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetIntImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		[NativeName("SetFloatFromScript")]
		[ThreadSafe]
		private void SetFloatImpl(int name, float value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetFloatImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		[NativeName("SetVectorFromScript")]
		[ThreadSafe]
		private void SetVectorImpl(int name, Vector4 value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetVectorImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000CF08 File Offset: 0x0000B108
		[ThreadSafe]
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetMatrixImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000CF30 File Offset: 0x0000B130
		[ThreadSafe]
		[NativeName("SetTextureFromScript")]
		private void SetTextureImpl(int name, [NotNull] Texture value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(value);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			MaterialPropertyBlock.SetTextureImpl_Injected(intPtr, name, intPtr2);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000CF78 File Offset: 0x0000B178
		[NativeName("SetRenderTextureFromScript")]
		[ThreadSafe]
		private void SetRenderTextureImpl(int name, [NotNull] RenderTexture value, RenderTextureSubElement element)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(value);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			MaterialPropertyBlock.SetRenderTextureImpl_Injected(intPtr, name, intPtr2, element);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		[ThreadSafe]
		[NativeName("SetBufferFromScript")]
		private void SetBufferImpl(int name, ComputeBuffer value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		[NativeName("SetConstantBufferFromScript")]
		[ThreadSafe]
		private void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetConstantBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000D02C File Offset: 0x0000B22C
		[NativeName("SetFloatArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetFloatArrayImpl(int name, float[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetFloatArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000D078 File Offset: 0x0000B278
		[NativeName("SetVectorArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetVectorArrayImpl(int name, Vector4[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetVectorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		[NativeName("SetMatrixArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetMatrixArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000635 RID: 1589
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateImpl();

		// Token: 0x06000636 RID: 1590
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Destroy", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImpl(IntPtr mpb);

		// Token: 0x06000637 RID: 1591 RVA: 0x0000D110 File Offset: 0x0000B310
		[ThreadSafe]
		private void Clear(bool keepMemory)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.Clear_Injected(intPtr, keepMemory);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000D133 File Offset: 0x0000B333
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000D140 File Offset: 0x0000B340
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000D194 File Offset: 0x0000B394
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000D23C File Offset: 0x0000B43C
		public MaterialPropertyBlock()
		{
			this.m_Ptr = MaterialPropertyBlock.CreateImpl();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000D254 File Offset: 0x0000B454
		~MaterialPropertyBlock()
		{
			this.Dispose();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000D284 File Offset: 0x0000B484
		private void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MaterialPropertyBlock.DestroyImpl(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000D2C6 File Offset: 0x0000B4C6
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000D2E5 File Offset: 0x0000B4E5
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000D2F6 File Offset: 0x0000B4F6
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000D302 File Offset: 0x0000B502
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000D30E File Offset: 0x0000B50E
		public void SetVector(string name, Vector4 value)
		{
			this.SetVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000D31F File Offset: 0x0000B51F
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetVectorImpl(nameID, value);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000D32B File Offset: 0x0000B52B
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000D33C File Offset: 0x0000B53C
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000D348 File Offset: 0x0000B548
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000D359 File Offset: 0x0000B559
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000D36A File Offset: 0x0000B56A
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000D376 File Offset: 0x0000B576
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000D383 File Offset: 0x0000B583
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000D392 File Offset: 0x0000B592
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000D3BA File Offset: 0x0000B5BA
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000D3C9 File Offset: 0x0000B5C9
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000651 RID: 1617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntImpl_Injected(IntPtr _unity_self, int name, int value);

		// Token: 0x06000652 RID: 1618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatImpl_Injected(IntPtr _unity_self, int name, float value);

		// Token: 0x06000653 RID: 1619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector4 value);

		// Token: 0x06000654 RID: 1620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixImpl_Injected(IntPtr _unity_self, int name, [In] ref Matrix4x4 value);

		// Token: 0x06000655 RID: 1621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000656 RID: 1622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x06000657 RID: 1623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000658 RID: 1624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06000659 RID: 1625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x0600065A RID: 1626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x0600065B RID: 1627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x0600065C RID: 1628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self, bool keepMemory);

		// Token: 0x040002B6 RID: 694
		internal IntPtr m_Ptr;

		// Token: 0x020000EC RID: 236
		internal static class BindingsMarshaller
		{
			// Token: 0x0600065D RID: 1629 RVA: 0x0000D3DD File Offset: 0x0000B5DD
			public static IntPtr ConvertToNative(MaterialPropertyBlock materialPropertyBlock)
			{
				return materialPropertyBlock.m_Ptr;
			}
		}
	}
}
