using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000222 RID: 546
	public class MainLightShadowCasterPass : ScriptableRenderPass
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x00042B2C File Offset: 0x00040D2C
		public MainLightShadowCasterPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("Draw Main Light Shadowmap");
			base.renderPassEvent = evt;
			this.m_PassData = new MainLightShadowCasterPass.PassData();
			this.m_MainLightShadowMatrices = new Matrix4x4[5];
			this.m_CascadeSlices = new ShadowSliceData[4];
			this.m_CascadeSplitDistances = new Vector4[4];
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._WorldToShadow = Shader.PropertyToID("_MainLightWorldToShadow");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams = Shader.PropertyToID("_MainLightShadowParams");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres0 = Shader.PropertyToID("_CascadeShadowSplitSpheres0");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres1 = Shader.PropertyToID("_CascadeShadowSplitSpheres1");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres2 = Shader.PropertyToID("_CascadeShadowSplitSpheres2");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres3 = Shader.PropertyToID("_CascadeShadowSplitSpheres3");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSphereRadii = Shader.PropertyToID("_CascadeShadowSplitSphereRadii");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset0 = Shader.PropertyToID("_MainLightShadowOffset0");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset1 = Shader.PropertyToID("_MainLightShadowOffset1");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize = Shader.PropertyToID("_MainLightShadowmapSize");
			this.m_MainLightShadowmapID = Shader.PropertyToID("_MainLightShadowmapTexture");
			this.m_EmptyShadowmapNeedsClear = true;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00042C42 File Offset: 0x00040E42
		public void Dispose()
		{
			RTHandle mainLightShadowmapTexture = this.m_MainLightShadowmapTexture;
			if (mainLightShadowmapTexture != null)
			{
				mainLightShadowmapTexture.Release();
			}
			RTHandle emptyMainLightShadowmapTexture = this.m_EmptyMainLightShadowmapTexture;
			if (emptyMainLightShadowmapTexture == null)
			{
				return;
			}
			emptyMainLightShadowmapTexture.Release();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00042C68 File Offset: 0x00040E68
		public bool Setup(ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			return this.Setup(universalRenderingData, cameraData, lightData, shadowData);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00042CA0 File Offset: 0x00040EA0
		public bool Setup(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			if (!shadowData.mainLightShadowsEnabled)
			{
				return false;
			}
			bool flag;
			using (new ProfilingScope(this.m_ProfilingSetupSampler))
			{
				if (!shadowData.supportsMainLightShadows)
				{
					flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants);
				}
				else
				{
					this.Clear();
					int shadowLightIndex = lightData.mainLightIndex;
					if (shadowLightIndex == -1)
					{
						flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants);
					}
					else
					{
						VisibleLight shadowLight = lightData.visibleLights[shadowLightIndex];
						if (shadowLight.light.shadows == LightShadows.None)
						{
							flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants);
						}
						else
						{
							if (shadowLight.lightType != LightType.Directional)
							{
								Debug.LogWarning("Only directional lights are supported as main light.");
							}
							Bounds bounds;
							if (!renderingData.cullResults.GetShadowCasterBounds(shadowLightIndex, out bounds))
							{
								flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants);
							}
							else
							{
								this.m_ShadowCasterCascadesCount = shadowData.mainLightShadowCascadesCount;
								this.renderTargetWidth = shadowData.mainLightRenderTargetWidth;
								this.renderTargetHeight = shadowData.mainLightRenderTargetHeight;
								ref URPLightShadowCullingInfos shadowCullingInfos = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(shadowLightIndex);
								for (int cascadeIndex = 0; cascadeIndex < this.m_ShadowCasterCascadesCount; cascadeIndex++)
								{
									ref ShadowSliceData sliceData = ref shadowCullingInfos.slices.UnsafeElementAt(cascadeIndex);
									Vector4[] cascadeSplitDistances = this.m_CascadeSplitDistances;
									int num = cascadeIndex;
									ShadowSplitData splitData = sliceData.splitData;
									cascadeSplitDistances[num] = splitData.cullingSphere;
									this.m_CascadeSlices[cascadeIndex] = sliceData;
									if (!shadowCullingInfos.IsSliceValid(cascadeIndex))
									{
										return this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants);
									}
								}
								this.UpdateTextureDescriptorIfNeeded();
								this.m_MaxShadowDistanceSq = cameraData.maxShadowDistance * cameraData.maxShadowDistance;
								this.m_CascadeBorder = shadowData.mainLightShadowCascadeBorder;
								this.m_CreateEmptyShadowmap = false;
								base.useNativeRenderPass = true;
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00042E8C File Offset: 0x0004108C
		private void UpdateTextureDescriptorIfNeeded()
		{
			if (this.m_MainLightShadowDescriptor.width != this.renderTargetWidth || this.m_MainLightShadowDescriptor.height != this.renderTargetHeight || this.m_MainLightShadowDescriptor.depthBufferBits != 16 || this.m_MainLightShadowDescriptor.colorFormat != RenderTextureFormat.Shadowmap)
			{
				this.m_MainLightShadowDescriptor = new RenderTextureDescriptor(this.renderTargetWidth, this.renderTargetHeight, RenderTextureFormat.Shadowmap, 16);
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00042EF6 File Offset: 0x000410F6
		private bool SetupForEmptyRendering(bool stripShadowsOffVariants)
		{
			if (!stripShadowsOffVariants)
			{
				return false;
			}
			this.m_CreateEmptyShadowmap = true;
			base.useNativeRenderPass = false;
			return true;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00042F0C File Offset: 0x0004110C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			if (this.m_CreateEmptyShadowmap)
			{
				if (ShadowUtils.ShadowRTReAllocateIfNeeded(ref this.m_EmptyMainLightShadowmapTexture, 1, 1, 16, 1, 0f, "_EmptyMainLightShadowmapTexture"))
				{
					this.m_EmptyShadowmapNeedsClear = true;
				}
				if (!this.m_EmptyShadowmapNeedsClear)
				{
					if (Application.platform == RuntimePlatform.Android && PlatformAutoDetect.isRunningOnPowerVRGPU)
					{
						base.ResetTarget();
					}
					return;
				}
				base.ConfigureTarget(this.m_EmptyMainLightShadowmapTexture);
				this.m_EmptyShadowmapNeedsClear = false;
			}
			else
			{
				ShadowUtils.ShadowRTReAllocateIfNeeded(ref this.m_MainLightShadowmapTexture, this.renderTargetWidth, this.renderTargetHeight, 16, 1, 0f, "_MainLightShadowmapTexture");
				base.ConfigureTarget(this.m_MainLightShadowmapTexture);
			}
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00042FB8 File Offset: 0x000411B8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			if (this.m_CreateEmptyShadowmap)
			{
				this.SetEmptyMainLightCascadeShadowmap(CommandBufferHelpers.GetRasterCommandBuffer(universalRenderingData.commandBuffer));
				universalRenderingData.commandBuffer.SetGlobalTexture(this.m_MainLightShadowmapID, this.m_EmptyMainLightShadowmapTexture.nameID);
				return;
			}
			this.InitPassData(ref this.m_PassData, universalRenderingData, cameraData, lightData, shadowData);
			this.InitRendererLists(ref this.m_PassData, context, null, false);
			this.RenderMainLightCascadeShadowmap(CommandBufferHelpers.GetRasterCommandBuffer(universalRenderingData.commandBuffer), ref this.m_PassData, false);
			universalRenderingData.commandBuffer.SetGlobalTexture(this.m_MainLightShadowmapID, this.m_MainLightShadowmapTexture.nameID);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00043070 File Offset: 0x00041270
		private void Clear()
		{
			for (int i = 0; i < this.m_MainLightShadowMatrices.Length; i++)
			{
				this.m_MainLightShadowMatrices[i] = Matrix4x4.identity;
			}
			for (int j = 0; j < this.m_CascadeSplitDistances.Length; j++)
			{
				this.m_CascadeSplitDistances[j] = new Vector4(0f, 0f, 0f, 0f);
			}
			for (int k = 0; k < this.m_CascadeSlices.Length; k++)
			{
				this.m_CascadeSlices[k].Clear();
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000430FD File Offset: 0x000412FD
		private void SetEmptyMainLightCascadeShadowmap(RasterCommandBuffer cmd)
		{
			cmd.EnableKeyword(in ShaderGlobalKeywords.MainLightShadows);
			MainLightShadowCasterPass.SetEmptyMainLightShadowParams(cmd);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00043110 File Offset: 0x00041310
		internal static void SetEmptyMainLightShadowParams(RasterCommandBuffer cmd)
		{
			cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, MainLightShadowCasterPass.s_EmptyShadowParams);
			cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, MainLightShadowCasterPass.s_EmptyShadowmapSize);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00043134 File Offset: 0x00041334
		private void RenderMainLightCascadeShadowmap(RasterCommandBuffer cmd, ref MainLightShadowCasterPass.PassData data, bool isRenderGraph)
		{
			UniversalLightData lightData = data.lightData;
			int shadowLightIndex = lightData.mainLightIndex;
			if (shadowLightIndex == -1)
			{
				return;
			}
			VisibleLight shadowLight = lightData.visibleLights[shadowLightIndex];
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.MainLightShadow)))
			{
				ShadowUtils.SetCameraPosition(cmd, data.cameraData.worldSpaceCameraPos);
				if (!isRenderGraph)
				{
					ShadowUtils.SetWorldToCameraMatrix(cmd, data.cameraData.GetViewMatrix(0));
				}
				for (int cascadeIndex = 0; cascadeIndex < this.m_ShadowCasterCascadesCount; cascadeIndex++)
				{
					Vector4 shadowBias = ShadowUtils.GetShadowBias(ref shadowLight, shadowLightIndex, data.shadowData, this.m_CascadeSlices[cascadeIndex].projectionMatrix, (float)this.m_CascadeSlices[cascadeIndex].resolution);
					ShadowUtils.SetupShadowCasterConstantBuffer(cmd, ref shadowLight, shadowBias);
					cmd.SetKeyword(in ShaderGlobalKeywords.CastingPunctualLightShadow, false);
					RendererList shadowRendererList = (isRenderGraph ? data.shadowRendererListsHandle[cascadeIndex] : data.shadowRendererLists[cascadeIndex]);
					ShadowUtils.RenderShadowSlice(cmd, ref this.m_CascadeSlices[cascadeIndex], ref shadowRendererList, this.m_CascadeSlices[cascadeIndex].projectionMatrix, this.m_CascadeSlices[cascadeIndex].viewMatrix);
				}
				data.shadowData.isKeywordSoftShadowsEnabled = shadowLight.light.shadows == LightShadows.Soft && data.shadowData.supportsSoftShadows;
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadows, data.shadowData.mainLightShadowCascadesCount == 1);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowCascades, data.shadowData.mainLightShadowCascadesCount > 1);
				ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, data.shadowData);
				this.SetupMainLightShadowReceiverConstants(cmd, ref shadowLight, data.shadowData);
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00043308 File Offset: 0x00041508
		private void SetupMainLightShadowReceiverConstants(RasterCommandBuffer cmd, ref VisibleLight shadowLight, UniversalShadowData shadowData)
		{
			Light light = shadowLight.light;
			bool softShadows = shadowLight.light.shadows == LightShadows.Soft && shadowData.supportsSoftShadows;
			int cascadeCount = this.m_ShadowCasterCascadesCount;
			for (int i = 0; i < cascadeCount; i++)
			{
				this.m_MainLightShadowMatrices[i] = this.m_CascadeSlices[i].shadowTransform;
			}
			Matrix4x4 noOpShadowMatrix = Matrix4x4.zero;
			noOpShadowMatrix.m22 = (SystemInfo.usesReversedZBuffer ? 1f : 0f);
			for (int j = cascadeCount; j <= 4; j++)
			{
				this.m_MainLightShadowMatrices[j] = noOpShadowMatrix;
			}
			float invShadowAtlasWidth = 1f / (float)this.renderTargetWidth;
			float invShadowAtlasHeight = 1f / (float)this.renderTargetHeight;
			float invHalfShadowAtlasWidth = 0.5f * invShadowAtlasWidth;
			float invHalfShadowAtlasHeight = 0.5f * invShadowAtlasHeight;
			float softShadowsProp = ShadowUtils.SoftShadowQualityToShaderProperty(light, softShadows);
			float shadowFadeScale;
			float shadowFadeBias;
			ShadowUtils.GetScaleAndBiasForLinearDistanceFade(this.m_MaxShadowDistanceSq, this.m_CascadeBorder, out shadowFadeScale, out shadowFadeBias);
			cmd.SetGlobalMatrixArray(MainLightShadowCasterPass.MainLightShadowConstantBuffer._WorldToShadow, this.m_MainLightShadowMatrices);
			cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, new Vector4(light.shadowStrength, softShadowsProp, shadowFadeScale, shadowFadeBias));
			if (this.m_ShadowCasterCascadesCount > 1)
			{
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres0, this.m_CascadeSplitDistances[0]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres1, this.m_CascadeSplitDistances[1]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres2, this.m_CascadeSplitDistances[2]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres3, this.m_CascadeSplitDistances[3]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSphereRadii, new Vector4(this.m_CascadeSplitDistances[0].w * this.m_CascadeSplitDistances[0].w, this.m_CascadeSplitDistances[1].w * this.m_CascadeSplitDistances[1].w, this.m_CascadeSplitDistances[2].w * this.m_CascadeSplitDistances[2].w, this.m_CascadeSplitDistances[3].w * this.m_CascadeSplitDistances[3].w));
			}
			if (shadowData.supportsSoftShadows)
			{
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset0, new Vector4(-invHalfShadowAtlasWidth, -invHalfShadowAtlasHeight, invHalfShadowAtlasWidth, -invHalfShadowAtlasHeight));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset1, new Vector4(-invHalfShadowAtlasWidth, invHalfShadowAtlasHeight, invHalfShadowAtlasWidth, invHalfShadowAtlasHeight));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, new Vector4(invShadowAtlasWidth, invShadowAtlasHeight, (float)this.renderTargetWidth, (float)this.renderTargetHeight));
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00043588 File Offset: 0x00041788
		private void InitPassData(ref MainLightShadowCasterPass.PassData passData, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.emptyShadowmap = this.m_CreateEmptyShadowmap;
			passData.shadowmapID = this.m_MainLightShadowmapID;
			passData.renderingData = renderingData;
			passData.cameraData = cameraData;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000435DC File Offset: 0x000417DC
		private void InitEmptyPassData(ref MainLightShadowCasterPass.PassData passData, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.emptyShadowmap = this.m_CreateEmptyShadowmap;
			passData.shadowmapID = this.m_MainLightShadowmapID;
			passData.renderingData = renderingData;
			passData.cameraData = cameraData;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00043630 File Offset: 0x00041830
		private void InitRendererLists(ref MainLightShadowCasterPass.PassData passData, ScriptableRenderContext context, RenderGraph renderGraph, bool useRenderGraph)
		{
			int shadowLightIndex = passData.lightData.mainLightIndex;
			if (!this.m_CreateEmptyShadowmap && shadowLightIndex != -1)
			{
				ShadowDrawingSettings settings = new ShadowDrawingSettings(passData.renderingData.cullResults, shadowLightIndex);
				settings.useRenderingLayerMaskTest = UniversalRenderPipeline.asset.useRenderingLayers;
				for (int cascadeIndex = 0; cascadeIndex < this.m_ShadowCasterCascadesCount; cascadeIndex++)
				{
					if (useRenderGraph)
					{
						passData.shadowRendererListsHandle[cascadeIndex] = renderGraph.CreateShadowRendererList(ref settings);
					}
					else
					{
						passData.shadowRendererLists[cascadeIndex] = context.CreateShadowRendererList(ref settings);
					}
				}
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000436C0 File Offset: 0x000418C0
		internal TextureHandle Render(RenderGraph graph, ContextContainer frameData)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			MainLightShadowCasterPass.PassData passData;
			TextureHandle shadowTexture;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<MainLightShadowCasterPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/MainLightShadowCasterPass.cs", 466))
			{
				this.InitPassData(ref passData, renderingData, cameraData, lightData, shadowData);
				this.InitRendererLists(ref passData, default(ScriptableRenderContext), graph, true);
				if (!this.m_CreateEmptyShadowmap)
				{
					for (int cascadeIndex = 0; cascadeIndex < this.m_ShadowCasterCascadesCount; cascadeIndex++)
					{
						builder.UseRendererList(in passData.shadowRendererListsHandle[cascadeIndex]);
					}
					shadowTexture = UniversalRenderer.CreateRenderGraphTexture(graph, this.m_MainLightShadowDescriptor, "_MainLightShadowmapTexture", true, ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear, TextureWrapMode.Clamp);
					builder.SetRenderAttachmentDepth(shadowTexture, AccessFlags.Write);
				}
				else
				{
					shadowTexture = graph.defaultResources.defaultShadowTexture;
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (shadowTexture.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in shadowTexture, this.m_MainLightShadowmapID);
				}
				builder.SetRenderFunc<MainLightShadowCasterPass.PassData>(delegate(MainLightShadowCasterPass.PassData data, RasterGraphContext context)
				{
					if (!data.emptyShadowmap)
					{
						data.pass.RenderMainLightCascadeShadowmap(context.cmd, ref data, true);
						return;
					}
					data.pass.SetEmptyMainLightCascadeShadowmap(context.cmd);
				});
			}
			return shadowTexture;
		}

		// Token: 0x04000DAD RID: 3501
		private const int k_MaxCascades = 4;

		// Token: 0x04000DAE RID: 3502
		private const int k_ShadowmapBufferBits = 16;

		// Token: 0x04000DAF RID: 3503
		private float m_CascadeBorder;

		// Token: 0x04000DB0 RID: 3504
		private float m_MaxShadowDistanceSq;

		// Token: 0x04000DB1 RID: 3505
		private int m_ShadowCasterCascadesCount;

		// Token: 0x04000DB2 RID: 3506
		private int m_MainLightShadowmapID;

		// Token: 0x04000DB3 RID: 3507
		internal RTHandle m_MainLightShadowmapTexture;

		// Token: 0x04000DB4 RID: 3508
		private RTHandle m_EmptyMainLightShadowmapTexture;

		// Token: 0x04000DB5 RID: 3509
		private const int k_EmptyShadowMapDimensions = 1;

		// Token: 0x04000DB6 RID: 3510
		private const string k_MainLightShadowMapTextureName = "_MainLightShadowmapTexture";

		// Token: 0x04000DB7 RID: 3511
		private const string k_EmptyMainLightShadowMapTextureName = "_EmptyMainLightShadowmapTexture";

		// Token: 0x04000DB8 RID: 3512
		private static readonly Vector4 s_EmptyShadowParams = new Vector4(1f, 0f, 1f, 0f);

		// Token: 0x04000DB9 RID: 3513
		private static readonly Vector4 s_EmptyShadowmapSize = (MainLightShadowCasterPass.s_EmptyShadowmapSize = new Vector4(1f, 1f, 1f, 1f));

		// Token: 0x04000DBA RID: 3514
		private Matrix4x4[] m_MainLightShadowMatrices;

		// Token: 0x04000DBB RID: 3515
		private ShadowSliceData[] m_CascadeSlices;

		// Token: 0x04000DBC RID: 3516
		private Vector4[] m_CascadeSplitDistances;

		// Token: 0x04000DBD RID: 3517
		private RenderTextureDescriptor m_MainLightShadowDescriptor;

		// Token: 0x04000DBE RID: 3518
		private bool m_CreateEmptyShadowmap;

		// Token: 0x04000DBF RID: 3519
		private bool m_EmptyShadowmapNeedsClear;

		// Token: 0x04000DC0 RID: 3520
		private int renderTargetWidth;

		// Token: 0x04000DC1 RID: 3521
		private int renderTargetHeight;

		// Token: 0x04000DC2 RID: 3522
		private ProfilingSampler m_ProfilingSetupSampler = new ProfilingSampler("Setup Main Shadowmap");

		// Token: 0x04000DC3 RID: 3523
		private MainLightShadowCasterPass.PassData m_PassData;

		// Token: 0x02000223 RID: 547
		private static class MainLightShadowConstantBuffer
		{
			// Token: 0x04000DC4 RID: 3524
			public static int _WorldToShadow;

			// Token: 0x04000DC5 RID: 3525
			public static int _ShadowParams;

			// Token: 0x04000DC6 RID: 3526
			public static int _CascadeShadowSplitSpheres0;

			// Token: 0x04000DC7 RID: 3527
			public static int _CascadeShadowSplitSpheres1;

			// Token: 0x04000DC8 RID: 3528
			public static int _CascadeShadowSplitSpheres2;

			// Token: 0x04000DC9 RID: 3529
			public static int _CascadeShadowSplitSpheres3;

			// Token: 0x04000DCA RID: 3530
			public static int _CascadeShadowSplitSphereRadii;

			// Token: 0x04000DCB RID: 3531
			public static int _ShadowOffset0;

			// Token: 0x04000DCC RID: 3532
			public static int _ShadowOffset1;

			// Token: 0x04000DCD RID: 3533
			public static int _ShadowmapSize;
		}

		// Token: 0x02000224 RID: 548
		private class PassData
		{
			// Token: 0x04000DCE RID: 3534
			internal UniversalRenderingData renderingData;

			// Token: 0x04000DCF RID: 3535
			internal UniversalCameraData cameraData;

			// Token: 0x04000DD0 RID: 3536
			internal UniversalLightData lightData;

			// Token: 0x04000DD1 RID: 3537
			internal UniversalShadowData shadowData;

			// Token: 0x04000DD2 RID: 3538
			internal MainLightShadowCasterPass pass;

			// Token: 0x04000DD3 RID: 3539
			internal TextureHandle shadowmapTexture;

			// Token: 0x04000DD4 RID: 3540
			internal int shadowmapID;

			// Token: 0x04000DD5 RID: 3541
			internal bool emptyShadowmap;

			// Token: 0x04000DD6 RID: 3542
			internal RendererListHandle[] shadowRendererListsHandle = new RendererListHandle[4];

			// Token: 0x04000DD7 RID: 3543
			internal RendererList[] shadowRendererLists = new RendererList[4];
		}
	}
}
