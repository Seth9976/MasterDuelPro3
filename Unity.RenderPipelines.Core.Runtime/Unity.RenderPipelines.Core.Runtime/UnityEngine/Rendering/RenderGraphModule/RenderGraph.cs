using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000229 RID: 553
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class RenderGraph
	{
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00035718 File Offset: 0x00033918
		internal NativePassCompiler CompileNativeRenderGraph(int graphHash)
		{
			NativePassCompiler nativePassCompiler;
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.CompileRenderGraph)))
			{
				if (this.nativeCompiler == null)
				{
					this.nativeCompiler = new NativePassCompiler(this.m_CompilationCache);
				}
				if (!this.nativeCompiler.Initialize(this.m_Resources, this.m_RenderPasses, this.m_DebugParameters.disablePassCulling, this.name, this.m_EnableCompilationCaching, graphHash, this.m_ExecutionCount))
				{
					this.nativeCompiler.Compile(this.m_Resources);
				}
				NativeList<PassData> passData = this.nativeCompiler.contextData.passData;
				int numPasses = passData.Length;
				for (int i = 0; i < numPasses; i++)
				{
					if (!passData.ElementAt(i).culled)
					{
						RenderGraphPass rp = this.m_RenderPasses[i];
						this.m_RendererLists.AddRange(rp.usedRendererListList);
					}
				}
				this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, this.m_RendererListCulling);
				nativePassCompiler = this.nativeCompiler;
			}
			return nativePassCompiler;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00035840 File Offset: 0x00033A40
		private void ExecuteNativeRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.ExecuteRenderGraph)))
			{
				this.nativeCompiler.ExecuteGraph(this.m_RenderGraphContext, this.m_Resources, in this.m_RenderPasses);
				if (!this.m_RenderGraphContext.contextlessTesting)
				{
					this.m_RenderGraphContext.renderContext.ExecuteCommandBuffer(this.m_RenderGraphContext.cmd);
				}
				this.m_RenderGraphContext.cmd.Clear();
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000358DC File Offset: 0x00033ADC
		[Conditional("UNITY_EDITOR")]
		private void AddPassDebugMetadata(string passName, string file, int line)
		{
			if (this.m_CaptureDebugDataForExecution == null)
			{
				return;
			}
			for (int i = 0; i < this.k_PassNameDebugIgnoreList.Length; i++)
			{
				if (passName == this.k_PassNameDebugIgnoreList[i])
				{
					return;
				}
			}
			if (!RenderGraph.DebugData.s_PassScriptMetadata.TryAdd(passName, new RenderGraph.DebugData.PassScriptInfo
			{
				filePath = file,
				line = line
			}))
			{
				string existingFile = RenderGraph.DebugData.s_PassScriptMetadata[passName].filePath;
				int existingLine = RenderGraph.DebugData.s_PassScriptMetadata[passName].line;
				if (existingFile != file || existingLine != line)
				{
					Debug.LogWarning(string.Format("Two passes called {0} in different locations: {1}:{2}", passName, existingFile, existingLine) + string.Format(" and {0}:{1}. Jumping to source from Render Graph Viewer will only work correctly for {2}:{3}.", new object[] { file, line, existingFile, existingLine }));
				}
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000359AB File Offset: 0x00033BAB
		[Conditional("UNITY_EDITOR")]
		private void ClearPassDebugMetadata()
		{
			RenderGraph.DebugData.s_PassScriptMetadata.Clear();
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000359B7 File Offset: 0x00033BB7
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000359BF File Offset: 0x00033BBF
		public bool nativeRenderPassesEnabled { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000359C8 File Offset: 0x00033BC8
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x000359D0 File Offset: 0x00033BD0
		public string name { get; private set; } = "RenderGraph";

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000359D9 File Offset: 0x00033BD9
		internal void RequestCaptureDebugData(string executionName)
		{
			this.m_CaptureDebugDataForExecution = executionName;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000359E2 File Offset: 0x00033BE2
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x000359E9 File Offset: 0x00033BE9
		public static bool isRenderGraphViewerActive { get; internal set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000359F1 File Offset: 0x00033BF1
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x000359F8 File Offset: 0x00033BF8
		internal static bool enableValidityChecks { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00035A00 File Offset: 0x00033C00
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.m_DefaultResources;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00035A08 File Offset: 0x00033C08
		public RenderGraph(string name = "RenderGraph")
		{
			this.name = name;
			RenderGraphGlobalSettings renderGraphGlobalSettings;
			if (GraphicsSettings.TryGetRenderPipelineSettings<RenderGraphGlobalSettings>(out renderGraphGlobalSettings))
			{
				this.m_EnableCompilationCaching = renderGraphGlobalSettings.enableCompilationCaching;
				if (this.m_EnableCompilationCaching)
				{
					this.m_CompilationCache = new RenderGraphCompilationCache();
				}
				RenderGraph.enableValidityChecks = renderGraphGlobalSettings.enableValidityChecks;
			}
			else
			{
				RenderGraph.enableValidityChecks = true;
			}
			this.m_TempMRTArrays = new RenderTargetIdentifier[RenderGraph.kMaxMRTCount][];
			for (int i = 0; i < RenderGraph.kMaxMRTCount; i++)
			{
				this.m_TempMRTArrays[i] = new RenderTargetIdentifier[i + 1];
			}
			this.m_Resources = new RenderGraphResourceRegistry(this.m_DebugParameters, this.m_FrameInformationLogger);
			RenderGraph.s_RegisteredGraphs.Add(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphRegistered;
			if (onGraphRegisteredDelegate == null)
			{
				return;
			}
			onGraphRegisteredDelegate(this);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00035B88 File Offset: 0x00033D88
		public void Cleanup()
		{
			this.m_Resources.Cleanup();
			this.m_DefaultResources.Cleanup();
			this.m_RenderGraphPool.Cleanup();
			RenderGraph.s_RegisteredGraphs.Remove(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphUnregistered;
			if (onGraphRegisteredDelegate != null)
			{
				onGraphRegisteredDelegate(this);
			}
			NativePassCompiler nativePassCompiler = this.nativeCompiler;
			if (nativePassCompiler != null)
			{
				CompilerContextData contextData = nativePassCompiler.contextData;
				if (contextData != null)
				{
					contextData.Dispose();
				}
			}
			RenderGraphCompilationCache compilationCache = this.m_CompilationCache;
			if (compilationCache == null)
			{
				return;
			}
			compilationCache.Clear();
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00035BFF File Offset: 0x00033DFF
		internal RenderGraphDebugParams debugParams
		{
			get
			{
				return this.m_DebugParameters;
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00035C07 File Offset: 0x00033E07
		internal List<DebugUI.Widget> GetWidgetList()
		{
			return this.m_DebugParameters.GetWidgetList(this.name);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00035C1A File Offset: 0x00033E1A
		internal bool areAnySettingsActive
		{
			get
			{
				return this.m_DebugParameters.AreAnySettingsActive;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00035C27 File Offset: 0x00033E27
		public void RegisterDebug(DebugUI.Panel panel = null)
		{
			this.m_DebugParameters.RegisterDebug(this.name, panel);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00035C3B File Offset: 0x00033E3B
		public void UnRegisterDebug()
		{
			this.m_DebugParameters.UnRegisterDebug(this.name);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00035C4E File Offset: 0x00033E4E
		public static List<RenderGraph> GetRegisteredRenderGraphs()
		{
			return RenderGraph.s_RegisteredGraphs;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00035C58 File Offset: 0x00033E58
		internal RenderGraph.DebugData GetDebugData(string executionName)
		{
			RenderGraph.DebugData debugData;
			if (this.m_DebugData.TryGetValue(executionName, out debugData))
			{
				return debugData;
			}
			return null;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00035C78 File Offset: 0x00033E78
		public void EndFrame()
		{
			this.m_Resources.PurgeUnusedGraphicsResources();
			if (this.m_DebugParameters.logFrameInformation)
			{
				Debug.Log(this.m_FrameInformationLogger.GetAllLogs());
				this.m_DebugParameters.logFrameInformation = false;
			}
			if (this.m_DebugParameters.logResources)
			{
				this.m_Resources.FlushLogs();
				this.m_DebugParameters.logResources = false;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00035CDD File Offset: 0x00033EDD
		public TextureHandle ImportTexture(RTHandle rt)
		{
			return this.m_Resources.ImportTexture(in rt, false);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00035CED File Offset: 0x00033EED
		public TextureHandle ImportTexture(RTHandle rt, ImportResourceParams importParams)
		{
			return this.m_Resources.ImportTexture(in rt, in importParams, false);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00035CFF File Offset: 0x00033EFF
		public TextureHandle ImportTexture(RTHandle rt, RenderTargetInfo info, ImportResourceParams importParams = default(ImportResourceParams))
		{
			return this.m_Resources.ImportTexture(in rt, info, in importParams);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00035D11 File Offset: 0x00033F11
		internal TextureHandle ImportTexture(RTHandle rt, bool isBuiltin)
		{
			return this.m_Resources.ImportTexture(in rt, isBuiltin);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00035D21 File Offset: 0x00033F21
		public TextureHandle ImportBackbuffer(RenderTargetIdentifier rt, RenderTargetInfo info, ImportResourceParams importParams = default(ImportResourceParams))
		{
			return this.m_Resources.ImportBackbuffer(rt, in info, in importParams);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00035D34 File Offset: 0x00033F34
		public TextureHandle ImportBackbuffer(RenderTargetIdentifier rt)
		{
			RenderTargetInfo dummy = default(RenderTargetInfo);
			dummy.width = (dummy.height = (dummy.volumeDepth = (dummy.msaaSamples = 1)));
			dummy.format = GraphicsFormat.R8G8B8A8_SRGB;
			RenderGraphResourceRegistry resources = this.m_Resources;
			ImportResourceParams importResourceParams = default(ImportResourceParams);
			return resources.ImportBackbuffer(rt, in dummy, in importResourceParams);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00035D8E File Offset: 0x00033F8E
		public TextureHandle CreateTexture(in TextureDesc desc)
		{
			return this.m_Resources.CreateTexture(in desc, -1);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00035D9D File Offset: 0x00033F9D
		public TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease = false)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be created outside of render graph execution.");
			}
			return this.m_Resources.CreateSharedTexture(in desc, explicitRelease);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00035DBF File Offset: 0x00033FBF
		public void RefreshSharedTextureDesc(TextureHandle handle, in TextureDesc desc)
		{
			this.m_Resources.RefreshSharedTextureDesc(in handle, in desc);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00035DCF File Offset: 0x00033FCF
		public void ReleaseSharedTexture(TextureHandle texture)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be release outside of render graph execution.");
			}
			this.m_Resources.ReleaseSharedTexture(in texture);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00035DF4 File Offset: 0x00033FF4
		public TextureHandle CreateTexture(TextureHandle texture)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(in texture.handle, false);
			return resources.CreateTexture(in textureResourceDesc, -1);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00035E23 File Offset: 0x00034023
		public void CreateTextureIfInvalid(in TextureDesc desc, ref TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				texture = this.m_Resources.CreateTexture(in desc, -1);
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00035E40 File Offset: 0x00034040
		public TextureDesc GetTextureDesc(TextureHandle texture)
		{
			return this.m_Resources.GetTextureResourceDesc(in texture.handle, false);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00035E58 File Offset: 0x00034058
		public RenderTargetInfo GetRenderTargetInfo(TextureHandle texture)
		{
			RenderTargetInfo info;
			this.m_Resources.GetRenderTargetInfo(in texture.handle, out info);
			return info;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00035E7A File Offset: 0x0003407A
		public RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			return this.m_Resources.CreateRendererList(in desc);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00035E88 File Offset: 0x00034088
		public RendererListHandle CreateRendererList(in RendererListParams desc)
		{
			return this.m_Resources.CreateRendererList(in desc);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035E96 File Offset: 0x00034096
		public RendererListHandle CreateShadowRendererList(ref ShadowDrawingSettings shadowDrawingSettings)
		{
			return this.m_Resources.CreateShadowRendererList(this.m_RenderGraphContext.renderContext, ref shadowDrawingSettings);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00035EAF File Offset: 0x000340AF
		public RendererListHandle CreateGizmoRendererList(in Camera camera, in GizmoSubset gizmoSubset)
		{
			return this.m_Resources.CreateGizmoRendererList(this.m_RenderGraphContext.renderContext, in camera, in gizmoSubset);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00035ECC File Offset: 0x000340CC
		public RendererListHandle CreateUIOverlayRendererList(in Camera camera)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			ScriptableRenderContext renderContext = this.m_RenderGraphContext.renderContext;
			UISubset uisubset = UISubset.All;
			return resources.CreateUIOverlayRendererList(renderContext, in camera, in uisubset);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00035EF4 File Offset: 0x000340F4
		public RendererListHandle CreateUIOverlayRendererList(in Camera camera, in UISubset uiSubset)
		{
			return this.m_Resources.CreateUIOverlayRendererList(this.m_RenderGraphContext.renderContext, in camera, in uiSubset);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00035F0E File Offset: 0x0003410E
		public RendererListHandle CreateWireOverlayRendererList(in Camera camera)
		{
			return this.m_Resources.CreateWireOverlayRendererList(this.m_RenderGraphContext.renderContext, in camera);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00035F27 File Offset: 0x00034127
		public RendererListHandle CreateSkyboxRendererList(in Camera camera)
		{
			return this.m_Resources.CreateSkyboxRendererList(this.m_RenderGraphContext.renderContext, in camera);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00035F40 File Offset: 0x00034140
		public RendererListHandle CreateSkyboxRendererList(in Camera camera, Matrix4x4 projectionMatrix, Matrix4x4 viewMatrix)
		{
			return this.m_Resources.CreateSkyboxRendererList(this.m_RenderGraphContext.renderContext, in camera, projectionMatrix, viewMatrix);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00035F5B File Offset: 0x0003415B
		public RendererListHandle CreateSkyboxRendererList(in Camera camera, Matrix4x4 projectionMatrixL, Matrix4x4 viewMatrixL, Matrix4x4 projectionMatrixR, Matrix4x4 viewMatrixR)
		{
			return this.m_Resources.CreateSkyboxRendererList(this.m_RenderGraphContext.renderContext, in camera, projectionMatrixL, viewMatrixL, projectionMatrixR, viewMatrixR);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00035F7A File Offset: 0x0003417A
		public BufferHandle ImportBuffer(GraphicsBuffer graphicsBuffer, bool forceRelease = false)
		{
			return this.m_Resources.ImportBuffer(graphicsBuffer, forceRelease);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00035F89 File Offset: 0x00034189
		public BufferHandle CreateBuffer(in BufferDesc desc)
		{
			return this.m_Resources.CreateBuffer(in desc, -1);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00035F98 File Offset: 0x00034198
		public BufferHandle CreateBuffer(in BufferHandle graphicsBuffer)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			BufferDesc bufferResourceDesc = this.m_Resources.GetBufferResourceDesc(in graphicsBuffer.handle, false);
			return resources.CreateBuffer(in bufferResourceDesc, -1);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00035FC6 File Offset: 0x000341C6
		public BufferDesc GetBufferDesc(in BufferHandle graphicsBuffer)
		{
			return this.m_Resources.GetBufferResourceDesc(in graphicsBuffer.handle, false);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00035FDA File Offset: 0x000341DA
		public RayTracingAccelerationStructureHandle ImportRayTracingAccelerationStructure(in RayTracingAccelerationStructure accelStruct, string name = null)
		{
			return this.m_Resources.ImportRayTracingAccelerationStructure(in accelStruct, name);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00035FE9 File Offset: 0x000341E9
		public IRasterRenderGraphBuilder AddRasterRenderPass<PassData>(string passName, out PassData passData, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			return this.AddRasterRenderPass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName), file, line);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00036000 File Offset: 0x00034200
		public IRasterRenderGraphBuilder AddRasterRenderPass<PassData>(string passName, out PassData passData, ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			RasterRenderGraphPass<PassData> renderPass = this.m_RenderGraphPool.Get<RasterRenderGraphPass<PassData>>();
			renderPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, RenderGraphPassType.Raster, sampler);
			passData = renderPass.data;
			this.m_RenderPasses.Add(renderPass);
			this.m_builderInstance.Setup(renderPass, this.m_Resources, this);
			return this.m_builderInstance;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00036069 File Offset: 0x00034269
		public IComputeRenderGraphBuilder AddComputePass<PassData>(string passName, out PassData passData, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			return this.AddComputePass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName), file, line);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00036080 File Offset: 0x00034280
		public IComputeRenderGraphBuilder AddComputePass<PassData>(string passName, out PassData passData, ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			ComputeRenderGraphPass<PassData> renderPass = this.m_RenderGraphPool.Get<ComputeRenderGraphPass<PassData>>();
			renderPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, RenderGraphPassType.Compute, sampler);
			passData = renderPass.data;
			this.m_RenderPasses.Add(renderPass);
			this.m_builderInstance.Setup(renderPass, this.m_Resources, this);
			return this.m_builderInstance;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000360E9 File Offset: 0x000342E9
		public IUnsafeRenderGraphBuilder AddUnsafePass<PassData>(string passName, out PassData passData, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			return this.AddUnsafePass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName), file, line);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00036100 File Offset: 0x00034300
		public IUnsafeRenderGraphBuilder AddUnsafePass<PassData>(string passName, out PassData passData, ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			UnsafeRenderGraphPass<PassData> renderPass = this.m_RenderGraphPool.Get<UnsafeRenderGraphPass<PassData>>();
			renderPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, RenderGraphPassType.Unsafe, sampler);
			renderPass.AllowGlobalState(true);
			passData = renderPass.data;
			this.m_RenderPasses.Add(renderPass);
			this.m_builderInstance.Setup(renderPass, this.m_Resources, this);
			return this.m_builderInstance;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00036170 File Offset: 0x00034370
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData, ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			RenderGraphPass<PassData> renderPass = this.m_RenderGraphPool.Get<RenderGraphPass<PassData>>();
			renderPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, RenderGraphPassType.Legacy, sampler);
			renderPass.AllowGlobalState(true);
			passData = renderPass.data;
			this.m_RenderPasses.Add(renderPass);
			return new RenderGraphBuilder(renderPass, this.m_Resources, this);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000361D4 File Offset: 0x000343D4
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) where PassData : class, new()
		{
			return this.AddRenderPass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName), file, line);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000361E8 File Offset: 0x000343E8
		public void BeginRecording(in RenderGraphParameters parameters)
		{
			this.m_CurrentFrameIndex = parameters.currentFrameIndex;
			this.m_CurrentExecutionName = ((parameters.executionName != null) ? parameters.executionName : "RenderGraphExecution");
			this.m_HasRenderGraphBegun = true;
			this.m_RendererListCulling = parameters.rendererListCulling && !this.m_EnableCompilationCaching;
			RenderGraphResourceRegistry resources = this.m_Resources;
			int executionCount = this.m_ExecutionCount;
			this.m_ExecutionCount = executionCount + 1;
			resources.BeginRenderGraph(executionCount);
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.Initialize(this.m_CurrentExecutionName);
			}
			this.m_DefaultResources.InitializeForRendering(this);
			this.m_RenderGraphContext.cmd = parameters.commandBuffer;
			this.m_RenderGraphContext.renderContext = parameters.scriptableRenderContext;
			this.m_RenderGraphContext.contextlessTesting = parameters.invalidContextForTesting;
			this.m_RenderGraphContext.renderGraphPool = this.m_RenderGraphPool;
			this.m_RenderGraphContext.defaultResources = this.m_DefaultResources;
			if (this.m_DebugParameters.immediateMode)
			{
				this.UpdateCurrentCompiledGraph(-1, true);
				this.LogFrameInformation();
				this.m_CurrentCompiledGraph.compiledPassInfos.Resize(this.m_CurrentCompiledGraph.compiledPassInfos.capacity, false);
				this.m_CurrentImmediatePassIndex = 0;
				for (int i = 0; i < 3; i++)
				{
					if (this.m_ImmediateModeResourceList[i] == null)
					{
						this.m_ImmediateModeResourceList[i] = new List<int>();
					}
					this.m_ImmediateModeResourceList[i].Clear();
				}
				this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003635D File Offset: 0x0003455D
		public void EndRecordingAndExecute()
		{
			this.Execute();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00036368 File Offset: 0x00034568
		internal void Execute()
		{
			this.m_ExecutionExceptionWasRaised = false;
			try
			{
				if (!this.m_DebugParameters.immediateMode)
				{
					this.LogFrameInformation();
					int graphHash = 0;
					if (this.m_EnableCompilationCaching)
					{
						graphHash = this.ComputeGraphHash();
					}
					if (this.nativeRenderPassesEnabled)
					{
						this.CompileNativeRenderGraph(graphHash);
					}
					else
					{
						this.CompileRenderGraph(graphHash);
					}
					this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
					if (this.nativeRenderPassesEnabled)
					{
						this.ExecuteNativeRenderGraph();
					}
					else
					{
						this.ExecuteRenderGraph();
					}
					this.ClearGlobalBindings();
				}
			}
			catch (Exception e)
			{
				if (this.m_RenderGraphContext.contextlessTesting)
				{
					throw;
				}
				Debug.LogError("Render Graph Execution error");
				if (!this.m_ExecutionExceptionWasRaised)
				{
					Debug.LogException(e);
				}
				this.m_ExecutionExceptionWasRaised = true;
			}
			finally
			{
				if (this.m_DebugParameters.immediateMode)
				{
					this.ReleaseImmediateModeResources();
				}
				this.ClearCompiledGraph(this.m_CurrentCompiledGraph, this.m_EnableCompilationCaching);
				this.m_Resources.EndExecute();
				this.InvalidateContext();
				this.m_HasRenderGraphBegun = false;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00036474 File Offset: 0x00034674
		public void BeginProfilingSampler(ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (sampler == null)
			{
				return;
			}
			RenderGraph.ProfilingScopePassData passData;
			using (RenderGraphBuilder builder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("BeginProfile", out passData, null, file, line))
			{
				passData.sampler = sampler;
				builder.AllowPassCulling(false);
				builder.GenerateDebugData(false);
				builder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.Begin(ctx.cmd);
				});
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x000364F4 File Offset: 0x000346F4
		public void EndProfilingSampler(ProfilingSampler sampler, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (sampler == null)
			{
				return;
			}
			RenderGraph.ProfilingScopePassData passData;
			using (RenderGraphBuilder builder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("EndProfile", out passData, null, file, line))
			{
				passData.sampler = sampler;
				builder.AllowPassCulling(false);
				builder.GenerateDebugData(false);
				builder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.End(ctx.cmd);
				});
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00036574 File Offset: 0x00034774
		internal DynamicArray<RenderGraph.CompiledPassInfo> GetCompiledPassInfos()
		{
			return this.m_CurrentCompiledGraph.compiledPassInfos;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00036581 File Offset: 0x00034781
		internal void ClearCompiledGraph()
		{
			this.ClearCompiledGraph(this.m_CurrentCompiledGraph, false);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00036590 File Offset: 0x00034790
		private void ClearCompiledGraph(RenderGraph.CompiledGraph compiledGraph, bool useCompilationCaching)
		{
			this.ClearRenderPasses();
			this.m_Resources.Clear(this.m_ExecutionExceptionWasRaised);
			this.m_RendererLists.Clear();
			this.registeredGlobals.Clear();
			if (!useCompilationCaching && !this.nativeRenderPassesEnabled && compiledGraph != null)
			{
				compiledGraph.Clear();
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000365DE File Offset: 0x000347DE
		private void InvalidateContext()
		{
			this.m_RenderGraphContext.cmd = null;
			this.m_RenderGraphContext.renderGraphPool = null;
			this.m_RenderGraphContext.defaultResources = null;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00036604 File Offset: 0x00034804
		internal void OnPassAdded(RenderGraphPass pass)
		{
			if (this.m_DebugParameters.immediateMode)
			{
				this.ExecutePassImmediately(pass);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000F20 RID: 3872 RVA: 0x0003661C File Offset: 0x0003481C
		// (remove) Token: 0x06000F21 RID: 3873 RVA: 0x00036650 File Offset: 0x00034850
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphRegistered;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000F22 RID: 3874 RVA: 0x00036684 File Offset: 0x00034884
		// (remove) Token: 0x06000F23 RID: 3875 RVA: 0x000366B8 File Offset: 0x000348B8
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000F24 RID: 3876 RVA: 0x000366EC File Offset: 0x000348EC
		// (remove) Token: 0x06000F25 RID: 3877 RVA: 0x00036720 File Offset: 0x00034920
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000F26 RID: 3878 RVA: 0x00036754 File Offset: 0x00034954
		// (remove) Token: 0x06000F27 RID: 3879 RVA: 0x00036788 File Offset: 0x00034988
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000F28 RID: 3880 RVA: 0x000367BC File Offset: 0x000349BC
		// (remove) Token: 0x06000F29 RID: 3881 RVA: 0x000367F0 File Offset: 0x000349F0
		internal static event Action onDebugDataCaptured;

		// Token: 0x06000F2A RID: 3882 RVA: 0x00036824 File Offset: 0x00034A24
		internal int ComputeGraphHash()
		{
			int value;
			using (new ProfilingScope(ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.ComputeHashRenderGraph)))
			{
				HashFNV1A32 hash128 = HashFNV1A32.Create();
				for (int i = 0; i < this.m_RenderPasses.Count; i++)
				{
					this.m_RenderPasses[i].ComputeHash(ref hash128, this.m_Resources);
				}
				value = hash128.value;
			}
			return value;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003689C File Offset: 0x00034A9C
		private void CountReferences()
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			DynamicArray<RenderGraph.CompiledResourceInfo>[] compiledResourceInfo = this.m_CurrentCompiledGraph.compiledResourcesInfos;
			for (int passIndex = 0; passIndex < compiledPassInfo.size; passIndex++)
			{
				RenderGraphPass pass = this.m_RenderPasses[passIndex];
				ref RenderGraph.CompiledPassInfo passInfo = ref compiledPassInfo[passIndex];
				for (int type = 0; type < 3; type++)
				{
					foreach (ResourceHandle resource in pass.resourceReadLists[type])
					{
						ref RenderGraph.CompiledResourceInfo ptr = ref compiledResourceInfo[type][resource.index];
						ptr.imported = this.m_Resources.IsRenderGraphResourceImported(in resource);
						ptr.consumers.Add(passIndex);
						ptr.refCount++;
					}
					foreach (ResourceHandle resource2 in pass.resourceWriteLists[type])
					{
						ref RenderGraph.CompiledResourceInfo info = ref compiledResourceInfo[type][resource2.index];
						info.imported = this.m_Resources.IsRenderGraphResourceImported(in resource2);
						info.producers.Add(passIndex);
						passInfo.hasSideEffect = info.imported;
						passInfo.refCount++;
					}
					foreach (ResourceHandle resource3 in pass.transientResourceList[type])
					{
						ref RenderGraph.CompiledResourceInfo ptr2 = ref compiledResourceInfo[type][resource3.index];
						ptr2.refCount++;
						ptr2.consumers.Add(passIndex);
						ptr2.producers.Add(passIndex);
					}
				}
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00036A80 File Offset: 0x00034C80
		private unsafe void CullUnusedPasses()
		{
			if (this.m_DebugParameters.disablePassCulling)
			{
				if (this.m_DebugParameters.enableLogging)
				{
					this.m_FrameInformationLogger.LogLine("- Pass Culling Disabled -\n", Array.Empty<object>());
				}
				return;
			}
			for (int type = 0; type < 3; type++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> resourceUsageList = this.m_CurrentCompiledGraph.compiledResourcesInfos[type];
				this.m_CullingStack.Clear();
				for (int i = 1; i < resourceUsageList.size; i++)
				{
					if (resourceUsageList[i].refCount == 0)
					{
						this.m_CullingStack.Push(i);
					}
				}
				while (this.m_CullingStack.Count != 0)
				{
					foreach (int producerIndex in resourceUsageList[this.m_CullingStack.Pop()]->producers)
					{
						ref RenderGraph.CompiledPassInfo producerInfo = ref this.m_CurrentCompiledGraph.compiledPassInfos[producerIndex];
						RenderGraphPass producerPass = this.m_RenderPasses[producerIndex];
						producerInfo.refCount--;
						if (producerInfo.refCount == 0 && !producerInfo.hasSideEffect && producerInfo.allowPassCulling)
						{
							producerInfo.culled = true;
							foreach (ResourceHandle resource in producerPass.resourceReadLists[type])
							{
								ref RenderGraph.CompiledResourceInfo ptr = ref resourceUsageList[resource.index];
								ptr.refCount--;
								if (ptr.refCount == 0)
								{
									this.m_CullingStack.Push(resource.index);
								}
							}
						}
					}
				}
			}
			this.LogCulledPasses();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00036C4C File Offset: 0x00034E4C
		private void UpdatePassSynchronization(ref RenderGraph.CompiledPassInfo currentPassInfo, ref RenderGraph.CompiledPassInfo producerPassInfo, int currentPassIndex, int lastProducer, ref int intLastSyncIndex)
		{
			currentPassInfo.syncToPassIndex = lastProducer;
			intLastSyncIndex = lastProducer;
			producerPassInfo.needGraphicsFence = true;
			if (producerPassInfo.syncFromPassIndex == -1)
			{
				producerPassInfo.syncFromPassIndex = currentPassIndex;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00036C74 File Offset: 0x00034E74
		private void UpdateResourceSynchronization(ref int lastGraphicsPipeSync, ref int lastComputePipeSync, int currentPassIndex, in RenderGraph.CompiledResourceInfo resource)
		{
			int lastProducer = this.GetLatestProducerIndex(currentPassIndex, in resource);
			if (lastProducer != -1)
			{
				DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
				ref RenderGraph.CompiledPassInfo currentPassInfo = ref compiledPassInfo[currentPassIndex];
				if (this.m_CurrentCompiledGraph.compiledPassInfos[lastProducer].enableAsyncCompute != currentPassInfo.enableAsyncCompute)
				{
					if (currentPassInfo.enableAsyncCompute)
					{
						if (lastProducer > lastGraphicsPipeSync)
						{
							this.UpdatePassSynchronization(ref currentPassInfo, compiledPassInfo[lastProducer], currentPassIndex, lastProducer, ref lastGraphicsPipeSync);
							return;
						}
					}
					else if (lastProducer > lastComputePipeSync)
					{
						this.UpdatePassSynchronization(ref currentPassInfo, compiledPassInfo[lastProducer], currentPassIndex, lastProducer, ref lastComputePipeSync);
					}
				}
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00036CF8 File Offset: 0x00034EF8
		private int GetFirstValidConsumerIndex(int passIndex, in RenderGraph.CompiledResourceInfo info)
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			foreach (int consumer in info.consumers)
			{
				if (consumer > passIndex && !compiledPassInfo[consumer].culled)
				{
					return consumer;
				}
			}
			return -1;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00036D6C File Offset: 0x00034F6C
		private int FindTextureProducer(int consumerPass, in RenderGraph.CompiledResourceInfo info, out int index)
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			int previousPass = 0;
			for (index = 0; index < info.producers.Count; index++)
			{
				int currentPass = info.producers[index];
				if (!compiledPassInfo[currentPass].culled)
				{
					return currentPass;
				}
				if (currentPass >= consumerPass)
				{
					return previousPass;
				}
				previousPass = currentPass;
			}
			return previousPass;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00036DC8 File Offset: 0x00034FC8
		private unsafe int GetLatestProducerIndex(int passIndex, in RenderGraph.CompiledResourceInfo info)
		{
			int result = -1;
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			foreach (int producer in info.producers)
			{
				RenderGraph.CompiledPassInfo producerPassInfo = *compiledPassInfo[producer];
				if (producer >= passIndex || producerPassInfo.culled || producerPassInfo.culledByRendererList)
				{
					return result;
				}
				result = producer;
			}
			return result;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00036E54 File Offset: 0x00035054
		private int GetLatestValidReadIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.consumers.Count == 0)
			{
				return -1;
			}
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			List<int> consumers = info.consumers;
			for (int i = consumers.Count - 1; i >= 0; i--)
			{
				if (!compiledPassInfo[consumers[i]].culled)
				{
					return consumers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00036EB4 File Offset: 0x000350B4
		private int GetFirstValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			List<int> producers = info.producers;
			for (int i = 0; i < producers.Count; i++)
			{
				if (!compiledPassInfo[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00036F14 File Offset: 0x00035114
		private int GetLatestValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			List<int> producers = info.producers;
			for (int i = producers.Count - 1; i >= 0; i--)
			{
				if (!compiledPassInfo[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00036F74 File Offset: 0x00035174
		private void CreateRendererLists()
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			for (int passIndex = 0; passIndex < compiledPassInfo.size; passIndex++)
			{
				ref RenderGraph.CompiledPassInfo passInfo = ref compiledPassInfo[passIndex];
				if (!passInfo.culled)
				{
					this.m_RendererLists.AddRange(this.m_RenderPasses[passInfo.index].usedRendererListList);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, this.m_RendererListCulling);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00036FF4 File Offset: 0x000351F4
		internal bool GetImportedFallback(TextureDesc desc, out TextureHandle fallback)
		{
			fallback = TextureHandle.nullHandle;
			if (!desc.bindTextureMS)
			{
				if (desc.depthBufferBits != DepthBits.None)
				{
					fallback = this.defaultResources.whiteTexture;
				}
				else if (desc.clearColor == Color.black || desc.clearColor == default(Color))
				{
					if (desc.dimension == TextureXR.dimension)
					{
						fallback = this.defaultResources.blackTextureXR;
					}
					else if (desc.dimension == TextureDimension.Tex3D)
					{
						fallback = this.defaultResources.blackTexture3DXR;
					}
					else if (desc.dimension == TextureDimension.Tex2D)
					{
						fallback = this.defaultResources.blackTexture;
					}
				}
				else if (desc.clearColor == Color.white)
				{
					if (desc.dimension == TextureXR.dimension)
					{
						fallback = this.defaultResources.whiteTextureXR;
					}
					else if (desc.dimension == TextureDimension.Tex2D)
					{
						fallback = this.defaultResources.whiteTexture;
					}
				}
			}
			return fallback.IsValid();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003710C File Offset: 0x0003530C
		private void AllocateCulledPassResources(ref RenderGraph.CompiledPassInfo passInfo)
		{
			int passIndex = passInfo.index;
			RenderGraphPass pass = this.m_RenderPasses[passIndex];
			for (int type = 0; type < 3; type++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> resourcesInfo = this.m_CurrentCompiledGraph.compiledResourcesInfos[type];
				foreach (ResourceHandle resourceHandle in pass.resourceWriteLists[type])
				{
					ref RenderGraph.CompiledResourceInfo compiledResource = ref resourcesInfo[resourceHandle.index];
					int consumerPass = this.GetFirstValidConsumerIndex(passIndex, in compiledResource);
					int index;
					int producerPass = this.FindTextureProducer(consumerPass, in compiledResource, out index);
					if (consumerPass != -1 && passIndex == producerPass)
					{
						if (type == 0)
						{
							TextureResource textureResource = this.m_Resources.GetTextureResource(in resourceHandle);
							TextureHandle fallback;
							if (!textureResource.desc.disableFallBackToImportedTexture && this.GetImportedFallback(textureResource.desc, out fallback))
							{
								compiledResource.imported = true;
								textureResource.imported = true;
								textureResource.graphicsResource = this.m_Resources.GetTexture(in fallback);
								continue;
							}
							textureResource.desc.sizeMode = TextureSizeMode.Explicit;
							textureResource.desc.width = 1;
							textureResource.desc.height = 1;
							textureResource.desc.clearBuffer = true;
						}
						compiledResource.producers[index - 1] = consumerPass;
					}
				}
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00037270 File Offset: 0x00035470
		private unsafe void UpdateResourceAllocationAndSynchronization()
		{
			int lastGraphicsPipeSync = -1;
			int lastComputePipeSync = -1;
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			DynamicArray<RenderGraph.CompiledResourceInfo>[] compiledResourceInfo = this.m_CurrentCompiledGraph.compiledResourcesInfos;
			for (int passIndex = 0; passIndex < compiledPassInfo.size; passIndex++)
			{
				ref RenderGraph.CompiledPassInfo passInfo = ref compiledPassInfo[passIndex];
				if (passInfo.culledByRendererList)
				{
					this.AllocateCulledPassResources(ref passInfo);
				}
				if (!passInfo.culled)
				{
					RenderGraphPass pass = this.m_RenderPasses[passInfo.index];
					for (int type = 0; type < 3; type++)
					{
						DynamicArray<RenderGraph.CompiledResourceInfo> resourcesInfo = compiledResourceInfo[type];
						foreach (ResourceHandle resource in pass.resourceReadLists[type])
						{
							this.UpdateResourceSynchronization(ref lastGraphicsPipeSync, ref lastComputePipeSync, passIndex, resourcesInfo[resource.index]);
						}
						foreach (ResourceHandle resource2 in pass.resourceWriteLists[type])
						{
							this.UpdateResourceSynchronization(ref lastGraphicsPipeSync, ref lastComputePipeSync, passIndex, resourcesInfo[resource2.index]);
						}
					}
				}
			}
			for (int type2 = 0; type2 < 3; type2++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> resourceInfos = compiledResourceInfo[type2];
				for (int i = 1; i < resourceInfos.size; i++)
				{
					RenderGraph.CompiledResourceInfo resourceInfo = *resourceInfos[i];
					bool sharedResource = this.m_Resources.IsRenderGraphResourceShared((RenderGraphResourceType)type2, i);
					bool forceRelease = this.m_Resources.IsRenderGraphResourceForceReleased((RenderGraphResourceType)type2, i);
					if (!resourceInfo.imported || sharedResource || forceRelease)
					{
						int firstWriteIndex = this.GetFirstValidWriteIndex(in resourceInfo);
						if (firstWriteIndex != -1)
						{
							compiledPassInfo[firstWriteIndex].resourceCreateList[type2].Add(i);
						}
						int latestValidReadIndex = this.GetLatestValidReadIndex(in resourceInfo);
						int latestValidWriteIndex = this.GetLatestValidWriteIndex(in resourceInfo);
						int lastReadPassIndex = ((firstWriteIndex != -1 || resourceInfo.imported) ? Math.Max(latestValidWriteIndex, latestValidReadIndex) : (-1));
						if (lastReadPassIndex != -1)
						{
							if (compiledPassInfo[lastReadPassIndex].enableAsyncCompute)
							{
								int currentPassIndex = lastReadPassIndex;
								int firstWaitingPassIndex = compiledPassInfo[currentPassIndex].syncFromPassIndex;
								while (firstWaitingPassIndex == -1 && currentPassIndex++ < compiledPassInfo.size - 1)
								{
									if (compiledPassInfo[currentPassIndex].enableAsyncCompute)
									{
										firstWaitingPassIndex = compiledPassInfo[currentPassIndex].syncFromPassIndex;
									}
								}
								if (currentPassIndex == compiledPassInfo.size)
								{
									if (!compiledPassInfo[lastReadPassIndex].hasSideEffect)
									{
										RenderGraphPass invalidPass = this.m_RenderPasses[lastReadPassIndex];
										string resName = "<unknown>";
										throw new InvalidOperationException(string.Format("{0} resource '{1}' in asynchronous pass '{2}' is missing synchronization on the graphics pipeline.", (RenderGraphResourceType)type2, resName, invalidPass.name));
									}
									firstWaitingPassIndex = currentPassIndex;
								}
								int releasePassIndex = Math.Max(0, firstWaitingPassIndex - 1);
								while (compiledPassInfo[releasePassIndex].culled)
								{
									releasePassIndex = Math.Max(0, releasePassIndex - 1);
								}
								compiledPassInfo[releasePassIndex].resourceReleaseList[type2].Add(i);
							}
							else
							{
								compiledPassInfo[lastReadPassIndex].resourceReleaseList[type2].Add(i);
							}
						}
						if (sharedResource && (firstWriteIndex != -1 || lastReadPassIndex != -1))
						{
							this.m_Resources.UpdateSharedResourceLastFrameIndex(type2, i);
						}
					}
				}
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000375CC File Offset: 0x000357CC
		private unsafe void UpdateAllSharedResourceLastFrameIndex()
		{
			for (int type = 0; type < 3; type++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> resourceInfos = this.m_CurrentCompiledGraph.compiledResourcesInfos[type];
				int sharedResourceCount = this.m_Resources.GetSharedResourceCount((RenderGraphResourceType)type);
				for (int i = 1; i <= sharedResourceCount; i++)
				{
					RenderGraph.CompiledResourceInfo resourceInfo = *resourceInfos[i];
					int latestValidReadIndex = this.GetLatestValidReadIndex(in resourceInfo);
					if (this.GetFirstValidWriteIndex(in resourceInfo) != -1 || latestValidReadIndex != -1)
					{
						this.m_Resources.UpdateSharedResourceLastFrameIndex(type, i);
					}
				}
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00037644 File Offset: 0x00035844
		private bool AreRendererListsEmpty(List<RendererListHandle> rendererLists)
		{
			foreach (RendererListHandle handle in rendererLists)
			{
				RendererList rendererList = this.m_Resources.GetRendererList(in handle);
				if (this.m_RenderGraphContext.renderContext.QueryRendererListStatus(rendererList) == RendererListStatus.kRendererListPopulated)
				{
					return false;
				}
			}
			return rendererLists.Count > 0;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000376C0 File Offset: 0x000358C0
		private void TryCullPassAtIndex(int passIndex)
		{
			ref RenderGraph.CompiledPassInfo compiledPass = ref this.m_CurrentCompiledGraph.compiledPassInfos[passIndex];
			RenderGraphPass pass = this.m_RenderPasses[passIndex];
			if (!compiledPass.culled && pass.allowPassCulling && pass.allowRendererListCulling && !compiledPass.hasSideEffect && this.AreRendererListsEmpty(pass.usedRendererListList))
			{
				compiledPass.culled = (compiledPass.culledByRendererList = true);
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003772C File Offset: 0x0003592C
		private unsafe void CullRendererLists()
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			for (int passIndex = 0; passIndex < compiledPassInfo.size; passIndex++)
			{
				RenderGraph.CompiledPassInfo compiledPass = *compiledPassInfo[passIndex];
				if (!compiledPass.culled && !compiledPass.hasSideEffect && this.m_RenderPasses[passIndex].usedRendererListList.Count > 0)
				{
					this.TryCullPassAtIndex(passIndex);
				}
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00037794 File Offset: 0x00035994
		private bool UpdateCurrentCompiledGraph(int graphHash, bool forceNoCaching = false)
		{
			bool cached = false;
			if (this.m_EnableCompilationCaching && !forceNoCaching)
			{
				cached = this.m_CompilationCache.GetCompilationCache(graphHash, this.m_ExecutionCount, out this.m_CurrentCompiledGraph);
			}
			else
			{
				this.m_CurrentCompiledGraph = this.m_DefaultCompiledGraph;
			}
			return cached;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000377D8 File Offset: 0x000359D8
		internal void CompileRenderGraph(int graphHash)
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.CompileRenderGraph)))
			{
				bool flag = this.UpdateCurrentCompiledGraph(graphHash, false);
				if (!flag)
				{
					this.m_CurrentCompiledGraph.Clear();
					this.m_CurrentCompiledGraph.InitializeCompilationData(this.m_RenderPasses, this.m_Resources);
					this.CountReferences();
					this.CullUnusedPasses();
				}
				this.CreateRendererLists();
				if (!flag)
				{
					if (this.m_RendererListCulling)
					{
						this.CullRendererLists();
					}
					this.UpdateResourceAllocationAndSynchronization();
				}
				else
				{
					this.UpdateAllSharedResourceLastFrameIndex();
				}
				this.LogRendererListsCreation();
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00037880 File Offset: 0x00035A80
		private ref RenderGraph.CompiledPassInfo CompilePassImmediatly(RenderGraphPass pass)
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			if (this.m_CurrentImmediatePassIndex >= compiledPassInfo.size)
			{
				compiledPassInfo.Resize(compiledPassInfo.size * 2, false);
			}
			DynamicArray<RenderGraph.CompiledPassInfo> dynamicArray = compiledPassInfo;
			int currentImmediatePassIndex = this.m_CurrentImmediatePassIndex;
			this.m_CurrentImmediatePassIndex = currentImmediatePassIndex + 1;
			ref RenderGraph.CompiledPassInfo passInfo = ref dynamicArray[currentImmediatePassIndex];
			passInfo.Reset(pass, this.m_CurrentImmediatePassIndex - 1);
			passInfo.enableAsyncCompute = false;
			for (int iType = 0; iType < 3; iType++)
			{
				foreach (ResourceHandle res in pass.transientResourceList[iType])
				{
					passInfo.resourceCreateList[iType].Add(res.index);
					passInfo.resourceReleaseList[iType].Add(res.index);
				}
				foreach (ResourceHandle res2 in pass.resourceWriteLists[iType])
				{
					if (!pass.transientResourceList[iType].Contains(res2) && !this.m_Resources.IsGraphicsResourceCreated(in res2))
					{
						passInfo.resourceCreateList[iType].Add(res2.index);
						this.m_ImmediateModeResourceList[iType].Add(res2.index);
					}
				}
				foreach (ResourceHandle resourceHandle in pass.resourceReadLists[iType])
				{
				}
			}
			foreach (RendererListHandle rl in pass.usedRendererListList)
			{
				if (!this.m_Resources.IsRendererListCreated(in rl))
				{
					this.m_RendererLists.Add(rl);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, false);
			this.m_RendererLists.Clear();
			return ref passInfo;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00037AA8 File Offset: 0x00035CA8
		private void ExecutePassImmediately(RenderGraphPass pass)
		{
			this.ExecuteCompiledPass(this.CompilePassImmediatly(pass));
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00037AB8 File Offset: 0x00035CB8
		private void ExecuteCompiledPass(ref RenderGraph.CompiledPassInfo passInfo)
		{
			if (passInfo.culled)
			{
				return;
			}
			RenderGraphPass pass = this.m_RenderPasses[passInfo.index];
			if (!pass.HasRenderFunc())
			{
				throw new InvalidOperationException("RenderPass " + pass.name + " was not provided with an execute function.");
			}
			try
			{
				using (new ProfilingScope(this.m_RenderGraphContext.cmd, pass.customSampler))
				{
					this.LogRenderPassBegin(in passInfo);
					using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
					{
						this.m_RenderGraphContext.executingPass = pass;
						this.PreRenderPassExecute(in passInfo, pass, this.m_RenderGraphContext);
						pass.Execute(this.m_RenderGraphContext);
						this.PostRenderPassExecute(ref passInfo, pass, this.m_RenderGraphContext);
					}
				}
			}
			catch (Exception e)
			{
				if (!this.m_RenderGraphContext.contextlessTesting)
				{
					this.m_ExecutionExceptionWasRaised = true;
					Debug.LogError(string.Format("Render Graph execution error at pass '{0}' ({1})", pass.name, passInfo.index));
					Debug.LogException(e);
				}
				throw;
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00037BEC File Offset: 0x00035DEC
		private void ExecuteRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.ExecuteRenderGraph)))
			{
				DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
				for (int passIndex = 0; passIndex < compiledPassInfo.size; passIndex++)
				{
					this.ExecuteCompiledPass(compiledPassInfo[passIndex]);
				}
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00037C5C File Offset: 0x00035E5C
		private void PreRenderPassSetRenderTargets(in RenderGraph.CompiledPassInfo passInfo, RenderGraphPass pass, InternalRenderGraphContext rgContext)
		{
			TextureAccess textureAccess = pass.depthAccess;
			bool depthBufferIsValid = textureAccess.textureHandle.IsValid();
			if (!depthBufferIsValid && pass.colorBufferMaxIndex == -1)
			{
				return;
			}
			TextureAccess[] colorBufferAccess = pass.colorBufferAccess;
			if (pass.colorBufferMaxIndex > 0)
			{
				RenderTargetIdentifier[] mrtArray = this.m_TempMRTArrays[pass.colorBufferMaxIndex];
				for (int i = 0; i <= pass.colorBufferMaxIndex; i++)
				{
					mrtArray[i] = this.m_Resources.GetTexture(in colorBufferAccess[i].textureHandle);
				}
				if (depthBufferIsValid)
				{
					CommandBuffer cmd = rgContext.cmd;
					RenderTargetIdentifier[] array = mrtArray;
					RenderGraphResourceRegistry resources = this.m_Resources;
					textureAccess = pass.depthAccess;
					CoreUtils.SetRenderTarget(cmd, array, resources.GetTexture(in textureAccess.textureHandle));
					return;
				}
				throw new InvalidOperationException("Setting MRTs without a depth buffer is not supported.");
			}
			else if (depthBufferIsValid)
			{
				if (pass.colorBufferMaxIndex > -1)
				{
					CommandBuffer cmd2 = rgContext.cmd;
					RTHandle texture = this.m_Resources.GetTexture(in pass.colorBufferAccess[0].textureHandle);
					RenderGraphResourceRegistry resources2 = this.m_Resources;
					textureAccess = pass.depthAccess;
					CoreUtils.SetRenderTarget(cmd2, texture, resources2.GetTexture(in textureAccess.textureHandle), 0, CubemapFace.Unknown, -1);
					return;
				}
				CommandBuffer cmd3 = rgContext.cmd;
				RenderGraphResourceRegistry resources3 = this.m_Resources;
				textureAccess = pass.depthAccess;
				CoreUtils.SetRenderTarget(cmd3, resources3.GetTexture(in textureAccess.textureHandle), ClearFlag.None, 0, CubemapFace.Unknown, -1);
				return;
			}
			else
			{
				if (pass.colorBufferAccess[0].textureHandle.IsValid())
				{
					CoreUtils.SetRenderTarget(rgContext.cmd, this.m_Resources.GetTexture(in pass.colorBufferAccess[0].textureHandle), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					return;
				}
				throw new InvalidOperationException("Neither Depth nor color render targets are correctly setup at pass " + pass.name + ".");
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00037DF0 File Offset: 0x00035FF0
		private void PreRenderPassExecute(in RenderGraph.CompiledPassInfo passInfo, RenderGraphPass pass, InternalRenderGraphContext rgContext)
		{
			this.m_PreviousCommandBuffer = rgContext.cmd;
			bool executedWorkDuringResourceCreation = false;
			for (int type = 0; type < 3; type++)
			{
				foreach (int resource in passInfo.resourceCreateList[type])
				{
					executedWorkDuringResourceCreation |= this.m_Resources.CreatePooledResource(rgContext, type, resource);
				}
			}
			if (passInfo.enableFoveatedRasterization)
			{
				rgContext.cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
			}
			this.PreRenderPassSetRenderTargets(in passInfo, pass, rgContext);
			if (passInfo.enableAsyncCompute)
			{
				GraphicsFence previousFence = default(GraphicsFence);
				if (executedWorkDuringResourceCreation)
				{
					previousFence = rgContext.cmd.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.AllGPUOperations);
				}
				if (!rgContext.contextlessTesting)
				{
					rgContext.renderContext.ExecuteCommandBuffer(rgContext.cmd);
				}
				rgContext.cmd.Clear();
				CommandBuffer asyncCmd = CommandBufferPool.Get(pass.name);
				asyncCmd.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
				rgContext.cmd = asyncCmd;
				if (executedWorkDuringResourceCreation)
				{
					rgContext.cmd.WaitOnAsyncGraphicsFence(previousFence);
				}
			}
			if (passInfo.syncToPassIndex != -1)
			{
				rgContext.cmd.WaitOnAsyncGraphicsFence(this.m_CurrentCompiledGraph.compiledPassInfos[passInfo.syncToPassIndex].fence);
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00037F28 File Offset: 0x00036128
		private void PostRenderPassExecute(ref RenderGraph.CompiledPassInfo passInfo, RenderGraphPass pass, InternalRenderGraphContext rgContext)
		{
			foreach (ValueTuple<TextureHandle, int> tex in pass.setGlobalsList)
			{
				rgContext.cmd.SetGlobalTexture(tex.Item2, tex.Item1);
			}
			if (passInfo.needGraphicsFence)
			{
				passInfo.fence = rgContext.cmd.CreateAsyncGraphicsFence();
			}
			if (passInfo.enableAsyncCompute)
			{
				rgContext.renderContext.ExecuteCommandBufferAsync(rgContext.cmd, ComputeQueueType.Background);
				CommandBufferPool.Release(rgContext.cmd);
				rgContext.cmd = this.m_PreviousCommandBuffer;
			}
			if (passInfo.enableFoveatedRasterization)
			{
				rgContext.cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
			this.m_RenderGraphPool.ReleaseAllTempAlloc();
			for (int type = 0; type < 3; type++)
			{
				foreach (int resource in passInfo.resourceReleaseList[type])
				{
					this.m_Resources.ReleasePooledResource(rgContext, type, resource);
				}
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00038054 File Offset: 0x00036254
		private void ClearRenderPasses()
		{
			foreach (RenderGraphPass renderGraphPass in this.m_RenderPasses)
			{
				renderGraphPass.Release(this.m_RenderGraphPool);
			}
			this.m_RenderPasses.Clear();
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000380B8 File Offset: 0x000362B8
		private void ReleaseImmediateModeResources()
		{
			for (int type = 0; type < 3; type++)
			{
				foreach (int resource in this.m_ImmediateModeResourceList[type])
				{
					this.m_Resources.ReleasePooledResource(this.m_RenderGraphContext, type, resource);
				}
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00038128 File Offset: 0x00036328
		private void LogFrameInformation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("==== Staring render graph frame for: " + this.m_CurrentExecutionName + " ====", Array.Empty<object>());
				if (!this.m_DebugParameters.immediateMode)
				{
					this.m_FrameInformationLogger.LogLine("Number of passes declared: {0}\n", new object[] { this.m_RenderPasses.Count });
				}
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003819D File Offset: 0x0003639D
		private void LogRendererListsCreation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Number of renderer lists created: {0}\n", new object[] { this.m_RendererLists.Count });
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000381D8 File Offset: 0x000363D8
		private void LogRenderPassBegin(in RenderGraph.CompiledPassInfo passInfo)
		{
			if (this.m_DebugParameters.enableLogging)
			{
				RenderGraphPass pass = this.m_RenderPasses[passInfo.index];
				this.m_FrameInformationLogger.LogLine("[{0}][{1}] \"{2}\"", new object[]
				{
					pass.index,
					pass.enableAsyncCompute ? "Compute" : "Graphics",
					pass.name
				});
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					if (passInfo.syncToPassIndex != -1)
					{
						this.m_FrameInformationLogger.LogLine("Synchronize with [{0}]", new object[] { passInfo.syncToPassIndex });
					}
				}
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000382A8 File Offset: 0x000364A8
		private void LogCulledPasses()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Pass Culling Report:", Array.Empty<object>());
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
					for (int i = 0; i < compiledPassInfo.size; i++)
					{
						if (compiledPassInfo[i].culled)
						{
							RenderGraphPass pass = this.m_RenderPasses[i];
							this.m_FrameInformationLogger.LogLine("[{0}] {1}", new object[] { pass.index, pass.name });
						}
					}
					this.m_FrameInformationLogger.LogLine("\n", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000104E9 File Offset: 0x0000E6E9
		private ProfilingSampler GetDefaultProfilingSampler(string name)
		{
			return null;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00038384 File Offset: 0x00036584
		private void UpdateImportedResourceLifeTime(ref RenderGraph.DebugData.ResourceData data, List<int> passList)
		{
			foreach (int pass in passList)
			{
				if (data.creationPassIndex == -1)
				{
					data.creationPassIndex = pass;
				}
				else
				{
					data.creationPassIndex = Math.Min(data.creationPassIndex, pass);
				}
				if (data.releasePassIndex == -1)
				{
					data.releasePassIndex = pass;
				}
				else
				{
					data.releasePassIndex = Math.Max(data.releasePassIndex, pass);
				}
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00038414 File Offset: 0x00036614
		private void GenerateDebugData()
		{
			if (this.m_ExecutionExceptionWasRaised)
			{
				return;
			}
			if (!RenderGraph.isRenderGraphViewerActive)
			{
				this.CleanupDebugData();
				return;
			}
			RenderGraph.DebugData debugData;
			if (!this.m_DebugData.TryGetValue(this.m_CurrentExecutionName, out debugData))
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionRegistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, this.m_CurrentExecutionName);
				}
				debugData = new RenderGraph.DebugData();
				this.m_DebugData.Add(this.m_CurrentExecutionName, debugData);
				return;
			}
			if (this.m_CaptureDebugDataForExecution == null || !this.m_CaptureDebugDataForExecution.Equals(this.m_CurrentExecutionName))
			{
				return;
			}
			debugData.Clear();
			if (this.nativeRenderPassesEnabled)
			{
				this.nativeCompiler.GenerateNativeCompilerDebugData(ref debugData);
			}
			else
			{
				this.GenerateCompilerDebugData(ref debugData);
			}
			Action action = RenderGraph.onDebugDataCaptured;
			if (action != null)
			{
				action();
			}
			this.m_CaptureDebugDataForExecution = null;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000384D8 File Offset: 0x000366D8
		private void GenerateCompilerDebugData(ref RenderGraph.DebugData debugData)
		{
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfo = this.m_CurrentCompiledGraph.compiledPassInfos;
			DynamicArray<RenderGraph.CompiledResourceInfo>[] compiledResourceInfo = this.m_CurrentCompiledGraph.compiledResourcesInfos;
			for (int type = 0; type < 3; type++)
			{
				for (int i = 0; i < compiledResourceInfo[type].size; i++)
				{
					ref RenderGraph.CompiledResourceInfo resourceInfo = ref compiledResourceInfo[type][i];
					RenderGraph.DebugData.ResourceData newResource = default(RenderGraph.DebugData.ResourceData);
					if (i != 0)
					{
						string resName = this.m_Resources.GetRenderGraphResourceName((RenderGraphResourceType)type, i);
						newResource.name = ((!string.IsNullOrEmpty(resName)) ? resName : "(unnamed)");
						newResource.imported = this.m_Resources.IsRenderGraphResourceImported((RenderGraphResourceType)type, i);
					}
					else
					{
						newResource.name = "<null>";
						newResource.imported = true;
					}
					newResource.creationPassIndex = -1;
					newResource.releasePassIndex = -1;
					RenderGraphResourceType resourceType = (RenderGraphResourceType)type;
					ResourceHandle handle = new ResourceHandle(i, resourceType, false);
					if (i != 0 && handle.IsValid())
					{
						if (resourceType == RenderGraphResourceType.Texture)
						{
							RenderTargetInfo renderTargetInfo;
							this.m_Resources.GetRenderTargetInfo(in handle, out renderTargetInfo);
							newResource.textureData = new RenderGraph.DebugData.TextureResourceData
							{
								width = renderTargetInfo.width,
								height = renderTargetInfo.height,
								depth = renderTargetInfo.volumeDepth,
								samples = renderTargetInfo.msaaSamples,
								format = renderTargetInfo.format
							};
						}
						else if (resourceType == RenderGraphResourceType.Buffer)
						{
							BufferDesc bufferDesc = this.m_Resources.GetBufferResourceDesc(in handle, true);
							newResource.bufferData = new RenderGraph.DebugData.BufferResourceData
							{
								count = bufferDesc.count,
								stride = bufferDesc.stride,
								target = bufferDesc.target,
								usage = bufferDesc.usageFlags
							};
						}
					}
					newResource.consumerList = new List<int>(resourceInfo.consumers);
					newResource.producerList = new List<int>(resourceInfo.producers);
					if (newResource.imported)
					{
						this.UpdateImportedResourceLifeTime(ref newResource, newResource.consumerList);
						this.UpdateImportedResourceLifeTime(ref newResource, newResource.producerList);
					}
					debugData.resourceLists[type].Add(newResource);
				}
			}
			for (int j = 0; j < compiledPassInfo.size; j++)
			{
				ref RenderGraph.CompiledPassInfo passInfo = ref compiledPassInfo[j];
				RenderGraphPass pass = this.m_RenderPasses[passInfo.index];
				RenderGraph.DebugData.PassData newPass = default(RenderGraph.DebugData.PassData);
				newPass.name = pass.name;
				newPass.type = pass.type;
				newPass.culled = passInfo.culled;
				newPass.async = passInfo.enableAsyncCompute;
				newPass.generateDebugData = pass.generateDebugData;
				newPass.resourceReadLists = new List<int>[3];
				newPass.resourceWriteLists = new List<int>[3];
				newPass.syncFromPassIndex = passInfo.syncFromPassIndex;
				newPass.syncToPassIndex = passInfo.syncToPassIndex;
				RenderGraph.DebugData.s_PassScriptMetadata.TryGetValue(pass.name, out newPass.scriptInfo);
				for (int type2 = 0; type2 < 3; type2++)
				{
					newPass.resourceReadLists[type2] = new List<int>();
					newPass.resourceWriteLists[type2] = new List<int>();
					foreach (ResourceHandle resourceRead in pass.resourceReadLists[type2])
					{
						newPass.resourceReadLists[type2].Add(resourceRead.index);
					}
					foreach (ResourceHandle resourceWrite in pass.resourceWriteLists[type2])
					{
						newPass.resourceWriteLists[type2].Add(resourceWrite.index);
					}
					foreach (int resourceCreate in passInfo.resourceCreateList[type2])
					{
						RenderGraph.DebugData.ResourceData res = debugData.resourceLists[type2][resourceCreate];
						if (!res.imported)
						{
							res.creationPassIndex = j;
							debugData.resourceLists[type2][resourceCreate] = res;
						}
					}
					foreach (int resourceRelease in passInfo.resourceReleaseList[type2])
					{
						RenderGraph.DebugData.ResourceData res2 = debugData.resourceLists[type2][resourceRelease];
						if (!res2.imported)
						{
							res2.releasePassIndex = j;
							debugData.resourceLists[type2][resourceRelease] = res2;
						}
					}
				}
				debugData.passList.Add(newPass);
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000389A4 File Offset: 0x00036BA4
		private void CleanupDebugData()
		{
			foreach (KeyValuePair<string, RenderGraph.DebugData> kvp in this.m_DebugData)
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionUnregistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, kvp.Key);
				}
			}
			this.m_DebugData.Clear();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00038A14 File Offset: 0x00036C14
		internal void SetGlobal(TextureHandle h, int globalPropertyId)
		{
			if (!h.IsValid())
			{
				throw new ArgumentException("Attempting to register an invalid texture handle as a global");
			}
			this.registeredGlobals[globalPropertyId] = h;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00038A37 File Offset: 0x00036C37
		internal bool IsGlobal(int globalPropertyId)
		{
			return this.registeredGlobals.ContainsKey(globalPropertyId);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00038A45 File Offset: 0x00036C45
		internal Dictionary<int, TextureHandle>.ValueCollection AllGlobals()
		{
			return this.registeredGlobals.Values;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00038A54 File Offset: 0x00036C54
		internal TextureHandle GetGlobal(int globalPropertyId)
		{
			TextureHandle h;
			this.registeredGlobals.TryGetValue(globalPropertyId, out h);
			return h;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00038A74 File Offset: 0x00036C74
		internal void ClearGlobalBindings()
		{
			foreach (KeyValuePair<int, TextureHandle> globalTex in this.registeredGlobals)
			{
				this.m_RenderGraphContext.cmd.SetGlobalTexture(globalTex.Key, this.defaultResources.blackTexture);
			}
		}

		// Token: 0x0400098A RID: 2442
		private NativePassCompiler nativeCompiler;

		// Token: 0x0400098B RID: 2443
		private readonly string[] k_PassNameDebugIgnoreList = new string[] { "BeginProfile", "EndProfile" };

		// Token: 0x0400098C RID: 2444
		public static readonly int kMaxMRTCount = 8;

		// Token: 0x0400098E RID: 2446
		internal RenderGraphResourceRegistry m_Resources;

		// Token: 0x0400098F RID: 2447
		private RenderGraphObjectPool m_RenderGraphPool = new RenderGraphObjectPool();

		// Token: 0x04000990 RID: 2448
		private RenderGraphBuilders m_builderInstance = new RenderGraphBuilders();

		// Token: 0x04000991 RID: 2449
		internal List<RenderGraphPass> m_RenderPasses = new List<RenderGraphPass>(64);

		// Token: 0x04000992 RID: 2450
		private List<RendererListHandle> m_RendererLists = new List<RendererListHandle>(32);

		// Token: 0x04000993 RID: 2451
		private RenderGraphDebugParams m_DebugParameters = new RenderGraphDebugParams();

		// Token: 0x04000994 RID: 2452
		private RenderGraphLogger m_FrameInformationLogger = new RenderGraphLogger();

		// Token: 0x04000995 RID: 2453
		private RenderGraphDefaultResources m_DefaultResources = new RenderGraphDefaultResources();

		// Token: 0x04000996 RID: 2454
		private Dictionary<int, ProfilingSampler> m_DefaultProfilingSamplers = new Dictionary<int, ProfilingSampler>();

		// Token: 0x04000997 RID: 2455
		private InternalRenderGraphContext m_RenderGraphContext = new InternalRenderGraphContext();

		// Token: 0x04000998 RID: 2456
		private CommandBuffer m_PreviousCommandBuffer;

		// Token: 0x04000999 RID: 2457
		private List<int>[] m_ImmediateModeResourceList = new List<int>[3];

		// Token: 0x0400099A RID: 2458
		private RenderGraphCompilationCache m_CompilationCache;

		// Token: 0x0400099B RID: 2459
		private RenderTargetIdentifier[][] m_TempMRTArrays;

		// Token: 0x0400099C RID: 2460
		private Stack<int> m_CullingStack = new Stack<int>();

		// Token: 0x0400099D RID: 2461
		private string m_CurrentExecutionName;

		// Token: 0x0400099E RID: 2462
		private int m_ExecutionCount;

		// Token: 0x0400099F RID: 2463
		private int m_CurrentFrameIndex;

		// Token: 0x040009A0 RID: 2464
		private int m_CurrentImmediatePassIndex;

		// Token: 0x040009A1 RID: 2465
		private bool m_ExecutionExceptionWasRaised;

		// Token: 0x040009A2 RID: 2466
		private bool m_HasRenderGraphBegun;

		// Token: 0x040009A3 RID: 2467
		private bool m_RendererListCulling;

		// Token: 0x040009A4 RID: 2468
		private bool m_EnableCompilationCaching;

		// Token: 0x040009A5 RID: 2469
		private RenderGraph.CompiledGraph m_DefaultCompiledGraph = new RenderGraph.CompiledGraph();

		// Token: 0x040009A6 RID: 2470
		private RenderGraph.CompiledGraph m_CurrentCompiledGraph;

		// Token: 0x040009A7 RID: 2471
		private string m_CaptureDebugDataForExecution;

		// Token: 0x040009A8 RID: 2472
		private Dictionary<string, RenderGraph.DebugData> m_DebugData = new Dictionary<string, RenderGraph.DebugData>();

		// Token: 0x040009A9 RID: 2473
		private static List<RenderGraph> s_RegisteredGraphs = new List<RenderGraph>();

		// Token: 0x040009AD RID: 2477
		private const string k_BeginProfilingSamplerPassName = "BeginProfile";

		// Token: 0x040009AE RID: 2478
		private const string k_EndProfilingSamplerPassName = "EndProfile";

		// Token: 0x040009B4 RID: 2484
		private Dictionary<int, TextureHandle> registeredGlobals = new Dictionary<int, TextureHandle>();

		// Token: 0x0200022A RID: 554
		internal class DebugData
		{
			// Token: 0x06000F57 RID: 3927 RVA: 0x00038AFC File Offset: 0x00036CFC
			public DebugData()
			{
				for (int i = 0; i < 3; i++)
				{
					this.resourceLists[i] = new List<RenderGraph.DebugData.ResourceData>();
				}
			}

			// Token: 0x06000F58 RID: 3928 RVA: 0x00038B40 File Offset: 0x00036D40
			public void Clear()
			{
				this.passList.Clear();
				for (int i = 0; i < 3; i++)
				{
					this.resourceLists[i].Clear();
				}
			}

			// Token: 0x040009B5 RID: 2485
			public readonly List<RenderGraph.DebugData.PassData> passList = new List<RenderGraph.DebugData.PassData>();

			// Token: 0x040009B6 RID: 2486
			public readonly List<RenderGraph.DebugData.ResourceData>[] resourceLists = new List<RenderGraph.DebugData.ResourceData>[3];

			// Token: 0x040009B7 RID: 2487
			public bool isNRPCompiler;

			// Token: 0x040009B8 RID: 2488
			internal static readonly Dictionary<string, RenderGraph.DebugData.PassScriptInfo> s_PassScriptMetadata = new Dictionary<string, RenderGraph.DebugData.PassScriptInfo>();

			// Token: 0x0200022B RID: 555
			[DebuggerDisplay("PassDebug: {name}")]
			public struct PassData
			{
				// Token: 0x040009B9 RID: 2489
				public string name;

				// Token: 0x040009BA RID: 2490
				public RenderGraphPassType type;

				// Token: 0x040009BB RID: 2491
				public List<int>[] resourceReadLists;

				// Token: 0x040009BC RID: 2492
				public List<int>[] resourceWriteLists;

				// Token: 0x040009BD RID: 2493
				public bool culled;

				// Token: 0x040009BE RID: 2494
				public bool async;

				// Token: 0x040009BF RID: 2495
				public int nativeSubPassIndex;

				// Token: 0x040009C0 RID: 2496
				public int syncToPassIndex;

				// Token: 0x040009C1 RID: 2497
				public int syncFromPassIndex;

				// Token: 0x040009C2 RID: 2498
				public bool generateDebugData;

				// Token: 0x040009C3 RID: 2499
				public RenderGraph.DebugData.PassData.NRPInfo nrpInfo;

				// Token: 0x040009C4 RID: 2500
				public RenderGraph.DebugData.PassScriptInfo scriptInfo;

				// Token: 0x0200022C RID: 556
				public class NRPInfo
				{
					// Token: 0x040009C5 RID: 2501
					public RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo nativePassInfo;

					// Token: 0x040009C6 RID: 2502
					public List<int> textureFBFetchList = new List<int>();

					// Token: 0x040009C7 RID: 2503
					public List<int> setGlobals = new List<int>();

					// Token: 0x040009C8 RID: 2504
					public int width;

					// Token: 0x040009C9 RID: 2505
					public int height;

					// Token: 0x040009CA RID: 2506
					public int volumeDepth;

					// Token: 0x040009CB RID: 2507
					public int samples;

					// Token: 0x040009CC RID: 2508
					public bool hasDepth;

					// Token: 0x0200022D RID: 557
					public class NativeRenderPassInfo
					{
						// Token: 0x040009CD RID: 2509
						public string passBreakReasoning;

						// Token: 0x040009CE RID: 2510
						public List<RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo> attachmentInfos;

						// Token: 0x040009CF RID: 2511
						public Dictionary<int, RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo> passCompatibility;

						// Token: 0x040009D0 RID: 2512
						public List<int> mergedPassIds;

						// Token: 0x0200022E RID: 558
						public class AttachmentInfo
						{
							// Token: 0x040009D1 RID: 2513
							public string resourceName;

							// Token: 0x040009D2 RID: 2514
							public int attachmentIndex;

							// Token: 0x040009D3 RID: 2515
							public string loadAction;

							// Token: 0x040009D4 RID: 2516
							public string loadReason;

							// Token: 0x040009D5 RID: 2517
							public string storeAction;

							// Token: 0x040009D6 RID: 2518
							public string storeReason;

							// Token: 0x040009D7 RID: 2519
							public string storeMsaaReason;
						}

						// Token: 0x0200022F RID: 559
						public struct PassCompatibilityInfo
						{
							// Token: 0x040009D8 RID: 2520
							public string message;

							// Token: 0x040009D9 RID: 2521
							public bool isCompatible;
						}
					}
				}
			}

			// Token: 0x02000230 RID: 560
			public class BufferResourceData
			{
				// Token: 0x040009DA RID: 2522
				public int count;

				// Token: 0x040009DB RID: 2523
				public int stride;

				// Token: 0x040009DC RID: 2524
				public GraphicsBuffer.Target target;

				// Token: 0x040009DD RID: 2525
				public GraphicsBuffer.UsageFlags usage;
			}

			// Token: 0x02000231 RID: 561
			public class TextureResourceData
			{
				// Token: 0x040009DE RID: 2526
				public int width;

				// Token: 0x040009DF RID: 2527
				public int height;

				// Token: 0x040009E0 RID: 2528
				public int depth;

				// Token: 0x040009E1 RID: 2529
				public bool bindMS;

				// Token: 0x040009E2 RID: 2530
				public int samples;

				// Token: 0x040009E3 RID: 2531
				public GraphicsFormat format;

				// Token: 0x040009E4 RID: 2532
				public bool clearBuffer;
			}

			// Token: 0x02000232 RID: 562
			[DebuggerDisplay("ResourceDebug: {name} [{creationPassIndex}:{releasePassIndex}]")]
			public struct ResourceData
			{
				// Token: 0x040009E5 RID: 2533
				public string name;

				// Token: 0x040009E6 RID: 2534
				public bool imported;

				// Token: 0x040009E7 RID: 2535
				public int creationPassIndex;

				// Token: 0x040009E8 RID: 2536
				public int releasePassIndex;

				// Token: 0x040009E9 RID: 2537
				public List<int> consumerList;

				// Token: 0x040009EA RID: 2538
				public List<int> producerList;

				// Token: 0x040009EB RID: 2539
				public bool memoryless;

				// Token: 0x040009EC RID: 2540
				public RenderGraph.DebugData.TextureResourceData textureData;

				// Token: 0x040009ED RID: 2541
				public RenderGraph.DebugData.BufferResourceData bufferData;
			}

			// Token: 0x02000233 RID: 563
			public class PassScriptInfo
			{
				// Token: 0x040009EE RID: 2542
				public string filePath;

				// Token: 0x040009EF RID: 2543
				public int line;
			}
		}

		// Token: 0x02000234 RID: 564
		internal struct CompiledResourceInfo
		{
			// Token: 0x06000F60 RID: 3936 RVA: 0x00038B9C File Offset: 0x00036D9C
			public void Reset()
			{
				if (this.producers == null)
				{
					this.producers = new List<int>();
				}
				if (this.consumers == null)
				{
					this.consumers = new List<int>();
				}
				this.producers.Clear();
				this.consumers.Clear();
				this.refCount = 0;
				this.imported = false;
			}

			// Token: 0x040009F0 RID: 2544
			public List<int> producers;

			// Token: 0x040009F1 RID: 2545
			public List<int> consumers;

			// Token: 0x040009F2 RID: 2546
			public int refCount;

			// Token: 0x040009F3 RID: 2547
			public bool imported;
		}

		// Token: 0x02000235 RID: 565
		[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
		internal struct CompiledPassInfo
		{
			// Token: 0x06000F61 RID: 3937 RVA: 0x00038BF4 File Offset: 0x00036DF4
			public void Reset(RenderGraphPass pass, int index)
			{
				this.name = pass.name;
				this.index = index;
				this.enableAsyncCompute = pass.enableAsyncCompute;
				this.allowPassCulling = pass.allowPassCulling;
				this.enableFoveatedRasterization = pass.enableFoveatedRasterization;
				if (this.resourceCreateList == null)
				{
					this.resourceCreateList = new List<int>[3];
					this.resourceReleaseList = new List<int>[3];
					for (int i = 0; i < 3; i++)
					{
						this.resourceCreateList[i] = new List<int>();
						this.resourceReleaseList[i] = new List<int>();
					}
				}
				for (int j = 0; j < 3; j++)
				{
					this.resourceCreateList[j].Clear();
					this.resourceReleaseList[j].Clear();
				}
				this.refCount = 0;
				this.culled = false;
				this.culledByRendererList = false;
				this.hasSideEffect = false;
				this.syncToPassIndex = -1;
				this.syncFromPassIndex = -1;
				this.needGraphicsFence = false;
			}

			// Token: 0x040009F4 RID: 2548
			public string name;

			// Token: 0x040009F5 RID: 2549
			public int index;

			// Token: 0x040009F6 RID: 2550
			public List<int>[] resourceCreateList;

			// Token: 0x040009F7 RID: 2551
			public List<int>[] resourceReleaseList;

			// Token: 0x040009F8 RID: 2552
			public GraphicsFence fence;

			// Token: 0x040009F9 RID: 2553
			public int refCount;

			// Token: 0x040009FA RID: 2554
			public int syncToPassIndex;

			// Token: 0x040009FB RID: 2555
			public int syncFromPassIndex;

			// Token: 0x040009FC RID: 2556
			public bool enableAsyncCompute;

			// Token: 0x040009FD RID: 2557
			public bool allowPassCulling;

			// Token: 0x040009FE RID: 2558
			public bool needGraphicsFence;

			// Token: 0x040009FF RID: 2559
			public bool culled;

			// Token: 0x04000A00 RID: 2560
			public bool culledByRendererList;

			// Token: 0x04000A01 RID: 2561
			public bool hasSideEffect;

			// Token: 0x04000A02 RID: 2562
			public bool enableFoveatedRasterization;
		}

		// Token: 0x02000236 RID: 566
		internal interface ICompiledGraph
		{
			// Token: 0x06000F62 RID: 3938
			void Clear();
		}

		// Token: 0x02000237 RID: 567
		internal class CompiledGraph : RenderGraph.ICompiledGraph
		{
			// Token: 0x06000F63 RID: 3939 RVA: 0x00038CD8 File Offset: 0x00036ED8
			public CompiledGraph()
			{
				for (int i = 0; i < 3; i++)
				{
					this.compiledResourcesInfos[i] = new DynamicArray<RenderGraph.CompiledResourceInfo>();
				}
			}

			// Token: 0x06000F64 RID: 3940 RVA: 0x00038D1C File Offset: 0x00036F1C
			public void Clear()
			{
				for (int i = 0; i < 3; i++)
				{
					this.compiledResourcesInfos[i].Clear();
				}
				this.compiledPassInfos.Clear();
			}

			// Token: 0x06000F65 RID: 3941 RVA: 0x00038D50 File Offset: 0x00036F50
			private void InitResourceInfosData(DynamicArray<RenderGraph.CompiledResourceInfo> resourceInfos, int count)
			{
				resourceInfos.Resize(count, false);
				for (int i = 0; i < resourceInfos.size; i++)
				{
					resourceInfos[i].Reset();
				}
			}

			// Token: 0x06000F66 RID: 3942 RVA: 0x00038D84 File Offset: 0x00036F84
			public void InitializeCompilationData(List<RenderGraphPass> passes, RenderGraphResourceRegistry resources)
			{
				this.InitResourceInfosData(this.compiledResourcesInfos[0], resources.GetTextureResourceCount());
				this.InitResourceInfosData(this.compiledResourcesInfos[1], resources.GetBufferResourceCount());
				this.InitResourceInfosData(this.compiledResourcesInfos[2], resources.GetRayTracingAccelerationStructureResourceCount());
				this.compiledPassInfos.Resize(passes.Count, false);
				for (int i = 0; i < this.compiledPassInfos.size; i++)
				{
					this.compiledPassInfos[i].Reset(passes[i], i);
				}
			}

			// Token: 0x04000A03 RID: 2563
			public DynamicArray<RenderGraph.CompiledResourceInfo>[] compiledResourcesInfos = new DynamicArray<RenderGraph.CompiledResourceInfo>[3];

			// Token: 0x04000A04 RID: 2564
			public DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfos = new DynamicArray<RenderGraph.CompiledPassInfo>();

			// Token: 0x04000A05 RID: 2565
			public int lastExecutionFrame;
		}

		// Token: 0x02000238 RID: 568
		private class ProfilingScopePassData
		{
			// Token: 0x04000A06 RID: 2566
			public ProfilingSampler sampler;
		}

		// Token: 0x02000239 RID: 569
		// (Invoke) Token: 0x06000F69 RID: 3945
		internal delegate void OnGraphRegisteredDelegate(RenderGraph graph);

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x06000F6D RID: 3949
		internal delegate void OnExecutionRegisteredDelegate(RenderGraph graph, string executionName);
	}
}
