using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001CE RID: 462
	public static class HDROutputUtils
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x000308F0 File Offset: 0x0002EAF0
		public static bool GetColorSpaceForGamut(ColorGamut gamut, out int colorspace)
		{
			if (ColorGamutUtility.GetWhitePoint(gamut) != WhitePoint.D65)
			{
				Debug.LogWarningFormat("{0} white point is currently unsupported for outputting to HDR.", new object[] { gamut.ToString() });
				colorspace = -1;
				return false;
			}
			switch (ColorGamutUtility.GetColorPrimaries(gamut))
			{
			case ColorPrimaries.Rec709:
				colorspace = 0;
				return true;
			case ColorPrimaries.Rec2020:
				colorspace = 1;
				return true;
			case ColorPrimaries.P3:
				colorspace = 2;
				return true;
			default:
				Debug.LogWarningFormat("{0} color space is currently unsupported for outputting to HDR.", new object[] { gamut.ToString() });
				colorspace = -1;
				return false;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00030978 File Offset: 0x0002EB78
		public static bool GetColorEncodingForGamut(ColorGamut gamut, out int encoding)
		{
			switch (ColorGamutUtility.GetTransferFunction(gamut))
			{
			case TransferFunction.sRGB:
				encoding = 0;
				return true;
			case TransferFunction.PQ:
				encoding = 2;
				return true;
			case TransferFunction.Linear:
				encoding = 3;
				return true;
			case TransferFunction.Gamma22:
				encoding = 4;
				return true;
			}
			Debug.LogWarningFormat("{0} color encoding is currently unsupported for outputting to HDR.", new object[] { gamut.ToString() });
			encoding = -1;
			return false;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public static void ConfigureHDROutput(Material material, ColorGamut gamut, HDROutputUtils.Operation operations)
		{
			int colorSpace;
			int encoding;
			if (!HDROutputUtils.GetColorSpaceForGamut(gamut, out colorSpace) || !HDROutputUtils.GetColorEncodingForGamut(gamut, out encoding))
			{
				return;
			}
			material.SetInteger(HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorSpace);
			material.SetInteger(HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(HDROutputUtils.Operation.ColorConversion));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRInput.name, operations == HDROutputUtils.Operation.None);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00030AE8 File Offset: 0x0002ECE8
		public static void ConfigureHDROutput(MaterialPropertyBlock properties, ColorGamut gamut)
		{
			int colorSpace;
			int encoding;
			if (!HDROutputUtils.GetColorSpaceForGamut(gamut, out colorSpace) || !HDROutputUtils.GetColorEncodingForGamut(gamut, out encoding))
			{
				return;
			}
			properties.SetInteger(HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorSpace);
			properties.SetInteger(HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00030B24 File Offset: 0x0002ED24
		public static void ConfigureHDROutput(Material material, HDROutputUtils.Operation operations)
		{
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(HDROutputUtils.Operation.ColorConversion));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(material, HDROutputUtils.ShaderKeywords.HDRInput.name, operations == HDROutputUtils.Operation.None);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00030BFC File Offset: 0x0002EDFC
		public static void ConfigureHDROutput(ComputeShader computeShader, ColorGamut gamut, HDROutputUtils.Operation operations)
		{
			int colorSpace;
			int encoding;
			if (!HDROutputUtils.GetColorSpaceForGamut(gamut, out colorSpace) || !HDROutputUtils.GetColorEncodingForGamut(gamut, out encoding))
			{
				return;
			}
			computeShader.SetInt(HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorSpace);
			computeShader.SetInt(HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
			CoreUtils.SetKeyword(computeShader, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(computeShader, HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(HDROutputUtils.Operation.ColorConversion));
			CoreUtils.SetKeyword(computeShader, HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(HDROutputUtils.Operation.ColorEncoding));
			CoreUtils.SetKeyword(computeShader, HDROutputUtils.ShaderKeywords.HDRInput.name, operations == HDROutputUtils.Operation.None);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00030D04 File Offset: 0x0002EF04
		public static bool IsShaderVariantValid(ShaderKeywordSet shaderKeywordSet, bool isHDREnabled)
		{
			bool hasHDRKeywords = shaderKeywordSet.IsEnabled(HDROutputUtils.ShaderKeywords.HDREncoding) || shaderKeywordSet.IsEnabled(HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion) || shaderKeywordSet.IsEnabled(HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding) || shaderKeywordSet.IsEnabled(HDROutputUtils.ShaderKeywords.HDRInput);
			return isHDREnabled || !hasHDRKeywords;
		}

		// Token: 0x020001CF RID: 463
		[Flags]
		public enum Operation
		{
			// Token: 0x040008E4 RID: 2276
			None = 0,
			// Token: 0x040008E5 RID: 2277
			ColorConversion = 1,
			// Token: 0x040008E6 RID: 2278
			ColorEncoding = 2
		}

		// Token: 0x020001D0 RID: 464
		public struct HDRDisplayInformation
		{
			// Token: 0x06000D47 RID: 3399 RVA: 0x00030D56 File Offset: 0x0002EF56
			public HDRDisplayInformation(int maxFullFrameToneMapLuminance, int maxToneMapLuminance, int minToneMapLuminance, float hdrPaperWhiteNits)
			{
				this.maxFullFrameToneMapLuminance = maxFullFrameToneMapLuminance;
				this.maxToneMapLuminance = maxToneMapLuminance;
				this.minToneMapLuminance = minToneMapLuminance;
				this.paperWhiteNits = hdrPaperWhiteNits;
			}

			// Token: 0x040008E7 RID: 2279
			public int maxFullFrameToneMapLuminance;

			// Token: 0x040008E8 RID: 2280
			public int maxToneMapLuminance;

			// Token: 0x040008E9 RID: 2281
			public int minToneMapLuminance;

			// Token: 0x040008EA RID: 2282
			public float paperWhiteNits;
		}

		// Token: 0x020001D1 RID: 465
		public static class ShaderKeywords
		{
			// Token: 0x040008EB RID: 2283
			public const string HDR_COLORSPACE_CONVERSION = "HDR_COLORSPACE_CONVERSION";

			// Token: 0x040008EC RID: 2284
			public const string HDR_ENCODING = "HDR_ENCODING";

			// Token: 0x040008ED RID: 2285
			public const string HDR_COLORSPACE_CONVERSION_AND_ENCODING = "HDR_COLORSPACE_CONVERSION_AND_ENCODING";

			// Token: 0x040008EE RID: 2286
			public const string HDR_INPUT = "HDR_INPUT";

			// Token: 0x040008EF RID: 2287
			internal static readonly ShaderKeyword HDRColorSpaceConversion = new ShaderKeyword("HDR_COLORSPACE_CONVERSION");

			// Token: 0x040008F0 RID: 2288
			internal static readonly ShaderKeyword HDREncoding = new ShaderKeyword("HDR_ENCODING");

			// Token: 0x040008F1 RID: 2289
			internal static readonly ShaderKeyword HDRColorSpaceConversionAndEncoding = new ShaderKeyword("HDR_COLORSPACE_CONVERSION_AND_ENCODING");

			// Token: 0x040008F2 RID: 2290
			internal static readonly ShaderKeyword HDRInput = new ShaderKeyword("HDR_INPUT");
		}

		// Token: 0x020001D2 RID: 466
		private static class ShaderPropertyId
		{
			// Token: 0x040008F3 RID: 2291
			public static readonly int hdrColorSpace = Shader.PropertyToID("_HDRColorspace");

			// Token: 0x040008F4 RID: 2292
			public static readonly int hdrEncoding = Shader.PropertyToID("_HDREncoding");
		}
	}
}
