using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class PostProcessData : ScriptableObject
	{
		// Token: 0x04000028 RID: 40
		public PostProcessData.ShaderResources shaders;

		// Token: 0x04000029 RID: 41
		public PostProcessData.TextureResources textures;

		// Token: 0x02000011 RID: 17
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x0400002A RID: 42
			[Reload("Shaders/PostProcessing/StopNaN.shader", ReloadAttribute.Package.Root)]
			public Shader stopNanPS;

			// Token: 0x0400002B RID: 43
			[Reload("Shaders/PostProcessing/SubpixelMorphologicalAntialiasing.shader", ReloadAttribute.Package.Root)]
			public Shader subpixelMorphologicalAntialiasingPS;

			// Token: 0x0400002C RID: 44
			[Reload("Shaders/PostProcessing/GaussianDepthOfField.shader", ReloadAttribute.Package.Root)]
			public Shader gaussianDepthOfFieldPS;

			// Token: 0x0400002D RID: 45
			[Reload("Shaders/PostProcessing/BokehDepthOfField.shader", ReloadAttribute.Package.Root)]
			public Shader bokehDepthOfFieldPS;

			// Token: 0x0400002E RID: 46
			[Reload("Shaders/PostProcessing/CameraMotionBlur.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionBlurPS;

			// Token: 0x0400002F RID: 47
			[Reload("Shaders/PostProcessing/PaniniProjection.shader", ReloadAttribute.Package.Root)]
			public Shader paniniProjectionPS;

			// Token: 0x04000030 RID: 48
			[Reload("Shaders/PostProcessing/LutBuilderLdr.shader", ReloadAttribute.Package.Root)]
			public Shader lutBuilderLdrPS;

			// Token: 0x04000031 RID: 49
			[Reload("Shaders/PostProcessing/LutBuilderHdr.shader", ReloadAttribute.Package.Root)]
			public Shader lutBuilderHdrPS;

			// Token: 0x04000032 RID: 50
			[Reload("Shaders/PostProcessing/Bloom.shader", ReloadAttribute.Package.Root)]
			public Shader bloomPS;

			// Token: 0x04000033 RID: 51
			[Reload("Shaders/PostProcessing/TemporalAA.shader", ReloadAttribute.Package.Root)]
			public Shader temporalAntialiasingPS;

			// Token: 0x04000034 RID: 52
			[Reload("Shaders/PostProcessing/LensFlareDataDriven.shader", ReloadAttribute.Package.Root)]
			public Shader LensFlareDataDrivenPS;

			// Token: 0x04000035 RID: 53
			[Reload("Shaders/PostProcessing/LensFlareScreenSpace.shader", ReloadAttribute.Package.Root)]
			public Shader LensFlareScreenSpacePS;

			// Token: 0x04000036 RID: 54
			[Reload("Shaders/PostProcessing/ScalingSetup.shader", ReloadAttribute.Package.Root)]
			public Shader scalingSetupPS;

			// Token: 0x04000037 RID: 55
			[Reload("Shaders/PostProcessing/EdgeAdaptiveSpatialUpsampling.shader", ReloadAttribute.Package.Root)]
			public Shader easuPS;

			// Token: 0x04000038 RID: 56
			[Reload("Shaders/PostProcessing/UberPost.shader", ReloadAttribute.Package.Root)]
			public Shader uberPostPS;

			// Token: 0x04000039 RID: 57
			[Reload("Shaders/PostProcessing/FinalPost.shader", ReloadAttribute.Package.Root)]
			public Shader finalPostPassPS;
		}

		// Token: 0x02000012 RID: 18
		[ReloadGroup]
		[Serializable]
		public sealed class TextureResources
		{
			// Token: 0x0400003A RID: 58
			[Reload("Textures/BlueNoise16/L/LDR_LLL1_{0}.png", 0, 32, ReloadAttribute.Package.Root)]
			public Texture2D[] blueNoise16LTex;

			// Token: 0x0400003B RID: 59
			[Reload(new string[] { "Textures/FilmGrain/Thin01.png", "Textures/FilmGrain/Thin02.png", "Textures/FilmGrain/Medium01.png", "Textures/FilmGrain/Medium02.png", "Textures/FilmGrain/Medium03.png", "Textures/FilmGrain/Medium04.png", "Textures/FilmGrain/Medium05.png", "Textures/FilmGrain/Medium06.png", "Textures/FilmGrain/Large01.png", "Textures/FilmGrain/Large02.png" }, ReloadAttribute.Package.Root)]
			public Texture2D[] filmGrainTex;

			// Token: 0x0400003C RID: 60
			[Reload("Textures/SMAA/AreaTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D smaaAreaTex;

			// Token: 0x0400003D RID: 61
			[Reload("Textures/SMAA/SearchTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D smaaSearchTex;
		}
	}
}
