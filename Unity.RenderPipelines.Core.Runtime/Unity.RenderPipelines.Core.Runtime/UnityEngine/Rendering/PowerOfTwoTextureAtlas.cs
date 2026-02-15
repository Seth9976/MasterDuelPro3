using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000192 RID: 402
	public class PowerOfTwoTextureAtlas : Texture2DAtlas
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00027BBF File Offset: 0x00025DBF
		public PowerOfTwoTextureAtlas(int size, int mipPadding, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, string name = "", bool useMipMap = true)
			: base(size, size, format, filterMode, true, name, useMipMap)
		{
			this.m_MipPadding = mipPadding;
			int num = size & (size - 1);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00027BE9 File Offset: 0x00025DE9
		public int mipPadding
		{
			get
			{
				return this.m_MipPadding;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00027BF1 File Offset: 0x00025DF1
		private int GetTexturePadding()
		{
			return (int)Mathf.Pow(2f, (float)this.m_MipPadding) * 2;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00027C08 File Offset: 0x00025E08
		public Vector4 GetPayloadScaleOffset(Texture texture, in Vector4 scaleOffset)
		{
			int pixelPadding = this.GetTexturePadding();
			Vector2 paddingSize = Vector2.one * (float)pixelPadding;
			Vector2 textureSize = this.GetPowerOfTwoTextureSize(texture);
			return PowerOfTwoTextureAtlas.GetPayloadScaleOffset(in textureSize, in paddingSize, in scaleOffset);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00027C3C File Offset: 0x00025E3C
		public static Vector4 GetPayloadScaleOffset(in Vector2 textureSize, in Vector2 paddingSize, in Vector4 scaleOffset)
		{
			Vector2 subTexScale = new Vector2(scaleOffset.x, scaleOffset.y);
			Vector2 vector = new Vector2(scaleOffset.z, scaleOffset.w);
			Vector2 scalePadding = (textureSize + paddingSize) / textureSize;
			Vector2 offsetPadding = paddingSize / 2f / (textureSize + paddingSize);
			Vector2 insetScale = subTexScale / scalePadding;
			Vector2 insetOffset = vector + subTexScale * offsetPadding;
			return new Vector4(insetScale.x, insetScale.y, insetOffset.x, insetOffset.y);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00027CE8 File Offset: 0x00025EE8
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, PowerOfTwoTextureAtlas.BlitType blitType)
		{
			int mipCount = base.GetTextureMipmapCount(texture.width, texture.height);
			int pixelPadding = this.GetTexturePadding();
			Vector2 textureSize = this.GetPowerOfTwoTextureSize(texture);
			bool bilinear = texture.filterMode > FilterMode.Point;
			if (!blitMips)
			{
				mipCount = 1;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<CoreProfileId>(CoreProfileId.BlitTextureInPotAtlas)))
			{
				for (int mipLevel = 0; mipLevel < mipCount; mipLevel++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, mipLevel);
					switch (blitType)
					{
					case PowerOfTwoTextureAtlas.BlitType.Padding:
						Blitter.BlitQuadWithPadding(cmd, texture, textureSize, sourceScaleOffset, scaleOffset, mipLevel, bilinear, pixelPadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.PaddingMultiply:
						Blitter.BlitQuadWithPaddingMultiply(cmd, texture, textureSize, sourceScaleOffset, scaleOffset, mipLevel, bilinear, pixelPadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPadding:
						Blitter.BlitOctahedralWithPadding(cmd, texture, textureSize, sourceScaleOffset, scaleOffset, mipLevel, bilinear, pixelPadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply:
						Blitter.BlitOctahedralWithPaddingMultiply(cmd, texture, textureSize, sourceScaleOffset, scaleOffset, mipLevel, bilinear, pixelPadding);
						break;
					}
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00027DD4 File Offset: 0x00025FD4
		public override void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.Padding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00027E04 File Offset: 0x00026004
		public void BlitTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.PaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00027E34 File Offset: 0x00026034
		public override void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPadding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00027E64 File Offset: 0x00026064
		public void BlitOctahedralTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00027E94 File Offset: 0x00026094
		private void TextureSizeToPowerOfTwo(Texture texture, ref int width, ref int height)
		{
			width = Mathf.NextPowerOfTwo(width);
			height = Mathf.NextPowerOfTwo(height);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00027EA8 File Offset: 0x000260A8
		private Vector2 GetPowerOfTwoTextureSize(Texture texture)
		{
			int width = texture.width;
			int height = texture.height;
			this.TextureSizeToPowerOfTwo(texture, ref width, ref height);
			return new Vector2((float)width, (float)height);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00027ED8 File Offset: 0x000260D8
		public override bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			if (height != width)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Can't place ",
					(texture != null) ? texture.ToString() : null,
					" in the atlas ",
					this.m_AtlasTexture.name,
					": Only squared texture are allowed in this atlas."
				}));
				return false;
			}
			this.TextureSizeToPowerOfTwo(texture, ref height, ref width);
			return base.AllocateTexture(cmd, ref scaleOffset, texture, width, height, -1);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00027F4C File Offset: 0x0002614C
		public void ResetRequestedTexture()
		{
			this.m_RequestedTextures.Clear();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00027F59 File Offset: 0x00026159
		public bool ReserveSpace(Texture texture)
		{
			return this.ReserveSpace(texture, texture.width, texture.height);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00027F6E File Offset: 0x0002616E
		public bool ReserveSpace(Texture texture, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(texture), width, height);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00027F7F File Offset: 0x0002617F
		public bool ReserveSpace(Texture textureA, Texture textureB, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(textureA, textureB), width, height);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00027F94 File Offset: 0x00026194
		public bool ReserveSpace(int id, int width, int height)
		{
			this.m_RequestedTextures[id] = new Vector2Int(width, height);
			Vector2Int cachedSize = base.GetCachedTextureSize(id);
			Vector4 vector;
			if (!base.IsCached(out vector, id) || cachedSize.x != width || cachedSize.y != height)
			{
				Vector4 scaleBias = Vector4.zero;
				if (!this.AllocateTextureWithoutBlit(id, width, height, ref scaleBias))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00027FF4 File Offset: 0x000261F4
		public bool RelayoutEntries()
		{
			List<ValueTuple<int, Vector2Int>> entries = new List<ValueTuple<int, Vector2Int>>();
			foreach (KeyValuePair<int, Vector2Int> entry in this.m_RequestedTextures)
			{
				entries.Add(new ValueTuple<int, Vector2Int>(entry.Key, entry.Value));
			}
			base.ResetAllocator();
			entries.Sort(([TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c1, [TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c2) => c2.Item2.magnitude.CompareTo(c1.Item2.magnitude));
			bool success = true;
			Vector4 newScaleOffset = Vector4.zero;
			foreach (ValueTuple<int, Vector2Int> e in entries)
			{
				bool flag = success;
				int item = e.Item1;
				Vector2Int vector2Int = e.Item2;
				int x = vector2Int.x;
				vector2Int = e.Item2;
				success = flag & this.AllocateTextureWithoutBlit(item, x, vector2Int.y, ref newScaleOffset);
			}
			return success;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00028100 File Offset: 0x00026300
		public static long GetApproxCacheSizeInByte(int nbElement, int resolution, bool hasMipmap, GraphicsFormat format)
		{
			return (long)((double)(nbElement * resolution * resolution) * (double)((hasMipmap ? 1.33f : 1f) * GraphicsFormatUtility.GetBlockSize(format)));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00028124 File Offset: 0x00026324
		public static int GetMaxCacheSizeForWeightInByte(int weight, bool hasMipmap, GraphicsFormat format)
		{
			float bytePerPixel = GraphicsFormatUtility.GetBlockSize(format) * (hasMipmap ? 1.33f : 1f);
			return CoreUtils.PreviousPowerOfTwo((int)Mathf.Sqrt((float)weight / bytePerPixel));
		}

		// Token: 0x040007AC RID: 1964
		private readonly int m_MipPadding;

		// Token: 0x040007AD RID: 1965
		private const float k_MipmapFactorApprox = 1.33f;

		// Token: 0x040007AE RID: 1966
		private Dictionary<int, Vector2Int> m_RequestedTextures = new Dictionary<int, Vector2Int>();

		// Token: 0x02000193 RID: 403
		private enum BlitType
		{
			// Token: 0x040007B0 RID: 1968
			Padding,
			// Token: 0x040007B1 RID: 1969
			PaddingMultiply,
			// Token: 0x040007B2 RID: 1970
			OctahedralPadding,
			// Token: 0x040007B3 RID: 1971
			OctahedralPaddingMultiply
		}
	}
}
