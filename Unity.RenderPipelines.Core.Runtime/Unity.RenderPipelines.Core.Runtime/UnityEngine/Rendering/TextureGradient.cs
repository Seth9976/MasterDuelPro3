using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class TextureGradient : IDisposable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0003265C File Offset: 0x0003085C
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00032664 File Offset: 0x00030864
		public int textureSize { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0003266D File Offset: 0x0003086D
		[HideInInspector]
		public GradientColorKey[] colorKeys
		{
			get
			{
				Gradient gradient = this.m_Gradient;
				if (gradient == null)
				{
					return null;
				}
				return gradient.colorKeys;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00032680 File Offset: 0x00030880
		[HideInInspector]
		public GradientAlphaKey[] alphaKeys
		{
			get
			{
				Gradient gradient = this.m_Gradient;
				if (gradient == null)
				{
					return null;
				}
				return gradient.alphaKeys;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00032694 File Offset: 0x00030894
		public TextureGradient(Gradient baseCurve)
			: this(baseCurve.colorKeys, baseCurve.alphaKeys, GradientMode.PerceptualBlend, ColorSpace.Uninitialized, -1, false)
		{
			this.mode = baseCurve.mode;
			this.colorSpace = baseCurve.colorSpace;
			this.m_Gradient.mode = baseCurve.mode;
			this.m_Gradient.colorSpace = baseCurve.colorSpace;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000326F1 File Offset: 0x000308F1
		public TextureGradient(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys, GradientMode mode = GradientMode.PerceptualBlend, ColorSpace colorSpace = ColorSpace.Uninitialized, int requestedTextureSize = -1, bool precise = false)
		{
			this.Rebuild(colorKeys, alphaKeys, mode, colorSpace, requestedTextureSize, precise);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00032720 File Offset: 0x00030920
		private void Rebuild(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys, GradientMode mode, ColorSpace colorSpace, int requestedTextureSize, bool precise)
		{
			this.m_Gradient = new Gradient();
			this.m_Gradient.mode = mode;
			this.m_Gradient.colorSpace = colorSpace;
			this.m_Gradient.SetKeys(colorKeys, alphaKeys);
			this.m_Precise = precise;
			this.m_RequestedTextureSize = requestedTextureSize;
			if (requestedTextureSize > 0)
			{
				this.textureSize = requestedTextureSize;
			}
			else
			{
				float smallestDelta = 1f;
				float[] times = new float[colorKeys.Length + alphaKeys.Length];
				for (int i = 0; i < colorKeys.Length; i++)
				{
					times[i] = colorKeys[i].time;
				}
				for (int j = 0; j < alphaKeys.Length; j++)
				{
					times[colorKeys.Length + j] = alphaKeys[j].time;
				}
				Array.Sort<float>(times);
				for (int k = 1; k < times.Length; k++)
				{
					int k2 = Math.Max(k - 1, 0);
					int k3 = Math.Min(k, times.Length - 1);
					float delta = Mathf.Abs(times[k2] - times[k3]);
					if (delta > 0f && delta < smallestDelta)
					{
						smallestDelta = delta;
					}
				}
				float scale;
				if (precise || mode == GradientMode.Fixed)
				{
					scale = 4f;
				}
				else
				{
					scale = 2f;
				}
				float sizef = scale * Mathf.Ceil(1f / smallestDelta + 1f);
				this.textureSize = Mathf.RoundToInt(sizef);
				this.textureSize = Math.Min(this.textureSize, 1024);
			}
			this.SetDirty();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00005704 File Offset: 0x00003904
		public void Dispose()
		{
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00032883 File Offset: 0x00030A83
		public void Release()
		{
			if (this.m_Texture != null)
			{
				CoreUtils.Destroy(this.m_Texture);
			}
			this.m_Texture = null;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000328A5 File Offset: 0x00030AA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			this.m_IsTextureDirty = true;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002B3E8 File Offset: 0x000295E8
		private static GraphicsFormat GetTextureFormat()
		{
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000328B0 File Offset: 0x00030AB0
		public Texture2D GetTexture()
		{
			float step = 1f / (float)(this.textureSize - 1);
			if (this.m_Texture != null && this.m_Texture.width != this.textureSize)
			{
				Object.DestroyImmediate(this.m_Texture);
				this.m_Texture = null;
			}
			if (this.m_Texture == null)
			{
				this.m_Texture = new Texture2D(this.textureSize, 1, TextureGradient.GetTextureFormat(), TextureCreationFlags.None);
				this.m_Texture.name = "GradientTexture";
				this.m_Texture.hideFlags = HideFlags.HideAndDontSave;
				this.m_Texture.filterMode = FilterMode.Bilinear;
				this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				this.m_Texture.anisoLevel = 0;
				this.m_IsTextureDirty = true;
			}
			if (this.m_IsTextureDirty)
			{
				Color[] pixels = new Color[this.textureSize];
				for (int i = 0; i < this.textureSize; i++)
				{
					pixels[i] = this.Evaluate((float)i * step);
				}
				this.m_Texture.SetPixels(pixels);
				this.m_Texture.Apply(false, false);
				this.m_IsTextureDirty = false;
				this.m_Texture.IncrementUpdateCount();
			}
			return this.m_Texture;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000329D5 File Offset: 0x00030BD5
		public Color Evaluate(float time)
		{
			if (this.textureSize <= 0)
			{
				return Color.black;
			}
			return this.m_Gradient.Evaluate(time);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000329F2 File Offset: 0x00030BF2
		public void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys, GradientMode mode, ColorSpace colorSpace)
		{
			this.m_Gradient.SetKeys(colorKeys, alphaKeys);
			this.m_Gradient.mode = mode;
			this.m_Gradient.colorSpace = colorSpace;
			this.Rebuild(colorKeys, alphaKeys, mode, colorSpace, this.m_RequestedTextureSize, this.m_Precise);
		}

		// Token: 0x0400092B RID: 2347
		[SerializeField]
		private Gradient m_Gradient;

		// Token: 0x0400092C RID: 2348
		private Texture2D m_Texture;

		// Token: 0x0400092D RID: 2349
		private int m_RequestedTextureSize = -1;

		// Token: 0x0400092E RID: 2350
		private bool m_IsTextureDirty;

		// Token: 0x0400092F RID: 2351
		private bool m_Precise;

		// Token: 0x04000930 RID: 2352
		[SerializeField]
		[HideInInspector]
		public GradientMode mode = GradientMode.PerceptualBlend;

		// Token: 0x04000931 RID: 2353
		[SerializeField]
		[HideInInspector]
		public ColorSpace colorSpace = ColorSpace.Uninitialized;
	}
}
