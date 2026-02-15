using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A0 RID: 416
	public class Texture2DAtlas
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002A137 File Offset: 0x00028337
		public static int maxMipLevelPadding
		{
			get
			{
				return Texture2DAtlas.s_MaxMipLevelPadding;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002A13E File Offset: 0x0002833E
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002A148 File Offset: 0x00028348
		public Texture2DAtlas(int width, int height, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, bool powerOfTwoPadding = false, string name = "", bool useMipMap = true)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_UseMipMaps = useMipMap;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, this.m_Format, 1, filterMode, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, useMipMap, false, false, 1, 0f, MSAASamples.None, false, false, false, RenderTextureMemoryless.None, VRTextureUsage.None, name);
			this.m_IsAtlasTextureOwner = true;
			int mipCount = (useMipMap ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int mipIdx = 0; mipIdx < mipCount; mipIdx++)
			{
				Graphics.SetRenderTarget(this.m_AtlasTexture, mipIdx);
				GL.Clear(false, true, Color.clear);
			}
			this.m_AtlasAllocator = new AtlasAllocator(width, height, powerOfTwoPadding);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002A227 File Offset: 0x00028427
		public void Release()
		{
			this.ResetAllocator();
			if (this.m_IsAtlasTextureOwner)
			{
				RTHandles.Release(this.m_AtlasTexture);
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002A242 File Offset: 0x00028442
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Reset();
			this.m_AllocationCache.Clear();
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002A268 File Offset: 0x00028468
		public void ClearTarget(CommandBuffer cmd)
		{
			int mipCount = (this.m_UseMipMaps ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int mipLevel = 0; mipLevel < mipCount; mipLevel++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, mipLevel);
				Blitter.BlitQuad(cmd, Texture2D.blackTexture, Texture2DAtlas.fullScaleOffset, Texture2DAtlas.fullScaleOffset, mipLevel, true);
			}
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002A2D3 File Offset: 0x000284D3
		private protected int GetTextureMipmapCount(int width, int height)
		{
			if (!this.m_UseMipMaps)
			{
				return 1;
			}
			return CoreUtils.GetMipCount((float)Mathf.Max(width, height));
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002A2EC File Offset: 0x000284EC
		private protected bool Is2D(Texture texture)
		{
			RenderTexture rt = texture as RenderTexture;
			return texture is Texture2D || (rt != null && rt.dimension == TextureDimension.Tex2D);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002A318 File Offset: 0x00028518
		private protected bool IsSingleChannelBlit(Texture source, Texture destination)
		{
			uint srcCount = GraphicsFormatUtility.GetComponentCount(source.graphicsFormat);
			uint dstCount = GraphicsFormatUtility.GetComponentCount(destination.graphicsFormat);
			if (srcCount == 1U || dstCount == 1U)
			{
				if (srcCount != dstCount)
				{
					return true;
				}
				int num = (1 << (int)(GraphicsFormatUtility.GetSwizzleA(source.graphicsFormat) & (FormatSwizzle)7) << 24) | (1 << (int)(GraphicsFormatUtility.GetSwizzleB(source.graphicsFormat) & (FormatSwizzle)7) << 16) | (1 << (int)(GraphicsFormatUtility.GetSwizzleG(source.graphicsFormat) & (FormatSwizzle)7) << 8) | (1 << (int)(GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) & (FormatSwizzle)7));
				int dstSwizzle = (1 << (int)(GraphicsFormatUtility.GetSwizzleA(destination.graphicsFormat) & (FormatSwizzle)7) << 24) | (1 << (int)(GraphicsFormatUtility.GetSwizzleB(destination.graphicsFormat) & (FormatSwizzle)7) << 16) | (1 << (int)(GraphicsFormatUtility.GetSwizzleG(destination.graphicsFormat) & (FormatSwizzle)7) << 8) | (1 << (int)(GraphicsFormatUtility.GetSwizzleR(destination.graphicsFormat) & (FormatSwizzle)7));
				if (num != dstSwizzle)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002A3FC File Offset: 0x000285FC
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, Texture2DAtlas.BlitType blitType)
		{
			int mipCount = this.GetTextureMipmapCount(texture.width, texture.height);
			if (!blitMips)
			{
				mipCount = 1;
			}
			for (int mipLevel = 0; mipLevel < mipCount; mipLevel++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, mipLevel);
				switch (blitType)
				{
				case Texture2DAtlas.BlitType.Default:
					Blitter.BlitQuad(cmd, texture, sourceScaleOffset, scaleOffset, mipLevel, true);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedral:
					Blitter.BlitCubeToOctahedral2DQuad(cmd, texture, scaleOffset, mipLevel);
					break;
				case Texture2DAtlas.BlitType.SingleChannel:
					Blitter.BlitQuadSingleChannel(cmd, texture, sourceScaleOffset, scaleOffset, mipLevel);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel:
					Blitter.BlitCubeToOctahedral2DQuadSingleChannel(cmd, texture, scaleOffset, mipLevel);
					break;
				}
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002A488 File Offset: 0x00028688
		private protected void MarkGPUTextureValid(int instanceId, bool mipAreValid = false)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = (mipAreValid ? 2 : 1);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002A49D File Offset: 0x0002869D
		private protected void MarkGPUTextureInvalid(int instanceId)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = 0;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002A4AC File Offset: 0x000286AC
		public virtual void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (this.Is2D(texture))
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.Default;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.SingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, blitType);
				int instanceID = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
				this.MarkGPUTextureValid(instanceID, blitMips);
				this.m_TextureHashes[instanceID] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002A514 File Offset: 0x00028714
		public virtual void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			this.BlitTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, overrideInstanceID);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002A528 File Offset: 0x00028728
		public virtual void BlitCubeTexture2D(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (texture.dimension == TextureDimension.Cube)
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedral;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, new Vector4(1f, 1f, 0f, 0f), blitMips, blitType);
				int instanceID = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
				this.MarkGPUTextureValid(instanceID, blitMips);
				this.m_TextureHashes[instanceID] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002A5A8 File Offset: 0x000287A8
		public virtual bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			int instanceID = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
			bool flag = this.AllocateTextureWithoutBlit(instanceID, width, height, ref scaleOffset);
			if (flag)
			{
				if (this.Is2D(texture))
				{
					this.BlitTexture(cmd, scaleOffset, texture, Texture2DAtlas.fullScaleOffset, true, -1);
				}
				else
				{
					this.BlitCubeTexture2D(cmd, scaleOffset, texture, true, -1);
				}
				this.MarkGPUTextureValid(instanceID, true);
				this.m_TextureHashes[instanceID] = CoreUtils.GetTextureHash(texture);
			}
			return flag;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002A61F File Offset: 0x0002881F
		public bool AllocateTextureWithoutBlit(Texture texture, int width, int height, ref Vector4 scaleOffset)
		{
			return this.AllocateTextureWithoutBlit(texture.GetInstanceID(), width, height, ref scaleOffset);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002A634 File Offset: 0x00028834
		public virtual bool AllocateTextureWithoutBlit(int instanceId, int width, int height, ref Vector4 scaleOffset)
		{
			scaleOffset = Vector4.zero;
			if (this.m_AtlasAllocator.Allocate(ref scaleOffset, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				this.m_AllocationCache[instanceId] = new ValueTuple<Vector4, Vector2Int>(scaleOffset, new Vector2Int(width, height));
				this.MarkGPUTextureInvalid(instanceId);
				this.m_TextureHashes[instanceId] = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002A6D4 File Offset: 0x000288D4
		private protected int GetTextureHash(Texture textureA, Texture textureB)
		{
			return CoreUtils.GetTextureHash(textureA) + 23 * CoreUtils.GetTextureHash(textureB);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002A6E6 File Offset: 0x000288E6
		public int GetTextureID(Texture texture)
		{
			return texture.GetInstanceID();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002A6EE File Offset: 0x000288EE
		public int GetTextureID(Texture textureA, Texture textureB)
		{
			return this.GetTextureID(textureA) + 23 * this.GetTextureID(textureB);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002A702 File Offset: 0x00028902
		public bool IsCached(out Vector4 scaleOffset, Texture textureA, Texture textureB)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(textureA, textureB));
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002A713 File Offset: 0x00028913
		public bool IsCached(out Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(texture));
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002A724 File Offset: 0x00028924
		public bool IsCached(out Vector4 scaleOffset, int id)
		{
			ValueTuple<Vector4, Vector2Int> value;
			bool flag = this.m_AllocationCache.TryGetValue(id, out value);
			scaleOffset = value.Item1;
			return flag;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002A74C File Offset: 0x0002894C
		internal Vector2Int GetCachedTextureSize(int id)
		{
			ValueTuple<Vector4, Vector2Int> value;
			this.m_AllocationCache.TryGetValue(id, out value);
			return value.Item2;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002A770 File Offset: 0x00028970
		public virtual bool NeedsUpdate(Texture texture, bool needMips = false)
		{
			RenderTexture rt = texture as RenderTexture;
			int key = this.GetTextureID(texture);
			int textureHash = CoreUtils.GetTextureHash(texture);
			if (rt != null)
			{
				int updateCount;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(key, out updateCount))
				{
					if ((ulong)rt.updateCount != (ulong)((long)updateCount))
					{
						this.m_IsGPUTextureUpToDate[key] = (int)rt.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[key] = (int)rt.updateCount;
				}
			}
			else
			{
				int hash;
				if (this.m_TextureHashes.TryGetValue(key, out hash) && hash != textureHash)
				{
					this.m_TextureHashes[key] = textureHash;
					return true;
				}
				int value;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(key, out value))
				{
					return value == 0 || (needMips && value == 1);
				}
			}
			return false;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002A828 File Offset: 0x00028A28
		public virtual bool NeedsUpdate(int id, int updateCount, bool needMips = false)
		{
			int atlasUpdateCount;
			if (this.m_IsGPUTextureUpToDate.TryGetValue(id, out atlasUpdateCount))
			{
				if (updateCount != atlasUpdateCount)
				{
					this.m_IsGPUTextureUpToDate[id] = updateCount;
					return true;
				}
			}
			else
			{
				this.m_IsGPUTextureUpToDate[id] = updateCount;
			}
			return false;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002A868 File Offset: 0x00028A68
		public virtual bool NeedsUpdate(Texture textureA, Texture textureB, bool needMips = false)
		{
			RenderTexture rtA = textureA as RenderTexture;
			RenderTexture rtB = textureB as RenderTexture;
			int key = this.GetTextureID(textureA, textureB);
			int textureHash = this.GetTextureHash(textureA, textureB);
			if (rtA != null || rtB != null)
			{
				int updateCount;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(key, out updateCount))
				{
					if (rtA != null && rtB != null && (ulong)Math.Min(rtA.updateCount, rtB.updateCount) != (ulong)((long)updateCount))
					{
						this.m_IsGPUTextureUpToDate[key] = (int)Math.Min(rtA.updateCount, rtB.updateCount);
						return true;
					}
					if (rtA != null && (ulong)rtA.updateCount != (ulong)((long)updateCount))
					{
						this.m_IsGPUTextureUpToDate[key] = (int)rtA.updateCount;
						return true;
					}
					if (rtB != null && (ulong)rtB.updateCount != (ulong)((long)updateCount))
					{
						this.m_IsGPUTextureUpToDate[key] = (int)rtB.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[key] = textureHash;
				}
			}
			else
			{
				int hash;
				if (this.m_TextureHashes.TryGetValue(key, out hash) && hash != textureHash)
				{
					this.m_TextureHashes[key] = key;
					return true;
				}
				int value;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(key, out value))
				{
					return value == 0 || (needMips && value == 1);
				}
			}
			return false;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002A9AC File Offset: 0x00028BAC
		public virtual bool AddTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, texture) || this.AllocateTexture(cmd, ref scaleOffset, texture, texture.width, texture.height, -1);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture oldTexture, Texture newTexture, ref Vector4 scaleOffset, Vector4 sourceScaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			if (this.IsCached(out scaleOffset, oldTexture))
			{
				if (updateIfNeeded && this.NeedsUpdate(newTexture, false))
				{
					if (this.Is2D(newTexture))
					{
						this.BlitTexture(cmd, scaleOffset, newTexture, sourceScaleOffset, blitMips, -1);
					}
					else
					{
						this.BlitCubeTexture2D(cmd, scaleOffset, newTexture, blitMips, -1);
					}
					this.MarkGPUTextureValid(this.GetTextureID(newTexture), blitMips);
				}
				return true;
			}
			return this.AllocateTexture(cmd, ref scaleOffset, newTexture, newTexture.width, newTexture.height, -1);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002AA4F File Offset: 0x00028C4F
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture texture, ref Vector4 scaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			return this.UpdateTexture(cmd, texture, texture, ref scaleOffset, Texture2DAtlas.fullScaleOffset, updateIfNeeded, blitMips);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002AA64 File Offset: 0x00028C64
		internal bool EnsureTextureSlot(out bool isUploadNeeded, ref Vector4 scaleBias, int key, int width, int height)
		{
			isUploadNeeded = false;
			ValueTuple<Vector4, Vector2Int> value;
			if (this.m_AllocationCache.TryGetValue(key, out value))
			{
				scaleBias = value.Item1;
				return true;
			}
			if (!this.m_AtlasAllocator.Allocate(ref scaleBias, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleBias.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
			this.m_AllocationCache.Add(key, new ValueTuple<Vector4, Vector2Int>(scaleBias, new Vector2Int(width, height)));
			return true;
		}

		// Token: 0x040007F3 RID: 2035
		private protected const int kGPUTexInvalid = 0;

		// Token: 0x040007F4 RID: 2036
		private protected const int kGPUTexValidMip0 = 1;

		// Token: 0x040007F5 RID: 2037
		private protected const int kGPUTexValidMipAll = 2;

		// Token: 0x040007F6 RID: 2038
		private protected RTHandle m_AtlasTexture;

		// Token: 0x040007F7 RID: 2039
		private protected int m_Width;

		// Token: 0x040007F8 RID: 2040
		private protected int m_Height;

		// Token: 0x040007F9 RID: 2041
		private protected GraphicsFormat m_Format;

		// Token: 0x040007FA RID: 2042
		private protected bool m_UseMipMaps;

		// Token: 0x040007FB RID: 2043
		private bool m_IsAtlasTextureOwner;

		// Token: 0x040007FC RID: 2044
		private AtlasAllocator m_AtlasAllocator;

		// Token: 0x040007FD RID: 2045
		[TupleElementNames(new string[] { "scaleOffset", "size" })]
		private Dictionary<int, ValueTuple<Vector4, Vector2Int>> m_AllocationCache = new Dictionary<int, ValueTuple<Vector4, Vector2Int>>();

		// Token: 0x040007FE RID: 2046
		private Dictionary<int, int> m_IsGPUTextureUpToDate = new Dictionary<int, int>();

		// Token: 0x040007FF RID: 2047
		private Dictionary<int, int> m_TextureHashes = new Dictionary<int, int>();

		// Token: 0x04000800 RID: 2048
		private static readonly Vector4 fullScaleOffset = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04000801 RID: 2049
		private static readonly int s_MaxMipLevelPadding = 10;

		// Token: 0x020001A1 RID: 417
		private enum BlitType
		{
			// Token: 0x04000803 RID: 2051
			Default,
			// Token: 0x04000804 RID: 2052
			CubeTo2DOctahedral,
			// Token: 0x04000805 RID: 2053
			SingleChannel,
			// Token: 0x04000806 RID: 2054
			CubeTo2DOctahedralSingleChannel
		}
	}
}
