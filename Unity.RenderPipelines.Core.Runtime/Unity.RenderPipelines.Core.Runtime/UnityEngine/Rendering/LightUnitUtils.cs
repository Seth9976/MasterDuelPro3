using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001D9 RID: 473
	public static class LightUnitUtils
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00031782 File Offset: 0x0002F982
		private static float k_LuminanceToEvFactor
		{
			get
			{
				return Mathf.Log(100f / ColorUtils.s_LightMeterCalibrationConstant, 2f);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00031799 File Offset: 0x0002F999
		private static float k_EvToLuminanceFactor
		{
			get
			{
				return -LightUnitUtils.k_LuminanceToEvFactor;
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000317A1 File Offset: 0x0002F9A1
		public static LightUnit GetNativeLightUnit(LightType lightType)
		{
			switch (lightType)
			{
			case LightType.Spot:
			case LightType.Point:
			case LightType.Pyramid:
				return LightUnit.Candela;
			case LightType.Directional:
			case LightType.Box:
				return LightUnit.Lux;
			case LightType.Area:
			case LightType.Disc:
			case LightType.Tube:
				return LightUnit.Nits;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000317D8 File Offset: 0x0002F9D8
		public static bool IsLightUnitSupported(LightType lightType, LightUnit lightUnit)
		{
			int lightUnitFlag = 1 << (int)lightUnit;
			switch (lightType)
			{
			case LightType.Spot:
			case LightType.Point:
			case LightType.Pyramid:
				return (lightUnitFlag & 23) > 0;
			case LightType.Directional:
			case LightType.Box:
				return (lightUnitFlag & 4) > 0;
			case LightType.Area:
			case LightType.Disc:
			case LightType.Tube:
				return (lightUnitFlag & 25) > 0;
			default:
				return false;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003182C File Offset: 0x0002FA2C
		public static float GetSolidAngleFromPointLight()
		{
			return 12.566371f;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00031834 File Offset: 0x0002FA34
		public static float GetSolidAngleFromSpotLight(float spotAngle)
		{
			double angle = 3.141592653589793 * (double)spotAngle / 180.0;
			return (float)(6.283185307179586 * (1.0 - Math.Cos(angle * 0.5)));
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00031880 File Offset: 0x0002FA80
		public static float GetSolidAngleFromPyramidLight(float spotAngle, float aspectRatio)
		{
			if (aspectRatio < 1f)
			{
				aspectRatio = (float)(1.0 / (double)aspectRatio);
			}
			double angleA = 3.141592653589793 * (double)spotAngle / 180.0;
			double angleB = Math.Atan(Math.Tan(0.5 * angleA) * (double)aspectRatio) * 2.0;
			return (float)(4.0 * Math.Asin(Math.Sin(angleA * 0.5) * Math.Sin(angleB * 0.5)));
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00031910 File Offset: 0x0002FB10
		internal static float GetSolidAngle(LightType lightType, bool spotReflector, float spotAngle, float aspectRatio)
		{
			float num;
			if (lightType != LightType.Spot)
			{
				if (lightType != LightType.Point)
				{
					if (lightType != LightType.Pyramid)
					{
						throw new ArgumentException("Solid angle is undefined for lights of type " + lightType.ToString());
					}
					num = (spotReflector ? LightUnitUtils.GetSolidAngleFromPyramidLight(spotAngle, aspectRatio) : 12.566371f);
				}
				else
				{
					num = LightUnitUtils.GetSolidAngleFromPointLight();
				}
			}
			else
			{
				num = (spotReflector ? LightUnitUtils.GetSolidAngleFromSpotLight(spotAngle) : 12.566371f);
			}
			return num;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00031977 File Offset: 0x0002FB77
		public static float GetAreaFromRectangleLight(float rectSizeX, float rectSizeY)
		{
			return Mathf.Abs(rectSizeX * rectSizeY) * 3.1415927f;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00031987 File Offset: 0x0002FB87
		public static float GetAreaFromRectangleLight(Vector2 rectSize)
		{
			return LightUnitUtils.GetAreaFromRectangleLight(rectSize.x, rectSize.y);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003199A File Offset: 0x0002FB9A
		public static float GetAreaFromDiscLight(float discRadius)
		{
			return discRadius * discRadius * 3.1415927f * 3.1415927f;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000319AB File Offset: 0x0002FBAB
		public static float GetAreaFromTubeLight(float tubeLength)
		{
			return Mathf.Abs(tubeLength) * 4f * 3.1415927f;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000319BF File Offset: 0x0002FBBF
		public static float LumenToCandela(float lumen, float solidAngle)
		{
			return lumen / solidAngle;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000319C4 File Offset: 0x0002FBC4
		public static float CandelaToLumen(float candela, float solidAngle)
		{
			return candela * solidAngle;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000319BF File Offset: 0x0002FBBF
		public static float LumenToNits(float lumen, float area)
		{
			return lumen / area;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000319C4 File Offset: 0x0002FBC4
		public static float NitsToLumen(float nits, float area)
		{
			return nits * area;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000319C9 File Offset: 0x0002FBC9
		public static float LuxToCandela(float lux, float distance)
		{
			return lux * (distance * distance);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000319D0 File Offset: 0x0002FBD0
		public static float CandelaToLux(float candela, float distance)
		{
			return candela / (distance * distance);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000319D7 File Offset: 0x0002FBD7
		public static float Ev100ToNits(float ev100)
		{
			return Mathf.Pow(2f, ev100 + LightUnitUtils.k_EvToLuminanceFactor);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000319EA File Offset: 0x0002FBEA
		public static float NitsToEv100(float nits)
		{
			return Mathf.Log(nits, 2f) + LightUnitUtils.k_LuminanceToEvFactor;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000319FD File Offset: 0x0002FBFD
		public static float Ev100ToCandela(float ev100)
		{
			return LightUnitUtils.Ev100ToNits(ev100);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00031A05 File Offset: 0x0002FC05
		public static float CandelaToEv100(float candela)
		{
			return LightUnitUtils.NitsToEv100(candela);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00031A10 File Offset: 0x0002FC10
		internal static float ConvertIntensityInternal(float intensity, LightUnit fromUnit, LightUnit toUnit, LightType lightType, float area, float luxAtDistance, float solidAngle)
		{
			if (!LightUnitUtils.IsLightUnitSupported(lightType, fromUnit) || !LightUnitUtils.IsLightUnitSupported(lightType, toUnit))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Converting ",
					fromUnit.ToString(),
					" to ",
					toUnit.ToString(),
					" is undefined for lights of type ",
					lightType.ToString()
				}));
			}
			if (fromUnit == toUnit)
			{
				return intensity;
			}
			switch (fromUnit)
			{
			case LightUnit.Lumen:
				switch (toUnit)
				{
				case LightUnit.Candela:
					return LightUnitUtils.LumenToCandela(intensity, solidAngle);
				case LightUnit.Lux:
					return LightUnitUtils.CandelaToLux(LightUnitUtils.LumenToCandela(intensity, solidAngle), luxAtDistance);
				case LightUnit.Nits:
					return LightUnitUtils.LumenToNits(intensity, area);
				case LightUnit.Ev100:
				{
					float num;
					switch (lightType)
					{
					case LightType.Spot:
					case LightType.Point:
					case LightType.Pyramid:
						num = LightUnitUtils.LumenToCandela(intensity, solidAngle);
						goto IL_012A;
					case LightType.Area:
					case LightType.Disc:
					case LightType.Tube:
						num = LightUnitUtils.LumenToNits(intensity, area);
						goto IL_012A;
					}
					throw new ArgumentException("Converting from Lumen to Ev100 is undefined for light type " + lightType.ToString());
					IL_012A:
					return LightUnitUtils.NitsToEv100(num);
				}
				default:
					throw new ArgumentOutOfRangeException("toUnit", toUnit, null);
				}
				break;
			case LightUnit.Candela:
				switch (toUnit)
				{
				case LightUnit.Lumen:
					return LightUnitUtils.CandelaToLumen(intensity, solidAngle);
				case LightUnit.Lux:
					return LightUnitUtils.CandelaToLux(intensity, luxAtDistance);
				case LightUnit.Ev100:
					return LightUnitUtils.NitsToEv100(intensity);
				}
				throw new ArgumentOutOfRangeException("toUnit", toUnit, null);
			case LightUnit.Lux:
				switch (toUnit)
				{
				case LightUnit.Lumen:
					return LightUnitUtils.CandelaToLumen(LightUnitUtils.LuxToCandela(intensity, luxAtDistance), solidAngle);
				case LightUnit.Candela:
					return LightUnitUtils.LuxToCandela(intensity, luxAtDistance);
				case LightUnit.Ev100:
					return LightUnitUtils.NitsToEv100(LightUnitUtils.LuxToCandela(intensity, luxAtDistance));
				}
				throw new ArgumentOutOfRangeException("toUnit", toUnit, null);
			case LightUnit.Nits:
				if (toUnit == LightUnit.Lumen)
				{
					return LightUnitUtils.NitsToLumen(intensity, area);
				}
				if (toUnit != LightUnit.Ev100)
				{
					throw new ArgumentOutOfRangeException("toUnit", toUnit, null);
				}
				return LightUnitUtils.NitsToEv100(intensity);
			case LightUnit.Ev100:
				switch (toUnit)
				{
				case LightUnit.Lumen:
				{
					float candelaOrNits = LightUnitUtils.Ev100ToNits(intensity);
					switch (lightType)
					{
					case LightType.Spot:
					case LightType.Point:
					case LightType.Pyramid:
						return LightUnitUtils.CandelaToLumen(candelaOrNits, solidAngle);
					case LightType.Area:
					case LightType.Disc:
					case LightType.Tube:
						return LightUnitUtils.NitsToLumen(candelaOrNits, area);
					}
					throw new ArgumentException("Converting from Lumen to Ev100 is undefined for light type " + lightType.ToString());
				}
				case LightUnit.Candela:
				case LightUnit.Nits:
					return LightUnitUtils.Ev100ToNits(intensity);
				case LightUnit.Lux:
					return LightUnitUtils.CandelaToLux(LightUnitUtils.Ev100ToNits(intensity), luxAtDistance);
				default:
					throw new ArgumentOutOfRangeException("toUnit", toUnit, null);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("fromUnit", fromUnit, null);
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00031CDC File Offset: 0x0002FEDC
		public static float ConvertIntensity(Light light, float intensity, LightUnit fromUnit, LightUnit toUnit)
		{
			LightType lightType = light.type;
			float num;
			switch (lightType)
			{
			case LightType.Area:
				num = LightUnitUtils.GetAreaFromRectangleLight(light.areaSize);
				goto IL_0063;
			case LightType.Disc:
				num = LightUnitUtils.GetAreaFromDiscLight(light.areaSize.x);
				goto IL_0063;
			case LightType.Tube:
				num = LightUnitUtils.GetAreaFromTubeLight(light.areaSize.x);
				goto IL_0063;
			}
			num = 0f;
			IL_0063:
			float area = num;
			float luxAtDistance = light.luxAtDistance;
			if (lightType == LightType.Spot || lightType == LightType.Point || lightType == LightType.Pyramid)
			{
				num = LightUnitUtils.GetSolidAngle(lightType, light.enableSpotReflector, light.spotAngle, light.areaSize.x);
			}
			else
			{
				num = 0f;
			}
			float solidAngle = num;
			return LightUnitUtils.ConvertIntensityInternal(intensity, fromUnit, toUnit, lightType, area, luxAtDistance, solidAngle);
		}

		// Token: 0x0400090D RID: 2317
		public const float SphereSolidAngle = 12.566371f;
	}
}
