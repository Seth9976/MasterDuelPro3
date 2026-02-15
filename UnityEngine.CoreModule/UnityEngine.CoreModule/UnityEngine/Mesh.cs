using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000127 RID: 295
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[ExcludeFromPreset]
	public sealed class Mesh : Object
	{
		// Token: 0x06000A25 RID: 2597
		[FreeFunction("MeshScripting::CreateMesh")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x06000A26 RID: 2598 RVA: 0x00012E2A File Offset: 0x0001102A
		[RequiredByNativeCode]
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x170001AD RID: 429
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00012E3C File Offset: 0x0001103C
		public IndexFormat indexFormat
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_indexFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00012E60 File Offset: 0x00011060
		[FreeFunction(Name = "MeshScripting::SetIndexBufferParams", HasExplicitThis = true, ThrowsException = true)]
		public void SetIndexBufferParams(int indexCount, IndexFormat format)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndexBufferParams_Injected(intPtr, indexCount, format);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00012E84 File Offset: 0x00011084
		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetIndexBufferData(IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetIndexBufferData_Injected(intPtr, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00012EB0 File Offset: 0x000110B0
		[FreeFunction(Name = "MeshScripting::SetVertexBufferParamsFromArray", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetVertexBufferParamsFromArray(int vertexCount, params VertexAttributeDescriptor[] attributes)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(attributes);
			fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Mesh.SetVertexBufferParamsFromArray_Injected(intPtr, vertexCount, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00012EFC File Offset: 0x000110FC
		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferData", HasExplicitThis = true)]
		private void InternalSetVertexBufferData(int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetVertexBufferData_Injected(intPtr, stream, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00012F2C File Offset: 0x0001112C
		[FreeFunction(Name = "MeshScripting::GetIndexCount", HasExplicitThis = true)]
		private uint GetIndexCountImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetIndexCountImpl_Injected(intPtr, submesh);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00012F50 File Offset: 0x00011150
		[FreeFunction(Name = "MeshScripting::GetTriangles", HasExplicitThis = true)]
		private int[] GetTrianglesImpl(int submesh, bool applyBaseVertex)
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Mesh.GetTrianglesImpl_Injected(intPtr, submesh, applyBaseVertex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00012F98 File Offset: 0x00011198
		[FreeFunction(Name = "MeshScripting::GetIndices", HasExplicitThis = true)]
		private int[] GetIndicesImpl(int submesh, bool applyBaseVertex)
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Mesh.GetIndicesImpl_Injected(intPtr, submesh, applyBaseVertex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00012FE0 File Offset: 0x000111E0
		[FreeFunction(Name = "SetMeshIndicesFromScript", HasExplicitThis = true, ThrowsException = true)]
		private void SetIndicesImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndicesImpl_Injected(intPtr, submesh, topology, indicesFormat, indices, arrayStart, arraySize, calculateBounds, baseVertex);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00013010 File Offset: 0x00011210
		[FreeFunction(Name = "SetMeshIndicesFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private void SetIndicesNativeArrayImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndicesNativeArrayImpl_Injected(intPtr, submesh, topology, indicesFormat, indices, arrayStart, arraySize, calculateBounds, baseVertex);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00013040 File Offset: 0x00011240
		[FreeFunction(Name = "MeshScripting::PrintErrorCantAccessChannel", HasExplicitThis = true)]
		private void PrintErrorCantAccessChannel(VertexAttribute ch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.PrintErrorCantAccessChannel_Injected(intPtr, ch);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00013064 File Offset: 0x00011264
		[FreeFunction(Name = "MeshScripting::HasChannel", HasExplicitThis = true)]
		public bool HasVertexAttribute(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.HasVertexAttribute_Injected(intPtr, attr);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00013088 File Offset: 0x00011288
		[FreeFunction(Name = "SetMeshComponentFromArrayFromScript", HasExplicitThis = true)]
		private void SetArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetArrayForChannelImpl_Injected(intPtr, channel, format, dim, values, arraySize, valuesStart, valuesCount, flags);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000130B8 File Offset: 0x000112B8
		[FreeFunction(Name = "AllocExtractMeshComponentFromScript", HasExplicitThis = true)]
		private Array GetAllocArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetAllocArrayFromChannelImpl_Injected(intPtr, channel, format, dim);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000130E0 File Offset: 0x000112E0
		[FreeFunction(Name = "ExtractMeshComponentFromScript", HasExplicitThis = true)]
		private void GetArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.GetArrayFromChannelImpl_Injected(intPtr, channel, format, dim, values);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00013108 File Offset: 0x00011308
		internal bool canAccess
		{
			[NativeMethod("CanAccessFromScript")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_canAccess_Injected(intPtr);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001312C File Offset: 0x0001132C
		public int vertexCount
		{
			[NativeMethod("GetVertexCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_vertexCount_Injected(intPtr);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00013150 File Offset: 0x00011350
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00013174 File Offset: 0x00011374
		public int subMeshCount
		{
			[NativeMethod(Name = "GetSubMeshCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_subMeshCount_Injected(intPtr);
			}
			[FreeFunction(Name = "MeshScripting::SetSubMeshCount", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_subMeshCount_Injected(intPtr, value);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00013198 File Offset: 0x00011398
		[FreeFunction("MeshScripting::SetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetSubMesh_Injected(intPtr, index, ref desc, flags);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000131C0 File Offset: 0x000113C0
		[FreeFunction("MeshScripting::GetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public SubMeshDescriptor GetSubMesh(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SubMeshDescriptor subMeshDescriptor;
			Mesh.GetSubMesh_Injected(intPtr, index, out subMeshDescriptor);
			return subMeshDescriptor;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000131E8 File Offset: 0x000113E8
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00013210 File Offset: 0x00011410
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Mesh.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_bounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00013234 File Offset: 0x00011434
		[NativeMethod("Clear")]
		private void ClearImpl(bool keepVertexLayout)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.ClearImpl_Injected(intPtr, keepVertexLayout);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00013258 File Offset: 0x00011458
		[NativeMethod("RecalculateBounds")]
		private void RecalculateBoundsImpl(MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateBoundsImpl_Injected(intPtr, flags);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001327C File Offset: 0x0001147C
		[NativeMethod("RecalculateNormals")]
		private void RecalculateNormalsImpl(MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateNormalsImpl_Injected(intPtr, flags);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000132A0 File Offset: 0x000114A0
		[NativeMethod("MarkDynamic")]
		private void MarkDynamicImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.MarkDynamicImpl_Injected(intPtr);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000132C4 File Offset: 0x000114C4
		[NativeMethod("UploadMeshData")]
		private void UploadMeshDataImpl(bool markNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.UploadMeshDataImpl_Injected(intPtr, markNoLongerReadable);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000132E8 File Offset: 0x000114E8
		internal static VertexAttribute GetUVChannel(int uvIndex)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentException("GetUVChannel called for bad uvIndex", "uvIndex");
			}
			return VertexAttribute.TexCoord0 + uvIndex;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001331C File Offset: 0x0001151C
		internal static int DefaultDimensionForChannel(VertexAttribute channel)
		{
			bool flag = channel == VertexAttribute.Position || channel == VertexAttribute.Normal;
			int num;
			if (flag)
			{
				num = 3;
			}
			else
			{
				bool flag2 = channel >= VertexAttribute.TexCoord0 && channel <= VertexAttribute.TexCoord7;
				if (flag2)
				{
					num = 2;
				}
				else
				{
					bool flag3 = channel == VertexAttribute.Tangent || channel == VertexAttribute.Color;
					if (!flag3)
					{
						throw new ArgumentException("DefaultDimensionForChannel called for bad channel", "channel");
					}
					num = 4;
				}
			}
			return num;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00013378 File Offset: 0x00011578
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = this.HasVertexAttribute(channel);
				if (flag)
				{
					return (T[])this.GetAllocArrayFromChannelImpl(channel, format, dim);
				}
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			return new T[0];
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000133C4 File Offset: 0x000115C4
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel)
		{
			return this.GetAllocArrayFromChannel<T>(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000133E4 File Offset: 0x000115E4
		private void SetSizedArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				bool flag5 = values == null;
				if (flag5)
				{
					valuesStart = 0;
				}
				this.SetArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000134C0 File Offset: 0x000116C0
		private void SetArrayForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int len = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, format, dim, values, len, 0, len, flags);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000134E8 File Offset: 0x000116E8
		private void SetArrayForChannel<T>(VertexAttribute channel, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int len = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), values, len, 0, len, flags);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00013514 File Offset: 0x00011714
		private void SetListForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, format, dim, NoAllocHelpers.ExtractArrayFromList<T>(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00013540 File Offset: 0x00011740
		private void SetListForChannel<T>(VertexAttribute channel, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), NoAllocHelpers.ExtractArrayFromList<T>(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001356E File Offset: 0x0001176E
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim)
		{
			this.GetListForChannel<T>(buffer, capacity, channel, dim, VertexAttributeFormat.Float32);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00013580 File Offset: 0x00011780
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim, VertexAttributeFormat channelType)
		{
			buffer.Clear();
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			else
			{
				bool flag2 = !this.HasVertexAttribute(channel);
				if (!flag2)
				{
					NoAllocHelpers.EnsureListElemCount<T>(buffer, capacity);
					this.GetArrayFromChannelImpl(channel, channelType, dim, NoAllocHelpers.ExtractArrayFromList<T>(buffer));
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000135D8 File Offset: 0x000117D8
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000135F1 File Offset: 0x000117F1
		public Vector3[] vertices
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Position);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Position, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00013600 File Offset: 0x00011800
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00013619 File Offset: 0x00011819
		public Vector3[] normals
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Normal);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Normal, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00013628 File Offset: 0x00011828
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00013641 File Offset: 0x00011841
		public Vector4[] tangents
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector4>(VertexAttribute.Tangent);
			}
			set
			{
				this.SetArrayForChannel<Vector4>(VertexAttribute.Tangent, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0001364E File Offset: 0x0001184E
		public Vector2[] uv
		{
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord0, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0001365B File Offset: 0x0001185B
		public Vector2[] uv2
		{
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord1, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00013668 File Offset: 0x00011868
		public Vector2[] uv3
		{
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord2, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00013678 File Offset: 0x00011878
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00013691 File Offset: 0x00011891
		public Color[] colors
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color>(VertexAttribute.Color);
			}
			set
			{
				this.SetArrayForChannel<Color>(VertexAttribute.Color, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000136A0 File Offset: 0x000118A0
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x000136BB File Offset: 0x000118BB
		public Color32[] colors32
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}
			set
			{
				this.SetArrayForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000136CA File Offset: 0x000118CA
		public void SetVertices(List<Vector3> inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength<Vector3>(inVertices));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000136DC File Offset: 0x000118DC
		[ExcludeFromDocs]
		public void SetVertices(List<Vector3> inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000136EA File Offset: 0x000118EA
		public void SetVertices(List<Vector3> inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Position, inVertices, start, length, flags);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000136FA File Offset: 0x000118FA
		public void SetNormals(List<Vector3> inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength<Vector3>(inNormals));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001370C File Offset: 0x0001190C
		[ExcludeFromDocs]
		public void SetNormals(List<Vector3> inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001371A File Offset: 0x0001191A
		public void SetNormals(List<Vector3> inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Normal, inNormals, start, length, flags);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001372A File Offset: 0x0001192A
		public void SetTangents(List<Vector4> inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength<Vector4>(inTangents));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001373C File Offset: 0x0001193C
		[ExcludeFromDocs]
		public void SetTangents(List<Vector4> inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001374A File Offset: 0x0001194A
		public void SetTangents(List<Vector4> inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector4>(VertexAttribute.Tangent, inTangents, start, length, flags);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001375A File Offset: 0x0001195A
		public void SetColors(List<Color> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color>(inColors));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001376C File Offset: 0x0001196C
		[ExcludeFromDocs]
		public void SetColors(List<Color> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001377A File Offset: 0x0001197A
		public void SetColors(List<Color> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color>(VertexAttribute.Color, inColors, start, length, flags);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001378A File Offset: 0x0001198A
		public void SetColors(List<Color32> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color32>(inColors));
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001379C File Offset: 0x0001199C
		[ExcludeFromDocs]
		public void SetColors(List<Color32> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000137AA File Offset: 0x000119AA
		public void SetColors(List<Color32> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, start, length, flags);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000137BC File Offset: 0x000119BC
		private void SetUvsImpl<T>(int uvIndex, int dim, List<T> uvs, int start, int length, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				Debug.LogError("The uv index is invalid. Must be in the range 0 to 7.");
			}
			else
			{
				this.SetListForChannel<T>(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, start, length, flags);
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000137FD File Offset: 0x000119FD
		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector2>(uvs));
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00013810 File Offset: 0x00011A10
		public void SetUVs(int channel, List<Vector4> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector4>(uvs));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00013823 File Offset: 0x00011A23
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00013833 File Offset: 0x00011A33
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector2>(channel, 2, uvs, start, length, flags);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00013845 File Offset: 0x00011A45
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00013855 File Offset: 0x00011A55
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector4>(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00013868 File Offset: 0x00011A68
		private void SetUvsImpl(int uvIndex, int dim, Array uvs, int arrayStart, int arraySize, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("uvIndex", uvIndex, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.SetSizedArrayForChannel(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, NoAllocHelpers.SafeLength(uvs), arrayStart, arraySize, flags);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000138B7 File Offset: 0x00011AB7
		public void SetUVs(int channel, Vector4[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000138CA File Offset: 0x00011ACA
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector4[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000138DA File Offset: 0x00011ADA
		public void SetUVs(int channel, Vector4[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000138EC File Offset: 0x00011AEC
		private void GetUVsImpl<T>(int uvIndex, List<T> uvs, int dim)
		{
			bool flag = uvs == null;
			if (flag)
			{
				throw new ArgumentNullException("uvs", "The result uvs list cannot be null.");
			}
			bool flag2 = uvIndex < 0 || uvIndex > 7;
			if (flag2)
			{
				throw new IndexOutOfRangeException("The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.GetListForChannel<T>(uvs, this.vertexCount, Mesh.GetUVChannel(uvIndex), dim);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00013941 File Offset: 0x00011B41
		public void GetUVs(int channel, List<Vector4> uvs)
		{
			this.GetUVsImpl<Vector4>(channel, uvs, 4);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001394E File Offset: 0x00011B4E
		public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
		{
			this.SetVertexBufferParamsFromArray(vertexCount, attributes);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001395C File Offset: 0x00011B5C
		public void SetVertexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferData(stream, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000139F5 File Offset: 0x00011BF5
		private void PrintErrorCantAccessIndices()
		{
			Debug.LogError(string.Format("Not allowed to access triangles/indices on mesh '{0}' (isReadable is false; Read/Write must be enabled in import settings)", base.name));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00013A10 File Offset: 0x00011C10
		private bool CheckCanAccessSubmesh(int submesh, bool errorAboutTriangles)
		{
			bool flag = !this.canAccess;
			bool flag2;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
				flag2 = false;
			}
			else
			{
				bool flag3 = submesh < 0 || submesh >= this.subMeshCount;
				if (flag3)
				{
					Debug.LogError(string.Format("Failed getting {0}. Submesh index is out of bounds.", errorAboutTriangles ? "triangles" : "indices"), this);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00013A78 File Offset: 0x00011C78
		private bool CheckCanAccessSubmeshTriangles(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, true);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00013A94 File Offset: 0x00011C94
		private bool CheckCanAccessSubmeshIndices(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, false);
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00013AB0 File Offset: 0x00011CB0
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public int[] triangles
		{
			get
			{
				bool canAccess = this.canAccess;
				int[] array;
				if (canAccess)
				{
					array = this.GetTrianglesImpl(-1, true);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
					array = new int[0];
				}
				return array;
			}
			set
			{
				bool canAccess = this.canAccess;
				if (canAccess)
				{
					this.SetTrianglesImpl(-1, IndexFormat.UInt32, value, NoAllocHelpers.SafeLength(value), 0, NoAllocHelpers.SafeLength(value), true, 0);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
				}
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00013B20 File Offset: 0x00011D20
		[ExcludeFromDocs]
		public int[] GetIndices(int submesh)
		{
			return this.GetIndices(submesh, true);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00013B3C File Offset: 0x00011D3C
		public int[] GetIndices(int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshIndices(submesh) ? this.GetIndicesImpl(submesh, applyBaseVertex) : new int[0];
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00013B68 File Offset: 0x00011D68
		public void SetIndexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public uint GetIndexCount(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexCountImpl(submesh);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00013C2C File Offset: 0x00011E2C
		private void CheckIndicesArrayRange(int valuesLength, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start can't be negative.");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", length, "Mesh indices array length can't be negative.");
			}
			bool flag3 = start >= valuesLength && length != 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start is outside of array size.");
			}
			bool flag4 = start + length > valuesLength;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length", start + length, "Mesh indices array start+count is outside of array size.");
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00013CC0 File Offset: 0x00011EC0
		private void SetTrianglesImpl(int submesh, IndexFormat indicesFormat, Array triangles, int trianglesArrayLength, int start, int length, bool calculateBounds, int baseVertex)
		{
			this.CheckIndicesArrayRange(trianglesArrayLength, start, length);
			this.SetIndicesImpl(submesh, MeshTopology.Triangles, indicesFormat, triangles, start, length, calculateBounds, baseVertex);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00013CF0 File Offset: 0x00011EF0
		public void SetTriangles(int[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00013D21 File Offset: 0x00011F21
		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00013D2F File Offset: 0x00011F2F
		public void SetTriangles(List<int> triangles, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<int>(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00013D48 File Offset: 0x00011F48
		public void SetTriangles(List<int> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, NoAllocHelpers.ExtractArrayFromList<int>(triangles), NoAllocHelpers.SafeLength<int>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00013D7E File Offset: 0x00011F7E
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh)
		{
			this.SetIndices(indices, topology, submesh, true, 0);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00013D8D File Offset: 0x00011F8D
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, bool calculateBounds)
		{
			this.SetIndices(indices, topology, submesh, calculateBounds, 0);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00013D9D File Offset: 0x00011F9D
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public void SetIndices(int[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00013DF6 File Offset: 0x00011FF6
		public void SetIndices(ushort[] indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00013E10 File Offset: 0x00012010
		public void SetIndices(ushort[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00013E4E File Offset: 0x0001204E
		public void SetIndices<T>(NativeArray<T> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			this.SetIndices<T>(indices, 0, indices.Length, topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00013E68 File Offset: 0x00012068
		public void SetIndices<T>(NativeArray<T> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int tSize = UnsafeUtility.SizeOf<T>();
				bool flag2 = tSize != 2 && tSize != 4;
				if (flag2)
				{
					throw new ArgumentException("SetIndices with NativeArray should use type is 2 or 4 bytes in size");
				}
				this.CheckIndicesArrayRange(indices.Length, indicesStart, indicesLength);
				this.SetIndicesNativeArrayImpl(submesh, topology, (tSize == 2) ? IndexFormat.UInt16 : IndexFormat.UInt32, (IntPtr)indices.GetUnsafeReadOnlyPtr<T>(), indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00013EDB File Offset: 0x000120DB
		public void SetIndices(List<int> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<int>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00013EF4 File Offset: 0x000120F4
		public void SetIndices(List<int> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int[] indicesArray = NoAllocHelpers.ExtractArrayFromList<int>(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<int>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indicesArray, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00013F39 File Offset: 0x00012139
		[ExcludeFromDocs]
		public void Clear()
		{
			this.ClearImpl(true);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00013F44 File Offset: 0x00012144
		[ExcludeFromDocs]
		public void RecalculateBounds()
		{
			this.RecalculateBounds(MeshUpdateFlags.Default);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00013F4F File Offset: 0x0001214F
		[ExcludeFromDocs]
		public void RecalculateNormals()
		{
			this.RecalculateNormals(MeshUpdateFlags.Default);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00013F5C File Offset: 0x0001215C
		public void RecalculateBounds([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateBoundsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateBounds() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00013F94 File Offset: 0x00012194
		public void RecalculateNormals([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateNormalsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateNormals() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00013FCC File Offset: 0x000121CC
		public void MarkDynamic()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.MarkDynamicImpl();
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00013FEC File Offset: 0x000121EC
		public void UploadMeshData(bool markNoLongerReadable)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.UploadMeshDataImpl(markNoLongerReadable);
			}
		}

		// Token: 0x06000A9A RID: 2714
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_indexFormat_Injected(IntPtr _unity_self, IndexFormat value);

		// Token: 0x06000A9B RID: 2715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndexBufferParams_Injected(IntPtr _unity_self, int indexCount, IndexFormat format);

		// Token: 0x06000A9C RID: 2716
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetIndexBufferData_Injected(IntPtr _unity_self, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000A9D RID: 2717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVertexBufferParamsFromArray_Injected(IntPtr _unity_self, int vertexCount, params ManagedSpanWrapper attributes);

		// Token: 0x06000A9E RID: 2718
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetVertexBufferData_Injected(IntPtr _unity_self, int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000A9F RID: 2719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetIndexCountImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x06000AA0 RID: 2720
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrianglesImpl_Injected(IntPtr _unity_self, int submesh, bool applyBaseVertex, out BlittableArrayWrapper ret);

		// Token: 0x06000AA1 RID: 2721
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesImpl_Injected(IntPtr _unity_self, int submesh, bool applyBaseVertex, out BlittableArrayWrapper ret);

		// Token: 0x06000AA2 RID: 2722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndicesImpl_Injected(IntPtr _unity_self, int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x06000AA3 RID: 2723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndicesNativeArrayImpl_Injected(IntPtr _unity_self, int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x06000AA4 RID: 2724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrintErrorCantAccessChannel_Injected(IntPtr _unity_self, VertexAttribute ch);

		// Token: 0x06000AA5 RID: 2725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVertexAttribute_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x06000AA6 RID: 2726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetArrayForChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		// Token: 0x06000AA7 RID: 2727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array GetAllocArrayFromChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim);

		// Token: 0x06000AA8 RID: 2728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetArrayFromChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim, Array values);

		// Token: 0x06000AA9 RID: 2729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canAccess_Injected(IntPtr _unity_self);

		// Token: 0x06000AAA RID: 2730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertexCount_Injected(IntPtr _unity_self);

		// Token: 0x06000AAB RID: 2731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subMeshCount_Injected(IntPtr _unity_self);

		// Token: 0x06000AAC RID: 2732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subMeshCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000AAD RID: 2733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSubMesh_Injected(IntPtr _unity_self, int index, [In] ref SubMeshDescriptor desc, MeshUpdateFlags flags);

		// Token: 0x06000AAE RID: 2734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSubMesh_Injected(IntPtr _unity_self, int index, out SubMeshDescriptor ret);

		// Token: 0x06000AAF RID: 2735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000AB0 RID: 2736
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x06000AB1 RID: 2737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearImpl_Injected(IntPtr _unity_self, bool keepVertexLayout);

		// Token: 0x06000AB2 RID: 2738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateBoundsImpl_Injected(IntPtr _unity_self, MeshUpdateFlags flags);

		// Token: 0x06000AB3 RID: 2739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateNormalsImpl_Injected(IntPtr _unity_self, MeshUpdateFlags flags);

		// Token: 0x06000AB4 RID: 2740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkDynamicImpl_Injected(IntPtr _unity_self);

		// Token: 0x06000AB5 RID: 2741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UploadMeshDataImpl_Injected(IntPtr _unity_self, bool markNoLongerReadable);

		// Token: 0x02000128 RID: 296
		[StaticAccessor("MeshDataBindings", StaticAccessorType.DoubleColon)]
		[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
		public struct MeshData
		{
			// Token: 0x040003F3 RID: 1011
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_Ptr;
		}

		// Token: 0x02000129 RID: 297
		[DefaultMember("Item")]
		[StaticAccessor("MeshDataArrayBindings", StaticAccessorType.DoubleColon)]
		[NativeContainer]
		[NativeContainerSupportsMinMaxWriteRestriction]
		public struct MeshDataArray : IDisposable
		{
			// Token: 0x06000AB6 RID: 2742
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void ReleaseMeshDatas(IntPtr* datas, int count);

			// Token: 0x06000AB7 RID: 2743
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void CreateNewMeshDatas(IntPtr* datas, int count);

			// Token: 0x06000AB8 RID: 2744 RVA: 0x0001400C File Offset: 0x0001220C
			public unsafe void Dispose()
			{
				UnsafeUtility.LeakErase((IntPtr)((void*)this.m_Ptrs), LeakCategory.MeshDataArray);
				bool flag = this.m_Length != 0;
				if (flag)
				{
					Mesh.MeshDataArray.ReleaseMeshDatas(this.m_Ptrs, this.m_Length);
					UnsafeUtility.Free((void*)this.m_Ptrs, Allocator.Persistent);
				}
				this.m_Ptrs = null;
				this.m_Length = 0;
			}

			// Token: 0x06000AB9 RID: 2745 RVA: 0x0001406C File Offset: 0x0001226C
			internal unsafe MeshDataArray(int meshesCount)
			{
				bool flag = meshesCount < 0;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh count can not be negative (was {0})", meshesCount));
				}
				this.m_Length = meshesCount;
				int totalSize = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)totalSize, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.CreateNewMeshDatas(this.m_Ptrs, meshesCount);
			}

			// Token: 0x040003F4 RID: 1012
			[NativeDisableUnsafePtrRestriction]
			internal unsafe IntPtr* m_Ptrs;

			// Token: 0x040003F5 RID: 1013
			internal int m_Length;
		}
	}
}
