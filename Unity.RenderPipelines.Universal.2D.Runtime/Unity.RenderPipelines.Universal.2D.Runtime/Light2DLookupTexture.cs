using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000042 RID: 66
	internal static class Light2DLookupTexture
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000E5D6 File Offset: 0x0000C7D6
		public static RTHandle GetLightLookupTexture_Rendergraph()
		{
			if (Light2DLookupTexture.s_PointLightLookupTexture == null || Light2DLookupTexture.m_LightLookupRTHandle == null)
			{
				Texture lightLookupTexture = Light2DLookupTexture.GetLightLookupTexture();
				RTHandle lightLookupRTHandle = Light2DLookupTexture.m_LightLookupRTHandle;
				if (lightLookupRTHandle != null)
				{
					lightLookupRTHandle.Release();
				}
				Light2DLookupTexture.m_LightLookupRTHandle = RTHandles.Alloc(lightLookupTexture);
			}
			return Light2DLookupTexture.m_LightLookupRTHandle;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000E610 File Offset: 0x0000C810
		public static RTHandle GetFallOffLookupTexture_Rendergraph()
		{
			if (Light2DLookupTexture.s_FalloffLookupTexture == null || Light2DLookupTexture.m_FalloffRTHandle == null)
			{
				Texture falloffLookupTexture = Light2DLookupTexture.GetFalloffLookupTexture();
				RTHandle falloffRTHandle = Light2DLookupTexture.m_FalloffRTHandle;
				if (falloffRTHandle != null)
				{
					falloffRTHandle.Release();
				}
				Light2DLookupTexture.m_FalloffRTHandle = RTHandles.Alloc(falloffLookupTexture);
			}
			return Light2DLookupTexture.m_FalloffRTHandle;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000E64A File Offset: 0x0000C84A
		public static void Release()
		{
			RTHandle falloffRTHandle = Light2DLookupTexture.m_FalloffRTHandle;
			if (falloffRTHandle != null)
			{
				falloffRTHandle.Release();
			}
			RTHandle lightLookupRTHandle = Light2DLookupTexture.m_LightLookupRTHandle;
			if (lightLookupRTHandle != null)
			{
				lightLookupRTHandle.Release();
			}
			Light2DLookupTexture.m_FalloffRTHandle = null;
			Light2DLookupTexture.m_LightLookupRTHandle = null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000E678 File Offset: 0x0000C878
		public static Texture GetLightLookupTexture()
		{
			if (Light2DLookupTexture.s_PointLightLookupTexture == null)
			{
				Light2DLookupTexture.s_PointLightLookupTexture = Light2DLookupTexture.CreatePointLightLookupTexture();
			}
			return Light2DLookupTexture.s_PointLightLookupTexture;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000E696 File Offset: 0x0000C896
		public static Texture GetFalloffLookupTexture()
		{
			if (Light2DLookupTexture.s_FalloffLookupTexture == null)
			{
				Light2DLookupTexture.s_FalloffLookupTexture = Light2DLookupTexture.CreateFalloffLookupTexture();
			}
			return Light2DLookupTexture.s_FalloffLookupTexture;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		private static Texture2D CreatePointLightLookupTexture()
		{
			GraphicsFormat textureFormat = GraphicsFormat.R8G8B8A8_UNorm;
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.SetPixels))
			{
				textureFormat = GraphicsFormat.R16G16B16A16_SFloat;
			}
			else if (SystemInfo.IsFormatSupported(GraphicsFormat.R32G32B32A32_SFloat, GraphicsFormatUsage.SetPixels))
			{
				textureFormat = GraphicsFormat.R32G32B32A32_SFloat;
			}
			Texture2D texture = new Texture2D(256, 256, textureFormat, TextureCreationFlags.None);
			texture.name = Light2DLookupTexture.k_LightLookupProperty;
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;
			Vector2 center = new Vector2(128f, 128f);
			for (int y = 0; y < 256; y++)
			{
				for (int x = 0; x < 256; x++)
				{
					Vector2 pos = new Vector2((float)x, (float)y);
					float distance = Vector2.Distance(pos, center);
					Vector2 relPos = pos - center;
					Vector2 direction = center - pos;
					direction.Normalize();
					float red;
					if (x == 255 || y == 255)
					{
						red = 0f;
					}
					else
					{
						red = Mathf.Clamp(1f - 2f * distance / 256f, 0f, 1f);
					}
					float angle = Mathf.Acos(Vector2.Dot(Vector2.down, relPos.normalized)) / 3.1415927f;
					float green = Mathf.Clamp(1f - angle, 0f, 1f);
					float blue = direction.x;
					float alpha = direction.y;
					Color color = new Color(red, green, blue, alpha);
					texture.SetPixel(x, y, color);
				}
			}
			texture.Apply();
			return texture;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000E830 File Offset: 0x0000CA30
		private static Texture2D CreateFalloffLookupTexture()
		{
			Texture2D texture = new Texture2D(2048, 128, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
			texture.name = Light2DLookupTexture.k_FalloffLookupProperty;
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;
			for (int y = 0; y < 192; y++)
			{
				float baseValue = (float)(y + 32) / 256f;
				float exponent = Mathf.Log(-baseValue + 1f) / Mathf.Log(baseValue);
				for (int x = 0; x < 2048; x++)
				{
					float red = Mathf.Pow((float)x / 2048f, exponent);
					Color color = new Color(red, 0f, 0f, 1f);
					if (y >= 32 && y < 160)
					{
						texture.SetPixel(x, y - 32, color);
					}
				}
			}
			texture.Apply();
			return texture;
		}

		// Token: 0x0400014C RID: 332
		internal static readonly string k_LightLookupProperty = "_LightLookup";

		// Token: 0x0400014D RID: 333
		internal static readonly string k_FalloffLookupProperty = "_FalloffLookup";

		// Token: 0x0400014E RID: 334
		internal static readonly int k_LightLookupID = Shader.PropertyToID(Light2DLookupTexture.k_LightLookupProperty);

		// Token: 0x0400014F RID: 335
		internal static readonly int k_FalloffLookupID = Shader.PropertyToID(Light2DLookupTexture.k_FalloffLookupProperty);

		// Token: 0x04000150 RID: 336
		private static Texture2D s_PointLightLookupTexture;

		// Token: 0x04000151 RID: 337
		private static Texture2D s_FalloffLookupTexture;

		// Token: 0x04000152 RID: 338
		private static RTHandle m_LightLookupRTHandle = null;

		// Token: 0x04000153 RID: 339
		private static RTHandle m_FalloffRTHandle = null;
	}
}
