using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002D RID: 45
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	[Serializable]
	public struct Light2DBlendStyle
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000B764 File Offset: 0x00009964
		internal Vector2 blendFactors
		{
			get
			{
				Vector2 result = default(Vector2);
				switch (this.blendMode)
				{
				case Light2DBlendStyle.BlendMode.Additive:
					result.x = 0f;
					result.y = 1f;
					break;
				case Light2DBlendStyle.BlendMode.Multiply:
					result.x = 1f;
					result.y = 0f;
					break;
				case Light2DBlendStyle.BlendMode.Subtractive:
					result.x = 0f;
					result.y = -1f;
					break;
				default:
					result.x = 1f;
					result.y = 0f;
					break;
				}
				return result;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000B7FC File Offset: 0x000099FC
		internal Light2DBlendStyle.MaskChannelFilter maskTextureChannelFilter
		{
			get
			{
				switch (this.maskTextureChannel)
				{
				case Light2DBlendStyle.TextureChannel.R:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.G:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.B:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.A:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 0f, 1f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusR:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(1f, 0f, 0f, 0f), new Vector4(1f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusG:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusB:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 1f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusA:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 0f, 1f), new Vector4(0f, 0f, 0f, 1f));
				}
				return new Light2DBlendStyle.MaskChannelFilter(Vector4.zero, Vector4.zero);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000BA0E File Offset: 0x00009C0E
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000BA16 File Offset: 0x00009C16
		internal bool isDirty { readonly get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000BA1F File Offset: 0x00009C1F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000BA27 File Offset: 0x00009C27
		internal bool hasRenderTarget { readonly get; set; }

		// Token: 0x040000EB RID: 235
		public string name;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		internal Light2DBlendStyle.TextureChannel maskTextureChannel;

		// Token: 0x040000ED RID: 237
		[SerializeField]
		internal Light2DBlendStyle.BlendMode blendMode;

		// Token: 0x040000F0 RID: 240
		internal int renderTargetHandleId;

		// Token: 0x040000F1 RID: 241
		internal RTHandle renderTargetHandle;

		// Token: 0x0200002E RID: 46
		internal enum TextureChannel
		{
			// Token: 0x040000F3 RID: 243
			None,
			// Token: 0x040000F4 RID: 244
			R,
			// Token: 0x040000F5 RID: 245
			G,
			// Token: 0x040000F6 RID: 246
			B,
			// Token: 0x040000F7 RID: 247
			A,
			// Token: 0x040000F8 RID: 248
			OneMinusR,
			// Token: 0x040000F9 RID: 249
			OneMinusG,
			// Token: 0x040000FA RID: 250
			OneMinusB,
			// Token: 0x040000FB RID: 251
			OneMinusA
		}

		// Token: 0x0200002F RID: 47
		internal struct MaskChannelFilter
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000141 RID: 321 RVA: 0x0000BA30 File Offset: 0x00009C30
			// (set) Token: 0x06000142 RID: 322 RVA: 0x0000BA38 File Offset: 0x00009C38
			public Vector4 mask { readonly get; private set; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000143 RID: 323 RVA: 0x0000BA41 File Offset: 0x00009C41
			// (set) Token: 0x06000144 RID: 324 RVA: 0x0000BA49 File Offset: 0x00009C49
			public Vector4 inverted { readonly get; private set; }

			// Token: 0x06000145 RID: 325 RVA: 0x0000BA52 File Offset: 0x00009C52
			public MaskChannelFilter(Vector4 m, Vector4 i)
			{
				this.mask = m;
				this.inverted = i;
			}
		}

		// Token: 0x02000030 RID: 48
		internal enum BlendMode
		{
			// Token: 0x040000FF RID: 255
			Additive,
			// Token: 0x04000100 RID: 256
			Multiply,
			// Token: 0x04000101 RID: 257
			Subtractive
		}

		// Token: 0x02000031 RID: 49
		[Serializable]
		internal struct BlendFactors
		{
			// Token: 0x04000102 RID: 258
			public float multiplicative;

			// Token: 0x04000103 RID: 259
			public float additive;
		}
	}
}
