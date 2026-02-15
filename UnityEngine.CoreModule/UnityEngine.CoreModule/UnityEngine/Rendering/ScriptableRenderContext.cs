using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C5 RID: 965
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		// Token: 0x06001A2E RID: 6702
		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int volumeDepth, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		// Token: 0x06001A2F RID: 6703
		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly, bool isStencilReadOnly);

		// Token: 0x06001A30 RID: 6704
		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSubPass_Internal(IntPtr self);

		// Token: 0x06001A31 RID: 6705
		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		// Token: 0x06001A32 RID: 6706
		[FreeFunction("ScriptableRenderContext::HasInvokeOnRenderObjectCallbacks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasInvokeOnRenderObjectCallbacks_Internal();

		// Token: 0x06001A33 RID: 6707 RVA: 0x00039440 File Offset: 0x00037640
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00039458 File Offset: 0x00037658
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_CullShadowCasters")]
		private static void Internal_CullShadowCasters(ScriptableRenderContext renderLoop, IntPtr context)
		{
			ScriptableRenderContext.Internal_CullShadowCasters_Injected(ref renderLoop, context);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00039470 File Offset: 0x00037670
		[FreeFunction("InitializeSortSettings")]
		internal static void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings)
		{
			ScriptableRenderContext.InitializeSortSettings_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera), out sortingSettings);
		}

		// Token: 0x06001A36 RID: 6710
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Submit_Internal();

		// Token: 0x06001A37 RID: 6711
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SubmitForRenderPassValidation_Internal();

		// Token: 0x06001A38 RID: 6712
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCameras_Internal(Type listType, object resultList);

		// Token: 0x06001A39 RID: 6713 RVA: 0x0003948C File Offset: 0x0003768C
		[FreeFunction("PlayerEmitCanvasGeometryForCamera")]
		public static void EmitGeometryForCamera(Camera camera)
		{
			ScriptableRenderContext.EmitGeometryForCamera_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000394A4 File Offset: 0x000376A4
		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, (commandBuffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(commandBuffer));
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000394C8 File Offset: 0x000376C8
		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, (commandBuffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(commandBuffer), queueType);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000394EC File Offset: 0x000376EC
		private void SetupCameraProperties_Internal([NotNull] Camera camera, bool stereoSetup, int eye)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, intPtr, stereoSetup, eye);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00039528 File Offset: 0x00037728
		private void DrawWireOverlay_Impl([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.DrawWireOverlay_Impl_Injected(ref this, intPtr);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00039560 File Offset: 0x00037760
		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00039578 File Offset: 0x00037778
		private RendererList CreateRendererList_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			RendererList rendererList;
			ScriptableRenderContext.CreateRendererList_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0003959C File Offset: 0x0003779C
		private RendererList CreateShadowRendererList_Internal(IntPtr shadowDrawinSettings)
		{
			RendererList rendererList;
			ScriptableRenderContext.CreateShadowRendererList_Internal_Injected(ref this, shadowDrawinSettings, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000395B4 File Offset: 0x000377B4
		private RendererList CreateSkyboxRendererList_Internal([NotNull] Camera camera, int mode, Matrix4x4 proj, Matrix4x4 view, Matrix4x4 projR, Matrix4x4 viewR)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateSkyboxRendererList_Internal_Injected(ref this, intPtr, mode, ref proj, ref view, ref projR, ref viewR, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000395F8 File Offset: 0x000377F8
		private RendererList CreateGizmoRendererList_Internal([NotNull] Camera camera, GizmoSubset gizmoSubset)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateGizmoRendererList_Internal_Injected(ref this, intPtr, gizmoSubset, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00039634 File Offset: 0x00037834
		private RendererList CreateUIOverlayRendererList_Internal([NotNull] Camera camera, UISubset uiSubset)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateUIOverlayRendererList_Internal_Injected(ref this, intPtr, uiSubset, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00039670 File Offset: 0x00037870
		private RendererList CreateWireOverlayRendererList_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateWireOverlayRendererList_Internal_Injected(ref this, intPtr, out rendererList);
			return rendererList;
		}

		// Token: 0x06001A45 RID: 6725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PrepareRendererListsAsync_Internal(object rendererLists);

		// Token: 0x06001A46 RID: 6726 RVA: 0x000396AC File Offset: 0x000378AC
		private RendererListStatus QueryRendererListStatus_Internal(RendererList handle)
		{
			return ScriptableRenderContext.QueryRendererListStatus_Internal_Injected(ref this, ref handle);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000396C1 File Offset: 0x000378C1
		internal ScriptableRenderContext(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000396CB File Offset: 0x000378CB
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, 1, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000396F3 File Offset: 0x000378F3
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00039728 File Offset: 0x00037928
		public void BeginSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00039751 File Offset: 0x00037951
		public void EndSubPass()
		{
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00039760 File Offset: 0x00037960
		public void EndRenderPass()
		{
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0003976F File Offset: 0x0003796F
		public void Submit()
		{
			this.Submit_Internal();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0003977C File Offset: 0x0003797C
		public bool SubmitForRenderPassValidation()
		{
			return this.SubmitForRenderPassValidation_Internal();
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00039794 File Offset: 0x00037994
		public bool HasInvokeOnRenderObjectCallbacks()
		{
			return ScriptableRenderContext.HasInvokeOnRenderObjectCallbacks_Internal();
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000397AB File Offset: 0x000379AB
		internal void GetCameras(List<Camera> results)
		{
			this.GetCameras_Internal(typeof(Camera), results);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000397C0 File Offset: 0x000379C0
		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00039808 File Offset: 0x00037A08
		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00039851 File Offset: 0x00037A51
		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0003985E File Offset: 0x00037A5E
		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0003986B File Offset: 0x00037A6B
		public void DrawWireOverlay(Camera camera)
		{
			this.DrawWireOverlay_Impl(camera);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00039878 File Offset: 0x00037A78
		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults results = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&results)));
			return results;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000398A8 File Offset: 0x00037AA8
		public unsafe void CullShadowCasters(CullingResults cullingResults, ShadowCastersCullingInfos infos)
		{
			ScriptableRenderContext.CullShadowCastersContext context = default(ScriptableRenderContext.CullShadowCastersContext);
			context.cullResults = cullingResults.ptr;
			context.splitBuffer = (ShadowSplitData*)infos.splitBuffer.GetUnsafePtr<ShadowSplitData>();
			context.splitBufferLength = infos.splitBuffer.Length;
			context.perLightInfos = (LightShadowCasterCullingInfo*)infos.perLightInfos.GetUnsafePtr<LightShadowCasterCullingInfo>();
			context.perLightInfoCount = infos.perLightInfos.Length;
			ScriptableRenderContext.Internal_CullShadowCasters(this, (IntPtr)((void*)(&context)));
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0003992C File Offset: 0x00037B2C
		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00039954 File Offset: 0x00037B54
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0003998C File Offset: 0x00037B8C
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000399AC File Offset: 0x00037BAC
		public RendererList CreateRendererList(ref RendererListParams param)
		{
			param.Validate();
			return this.CreateRendererList_Internal(param.cullingResults.ptr, ref param.drawSettings, ref param.filteringSettings, param.tagName, param.isPassTagName, param.tagsValuePtr, param.stateBlocksPtr, param.numStateBlocks);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00039A04 File Offset: 0x00037C04
		public unsafe RendererList CreateShadowRendererList(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* settingsPtr = ptr;
				return this.CreateShadowRendererList_Internal((IntPtr)((void*)settingsPtr));
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00039A28 File Offset: 0x00037C28
		public RendererList CreateSkyboxRendererList(Camera camera, Matrix4x4 projectionMatrixL, Matrix4x4 viewMatrixL, Matrix4x4 projectionMatrixR, Matrix4x4 viewMatrixR)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 2, projectionMatrixL, viewMatrixL, projectionMatrixR, viewMatrixR);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00039A48 File Offset: 0x00037C48
		public RendererList CreateSkyboxRendererList(Camera camera, Matrix4x4 projectionMatrix, Matrix4x4 viewMatrix)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 1, projectionMatrix, viewMatrix, Matrix4x4.identity, Matrix4x4.identity);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00039A70 File Offset: 0x00037C70
		public RendererList CreateSkyboxRendererList(Camera camera)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 0, Matrix4x4.identity, Matrix4x4.identity, Matrix4x4.identity, Matrix4x4.identity);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00039AA0 File Offset: 0x00037CA0
		public RendererList CreateGizmoRendererList(Camera camera, GizmoSubset gizmoSubset)
		{
			return this.CreateGizmoRendererList_Internal(camera, gizmoSubset);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00039ABC File Offset: 0x00037CBC
		public RendererList CreateUIOverlayRendererList(Camera camera)
		{
			return this.CreateUIOverlayRendererList_Internal(camera, UISubset.All);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00039AD8 File Offset: 0x00037CD8
		public RendererList CreateUIOverlayRendererList(Camera camera, UISubset uiSubset)
		{
			return this.CreateUIOverlayRendererList_Internal(camera, uiSubset);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00039AF4 File Offset: 0x00037CF4
		public RendererList CreateWireOverlayRendererList(Camera camera)
		{
			return this.CreateWireOverlayRendererList_Internal(camera);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00039B0D File Offset: 0x00037D0D
		public void PrepareRendererListsAsync(List<RendererList> rendererLists)
		{
			this.PrepareRendererListsAsync_Internal(rendererLists);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00039B18 File Offset: 0x00037D18
		public RendererListStatus QueryRendererListStatus(RendererList rendererList)
		{
			return this.QueryRendererListStatus_Internal(rendererList);
		}

		// Token: 0x06001A67 RID: 6759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, [In] ref ScriptableRenderContext renderLoop, IntPtr results);

		// Token: 0x06001A68 RID: 6760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CullShadowCasters_Injected([In] ref ScriptableRenderContext renderLoop, IntPtr context);

		// Token: 0x06001A69 RID: 6761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeSortSettings_Injected(IntPtr camera, out SortingSettings sortingSettings);

		// Token: 0x06001A6A RID: 6762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EmitGeometryForCamera_Injected(IntPtr camera);

		// Token: 0x06001A6B RID: 6763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr commandBuffer);

		// Token: 0x06001A6C RID: 6764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr commandBuffer, ComputeQueueType queueType);

		// Token: 0x06001A6D RID: 6765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, bool stereoSetup, int eye);

		// Token: 0x06001A6E RID: 6766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireOverlay_Impl_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera);

		// Token: 0x06001A6F RID: 6767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, [In] ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount, out RendererList ret);

		// Token: 0x06001A70 RID: 6768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateShadowRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawinSettings, out RendererList ret);

		// Token: 0x06001A71 RID: 6769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSkyboxRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, int mode, [In] ref Matrix4x4 proj, [In] ref Matrix4x4 view, [In] ref Matrix4x4 projR, [In] ref Matrix4x4 viewR, out RendererList ret);

		// Token: 0x06001A72 RID: 6770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGizmoRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, GizmoSubset gizmoSubset, out RendererList ret);

		// Token: 0x06001A73 RID: 6771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUIOverlayRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, UISubset uiSubset, out RendererList ret);

		// Token: 0x06001A74 RID: 6772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWireOverlayRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, out RendererList ret);

		// Token: 0x06001A75 RID: 6773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RendererListStatus QueryRendererListStatus_Internal_Injected(ref ScriptableRenderContext _unity_self, [In] ref RendererList handle);

		// Token: 0x04000C6A RID: 3178
		private static readonly ShaderTagId kRenderTypeTag = new ShaderTagId("RenderType");

		// Token: 0x04000C6B RID: 3179
		private IntPtr m_Ptr;

		// Token: 0x04000C6C RID: 3180
		private const bool deprecateDrawXmethods = false;

		// Token: 0x020003C6 RID: 966
		private struct CullShadowCastersContext
		{
			// Token: 0x04000C6D RID: 3181
			public IntPtr cullResults;

			// Token: 0x04000C6E RID: 3182
			public unsafe ShadowSplitData* splitBuffer;

			// Token: 0x04000C6F RID: 3183
			public int splitBufferLength;

			// Token: 0x04000C70 RID: 3184
			public unsafe LightShadowCasterCullingInfo* perLightInfos;

			// Token: 0x04000C71 RID: 3185
			public int perLightInfoCount;
		}
	}
}
