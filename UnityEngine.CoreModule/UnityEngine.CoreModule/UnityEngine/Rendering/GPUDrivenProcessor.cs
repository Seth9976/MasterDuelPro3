using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000390 RID: 912
	[NativeHeader("Runtime/Camera/GPUDrivenProcessor.h")]
	[RequiredByNativeCode]
	internal class GPUDrivenProcessor
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00035688 File Offset: 0x00033888
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x00035690 File Offset: 0x00033890
		internal List<Mesh> scratchMeshes { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00035699 File Offset: 0x00033899
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x000356A1 File Offset: 0x000338A1
		internal List<Material> scratchMaterials { get; private set; }

		// Token: 0x06001901 RID: 6401 RVA: 0x000356AA File Offset: 0x000338AA
		public GPUDrivenProcessor()
		{
			this.m_Ptr = GPUDrivenProcessor.Internal_Create();
			this.scratchMeshes = new List<Mesh>();
			this.scratchMaterials = new List<Material>();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000356D8 File Offset: 0x000338D8
		~GPUDrivenProcessor()
		{
			this.Destroy();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00035708 File Offset: 0x00033908
		public void Dispose()
		{
			this.scratchMeshes = null;
			this.scratchMaterials = null;
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0003572C File Offset: 0x0003392C
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GPUDrivenProcessor.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06001905 RID: 6405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06001906 RID: 6406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06001907 RID: 6407 RVA: 0x00035768 File Offset: 0x00033968
		private unsafe void EnableGPUDrivenRenderingAndDispatchRendererData(ReadOnlySpan<int> renderersID, GPUDrivenRendererDataNativeCallback callback, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback param)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.EnableGPUDrivenRenderingAndDispatchRendererData_Injected(intPtr, ref managedSpanWrapper, callback, meshes, materials, param);
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000357B0 File Offset: 0x000339B0
		public void EnableGPUDrivenRenderingAndDispatchRendererData(ReadOnlySpan<int> renderersID, GPUDrivenRendererDataCallback callback)
		{
			this.scratchMeshes.Clear();
			this.scratchMaterials.Clear();
			this.EnableGPUDrivenRenderingAndDispatchRendererData(renderersID, GPUDrivenProcessor.s_NativeRendererCallback, this.scratchMeshes, this.scratchMaterials, callback);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000357E8 File Offset: 0x000339E8
		public unsafe void DisableGPUDrivenRendering(ReadOnlySpan<int> renderersID)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.DisableGPUDrivenRendering_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0003582C File Offset: 0x00033A2C
		private unsafe void DispatchLODGroupData(ReadOnlySpan<int> lodGroupID, GPUDrivenLODGroupDataNativeCallback callback, GPUDrivenLODGroupDataCallback param)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = lodGroupID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.DispatchLODGroupData_Injected(intPtr, ref managedSpanWrapper, callback, param);
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00035870 File Offset: 0x00033A70
		public void DispatchLODGroupData(ReadOnlySpan<int> lodGroupID, GPUDrivenLODGroupDataCallback callback)
		{
			this.DispatchLODGroupData(lodGroupID, GPUDrivenProcessor.s_NativeLODGroupCallback, callback);
		}

		// Token: 0x1700038A RID: 906
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x00035884 File Offset: 0x00033A84
		public bool enablePartialRendering
		{
			set
			{
				IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GPUDrivenProcessor.set_enablePartialRendering_Injected(intPtr, value);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000358A8 File Offset: 0x00033AA8
		public void ClearMaterialFilters()
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GPUDrivenProcessor.ClearMaterialFilters_Injected(intPtr);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000358CC File Offset: 0x00033ACC
		[FreeFunction("GPUDrivenProcessor::FindUnsupportedMaterials", IsThreadSafe = true)]
		private unsafe static int FindUnsupportedMaterialsImpl(ReadOnlySpan<int> materialIDs, Span<int> unsupportedMaterialIDs)
		{
			ReadOnlySpan<int> readOnlySpan = materialIDs;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<int> span = unsupportedMaterialIDs;
				int num;
				fixed (int* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					num = GPUDrivenProcessor.FindUnsupportedMaterialsImpl_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
				return num;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00035924 File Offset: 0x00033B24
		public static int FindUnsupportedMaterialIDs(NativeArray<int> materialIDs, NativeArray<int> unsupportedMaterialIDs)
		{
			return GPUDrivenProcessor.FindUnsupportedMaterialsImpl(in materialIDs, in unsupportedMaterialIDs);
		}

		// Token: 0x06001911 RID: 6417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGPUDrivenRenderingAndDispatchRendererData_Injected(IntPtr _unity_self, ref ManagedSpanWrapper renderersID, GPUDrivenRendererDataNativeCallback callback, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback param);

		// Token: 0x06001912 RID: 6418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGPUDrivenRendering_Injected(IntPtr _unity_self, ref ManagedSpanWrapper renderersID);

		// Token: 0x06001913 RID: 6419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchLODGroupData_Injected(IntPtr _unity_self, ref ManagedSpanWrapper lodGroupID, GPUDrivenLODGroupDataNativeCallback callback, GPUDrivenLODGroupDataCallback param);

		// Token: 0x06001914 RID: 6420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enablePartialRendering_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001915 RID: 6421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMaterialFilters_Injected(IntPtr _unity_self);

		// Token: 0x06001916 RID: 6422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindUnsupportedMaterialsImpl_Injected(ref ManagedSpanWrapper materialIDs, ref ManagedSpanWrapper unsupportedMaterialIDs);

		// Token: 0x04000B05 RID: 2821
		internal IntPtr m_Ptr;

		// Token: 0x04000B08 RID: 2824
		private static GPUDrivenRendererDataNativeCallback s_NativeRendererCallback = delegate(in GPUDrivenRendererGroupDataNative nativeData, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback callback)
		{
			NativeArray<int> rendererGroupID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.rendererGroupID, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Bounds> localBounds = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Bounds>((void*)nativeData.localBounds, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Vector4> lightmapScaleOffset = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>((void*)nativeData.lightmapScaleOffset, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> gameObjectLayer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.gameObjectLayer, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<uint> renderingLayerMask = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<uint>((void*)nativeData.renderingLayerMask, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> lodGroupID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodGroupID, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> lightmapIndex = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.motionVecGenMode, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<GPUDrivenPackedRendererData> packedRendererData = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenPackedRendererData>((void*)nativeData.packedRendererData, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> rendererPriority = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.rendererPriority, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> meshIndex = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.meshIndex, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<short> subMeshStartIndex = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.subMeshStartIndex, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> materialsOffset = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialsOffset, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<short> materialsCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.materialsCount, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> instancesOffset = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<int> instancesCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<GPUDrivenRendererEditorData> editorData = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenRendererEditorData>((void*)nativeData.editorData, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> invalidRendererGroupID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.invalidRendererGroupID, nativeData.invalidRendererGroupIDCount, Allocator.Invalid);
			NativeArray<Matrix4x4> localToWorldMatrix = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>((void*)nativeData.localToWorldMatrix, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Matrix4x4> prevLocalToWorldMatrix = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>((void*)nativeData.prevLocalToWorldMatrix, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> rendererGroupIndex = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<int> meshID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.meshID, nativeData.meshCount, Allocator.Invalid);
			NativeArray<short> subMeshCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.subMeshCount, nativeData.meshCount, Allocator.Invalid);
			NativeArray<int> subMeshDescOffset = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.subMeshDescOffset, nativeData.meshCount, Allocator.Invalid);
			NativeArray<SubMeshDescriptor> subMeshDesc = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<SubMeshDescriptor>((void*)nativeData.subMeshDesc, nativeData.subMeshDescCount, Allocator.Invalid);
			NativeArray<int> materialIndex = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialIndex, nativeData.materialIndexCount, Allocator.Invalid);
			NativeArray<int> materialID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialID, nativeData.materialCount, Allocator.Invalid);
			NativeArray<GPUDrivenPackedMaterialData> packedMaterialData = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenPackedMaterialData>((void*)nativeData.packedMaterialData, nativeData.materialCount, Allocator.Invalid);
			NativeArray<int> materialFilterFlags = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialFilterFlags, nativeData.materialCount, Allocator.Invalid);
			GPUDrivenRendererGroupData data = new GPUDrivenRendererGroupData
			{
				rendererGroupID = rendererGroupID,
				localBounds = localBounds,
				lightmapScaleOffset = lightmapScaleOffset,
				gameObjectLayer = gameObjectLayer,
				renderingLayerMask = renderingLayerMask,
				lodGroupID = lodGroupID,
				lightmapIndex = lightmapIndex,
				packedRendererData = packedRendererData,
				rendererPriority = rendererPriority,
				meshIndex = meshIndex,
				subMeshStartIndex = subMeshStartIndex,
				materialsOffset = materialsOffset,
				materialsCount = materialsCount,
				instancesOffset = instancesOffset,
				instancesCount = instancesCount,
				editorData = editorData,
				invalidRendererGroupID = invalidRendererGroupID,
				localToWorldMatrix = localToWorldMatrix,
				prevLocalToWorldMatrix = prevLocalToWorldMatrix,
				rendererGroupIndex = rendererGroupIndex,
				meshID = meshID,
				subMeshCount = subMeshCount,
				subMeshDescOffset = subMeshDescOffset,
				subMeshDesc = subMeshDesc,
				materialIndex = materialIndex,
				materialID = materialID,
				packedMaterialData = packedMaterialData,
				materialFilterFlags = materialFilterFlags
			};
			callback(in data, meshes, materials);
		};

		// Token: 0x04000B09 RID: 2825
		private static GPUDrivenLODGroupDataNativeCallback s_NativeLODGroupCallback = delegate(in GPUDrivenLODGroupDataNative nativeData, GPUDrivenLODGroupDataCallback callback)
		{
			NativeArray<int> lodGroupID2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodGroupID, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> lodOffset = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodOffset, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> lodCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodCount, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<LODFadeMode> fadeMode = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LODFadeMode>((void*)nativeData.fadeMode, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<Vector3> worldSpaceReferencePoint = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>((void*)nativeData.worldSpaceReferencePoint, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<float> worldSpaceSize = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.worldSpaceSize, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<short> renderersCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.renderersCount, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<bool> lastLODIsBillboard = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<bool>((void*)nativeData.lastLODIsBillboard, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> invalidLODGroupID = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.invalidLODGroupID, nativeData.invalidLODGroupCount, Allocator.Invalid);
			NativeArray<short> lodRenderersCount = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.lodRenderersCount, nativeData.lodDataCount, Allocator.Invalid);
			NativeArray<float> lodScreenRelativeTransitionHeight = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.lodScreenRelativeTransitionHeight, nativeData.lodDataCount, Allocator.Invalid);
			NativeArray<float> lodFadeTransitionWidth = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.lodFadeTransitionWidth, nativeData.lodDataCount, Allocator.Invalid);
			GPUDrivenLODGroupData data2 = new GPUDrivenLODGroupData
			{
				lodGroupID = lodGroupID2,
				lodOffset = lodOffset,
				lodCount = lodCount,
				fadeMode = fadeMode,
				worldSpaceReferencePoint = worldSpaceReferencePoint,
				worldSpaceSize = worldSpaceSize,
				renderersCount = renderersCount,
				lastLODIsBillboard = lastLODIsBillboard,
				invalidLODGroupID = invalidLODGroupID,
				lodRenderersCount = lodRenderersCount,
				lodScreenRelativeTransitionHeight = lodScreenRelativeTransitionHeight,
				lodFadeTransitionWidth = lodFadeTransitionWidth
			};
			callback(in data2);
		};

		// Token: 0x02000391 RID: 913
		internal static class BindingsMarshaller
		{
			// Token: 0x06001917 RID: 6423 RVA: 0x00035975 File Offset: 0x00033B75
			public static IntPtr ConvertToNative(GPUDrivenProcessor obj)
			{
				return obj.m_Ptr;
			}
		}
	}
}
