using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000387 RID: 903
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class BatchRendererGroup : IDisposable
	{
		// Token: 0x060018D6 RID: 6358 RVA: 0x000352F4 File Offset: 0x000334F4
		public unsafe BatchRendererGroup(BatchRendererGroupCreateInfo info)
		{
			this.m_PerformCulling = info.cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this, (void*)info.userContext);
			this.m_FinishedCulling = info.finishedCullingCallback;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00035343 File Offset: 0x00033543
		public void Dispose()
		{
			BatchRendererGroup.Destroy(this.m_GroupHandle);
			this.m_GroupHandle = IntPtr.Zero;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00035360 File Offset: 0x00033560
		private BatchID AddDrawCommandBatch(IntPtr values, int count, GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchID batchID;
			BatchRendererGroup.AddDrawCommandBatch_Injected(intPtr, values, count, ref buffer, bufferOffset, windowSize, out batchID);
			return batchID;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00035390 File Offset: 0x00033590
		public BatchID AddBatch(NativeArray<MetadataValue> batchMetadata, GraphicsBufferHandle buffer)
		{
			return this.AddDrawCommandBatch((IntPtr)batchMetadata.GetUnsafeReadOnlyPtr<MetadataValue>(), batchMetadata.Length, buffer, 0U, 0U);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000353C0 File Offset: 0x000335C0
		private void RemoveDrawCommandBatch(BatchID batchID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.RemoveDrawCommandBatch_Injected(intPtr, ref batchID);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000353E4 File Offset: 0x000335E4
		public void RemoveBatch(BatchID batchID)
		{
			this.RemoveDrawCommandBatch(batchID);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000353F0 File Offset: 0x000335F0
		internal unsafe void RegisterMaterials(ReadOnlySpan<int> materialID, Span<BatchMaterialID> batchMaterialID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = materialID;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<BatchMaterialID> span = batchMaterialID;
				fixed (BatchMaterialID* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					BatchRendererGroup.RegisterMaterials_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00035458 File Offset: 0x00033658
		public void UnregisterMaterial(BatchMaterialID material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.UnregisterMaterial_Injected(intPtr, ref material);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0003547C File Offset: 0x0003367C
		internal unsafe void RegisterMeshes(ReadOnlySpan<int> meshID, Span<BatchMeshID> batchMeshID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = meshID;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<BatchMeshID> span = batchMeshID;
				fixed (BatchMeshID* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					BatchRendererGroup.RegisterMeshes_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000354E4 File Offset: 0x000336E4
		public void UnregisterMesh(BatchMeshID mesh)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.UnregisterMesh_Injected(intPtr, ref mesh);
		}

		// Token: 0x060018E0 RID: 6368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern BatchBufferTarget GetBufferTarget();

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00035508 File Offset: 0x00033708
		public static BatchBufferTarget BufferTarget
		{
			get
			{
				return BatchRendererGroup.GetBufferTarget();
			}
		}

		// Token: 0x060018E2 RID: 6370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Create([Unmarshalled] BatchRendererGroup group, void* userContext);

		// Token: 0x060018E3 RID: 6371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr groupHandle);

		// Token: 0x060018E4 RID: 6372 RVA: 0x00035510 File Offset: 0x00033710
		[RequiredByNativeCode]
		private unsafe static void InvokeOnPerformCulling(BatchRendererGroup group, ref BatchRendererCullingOutput context, ref LODParameters lodParameters, IntPtr userContext)
		{
			NativeArray<Plane> cullingPlanes = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Plane>((void*)context.cullingPlanes, context.cullingPlaneCount, Allocator.Invalid);
			NativeArray<CullingSplit> cullingSplits = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<CullingSplit>((void*)context.cullingSplits, context.cullingSplitCount, Allocator.Invalid);
			NativeArray<BatchCullingOutputDrawCommands> drawCommands = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BatchCullingOutputDrawCommands>((void*)context.drawCommands, 1, Allocator.Invalid);
			try
			{
				BatchCullingOutput cullingOutput = new BatchCullingOutput
				{
					drawCommands = drawCommands,
					customCullingResult = new NativeArray<IntPtr>(1, Allocator.Temp, NativeArrayOptions.ClearMemory)
				};
				context.cullingJobsFence = group.m_PerformCulling(group, new BatchCullingContext(cullingPlanes, cullingSplits, lodParameters, context.localToWorldMatrix, context.viewType, context.projectionType, context.cullingFlags, context.viewID, context.cullingLayerMask, context.sceneCullingMask, context.splitExclusionMask, context.receiverPlaneOffset, context.receiverPlaneCount, context.occlusionBuffer), cullingOutput, userContext);
				context.customCullingResult = cullingOutput.customCullingResult[0];
			}
			finally
			{
				JobHandle.ScheduleBatchedJobs();
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00035608 File Offset: 0x00033808
		[RequiredByNativeCode]
		private static void InvokeOnFinishedCulling(BatchRendererGroup group, IntPtr customCullingResult)
		{
			try
			{
				bool flag = group.m_FinishedCulling != null;
				if (flag)
				{
					group.m_FinishedCulling(customCullingResult);
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00035650 File Offset: 0x00033850
		[FreeFunction("BatchRendererGroup::OcclusionTestAABB", IsThreadSafe = true)]
		internal static bool OcclusionTestAABB(IntPtr occlusionBuffer, Bounds aabb)
		{
			return BatchRendererGroup.OcclusionTestAABB_Injected(occlusionBuffer, ref aabb);
		}

		// Token: 0x060018E7 RID: 6375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDrawCommandBatch_Injected(IntPtr _unity_self, IntPtr values, int count, [In] ref GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize, out BatchID ret);

		// Token: 0x060018E8 RID: 6376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveDrawCommandBatch_Injected(IntPtr _unity_self, [In] ref BatchID batchID);

		// Token: 0x060018E9 RID: 6377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMaterials_Injected(IntPtr _unity_self, ref ManagedSpanWrapper materialID, ref ManagedSpanWrapper batchMaterialID);

		// Token: 0x060018EA RID: 6378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterMaterial_Injected(IntPtr _unity_self, [In] ref BatchMaterialID material);

		// Token: 0x060018EB RID: 6379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMeshes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper meshID, ref ManagedSpanWrapper batchMeshID);

		// Token: 0x060018EC RID: 6380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterMesh_Injected(IntPtr _unity_self, [In] ref BatchMeshID mesh);

		// Token: 0x060018ED RID: 6381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool OcclusionTestAABB_Injected(IntPtr occlusionBuffer, [In] ref Bounds aabb);

		// Token: 0x04000B02 RID: 2818
		private IntPtr m_GroupHandle = IntPtr.Zero;

		// Token: 0x04000B03 RID: 2819
		private BatchRendererGroup.OnPerformCulling m_PerformCulling;

		// Token: 0x04000B04 RID: 2820
		private BatchRendererGroup.OnFinishedCulling m_FinishedCulling;

		// Token: 0x02000388 RID: 904
		// (Invoke) Token: 0x060018EF RID: 6383
		public delegate JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext, BatchCullingOutput cullingOutput, IntPtr userContext);

		// Token: 0x02000389 RID: 905
		// (Invoke) Token: 0x060018F1 RID: 6385
		public delegate void OnFinishedCulling(IntPtr customCullingResult);

		// Token: 0x0200038A RID: 906
		internal static class BindingsMarshaller
		{
			// Token: 0x060018F2 RID: 6386 RVA: 0x00035665 File Offset: 0x00033865
			public static IntPtr ConvertToNative(BatchRendererGroup batchRendererGroup)
			{
				return batchRendererGroup.m_GroupHandle;
			}
		}
	}
}
