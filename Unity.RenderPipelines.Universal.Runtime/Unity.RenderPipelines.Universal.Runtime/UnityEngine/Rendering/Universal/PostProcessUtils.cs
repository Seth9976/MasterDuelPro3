using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000152 RID: 338
	public static class PostProcessUtils
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x000236A8 File Offset: 0x000218A8
		[Obsolete("This method is obsolete. Use ConfigureDithering override that takes camera pixel width and height instead.")]
		public static int ConfigureDithering(PostProcessData data, int index, Camera camera, Material material)
		{
			return PostProcessUtils.ConfigureDithering(data, index, camera.pixelWidth, camera.pixelHeight, material);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000236C0 File Offset: 0x000218C0
		public static int ConfigureDithering(PostProcessData data, int index, int cameraPixelWidth, int cameraPixelHeight, Material material)
		{
			Texture2D[] blueNoise = data.textures.blueNoise16LTex;
			if (blueNoise == null || blueNoise.Length == 0)
			{
				return 0;
			}
			if (++index >= blueNoise.Length)
			{
				index = 0;
			}
			Random.State state = Random.state;
			Random.InitState(Time.frameCount);
			float rndOffsetX = Random.value;
			float rndOffsetY = Random.value;
			Random.state = state;
			Texture2D noiseTex = blueNoise[index];
			material.SetTexture(PostProcessUtils.ShaderConstants._BlueNoise_Texture, noiseTex);
			material.SetVector(PostProcessUtils.ShaderConstants._Dithering_Params, new Vector4((float)cameraPixelWidth / (float)noiseTex.width, (float)cameraPixelHeight / (float)noiseTex.height, rndOffsetX, rndOffsetY));
			return index;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00023749 File Offset: 0x00021949
		[Obsolete("This method is obsolete. Use ConfigureFilmGrain override that takes camera pixel width and height instead.")]
		public static void ConfigureFilmGrain(PostProcessData data, FilmGrain settings, Camera camera, Material material)
		{
			PostProcessUtils.ConfigureFilmGrain(data, settings, camera.pixelWidth, camera.pixelHeight, material);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00023760 File Offset: 0x00021960
		public static void ConfigureFilmGrain(PostProcessData data, FilmGrain settings, int cameraPixelWidth, int cameraPixelHeight, Material material)
		{
			Texture texture = settings.texture.value;
			if (settings.type.value != FilmGrainLookup.Custom)
			{
				texture = data.textures.filmGrainTex[(int)settings.type.value];
			}
			Random.State state = Random.state;
			Random.InitState(Time.frameCount);
			float rndOffsetX = Random.value;
			float rndOffsetY = Random.value;
			Random.state = state;
			Vector4 tilingParams = ((texture == null) ? Vector4.zero : new Vector4((float)cameraPixelWidth / (float)texture.width, (float)cameraPixelHeight / (float)texture.height, rndOffsetX, rndOffsetY));
			material.SetTexture(PostProcessUtils.ShaderConstants._Grain_Texture, texture);
			material.SetVector(PostProcessUtils.ShaderConstants._Grain_Params, new Vector2(settings.intensity.value * 4f, settings.response.value));
			material.SetVector(PostProcessUtils.ShaderConstants._Grain_TilingParams, tilingParams);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00023838 File Offset: 0x00021A38
		internal static void SetSourceSize(RasterCommandBuffer cmd, RTHandle source)
		{
			float width = (float)source.rt.width;
			float height = (float)source.rt.height;
			if (source.rt.useDynamicScale)
			{
				width *= ScalableBufferManager.widthScaleFactor;
				height *= ScalableBufferManager.heightScaleFactor;
			}
			cmd.SetGlobalVector(PostProcessUtils.ShaderConstants._SourceSize, new Vector4(width, height, 1f / width, 1f / height));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002389C File Offset: 0x00021A9C
		internal static void SetSourceSize(CommandBuffer cmd, RTHandle source)
		{
			PostProcessUtils.SetSourceSize(CommandBufferHelpers.GetRasterCommandBuffer(cmd), source);
		}

		// Token: 0x02000153 RID: 339
		private static class ShaderConstants
		{
			// Token: 0x040007CF RID: 1999
			public static readonly int _Grain_Texture = Shader.PropertyToID("_Grain_Texture");

			// Token: 0x040007D0 RID: 2000
			public static readonly int _Grain_Params = Shader.PropertyToID("_Grain_Params");

			// Token: 0x040007D1 RID: 2001
			public static readonly int _Grain_TilingParams = Shader.PropertyToID("_Grain_TilingParams");

			// Token: 0x040007D2 RID: 2002
			public static readonly int _BlueNoise_Texture = Shader.PropertyToID("_BlueNoise_Texture");

			// Token: 0x040007D3 RID: 2003
			public static readonly int _Dithering_Params = Shader.PropertyToID("_Dithering_Params");

			// Token: 0x040007D4 RID: 2004
			public static readonly int _SourceSize = Shader.PropertyToID("_SourceSize");
		}
	}
}
