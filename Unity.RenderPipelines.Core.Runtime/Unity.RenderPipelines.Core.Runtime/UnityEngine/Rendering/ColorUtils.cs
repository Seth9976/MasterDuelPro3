using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001B5 RID: 437
	public static class ColorUtils
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002D7B4 File Offset: 0x0002B9B4
		public static float lensImperfectionExposureScale
		{
			get
			{
				return 78f / (100f * ColorUtils.s_LensAttenuation);
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002D7C7 File Offset: 0x0002B9C7
		public static float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		public static Vector3 CIExyToLMS(float x, float y)
		{
			float Y = 1f;
			float X = Y * x / y;
			float Z = Y * (1f - x - y) / y;
			float num = 0.7328f * X + 0.4296f * Y - 0.1624f * Z;
			float M = -0.7036f * X + 1.6975f * Y + 0.0061f * Z;
			float S = 0.003f * X + 0.0136f * Y + 0.9834f * Z;
			return new Vector3(num, M, S);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002D858 File Offset: 0x0002BA58
		public static Vector3 ColorBalanceToLMSCoeffs(float temperature, float tint)
		{
			float t = temperature / 65f;
			float t2 = tint / 65f;
			float num = 0.31271f - t * ((t < 0f) ? 0.1f : 0.05f);
			float y = ColorUtils.StandardIlluminantY(num) + t2 * 0.05f;
			Vector3 w = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 w2 = ColorUtils.CIExyToLMS(num, y);
			return new Vector3(w.x / w2.x, w.y / w2.y, w.z / w2.z);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		public static ValueTuple<Vector4, Vector4, Vector4> PrepareShadowsMidtonesHighlights(in Vector4 inShadows, in Vector4 inMidtones, in Vector4 inHighlights)
		{
			Vector4 shadows = inShadows;
			shadows.x = Mathf.GammaToLinearSpace(shadows.x);
			shadows.y = Mathf.GammaToLinearSpace(shadows.y);
			shadows.z = Mathf.GammaToLinearSpace(shadows.z);
			float weight = shadows.w * ((Mathf.Sign(shadows.w) < 0f) ? 1f : 4f);
			shadows.x = Mathf.Max(shadows.x + weight, 0f);
			shadows.y = Mathf.Max(shadows.y + weight, 0f);
			shadows.z = Mathf.Max(shadows.z + weight, 0f);
			shadows.w = 0f;
			Vector4 midtones = inMidtones;
			midtones.x = Mathf.GammaToLinearSpace(midtones.x);
			midtones.y = Mathf.GammaToLinearSpace(midtones.y);
			midtones.z = Mathf.GammaToLinearSpace(midtones.z);
			weight = midtones.w * ((Mathf.Sign(midtones.w) < 0f) ? 1f : 4f);
			midtones.x = Mathf.Max(midtones.x + weight, 0f);
			midtones.y = Mathf.Max(midtones.y + weight, 0f);
			midtones.z = Mathf.Max(midtones.z + weight, 0f);
			midtones.w = 0f;
			Vector4 highlights = inHighlights;
			highlights.x = Mathf.GammaToLinearSpace(highlights.x);
			highlights.y = Mathf.GammaToLinearSpace(highlights.y);
			highlights.z = Mathf.GammaToLinearSpace(highlights.z);
			weight = highlights.w * ((Mathf.Sign(highlights.w) < 0f) ? 1f : 4f);
			highlights.x = Mathf.Max(highlights.x + weight, 0f);
			highlights.y = Mathf.Max(highlights.y + weight, 0f);
			highlights.z = Mathf.Max(highlights.z + weight, 0f);
			highlights.w = 0f;
			return new ValueTuple<Vector4, Vector4, Vector4>(shadows, midtones, highlights);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002DB34 File Offset: 0x0002BD34
		public static ValueTuple<Vector4, Vector4, Vector4> PrepareLiftGammaGain(in Vector4 inLift, in Vector4 inGamma, in Vector4 inGain)
		{
			Vector4 lift = inLift;
			lift.x = Mathf.GammaToLinearSpace(lift.x) * 0.15f;
			lift.y = Mathf.GammaToLinearSpace(lift.y) * 0.15f;
			lift.z = Mathf.GammaToLinearSpace(lift.z) * 0.15f;
			Color color = lift;
			float lumLift = ColorUtils.Luminance(in color);
			lift.x = lift.x - lumLift + lift.w;
			lift.y = lift.y - lumLift + lift.w;
			lift.z = lift.z - lumLift + lift.w;
			lift.w = 0f;
			Vector4 gamma = inGamma;
			gamma.x = Mathf.GammaToLinearSpace(gamma.x) * 0.8f;
			gamma.y = Mathf.GammaToLinearSpace(gamma.y) * 0.8f;
			gamma.z = Mathf.GammaToLinearSpace(gamma.z) * 0.8f;
			color = gamma;
			float lumGamma = ColorUtils.Luminance(in color);
			gamma.w += 1f;
			gamma.x = 1f / Mathf.Max(gamma.x - lumGamma + gamma.w, 0.001f);
			gamma.y = 1f / Mathf.Max(gamma.y - lumGamma + gamma.w, 0.001f);
			gamma.z = 1f / Mathf.Max(gamma.z - lumGamma + gamma.w, 0.001f);
			gamma.w = 0f;
			Vector4 gain = inGain;
			gain.x = Mathf.GammaToLinearSpace(gain.x) * 0.8f;
			gain.y = Mathf.GammaToLinearSpace(gain.y) * 0.8f;
			gain.z = Mathf.GammaToLinearSpace(gain.z) * 0.8f;
			color = gain;
			float lumGain = ColorUtils.Luminance(in color);
			gain.w += 1f;
			gain.x = gain.x - lumGain + gain.w;
			gain.y = gain.y - lumGain + gain.w;
			gain.z = gain.z - lumGain + gain.w;
			gain.w = 0f;
			return new ValueTuple<Vector4, Vector4, Vector4>(lift, gamma, gain);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		public static ValueTuple<Vector4, Vector4> PrepareSplitToning(in Vector4 inShadows, in Vector4 inHighlights, float balance)
		{
			Vector4 shadows = inShadows;
			Vector4 highlights = inHighlights;
			shadows.w = balance / 100f;
			highlights.w = 0f;
			return new ValueTuple<Vector4, Vector4>(shadows, highlights);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002DDEC File Offset: 0x0002BFEC
		public static float Luminance(in Color color)
		{
			return color.r * 0.2126729f + color.g * 0.7151522f + color.b * 0.072175f;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002DE14 File Offset: 0x0002C014
		public static float ComputeEV100(float aperture, float shutterSpeed, float ISO)
		{
			return Mathf.Log(aperture * aperture / shutterSpeed * 100f / ISO, 2f);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002DE30 File Offset: 0x0002C030
		public static float ConvertEV100ToExposure(float EV100)
		{
			float maxLuminance = ColorUtils.lensImperfectionExposureScale * Mathf.Pow(2f, EV100);
			return 1f / maxLuminance;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002DE56 File Offset: 0x0002C056
		public static float ConvertExposureToEV100(float exposure)
		{
			return Mathf.Log(1f / (ColorUtils.lensImperfectionExposureScale * exposure), 2f);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002DE70 File Offset: 0x0002C070
		public static float ComputeEV100FromAvgLuminance(float avgLuminance)
		{
			float K = ColorUtils.s_LightMeterCalibrationConstant;
			return Mathf.Log(avgLuminance * 100f / K, 2f);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002DE96 File Offset: 0x0002C096
		public static float ComputeISO(float aperture, float shutterSpeed, float targetEV100)
		{
			return aperture * aperture * 100f / (shutterSpeed * Mathf.Pow(2f, targetEV100));
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		public static uint ToHex(Color c)
		{
			return ((uint)(c.a * 255f) << 24) | ((uint)(c.r * 255f) << 16) | ((uint)(c.g * 255f) << 8) | (uint)(c.b * 255f);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002DEFC File Offset: 0x0002C0FC
		public static Color ToRGBA(uint hex)
		{
			return new Color(((hex >> 16) & 255U) / 255f, ((hex >> 8) & 255U) / 255f, (hex & 255U) / 255f, ((hex >> 24) & 255U) / 255f);
		}

		// Token: 0x0400086E RID: 2158
		public static float s_LightMeterCalibrationConstant = 12.5f;

		// Token: 0x0400086F RID: 2159
		public static float s_LensAttenuation = 0.65f;
	}
}
