using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003FD RID: 1021
	[NativeHeader("Runtime/Graphics/Format.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/TextureFormat.h")]
	public class GraphicsFormatUtility
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x0003C918 File Offset: 0x0003AB18
		[FreeFunction("GetGraphicsFormat_Native_Texture")]
		internal static GraphicsFormat GetFormat([NotNull] Texture texture)
		{
			if (texture == null)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(texture);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			return GraphicsFormatUtility.GetFormat_Injected(intPtr);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0003C950 File Offset: 0x0003AB50
		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		// Token: 0x06001B58 RID: 7000
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		// Token: 0x06001B59 RID: 7001 RVA: 0x0003C96C File Offset: 0x0003AB6C
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		// Token: 0x06001B5A RID: 7002
		[FreeFunction(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		// Token: 0x06001B5B RID: 7003 RVA: 0x0003C988 File Offset: 0x0003AB88
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			bool defaultSRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool sRGB = ((readWrite == RenderTextureReadWrite.Default) ? defaultSRGB : (readWrite == RenderTextureReadWrite.sRGB));
			return GraphicsFormatUtility.GetGraphicsFormat(format, sRGB);
		}

		// Token: 0x06001B5C RID: 7004
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetDepthStencilFormatFromBitsLegacy_Native(int minimumDepthBits);

		// Token: 0x06001B5D RID: 7005 RVA: 0x0003C9B8 File Offset: 0x0003ABB8
		public static GraphicsFormat GetDepthStencilFormat(int depthBits)
		{
			return GraphicsFormatUtility.GetDepthStencilFormatFromBitsLegacy_Native(depthBits);
		}

		// Token: 0x06001B5E RID: 7006
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDepthBits(GraphicsFormat format);

		// Token: 0x06001B5F RID: 7007 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		public static GraphicsFormat GetDepthStencilFormat(int minimumDepthBits, int minimumStencilBits)
		{
			bool flag = minimumDepthBits == 0 && minimumStencilBits == 0;
			GraphicsFormat graphicsFormat;
			if (flag)
			{
				graphicsFormat = GraphicsFormat.None;
			}
			else
			{
				bool flag2 = minimumDepthBits < 0 || minimumStencilBits < 0;
				if (flag2)
				{
					throw new ArgumentException("Number of bits in DepthStencil format can't be negative.");
				}
				bool flag3 = minimumDepthBits > 32;
				if (flag3)
				{
					throw new ArgumentException("Number of depth buffer bits cannot exceed 32.");
				}
				bool flag4 = minimumStencilBits > 8;
				if (flag4)
				{
					throw new ArgumentException("Number of stencil buffer bits cannot exceed 8.");
				}
				bool flag5 = minimumDepthBits == 0;
				if (flag5)
				{
					minimumDepthBits = 0;
				}
				else
				{
					bool flag6 = minimumDepthBits <= 16;
					if (flag6)
					{
						minimumDepthBits = 16;
					}
					else
					{
						bool flag7 = minimumDepthBits <= 24;
						if (flag7)
						{
							minimumDepthBits = 24;
						}
						else
						{
							minimumDepthBits = 32;
						}
					}
				}
				bool flag8 = minimumStencilBits != 0;
				if (flag8)
				{
					minimumStencilBits = 8;
				}
				Debug.Assert(GraphicsFormatUtility.tableNoStencil.Length == GraphicsFormatUtility.tableStencil.Length);
				GraphicsFormat[] table = ((minimumStencilBits > 0) ? GraphicsFormatUtility.tableStencil : GraphicsFormatUtility.tableNoStencil);
				int formatIndex = minimumDepthBits / 8;
				for (int i = formatIndex; i < table.Length; i++)
				{
					GraphicsFormat format = table[i];
					bool flag9 = SystemInfo.IsFormatSupported(format, GraphicsFormatUsage.Render);
					if (flag9)
					{
						return format;
					}
				}
				graphicsFormat = GraphicsFormat.None;
			}
			return graphicsFormat;
		}

		// Token: 0x06001B60 RID: 7008
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		// Token: 0x06001B61 RID: 7009
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetSRGBFormat(GraphicsFormat format);

		// Token: 0x06001B62 RID: 7010
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetLinearFormat(GraphicsFormat format);

		// Token: 0x06001B63 RID: 7011
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		// Token: 0x06001B64 RID: 7012
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		// Token: 0x06001B65 RID: 7013
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetComponentCount(GraphicsFormat format);

		// Token: 0x06001B66 RID: 7014 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		[FreeFunction(IsThreadSafe = true)]
		public static string GetFormatString(GraphicsFormat format)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GraphicsFormatUtility.GetFormatString_Injected(format, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001B67 RID: 7015
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsCompressedFormat_Native_TextureFormat(TextureFormat format);

		// Token: 0x06001B68 RID: 7016 RVA: 0x0003CB20 File Offset: 0x0003AD20
		public static bool IsCompressedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsCompressedFormat_Native_TextureFormat(format);
		}

		// Token: 0x06001B69 RID: 7017
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanDecompressFormat(GraphicsFormat format, bool wholeImage);

		// Token: 0x06001B6A RID: 7018 RVA: 0x0003CB38 File Offset: 0x0003AD38
		internal static bool CanDecompressFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.CanDecompressFormat(format, true);
		}

		// Token: 0x06001B6B RID: 7019
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		// Token: 0x06001B6C RID: 7020
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		// Token: 0x06001B6D RID: 7021
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		// Token: 0x06001B6E RID: 7022
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		// Token: 0x06001B6F RID: 7023
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDepthStencilFormat(GraphicsFormat format);

		// Token: 0x06001B70 RID: 7024
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		// Token: 0x06001B71 RID: 7025
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		// Token: 0x06001B72 RID: 7026
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		// Token: 0x06001B73 RID: 7027
		[FreeFunction("IsCompressedCrunchTextureFormat", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsCrunchFormat(TextureFormat format);

		// Token: 0x06001B74 RID: 7028
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleR(GraphicsFormat format);

		// Token: 0x06001B75 RID: 7029
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleG(GraphicsFormat format);

		// Token: 0x06001B76 RID: 7030
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleB(GraphicsFormat format);

		// Token: 0x06001B77 RID: 7031
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleA(GraphicsFormat format);

		// Token: 0x06001B78 RID: 7032
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockSize(GraphicsFormat format);

		// Token: 0x06001B7A RID: 7034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetFormat_Injected(IntPtr texture);

		// Token: 0x06001B7B RID: 7035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFormatString_Injected(GraphicsFormat format, out ManagedSpanWrapper ret);

		// Token: 0x04000E47 RID: 3655
		private static readonly GraphicsFormat[] tableNoStencil = new GraphicsFormat[]
		{
			GraphicsFormat.None,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D24_UNorm,
			GraphicsFormat.D32_SFloat
		};

		// Token: 0x04000E48 RID: 3656
		private static readonly GraphicsFormat[] tableStencil = new GraphicsFormat[]
		{
			GraphicsFormat.S8_UInt,
			GraphicsFormat.D16_UNorm_S8_UInt,
			GraphicsFormat.D16_UNorm_S8_UInt,
			GraphicsFormat.D24_UNorm_S8_UInt,
			GraphicsFormat.D32_SFloat_S8_UInt
		};
	}
}
