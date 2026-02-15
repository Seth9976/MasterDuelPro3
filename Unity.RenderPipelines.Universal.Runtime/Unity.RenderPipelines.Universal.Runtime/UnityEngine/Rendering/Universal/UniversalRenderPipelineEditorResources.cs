using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B9 RID: 185
	[Obsolete("Moved to GraphicsSettings. #from(23.3)", false)]
	public class UniversalRenderPipelineEditorResources : ScriptableObject
	{
		// Token: 0x040003B4 RID: 948
		[Obsolete("UniversalRenderPipelineEditorResources.ShaderResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorShaders>(). #from(23.3)", false)]
		public UniversalRenderPipelineEditorResources.ShaderResources shaders;

		// Token: 0x040003B5 RID: 949
		[Obsolete("UniversalRenderPipelineEditorResources.MaterialResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorMaterials>(). #from(23.3)", false)]
		public UniversalRenderPipelineEditorResources.MaterialResources materials;

		// Token: 0x020000BA RID: 186
		[ReloadGroup]
		[Obsolete("UniversalRenderPipelineEditorResources.ShaderResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorShaders>(). #from(23.3)", false)]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040003B6 RID: 950
			[Reload("Shaders/AutodeskInteractive/AutodeskInteractive.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractivePS;

			// Token: 0x040003B7 RID: 951
			[Reload("Shaders/AutodeskInteractive/AutodeskInteractiveTransparent.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractiveTransparentPS;

			// Token: 0x040003B8 RID: 952
			[Reload("Shaders/AutodeskInteractive/AutodeskInteractiveMasked.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractiveMaskedPS;

			// Token: 0x040003B9 RID: 953
			[Reload("Shaders/Terrain/TerrainDetailLit.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailLitPS;

			// Token: 0x040003BA RID: 954
			[Reload("Shaders/Terrain/WavingGrass.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailGrassPS;

			// Token: 0x040003BB RID: 955
			[Reload("Shaders/Terrain/WavingGrassBillboard.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailGrassBillboardPS;

			// Token: 0x040003BC RID: 956
			[Reload("Shaders/Nature/SpeedTree7.shader", ReloadAttribute.Package.Root)]
			public Shader defaultSpeedTree7PS;

			// Token: 0x040003BD RID: 957
			[Reload("Shaders/Nature/SpeedTree8_PBRLit.shadergraph", ReloadAttribute.Package.Root)]
			public Shader defaultSpeedTree8PS;
		}

		// Token: 0x020000BB RID: 187
		[ReloadGroup]
		[Obsolete("UniversalRenderPipelineEditorResources.MaterialResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorMaterials>(). #from(23.3)", false)]
		[Serializable]
		public sealed class MaterialResources
		{
			// Token: 0x040003BE RID: 958
			[Reload("Runtime/Materials/Lit.mat", ReloadAttribute.Package.Root)]
			public Material lit;

			// Token: 0x040003BF RID: 959
			[Reload("Runtime/Materials/ParticlesUnlit.mat", ReloadAttribute.Package.Root)]
			public Material particleLit;

			// Token: 0x040003C0 RID: 960
			[Reload("Runtime/Materials/TerrainLit.mat", ReloadAttribute.Package.Root)]
			public Material terrainLit;

			// Token: 0x040003C1 RID: 961
			[Reload("Runtime/Materials/Decal.mat", ReloadAttribute.Package.Root)]
			public Material decal;
		}
	}
}
