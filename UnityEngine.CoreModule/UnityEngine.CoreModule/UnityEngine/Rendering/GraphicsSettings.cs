using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035A RID: 858
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : Object
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001674 RID: 5748
		// (set) Token: 0x06001675 RID: 5749
		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000371 RID: 881
		// (set) Token: 0x06001676 RID: 5750
		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000372 RID: 882
		// (set) Token: 0x06001677 RID: 5751
		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001678 RID: 5752
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		// Token: 0x06001679 RID: 5753 RVA: 0x0002F304 File Offset: 0x0002D504
		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0002F324 File Offset: 0x0002D524
		[NativeName("CurrentRenderPipeline")]
		private static ScriptableObject INTERNAL_currentRenderPipeline
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(GraphicsSettings.get_INTERNAL_currentRenderPipeline_Injected());
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0002F33C File Offset: 0x0002D53C
		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0002F358 File Offset: 0x0002D558
		public static bool isScriptableRenderPipelineEnabled
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline != null;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0002F365 File Offset: 0x0002D565
		public static Type currentRenderPipelineAssetType
		{
			get
			{
				return GraphicsSettings.isScriptableRenderPipelineEnabled ? GraphicsSettings.INTERNAL_currentRenderPipeline.GetType() : null;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0002F37C File Offset: 0x0002D57C
		[RequiredByNativeCode]
		[VisibleToOtherModules]
		internal static Shader GetDefaultShader(DefaultShaderType type)
		{
			RenderPipelineAsset rp = GraphicsSettings.currentRenderPipeline;
			bool flag = GraphicsSettings.currentRenderPipeline == null;
			Shader shader;
			if (flag)
			{
				shader = null;
			}
			else
			{
				if (!true)
				{
				}
				Shader shader2;
				switch (type)
				{
				case DefaultShaderType.Default:
					shader2 = rp.defaultShader;
					break;
				case DefaultShaderType.AutodeskInteractive:
					shader2 = rp.autodeskInteractiveShader;
					break;
				case DefaultShaderType.AutodeskInteractiveTransparent:
					shader2 = rp.autodeskInteractiveTransparentShader;
					break;
				case DefaultShaderType.AutodeskInteractiveMasked:
					shader2 = rp.autodeskInteractiveMaskedShader;
					break;
				case DefaultShaderType.TerrainDetailLit:
					shader2 = rp.terrainDetailLitShader;
					break;
				case DefaultShaderType.TerrainDetailGrass:
					shader2 = rp.terrainDetailGrassShader;
					break;
				case DefaultShaderType.TerrainDetailGrassBillboard:
					shader2 = rp.terrainDetailGrassBillboardShader;
					break;
				case DefaultShaderType.SpeedTree7:
					shader2 = rp.defaultSpeedTree7Shader;
					break;
				case DefaultShaderType.SpeedTree8:
					shader2 = rp.defaultSpeedTree8Shader;
					break;
				case DefaultShaderType.SpeedTree9:
					shader2 = rp.defaultSpeedTree9Shader;
					break;
				default:
					throw new NotImplementedException(string.Format("DefaultShaderType {0} not implemented", type));
				}
				if (!true)
				{
				}
				shader = shader2;
			}
			return shader;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0002F454 File Offset: 0x0002D654
		[RequiredByNativeCode]
		[VisibleToOtherModules]
		internal static Material GetDefaultMaterial(DefaultMaterialType type)
		{
			RenderPipelineAsset rp = GraphicsSettings.currentRenderPipeline;
			bool flag = GraphicsSettings.currentRenderPipeline == null;
			Material material;
			if (flag)
			{
				material = null;
			}
			else
			{
				if (!true)
				{
				}
				Material material2;
				switch (type)
				{
				case DefaultMaterialType.Default:
					material2 = rp.defaultMaterial;
					break;
				case DefaultMaterialType.Particle:
					material2 = rp.defaultParticleMaterial;
					break;
				case DefaultMaterialType.Line:
					material2 = rp.defaultLineMaterial;
					break;
				case DefaultMaterialType.Terrain:
					material2 = rp.defaultTerrainMaterial;
					break;
				case DefaultMaterialType.Sprite:
					material2 = rp.default2DMaterial;
					break;
				case DefaultMaterialType.SpriteMask:
					material2 = rp.default2DMaskMaterial;
					break;
				case DefaultMaterialType.UGUI:
					material2 = rp.defaultUIMaterial;
					break;
				case DefaultMaterialType.UGUI_Overdraw:
					material2 = rp.defaultUIOverdrawMaterial;
					break;
				case DefaultMaterialType.UGUI_ETC1Supported:
					material2 = rp.defaultUIETC1SupportedMaterial;
					break;
				default:
					throw new NotImplementedException(string.Format("DefaultMaterialType {0} not implemented", type));
				}
				if (!true)
				{
				}
				material = material2;
			}
			return material;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0002F520 File Offset: 0x0002D720
		[NativeName("GetSettingsForRenderPipeline")]
		private unsafe static Object Internal_GetSettingsForRenderPipeline(string renderpipelineName)
		{
			Object @object;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(renderpipelineName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = renderpipelineName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GraphicsSettings.Internal_GetSettingsForRenderPipeline_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0002F580 File Offset: 0x0002D780
		public static RenderPipelineGlobalSettings GetSettingsForRenderPipeline<T>() where T : RenderPipeline
		{
			return GraphicsSettings.Internal_GetSettingsForRenderPipeline(typeof(T).FullName) as RenderPipelineGlobalSettings;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0002F5AC File Offset: 0x0002D7AC
		private static RenderPipelineGlobalSettings Internal_GetCurrentRenderPipelineGlobalSettings()
		{
			RenderPipelineGlobalSettings asset = null;
			bool flag = GraphicsSettings.currentRenderPipeline != null;
			if (flag)
			{
				asset = GraphicsSettings.Internal_GetSettingsForRenderPipeline(GraphicsSettings.currentRenderPipeline.pipelineTypeFullName) as RenderPipelineGlobalSettings;
			}
			return asset;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		public static bool TryGetCurrentRenderPipelineGlobalSettings(out RenderPipelineGlobalSettings asset)
		{
			asset = GraphicsSettings.s_CurrentRenderPipelineGlobalSettings.Value;
			return asset != null;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0002F610 File Offset: 0x0002D810
		public static T GetRenderPipelineSettings<T>() where T : class, IRenderPipelineGraphicsSettings
		{
			T settings;
			GraphicsSettings.TryGetRenderPipelineSettings<T>(out settings);
			return settings;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0002F62C File Offset: 0x0002D82C
		public static bool TryGetRenderPipelineSettings<T>(out T settings) where T : class, IRenderPipelineGraphicsSettings
		{
			settings = default(T);
			RenderPipelineGlobalSettings asset;
			bool flag = !GraphicsSettings.TryGetCurrentRenderPipelineGlobalSettings(out asset);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				IRenderPipelineGraphicsSettings baseSettings;
				bool flag3 = asset.TryGet(typeof(T), out baseSettings);
				if (flag3)
				{
					settings = baseSettings as T;
				}
				flag2 = settings != null;
			}
			return flag2;
		}

		// Token: 0x06001687 RID: 5767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_INTERNAL_currentRenderPipeline_Injected();

		// Token: 0x06001688 RID: 5768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetSettingsForRenderPipeline_Injected(ref ManagedSpanWrapper renderpipelineName);

		// Token: 0x04000A26 RID: 2598
		private static Lazy<RenderPipelineGlobalSettings> s_CurrentRenderPipelineGlobalSettings = new Lazy<RenderPipelineGlobalSettings>(() => GraphicsSettings.Internal_GetCurrentRenderPipelineGlobalSettings());
	}
}
