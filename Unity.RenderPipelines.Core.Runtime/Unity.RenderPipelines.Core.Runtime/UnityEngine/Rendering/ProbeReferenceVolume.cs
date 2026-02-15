using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering
{
	// Token: 0x020000FE RID: 254
	public class ProbeReferenceVolume
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x000155E4 File Offset: 0x000137E4
		public void BindAPVRuntimeResources(CommandBuffer cmdBuffer, bool isProbeVolumeEnabled)
		{
			bool needToBindNeutral = true;
			ProbeReferenceVolume refVolume = ProbeReferenceVolume.instance;
			if (isProbeVolumeEnabled && this.m_ProbeReferenceVolumeInit)
			{
				ProbeReferenceVolume.RuntimeResources rr = refVolume.GetRuntimeResources();
				if ((rr.index != null && rr.L0_L1rx != null && rr.L1_G_ry != null && rr.L1_B_rz != null) & ((refVolume.shBands == ProbeVolumeSHBands.SphericalHarmonicsL2 && rr.L2_0 != null) || refVolume.shBands == ProbeVolumeSHBands.SphericalHarmonicsL1))
				{
					cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._APVResIndex, rr.index);
					cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._APVResCellIndices, rr.cellIndices);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL0_L1Rx, rr.L0_L1rx);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL1G_L1Ry, rr.L1_G_ry);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL1B_L1Rz, rr.L1_B_rz);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResValidity, rr.Validity);
					int skyOcclusionTexL0L = ProbeReferenceVolume.ShaderIDs._SkyOcclusionTexL0L1;
					RenderTexture renderTexture = rr.SkyOcclusionL0L1;
					cmdBuffer.SetGlobalTexture(skyOcclusionTexL0L, (renderTexture != null) ? renderTexture : CoreUtils.blackVolumeTexture);
					int skyShadingDirectionIndicesTex = ProbeReferenceVolume.ShaderIDs._SkyShadingDirectionIndicesTex;
					renderTexture = rr.SkyShadingDirectionIndices;
					cmdBuffer.SetGlobalTexture(skyShadingDirectionIndicesTex, (renderTexture != null) ? renderTexture : CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._SkyPrecomputedDirections, rr.SkyPrecomputedDirections);
					cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._AntiLeakData, rr.QualityLeakReductionData);
					if (refVolume.shBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_0, rr.L2_0);
						cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_1, rr.L2_1);
						cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_2, rr.L2_2);
						cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_3, rr.L2_3);
					}
					int apvprobeOcclusion = ProbeReferenceVolume.ShaderIDs._APVProbeOcclusion;
					renderTexture = rr.ProbeOcclusion;
					cmdBuffer.SetGlobalTexture(apvprobeOcclusion, (renderTexture != null) ? renderTexture : CoreUtils.whiteVolumeTexture);
					needToBindNeutral = false;
				}
			}
			if (needToBindNeutral)
			{
				if (this.m_EmptyIndexBuffer == null)
				{
					this.m_EmptyIndexBuffer = new ComputeBuffer(1, 12, ComputeBufferType.Structured);
				}
				cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._APVResIndex, this.m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._APVResCellIndices, this.m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL0_L1Rx, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL1G_L1Ry, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL1B_L1Rz, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResValidity, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._SkyOcclusionTexL0L1, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._SkyShadingDirectionIndicesTex, CoreUtils.blackVolumeTexture);
				cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._SkyPrecomputedDirections, this.m_EmptyIndexBuffer);
				cmdBuffer.SetGlobalBuffer(ProbeReferenceVolume.ShaderIDs._AntiLeakData, this.m_EmptyIndexBuffer);
				if (refVolume.shBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_0, CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_1, CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_2, CoreUtils.blackVolumeTexture);
					cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVResL2_3, CoreUtils.blackVolumeTexture);
				}
				cmdBuffer.SetGlobalTexture(ProbeReferenceVolume.ShaderIDs._APVProbeOcclusion, CoreUtils.whiteVolumeTexture);
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00015934 File Offset: 0x00013B34
		public bool UpdateShaderVariablesProbeVolumes(CommandBuffer cmd, ProbeVolumesOptions probeVolumeOptions, int taaFrameIndex, bool supportRenderingLayers = false)
		{
			bool flag = this.DataHasBeenLoaded();
			if (flag)
			{
				ProbeVolumeShadingParameters parameters;
				parameters.normalBias = probeVolumeOptions.normalBias.value;
				parameters.viewBias = probeVolumeOptions.viewBias.value;
				parameters.scaleBiasByMinDistanceBetweenProbes = probeVolumeOptions.scaleBiasWithMinProbeDistance.value;
				parameters.samplingNoise = probeVolumeOptions.samplingNoise.value;
				parameters.weight = probeVolumeOptions.intensityMultiplier.value;
				parameters.leakReductionMode = probeVolumeOptions.leakReductionMode.value;
				parameters.frameIndexForNoise = taaFrameIndex * (probeVolumeOptions.animateSamplingNoise.value ? 1 : 0);
				parameters.reflNormalizationLowerClamp = 0.005f;
				parameters.reflNormalizationUpperClamp = (probeVolumeOptions.occlusionOnlyReflectionNormalization.value ? 1f : 7f);
				parameters.skyOcclusionIntensity = (this.skyOcclusion ? probeVolumeOptions.skyOcclusionIntensityMultiplier.value : 0f);
				parameters.skyOcclusionShadingDirection = this.skyOcclusion && this.skyOcclusionShadingDirection;
				parameters.regionCount = this.m_CurrentBakingSet.bakedMaskCount;
				parameters.regionLayerMasks = (supportRenderingLayers ? this.m_CurrentBakingSet.bakedLayerMasks : uint.MaxValue);
				parameters.worldOffset = probeVolumeOptions.worldOffset.value;
				this.UpdateConstantBuffer(cmd, parameters);
			}
			return flag;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00015A82 File Offset: 0x00013C82
		internal ProbeVolumeDebug probeVolumeDebug { get; } = new ProbeVolumeDebug();

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00015A8A File Offset: 0x00013C8A
		public Color[] subdivisionDebugColors { get; } = new Color[7];

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00015A94 File Offset: 0x00013C94
		private Mesh debugMesh
		{
			get
			{
				if (this.m_DebugMesh == null)
				{
					this.m_DebugMesh = DebugShapes.instance.BuildCustomSphereMesh(0.5f, 9U, 8U);
					this.m_DebugMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000000f);
				}
				return this.m_DebugMesh;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00015AF1 File Offset: 0x00013CF1
		[Obsolete("Use the other override to support sampling offset in debug modes.")]
		public void RenderDebug(Camera camera, Texture exposureTexture)
		{
			this.RenderDebug(camera, null, exposureTexture);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00015AFC File Offset: 0x00013CFC
		public void RenderDebug(Camera camera, ProbeVolumesOptions options, Texture exposureTexture)
		{
			if (camera.cameraType != CameraType.Reflection && camera.cameraType != CameraType.Preview)
			{
				if (options != null)
				{
					ProbeVolumeDebug.currentOffset = options.worldOffset.value;
				}
				this.DrawProbeDebug(camera, exposureTexture);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00015B32 File Offset: 0x00013D32
		public bool IsProbeSamplingDebugEnabled()
		{
			return ProbeReferenceVolume.probeSamplingDebugData.update > ProbeSamplingDebugUpdate.Never;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00015B44 File Offset: 0x00013D44
		public bool GetProbeSamplingDebugResources(Camera camera, out GraphicsBuffer resultBuffer, out Vector2 coords)
		{
			resultBuffer = ProbeReferenceVolume.probeSamplingDebugData.positionNormalBuffer;
			coords = ProbeReferenceVolume.probeSamplingDebugData.coordinates;
			if (!this.probeVolumeDebug.drawProbeSamplingDebug)
			{
				return false;
			}
			if (ProbeReferenceVolume.probeSamplingDebugData.update == ProbeSamplingDebugUpdate.Never)
			{
				return false;
			}
			if (ProbeReferenceVolume.probeSamplingDebugData.update == ProbeSamplingDebugUpdate.Once)
			{
				ProbeReferenceVolume.probeSamplingDebugData.update = ProbeSamplingDebugUpdate.Never;
				ProbeReferenceVolume.probeSamplingDebugData.forceScreenCenterCoordinates = false;
			}
			return true;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00015BB0 File Offset: 0x00013DB0
		private bool TryCreateDebugRenderData()
		{
			ProbeVolumeDebugResources debugResources;
			if (!GraphicsSettings.TryGetRenderPipelineSettings<ProbeVolumeDebugResources>(out debugResources))
			{
				return false;
			}
			ShaderStrippingSetting shaderStrippingSetting;
			if (GraphicsSettings.TryGetRenderPipelineSettings<ShaderStrippingSetting>(out shaderStrippingSetting) && shaderStrippingSetting.stripRuntimeDebugShaders)
			{
				return false;
			}
			this.m_DebugMaterial = CoreUtils.CreateEngineMaterial(debugResources.probeVolumeDebugShader);
			this.m_DebugMaterial.enableInstancing = true;
			this.m_DebugProbeSamplingMesh = debugResources.probeSamplingDebugMesh;
			this.m_DebugProbeSamplingMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000000f);
			this.m_ProbeSamplingDebugMaterial = CoreUtils.CreateEngineMaterial(debugResources.probeVolumeSamplingDebugShader);
			this.m_ProbeSamplingDebugMaterial02 = CoreUtils.CreateEngineMaterial(debugResources.probeVolumeDebugShader);
			this.m_ProbeSamplingDebugMaterial02.enableInstancing = true;
			ProbeReferenceVolume.probeSamplingDebugData.positionNormalBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 2, Marshal.SizeOf(typeof(Vector4)));
			this.m_DisplayNumbersTexture = debugResources.numbersDisplayTex;
			this.m_DebugOffsetMesh = Resources.GetBuiltinResource<Mesh>("pyramid.fbx");
			this.m_DebugOffsetMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000000f);
			this.m_DebugOffsetMaterial = CoreUtils.CreateEngineMaterial(debugResources.probeVolumeOffsetDebugShader);
			this.m_DebugOffsetMaterial.enableInstancing = true;
			this.m_DebugFragmentationMaterial = CoreUtils.CreateEngineMaterial(debugResources.probeVolumeFragmentationDebugShader);
			this.subdivisionDebugColors[0] = ProbeVolumeDebugColorPreferences.s_DetailSubdivision;
			this.subdivisionDebugColors[1] = ProbeVolumeDebugColorPreferences.s_MediumSubdivision;
			this.subdivisionDebugColors[2] = ProbeVolumeDebugColorPreferences.s_LowSubdivision;
			this.subdivisionDebugColors[3] = ProbeVolumeDebugColorPreferences.s_VeryLowSubdivision;
			this.subdivisionDebugColors[4] = ProbeVolumeDebugColorPreferences.s_SparseSubdivision;
			this.subdivisionDebugColors[5] = ProbeVolumeDebugColorPreferences.s_SparsestSubdivision;
			this.subdivisionDebugColors[6] = ProbeVolumeDebugColorPreferences.s_DetailSubdivision;
			return true;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00015D5D File Offset: 0x00013F5D
		private void InitializeDebug()
		{
			if (this.TryCreateDebugRenderData())
			{
				this.RegisterDebug();
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00015D70 File Offset: 0x00013F70
		private void CleanupDebug()
		{
			this.UnregisterDebug(true);
			CoreUtils.Destroy(this.m_DebugMaterial);
			CoreUtils.Destroy(this.m_ProbeSamplingDebugMaterial);
			CoreUtils.Destroy(this.m_ProbeSamplingDebugMaterial02);
			CoreUtils.Destroy(this.m_DebugOffsetMaterial);
			CoreUtils.Destroy(this.m_DebugFragmentationMaterial);
			ProbeSamplingDebugData probeSamplingDebugData = ProbeReferenceVolume.probeSamplingDebugData;
			CoreUtils.SafeRelease((probeSamplingDebugData != null) ? probeSamplingDebugData.positionNormalBuffer : null);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00015DD1 File Offset: 0x00013FD1
		private void DebugCellIndexChanged<T>(DebugUI.Field<T> field, T value)
		{
			this.ClearDebugData();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00015DDC File Offset: 0x00013FDC
		private void RegisterDebug()
		{
			List<DebugUI.Widget> widgetList = new List<DebugUI.Widget>();
			widgetList.Add(new DebugUI.RuntimeDebugShadersMessageBox());
			DebugUI.Container container = new DebugUI.Container();
			container.displayName = "Subdivision Visualization";
			container.isHiddenCallback = () => false;
			DebugUI.Container subdivContainer = container;
			subdivContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Cells",
				tooltip = "Draw Cells used for loading and streaming.",
				getter = () => this.probeVolumeDebug.drawCells,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.drawCells = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			subdivContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Bricks",
				tooltip = "Display Subdivision bricks.",
				getter = () => this.probeVolumeDebug.drawBricks,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.drawBricks = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			ObservableList<DebugUI.Widget> children = subdivContainer.children;
			DebugUI.FloatField floatField = new DebugUI.FloatField();
			floatField.displayName = "Debug Draw Distance";
			floatField.tooltip = "How far from the Scene Camera to draw debug visualization for Cells and Bricks. Large distances can impact Editor performance.";
			floatField.getter = () => this.probeVolumeDebug.subdivisionViewCullingDistance;
			floatField.setter = delegate(float value)
			{
				this.probeVolumeDebug.subdivisionViewCullingDistance = value;
			};
			floatField.min = () => 0f;
			children.Add(floatField);
			widgetList.Add(subdivContainer);
			widgetList.Add(new DebugUI.RuntimeDebugShadersMessageBox());
			DebugUI.Container probeContainer = new DebugUI.Container
			{
				displayName = "Probe Visualization"
			};
			probeContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Probes",
				tooltip = "Render the debug view showing probe positions. Use the shading mode to determine which type of lighting data to visualize.",
				getter = () => this.probeVolumeDebug.drawProbes,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.drawProbes = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			DebugUI.Container probeContainerChildren = new DebugUI.Container
			{
				isHiddenCallback = () => !this.probeVolumeDebug.drawProbes
			};
			probeContainerChildren.children.Add(new DebugUI.EnumField
			{
				displayName = "Probe Shading Mode",
				tooltip = "Choose which lighting data to show in the probe debug visualization.",
				getter = () => (int)this.probeVolumeDebug.probeShading,
				setter = delegate(int value)
				{
					this.probeVolumeDebug.probeShading = (DebugProbeShadingMode)value;
				},
				autoEnum = typeof(DebugProbeShadingMode),
				getIndex = () => (int)this.probeVolumeDebug.probeShading,
				setIndex = delegate(int value)
				{
					this.probeVolumeDebug.probeShading = (DebugProbeShadingMode)value;
				}
			});
			ObservableList<DebugUI.Widget> children2 = probeContainerChildren.children;
			DebugUI.FloatField floatField2 = new DebugUI.FloatField();
			floatField2.displayName = "Debug Size";
			floatField2.tooltip = "The size of probes shown in the debug view.";
			floatField2.getter = () => this.probeVolumeDebug.probeSize;
			floatField2.setter = delegate(float value)
			{
				this.probeVolumeDebug.probeSize = value;
			};
			floatField2.min = () => 0.05f;
			floatField2.max = () => 10f;
			children2.Add(floatField2);
			DebugUI.FloatField exposureCompensation = new DebugUI.FloatField
			{
				displayName = "Exposure Compensation",
				tooltip = "Modify the brightness of probe visualizations. Decrease this number to make very bright probes more visible.",
				getter = () => this.probeVolumeDebug.exposureCompensation,
				setter = delegate(float value)
				{
					this.probeVolumeDebug.exposureCompensation = value;
				},
				isHiddenCallback = delegate
				{
					switch (this.probeVolumeDebug.probeShading)
					{
					case DebugProbeShadingMode.SH:
						return false;
					case DebugProbeShadingMode.SHL0:
						return false;
					case DebugProbeShadingMode.SHL0L1:
						return false;
					case DebugProbeShadingMode.SkyOcclusionSH:
						return false;
					case DebugProbeShadingMode.SkyDirection:
						return false;
					case DebugProbeShadingMode.ProbeOcclusion:
						return false;
					}
					return true;
				}
			};
			probeContainerChildren.children.Add(exposureCompensation);
			ObservableList<DebugUI.Widget> children3 = probeContainerChildren.children;
			DebugUI.IntField intField = new DebugUI.IntField();
			intField.displayName = "Max Subdivisions Displayed";
			intField.tooltip = "The highest (most dense) probe subdivision level displayed in the debug view.";
			intField.getter = () => this.probeVolumeDebug.maxSubdivToVisualize;
			intField.setter = delegate(int v)
			{
				this.probeVolumeDebug.maxSubdivToVisualize = Mathf.Max(0, Mathf.Min(v, this.GetMaxSubdivision() - 1));
			};
			intField.min = () => 0;
			intField.max = () => Mathf.Max(0, this.GetMaxSubdivision() - 1);
			children3.Add(intField);
			ObservableList<DebugUI.Widget> children4 = probeContainerChildren.children;
			DebugUI.IntField intField2 = new DebugUI.IntField();
			intField2.displayName = "Min Subdivisions Displayed";
			intField2.tooltip = "The lowest (least dense) probe subdivision level displayed in the debug view.";
			intField2.getter = () => this.probeVolumeDebug.minSubdivToVisualize;
			intField2.setter = delegate(int v)
			{
				this.probeVolumeDebug.minSubdivToVisualize = Mathf.Max(v, 0);
			};
			intField2.min = () => 0;
			intField2.max = () => Mathf.Max(0, this.GetMaxSubdivision() - 1);
			children4.Add(intField2);
			probeContainer.children.Add(probeContainerChildren);
			probeContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Debug Probe Sampling",
				tooltip = "Render the debug view displaying how probes are sampled for a selected pixel. Use the viewport overlay 'SelectPixel' button or Ctrl+Click on the viewport to select the debugged pixel",
				getter = () => this.probeVolumeDebug.drawProbeSamplingDebug,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.drawProbeSamplingDebug = value;
					ProbeReferenceVolume.probeSamplingDebugData.update = ProbeSamplingDebugUpdate.Once;
					ProbeReferenceVolume.probeSamplingDebugData.forceScreenCenterCoordinates = true;
				}
			});
			DebugUI.Container drawProbeSamplingDebugChildren = new DebugUI.Container
			{
				isHiddenCallback = () => !this.probeVolumeDebug.drawProbeSamplingDebug
			};
			ObservableList<DebugUI.Widget> children5 = drawProbeSamplingDebugChildren.children;
			DebugUI.FloatField floatField3 = new DebugUI.FloatField();
			floatField3.displayName = "Debug Size";
			floatField3.tooltip = "The size of gizmos shown in the debug view.";
			floatField3.getter = () => this.probeVolumeDebug.probeSamplingDebugSize;
			floatField3.setter = delegate(float value)
			{
				this.probeVolumeDebug.probeSamplingDebugSize = value;
			};
			floatField3.min = () => 0.05f;
			floatField3.max = () => 10f;
			children5.Add(floatField3);
			drawProbeSamplingDebugChildren.children.Add(new DebugUI.BoolField
			{
				displayName = "Debug With Sampling Noise",
				tooltip = "Enable Sampling Noise for this debug view. It should be enabled for accuracy but it can make results more difficult to read",
				getter = () => this.probeVolumeDebug.debugWithSamplingNoise,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.debugWithSamplingNoise = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			probeContainer.children.Add(drawProbeSamplingDebugChildren);
			probeContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Virtual Offset Debug",
				tooltip = "Enable Virtual Offset debug visualization. Indicates the offsets applied to probe positions. These are used to capture lighting when probes are considered invalid.",
				getter = () => this.probeVolumeDebug.drawVirtualOffsetPush,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.drawVirtualOffsetPush = value;
					if (this.probeVolumeDebug.drawVirtualOffsetPush && this.probeVolumeDebug.drawProbes && this.m_CurrentBakingSet != null)
					{
						float searchDistance = (float)ProbeReferenceVolume.CellSize(0) * this.MinBrickSize() / 3f * this.m_CurrentBakingSet.settings.virtualOffsetSettings.searchMultiplier + this.m_CurrentBakingSet.settings.virtualOffsetSettings.outOfGeoOffset;
						this.probeVolumeDebug.probeSize = Mathf.Min(this.probeVolumeDebug.probeSize, Mathf.Clamp(searchDistance, 0.05f, 10f));
					}
				}
			});
			DebugUI.Container drawVirtualOffsetDebugChildren = new DebugUI.Container
			{
				isHiddenCallback = () => !this.probeVolumeDebug.drawVirtualOffsetPush
			};
			DebugUI.FloatField floatField4 = new DebugUI.FloatField();
			floatField4.displayName = "Debug Size";
			floatField4.tooltip = "Modify the size of the arrows used in the virtual offset debug visualization.";
			floatField4.getter = () => this.probeVolumeDebug.offsetSize;
			floatField4.setter = delegate(float value)
			{
				this.probeVolumeDebug.offsetSize = value;
			};
			floatField4.min = () => 0.001f;
			floatField4.max = () => 0.1f;
			floatField4.isHiddenCallback = () => !this.probeVolumeDebug.drawVirtualOffsetPush;
			DebugUI.FloatField voOffset = floatField4;
			drawVirtualOffsetDebugChildren.children.Add(voOffset);
			probeContainer.children.Add(drawVirtualOffsetDebugChildren);
			ObservableList<DebugUI.Widget> children6 = probeContainer.children;
			DebugUI.FloatField floatField5 = new DebugUI.FloatField();
			floatField5.displayName = "Debug Draw Distance";
			floatField5.tooltip = "How far from the Scene Camera to draw probe debug visualizations. Large distances can impact Editor performance.";
			floatField5.getter = () => this.probeVolumeDebug.probeCullingDistance;
			floatField5.setter = delegate(float value)
			{
				this.probeVolumeDebug.probeCullingDistance = value;
			};
			floatField5.min = () => 0f;
			children6.Add(floatField5);
			widgetList.Add(probeContainer);
			DebugUI.Container adjustmentContainer = new DebugUI.Container
			{
				displayName = "Probe Adjustment Volumes"
			};
			adjustmentContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Auto Display Probes",
				tooltip = "When enabled and a Probe Adjustment Volumes is selected, automatically display the probes.",
				getter = () => this.probeVolumeDebug.autoDrawProbes,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.autoDrawProbes = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			adjustmentContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Isolate Affected",
				tooltip = "When enabled, only displayed probes in the influence of the currently selected Probe Adjustment Volumes.",
				getter = () => this.probeVolumeDebug.isolationProbeDebug,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.isolationProbeDebug = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.<RegisterDebug>g__RefreshDebug|42_0<bool>)
			});
			widgetList.Add(adjustmentContainer);
			DebugUI.Container streamingContainer = new DebugUI.Container
			{
				displayName = "Streaming",
				isHiddenCallback = () => !this.gpuStreamingEnabled && !this.diskStreamingEnabled
			};
			streamingContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Freeze Streaming",
				tooltip = "Stop Unity from streaming probe data in or out of GPU memory.",
				getter = () => this.probeVolumeDebug.freezeStreaming,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.freezeStreaming = value;
				}
			});
			streamingContainer.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Streaming Score",
				getter = () => this.probeVolumeDebug.displayCellStreamingScore,
				setter = delegate(bool value)
				{
					this.probeVolumeDebug.displayCellStreamingScore = value;
				}
			});
			ObservableList<DebugUI.Widget> children7 = streamingContainer.children;
			DebugUI.BoolField boolField = new DebugUI.BoolField();
			boolField.displayName = "Maximum cell streaming";
			boolField.tooltip = "Enable streaming as many cells as possible every frame.";
			boolField.getter = () => ProbeReferenceVolume.instance.loadMaxCellsPerFrame;
			boolField.setter = delegate(bool value)
			{
				ProbeReferenceVolume.instance.loadMaxCellsPerFrame = value;
			};
			children7.Add(boolField);
			DebugUI.Container container2 = new DebugUI.Container();
			container2.isHiddenCallback = () => ProbeReferenceVolume.instance.loadMaxCellsPerFrame;
			DebugUI.Container maxCellStreamingContainerChildren = container2;
			ObservableList<DebugUI.Widget> children8 = maxCellStreamingContainerChildren.children;
			DebugUI.IntField intField3 = new DebugUI.IntField();
			intField3.displayName = "Loaded Cells Per Frame";
			intField3.tooltip = "Determines the maximum number of Cells Unity streams per frame. Loading more Cells per frame can impact performance.";
			intField3.getter = () => ProbeReferenceVolume.instance.numberOfCellsLoadedPerFrame;
			intField3.setter = delegate(int value)
			{
				ProbeReferenceVolume.instance.SetNumberOfCellsLoadedPerFrame(value);
			};
			intField3.min = () => 1;
			intField3.max = () => 10;
			children8.Add(intField3);
			streamingContainer.children.Add(maxCellStreamingContainerChildren);
			if (Debug.isDebugBuild)
			{
				streamingContainer.children.Add(new DebugUI.BoolField
				{
					displayName = "Display Index Fragmentation",
					getter = () => this.probeVolumeDebug.displayIndexFragmentation,
					setter = delegate(bool value)
					{
						this.probeVolumeDebug.displayIndexFragmentation = value;
					}
				});
				DebugUI.Container indexDefragContainerChildren = new DebugUI.Container
				{
					isHiddenCallback = () => !this.probeVolumeDebug.displayIndexFragmentation
				};
				ObservableList<DebugUI.Widget> children9 = indexDefragContainerChildren.children;
				DebugUI.Value value2 = new DebugUI.Value();
				value2.displayName = "Index Fragmentation Rate";
				value2.getter = () => ProbeReferenceVolume.instance.indexFragmentationRate;
				children9.Add(value2);
				streamingContainer.children.Add(indexDefragContainerChildren);
				streamingContainer.children.Add(new DebugUI.BoolField
				{
					displayName = "Verbose Log",
					getter = () => this.probeVolumeDebug.verboseStreamingLog,
					setter = delegate(bool value)
					{
						this.probeVolumeDebug.verboseStreamingLog = value;
					}
				});
				streamingContainer.children.Add(new DebugUI.BoolField
				{
					displayName = "Debug Streaming",
					getter = () => this.probeVolumeDebug.debugStreaming,
					setter = delegate(bool value)
					{
						this.probeVolumeDebug.debugStreaming = value;
					}
				});
			}
			widgetList.Add(streamingContainer);
			if (this.supportScenarioBlending && this.m_CurrentBakingSet != null)
			{
				DebugUI.Container blendingContainer = new DebugUI.Container
				{
					displayName = "Scenario Blending"
				};
				ObservableList<DebugUI.Widget> children10 = blendingContainer.children;
				DebugUI.IntField intField4 = new DebugUI.IntField();
				intField4.displayName = "Number Of Cells Blended Per Frame";
				intField4.getter = () => ProbeReferenceVolume.instance.numberOfCellsBlendedPerFrame;
				intField4.setter = delegate(int value)
				{
					ProbeReferenceVolume.instance.numberOfCellsBlendedPerFrame = value;
				};
				intField4.min = () => 0;
				children10.Add(intField4);
				ObservableList<DebugUI.Widget> children11 = blendingContainer.children;
				DebugUI.FloatField floatField6 = new DebugUI.FloatField();
				floatField6.displayName = "Turnover Rate";
				floatField6.getter = () => ProbeReferenceVolume.instance.turnoverRate;
				floatField6.setter = delegate(float value)
				{
					ProbeReferenceVolume.instance.turnoverRate = value;
				};
				floatField6.min = () => 0f;
				floatField6.max = () => 1f;
				children11.Add(floatField6);
				this.m_DebugScenarioField = new DebugUI.EnumField
				{
					displayName = "Scenario Blend Target",
					tooltip = "Select another lighting scenario to blend with the active lighting scenario.",
					enumNames = this.m_DebugScenarioNames,
					enumValues = this.m_DebugScenarioValues,
					getIndex = delegate
					{
						if (this.m_CurrentBakingSet == null)
						{
							return 0;
						}
						this.<RegisterDebug>g__RefreshScenarioNames|42_75(ProbeReferenceVolume.GetSceneGUID(SceneManager.GetActiveScene()));
						this.probeVolumeDebug.otherStateIndex = 0;
						if (!string.IsNullOrEmpty(this.m_CurrentBakingSet.otherScenario))
						{
							for (int i = 1; i < this.m_DebugScenarioNames.Length; i++)
							{
								if (this.m_DebugScenarioNames[i].text == this.m_CurrentBakingSet.otherScenario)
								{
									this.probeVolumeDebug.otherStateIndex = i;
									break;
								}
							}
						}
						return this.probeVolumeDebug.otherStateIndex;
					},
					setIndex = delegate(int value)
					{
						string other = ((value == 0) ? null : this.m_DebugScenarioNames[value].text);
						this.m_CurrentBakingSet.BlendLightingScenario(other, this.m_CurrentBakingSet.scenarioBlendingFactor);
						this.probeVolumeDebug.otherStateIndex = value;
					},
					getter = () => this.probeVolumeDebug.otherStateIndex,
					setter = delegate(int value)
					{
						this.probeVolumeDebug.otherStateIndex = value;
					}
				};
				blendingContainer.children.Add(this.m_DebugScenarioField);
				ObservableList<DebugUI.Widget> children12 = blendingContainer.children;
				DebugUI.FloatField floatField7 = new DebugUI.FloatField();
				floatField7.displayName = "Scenario Blending Factor";
				floatField7.tooltip = "Blend between lighting scenarios by adjusting this slider.";
				floatField7.getter = () => ProbeReferenceVolume.instance.scenarioBlendingFactor;
				floatField7.setter = delegate(float value)
				{
					ProbeReferenceVolume.instance.scenarioBlendingFactor = value;
				};
				floatField7.min = () => 0f;
				floatField7.max = () => 1f;
				children12.Add(floatField7);
				widgetList.Add(blendingContainer);
			}
			if (widgetList.Count > 0)
			{
				this.m_DebugItems = widgetList.ToArray();
				DebugManager.instance.GetPanel(ProbeReferenceVolume.k_DebugPanelName, true, 0, false).children.Add(this.m_DebugItems);
			}
			DebugManager.instance.RegisterData(this.probeVolumeDebug);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016C8D File Offset: 0x00014E8D
		private void UnregisterDebug(bool destroyPanel)
		{
			if (destroyPanel)
			{
				DebugManager.instance.RemovePanel(ProbeReferenceVolume.k_DebugPanelName);
				return;
			}
			DebugManager.instance.GetPanel(ProbeReferenceVolume.k_DebugPanelName, false, 0, false).children.Remove(this.m_DebugItems);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00016CC8 File Offset: 0x00014EC8
		public void RenderFragmentationOverlay(RenderGraph renderGraph, TextureHandle colorBuffer, TextureHandle depthBuffer, DebugOverlay debugOverlay)
		{
			if (!this.m_ProbeReferenceVolumeInit || !this.probeVolumeDebug.displayIndexFragmentation)
			{
				return;
			}
			ProbeReferenceVolume.RenderFragmentationOverlayPassData passData;
			using (RenderGraphBuilder builder = renderGraph.AddRenderPass<ProbeReferenceVolume.RenderFragmentationOverlayPassData>("APVFragmentationOverlay", out passData, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ProbeReferenceVolume.Debug.cs", 826))
			{
				passData.debugOverlay = debugOverlay;
				passData.debugFragmentationMaterial = this.m_DebugFragmentationMaterial;
				passData.colorBuffer = builder.UseColorBuffer(in colorBuffer, 0);
				passData.depthBuffer = builder.UseDepthBuffer(in depthBuffer, DepthAccess.ReadWrite);
				passData.debugFragmentationData = this.m_Index.GetDebugFragmentationBuffer();
				passData.chunkCount = passData.debugFragmentationData.count;
				builder.SetRenderFunc<ProbeReferenceVolume.RenderFragmentationOverlayPassData>(delegate(ProbeReferenceVolume.RenderFragmentationOverlayPassData data, RenderGraphContext ctx)
				{
					MaterialPropertyBlock mpb = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
					data.debugOverlay.SetViewport(ctx.cmd);
					mpb.SetInt("_ChunkCount", data.chunkCount);
					mpb.SetBuffer("_DebugFragmentation", data.debugFragmentationData);
					ctx.cmd.DrawProcedural(Matrix4x4.identity, data.debugFragmentationMaterial, 0, MeshTopology.Triangles, 3, 1, mpb);
					data.debugOverlay.Next(1f);
				});
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00016DA0 File Offset: 0x00014FA0
		private bool ShouldCullCell(Vector3 cellPosition, Transform cameraTransform, Plane[] frustumPlanes)
		{
			Bounds volumeAABB = this.GetCellBounds(cellPosition);
			float cellSize = this.MaxBrickSize();
			float distanceRoundedUpWithCellSize = (float)Mathf.CeilToInt(this.probeVolumeDebug.probeCullingDistance / cellSize) * cellSize;
			return Vector3.Distance(cameraTransform.position, volumeAABB.center) > distanceRoundedUpWithCellSize || !GeometryUtility.TestPlanesAABB(frustumPlanes, volumeAABB);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00016DF3 File Offset: 0x00014FF3
		private static void UpdateDebugFromSelection(ref Vector4[] _AdjustmentVolumeBounds, ref int _AdjustmentVolumeCount)
		{
			int s_ActiveAdjustmentVolumes = ProbeVolumeDebug.s_ActiveAdjustmentVolumes;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00016DFC File Offset: 0x00014FFC
		private Bounds GetCellBounds(Vector3 cellPosition)
		{
			float cellSize = this.MaxBrickSize();
			return new Bounds(this.ProbeOffset() + ProbeVolumeDebug.currentOffset + cellPosition * cellSize + Vector3.one * (cellSize / 2f), cellSize * Vector3.one);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00016E54 File Offset: 0x00015054
		private bool ShouldCullCell(Vector3 cellPosition, Vector4[] adjustmentVolumeBounds, int adjustmentVolumeCount)
		{
			Bounds cellAABB = this.GetCellBounds(cellPosition);
			for (int touchup = 0; touchup < adjustmentVolumeCount; touchup++)
			{
				Vector3 center = adjustmentVolumeBounds[touchup * 3];
				if (adjustmentVolumeBounds[touchup * 3].w == 3.4028235E+38f)
				{
					float diameter = adjustmentVolumeBounds[touchup * 3 + 1].x * 2f;
					Bounds bounds = new Bounds(center, new Vector3(diameter, diameter, diameter));
					if (bounds.Intersects(cellAABB))
					{
						return false;
					}
				}
				else
				{
					ProbeReferenceVolume.Volume volume = default(ProbeReferenceVolume.Volume);
					volume.X = adjustmentVolumeBounds[touchup * 3 + 1];
					volume.Y = adjustmentVolumeBounds[touchup * 3 + 2];
					volume.Z = new Vector3(adjustmentVolumeBounds[touchup * 3].w, adjustmentVolumeBounds[touchup * 3 + 1].w, adjustmentVolumeBounds[touchup * 3 + 2].w);
					volume.corner = center - volume.X - volume.Y - volume.Z;
					volume.X *= 2f;
					volume.Y *= 2.05f;
					volume.Z *= 2f;
					Bounds bounds2 = volume.CalculateAABB();
					if (ProbeVolumePositioning.OBBAABBIntersect(in volume, in cellAABB, in bounds2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00016FE0 File Offset: 0x000151E0
		private void DrawProbeDebug(Camera camera, Texture exposureTexture)
		{
			if (!this.enabledBySRP || !this.isInitialized)
			{
				return;
			}
			bool drawProbes = this.probeVolumeDebug.drawProbes;
			object obj = drawProbes || this.probeVolumeDebug.drawVirtualOffsetPush || this.probeVolumeDebug.drawProbeSamplingDebug;
			int adjustmentVolumeCount = 0;
			Vector4[] adjustmentVolumeBounds = ProbeReferenceVolume.s_BoundsArray;
			object obj2 = obj;
			if (obj2 == null && this.probeVolumeDebug.autoDrawProbes)
			{
				ProbeReferenceVolume.UpdateDebugFromSelection(ref adjustmentVolumeBounds, ref adjustmentVolumeCount);
				drawProbes |= adjustmentVolumeCount != 0;
			}
			if (obj2 == null && !drawProbes)
			{
				return;
			}
			GeometryUtility.CalculateFrustumPlanes(camera, this.m_DebugFrustumPlanes);
			this.m_DebugMaterial.shaderKeywords = null;
			if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL1)
			{
				this.m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L1");
			}
			else if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				this.m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L2");
			}
			this.m_DebugMaterial.renderQueue = 3000;
			this.m_DebugOffsetMaterial.renderQueue = 3000;
			this.m_ProbeSamplingDebugMaterial.renderQueue = 3000;
			this.m_ProbeSamplingDebugMaterial02.renderQueue = 3000;
			this.m_DebugMaterial.SetVector("_DebugEmptyProbeData", APVDefinitions.debugEmptyColor);
			if (this.probeVolumeDebug.drawProbeSamplingDebug)
			{
				this.m_ProbeSamplingDebugMaterial.SetInt("_ShadingMode", (int)this.probeVolumeDebug.probeShading);
				this.m_ProbeSamplingDebugMaterial.SetInt("_RenderingLayerMask", (int)this.probeVolumeDebug.samplingRenderingLayer);
				this.m_ProbeSamplingDebugMaterial.SetVector("_DebugArrowColor", new Vector4(1f, 1f, 1f, 1f));
				this.m_ProbeSamplingDebugMaterial.SetVector("_DebugLocator01Color", new Vector4(1f, 1f, 1f, 1f));
				this.m_ProbeSamplingDebugMaterial.SetVector("_DebugLocator02Color", new Vector4(0.3f, 0.3f, 0.3f, 1f));
				this.m_ProbeSamplingDebugMaterial.SetFloat("_ProbeSize", this.probeVolumeDebug.probeSamplingDebugSize);
				this.m_ProbeSamplingDebugMaterial.SetTexture("_NumbersTex", this.m_DisplayNumbersTexture);
				this.m_ProbeSamplingDebugMaterial.SetInt("_DebugSamplingNoise", Convert.ToInt32(this.probeVolumeDebug.debugWithSamplingNoise));
				this.m_ProbeSamplingDebugMaterial.SetInt("_ForceDebugNormalViewBias", 0);
				this.m_ProbeSamplingDebugMaterial.SetBuffer("_positionNormalBuffer", ProbeReferenceVolume.probeSamplingDebugData.positionNormalBuffer);
				Graphics.DrawMesh(this.m_DebugProbeSamplingMesh, new Vector4(0f, 0f, 0f, 1f), Quaternion.identity, this.m_ProbeSamplingDebugMaterial, 0, camera);
				Graphics.ClearRandomWriteTargets();
			}
			int minAvailableSubdiv = ((this.cells.Count > 0) ? (this.GetMaxSubdivision() - 1) : 0);
			foreach (ProbeReferenceVolume.Cell cell in this.cells.Values)
			{
				minAvailableSubdiv = Mathf.Min(minAvailableSubdiv, cell.desc.minSubdiv);
			}
			int maxSubdivToVisualize = Mathf.Max(0, Mathf.Min(this.probeVolumeDebug.maxSubdivToVisualize, this.GetMaxSubdivision() - 1));
			int minSubdivToVisualize = Mathf.Clamp(this.probeVolumeDebug.minSubdivToVisualize, minAvailableSubdiv, maxSubdivToVisualize);
			this.m_MaxSubdivVisualizedIsMaxAvailable = maxSubdivToVisualize == this.GetMaxSubdivision() - 1;
			bool adjustmentCulling = drawProbes && !this.probeVolumeDebug.drawProbes && this.probeVolumeDebug.isolationProbeDebug;
			foreach (ProbeReferenceVolume.Cell cell2 in this.cells.Values)
			{
				if (!this.ShouldCullCell(cell2.desc.position, camera.transform, this.m_DebugFrustumPlanes) && (!adjustmentCulling || !this.ShouldCullCell(cell2.desc.position, adjustmentVolumeBounds, adjustmentVolumeCount)))
				{
					ProbeReferenceVolume.CellInstancedDebugProbes debug = this.CreateInstancedProbes(cell2);
					if (debug != null)
					{
						for (int i = 0; i < debug.probeBuffers.Count; i++)
						{
							MaterialPropertyBlock props = debug.props[i];
							props.SetInt("_ShadingMode", (int)this.probeVolumeDebug.probeShading);
							props.SetFloat("_ExposureCompensation", this.probeVolumeDebug.exposureCompensation);
							props.SetFloat("_ProbeSize", this.probeVolumeDebug.probeSize);
							props.SetFloat("_CullDistance", this.probeVolumeDebug.probeCullingDistance);
							props.SetInt("_MaxAllowedSubdiv", maxSubdivToVisualize);
							props.SetInt("_MinAllowedSubdiv", minSubdivToVisualize);
							props.SetFloat("_ValidityThreshold", this.m_CurrentBakingSet.settings.dilationSettings.dilationValidityThreshold);
							props.SetInt("_RenderingLayerMask", (int)this.probeVolumeDebug.visibleLayers);
							props.SetFloat("_OffsetSize", this.probeVolumeDebug.offsetSize);
							props.SetTexture("_ExposureTexture", exposureTexture);
							if (drawProbes)
							{
								this.m_DebugMaterial.SetVectorArray("_TouchupVolumeBounds", adjustmentVolumeBounds);
								this.m_DebugMaterial.SetInt("_AdjustmentVolumeCount", this.probeVolumeDebug.isolationProbeDebug ? adjustmentVolumeCount : 0);
								this.m_DebugMaterial.SetVector("_ScreenSize", new Vector4((float)camera.pixelWidth, (float)camera.pixelHeight, 1f / (float)camera.pixelWidth, 1f / (float)camera.pixelHeight));
								Matrix4x4[] probeBuffer = debug.probeBuffers[i];
								this.m_DebugMaterial.SetInt("_DebugProbeVolumeSampling", 0);
								this.m_DebugMaterial.SetBuffer("_positionNormalBuffer", ProbeReferenceVolume.probeSamplingDebugData.positionNormalBuffer);
								Graphics.DrawMeshInstanced(this.debugMesh, 0, this.m_DebugMaterial, probeBuffer, probeBuffer.Length, props, ShadowCastingMode.Off, false, 0, camera, LightProbeUsage.Off, null);
							}
							if (this.probeVolumeDebug.drawProbeSamplingDebug)
							{
								Matrix4x4[] probeBuffer2 = debug.probeBuffers[i];
								this.m_ProbeSamplingDebugMaterial02.SetInt("_DebugProbeVolumeSampling", 1);
								props.SetInt("_ShadingMode", 0);
								props.SetFloat("_ProbeSize", this.probeVolumeDebug.probeSamplingDebugSize);
								props.SetInt("_DebugSamplingNoise", Convert.ToInt32(this.probeVolumeDebug.debugWithSamplingNoise));
								props.SetInt("_RenderingLayerMask", (int)this.probeVolumeDebug.samplingRenderingLayer);
								this.m_ProbeSamplingDebugMaterial02.SetBuffer("_positionNormalBuffer", ProbeReferenceVolume.probeSamplingDebugData.positionNormalBuffer);
								Graphics.DrawMeshInstanced(this.debugMesh, 0, this.m_ProbeSamplingDebugMaterial02, probeBuffer2, probeBuffer2.Length, props, ShadowCastingMode.Off, false, 0, camera, LightProbeUsage.Off, null);
							}
							if (this.probeVolumeDebug.drawVirtualOffsetPush)
							{
								this.m_DebugOffsetMaterial.SetVectorArray("_TouchupVolumeBounds", adjustmentVolumeBounds);
								this.m_DebugOffsetMaterial.SetInt("_AdjustmentVolumeCount", this.probeVolumeDebug.isolationProbeDebug ? adjustmentVolumeCount : 0);
								Matrix4x4[] offsetBuffer = debug.offsetBuffers[i];
								Graphics.DrawMeshInstanced(this.m_DebugOffsetMesh, 0, this.m_DebugOffsetMaterial, offsetBuffer, offsetBuffer.Length, props, ShadowCastingMode.Off, false, 0, camera, LightProbeUsage.Off, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00017710 File Offset: 0x00015910
		internal void ResetDebugViewToMaxSubdiv()
		{
			if (this.m_MaxSubdivVisualizedIsMaxAvailable)
			{
				this.probeVolumeDebug.maxSubdivToVisualize = this.GetMaxSubdivision() - 1;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001772D File Offset: 0x0001592D
		private void ClearDebugData()
		{
			this.realtimeSubdivisionInfo.Clear();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001773C File Offset: 0x0001593C
		private ProbeReferenceVolume.CellInstancedDebugProbes CreateInstancedProbes(ProbeReferenceVolume.Cell cell)
		{
			if (cell.debugProbes != null)
			{
				return cell.debugProbes;
			}
			int maxSubdiv = this.GetMaxSubdivision() - 1;
			if (!cell.data.bricks.IsCreated || cell.data.bricks.Length == 0 || !cell.data.probePositions.IsCreated || !cell.loaded)
			{
				return null;
			}
			List<Matrix4x4[]> probeBuffers = new List<Matrix4x4[]>();
			List<Matrix4x4[]> offsetBuffers = new List<Matrix4x4[]>();
			List<MaterialPropertyBlock> props = new List<MaterialPropertyBlock>();
			List<ProbeBrickPool.BrickChunkAlloc> chunks = cell.poolInfo.chunkList;
			Vector4[] texels = new Vector4[511];
			float[] layer = new float[511];
			float[] validity = new float[511];
			float[] dilationThreshold = new float[511];
			float[] relativeSize = new float[511];
			float[] touchupUpVolumeAction = ((cell.data.touchupVolumeInteraction.Length > 0) ? new float[511] : null);
			Vector4[] offsets = ((cell.data.offsetVectors.Length > 0) ? new Vector4[511] : null);
			List<Matrix4x4> probeBuffer = new List<Matrix4x4>();
			List<Matrix4x4> offsetBuffer = new List<Matrix4x4>();
			ProbeReferenceVolume.CellInstancedDebugProbes debugData = new ProbeReferenceVolume.CellInstancedDebugProbes();
			debugData.probeBuffers = probeBuffers;
			debugData.offsetBuffers = offsetBuffers;
			debugData.props = props;
			int chunkSizeInProbes = ProbeBrickPool.GetChunkSizeInProbeCount();
			Vector3Int loc = ProbeBrickPool.ProbeCountToDataLocSize(chunkSizeInProbes);
			float baseThreshold = this.m_CurrentBakingSet.settings.dilationSettings.dilationValidityThreshold;
			int idxInBatch = 0;
			int globalIndex = 0;
			int brickCount = cell.desc.probeCount / 64;
			int bx = 0;
			int by = 0;
			int bz = 0;
			for (int brickIndex = 0; brickIndex < brickCount; brickIndex++)
			{
				int brickSize = cell.data.bricks[brickIndex].subdivisionLevel;
				int chunkIndex = brickIndex / ProbeBrickPool.GetChunkSizeInBrickCount();
				ProbeBrickPool.BrickChunkAlloc chunk = chunks[chunkIndex];
				Vector3Int brickStart = new Vector3Int(chunk.x + bx, chunk.y + by, chunk.z + bz);
				for (int z = 0; z < 4; z++)
				{
					for (int y = 0; y < 4; y++)
					{
						for (int x = 0; x < 4; x++)
						{
							Vector3Int texelLoc = new Vector3Int(brickStart.x + x, brickStart.y + y, brickStart.z + z);
							int probeFlatIndex = chunkIndex * chunkSizeInProbes + (bx + x) + loc.x * (by + y + loc.y * (bz + z));
							Vector3 position = cell.data.probePositions[probeFlatIndex] - this.ProbeOffset();
							probeBuffer.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * (0.3f * (float)(brickSize + 1))));
							validity[idxInBatch] = cell.data.validity[probeFlatIndex];
							dilationThreshold[idxInBatch] = baseThreshold;
							texels[idxInBatch] = new Vector4((float)texelLoc.x, (float)texelLoc.y, (float)texelLoc.z, (float)brickSize);
							relativeSize[idxInBatch] = (float)brickSize / (float)maxSubdiv;
							layer[idxInBatch] = math.asfloat((cell.data.layer.Length > 0) ? ((uint)cell.data.layer[probeFlatIndex]) : uint.MaxValue);
							if (touchupUpVolumeAction != null)
							{
								touchupUpVolumeAction[idxInBatch] = cell.data.touchupVolumeInteraction[probeFlatIndex];
								dilationThreshold[idxInBatch] = ((touchupUpVolumeAction[idxInBatch] > 1f) ? (touchupUpVolumeAction[idxInBatch] - 1f) : baseThreshold);
							}
							if (offsets != null)
							{
								Vector3 offset = cell.data.offsetVectors[probeFlatIndex];
								offsets[idxInBatch] = offset;
								if (offset.sqrMagnitude < 1E-06f)
								{
									offsetBuffer.Add(Matrix4x4.identity);
								}
								else
								{
									Quaternion orientation = Quaternion.LookRotation(-offset);
									Vector3 scale = new Vector3(0.5f, 0.5f, offset.magnitude);
									offsetBuffer.Add(Matrix4x4.TRS(position + offset, orientation, scale));
								}
							}
							idxInBatch++;
							if (probeBuffer.Count >= 511 || globalIndex == cell.desc.probeCount - 1)
							{
								idxInBatch = 0;
								MaterialPropertyBlock prop = new MaterialPropertyBlock();
								prop.SetFloatArray("_Validity", validity);
								prop.SetFloatArray("_RenderingLayer", layer);
								prop.SetFloatArray("_DilationThreshold", dilationThreshold);
								prop.SetFloatArray("_TouchupedByVolume", touchupUpVolumeAction);
								prop.SetFloatArray("_RelativeSize", relativeSize);
								prop.SetVectorArray("_IndexInAtlas", texels);
								if (offsets != null)
								{
									prop.SetVectorArray("_Offset", offsets);
								}
								props.Add(prop);
								probeBuffers.Add(probeBuffer.ToArray());
								probeBuffer.Clear();
								offsetBuffers.Add(offsetBuffer.ToArray());
								offsetBuffer.Clear();
							}
							globalIndex++;
						}
					}
				}
				bx += 4;
				if (bx >= loc.x)
				{
					bx = 0;
					by += 4;
					if (by >= loc.y)
					{
						by = 0;
						bz += 4;
						if (bz >= loc.z)
						{
							bx = 0;
							by = 0;
							bz = 0;
						}
					}
				}
			}
			cell.debugProbes = debugData;
			return debugData;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00015DD1 File Offset: 0x00013FD1
		private void OnClearLightingdata()
		{
			this.ClearDebugData();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00017C83 File Offset: 0x00015E83
		public void EnableMaxCellStreaming(bool value)
		{
			this.m_LoadMaxCellsPerFrame = value;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00017C8C File Offset: 0x00015E8C
		public void SetNumberOfCellsLoadedPerFrame(int numberOfCells)
		{
			this.m_NumberOfCellsLoadedPerFrame = Mathf.Min(10, Mathf.Max(1, numberOfCells));
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00017CA2 File Offset: 0x00015EA2
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00017C83 File Offset: 0x00015E83
		public bool loadMaxCellsPerFrame
		{
			get
			{
				return this.m_LoadMaxCellsPerFrame;
			}
			set
			{
				this.m_LoadMaxCellsPerFrame = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00017CAA File Offset: 0x00015EAA
		private int numberOfCellsLoadedPerFrame
		{
			get
			{
				if (!this.m_LoadMaxCellsPerFrame)
				{
					return this.m_NumberOfCellsLoadedPerFrame;
				}
				return this.cells.Count;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00017CC6 File Offset: 0x00015EC6
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00017CCE File Offset: 0x00015ECE
		public int numberOfCellsBlendedPerFrame
		{
			get
			{
				return this.m_NumberOfCellsBlendedPerFrame;
			}
			set
			{
				this.m_NumberOfCellsBlendedPerFrame = Mathf.Max(1, value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00017CDD File Offset: 0x00015EDD
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00017CE5 File Offset: 0x00015EE5
		public float turnoverRate
		{
			get
			{
				return this.m_TurnoverRate;
			}
			set
			{
				this.m_TurnoverRate = Mathf.Clamp01(value);
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00017CF3 File Offset: 0x00015EF3
		private void InitStreaming()
		{
			this.m_OnStreamingComplete = new ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate(this.OnStreamingComplete);
			this.m_OnBlendingStreamingComplete = new ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate(this.OnBlendingStreamingComplete);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00017D1C File Offset: 0x00015F1C
		private void CleanupStreaming()
		{
			this.ProcessNewRequests();
			this.UpdateActiveRequests(null);
			for (int i = 0; i < this.m_StreamingRequestsPool.countAll; i++)
			{
				this.m_StreamingRequestsPool.Get().Dispose();
			}
			if (this.m_ScratchBufferPool != null)
			{
				this.m_ScratchBufferPool.Cleanup();
				this.m_ScratchBufferPool = null;
			}
			this.m_StreamingRequestsPool = new ObjectPool<ProbeReferenceVolume.CellStreamingRequest>(delegate(ProbeReferenceVolume.CellStreamingRequest val)
			{
				val.Clear();
			}, null, true);
			this.m_ActiveStreamingRequests.Clear();
			this.m_StreamingQueue.Clear();
			this.m_OnStreamingComplete = null;
			this.m_OnBlendingStreamingComplete = null;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00017DC8 File Offset: 0x00015FC8
		internal unsafe void ScenarioBlendingChanged(bool scenarioChanged)
		{
			if (scenarioChanged)
			{
				this.UnloadAllBlendingCells();
				for (int i = 0; i < this.m_ToBeLoadedBlendingCells.size; i++)
				{
					this.m_ToBeLoadedBlendingCells[i]->blendingInfo.ForceReupload();
				}
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00017E0C File Offset: 0x0001600C
		private static void ComputeCellStreamingScore(ProbeReferenceVolume.Cell cell, Vector3 cameraPosition, Vector3 cameraDirection)
		{
			Vector3 cameraToCell = (cell.desc.position - cameraPosition).normalized;
			cell.streamingInfo.streamingScore = Vector3.Distance(cameraPosition, cell.desc.position);
			cell.streamingInfo.streamingScore *= 2f - Vector3.Dot(cameraDirection, cameraToCell);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00017E78 File Offset: 0x00016078
		private unsafe void ComputeStreamingScore(Vector3 cameraPosition, Vector3 cameraDirection, DynamicArray<ProbeReferenceVolume.Cell> cells)
		{
			for (int i = 0; i < cells.size; i++)
			{
				ProbeReferenceVolume.ComputeCellStreamingScore(*cells[i], cameraPosition, cameraDirection);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00017EA8 File Offset: 0x000160A8
		private unsafe void ComputeBestToBeLoadedCells(Vector3 cameraPosition, Vector3 cameraDirection)
		{
			this.m_BestToBeLoadedCells.Clear();
			this.m_BestToBeLoadedCells.Reserve(this.m_ToBeLoadedCells.size, false);
			foreach (ref ProbeReferenceVolume.Cell ptr in this.m_ToBeLoadedCells)
			{
				ProbeReferenceVolume.Cell cell = ptr;
				ProbeReferenceVolume.ComputeCellStreamingScore(cell, cameraPosition, cameraDirection);
				this.minStreamingScore = Mathf.Min(this.minStreamingScore, cell.streamingInfo.streamingScore);
				this.maxStreamingScore = Mathf.Max(this.maxStreamingScore, cell.streamingInfo.streamingScore);
				int currentBestCellsSize = Math.Min(this.m_BestToBeLoadedCells.size, this.numberOfCellsLoadedPerFrame);
				int index = 0;
				while (index < currentBestCellsSize && cell.streamingInfo.streamingScore >= this.m_BestToBeLoadedCells[index]->streamingInfo.streamingScore)
				{
					index++;
				}
				if (index < this.numberOfCellsLoadedPerFrame)
				{
					this.m_BestToBeLoadedCells.Insert(index, cell);
				}
				if (this.m_BestToBeLoadedCells.size > this.numberOfCellsLoadedPerFrame)
				{
					this.m_BestToBeLoadedCells.Resize(this.numberOfCellsLoadedPerFrame, false);
				}
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00017FC0 File Offset: 0x000161C0
		private unsafe void ComputeStreamingScoreAndWorseLoadedCells(Vector3 cameraPosition, Vector3 cameraDirection)
		{
			this.m_WorseLoadedCells.Clear();
			this.m_WorseLoadedCells.Reserve(this.m_LoadedCells.size, false);
			int requiredSHChunks = 0;
			int requiredIndexChunks = 0;
			foreach (ref ProbeReferenceVolume.Cell ptr in this.m_BestToBeLoadedCells)
			{
				ProbeReferenceVolume.Cell cell = ptr;
				requiredSHChunks += cell.desc.shChunkCount;
				requiredIndexChunks += cell.desc.indexChunkCount;
			}
			foreach (ref ProbeReferenceVolume.Cell ptr2 in this.m_LoadedCells)
			{
				ProbeReferenceVolume.Cell cell2 = ptr2;
				ProbeReferenceVolume.ComputeCellStreamingScore(cell2, cameraPosition, cameraDirection);
				this.minStreamingScore = Mathf.Min(this.minStreamingScore, cell2.streamingInfo.streamingScore);
				this.maxStreamingScore = Mathf.Max(this.maxStreamingScore, cell2.streamingInfo.streamingScore);
				int currentWorseSize = this.m_WorseLoadedCells.size;
				int index = 0;
				while (index < currentWorseSize && cell2.streamingInfo.streamingScore <= this.m_WorseLoadedCells[index]->streamingInfo.streamingScore)
				{
					index++;
				}
				this.m_WorseLoadedCells.Insert(index, cell2);
				int currentSHChunks = 0;
				int currentIndexChunks = 0;
				int newSize = 0;
				for (int i = 0; i < this.m_WorseLoadedCells.size; i++)
				{
					ProbeReferenceVolume.Cell worseCell = *this.m_WorseLoadedCells[i];
					currentSHChunks += worseCell.desc.shChunkCount;
					currentIndexChunks += worseCell.desc.indexChunkCount;
					if (currentSHChunks >= requiredSHChunks && currentIndexChunks >= requiredIndexChunks)
					{
						newSize = i + 1;
						break;
					}
				}
				if (newSize != 0)
				{
					this.m_WorseLoadedCells.Resize(newSize, false);
				}
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00018168 File Offset: 0x00016368
		private unsafe void ComputeBlendingScore(DynamicArray<ProbeReferenceVolume.Cell> cells, float worstScore)
		{
			float factor = this.scenarioBlendingFactor;
			for (int i = 0; i < cells.size; i++)
			{
				ProbeReferenceVolume.Cell cell = *cells[i];
				ProbeReferenceVolume.CellBlendingInfo blendingInfo = cell.blendingInfo;
				if (factor != blendingInfo.blendingFactor)
				{
					blendingInfo.blendingScore = cell.streamingInfo.streamingScore;
					if (blendingInfo.ShouldPrioritize())
					{
						blendingInfo.blendingScore -= worstScore;
					}
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000181D0 File Offset: 0x000163D0
		private bool TryLoadCell(ProbeReferenceVolume.Cell cell, ref int shBudget, ref int indexBudget, DynamicArray<ProbeReferenceVolume.Cell> loadedCells)
		{
			if (cell.poolInfo.shChunkCount <= shBudget && cell.indexInfo.indexChunkCount <= indexBudget && this.LoadCell(cell, true))
			{
				loadedCells.Add(in cell);
				shBudget -= cell.poolInfo.shChunkCount;
				indexBudget -= cell.indexInfo.indexChunkCount;
				return true;
			}
			return false;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00018232 File Offset: 0x00016432
		private void UnloadBlendingCell(ProbeReferenceVolume.Cell cell, DynamicArray<ProbeReferenceVolume.Cell> unloadedCells)
		{
			this.UnloadBlendingCell(cell);
			unloadedCells.Add(in cell);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00018244 File Offset: 0x00016444
		private bool TryLoadBlendingCell(ProbeReferenceVolume.Cell cell, DynamicArray<ProbeReferenceVolume.Cell> loadedCells)
		{
			if (!cell.UpdateCellScenarioData(this.lightingScenario, this.m_CurrentBakingSet.otherScenario))
			{
				return false;
			}
			if (!this.AddBlendingBricks(cell))
			{
				return false;
			}
			loadedCells.Add(in cell);
			return true;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00018278 File Offset: 0x00016478
		private unsafe void ComputeMinMaxStreamingScore()
		{
			this.minStreamingScore = float.MaxValue;
			this.maxStreamingScore = float.MinValue;
			if (this.m_ToBeLoadedCells.size != 0)
			{
				this.minStreamingScore = Mathf.Min(this.minStreamingScore, this.m_ToBeLoadedCells[0]->streamingInfo.streamingScore);
				this.maxStreamingScore = Mathf.Max(this.maxStreamingScore, this.m_ToBeLoadedCells[this.m_ToBeLoadedCells.size - 1]->streamingInfo.streamingScore);
			}
			if (this.m_LoadedCells.size != 0)
			{
				this.minStreamingScore = Mathf.Min(this.minStreamingScore, this.m_LoadedCells[0]->streamingInfo.streamingScore);
				this.maxStreamingScore = Mathf.Max(this.maxStreamingScore, this.m_LoadedCells[this.m_LoadedCells.size - 1]->streamingInfo.streamingScore);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001836D File Offset: 0x0001656D
		public void UpdateCellStreaming(CommandBuffer cmd, Camera camera)
		{
			this.UpdateCellStreaming(cmd, camera, null);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00018378 File Offset: 0x00016578
		public unsafe void UpdateCellStreaming(CommandBuffer cmd, Camera camera, ProbeVolumesOptions options)
		{
			if (!this.isInitialized || this.m_CurrentBakingSet == null)
			{
				return;
			}
			using (new ProfilingScope(ProfilingSampler.Get<CoreProfileId>(CoreProfileId.APVCellStreamingUpdate)))
			{
				Vector3 cameraPosition = camera.transform.position;
				if (!this.probeVolumeDebug.freezeStreaming)
				{
					this.m_FrozenCameraPosition = cameraPosition;
					this.m_FrozenCameraDirection = camera.transform.forward;
				}
				Vector3 offset = this.ProbeOffset() + ((options != null) ? options.worldOffset.value : Vector3.zero);
				Vector3 cameraPositionCellSpace = (this.m_FrozenCameraPosition - offset) / this.MaxBrickSize() - Vector3.one * 0.5f;
				DynamicArray<ProbeReferenceVolume.Cell> bestUnloadedCells;
				if (this.m_LoadMaxCellsPerFrame)
				{
					this.ComputeStreamingScore(cameraPositionCellSpace, this.m_FrozenCameraDirection, this.m_ToBeLoadedCells);
					this.m_ToBeLoadedCells.QuickSort<ProbeReferenceVolume.Cell>();
					bestUnloadedCells = this.m_ToBeLoadedCells;
				}
				else
				{
					this.minStreamingScore = float.MaxValue;
					this.maxStreamingScore = float.MinValue;
					this.ComputeBestToBeLoadedCells(cameraPositionCellSpace, this.m_FrozenCameraDirection);
					bestUnloadedCells = this.m_BestToBeLoadedCells;
				}
				int indexChunkBudget = this.m_Index.GetRemainingChunkCount();
				int shChunkBudget = this.m_Pool.GetRemainingChunkCount();
				int cellCountToLoad = Mathf.Min(this.numberOfCellsLoadedPerFrame, bestUnloadedCells.size);
				bool didRecomputeScoresForLoadedCells = false;
				if (this.m_SupportGPUStreaming)
				{
					if (this.m_IndexDefragmentationInProgress)
					{
						this.UpdateIndexDefragmentation();
					}
					else
					{
						bool needComputeFragmentation = false;
						while (this.m_TempCellToLoadList.size < cellCountToLoad)
						{
							ProbeReferenceVolume.Cell cellInfo = *bestUnloadedCells[this.m_TempCellToLoadList.size];
							if (!this.TryLoadCell(cellInfo, ref shChunkBudget, ref indexChunkBudget, this.m_TempCellToLoadList))
							{
								break;
							}
						}
						if (this.m_TempCellToLoadList.size != cellCountToLoad && !this.m_IndexDefragmentationInProgress)
						{
							DynamicArray<ProbeReferenceVolume.Cell> worseLoadedCells;
							if (this.m_LoadMaxCellsPerFrame)
							{
								this.ComputeStreamingScore(cameraPositionCellSpace, this.m_FrozenCameraDirection, this.m_LoadedCells);
								this.m_LoadedCells.QuickSort<ProbeReferenceVolume.Cell>();
								worseLoadedCells = this.m_LoadedCells;
							}
							else
							{
								this.ComputeStreamingScoreAndWorseLoadedCells(cameraPositionCellSpace, this.m_FrozenCameraDirection);
								worseLoadedCells = this.m_WorseLoadedCells;
							}
							didRecomputeScoresForLoadedCells = true;
							int pendingUnloadCount = 0;
							while (this.m_TempCellToLoadList.size < cellCountToLoad && worseLoadedCells.size - pendingUnloadCount != 0)
							{
								int worseCellIndex = (this.m_LoadMaxCellsPerFrame ? (worseLoadedCells.size - pendingUnloadCount - 1) : pendingUnloadCount);
								ProbeReferenceVolume.Cell worseLoadedCell = *worseLoadedCells[worseCellIndex];
								ProbeReferenceVolume.Cell bestUnloadedCell = *bestUnloadedCells[this.m_TempCellToLoadList.size];
								if (worseLoadedCell.streamingInfo.streamingScore <= bestUnloadedCell.streamingInfo.streamingScore)
								{
									break;
								}
								while (pendingUnloadCount < worseLoadedCells.size && worseLoadedCell.streamingInfo.streamingScore > bestUnloadedCell.streamingInfo.streamingScore && (shChunkBudget < bestUnloadedCell.desc.shChunkCount || indexChunkBudget < bestUnloadedCell.desc.indexChunkCount))
								{
									bool verboseStreamingLog = this.probeVolumeDebug.verboseStreamingLog;
									pendingUnloadCount++;
									this.UnloadCell(worseLoadedCell);
									shChunkBudget += worseLoadedCell.desc.shChunkCount;
									indexChunkBudget += worseLoadedCell.desc.indexChunkCount;
									this.m_TempCellToUnloadList.Add(in worseLoadedCell);
									worseCellIndex = (this.m_LoadMaxCellsPerFrame ? (worseLoadedCells.size - pendingUnloadCount - 1) : pendingUnloadCount);
									if (pendingUnloadCount < worseLoadedCells.size)
									{
										worseLoadedCell = *worseLoadedCells[worseCellIndex];
									}
								}
								if (shChunkBudget >= bestUnloadedCell.desc.shChunkCount && indexChunkBudget >= bestUnloadedCell.desc.indexChunkCount && !this.TryLoadCell(bestUnloadedCell, ref shChunkBudget, ref indexChunkBudget, this.m_TempCellToLoadList))
								{
									needComputeFragmentation = true;
									break;
								}
							}
						}
						if (needComputeFragmentation)
						{
							this.m_Index.ComputeFragmentationRate();
						}
						if (this.m_Index.fragmentationRate >= 0.2f)
						{
							this.StartIndexDefragmentation();
						}
					}
				}
				else
				{
					int i = 0;
					while (i < cellCountToLoad)
					{
						ProbeReferenceVolume.Cell cellInfo2 = *this.m_ToBeLoadedCells[this.m_TempCellToLoadList.size];
						if (!this.TryLoadCell(cellInfo2, ref shChunkBudget, ref indexChunkBudget, this.m_TempCellToLoadList))
						{
							if (i > 0)
							{
								Debug.LogWarning("Max Memory Budget for Adaptive Probe Volumes has been reached, but there is still more data to load. Consider either increasing the Memory Budget, enabling GPU Streaming, or reducing the probe count.");
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
				if (!didRecomputeScoresForLoadedCells && this.supportScenarioBlending)
				{
					this.ComputeStreamingScore(cameraPositionCellSpace, this.m_FrozenCameraDirection, this.m_LoadedCells);
				}
				if (this.m_LoadMaxCellsPerFrame)
				{
					this.ComputeMinMaxStreamingScore();
				}
				foreach (ref ProbeReferenceVolume.Cell ptr in this.m_TempCellToLoadList)
				{
					ProbeReferenceVolume.Cell cell = ptr;
					this.m_ToBeLoadedCells.Remove(cell);
				}
				this.m_LoadedCells.AddRange(this.m_TempCellToLoadList);
				if (this.m_TempCellToUnloadList.size > 0)
				{
					foreach (ref ProbeReferenceVolume.Cell ptr2 in this.m_TempCellToUnloadList)
					{
						ProbeReferenceVolume.Cell cell2 = ptr2;
						this.m_LoadedCells.Remove(cell2);
					}
					this.ComputeCellGlobalInfo();
				}
				this.m_ToBeLoadedCells.AddRange(this.m_TempCellToUnloadList);
				this.m_TempCellToLoadList.Clear();
				this.m_TempCellToUnloadList.Clear();
				this.UpdateDiskStreaming(cmd);
			}
			if (this.supportScenarioBlending)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<CoreProfileId>(CoreProfileId.APVScenarioBlendingUpdate)))
				{
					this.UpdateBlendingCellStreaming(cmd);
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000188C8 File Offset: 0x00016AC8
		private unsafe int FindWorstBlendingCellToBeLoaded()
		{
			int idx = -1;
			float worstBlending = -1f;
			float factor = this.scenarioBlendingFactor;
			for (int i = this.m_TempBlendingCellToLoadList.size; i < this.m_ToBeLoadedBlendingCells.size; i++)
			{
				float score = Mathf.Abs(this.m_ToBeLoadedBlendingCells[i]->blendingInfo.blendingFactor - factor);
				if (score > worstBlending)
				{
					idx = i;
					if (this.m_ToBeLoadedBlendingCells[i]->blendingInfo.ShouldReupload())
					{
						break;
					}
					worstBlending = score;
				}
			}
			return idx;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00018948 File Offset: 0x00016B48
		private static int BlendingComparer(ProbeReferenceVolume.Cell a, ProbeReferenceVolume.Cell b)
		{
			if (a.blendingInfo.blendingScore < b.blendingInfo.blendingScore)
			{
				return -1;
			}
			if (a.blendingInfo.blendingScore > b.blendingInfo.blendingScore)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00018980 File Offset: 0x00016B80
		private unsafe void UpdateBlendingCellStreaming(CommandBuffer cmd)
		{
			float num = ((this.m_LoadedCells.size != 0) ? this.m_LoadedCells[this.m_LoadedCells.size - 1]->streamingInfo.streamingScore : 0f);
			float worstToBeLoaded = ((this.m_ToBeLoadedCells.size != 0) ? this.m_ToBeLoadedCells[this.m_ToBeLoadedCells.size - 1]->streamingInfo.streamingScore : 0f);
			float worstScore = Mathf.Max(num, worstToBeLoaded);
			this.ComputeBlendingScore(this.m_ToBeLoadedBlendingCells, worstScore);
			this.ComputeBlendingScore(this.m_LoadedBlendingCells, worstScore);
			this.m_ToBeLoadedBlendingCells.QuickSort(ProbeReferenceVolume.s_BlendingComparer);
			this.m_LoadedBlendingCells.QuickSort(ProbeReferenceVolume.s_BlendingComparer);
			int cellCountToLoad = Mathf.Min(this.numberOfCellsLoadedPerFrame, this.m_ToBeLoadedBlendingCells.size);
			while (this.m_TempBlendingCellToLoadList.size < cellCountToLoad)
			{
				ProbeReferenceVolume.Cell blendingCell = *this.m_ToBeLoadedBlendingCells[this.m_TempBlendingCellToLoadList.size];
				if (!this.TryLoadBlendingCell(blendingCell, this.m_TempBlendingCellToLoadList))
				{
					break;
				}
			}
			if (this.m_TempBlendingCellToLoadList.size != cellCountToLoad)
			{
				int turnoverOffset = -1;
				int idx = (int)((float)this.m_LoadedBlendingCells.size * (1f - this.turnoverRate));
				ProbeReferenceVolume.Cell worstNoTurnover = ((idx < this.m_LoadedBlendingCells.size) ? (*this.m_LoadedBlendingCells[idx]) : null);
				while (this.m_TempBlendingCellToLoadList.size < cellCountToLoad && this.m_LoadedBlendingCells.size - this.m_TempBlendingCellToUnloadList.size != 0)
				{
					ProbeReferenceVolume.Cell worstCellLoaded = *this.m_LoadedBlendingCells[this.m_LoadedBlendingCells.size - this.m_TempBlendingCellToUnloadList.size - 1];
					ProbeReferenceVolume.Cell bestCellToBeLoaded = *this.m_ToBeLoadedBlendingCells[this.m_TempBlendingCellToLoadList.size];
					if (bestCellToBeLoaded.blendingInfo.blendingScore >= (worstNoTurnover ?? worstCellLoaded).blendingInfo.blendingScore)
					{
						if (worstNoTurnover == null)
						{
							break;
						}
						if (turnoverOffset == -1)
						{
							turnoverOffset = this.FindWorstBlendingCellToBeLoaded();
						}
						bestCellToBeLoaded = *this.m_ToBeLoadedBlendingCells[turnoverOffset];
						if (bestCellToBeLoaded.blendingInfo.IsUpToDate())
						{
							break;
						}
					}
					if (worstCellLoaded.streamingInfo.IsBlendingStreaming())
					{
						break;
					}
					this.UnloadBlendingCell(worstCellLoaded, this.m_TempBlendingCellToUnloadList);
					bool verboseStreamingLog = this.probeVolumeDebug.verboseStreamingLog;
					if (this.TryLoadBlendingCell(bestCellToBeLoaded, this.m_TempBlendingCellToLoadList) && turnoverOffset != -1)
					{
						*this.m_ToBeLoadedBlendingCells[turnoverOffset] = *this.m_ToBeLoadedBlendingCells[this.m_TempBlendingCellToLoadList.size - 1];
						*this.m_ToBeLoadedBlendingCells[this.m_TempBlendingCellToLoadList.size - 1] = bestCellToBeLoaded;
						if (++turnoverOffset >= this.m_ToBeLoadedBlendingCells.size)
						{
							turnoverOffset = this.m_TempBlendingCellToLoadList.size;
						}
					}
				}
				this.m_LoadedBlendingCells.RemoveRange(this.m_LoadedBlendingCells.size - this.m_TempBlendingCellToUnloadList.size, this.m_TempBlendingCellToUnloadList.size);
			}
			this.m_ToBeLoadedBlendingCells.RemoveRange(0, this.m_TempBlendingCellToLoadList.size);
			this.m_LoadedBlendingCells.AddRange(this.m_TempBlendingCellToLoadList);
			this.m_TempBlendingCellToLoadList.Clear();
			this.m_ToBeLoadedBlendingCells.AddRange(this.m_TempBlendingCellToUnloadList);
			this.m_TempBlendingCellToUnloadList.Clear();
			if (this.m_LoadedBlendingCells.size != 0)
			{
				float factor = this.scenarioBlendingFactor;
				int loadedBlendingCellIndex = 0;
				int blendedCellCount = 0;
				while (blendedCellCount < this.numberOfCellsBlendedPerFrame && loadedBlendingCellIndex < this.m_LoadedBlendingCells.size)
				{
					ProbeReferenceVolume.Cell blendingCell2 = *this.m_LoadedBlendingCells[loadedBlendingCellIndex++];
					if (!blendingCell2.streamingInfo.IsBlendingStreaming() && !blendingCell2.blendingInfo.IsUpToDate())
					{
						bool verboseStreamingLog2 = this.probeVolumeDebug.verboseStreamingLog;
						blendingCell2.blendingInfo.blendingFactor = factor;
						blendingCell2.blendingInfo.MarkUpToDate();
						this.m_BlendingPool.BlendChunks(blendingCell2, this.m_Pool);
						blendedCellCount++;
					}
				}
				this.m_BlendingPool.PerformBlending(cmd, factor, this.m_Pool);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00018D84 File Offset: 0x00016F84
		private static int DefragComparer(ProbeReferenceVolume.Cell a, ProbeReferenceVolume.Cell b)
		{
			if (a.indexInfo.updateInfo.GetNumberOfChunks() > b.indexInfo.updateInfo.GetNumberOfChunks())
			{
				return 1;
			}
			if (a.indexInfo.updateInfo.GetNumberOfChunks() < b.indexInfo.updateInfo.GetNumberOfChunks())
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00018DDC File Offset: 0x00016FDC
		private void StartIndexDefragmentation()
		{
			if (!this.m_SupportGPUStreaming)
			{
				return;
			}
			this.m_IndexDefragmentationInProgress = true;
			this.m_IndexDefragCells.Clear();
			this.m_IndexDefragCells.AddRange(this.m_LoadedCells);
			this.m_IndexDefragCells.QuickSort(ProbeReferenceVolume.s_DefragComparer);
			this.m_DefragIndex.Clear();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00018E30 File Offset: 0x00017030
		private unsafe void UpdateIndexDefragmentation()
		{
			using (new ProfilingScope(ProfilingSampler.Get<CoreProfileId>(CoreProfileId.APVIndexDefragUpdate)))
			{
				this.m_TempIndexDefragCells.Clear();
				int numberOfCellsToProcess = Mathf.Min(this.m_IndexDefragCells.size, this.numberOfCellsLoadedPerFrame);
				int i = 0;
				int processedCells = 0;
				while (i < this.m_IndexDefragCells.size && processedCells < numberOfCellsToProcess)
				{
					ProbeReferenceVolume.Cell cell = *this.m_IndexDefragCells[this.m_IndexDefragCells.size - i - 1];
					this.m_DefragIndex.FindSlotsForEntries(ref cell.indexInfo.updateInfo.entriesInfo);
					this.m_DefragIndex.ReserveChunks(cell.indexInfo.updateInfo.entriesInfo, false);
					if (!cell.streamingInfo.IsStreaming() && !cell.streamingInfo.IsBlendingStreaming())
					{
						this.m_DefragIndex.AddBricks(cell.indexInfo, cell.data.bricks, cell.poolInfo.chunkList, ProbeBrickPool.GetChunkSizeInBrickCount(), this.m_Pool.GetPoolWidth(), this.m_Pool.GetPoolHeight());
						this.m_DefragCellIndices.UpdateCell(cell.indexInfo);
						processedCells++;
					}
					else
					{
						this.m_TempIndexDefragCells.Add(in cell);
					}
					i++;
				}
				this.m_IndexDefragCells.Resize(this.m_IndexDefragCells.size - i, false);
				this.m_IndexDefragCells.AddRange(this.m_TempIndexDefragCells);
				if (this.m_IndexDefragCells.size == 0)
				{
					ProbeBrickIndex oldDefragIndex = this.m_DefragIndex;
					this.m_DefragIndex = this.m_Index;
					this.m_Index = oldDefragIndex;
					ProbeGlobalIndirection oldDefragCellIndices = this.m_DefragCellIndices;
					this.m_DefragCellIndices = this.m_CellIndices;
					this.m_CellIndices = oldDefragCellIndices;
					this.m_IndexDefragmentationInProgress = false;
				}
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00019010 File Offset: 0x00017210
		private void OnStreamingComplete(ProbeReferenceVolume.CellStreamingRequest request, CommandBuffer cmd)
		{
			request.cell.streamingInfo.request = null;
			this.UpdatePoolAndIndex(request.cell, request.scratchBuffer, request.scratchBufferLayout, request.poolIndex, cmd);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00019044 File Offset: 0x00017244
		private void OnBlendingStreamingComplete(ProbeReferenceVolume.CellStreamingRequest request, CommandBuffer cmd)
		{
			this.UpdatePool(cmd, request.cell.blendingInfo.chunkList, request.scratchBuffer, request.scratchBufferLayout, request.poolIndex);
			if (request.poolIndex == 0)
			{
				request.cell.streamingInfo.blendingRequest0 = null;
			}
			else
			{
				request.cell.streamingInfo.blendingRequest1 = null;
			}
			if (request.cell.streamingInfo.blendingRequest0 == null && request.cell.streamingInfo.blendingRequest1 == null && !request.cell.indexInfo.indexUpdated)
			{
				this.UpdateCellIndex(request.cell);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000190E8 File Offset: 0x000172E8
		private void PushDiskStreamingRequest(ProbeReferenceVolume.Cell cell, string scenario, int poolIndex, ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate onStreamingComplete)
		{
			ProbeReferenceVolume.CellStreamingRequest streamingRequest = this.m_StreamingRequestsPool.Get();
			streamingRequest.cell = cell;
			streamingRequest.state = ProbeReferenceVolume.CellStreamingRequest.State.Pending;
			streamingRequest.scenarioData = this.m_CurrentBakingSet.scenarios[scenario];
			streamingRequest.poolIndex = poolIndex;
			streamingRequest.onStreamingComplete = onStreamingComplete;
			if (poolIndex == -1 || poolIndex == 0)
			{
				streamingRequest.streamSharedData = true;
			}
			if (this.probeVolumeDebug.verboseStreamingLog)
			{
			}
			switch (poolIndex)
			{
			case -1:
				cell.streamingInfo.request = streamingRequest;
				break;
			case 0:
				cell.streamingInfo.blendingRequest0 = streamingRequest;
				break;
			case 1:
				cell.streamingInfo.blendingRequest1 = streamingRequest;
				break;
			}
			this.m_StreamingQueue.Enqueue(streamingRequest);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000191A0 File Offset: 0x000173A0
		private void CancelStreamingRequest(ProbeReferenceVolume.Cell cell)
		{
			this.m_Index.RemoveBricks(cell.indexInfo);
			this.m_Pool.Deallocate(cell.poolInfo.chunkList);
			if (cell.streamingInfo.request != null)
			{
				cell.streamingInfo.request.Cancel();
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000191F1 File Offset: 0x000173F1
		private void CancelBlendingStreamingRequest(ProbeReferenceVolume.Cell cell)
		{
			if (cell.streamingInfo.blendingRequest0 != null)
			{
				cell.streamingInfo.blendingRequest0.Cancel();
			}
			if (cell.streamingInfo.blendingRequest1 != null)
			{
				cell.streamingInfo.blendingRequest1.Cancel();
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00019230 File Offset: 0x00017430
		private unsafe bool ProcessDiskStreamingRequest(ProbeReferenceVolume.CellStreamingRequest request)
		{
			int cellIndex = request.cell.desc.index;
			ProbeReferenceVolume.Cell cell = this.cells[cellIndex];
			ProbeReferenceVolume.CellDesc cellDesc = cell.desc;
			ProbeReferenceVolume.CellData cellData = cell.data;
			ProbeReferenceVolume.CellStreamingScratchBuffer cellStreamingScratchBuffer;
			ProbeReferenceVolume.CellStreamingScratchBufferLayout layout;
			if (!this.m_ScratchBufferPool.AllocateScratchBuffer(cellDesc.shChunkCount, out cellStreamingScratchBuffer, out layout, this.m_DiskStreamingUseCompute))
			{
				return false;
			}
			if (!this.m_CurrentBakingSet.HasValidSharedData())
			{
				Debug.LogError("One or more data file missing for baking set " + this.m_CurrentBakingSet.name + ". Cannot load shared data.");
				return false;
			}
			if (!request.scenarioData.HasValidData(this.m_SHBands))
			{
				Debug.LogError(string.Concat(new string[]
				{
					"One or more data file missing for baking set ",
					this.m_CurrentBakingSet.name,
					" scenario ",
					this.lightingScenario,
					". Cannot load scenario data."
				}));
				return false;
			}
			if (this.probeVolumeDebug.verboseStreamingLog)
			{
				int poolIndex = request.poolIndex;
			}
			request.scratchBuffer = cellStreamingScratchBuffer;
			request.scratchBufferLayout = layout;
			request.bytesWritten = 0;
			byte* mappedBufferBaseAddr = (byte*)request.scratchBuffer.stagingBuffer.GetUnsafePtr<byte>();
			byte* mappedBufferAddr = mappedBufferBaseAddr;
			uint* destChunkAddr = (uint*)mappedBufferAddr;
			List<ProbeBrickPool.BrickChunkAlloc> destChunks = ((request.poolIndex == -1) ? request.cell.poolInfo.chunkList : request.cell.blendingInfo.chunkList);
			int destChunkCount = destChunks.Count;
			for (int i = 0; i < destChunkCount; i++)
			{
				ProbeBrickPool.BrickChunkAlloc destChunk = destChunks[i];
				destChunkAddr[i * 4] = (uint)destChunk.x;
				destChunkAddr[i * 4 + 1] = (uint)destChunk.y;
				destChunkAddr[i * 4 + 2] = (uint)destChunk.z;
				destChunkAddr[i * 4 + 3] = 0U;
			}
			mappedBufferAddr += destChunkCount * 4 * 4;
			destChunkAddr = (uint*)mappedBufferAddr;
			destChunks = request.cell.poolInfo.chunkList;
			for (int j = 0; j < destChunkCount; j++)
			{
				ProbeBrickPool.BrickChunkAlloc destChunk2 = destChunks[j];
				destChunkAddr[j * 4] = (uint)destChunk2.x;
				destChunkAddr[j * 4 + 1] = (uint)destChunk2.y;
				destChunkAddr[j * 4 + 2] = (uint)destChunk2.z;
				destChunkAddr[j * 4 + 3] = 0U;
			}
			mappedBufferAddr += destChunkCount * 4 * 4;
			ProbeVolumeStreamableAsset shL0L1DataAsset = request.scenarioData.cellDataAsset;
			ProbeVolumeStreamableAsset.StreamableCellDesc cellStreamingDesc = shL0L1DataAsset.streamableCellDescs[cellIndex];
			int chunkCount = cellDesc.shChunkCount;
			int num = this.m_CurrentBakingSet.L0ChunkSize * chunkCount;
			int L1Size = this.m_CurrentBakingSet.L1ChunkSize * chunkCount;
			int L0L1ReadSize = num + 2 * L1Size;
			request.cellDataStreamingRequest.AddReadCommand(cellStreamingDesc.offset, L0L1ReadSize, mappedBufferAddr);
			mappedBufferAddr += L0L1ReadSize;
			request.bytesWritten += request.cellDataStreamingRequest.RunCommands(shL0L1DataAsset.OpenFile());
			if (request.streamSharedData)
			{
				ProbeVolumeStreamableAsset sharedDataAsset = this.m_CurrentBakingSet.cellSharedDataAsset;
				cellStreamingDesc = sharedDataAsset.streamableCellDescs[cellIndex];
				int sharedChunkSize = this.m_CurrentBakingSet.sharedDataChunkSize;
				request.cellSharedDataStreamingRequest.AddReadCommand(cellStreamingDesc.offset, sharedChunkSize * chunkCount, mappedBufferAddr);
				mappedBufferAddr += sharedChunkSize * chunkCount;
				request.bytesWritten += request.cellSharedDataStreamingRequest.RunCommands(sharedDataAsset.OpenFile());
			}
			if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				ProbeVolumeStreamableAsset optionalDataAsset = request.scenarioData.cellOptionalDataAsset;
				cellStreamingDesc = optionalDataAsset.streamableCellDescs[cellIndex];
				int L2ReadSize = this.m_CurrentBakingSet.L2TextureChunkSize * chunkCount * 4;
				request.cellOptionalDataStreamingRequest.AddReadCommand(cellStreamingDesc.offset, L2ReadSize, mappedBufferAddr);
				mappedBufferAddr += L2ReadSize;
				request.bytesWritten += request.cellOptionalDataStreamingRequest.RunCommands(optionalDataAsset.OpenFile());
			}
			if (this.m_CurrentBakingSet.bakedProbeOcclusion)
			{
				ProbeVolumeStreamableAsset probeOcclusionDataAsset = request.scenarioData.cellProbeOcclusionDataAsset;
				cellStreamingDesc = probeOcclusionDataAsset.streamableCellDescs[cellIndex];
				int probeOcclusionReadSize = this.m_CurrentBakingSet.ProbeOcclusionChunkSize * chunkCount;
				request.cellProbeOcclusionDataStreamingRequest.AddReadCommand(cellStreamingDesc.offset, probeOcclusionReadSize, mappedBufferAddr);
				mappedBufferAddr += probeOcclusionReadSize;
				request.bytesWritten += request.cellProbeOcclusionDataStreamingRequest.RunCommands(probeOcclusionDataAsset.OpenFile());
			}
			cellData.bricks = new NativeArray<ProbeBrickIndex.Brick>(cellDesc.bricksCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ProbeVolumeStreamableAsset brickDataAsset = this.m_CurrentBakingSet.cellBricksDataAsset;
			cellStreamingDesc = brickDataAsset.streamableCellDescs[cellIndex];
			request.brickStreamingRequest.AddReadCommand(cellStreamingDesc.offset, brickDataAsset.elementSize * cellStreamingDesc.elementCount, (byte*)cellData.bricks.GetUnsafePtr<ProbeBrickIndex.Brick>());
			request.brickStreamingRequest.RunCommands(brickDataAsset.OpenFile());
			if (this.m_CurrentBakingSet.HasSupportData())
			{
				ProbeVolumeStreamableAsset supportDataAsset = this.m_CurrentBakingSet.cellSupportDataAsset;
				cellStreamingDesc = supportDataAsset.streamableCellDescs[cellIndex];
				int supportOffset = cellStreamingDesc.offset;
				int positionSize = cellStreamingDesc.elementCount * this.m_CurrentBakingSet.supportPositionChunkSize;
				int touchupSize = cellStreamingDesc.elementCount * this.m_CurrentBakingSet.supportTouchupChunkSize;
				int offsetsSize = cellStreamingDesc.elementCount * this.m_CurrentBakingSet.supportOffsetsChunkSize;
				int layerSize = cellStreamingDesc.elementCount * this.m_CurrentBakingSet.supportLayerMaskChunkSize;
				int validitySize = cellStreamingDesc.elementCount * this.m_CurrentBakingSet.supportValidityChunkSize;
				cellData.probePositions = new NativeArray<byte>(positionSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory).Reinterpret<Vector3>(1);
				cellData.validity = new NativeArray<byte>(validitySize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory).Reinterpret<float>(1);
				cellData.layer = new NativeArray<byte>(layerSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory).Reinterpret<byte>(1);
				cellData.touchupVolumeInteraction = new NativeArray<byte>(touchupSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory).Reinterpret<float>(1);
				cellData.offsetVectors = new NativeArray<byte>(offsetsSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory).Reinterpret<Vector3>(1);
				request.supportStreamingRequest.AddReadCommand(supportOffset, positionSize, (byte*)cellData.probePositions.GetUnsafePtr<Vector3>());
				supportOffset += positionSize;
				request.supportStreamingRequest.AddReadCommand(supportOffset, validitySize, (byte*)cellData.validity.GetUnsafePtr<float>());
				supportOffset += validitySize;
				request.supportStreamingRequest.AddReadCommand(supportOffset, touchupSize, (byte*)cellData.touchupVolumeInteraction.GetUnsafePtr<float>());
				supportOffset += touchupSize;
				request.supportStreamingRequest.AddReadCommand(supportOffset, layerSize, (byte*)cellData.layer.GetUnsafePtr<byte>());
				supportOffset += layerSize;
				request.supportStreamingRequest.AddReadCommand(supportOffset, offsetsSize, (byte*)cellData.offsetVectors.GetUnsafePtr<Vector3>());
				request.supportStreamingRequest.RunCommands(supportDataAsset.OpenFile());
			}
			request.state = ProbeReferenceVolume.CellStreamingRequest.State.Active;
			this.m_ActiveStreamingRequests.Add(request);
			return true;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000198A8 File Offset: 0x00017AA8
		private void AllocateScratchBufferPoolIfNeeded()
		{
			if (this.m_SupportDiskStreaming)
			{
				int shChunkSize = this.m_CurrentBakingSet.GetChunkGPUMemory(this.m_SHBands);
				int maxSHChunkCount = this.m_CurrentBakingSet.maxSHChunkCount;
				if (this.m_ScratchBufferPool == null || this.m_ScratchBufferPool.chunkSize != shChunkSize || this.m_ScratchBufferPool.maxChunkCount != maxSHChunkCount)
				{
					bool verboseStreamingLog = this.probeVolumeDebug.verboseStreamingLog;
					if (this.m_ScratchBufferPool != null)
					{
						this.m_ScratchBufferPool.Cleanup();
					}
					this.m_ScratchBufferPool = new ProbeVolumeScratchBufferPool(this.m_CurrentBakingSet, this.m_SHBands);
				}
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019938 File Offset: 0x00017B38
		private void UpdateActiveRequests(CommandBuffer cmd)
		{
			if (this.m_ActiveStreamingRequests.Count > 0)
			{
				for (int i = this.m_ActiveStreamingRequests.Count - 1; i >= 0; i--)
				{
					ProbeReferenceVolume.CellStreamingRequest request = this.m_ActiveStreamingRequests[i];
					bool releaseRequest = false;
					if (request.state == ProbeReferenceVolume.CellStreamingRequest.State.Canceled)
					{
						bool verboseStreamingLog = this.probeVolumeDebug.verboseStreamingLog;
						this.m_ScratchBufferPool.ReleaseScratchBuffer(request.scratchBuffer);
						releaseRequest = true;
					}
					else
					{
						request.UpdateState();
						if (request.state == ProbeReferenceVolume.CellStreamingRequest.State.Complete)
						{
							if (this.probeVolumeDebug.verboseStreamingLog)
							{
								int poolIndex = request.poolIndex;
							}
							if (request.scratchBuffer.buffer != null)
							{
								request.scratchBuffer.buffer.LockBufferForWrite<byte>(0, request.scratchBuffer.stagingBuffer.Length).CopyFrom(request.scratchBuffer.stagingBuffer);
								request.scratchBuffer.buffer.UnlockBufferAfterWrite<byte>(request.scratchBuffer.stagingBuffer.Length);
							}
							request.onStreamingComplete(request, cmd);
							this.m_ScratchBufferPool.ReleaseScratchBuffer(request.scratchBuffer);
							releaseRequest = true;
						}
						else if (request.state == ProbeReferenceVolume.CellStreamingRequest.State.Invalid)
						{
							bool verboseStreamingLog2 = this.probeVolumeDebug.verboseStreamingLog;
							this.m_ScratchBufferPool.ReleaseScratchBuffer(request.scratchBuffer);
							request.Reset();
							this.m_ActiveStreamingRequests.RemoveAt(i);
							this.m_StreamingQueue.Enqueue(request);
						}
					}
					if (releaseRequest)
					{
						this.m_ActiveStreamingRequests.RemoveAt(i);
						this.m_StreamingRequestsPool.Release(request);
					}
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019ABC File Offset: 0x00017CBC
		private void ProcessNewRequests()
		{
			ProbeReferenceVolume.CellStreamingRequest request;
			while (this.m_StreamingQueue.TryPeek(out request))
			{
				if (request.state == ProbeReferenceVolume.CellStreamingRequest.State.Canceled)
				{
					if (this.probeVolumeDebug.verboseStreamingLog)
					{
						int poolIndex = request.poolIndex;
					}
					this.m_StreamingRequestsPool.Release(request);
					this.m_StreamingQueue.Dequeue();
				}
				else
				{
					if (!this.ProcessDiskStreamingRequest(request))
					{
						break;
					}
					this.m_StreamingQueue.Dequeue();
				}
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019B28 File Offset: 0x00017D28
		private void UpdateDiskStreaming(CommandBuffer cmd)
		{
			if (!this.diskStreamingEnabled)
			{
				return;
			}
			using (new ProfilingScope(ProfilingSampler.Get<CoreProfileId>(CoreProfileId.APVDiskStreamingUpdate)))
			{
				this.AllocateScratchBufferPoolIfNeeded();
				this.ProcessNewRequests();
				this.UpdateActiveRequests(cmd);
				if (this.m_ActiveStreamingRequests.Count == 0 && this.m_StreamingQueue.Count == 0 && this.m_CurrentBakingSet.cellBricksDataAsset != null && this.m_CurrentBakingSet.cellBricksDataAsset.IsOpen())
				{
					bool verboseStreamingLog = this.probeVolumeDebug.verboseStreamingLog;
					this.m_CurrentBakingSet.cellBricksDataAsset.CloseFile();
					this.m_CurrentBakingSet.cellSupportDataAsset.CloseFile();
					this.m_CurrentBakingSet.cellSharedDataAsset.CloseFile();
					ProbeVolumeBakingSet.PerScenarioDataInfo scenarioData;
					if (this.m_CurrentBakingSet.scenarios.TryGetValue(this.lightingScenario, out scenarioData))
					{
						scenarioData.cellDataAsset.CloseFile();
						scenarioData.cellOptionalDataAsset.CloseFile();
						scenarioData.cellProbeOcclusionDataAsset.CloseFile();
					}
					ProbeVolumeBakingSet.PerScenarioDataInfo otherScenarioData;
					if (!string.IsNullOrEmpty(this.otherScenario) && this.m_CurrentBakingSet.scenarios.TryGetValue(this.lightingScenario, out otherScenarioData))
					{
						otherScenarioData.cellDataAsset.CloseFile();
						otherScenarioData.cellOptionalDataAsset.CloseFile();
						otherScenarioData.cellProbeOcclusionDataAsset.CloseFile();
					}
				}
			}
			if (this.probeVolumeDebug.debugStreaming && this.m_ToBeLoadedCells.size == 0 && this.m_ActiveStreamingRequests.Count == 0)
			{
				this.UnloadAllCells();
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00010FF6 File Offset: 0x0000F1F6
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		private void LogStreaming(string log)
		{
			Debug.Log(log);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00019CBC File Offset: 0x00017EBC
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00019CC4 File Offset: 0x00017EC4
		internal Bounds globalBounds
		{
			get
			{
				return this.m_CurrGlobalBounds;
			}
			set
			{
				this.m_CurrGlobalBounds = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00019CCD File Offset: 0x00017ECD
		public bool isInitialized
		{
			get
			{
				return this.m_IsInitialized;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00019CD5 File Offset: 0x00017ED5
		internal bool enabledBySRP
		{
			get
			{
				return this.m_EnabledBySRP;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00019CDD File Offset: 0x00017EDD
		internal bool vertexSampling
		{
			get
			{
				return this.m_VertexSampling;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00019CE5 File Offset: 0x00017EE5
		internal bool hasUnloadedCells
		{
			get
			{
				return this.m_ToBeLoadedCells.size != 0;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00019CF5 File Offset: 0x00017EF5
		internal bool supportLightingScenarios
		{
			get
			{
				return this.m_SupportScenarios;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00019CFD File Offset: 0x00017EFD
		internal bool supportScenarioBlending
		{
			get
			{
				return this.m_SupportScenarioBlending;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00019D05 File Offset: 0x00017F05
		internal bool gpuStreamingEnabled
		{
			get
			{
				return this.m_SupportGPUStreaming;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00019D0D File Offset: 0x00017F0D
		internal bool diskStreamingEnabled
		{
			get
			{
				return this.m_SupportDiskStreaming && !this.m_ForceNoDiskStreaming;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00019D22 File Offset: 0x00017F22
		public bool probeOcclusion
		{
			get
			{
				return this.m_CurrentBakingSet && this.m_CurrentBakingSet.bakedProbeOcclusion;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00019D3E File Offset: 0x00017F3E
		public bool skyOcclusion
		{
			get
			{
				return this.m_CurrentBakingSet && this.m_CurrentBakingSet.bakedSkyOcclusion;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00019D5A File Offset: 0x00017F5A
		public bool skyOcclusionShadingDirection
		{
			get
			{
				return this.m_CurrentBakingSet && this.m_CurrentBakingSet.bakedSkyShadingDirection;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00019D76 File Offset: 0x00017F76
		private bool useRenderingLayers
		{
			get
			{
				return this.m_CurrentBakingSet.bakedMaskCount != 1;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00019D89 File Offset: 0x00017F89
		public ProbeVolumeSHBands shBands
		{
			get
			{
				return this.m_SHBands;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00019D91 File Offset: 0x00017F91
		public ProbeVolumeBakingSet currentBakingSet
		{
			get
			{
				return this.m_CurrentBakingSet;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00019D99 File Offset: 0x00017F99
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00019DB5 File Offset: 0x00017FB5
		public string lightingScenario
		{
			get
			{
				if (!this.m_CurrentBakingSet)
				{
					return null;
				}
				return this.m_CurrentBakingSet.lightingScenario;
			}
			set
			{
				this.SetActiveScenario(value, true);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00019DBF File Offset: 0x00017FBF
		public string otherScenario
		{
			get
			{
				if (!this.m_CurrentBakingSet)
				{
					return null;
				}
				return this.m_CurrentBakingSet.otherScenario;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00019DDB File Offset: 0x00017FDB
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00019DFB File Offset: 0x00017FFB
		public float scenarioBlendingFactor
		{
			get
			{
				if (!this.m_CurrentBakingSet)
				{
					return 0f;
				}
				return this.m_CurrentBakingSet.scenarioBlendingFactor;
			}
			set
			{
				if (this.m_CurrentBakingSet != null)
				{
					this.m_CurrentBakingSet.BlendLightingScenario(this.m_CurrentBakingSet.otherScenario, value);
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00019E22 File Offset: 0x00018022
		internal static string GetSceneGUID(Scene scene)
		{
			return scene.GetGUID();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00019E2A File Offset: 0x0001802A
		internal void SetActiveScenario(string scenario, bool verbose = true)
		{
			if (this.m_CurrentBakingSet != null)
			{
				this.m_CurrentBakingSet.SetActiveScenario(scenario, verbose);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00019E47 File Offset: 0x00018047
		public void BlendLightingScenario(string otherScenario, float blendingFactor)
		{
			if (this.m_CurrentBakingSet != null)
			{
				this.m_CurrentBakingSet.BlendLightingScenario(otherScenario, blendingFactor);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00019E64 File Offset: 0x00018064
		public ProbeVolumeTextureMemoryBudget memoryBudget
		{
			get
			{
				return this.m_MemoryBudget;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00019E6C File Offset: 0x0001806C
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x00019E74 File Offset: 0x00018074
		internal List<ProbeVolumePerSceneData> perSceneDataList { get; private set; } = new List<ProbeVolumePerSceneData>();

		// Token: 0x06000878 RID: 2168 RVA: 0x00019E7D File Offset: 0x0001807D
		internal void RegisterPerSceneData(ProbeVolumePerSceneData data)
		{
			if (!this.perSceneDataList.Contains(data))
			{
				this.perSceneDataList.Add(data);
				if (this.m_IsInitialized)
				{
					data.Initialize();
				}
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00019EA8 File Offset: 0x000180A8
		public void SetActiveScene(Scene scene)
		{
			ProbeVolumePerSceneData perSceneData;
			if (this.TryGetPerSceneData(ProbeReferenceVolume.GetSceneGUID(scene), out perSceneData))
			{
				this.SetActiveBakingSet(perSceneData.serializedBakingSet);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00019ED4 File Offset: 0x000180D4
		public void SetActiveBakingSet(ProbeVolumeBakingSet bakingSet)
		{
			if (this.m_CurrentBakingSet == bakingSet)
			{
				return;
			}
			foreach (ProbeVolumePerSceneData probeVolumePerSceneData in this.perSceneDataList)
			{
				probeVolumePerSceneData.QueueSceneRemoval();
			}
			this.UnloadBakingSet();
			this.SetBakingSetAsCurrent(bakingSet);
			if (this.m_CurrentBakingSet != null)
			{
				foreach (ProbeVolumePerSceneData probeVolumePerSceneData2 in this.perSceneDataList)
				{
					probeVolumePerSceneData2.QueueSceneLoading();
				}
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00019F90 File Offset: 0x00018190
		private void SetBakingSetAsCurrent(ProbeVolumeBakingSet bakingSet)
		{
			this.m_CurrentBakingSet = bakingSet;
			if (this.m_CurrentBakingSet != null)
			{
				this.InitProbeReferenceVolume();
				this.m_CurrentBakingSet.Initialize(this.m_UseStreamingAssets);
				this.m_CurrGlobalBounds = this.m_CurrentBakingSet.globalBounds;
				this.SetSubdivisionDimensions(bakingSet.minBrickSize, bakingSet.maxSubdivision, bakingSet.bakedProbeOffset);
				this.m_NeedsIndexRebuild = true;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00019FF9 File Offset: 0x000181F9
		internal void RegisterBakingSet(ProbeVolumePerSceneData data)
		{
			if (this.m_CurrentBakingSet == null)
			{
				this.SetBakingSetAsCurrent(data.serializedBakingSet);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001A018 File Offset: 0x00018218
		internal void UnloadBakingSet()
		{
			this.PerformPendingOperations();
			if (this.m_CurrentBakingSet != null)
			{
				this.m_CurrentBakingSet.Cleanup();
			}
			this.m_CurrentBakingSet = null;
			this.m_CurrGlobalBounds = default(Bounds);
			if (this.m_ScratchBufferPool != null)
			{
				this.m_ScratchBufferPool.Cleanup();
				this.m_ScratchBufferPool = null;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001A071 File Offset: 0x00018271
		internal void UnregisterPerSceneData(ProbeVolumePerSceneData data)
		{
			this.perSceneDataList.Remove(data);
			if (this.perSceneDataList.Count == 0)
			{
				this.UnloadBakingSet();
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001A094 File Offset: 0x00018294
		internal bool TryGetPerSceneData(string sceneGUID, out ProbeVolumePerSceneData perSceneData)
		{
			foreach (ProbeVolumePerSceneData data in this.perSceneDataList)
			{
				if (ProbeReferenceVolume.GetSceneGUID(data.gameObject.scene) == sceneGUID)
				{
					perSceneData = data;
					return true;
				}
			}
			perSceneData = null;
			return false;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001A108 File Offset: 0x00018308
		internal float indexFragmentationRate
		{
			get
			{
				if (!this.m_ProbeReferenceVolumeInit)
				{
					return 0f;
				}
				return this.m_Index.fragmentationRate;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001A123 File Offset: 0x00018323
		public static ProbeReferenceVolume instance
		{
			get
			{
				return ProbeReferenceVolume._instance;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001A12C File Offset: 0x0001832C
		public void Initialize(in ProbeVolumeSystemParameters parameters)
		{
			if (this.m_IsInitialized)
			{
				Debug.LogError("Probe Volume System has already been initialized.");
				return;
			}
			ProbeVolumeGlobalSettings probeVolumeSettings = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeGlobalSettings>();
			this.m_MemoryBudget = parameters.memoryBudget;
			this.m_BlendingMemoryBudget = parameters.blendingMemoryBudget;
			this.m_SupportScenarios = parameters.supportScenarios;
			this.m_SupportScenarioBlending = parameters.supportScenarios && parameters.supportScenarioBlending && SystemInfo.supportsComputeShaders && this.m_BlendingMemoryBudget > (ProbeVolumeBlendingTextureMemoryBudget)0;
			this.m_SHBands = parameters.shBands;
			this.m_UseStreamingAssets = !probeVolumeSettings.probeVolumeDisableStreamingAssets;
			this.m_SupportGPUStreaming = parameters.supportGPUStreaming;
			ProbeVolumeRuntimeResources renderPipelineSettings = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeRuntimeResources>();
			ComputeShader streamingUploadCS = ((renderPipelineSettings != null) ? renderPipelineSettings.probeVolumeUploadDataCS : null);
			ProbeVolumeRuntimeResources renderPipelineSettings2 = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeRuntimeResources>();
			ComputeShader streamingUploadL2CS = ((renderPipelineSettings2 != null) ? renderPipelineSettings2.probeVolumeUploadDataL2CS : null);
			this.m_SupportDiskStreaming = parameters.supportDiskStreaming && SystemInfo.supportsComputeShaders && this.m_SupportGPUStreaming && this.m_UseStreamingAssets && streamingUploadCS != null && streamingUploadL2CS != null;
			this.m_DiskStreamingUseCompute = SystemInfo.supportsComputeShaders && streamingUploadCS != null && streamingUploadL2CS != null;
			this.InitializeDebug();
			ProbeVolumeConstantRuntimeResources.Initialize();
			ProbeBrickPool.Initialize();
			ProbeBrickBlendingPool.Initialize();
			this.InitStreaming();
			this.m_IsInitialized = true;
			this.m_NeedsIndexRebuild = true;
			this.sceneData = parameters.sceneData;
			this.m_EnabledBySRP = true;
			foreach (ProbeVolumePerSceneData probeVolumePerSceneData in this.perSceneDataList)
			{
				probeVolumePerSceneData.Initialize();
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001A2C4 File Offset: 0x000184C4
		public void SetEnableStateFromSRP(bool srpEnablesPV)
		{
			this.m_EnabledBySRP = srpEnablesPV;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001A2CD File Offset: 0x000184CD
		public void SetVertexSamplingEnabled(bool value)
		{
			this.m_VertexSampling = value;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001A2D8 File Offset: 0x000184D8
		internal void ForceSHBand(ProbeVolumeSHBands shBands)
		{
			this.m_SHBands = shBands;
			this.DeinitProbeReferenceVolume();
			foreach (ProbeVolumePerSceneData probeVolumePerSceneData in this.perSceneDataList)
			{
				probeVolumePerSceneData.Initialize();
			}
			this.PerformPendingOperations();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001A33C File Offset: 0x0001853C
		internal void ForceNoDiskStreaming(bool state)
		{
			this.m_ForceNoDiskStreaming = state;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001A348 File Offset: 0x00018548
		public void Cleanup()
		{
			CoreUtils.SafeRelease(this.m_EmptyIndexBuffer);
			this.m_EmptyIndexBuffer = null;
			ProbeVolumeConstantRuntimeResources.Cleanup();
			if (!this.m_IsInitialized)
			{
				Debug.LogError("Adaptive Probe Volumes have not been initialized before calling Cleanup.");
				return;
			}
			this.CleanupLoadedData();
			this.CleanupDebug();
			this.CleanupStreaming();
			this.DeinitProbeReferenceVolume();
			this.m_IsInitialized = false;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001A3A0 File Offset: 0x000185A0
		public int GetVideoMemoryCost()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				return 0;
			}
			return this.m_Pool.estimatedVMemCost + this.m_Index.estimatedVMemCost + this.m_CellIndices.estimatedVMemCost + this.m_BlendingPool.estimatedVMemCost + this.m_TemporaryDataLocationMemCost;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001A3F0 File Offset: 0x000185F0
		private void RemoveCell(int cellIndex)
		{
			ProbeReferenceVolume.Cell cellInfo;
			if (this.cells.TryGetValue(cellIndex, out cellInfo))
			{
				cellInfo.referenceCount--;
				if (cellInfo.referenceCount <= 0)
				{
					this.cells.Remove(cellIndex);
					if (cellInfo.loaded)
					{
						this.m_LoadedCells.Remove(cellInfo);
						this.UnloadCell(cellInfo);
					}
					else
					{
						this.m_ToBeLoadedCells.Remove(cellInfo);
					}
					this.m_CurrentBakingSet.ReleaseCell(cellIndex);
					this.m_CellPool.Release(cellInfo);
				}
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001A474 File Offset: 0x00018674
		internal void UnloadCell(ProbeReferenceVolume.Cell cell)
		{
			if (cell.loaded)
			{
				if (cell.blendingInfo.blending)
				{
					this.m_LoadedBlendingCells.Remove(cell);
					this.UnloadBlendingCell(cell);
				}
				else
				{
					this.m_ToBeLoadedBlendingCells.Remove(cell);
				}
				if (cell.indexInfo.flatIndicesInGlobalIndirection != null)
				{
					this.m_CellIndices.MarkEntriesAsUnloaded(cell.indexInfo.flatIndicesInGlobalIndirection);
				}
				if (this.diskStreamingEnabled)
				{
					if (cell.streamingInfo.IsStreaming())
					{
						this.CancelStreamingRequest(cell);
					}
					else
					{
						this.ReleaseBricks(cell);
						cell.data.Cleanup(!this.diskStreamingEnabled);
					}
				}
				else
				{
					this.ReleaseBricks(cell);
				}
				cell.loaded = false;
				cell.debugProbes = null;
				this.ClearDebugData();
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001A538 File Offset: 0x00018738
		internal void UnloadBlendingCell(ProbeReferenceVolume.Cell cell)
		{
			if (this.diskStreamingEnabled && cell.streamingInfo.IsBlendingStreaming())
			{
				this.CancelBlendingStreamingRequest(cell);
			}
			if (cell.blendingInfo.blending)
			{
				this.m_BlendingPool.Deallocate(cell.blendingInfo.chunkList);
				cell.blendingInfo.chunkList.Clear();
				cell.blendingInfo.blending = false;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001A5A0 File Offset: 0x000187A0
		internal unsafe void UnloadAllCells()
		{
			for (int i = 0; i < this.m_LoadedCells.size; i++)
			{
				this.UnloadCell(*this.m_LoadedCells[i]);
			}
			this.m_ToBeLoadedCells.AddRange(this.m_LoadedCells);
			this.m_LoadedCells.Clear();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001A5F4 File Offset: 0x000187F4
		internal unsafe void UnloadAllBlendingCells()
		{
			for (int i = 0; i < this.m_LoadedBlendingCells.size; i++)
			{
				this.UnloadBlendingCell(*this.m_LoadedBlendingCells[i]);
			}
			this.m_ToBeLoadedBlendingCells.AddRange(this.m_LoadedBlendingCells);
			this.m_LoadedBlendingCells.Clear();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001A648 File Offset: 0x00018848
		private void AddCell(int cellIndex)
		{
			ProbeReferenceVolume.Cell cell;
			if (!this.cells.TryGetValue(cellIndex, out cell))
			{
				ProbeReferenceVolume.CellDesc cellDesc = this.m_CurrentBakingSet.GetCellDesc(cellIndex);
				if (cellDesc != null)
				{
					cell = this.m_CellPool.Get();
					cell.desc = cellDesc;
					cell.data = this.m_CurrentBakingSet.GetCellData(cellIndex);
					cell.poolInfo.shChunkCount = cell.desc.shChunkCount;
					cell.indexInfo.flatIndicesInGlobalIndirection = this.m_CellIndices.GetFlatIndicesForCell(cellDesc.position);
					cell.indexInfo.indexChunkCount = cell.desc.indexChunkCount;
					cell.indexInfo.indirectionEntryInfo = cell.desc.indirectionEntryInfo;
					cell.indexInfo.updateInfo.entriesInfo = new ProbeBrickIndex.IndirectionEntryUpdateInfo[cellDesc.indirectionEntryInfo.Length];
					cell.referenceCount = 1;
					this.cells[cellIndex] = cell;
					this.m_ToBeLoadedCells.Add(in cell);
					return;
				}
			}
			else
			{
				cell.referenceCount++;
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001A74C File Offset: 0x0001894C
		internal bool LoadCell(ProbeReferenceVolume.Cell cell, bool ignoreErrorLog = false)
		{
			if (!this.ReservePoolChunks(cell.desc.bricksCount, cell.poolInfo.chunkList, ignoreErrorLog))
			{
				return false;
			}
			int indirectionBufferEntries = cell.indexInfo.indirectionEntryInfo.Length;
			ProbeReferenceVolume.CellIndexInfo indexInfo = cell.indexInfo;
			for (int entry = 0; entry < indirectionBufferEntries; entry++)
			{
				if (!cell.indexInfo.indirectionEntryInfo[entry].hasMinMax)
				{
					NativeArray<ProbeBrickIndex.Brick> nativeArray = cell.data.bricks;
					if (nativeArray.IsCreated)
					{
						ProbeReferenceVolume.IndirectionEntryInfo[] indirectionEntryInfo = cell.indexInfo.indirectionEntryInfo;
						int num = entry;
						nativeArray = cell.data.bricks;
						this.ComputeEntryMinMax(ref indirectionEntryInfo[num], in nativeArray);
					}
					else
					{
						int entrySize = ProbeReferenceVolume.CellSize(this.GetEntrySubdivLevel());
						cell.indexInfo.indirectionEntryInfo[entry].minBrickPos = Vector3Int.zero;
						cell.indexInfo.indirectionEntryInfo[entry].maxBrickPosPlusOne = new Vector3Int(entrySize + 1, entrySize + 1, entrySize + 1);
						cell.indexInfo.indirectionEntryInfo[entry].hasMinMax = true;
					}
				}
				int brickCountAtResForEntry = ProbeReferenceVolume.GetNumberOfBricksAtSubdiv(cell.indexInfo.indirectionEntryInfo[entry]);
				indexInfo.updateInfo.entriesInfo[entry].numberOfChunks = this.m_Index.GetNumberOfChunks(brickCountAtResForEntry);
			}
			if (this.m_Index.FindSlotsForEntries(ref indexInfo.updateInfo.entriesInfo))
			{
				bool scenarioValid = cell.UpdateCellScenarioData(this.lightingScenario, this.otherScenario);
				this.m_Index.ReserveChunks(indexInfo.updateInfo.entriesInfo, ignoreErrorLog);
				for (int entry2 = 0; entry2 < indirectionBufferEntries; entry2++)
				{
					indexInfo.updateInfo.entriesInfo[entry2].minValidBrickIndexForCellAtMaxRes = indexInfo.indirectionEntryInfo[entry2].minBrickPos;
					indexInfo.updateInfo.entriesInfo[entry2].maxValidBrickIndexForCellAtMaxResPlusOne = indexInfo.indirectionEntryInfo[entry2].maxBrickPosPlusOne;
					indexInfo.updateInfo.entriesInfo[entry2].entryPositionInBricksAtMaxRes = indexInfo.indirectionEntryInfo[entry2].positionInBricks;
					indexInfo.updateInfo.entriesInfo[entry2].minSubdivInCell = indexInfo.indirectionEntryInfo[entry2].minSubdiv;
					indexInfo.updateInfo.entriesInfo[entry2].hasOnlyBiggerBricks = indexInfo.indirectionEntryInfo[entry2].hasOnlyBiggerBricks;
				}
				cell.loaded = true;
				if (scenarioValid)
				{
					this.AddBricks(cell);
				}
				this.minLoadedCellPos = Vector3Int.Min(this.minLoadedCellPos, cell.desc.position);
				this.maxLoadedCellPos = Vector3Int.Max(this.maxLoadedCellPos, cell.desc.position);
				this.ClearDebugData();
				return true;
			}
			this.ReleasePoolChunks(cell.poolInfo.chunkList);
			this.StartIndexDefragmentation();
			return false;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001AA38 File Offset: 0x00018C38
		internal unsafe void LoadAllCells()
		{
			int loadedCellsCount = this.m_LoadedCells.size;
			for (int i = 0; i < this.m_ToBeLoadedCells.size; i++)
			{
				ProbeReferenceVolume.Cell cell = *this.m_ToBeLoadedCells[i];
				if (this.LoadCell(cell, true))
				{
					this.m_LoadedCells.Add(in cell);
				}
			}
			for (int j = loadedCellsCount; j < this.m_LoadedCells.size; j++)
			{
				this.m_ToBeLoadedCells.Remove(*this.m_LoadedCells[j]);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001AABC File Offset: 0x00018CBC
		private void ComputeCellGlobalInfo()
		{
			this.minLoadedCellPos = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
			this.maxLoadedCellPos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
			foreach (ProbeReferenceVolume.Cell cell in this.cells.Values)
			{
				if (cell.loaded)
				{
					this.minLoadedCellPos = Vector3Int.Min(cell.desc.position, this.minLoadedCellPos);
					this.maxLoadedCellPos = Vector3Int.Max(cell.desc.position, this.maxLoadedCellPos);
				}
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001AB84 File Offset: 0x00018D84
		internal void AddPendingSceneLoading(string sceneGUID, ProbeVolumeBakingSet bakingSet)
		{
			if (this.m_PendingScenesToBeLoaded.ContainsKey(sceneGUID))
			{
				this.m_PendingScenesToBeLoaded.Remove(sceneGUID);
			}
			if (bakingSet == null && this.m_CurrentBakingSet != null && this.m_CurrentBakingSet.singleSceneMode)
			{
				return;
			}
			if (bakingSet.chunkSizeInBricks != ProbeBrickPool.GetChunkSizeInBrickCount())
			{
				Debug.LogError("Trying to load Adaptive Probe Volumes data (" + bakingSet.name + ") baked with an older incompatible version of APV. Please rebake your data.");
				return;
			}
			if (this.m_CurrentBakingSet != null && bakingSet != this.m_CurrentBakingSet)
			{
				return;
			}
			if (this.m_PendingScenesToBeLoaded.Count != 0)
			{
				using (Dictionary<string, ValueTuple<ProbeVolumeBakingSet, List<int>>>.ValueCollection.Enumerator enumerator = this.m_PendingScenesToBeLoaded.Values.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ValueTuple<ProbeVolumeBakingSet, List<int>> toBeLoadedBakingSet = enumerator.Current;
						if (bakingSet != toBeLoadedBakingSet.Item1)
						{
							Debug.LogError("Trying to load Adaptive Probe Volumes data for a scene from a different baking set from other scenes that are being loaded. Please make sure all loaded scenes are in the same baking set.");
							return;
						}
					}
				}
			}
			this.m_PendingScenesToBeLoaded.Add(sceneGUID, new ValueTuple<ProbeVolumeBakingSet, List<int>>(bakingSet, this.m_CurrentBakingSet.GetSceneCellIndexList(sceneGUID)));
			this.m_NeedLoadAsset = true;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		internal void AddPendingSceneRemoval(string sceneGUID)
		{
			if (this.m_PendingScenesToBeLoaded.ContainsKey(sceneGUID))
			{
				this.m_PendingScenesToBeLoaded.Remove(sceneGUID);
			}
			if (this.m_ActiveScenes.Contains(sceneGUID))
			{
				this.m_PendingScenesToBeUnloaded.TryAdd(sceneGUID, this.m_CurrentBakingSet.GetSceneCellIndexList(sceneGUID));
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		internal void RemovePendingScene(string sceneGUID, List<int> cellList)
		{
			if (this.m_ActiveScenes.Contains(sceneGUID))
			{
				this.m_ActiveScenes.Remove(sceneGUID);
			}
			foreach (int cellIndex in cellList)
			{
				this.RemoveCell(cellIndex);
			}
			this.ClearDebugData();
			this.ComputeCellGlobalInfo();
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001AD70 File Offset: 0x00018F70
		private void PerformPendingIndexChangeAndInit()
		{
			if (this.m_NeedsIndexRebuild)
			{
				this.CleanupLoadedData();
				this.InitializeGlobalIndirection();
				this.m_HasChangedIndex = true;
				this.m_NeedsIndexRebuild = false;
				return;
			}
			this.m_HasChangedIndex = false;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001AD9C File Offset: 0x00018F9C
		internal void SetSubdivisionDimensions(float minBrickSize, int maxSubdiv, Vector3 offset)
		{
			this.m_MinBrickSize = minBrickSize;
			this.SetMaxSubdivision(maxSubdiv);
			this.m_ProbeOffset = offset;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		private bool LoadCells(List<int> cellIndices)
		{
			if (this.m_CurrentBakingSet.ResolveCellData(cellIndices))
			{
				this.ClearDebugData();
				for (int i = 0; i < cellIndices.Count; i++)
				{
					this.AddCell(cellIndices[i]);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		private void PerformPendingLoading()
		{
			if ((this.m_PendingScenesToBeLoaded.Count == 0 && this.m_ActiveScenes.Count == 0) || !this.m_NeedLoadAsset || !this.m_ProbeReferenceVolumeInit)
			{
				return;
			}
			this.m_Pool.EnsureTextureValidity();
			this.m_BlendingPool.EnsureTextureValidity();
			if (this.m_HasChangedIndex)
			{
				foreach (string sceneGUID in this.m_ActiveScenes)
				{
					this.LoadCells(this.m_CurrentBakingSet.GetSceneCellIndexList(sceneGUID));
				}
			}
			foreach (KeyValuePair<string, ValueTuple<ProbeVolumeBakingSet, List<int>>> loadRequest in this.m_PendingScenesToBeLoaded)
			{
				string sceneGUID2 = loadRequest.Key;
				if (this.LoadCells(loadRequest.Value.Item2) && !this.m_ActiveScenes.Contains(sceneGUID2))
				{
					this.m_ActiveScenes.Add(sceneGUID2);
				}
			}
			this.m_PendingScenesToBeLoaded.Clear();
			this.m_NeedLoadAsset = false;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001AF28 File Offset: 0x00019128
		private void PerformPendingDeletion()
		{
			foreach (KeyValuePair<string, List<int>> unloadRequest in this.m_PendingScenesToBeUnloaded)
			{
				this.RemovePendingScene(unloadRequest.Key, unloadRequest.Value);
			}
			this.m_PendingScenesToBeUnloaded.Clear();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001AF94 File Offset: 0x00019194
		internal void ComputeEntryMinMax(ref ProbeReferenceVolume.IndirectionEntryInfo entryInfo, ReadOnlySpan<ProbeBrickIndex.Brick> bricks)
		{
			int entrySize = ProbeReferenceVolume.CellSize(this.GetEntrySubdivLevel());
			Vector3Int entry_min = entryInfo.positionInBricks;
			Vector3Int entry_max = entryInfo.positionInBricks + new Vector3Int(entrySize, entrySize, entrySize);
			if (entryInfo.hasOnlyBiggerBricks)
			{
				entryInfo.minBrickPos = entry_min;
				entryInfo.maxBrickPosPlusOne = entry_max;
			}
			else
			{
				entryInfo.minBrickPos = (entryInfo.maxBrickPosPlusOne = Vector3Int.zero);
				bool initialized = false;
				for (int i = 0; i < bricks.Length; i++)
				{
					int brickSize = ProbeReferenceVolume.CellSize(bricks[i].subdivisionLevel);
					Vector3Int brickMin = bricks[i].position;
					Vector3Int brickMax = bricks[i].position + new Vector3Int(brickSize, brickSize, brickSize);
					if (ProbeBrickIndex.BrickOverlapEntry(brickMin, brickMax, entry_min, entry_max))
					{
						brickMin = Vector3Int.Max(brickMin, entry_min);
						brickMax = Vector3Int.Min(brickMax, entry_max);
						if (initialized)
						{
							entryInfo.minBrickPos = Vector3Int.Min(brickMin, entryInfo.minBrickPos);
							entryInfo.maxBrickPosPlusOne = Vector3Int.Max(brickMax, entryInfo.maxBrickPosPlusOne);
						}
						else
						{
							entryInfo.minBrickPos = brickMin;
							entryInfo.maxBrickPosPlusOne = brickMax;
							initialized = true;
						}
					}
				}
			}
			entryInfo.minBrickPos -= entry_min;
			entryInfo.maxBrickPosPlusOne = Vector3Int.one + entryInfo.maxBrickPosPlusOne - entry_min;
			entryInfo.hasMinMax = true;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001B0F0 File Offset: 0x000192F0
		internal static int GetNumberOfBricksAtSubdiv(ProbeReferenceVolume.IndirectionEntryInfo entryInfo)
		{
			if (entryInfo.hasOnlyBiggerBricks)
			{
				return 1;
			}
			Vector3Int bricksForEntry = (entryInfo.maxBrickPosPlusOne - entryInfo.minBrickPos) / ProbeReferenceVolume.CellSize(entryInfo.minSubdiv);
			return bricksForEntry.x * bricksForEntry.y * bricksForEntry.z;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001B140 File Offset: 0x00019340
		public void PerformPendingOperations()
		{
			this.PerformPendingDeletion();
			this.PerformPendingIndexChangeAndInit();
			this.PerformPendingLoading();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001B154 File Offset: 0x00019354
		internal void InitializeGlobalIndirection()
		{
			Vector3Int minCellPosition = (this.m_CurrentBakingSet ? this.m_CurrentBakingSet.minCellPosition : Vector3Int.zero);
			Vector3Int maxCellPosition = (this.m_CurrentBakingSet ? this.m_CurrentBakingSet.maxCellPosition : Vector3Int.zero);
			if (this.m_CellIndices != null)
			{
				this.m_CellIndices.Cleanup();
			}
			this.m_CellIndices = new ProbeGlobalIndirection(minCellPosition, maxCellPosition, Mathf.Max(1, (int)Mathf.Pow(3f, (float)(this.m_MaxSubdivision - 1))));
			if (this.m_SupportGPUStreaming)
			{
				if (this.m_DefragCellIndices != null)
				{
					this.m_DefragCellIndices.Cleanup();
				}
				this.m_DefragCellIndices = new ProbeGlobalIndirection(minCellPosition, maxCellPosition, Mathf.Max(1, (int)Mathf.Pow(3f, (float)(this.m_MaxSubdivision - 1))));
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001B220 File Offset: 0x00019420
		private void InitProbeReferenceVolume()
		{
			if (this.m_ProbeReferenceVolumeInit && !this.m_Pool.EnsureTextureValidity(this.useRenderingLayers, this.skyOcclusion, this.skyOcclusionShadingDirection, this.probeOcclusion))
			{
				this.m_TemporaryDataLocation.Cleanup();
				this.m_TemporaryDataLocation = ProbeBrickPool.CreateDataLocation(ProbeBrickPool.GetChunkSizeInProbeCount(), false, this.m_SHBands, "APV_Intermediate", false, true, this.useRenderingLayers, this.skyOcclusion, this.skyOcclusionShadingDirection, this.probeOcclusion, out this.m_TemporaryDataLocationMemCost);
			}
			if (!this.m_ProbeReferenceVolumeInit)
			{
				this.m_Pool = new ProbeBrickPool(this.m_MemoryBudget, this.m_SHBands, true, this.useRenderingLayers, this.skyOcclusion, this.skyOcclusionShadingDirection, this.probeOcclusion);
				this.m_BlendingPool = new ProbeBrickBlendingPool(this.m_BlendingMemoryBudget, this.m_SHBands, this.probeOcclusion);
				this.m_Index = new ProbeBrickIndex(this.m_MemoryBudget);
				if (this.m_SupportGPUStreaming)
				{
					this.m_DefragIndex = new ProbeBrickIndex(this.m_MemoryBudget);
				}
				this.InitializeGlobalIndirection();
				this.m_TemporaryDataLocation = ProbeBrickPool.CreateDataLocation(ProbeBrickPool.GetChunkSizeInProbeCount(), false, this.m_SHBands, "APV_Intermediate", false, true, this.useRenderingLayers, this.skyOcclusion, this.skyOcclusionShadingDirection, this.probeOcclusion, out this.m_TemporaryDataLocationMemCost);
				this.m_PositionOffsets[0] = 0f;
				float probeDelta = 0.33333334f;
				for (int i = 1; i < 3; i++)
				{
					this.m_PositionOffsets[i] = (float)i * probeDelta;
				}
				this.m_PositionOffsets[this.m_PositionOffsets.Length - 1] = 1f;
				this.m_ProbeReferenceVolumeInit = true;
				this.ClearDebugData();
				this.m_NeedLoadAsset = true;
			}
			if (DebugManager.instance.GetPanel(ProbeReferenceVolume.k_DebugPanelName, false, 0, false) != null)
			{
				ProbeReferenceVolume.instance.UnregisterDebug(false);
				ProbeReferenceVolume.instance.RegisterDebug();
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001B3E4 File Offset: 0x000195E4
		private ProbeReferenceVolume()
		{
			this.m_MinBrickSize = 1f;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001B5F0 File Offset: 0x000197F0
		public ProbeReferenceVolume.RuntimeResources GetRuntimeResources()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				return default(ProbeReferenceVolume.RuntimeResources);
			}
			ProbeReferenceVolume.RuntimeResources rr = default(ProbeReferenceVolume.RuntimeResources);
			this.m_Index.GetRuntimeResources(ref rr);
			this.m_CellIndices.GetRuntimeResources(ref rr);
			this.m_Pool.GetRuntimeResources(ref rr);
			ProbeVolumeConstantRuntimeResources.GetRuntimeResources(ref rr);
			return rr;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001B648 File Offset: 0x00019848
		internal void SetMaxSubdivision(int maxSubdivision)
		{
			if (Math.Min(maxSubdivision, 7) != this.m_MaxSubdivision)
			{
				this.m_MaxSubdivision = Math.Min(maxSubdivision, 7);
				if (this.m_CellIndices != null)
				{
					this.m_CellIndices.Cleanup();
				}
				if (this.m_SupportGPUStreaming && this.m_DefragCellIndices != null)
				{
					this.m_DefragCellIndices.Cleanup();
				}
				this.InitializeGlobalIndirection();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001B6A5 File Offset: 0x000198A5
		internal static int CellSize(int subdivisionLevel)
		{
			return (int)Mathf.Pow(3f, (float)subdivisionLevel);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001B6B4 File Offset: 0x000198B4
		internal float BrickSize(int subdivisionLevel)
		{
			return this.m_MinBrickSize * (float)ProbeReferenceVolume.CellSize(subdivisionLevel);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001B6C4 File Offset: 0x000198C4
		internal float MinBrickSize()
		{
			return this.m_MinBrickSize;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001B6CC File Offset: 0x000198CC
		internal float MaxBrickSize()
		{
			return this.BrickSize(this.m_MaxSubdivision - 1);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001B6DC File Offset: 0x000198DC
		internal Vector3 ProbeOffset()
		{
			return this.m_ProbeOffset;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001B6E4 File Offset: 0x000198E4
		internal int GetMaxSubdivision()
		{
			return this.m_MaxSubdivision;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001B6EC File Offset: 0x000198EC
		internal int GetMaxSubdivision(float multiplier)
		{
			return Mathf.CeilToInt((float)this.m_MaxSubdivision * multiplier);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001B6FC File Offset: 0x000198FC
		internal float GetDistanceBetweenProbes(int subdivisionLevel)
		{
			return this.BrickSize(subdivisionLevel) / 3f;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001B70B File Offset: 0x0001990B
		internal float MinDistanceBetweenProbes()
		{
			return this.GetDistanceBetweenProbes(0);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001B714 File Offset: 0x00019914
		internal int GetGlobalIndirectionEntryMaxSubdiv()
		{
			return 3;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001B717 File Offset: 0x00019917
		internal int GetEntrySubdivLevel()
		{
			return Mathf.Min(3, this.m_MaxSubdivision - 1);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001B727 File Offset: 0x00019927
		internal float GetEntrySize()
		{
			return this.BrickSize(this.GetEntrySubdivLevel());
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001B735 File Offset: 0x00019935
		public bool DataHasBeenLoaded()
		{
			return this.m_LoadedCells.size != 0;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001B748 File Offset: 0x00019948
		internal void Clear()
		{
			if (this.m_ProbeReferenceVolumeInit)
			{
				try
				{
					this.PerformPendingOperations();
				}
				finally
				{
					this.UnloadAllCells();
					this.m_ToBeLoadedCells.Clear();
					this.m_Pool.Clear();
					this.m_BlendingPool.Clear();
					this.m_Index.Clear();
					this.cells.Clear();
				}
			}
			if (this.clearAssetsOnVolumeClear)
			{
				this.m_PendingScenesToBeLoaded.Clear();
				this.m_ActiveScenes.Clear();
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001B7D4 File Offset: 0x000199D4
		private List<ProbeBrickPool.BrickChunkAlloc> GetSourceLocations(int count, int chunkSize, ProbeBrickPool.DataLocation dataLoc)
		{
			ProbeBrickPool.BrickChunkAlloc c = default(ProbeBrickPool.BrickChunkAlloc);
			this.m_TmpSrcChunks.Clear();
			this.m_TmpSrcChunks.Add(c);
			for (int i = 1; i < count; i++)
			{
				c.x += chunkSize * 4;
				if (c.x >= dataLoc.width)
				{
					c.x = 0;
					c.y += 4;
					if (c.y >= dataLoc.height)
					{
						c.y = 0;
						c.z += 4;
					}
				}
				this.m_TmpSrcChunks.Add(c);
			}
			return this.m_TmpSrcChunks;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001B870 File Offset: 0x00019A70
		private void UpdateDataLocationTexture<T>(Texture output, NativeArray<T> input) where T : struct
		{
			(output as Texture3D).GetPixelData<T>(0).GetSubArray(0, input.Length).CopyFrom(input);
			(output as Texture3D).Apply();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		private void UpdatePool(List<ProbeBrickPool.BrickChunkAlloc> chunkList, ProbeReferenceVolume.CellData.PerScenarioData data, NativeArray<byte> validityNeighMaskData, NativeArray<ushort> skyOcclusionL0L1Data, NativeArray<byte> skyShadingDirectionIndices, int chunkIndex, int poolIndex)
		{
			int chunkSizeInProbes = ProbeBrickPool.GetChunkSizeInProbeCount();
			this.UpdateDataLocationTexture<ushort>(this.m_TemporaryDataLocation.TexL0_L1rx, data.shL0L1RxData.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL1_G_ry, data.shL1GL1RyData.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL1_B_rz, data.shL1BL1RzData.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL2 && data.shL2Data_0.Length > 0)
			{
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL2_0, data.shL2Data_0.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL2_1, data.shL2Data_1.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL2_2, data.shL2Data_2.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexL2_3, data.shL2Data_3.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			}
			if (this.probeOcclusion && data.probeOcclusion.Length > 0)
			{
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexProbeOcclusion, data.probeOcclusion.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			}
			if (poolIndex == -1)
			{
				if (validityNeighMaskData.Length > 0)
				{
					if (this.m_CurrentBakingSet.bakedMaskCount == 1)
					{
						this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexValidity, validityNeighMaskData.GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
					}
					else
					{
						this.UpdateDataLocationTexture<uint>(this.m_TemporaryDataLocation.TexValidity, validityNeighMaskData.Reinterpret<uint>(1).GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
					}
				}
				if (this.skyOcclusion && skyOcclusionL0L1Data.Length > 0)
				{
					this.UpdateDataLocationTexture<ushort>(this.m_TemporaryDataLocation.TexSkyOcclusion, skyOcclusionL0L1Data.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
				}
				if (this.skyOcclusionShadingDirection && skyShadingDirectionIndices.Length > 0)
				{
					this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexSkyShadingDirectionIndices, skyShadingDirectionIndices.GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
				}
			}
			List<ProbeBrickPool.BrickChunkAlloc> srcChunks = this.GetSourceLocations(1, ProbeBrickPool.GetChunkSizeInBrickCount(), this.m_TemporaryDataLocation);
			if (poolIndex == -1)
			{
				this.m_Pool.Update(this.m_TemporaryDataLocation, srcChunks, chunkList, chunkIndex, this.m_SHBands);
				return;
			}
			this.m_BlendingPool.Update(this.m_TemporaryDataLocation, srcChunks, chunkList, chunkIndex, this.m_SHBands, poolIndex);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001BB40 File Offset: 0x00019D40
		private void UpdatePool(CommandBuffer cmd, List<ProbeBrickPool.BrickChunkAlloc> chunkList, ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, int poolIndex)
		{
			if (poolIndex == -1)
			{
				this.m_Pool.Update(cmd, dataBuffer, layout, chunkList, true, this.m_Pool.GetValidityTexture(), this.m_SHBands, this.skyOcclusion, this.m_Pool.GetSkyOcclusionTexture(), this.skyOcclusionShadingDirection, this.m_Pool.GetSkyShadingDirectionIndicesTexture(), this.probeOcclusion);
				return;
			}
			this.m_BlendingPool.Update(cmd, dataBuffer, layout, chunkList, this.m_SHBands, poolIndex, this.m_Pool.GetValidityTexture(), this.skyOcclusion, this.m_Pool.GetSkyOcclusionTexture(), this.skyOcclusionShadingDirection, this.m_Pool.GetSkyShadingDirectionIndicesTexture(), this.probeOcclusion);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		private void UpdateSharedData(List<ProbeBrickPool.BrickChunkAlloc> chunkList, NativeArray<byte> validityNeighMaskData, NativeArray<ushort> skyOcclusionData, NativeArray<byte> skyShadingDirectionIndices, int chunkIndex)
		{
			int chunkSizeInProbes = ProbeBrickPool.GetChunkSizeInBrickCount() * 64;
			if (this.m_CurrentBakingSet.bakedMaskCount == 1)
			{
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexValidity, validityNeighMaskData.GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
			}
			else
			{
				this.UpdateDataLocationTexture<uint>(this.m_TemporaryDataLocation.TexValidity, validityNeighMaskData.Reinterpret<uint>(1).GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
			}
			if (this.skyOcclusion && skyOcclusionData.Length > 0)
			{
				this.UpdateDataLocationTexture<ushort>(this.m_TemporaryDataLocation.TexSkyOcclusion, skyOcclusionData.GetSubArray(chunkIndex * chunkSizeInProbes * 4, chunkSizeInProbes * 4));
			}
			if (this.skyOcclusion && this.skyOcclusionShadingDirection && skyShadingDirectionIndices.Length > 0)
			{
				this.UpdateDataLocationTexture<byte>(this.m_TemporaryDataLocation.TexSkyShadingDirectionIndices, skyShadingDirectionIndices.GetSubArray(chunkIndex * chunkSizeInProbes, chunkSizeInProbes));
			}
			List<ProbeBrickPool.BrickChunkAlloc> srcChunks = this.GetSourceLocations(1, ProbeBrickPool.GetChunkSizeInBrickCount(), this.m_TemporaryDataLocation);
			this.m_Pool.UpdateValidity(this.m_TemporaryDataLocation, srcChunks, chunkList, chunkIndex);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001BCE4 File Offset: 0x00019EE4
		private bool AddBlendingBricks(ProbeReferenceVolume.Cell cell)
		{
			bool flag;
			using (new ProfilerMarker("AddBlendingBricks").Auto())
			{
				bool bypassBlending = this.m_CurrentBakingSet.otherScenario == null || !cell.hasTwoScenarios;
				if (!bypassBlending && !this.m_BlendingPool.Allocate(cell.poolInfo.shChunkCount, cell.blendingInfo.chunkList))
				{
					flag = false;
				}
				else
				{
					if (this.diskStreamingEnabled)
					{
						if (bypassBlending)
						{
							if (cell.blendingInfo.blendingFactor != this.scenarioBlendingFactor)
							{
								this.PushDiskStreamingRequest(cell, this.lightingScenario, -1, this.m_OnStreamingComplete);
							}
							cell.blendingInfo.MarkUpToDate();
						}
						else
						{
							this.PushDiskStreamingRequest(cell, this.lightingScenario, 0, this.m_OnBlendingStreamingComplete);
							this.PushDiskStreamingRequest(cell, this.otherScenario, 1, this.m_OnBlendingStreamingComplete);
						}
					}
					else
					{
						if (!cell.indexInfo.indexUpdated)
						{
							this.UpdateCellIndex(cell);
							List<ProbeBrickPool.BrickChunkAlloc> chunkList = cell.poolInfo.chunkList;
							for (int chunkIndex = 0; chunkIndex < chunkList.Count; chunkIndex++)
							{
								this.UpdateSharedData(chunkList, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, chunkIndex);
							}
						}
						if (bypassBlending)
						{
							if (cell.blendingInfo.blendingFactor != this.scenarioBlendingFactor)
							{
								List<ProbeBrickPool.BrickChunkAlloc> chunkList2 = cell.poolInfo.chunkList;
								for (int chunkIndex2 = 0; chunkIndex2 < chunkList2.Count; chunkIndex2++)
								{
									this.UpdatePool(chunkList2, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, chunkIndex2, -1);
								}
							}
							cell.blendingInfo.MarkUpToDate();
						}
						else
						{
							List<ProbeBrickPool.BrickChunkAlloc> chunkList3 = cell.blendingInfo.chunkList;
							for (int chunkIndex3 = 0; chunkIndex3 < chunkList3.Count; chunkIndex3++)
							{
								this.UpdatePool(chunkList3, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, chunkIndex3, 0);
								this.UpdatePool(chunkList3, cell.scenario1, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, chunkIndex3, 1);
							}
						}
					}
					cell.blendingInfo.blending = true;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001BF5C File Offset: 0x0001A15C
		private bool ReservePoolChunks(int brickCount, List<ProbeBrickPool.BrickChunkAlloc> chunkList, bool ignoreErrorLog)
		{
			int brickChunksCount = ProbeBrickPool.GetChunkCount(brickCount);
			chunkList.Clear();
			return this.m_Pool.Allocate(brickChunksCount, chunkList, ignoreErrorLog);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001BF84 File Offset: 0x0001A184
		private void ReleasePoolChunks(List<ProbeBrickPool.BrickChunkAlloc> chunkList)
		{
			this.m_Pool.Deallocate(chunkList);
			chunkList.Clear();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001BF98 File Offset: 0x0001A198
		private void UpdatePoolAndIndex(ProbeReferenceVolume.Cell cell, ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, int poolIndex, CommandBuffer cmd)
		{
			if (this.diskStreamingEnabled)
			{
				if (this.m_DiskStreamingUseCompute)
				{
					this.UpdatePool(cmd, cell.poolInfo.chunkList, dataBuffer, layout, poolIndex);
				}
				else
				{
					int chunkCount = cell.poolInfo.chunkList.Count;
					int offsetAdjustment = -2 * (chunkCount * 4 * 4);
					ProbeReferenceVolume.CellData.PerScenarioData data = default(ProbeReferenceVolume.CellData.PerScenarioData);
					data.shL0L1RxData = dataBuffer.stagingBuffer.GetSubArray(layout._L0L1rxOffset + offsetAdjustment, chunkCount * layout._L0Size).Reinterpret<ushort>(1);
					data.shL1GL1RyData = dataBuffer.stagingBuffer.GetSubArray(layout._L1GryOffset + offsetAdjustment, chunkCount * layout._L1Size);
					data.shL1BL1RzData = dataBuffer.stagingBuffer.GetSubArray(layout._L1BrzOffset + offsetAdjustment, chunkCount * layout._L1Size);
					NativeArray<byte> validityNeighMaskData = dataBuffer.stagingBuffer.GetSubArray(layout._ValidityOffset + offsetAdjustment, chunkCount * layout._ValiditySize);
					if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						data.shL2Data_0 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_0Offset + offsetAdjustment, chunkCount * layout._L2Size);
						data.shL2Data_1 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_1Offset + offsetAdjustment, chunkCount * layout._L2Size);
						data.shL2Data_2 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_2Offset + offsetAdjustment, chunkCount * layout._L2Size);
						data.shL2Data_3 = dataBuffer.stagingBuffer.GetSubArray(layout._L2_3Offset + offsetAdjustment, chunkCount * layout._L2Size);
					}
					if (this.probeOcclusion && layout._ProbeOcclusionSize > 0)
					{
						data.probeOcclusion = dataBuffer.stagingBuffer.GetSubArray(layout._ProbeOcclusionOffset + offsetAdjustment, chunkCount * layout._ProbeOcclusionSize);
					}
					NativeArray<ushort> skyOcclusionData = default(NativeArray<ushort>);
					if (this.skyOcclusion && layout._SkyOcclusionSize > 0)
					{
						skyOcclusionData = dataBuffer.stagingBuffer.GetSubArray(layout._SkyOcclusionOffset + offsetAdjustment, chunkCount * layout._SkyOcclusionSize).Reinterpret<ushort>(1);
					}
					NativeArray<byte> skyOcclusionDirectionData = default(NativeArray<byte>);
					if (this.skyOcclusion && this.skyOcclusionShadingDirection && layout._SkyShadingDirectionSize > 0)
					{
						skyOcclusionDirectionData = dataBuffer.stagingBuffer.GetSubArray(layout._SkyShadingDirectionOffset + offsetAdjustment, chunkCount * layout._SkyShadingDirectionSize);
					}
					for (int chunkIndex = 0; chunkIndex < chunkCount; chunkIndex++)
					{
						this.UpdatePool(cell.poolInfo.chunkList, data, validityNeighMaskData, skyOcclusionData, skyOcclusionDirectionData, chunkIndex, poolIndex);
					}
				}
			}
			else
			{
				for (int chunkIndex2 = 0; chunkIndex2 < cell.poolInfo.chunkList.Count; chunkIndex2++)
				{
					this.UpdatePool(cell.poolInfo.chunkList, cell.scenario0, cell.data.validityNeighMaskData, cell.data.skyOcclusionDataL0L1, cell.data.skyShadingDirectionIndices, chunkIndex2, poolIndex);
				}
			}
			if (!cell.indexInfo.indexUpdated)
			{
				this.UpdateCellIndex(cell);
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C264 File Offset: 0x0001A464
		private bool AddBricks(ProbeReferenceVolume.Cell cell)
		{
			bool flag;
			using (new ProfilerMarker("AddBricks").Auto())
			{
				if (this.supportScenarioBlending)
				{
					this.m_ToBeLoadedBlendingCells.Add(in cell);
				}
				if (!this.supportScenarioBlending || this.scenarioBlendingFactor == 0f || !cell.hasTwoScenarios)
				{
					if (this.diskStreamingEnabled)
					{
						this.PushDiskStreamingRequest(cell, this.m_CurrentBakingSet.lightingScenario, -1, this.m_OnStreamingComplete);
					}
					else
					{
						this.UpdatePoolAndIndex(cell, null, default(ProbeReferenceVolume.CellStreamingScratchBufferLayout), -1, null);
					}
					cell.blendingInfo.blendingFactor = 0f;
				}
				else if (this.supportScenarioBlending)
				{
					cell.blendingInfo.Prioritize();
					cell.indexInfo.indexUpdated = false;
				}
				cell.loaded = true;
				this.ClearDebugData();
				flag = true;
			}
			return flag;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C350 File Offset: 0x0001A550
		private void UpdateCellIndex(ProbeReferenceVolume.Cell cell)
		{
			cell.indexInfo.indexUpdated = true;
			NativeArray<ProbeBrickIndex.Brick> bricks = cell.data.bricks;
			this.m_Index.AddBricks(cell.indexInfo, bricks, cell.poolInfo.chunkList, ProbeBrickPool.GetChunkSizeInBrickCount(), this.m_Pool.GetPoolWidth(), this.m_Pool.GetPoolHeight());
			this.m_CellIndices.UpdateCell(cell.indexInfo);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		private void ReleaseBricks(ProbeReferenceVolume.Cell cell)
		{
			if (cell.poolInfo.chunkList.Count == 0)
			{
				Debug.Log("Tried to release bricks from an empty Cell.");
				return;
			}
			this.m_Index.RemoveBricks(cell.indexInfo);
			cell.indexInfo.indexUpdated = false;
			this.m_Pool.Deallocate(cell.poolInfo.chunkList);
			cell.poolInfo.chunkList.Clear();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C430 File Offset: 0x0001A630
		internal void UpdateConstantBuffer(CommandBuffer cmd, ProbeVolumeShadingParameters parameters)
		{
			float normalBias = parameters.normalBias;
			float viewBias = parameters.viewBias;
			APVLeakReductionMode leakReductionMode = parameters.leakReductionMode;
			if (parameters.scaleBiasByMinDistanceBetweenProbes)
			{
				normalBias *= this.MinDistanceBetweenProbes();
				viewBias *= this.MinDistanceBetweenProbes();
			}
			Vector3Int indexDim = this.m_CellIndices.GetGlobalIndirectionDimension();
			Vector3Int poolDim = this.m_Pool.GetPoolDimensions();
			Vector3Int minEntry;
			Vector3Int vector3Int;
			this.m_CellIndices.GetMinMaxEntry(out minEntry, out vector3Int);
			int entriesPerCell = this.m_CellIndices.entriesPerCellDimension;
			float skyDirectionWeight = (parameters.skyOcclusionShadingDirection ? 1f : 0f);
			Vector3 probeOffset = this.ProbeOffset() + parameters.worldOffset;
			ShaderVariablesProbeVolumes shaderVars;
			shaderVars._Offset_LayerCount = new Vector4(probeOffset.x, probeOffset.y, probeOffset.z, (float)parameters.regionCount);
			shaderVars._MinLoadedCellInEntries_IndirectionEntryDim = new Vector4((float)(this.minLoadedCellPos.x * entriesPerCell), (float)(this.minLoadedCellPos.y * entriesPerCell), (float)(this.minLoadedCellPos.z * entriesPerCell), this.GetEntrySize());
			shaderVars._MaxLoadedCellInEntries_RcpIndirectionEntryDim = new Vector4((float)((this.maxLoadedCellPos.x + 1) * entriesPerCell - 1), (float)((this.maxLoadedCellPos.y + 1) * entriesPerCell - 1), (float)((this.maxLoadedCellPos.z + 1) * entriesPerCell - 1), 1f / this.GetEntrySize());
			shaderVars._PoolDim_MinBrickSize = new Vector4((float)poolDim.x, (float)poolDim.y, (float)poolDim.z, this.MinBrickSize());
			shaderVars._RcpPoolDim_XY = new Vector4(1f / (float)poolDim.x, 1f / (float)poolDim.y, 1f / (float)poolDim.z, 1f / (float)(poolDim.x * poolDim.y));
			shaderVars._MinEntryPos_Noise = new Vector4((float)minEntry.x, (float)minEntry.y, (float)minEntry.z, parameters.samplingNoise);
			shaderVars._EntryCount_X_XY_LeakReduction = new uint4((uint)indexDim.x, (uint)(indexDim.x * indexDim.y), (uint)leakReductionMode, 0U);
			shaderVars._Biases_NormalizationClamp = new Vector4(normalBias, viewBias, parameters.reflNormalizationLowerClamp, parameters.reflNormalizationUpperClamp);
			shaderVars._FrameIndex_Weights = new Vector4((float)parameters.frameIndexForNoise, parameters.weight, parameters.skyOcclusionIntensity, skyDirectionWeight);
			shaderVars._ProbeVolumeLayerMask = parameters.regionLayerMasks;
			ConstantBuffer.PushGlobal<ShaderVariablesProbeVolumes>(cmd, in shaderVars, this.m_CBShaderID);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001C69C File Offset: 0x0001A89C
		private void DeinitProbeReferenceVolume()
		{
			if (this.m_ProbeReferenceVolumeInit)
			{
				foreach (ProbeVolumePerSceneData data in this.perSceneDataList)
				{
					this.AddPendingSceneRemoval(data.sceneGUID);
				}
				this.PerformPendingDeletion();
				this.m_Index.Cleanup();
				this.m_CellIndices.Cleanup();
				if (this.m_SupportGPUStreaming)
				{
					this.m_DefragIndex.Cleanup();
					this.m_DefragCellIndices.Cleanup();
				}
				if (this.m_Pool != null)
				{
					this.m_Pool.Cleanup();
					this.m_BlendingPool.Cleanup();
				}
				this.m_TemporaryDataLocation.Cleanup();
				this.m_ProbeReferenceVolumeInit = false;
				if (this.m_CurrentBakingSet != null)
				{
					this.m_CurrentBakingSet.Cleanup();
				}
				this.m_CurrentBakingSet = null;
			}
			else
			{
				ProbeGlobalIndirection cellIndices = this.m_CellIndices;
				if (cellIndices != null)
				{
					cellIndices.Cleanup();
				}
				ProbeGlobalIndirection defragCellIndices = this.m_DefragCellIndices;
				if (defragCellIndices != null)
				{
					defragCellIndices.Cleanup();
				}
			}
			this.ClearDebugData();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		private void CleanupLoadedData()
		{
			this.UnloadAllCells();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C81F File Offset: 0x0001AA1F
		[CompilerGenerated]
		private void <RegisterDebug>g__RefreshDebug|42_0<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebug(false);
			this.RegisterDebug();
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001CC20 File Offset: 0x0001AE20
		[CompilerGenerated]
		private void <RegisterDebug>g__RefreshScenarioNames|42_75(string guid)
		{
			HashSet<string> allScenarios = new HashSet<string>();
			foreach (ProbeVolumeBakingSet set in Resources.FindObjectsOfTypeAll<ProbeVolumeBakingSet>())
			{
				if (set.sceneGUIDs.Contains(guid))
				{
					foreach (string scenario in set.lightingScenarios)
					{
						allScenarios.Add(scenario);
					}
				}
			}
			allScenarios.Remove(this.m_CurrentBakingSet.lightingScenario);
			if (this.m_DebugActiveSceneGUID == guid && allScenarios.Count + 1 == this.m_DebugScenarioNames.Length && this.m_DebugActiveScenario == this.m_CurrentBakingSet.lightingScenario)
			{
				return;
			}
			int i = 0;
			ArrayExtensions.ResizeArray<GUIContent>(ref this.m_DebugScenarioNames, allScenarios.Count + 1);
			ArrayExtensions.ResizeArray<int>(ref this.m_DebugScenarioValues, allScenarios.Count + 1);
			this.m_DebugScenarioNames[0] = new GUIContent("None");
			this.m_DebugScenarioValues[0] = 0;
			foreach (string scenario2 in allScenarios)
			{
				i++;
				this.m_DebugScenarioNames[i] = new GUIContent(scenario2);
				this.m_DebugScenarioValues[i] = i;
			}
			this.m_DebugActiveSceneGUID = guid;
			this.m_DebugActiveScenario = this.m_CurrentBakingSet.lightingScenario;
			this.m_DebugScenarioField.enumNames = this.m_DebugScenarioNames;
			this.m_DebugScenarioField.enumValues = this.m_DebugScenarioValues;
			if (this.probeVolumeDebug.otherStateIndex >= this.m_DebugScenarioNames.Length)
			{
				this.probeVolumeDebug.otherStateIndex = 0;
			}
		}

		// Token: 0x04000392 RID: 914
		private ComputeBuffer m_EmptyIndexBuffer;

		// Token: 0x04000393 RID: 915
		private const int kProbesPerBatch = 511;

		// Token: 0x04000394 RID: 916
		public static readonly string k_DebugPanelName = "Probe Volumes";

		// Token: 0x04000397 RID: 919
		private Mesh m_DebugMesh;

		// Token: 0x04000398 RID: 920
		private DebugUI.Widget[] m_DebugItems;

		// Token: 0x04000399 RID: 921
		private Material m_DebugMaterial;

		// Token: 0x0400039A RID: 922
		private Mesh m_DebugProbeSamplingMesh;

		// Token: 0x0400039B RID: 923
		private Material m_ProbeSamplingDebugMaterial;

		// Token: 0x0400039C RID: 924
		private Material m_ProbeSamplingDebugMaterial02;

		// Token: 0x0400039D RID: 925
		private Texture m_DisplayNumbersTexture;

		// Token: 0x0400039E RID: 926
		internal static ProbeSamplingDebugData probeSamplingDebugData = new ProbeSamplingDebugData();

		// Token: 0x0400039F RID: 927
		private Mesh m_DebugOffsetMesh;

		// Token: 0x040003A0 RID: 928
		private Material m_DebugOffsetMaterial;

		// Token: 0x040003A1 RID: 929
		private Material m_DebugFragmentationMaterial;

		// Token: 0x040003A2 RID: 930
		private Plane[] m_DebugFrustumPlanes = new Plane[6];

		// Token: 0x040003A3 RID: 931
		private GUIContent[] m_DebugScenarioNames = new GUIContent[0];

		// Token: 0x040003A4 RID: 932
		private int[] m_DebugScenarioValues = new int[0];

		// Token: 0x040003A5 RID: 933
		private string m_DebugActiveSceneGUID;

		// Token: 0x040003A6 RID: 934
		private string m_DebugActiveScenario;

		// Token: 0x040003A7 RID: 935
		private DebugUI.EnumField m_DebugScenarioField;

		// Token: 0x040003A8 RID: 936
		internal Dictionary<Bounds, ProbeBrickIndex.Brick[]> realtimeSubdivisionInfo = new Dictionary<Bounds, ProbeBrickIndex.Brick[]>();

		// Token: 0x040003A9 RID: 937
		private bool m_MaxSubdivVisualizedIsMaxAvailable;

		// Token: 0x040003AA RID: 938
		private static Vector4[] s_BoundsArray = new Vector4[48];

		// Token: 0x040003AB RID: 939
		private bool m_LoadMaxCellsPerFrame;

		// Token: 0x040003AC RID: 940
		private const int kMaxCellLoadedPerFrame = 10;

		// Token: 0x040003AD RID: 941
		private int m_NumberOfCellsLoadedPerFrame = 1;

		// Token: 0x040003AE RID: 942
		private int m_NumberOfCellsBlendedPerFrame = 10000;

		// Token: 0x040003AF RID: 943
		private float m_TurnoverRate = 0.1f;

		// Token: 0x040003B0 RID: 944
		private DynamicArray<ProbeReferenceVolume.Cell> m_LoadedCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B1 RID: 945
		private DynamicArray<ProbeReferenceVolume.Cell> m_ToBeLoadedCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B2 RID: 946
		private DynamicArray<ProbeReferenceVolume.Cell> m_WorseLoadedCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B3 RID: 947
		private DynamicArray<ProbeReferenceVolume.Cell> m_BestToBeLoadedCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B4 RID: 948
		private DynamicArray<ProbeReferenceVolume.Cell> m_TempCellToLoadList = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B5 RID: 949
		private DynamicArray<ProbeReferenceVolume.Cell> m_TempCellToUnloadList = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B6 RID: 950
		private DynamicArray<ProbeReferenceVolume.Cell> m_LoadedBlendingCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B7 RID: 951
		private DynamicArray<ProbeReferenceVolume.Cell> m_ToBeLoadedBlendingCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B8 RID: 952
		private DynamicArray<ProbeReferenceVolume.Cell> m_TempBlendingCellToLoadList = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003B9 RID: 953
		private DynamicArray<ProbeReferenceVolume.Cell> m_TempBlendingCellToUnloadList = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003BA RID: 954
		private Vector3 m_FrozenCameraPosition;

		// Token: 0x040003BB RID: 955
		private Vector3 m_FrozenCameraDirection;

		// Token: 0x040003BC RID: 956
		private const float kIndexFragmentationThreshold = 0.2f;

		// Token: 0x040003BD RID: 957
		private bool m_IndexDefragmentationInProgress;

		// Token: 0x040003BE RID: 958
		private ProbeBrickIndex m_DefragIndex;

		// Token: 0x040003BF RID: 959
		private ProbeGlobalIndirection m_DefragCellIndices;

		// Token: 0x040003C0 RID: 960
		private DynamicArray<ProbeReferenceVolume.Cell> m_IndexDefragCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003C1 RID: 961
		private DynamicArray<ProbeReferenceVolume.Cell> m_TempIndexDefragCells = new DynamicArray<ProbeReferenceVolume.Cell>();

		// Token: 0x040003C2 RID: 962
		internal float minStreamingScore;

		// Token: 0x040003C3 RID: 963
		internal float maxStreamingScore;

		// Token: 0x040003C4 RID: 964
		private Queue<ProbeReferenceVolume.CellStreamingRequest> m_StreamingQueue = new Queue<ProbeReferenceVolume.CellStreamingRequest>();

		// Token: 0x040003C5 RID: 965
		private List<ProbeReferenceVolume.CellStreamingRequest> m_ActiveStreamingRequests = new List<ProbeReferenceVolume.CellStreamingRequest>();

		// Token: 0x040003C6 RID: 966
		private ObjectPool<ProbeReferenceVolume.CellStreamingRequest> m_StreamingRequestsPool = new ObjectPool<ProbeReferenceVolume.CellStreamingRequest>(null, delegate(ProbeReferenceVolume.CellStreamingRequest val)
		{
			val.Clear();
		}, true);

		// Token: 0x040003C7 RID: 967
		private bool m_DiskStreamingUseCompute;

		// Token: 0x040003C8 RID: 968
		private ProbeVolumeScratchBufferPool m_ScratchBufferPool;

		// Token: 0x040003C9 RID: 969
		private ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate m_OnStreamingComplete;

		// Token: 0x040003CA RID: 970
		private ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate m_OnBlendingStreamingComplete;

		// Token: 0x040003CB RID: 971
		private static DynamicArray<ProbeReferenceVolume.Cell>.SortComparer s_BlendingComparer = new DynamicArray<ProbeReferenceVolume.Cell>.SortComparer(ProbeReferenceVolume.BlendingComparer);

		// Token: 0x040003CC RID: 972
		private static DynamicArray<ProbeReferenceVolume.Cell>.SortComparer s_DefragComparer = new DynamicArray<ProbeReferenceVolume.Cell>.SortComparer(ProbeReferenceVolume.DefragComparer);

		// Token: 0x040003CD RID: 973
		private bool m_IsInitialized;

		// Token: 0x040003CE RID: 974
		private bool m_SupportScenarios;

		// Token: 0x040003CF RID: 975
		private bool m_SupportScenarioBlending;

		// Token: 0x040003D0 RID: 976
		private bool m_ForceNoDiskStreaming;

		// Token: 0x040003D1 RID: 977
		private bool m_SupportDiskStreaming;

		// Token: 0x040003D2 RID: 978
		private bool m_SupportGPUStreaming;

		// Token: 0x040003D3 RID: 979
		private bool m_UseStreamingAssets = true;

		// Token: 0x040003D4 RID: 980
		private float m_MinBrickSize;

		// Token: 0x040003D5 RID: 981
		private int m_MaxSubdivision;

		// Token: 0x040003D6 RID: 982
		private Vector3 m_ProbeOffset;

		// Token: 0x040003D7 RID: 983
		private ProbeBrickPool m_Pool;

		// Token: 0x040003D8 RID: 984
		private ProbeBrickIndex m_Index;

		// Token: 0x040003D9 RID: 985
		private ProbeGlobalIndirection m_CellIndices;

		// Token: 0x040003DA RID: 986
		private ProbeBrickBlendingPool m_BlendingPool;

		// Token: 0x040003DB RID: 987
		private List<ProbeBrickPool.BrickChunkAlloc> m_TmpSrcChunks = new List<ProbeBrickPool.BrickChunkAlloc>();

		// Token: 0x040003DC RID: 988
		private float[] m_PositionOffsets = new float[4];

		// Token: 0x040003DD RID: 989
		private Bounds m_CurrGlobalBounds;

		// Token: 0x040003DE RID: 990
		internal Dictionary<int, ProbeReferenceVolume.Cell> cells = new Dictionary<int, ProbeReferenceVolume.Cell>();

		// Token: 0x040003DF RID: 991
		private ObjectPool<ProbeReferenceVolume.Cell> m_CellPool = new ObjectPool<ProbeReferenceVolume.Cell>(delegate(ProbeReferenceVolume.Cell x)
		{
			x.Clear();
		}, null, false);

		// Token: 0x040003E0 RID: 992
		private ProbeBrickPool.DataLocation m_TemporaryDataLocation;

		// Token: 0x040003E1 RID: 993
		private int m_TemporaryDataLocationMemCost;

		// Token: 0x040003E2 RID: 994
		[Obsolete("This field is only kept for migration purpose.")]
		internal ProbeVolumeSceneData sceneData;

		// Token: 0x040003E3 RID: 995
		private Vector3Int minLoadedCellPos = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

		// Token: 0x040003E4 RID: 996
		private Vector3Int maxLoadedCellPos = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);

		// Token: 0x040003E5 RID: 997
		public Action<ProbeReferenceVolume.ExtraDataActionInput> retrieveExtraDataAction;

		// Token: 0x040003E6 RID: 998
		public Action checksDuringBakeAction;

		// Token: 0x040003E7 RID: 999
		private Dictionary<string, ValueTuple<ProbeVolumeBakingSet, List<int>>> m_PendingScenesToBeLoaded = new Dictionary<string, ValueTuple<ProbeVolumeBakingSet, List<int>>>();

		// Token: 0x040003E8 RID: 1000
		private Dictionary<string, List<int>> m_PendingScenesToBeUnloaded = new Dictionary<string, List<int>>();

		// Token: 0x040003E9 RID: 1001
		private List<string> m_ActiveScenes = new List<string>();

		// Token: 0x040003EA RID: 1002
		private ProbeVolumeBakingSet m_CurrentBakingSet;

		// Token: 0x040003EB RID: 1003
		private bool m_NeedLoadAsset;

		// Token: 0x040003EC RID: 1004
		private bool m_ProbeReferenceVolumeInit;

		// Token: 0x040003ED RID: 1005
		private bool m_EnabledBySRP;

		// Token: 0x040003EE RID: 1006
		private bool m_VertexSampling;

		// Token: 0x040003EF RID: 1007
		private bool m_NeedsIndexRebuild;

		// Token: 0x040003F0 RID: 1008
		private bool m_HasChangedIndex;

		// Token: 0x040003F1 RID: 1009
		private int m_CBShaderID = Shader.PropertyToID("ShaderVariablesProbeVolumes");

		// Token: 0x040003F2 RID: 1010
		private ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		// Token: 0x040003F3 RID: 1011
		private ProbeVolumeBlendingTextureMemoryBudget m_BlendingMemoryBudget;

		// Token: 0x040003F4 RID: 1012
		private ProbeVolumeSHBands m_SHBands;

		// Token: 0x040003F5 RID: 1013
		internal bool clearAssetsOnVolumeClear;

		// Token: 0x040003F6 RID: 1014
		internal static string defaultLightingScenario = "Default";

		// Token: 0x040003F7 RID: 1015
		private static ProbeReferenceVolume _instance = new ProbeReferenceVolume();

		// Token: 0x020000FF RID: 255
		internal static class ShaderIDs
		{
			// Token: 0x040003F9 RID: 1017
			public static readonly int _APVResIndex = Shader.PropertyToID("_APVResIndex");

			// Token: 0x040003FA RID: 1018
			public static readonly int _APVResCellIndices = Shader.PropertyToID("_APVResCellIndices");

			// Token: 0x040003FB RID: 1019
			public static readonly int _APVResL0_L1Rx = Shader.PropertyToID("_APVResL0_L1Rx");

			// Token: 0x040003FC RID: 1020
			public static readonly int _APVResL1G_L1Ry = Shader.PropertyToID("_APVResL1G_L1Ry");

			// Token: 0x040003FD RID: 1021
			public static readonly int _APVResL1B_L1Rz = Shader.PropertyToID("_APVResL1B_L1Rz");

			// Token: 0x040003FE RID: 1022
			public static readonly int _APVResL2_0 = Shader.PropertyToID("_APVResL2_0");

			// Token: 0x040003FF RID: 1023
			public static readonly int _APVResL2_1 = Shader.PropertyToID("_APVResL2_1");

			// Token: 0x04000400 RID: 1024
			public static readonly int _APVResL2_2 = Shader.PropertyToID("_APVResL2_2");

			// Token: 0x04000401 RID: 1025
			public static readonly int _APVResL2_3 = Shader.PropertyToID("_APVResL2_3");

			// Token: 0x04000402 RID: 1026
			public static readonly int _APVProbeOcclusion = Shader.PropertyToID("_APVProbeOcclusion");

			// Token: 0x04000403 RID: 1027
			public static readonly int _APVResValidity = Shader.PropertyToID("_APVResValidity");

			// Token: 0x04000404 RID: 1028
			public static readonly int _SkyOcclusionTexL0L1 = Shader.PropertyToID("_SkyOcclusionTexL0L1");

			// Token: 0x04000405 RID: 1029
			public static readonly int _SkyShadingDirectionIndicesTex = Shader.PropertyToID("_SkyShadingDirectionIndicesTex");

			// Token: 0x04000406 RID: 1030
			public static readonly int _SkyPrecomputedDirections = Shader.PropertyToID("_SkyPrecomputedDirections");

			// Token: 0x04000407 RID: 1031
			public static readonly int _AntiLeakData = Shader.PropertyToID("_AntiLeakData");
		}

		// Token: 0x02000100 RID: 256
		internal class CellInstancedDebugProbes
		{
			// Token: 0x04000408 RID: 1032
			public List<Matrix4x4[]> probeBuffers;

			// Token: 0x04000409 RID: 1033
			public List<Matrix4x4[]> offsetBuffers;

			// Token: 0x0400040A RID: 1034
			public List<MaterialPropertyBlock> props;
		}

		// Token: 0x02000101 RID: 257
		private class RenderFragmentationOverlayPassData
		{
			// Token: 0x0400040B RID: 1035
			public Material debugFragmentationMaterial;

			// Token: 0x0400040C RID: 1036
			public DebugOverlay debugOverlay;

			// Token: 0x0400040D RID: 1037
			public int chunkCount;

			// Token: 0x0400040E RID: 1038
			public ComputeBuffer debugFragmentationData;

			// Token: 0x0400040F RID: 1039
			public TextureHandle colorBuffer;

			// Token: 0x04000410 RID: 1040
			public TextureHandle depthBuffer;
		}

		// Token: 0x02000102 RID: 258
		internal class DiskStreamingRequest
		{
			// Token: 0x06000900 RID: 2304 RVA: 0x0001CFCA File Offset: 0x0001B1CA
			public DiskStreamingRequest(int maxRequestCount)
			{
				this.m_ReadCommandBuffer = new NativeArray<ReadCommand>(maxRequestCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
			public unsafe void AddReadCommand(int offset, int size, byte* dest)
			{
				int commandCount = this.m_ReadCommandArray.CommandCount;
				this.m_ReadCommandArray.CommandCount = commandCount + 1;
				this.m_ReadCommandBuffer[commandCount] = new ReadCommand
				{
					Buffer = (void*)dest,
					Offset = (long)offset,
					Size = (long)size
				};
				this.m_BytesWritten += size;
			}

			// Token: 0x06000902 RID: 2306 RVA: 0x0001D03C File Offset: 0x0001B23C
			public unsafe int RunCommands(FileHandle file)
			{
				this.m_ReadCommandArray.ReadCommands = (ReadCommand*)this.m_ReadCommandBuffer.GetUnsafePtr<ReadCommand>();
				this.m_ReadHandle = AsyncReadManager.Read(in file, this.m_ReadCommandArray);
				return this.m_BytesWritten;
			}

			// Token: 0x06000903 RID: 2307 RVA: 0x0001D070 File Offset: 0x0001B270
			public void Clear()
			{
				if (this.m_ReadHandle.IsValid())
				{
					this.m_ReadHandle.JobHandle.Complete();
				}
				this.m_ReadHandle = default(ReadHandle);
				this.m_ReadCommandArray.CommandCount = 0;
				this.m_BytesWritten = 0;
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x0001D0BC File Offset: 0x0001B2BC
			public void Cancel()
			{
				if (this.m_ReadHandle.IsValid())
				{
					this.m_ReadHandle.Cancel();
				}
			}

			// Token: 0x06000905 RID: 2309 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
			public void Wait()
			{
				if (this.m_ReadHandle.IsValid())
				{
					this.m_ReadHandle.JobHandle.Complete();
				}
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x0001D105 File Offset: 0x0001B305
			public void Dispose()
			{
				this.m_ReadCommandBuffer.Dispose();
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x0001D112 File Offset: 0x0001B312
			public ReadStatus GetStatus()
			{
				if (!this.m_ReadHandle.IsValid())
				{
					return ReadStatus.Complete;
				}
				return this.m_ReadHandle.Status;
			}

			// Token: 0x04000411 RID: 1041
			private ReadHandle m_ReadHandle;

			// Token: 0x04000412 RID: 1042
			private ReadCommandArray m_ReadCommandArray;

			// Token: 0x04000413 RID: 1043
			private NativeArray<ReadCommand> m_ReadCommandBuffer;

			// Token: 0x04000414 RID: 1044
			private int m_BytesWritten;
		}

		// Token: 0x02000103 RID: 259
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ProbeReferenceVolume.Streaming.cs", needAccessors = false, generateCBuffer = true)]
		internal struct CellStreamingScratchBufferLayout
		{
			// Token: 0x04000415 RID: 1045
			public int _SharedDestChunksOffset;

			// Token: 0x04000416 RID: 1046
			public int _L0L1rxOffset;

			// Token: 0x04000417 RID: 1047
			public int _L1GryOffset;

			// Token: 0x04000418 RID: 1048
			public int _L1BrzOffset;

			// Token: 0x04000419 RID: 1049
			public int _ValidityOffset;

			// Token: 0x0400041A RID: 1050
			public int _ProbeOcclusionOffset;

			// Token: 0x0400041B RID: 1051
			public int _SkyOcclusionOffset;

			// Token: 0x0400041C RID: 1052
			public int _SkyShadingDirectionOffset;

			// Token: 0x0400041D RID: 1053
			public int _L2_0Offset;

			// Token: 0x0400041E RID: 1054
			public int _L2_1Offset;

			// Token: 0x0400041F RID: 1055
			public int _L2_2Offset;

			// Token: 0x04000420 RID: 1056
			public int _L2_3Offset;

			// Token: 0x04000421 RID: 1057
			public int _L0Size;

			// Token: 0x04000422 RID: 1058
			public int _L0ProbeSize;

			// Token: 0x04000423 RID: 1059
			public int _L1Size;

			// Token: 0x04000424 RID: 1060
			public int _L1ProbeSize;

			// Token: 0x04000425 RID: 1061
			public int _ValiditySize;

			// Token: 0x04000426 RID: 1062
			public int _ValidityProbeSize;

			// Token: 0x04000427 RID: 1063
			public int _ProbeOcclusionSize;

			// Token: 0x04000428 RID: 1064
			public int _ProbeOcclusionProbeSize;

			// Token: 0x04000429 RID: 1065
			public int _SkyOcclusionSize;

			// Token: 0x0400042A RID: 1066
			public int _SkyOcclusionProbeSize;

			// Token: 0x0400042B RID: 1067
			public int _SkyShadingDirectionSize;

			// Token: 0x0400042C RID: 1068
			public int _SkyShadingDirectionProbeSize;

			// Token: 0x0400042D RID: 1069
			public int _L2Size;

			// Token: 0x0400042E RID: 1070
			public int _L2ProbeSize;

			// Token: 0x0400042F RID: 1071
			public int _ProbeCountInChunkLine;

			// Token: 0x04000430 RID: 1072
			public int _ProbeCountInChunkSlice;
		}

		// Token: 0x02000104 RID: 260
		internal class CellStreamingScratchBuffer
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x0001D130 File Offset: 0x0001B330
			public CellStreamingScratchBuffer(int chunkCount, int chunkSize, bool allocateGraphicsBuffers)
			{
				this.chunkCount = chunkCount;
				int bufferSize = chunkCount * chunkSize / 4 + chunkCount * 4;
				bufferSize += 2 * chunkCount * 4;
				if (allocateGraphicsBuffers)
				{
					for (int i = 0; i < 2; i++)
					{
						this.m_GraphicsBuffers[i] = new GraphicsBuffer(GraphicsBuffer.Target.Raw, GraphicsBuffer.UsageFlags.LockBufferForWrite, bufferSize, 4);
					}
				}
				this.m_CurrentBuffer = 0;
				this.stagingBuffer = new NativeArray<byte>(bufferSize * 4, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
			public void Swap()
			{
				this.m_CurrentBuffer = (this.m_CurrentBuffer + 1) % 2;
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
			public void Dispose()
			{
				for (int i = 0; i < 2; i++)
				{
					GraphicsBuffer graphicsBuffer = this.m_GraphicsBuffers[i];
					if (graphicsBuffer != null)
					{
						graphicsBuffer.Dispose();
					}
				}
				this.stagingBuffer.Dispose();
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001D1EB File Offset: 0x0001B3EB
			public GraphicsBuffer buffer
			{
				get
				{
					return this.m_GraphicsBuffers[this.m_CurrentBuffer];
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001D1FA File Offset: 0x0001B3FA
			public int chunkCount { get; }

			// Token: 0x04000431 RID: 1073
			public NativeArray<byte> stagingBuffer;

			// Token: 0x04000433 RID: 1075
			private int m_CurrentBuffer;

			// Token: 0x04000434 RID: 1076
			private GraphicsBuffer[] m_GraphicsBuffers = new GraphicsBuffer[2];
		}

		// Token: 0x02000105 RID: 261
		[DebuggerDisplay("Index = {cell.desc.index} State = {state}")]
		internal class CellStreamingRequest
		{
			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001D202 File Offset: 0x0001B402
			// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001D20A File Offset: 0x0001B40A
			public ProbeReferenceVolume.Cell cell { get; set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001D213 File Offset: 0x0001B413
			// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001D21B File Offset: 0x0001B41B
			public ProbeReferenceVolume.CellStreamingRequest.State state { get; set; }

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001D224 File Offset: 0x0001B424
			// (set) Token: 0x06000912 RID: 2322 RVA: 0x0001D22C File Offset: 0x0001B42C
			public ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer { get; set; }

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001D235 File Offset: 0x0001B435
			// (set) Token: 0x06000914 RID: 2324 RVA: 0x0001D23D File Offset: 0x0001B43D
			public ProbeReferenceVolume.CellStreamingScratchBufferLayout scratchBufferLayout { get; set; }

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001D246 File Offset: 0x0001B446
			// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001D24E File Offset: 0x0001B44E
			public ProbeVolumeBakingSet.PerScenarioDataInfo scenarioData { get; set; }

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001D257 File Offset: 0x0001B457
			// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001D25F File Offset: 0x0001B45F
			public int poolIndex { get; set; }

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001D268 File Offset: 0x0001B468
			// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001D270 File Offset: 0x0001B470
			public bool streamSharedData { get; set; }

			// Token: 0x0600091B RID: 2331 RVA: 0x0001D279 File Offset: 0x0001B479
			public bool IsStreaming()
			{
				return this.state == ProbeReferenceVolume.CellStreamingRequest.State.Pending || this.state == ProbeReferenceVolume.CellStreamingRequest.State.Active;
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x0001D290 File Offset: 0x0001B490
			public void Cancel()
			{
				if (this.state == ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					this.brickStreamingRequest.Cancel();
					this.supportStreamingRequest.Cancel();
					this.cellDataStreamingRequest.Cancel();
					this.cellOptionalDataStreamingRequest.Cancel();
					this.cellSharedDataStreamingRequest.Cancel();
					this.cellProbeOcclusionDataStreamingRequest.Cancel();
				}
				this.state = ProbeReferenceVolume.CellStreamingRequest.State.Canceled;
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
			public void WaitAll()
			{
				if (this.state == ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					this.brickStreamingRequest.Wait();
					this.supportStreamingRequest.Wait();
					this.cellDataStreamingRequest.Wait();
					this.cellOptionalDataStreamingRequest.Wait();
					this.cellSharedDataStreamingRequest.Wait();
					this.cellProbeOcclusionDataStreamingRequest.Wait();
				}
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x0001D348 File Offset: 0x0001B548
			public bool UpdateRequestState(ProbeReferenceVolume.DiskStreamingRequest request, ref bool isComplete)
			{
				ReadStatus status = request.GetStatus();
				if (status == ReadStatus.Failed)
				{
					return false;
				}
				isComplete &= status == ReadStatus.Complete;
				return true;
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x0001D36C File Offset: 0x0001B56C
			public void UpdateState()
			{
				if (this.state == ProbeReferenceVolume.CellStreamingRequest.State.Active)
				{
					bool isComplete = true;
					if (!(this.UpdateRequestState(this.brickStreamingRequest, ref isComplete) & this.UpdateRequestState(this.supportStreamingRequest, ref isComplete) & this.UpdateRequestState(this.cellDataStreamingRequest, ref isComplete) & this.UpdateRequestState(this.cellOptionalDataStreamingRequest, ref isComplete) & this.UpdateRequestState(this.cellSharedDataStreamingRequest, ref isComplete) & this.UpdateRequestState(this.cellProbeOcclusionDataStreamingRequest, ref isComplete)))
					{
						this.Cancel();
						this.state = ProbeReferenceVolume.CellStreamingRequest.State.Invalid;
						return;
					}
					if (isComplete)
					{
						this.state = ProbeReferenceVolume.CellStreamingRequest.State.Complete;
					}
				}
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x0001D3F7 File Offset: 0x0001B5F7
			public void Clear()
			{
				this.cell = null;
				this.Reset();
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x0001D408 File Offset: 0x0001B608
			public void Reset()
			{
				this.state = ProbeReferenceVolume.CellStreamingRequest.State.Pending;
				this.scratchBuffer = null;
				this.brickStreamingRequest.Clear();
				this.supportStreamingRequest.Clear();
				this.cellDataStreamingRequest.Clear();
				this.cellOptionalDataStreamingRequest.Clear();
				this.cellSharedDataStreamingRequest.Clear();
				this.cellProbeOcclusionDataStreamingRequest.Clear();
				this.bytesWritten = 0;
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x0001D46C File Offset: 0x0001B66C
			public void Dispose()
			{
				this.brickStreamingRequest.Dispose();
				this.supportStreamingRequest.Dispose();
				this.cellDataStreamingRequest.Dispose();
				this.cellOptionalDataStreamingRequest.Dispose();
				this.cellSharedDataStreamingRequest.Dispose();
				this.cellProbeOcclusionDataStreamingRequest.Dispose();
			}

			// Token: 0x0400043C RID: 1084
			public ProbeReferenceVolume.CellStreamingRequest.OnStreamingCompleteDelegate onStreamingComplete;

			// Token: 0x0400043D RID: 1085
			public ProbeReferenceVolume.DiskStreamingRequest cellDataStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(1);

			// Token: 0x0400043E RID: 1086
			public ProbeReferenceVolume.DiskStreamingRequest cellOptionalDataStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(1);

			// Token: 0x0400043F RID: 1087
			public ProbeReferenceVolume.DiskStreamingRequest cellSharedDataStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(1);

			// Token: 0x04000440 RID: 1088
			public ProbeReferenceVolume.DiskStreamingRequest cellProbeOcclusionDataStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(1);

			// Token: 0x04000441 RID: 1089
			public ProbeReferenceVolume.DiskStreamingRequest brickStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(1);

			// Token: 0x04000442 RID: 1090
			public ProbeReferenceVolume.DiskStreamingRequest supportStreamingRequest = new ProbeReferenceVolume.DiskStreamingRequest(5);

			// Token: 0x04000443 RID: 1091
			public int bytesWritten;

			// Token: 0x02000106 RID: 262
			public enum State
			{
				// Token: 0x04000445 RID: 1093
				Pending,
				// Token: 0x04000446 RID: 1094
				Active,
				// Token: 0x04000447 RID: 1095
				Canceled,
				// Token: 0x04000448 RID: 1096
				Invalid,
				// Token: 0x04000449 RID: 1097
				Complete
			}

			// Token: 0x02000107 RID: 263
			// (Invoke) Token: 0x06000925 RID: 2341
			public delegate void OnStreamingCompleteDelegate(ProbeReferenceVolume.CellStreamingRequest request, CommandBuffer cmd);
		}

		// Token: 0x02000108 RID: 264
		[Serializable]
		internal struct IndirectionEntryInfo
		{
			// Token: 0x0400044A RID: 1098
			public Vector3Int positionInBricks;

			// Token: 0x0400044B RID: 1099
			public int minSubdiv;

			// Token: 0x0400044C RID: 1100
			public Vector3Int minBrickPos;

			// Token: 0x0400044D RID: 1101
			public Vector3Int maxBrickPosPlusOne;

			// Token: 0x0400044E RID: 1102
			public bool hasMinMax;

			// Token: 0x0400044F RID: 1103
			public bool hasOnlyBiggerBricks;
		}

		// Token: 0x02000109 RID: 265
		[Serializable]
		internal class CellDesc
		{
			// Token: 0x06000928 RID: 2344 RVA: 0x0001D517 File Offset: 0x0001B717
			public override string ToString()
			{
				return string.Format("Index = {0} position = {1}", this.index, this.position);
			}

			// Token: 0x04000450 RID: 1104
			public Vector3Int position;

			// Token: 0x04000451 RID: 1105
			public int index;

			// Token: 0x04000452 RID: 1106
			public int probeCount;

			// Token: 0x04000453 RID: 1107
			public int minSubdiv;

			// Token: 0x04000454 RID: 1108
			public int indexChunkCount;

			// Token: 0x04000455 RID: 1109
			public int shChunkCount;

			// Token: 0x04000456 RID: 1110
			public int bricksCount;

			// Token: 0x04000457 RID: 1111
			public ProbeReferenceVolume.IndirectionEntryInfo[] indirectionEntryInfo;
		}

		// Token: 0x0200010A RID: 266
		internal class CellData
		{
			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001D539 File Offset: 0x0001B739
			// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001D541 File Offset: 0x0001B741
			public NativeArray<ushort> skyOcclusionDataL0L1 { get; internal set; }

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001D54A File Offset: 0x0001B74A
			// (set) Token: 0x0600092D RID: 2349 RVA: 0x0001D552 File Offset: 0x0001B752
			public NativeArray<byte> skyShadingDirectionIndices { get; internal set; }

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001D55B File Offset: 0x0001B75B
			// (set) Token: 0x0600092F RID: 2351 RVA: 0x0001D563 File Offset: 0x0001B763
			public NativeArray<ProbeBrickIndex.Brick> bricks { get; internal set; }

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000930 RID: 2352 RVA: 0x0001D56C File Offset: 0x0001B76C
			// (set) Token: 0x06000931 RID: 2353 RVA: 0x0001D574 File Offset: 0x0001B774
			public NativeArray<Vector3> probePositions { get; internal set; }

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001D57D File Offset: 0x0001B77D
			// (set) Token: 0x06000933 RID: 2355 RVA: 0x0001D585 File Offset: 0x0001B785
			public NativeArray<float> touchupVolumeInteraction { get; internal set; }

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001D58E File Offset: 0x0001B78E
			// (set) Token: 0x06000935 RID: 2357 RVA: 0x0001D596 File Offset: 0x0001B796
			public NativeArray<Vector3> offsetVectors { get; internal set; }

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001D59F File Offset: 0x0001B79F
			// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001D5A7 File Offset: 0x0001B7A7
			public NativeArray<float> validity { get; internal set; }

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
			// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
			public NativeArray<byte> layer { get; internal set; }

			// Token: 0x0600093A RID: 2362 RVA: 0x0001D5C4 File Offset: 0x0001B7C4
			public void CleanupPerScenarioData(in ProbeReferenceVolume.CellData.PerScenarioData data)
			{
				NativeArray<ushort> nativeArray = data.shL0L1RxData;
				NativeArray<byte> nativeArray2;
				if (nativeArray.IsCreated)
				{
					nativeArray = data.shL0L1RxData;
					nativeArray.Dispose();
					nativeArray2 = data.shL1GL1RyData;
					nativeArray2.Dispose();
					nativeArray2 = data.shL1BL1RzData;
					nativeArray2.Dispose();
				}
				nativeArray2 = data.shL2Data_0;
				if (nativeArray2.IsCreated)
				{
					nativeArray2 = data.shL2Data_0;
					nativeArray2.Dispose();
					nativeArray2 = data.shL2Data_1;
					nativeArray2.Dispose();
					nativeArray2 = data.shL2Data_2;
					nativeArray2.Dispose();
					nativeArray2 = data.shL2Data_3;
					nativeArray2.Dispose();
				}
				nativeArray2 = data.probeOcclusion;
				if (nativeArray2.IsCreated)
				{
					nativeArray2 = data.probeOcclusion;
					nativeArray2.Dispose();
				}
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x0001D674 File Offset: 0x0001B874
			public void Cleanup(bool cleanScenarioList)
			{
				if (this.validityNeighMaskData.IsCreated)
				{
					this.validityNeighMaskData.Dispose();
					this.validityNeighMaskData = default(NativeArray<byte>);
					foreach (ProbeReferenceVolume.CellData.PerScenarioData scenario in this.scenarios.Values)
					{
						this.CleanupPerScenarioData(in scenario);
					}
				}
				if (cleanScenarioList)
				{
					this.scenarios.Clear();
				}
				if (this.bricks.IsCreated)
				{
					this.bricks.Dispose();
					this.bricks = default(NativeArray<ProbeBrickIndex.Brick>);
				}
				if (this.skyOcclusionDataL0L1.IsCreated)
				{
					this.skyOcclusionDataL0L1.Dispose();
					this.skyOcclusionDataL0L1 = default(NativeArray<ushort>);
				}
				if (this.skyShadingDirectionIndices.IsCreated)
				{
					this.skyShadingDirectionIndices.Dispose();
					this.skyShadingDirectionIndices = default(NativeArray<byte>);
				}
				if (this.probePositions.IsCreated)
				{
					this.probePositions.Dispose();
					this.probePositions = default(NativeArray<Vector3>);
				}
				if (this.touchupVolumeInteraction.IsCreated)
				{
					this.touchupVolumeInteraction.Dispose();
					this.touchupVolumeInteraction = default(NativeArray<float>);
				}
				if (this.validity.IsCreated)
				{
					this.validity.Dispose();
					this.validity = default(NativeArray<float>);
				}
				if (this.layer.IsCreated)
				{
					this.layer.Dispose();
					this.layer = default(NativeArray<byte>);
				}
				if (this.offsetVectors.IsCreated)
				{
					this.offsetVectors.Dispose();
					this.offsetVectors = default(NativeArray<Vector3>);
				}
			}

			// Token: 0x04000458 RID: 1112
			public NativeArray<byte> validityNeighMaskData;

			// Token: 0x0400045B RID: 1115
			public Dictionary<string, ProbeReferenceVolume.CellData.PerScenarioData> scenarios = new Dictionary<string, ProbeReferenceVolume.CellData.PerScenarioData>();

			// Token: 0x0200010B RID: 267
			public struct PerScenarioData
			{
				// Token: 0x04000462 RID: 1122
				public NativeArray<ushort> shL0L1RxData;

				// Token: 0x04000463 RID: 1123
				public NativeArray<byte> shL1GL1RyData;

				// Token: 0x04000464 RID: 1124
				public NativeArray<byte> shL1BL1RzData;

				// Token: 0x04000465 RID: 1125
				public NativeArray<byte> shL2Data_0;

				// Token: 0x04000466 RID: 1126
				public NativeArray<byte> shL2Data_1;

				// Token: 0x04000467 RID: 1127
				public NativeArray<byte> shL2Data_2;

				// Token: 0x04000468 RID: 1128
				public NativeArray<byte> shL2Data_3;

				// Token: 0x04000469 RID: 1129
				public NativeArray<byte> probeOcclusion;
			}
		}

		// Token: 0x0200010C RID: 268
		internal class CellPoolInfo
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x0001D88F File Offset: 0x0001BA8F
			public void Clear()
			{
				this.chunkList.Clear();
			}

			// Token: 0x0400046A RID: 1130
			public List<ProbeBrickPool.BrickChunkAlloc> chunkList = new List<ProbeBrickPool.BrickChunkAlloc>();

			// Token: 0x0400046B RID: 1131
			public int shChunkCount;
		}

		// Token: 0x0200010D RID: 269
		internal class CellIndexInfo
		{
			// Token: 0x0600093F RID: 2367 RVA: 0x0001D8AF File Offset: 0x0001BAAF
			public void Clear()
			{
				this.flatIndicesInGlobalIndirection = null;
				this.updateInfo = default(ProbeBrickIndex.CellIndexUpdateInfo);
				this.indexUpdated = false;
				this.indirectionEntryInfo = null;
			}

			// Token: 0x0400046C RID: 1132
			public int[] flatIndicesInGlobalIndirection;

			// Token: 0x0400046D RID: 1133
			public ProbeBrickIndex.CellIndexUpdateInfo updateInfo;

			// Token: 0x0400046E RID: 1134
			public bool indexUpdated;

			// Token: 0x0400046F RID: 1135
			public ProbeReferenceVolume.IndirectionEntryInfo[] indirectionEntryInfo;

			// Token: 0x04000470 RID: 1136
			public int indexChunkCount;
		}

		// Token: 0x0200010E RID: 270
		internal class CellBlendingInfo
		{
			// Token: 0x06000941 RID: 2369 RVA: 0x0001D8D2 File Offset: 0x0001BAD2
			public void MarkUpToDate()
			{
				this.blendingScore = float.MaxValue;
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x0001D8DF File Offset: 0x0001BADF
			public bool IsUpToDate()
			{
				return this.blendingScore == float.MaxValue;
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0001D8EE File Offset: 0x0001BAEE
			public void ForceReupload()
			{
				this.blendingFactor = -1f;
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0001D8FB File Offset: 0x0001BAFB
			public bool ShouldReupload()
			{
				return this.blendingFactor == -1f;
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x0001D90A File Offset: 0x0001BB0A
			public void Prioritize()
			{
				this.blendingFactor = -2f;
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x0001D917 File Offset: 0x0001BB17
			public bool ShouldPrioritize()
			{
				return this.blendingFactor == -2f;
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x0001D926 File Offset: 0x0001BB26
			public void Clear()
			{
				this.chunkList.Clear();
				this.blendingScore = 0f;
				this.blendingFactor = 0f;
				this.blending = false;
			}

			// Token: 0x04000471 RID: 1137
			public List<ProbeBrickPool.BrickChunkAlloc> chunkList = new List<ProbeBrickPool.BrickChunkAlloc>();

			// Token: 0x04000472 RID: 1138
			public float blendingScore;

			// Token: 0x04000473 RID: 1139
			public float blendingFactor;

			// Token: 0x04000474 RID: 1140
			public bool blending;
		}

		// Token: 0x0200010F RID: 271
		internal class CellStreamingInfo
		{
			// Token: 0x06000949 RID: 2377 RVA: 0x0001D963 File Offset: 0x0001BB63
			public bool IsStreaming()
			{
				return this.request != null && this.request.IsStreaming();
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0001D97A File Offset: 0x0001BB7A
			public bool IsBlendingStreaming()
			{
				return (this.blendingRequest0 != null && this.blendingRequest0.IsStreaming()) || (this.blendingRequest1 != null && this.blendingRequest1.IsStreaming());
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
			public void Clear()
			{
				this.request = null;
				this.blendingRequest0 = null;
				this.blendingRequest1 = null;
				this.streamingScore = 0f;
			}

			// Token: 0x04000475 RID: 1141
			public ProbeReferenceVolume.CellStreamingRequest request;

			// Token: 0x04000476 RID: 1142
			public ProbeReferenceVolume.CellStreamingRequest blendingRequest0;

			// Token: 0x04000477 RID: 1143
			public ProbeReferenceVolume.CellStreamingRequest blendingRequest1;

			// Token: 0x04000478 RID: 1144
			public float streamingScore;
		}

		// Token: 0x02000110 RID: 272
		[DebuggerDisplay("Index = {desc.index} Loaded = {loaded}")]
		internal class Cell : IComparable<ProbeReferenceVolume.Cell>
		{
			// Token: 0x0600094D RID: 2381 RVA: 0x0001D9CA File Offset: 0x0001BBCA
			public int CompareTo(ProbeReferenceVolume.Cell other)
			{
				if (this.streamingInfo.streamingScore < other.streamingInfo.streamingScore)
				{
					return -1;
				}
				if (this.streamingInfo.streamingScore > other.streamingInfo.streamingScore)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x0600094E RID: 2382 RVA: 0x0001DA04 File Offset: 0x0001BC04
			public bool UpdateCellScenarioData(string scenario0, string scenario1)
			{
				if (!this.data.scenarios.TryGetValue(scenario0, out this.scenario0))
				{
					return false;
				}
				this.hasTwoScenarios = false;
				if (!string.IsNullOrEmpty(scenario1) && this.data.scenarios.TryGetValue(scenario1, out this.scenario1))
				{
					this.hasTwoScenarios = true;
				}
				return true;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0001DA5C File Offset: 0x0001BC5C
			public void Clear()
			{
				this.desc = null;
				this.data = null;
				this.poolInfo.Clear();
				this.indexInfo.Clear();
				this.blendingInfo.Clear();
				this.streamingInfo.Clear();
				this.referenceCount = 0;
				this.loaded = false;
				this.scenario0 = default(ProbeReferenceVolume.CellData.PerScenarioData);
				this.scenario1 = default(ProbeReferenceVolume.CellData.PerScenarioData);
				this.hasTwoScenarios = false;
				this.debugProbes = null;
			}

			// Token: 0x04000479 RID: 1145
			public ProbeReferenceVolume.CellDesc desc;

			// Token: 0x0400047A RID: 1146
			public ProbeReferenceVolume.CellData data;

			// Token: 0x0400047B RID: 1147
			public ProbeReferenceVolume.CellPoolInfo poolInfo = new ProbeReferenceVolume.CellPoolInfo();

			// Token: 0x0400047C RID: 1148
			public ProbeReferenceVolume.CellIndexInfo indexInfo = new ProbeReferenceVolume.CellIndexInfo();

			// Token: 0x0400047D RID: 1149
			public ProbeReferenceVolume.CellBlendingInfo blendingInfo = new ProbeReferenceVolume.CellBlendingInfo();

			// Token: 0x0400047E RID: 1150
			public ProbeReferenceVolume.CellStreamingInfo streamingInfo = new ProbeReferenceVolume.CellStreamingInfo();

			// Token: 0x0400047F RID: 1151
			public int referenceCount;

			// Token: 0x04000480 RID: 1152
			public bool loaded;

			// Token: 0x04000481 RID: 1153
			public ProbeReferenceVolume.CellData.PerScenarioData scenario0;

			// Token: 0x04000482 RID: 1154
			public ProbeReferenceVolume.CellData.PerScenarioData scenario1;

			// Token: 0x04000483 RID: 1155
			public bool hasTwoScenarios;

			// Token: 0x04000484 RID: 1156
			public ProbeReferenceVolume.CellInstancedDebugProbes debugProbes;
		}

		// Token: 0x02000111 RID: 273
		internal struct Volume : IEquatable<ProbeReferenceVolume.Volume>
		{
			// Token: 0x06000951 RID: 2385 RVA: 0x0001DB0C File Offset: 0x0001BD0C
			public Volume(Matrix4x4 trs, float maxSubdivision, float minSubdivision)
			{
				this.X = trs.GetColumn(0);
				this.Y = trs.GetColumn(1);
				this.Z = trs.GetColumn(2);
				this.corner = trs.GetColumn(3) - this.X * 0.5f - this.Y * 0.5f - this.Z * 0.5f;
				this.maxSubdivisionMultiplier = maxSubdivision;
				this.minSubdivisionMultiplier = minSubdivision;
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x0001DBB2 File Offset: 0x0001BDB2
			public Volume(Vector3 corner, Vector3 X, Vector3 Y, Vector3 Z, float maxSubdivision = 1f, float minSubdivision = 0f)
			{
				this.corner = corner;
				this.X = X;
				this.Y = Y;
				this.Z = Z;
				this.maxSubdivisionMultiplier = maxSubdivision;
				this.minSubdivisionMultiplier = minSubdivision;
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
			public Volume(ProbeReferenceVolume.Volume copy)
			{
				this.X = copy.X;
				this.Y = copy.Y;
				this.Z = copy.Z;
				this.corner = copy.corner;
				this.maxSubdivisionMultiplier = copy.maxSubdivisionMultiplier;
				this.minSubdivisionMultiplier = copy.minSubdivisionMultiplier;
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x0001DC3C File Offset: 0x0001BE3C
			public Volume(Bounds bounds)
			{
				Vector3 size = bounds.size;
				this.corner = bounds.center - size * 0.5f;
				this.X = new Vector3(size.x, 0f, 0f);
				this.Y = new Vector3(0f, size.y, 0f);
				this.Z = new Vector3(0f, 0f, size.z);
				this.maxSubdivisionMultiplier = (this.minSubdivisionMultiplier = 0f);
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x0001DCD4 File Offset: 0x0001BED4
			public Bounds CalculateAABB()
			{
				Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				for (int x = 0; x < 2; x++)
				{
					for (int y = 0; y < 2; y++)
					{
						for (int z = 0; z < 2; z++)
						{
							Vector3 dir = new Vector3((float)x, (float)y, (float)z);
							Vector3 pt = this.corner + this.X * dir.x + this.Y * dir.y + this.Z * dir.z;
							min = Vector3.Min(min, pt);
							max = Vector3.Max(max, pt);
						}
					}
				}
				return new Bounds((min + max) / 2f, max - min);
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x0001DDCC File Offset: 0x0001BFCC
			public void CalculateCenterAndSize(out Vector3 center, out Vector3 size)
			{
				size = new Vector3(this.X.magnitude, this.Y.magnitude, this.Z.magnitude);
				center = this.corner + this.X * 0.5f + this.Y * 0.5f + this.Z * 0.5f;
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x0001DE50 File Offset: 0x0001C050
			public void Transform(Matrix4x4 trs)
			{
				this.corner = trs.MultiplyPoint(this.corner);
				this.X = trs.MultiplyVector(this.X);
				this.Y = trs.MultiplyVector(this.Y);
				this.Z = trs.MultiplyVector(this.Z);
			}

			// Token: 0x06000958 RID: 2392 RVA: 0x0001DEAC File Offset: 0x0001C0AC
			public override string ToString()
			{
				return string.Format("Corner: {0}, X: {1}, Y: {2}, Z: {3}, MaxSubdiv: {4}", new object[] { this.corner, this.X, this.Y, this.Z, this.maxSubdivisionMultiplier });
			}

			// Token: 0x06000959 RID: 2393 RVA: 0x0001DF10 File Offset: 0x0001C110
			public bool Equals(ProbeReferenceVolume.Volume other)
			{
				return this.corner == other.corner && this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.minSubdivisionMultiplier == other.minSubdivisionMultiplier && this.maxSubdivisionMultiplier == other.maxSubdivisionMultiplier;
			}

			// Token: 0x04000485 RID: 1157
			internal Vector3 corner;

			// Token: 0x04000486 RID: 1158
			internal Vector3 X;

			// Token: 0x04000487 RID: 1159
			internal Vector3 Y;

			// Token: 0x04000488 RID: 1160
			internal Vector3 Z;

			// Token: 0x04000489 RID: 1161
			internal float maxSubdivisionMultiplier;

			// Token: 0x0400048A RID: 1162
			internal float minSubdivisionMultiplier;
		}

		// Token: 0x02000112 RID: 274
		internal struct RefVolTransform
		{
			// Token: 0x0400048B RID: 1163
			public Vector3 posWS;

			// Token: 0x0400048C RID: 1164
			public Quaternion rot;

			// Token: 0x0400048D RID: 1165
			public float scale;
		}

		// Token: 0x02000113 RID: 275
		public struct RuntimeResources
		{
			// Token: 0x0400048E RID: 1166
			public ComputeBuffer index;

			// Token: 0x0400048F RID: 1167
			public ComputeBuffer cellIndices;

			// Token: 0x04000490 RID: 1168
			public RenderTexture L0_L1rx;

			// Token: 0x04000491 RID: 1169
			public RenderTexture L1_G_ry;

			// Token: 0x04000492 RID: 1170
			public RenderTexture L1_B_rz;

			// Token: 0x04000493 RID: 1171
			public RenderTexture L2_0;

			// Token: 0x04000494 RID: 1172
			public RenderTexture L2_1;

			// Token: 0x04000495 RID: 1173
			public RenderTexture L2_2;

			// Token: 0x04000496 RID: 1174
			public RenderTexture L2_3;

			// Token: 0x04000497 RID: 1175
			public RenderTexture ProbeOcclusion;

			// Token: 0x04000498 RID: 1176
			public RenderTexture Validity;

			// Token: 0x04000499 RID: 1177
			public RenderTexture SkyOcclusionL0L1;

			// Token: 0x0400049A RID: 1178
			public RenderTexture SkyShadingDirectionIndices;

			// Token: 0x0400049B RID: 1179
			public ComputeBuffer SkyPrecomputedDirections;

			// Token: 0x0400049C RID: 1180
			public ComputeBuffer QualityLeakReductionData;
		}

		// Token: 0x02000114 RID: 276
		public struct ExtraDataActionInput
		{
		}
	}
}
