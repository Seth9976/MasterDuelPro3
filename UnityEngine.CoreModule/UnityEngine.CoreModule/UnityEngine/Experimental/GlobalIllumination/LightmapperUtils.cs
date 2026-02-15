using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003EC RID: 1004
	public static class LightmapperUtils
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x0003C00C File Offset: 0x0003A20C
		public static LightMode Extract(LightmapBakeType baketype)
		{
			return (baketype == LightmapBakeType.Realtime) ? LightMode.Realtime : ((baketype == LightmapBakeType.Mixed) ? LightMode.Mixed : LightMode.Baked);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0003C030 File Offset: 0x0003A230
		public static LinearColor ExtractIndirect(Light l)
		{
			return LinearColor.Convert(l.color, l.intensity * l.bounceIntensity);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0003C05C File Offset: 0x0003A25C
		public static float ExtractInnerCone(Light l)
		{
			return 2f * Mathf.Atan(Mathf.Tan(l.spotAngle * 0.5f * 0.017453292f) * 46f / 64f);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0003C09C File Offset: 0x0003A29C
		private static Color ExtractColorTemperature(Light l)
		{
			Color cct = new Color(1f, 1f, 1f);
			bool flag = l.useColorTemperature && GraphicsSettings.lightsUseLinearIntensity;
			if (flag)
			{
				cct = Mathf.CorrelatedColorTemperatureToRGB(l.colorTemperature);
			}
			return cct;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0003C0E5 File Offset: 0x0003A2E5
		private static void ApplyColorTemperature(Color cct, ref LinearColor lightColor)
		{
			lightColor.red *= cct.r;
			lightColor.green *= cct.g;
			lightColor.blue *= cct.b;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0003C124 File Offset: 0x0003A324
		public static void Extract(Light l, ref DirectionalLight dir)
		{
			dir.instanceID = l.GetInstanceID();
			dir.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			dir.shadow = l.shadows > LightShadows.None;
			dir.position = l.transform.position;
			dir.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor directColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref directColor);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			dir.color = directColor;
			dir.indirectColor = indirectColor;
			dir.penumbraWidthRadian = 0f;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		public static void Extract(Light l, ref PointLight point)
		{
			point.instanceID = l.GetInstanceID();
			point.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			point.shadow = l.shadows > LightShadows.None;
			point.position = l.transform.position;
			point.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor directColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref directColor);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			point.color = directColor;
			point.indirectColor = indirectColor;
			point.range = l.range;
			point.sphereRadius = 0f;
			point.falloff = FalloffType.Legacy;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0003C290 File Offset: 0x0003A490
		public static void Extract(Light l, ref SpotLight spot)
		{
			spot.instanceID = l.GetInstanceID();
			spot.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			spot.shadow = l.shadows > LightShadows.None;
			spot.position = l.transform.position;
			spot.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor directColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref directColor);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			spot.color = directColor;
			spot.indirectColor = indirectColor;
			spot.range = l.range;
			spot.sphereRadius = 0f;
			spot.coneAngle = l.spotAngle * 0.017453292f;
			spot.innerConeAngle = LightmapperUtils.ExtractInnerCone(l);
			spot.falloff = FalloffType.Legacy;
			spot.angularFalloff = AngularFalloffType.LUT;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0003C374 File Offset: 0x0003A574
		public static void Extract(Light l, ref RectangleLight rect)
		{
			rect.instanceID = l.GetInstanceID();
			rect.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			rect.shadow = l.shadows > LightShadows.None;
			rect.position = l.transform.position;
			rect.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor directColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref directColor);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			rect.color = directColor;
			rect.indirectColor = indirectColor;
			rect.range = l.dilatedRange;
			rect.width = 0f;
			rect.height = 0f;
			rect.falloff = FalloffType.Legacy;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0003C440 File Offset: 0x0003A640
		public static void Extract(Light l, ref DiscLight disc)
		{
			disc.instanceID = l.GetInstanceID();
			disc.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			disc.shadow = l.shadows > LightShadows.None;
			disc.position = l.transform.position;
			disc.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor directColor = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref directColor);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			disc.color = directColor;
			disc.indirectColor = indirectColor;
			disc.range = l.dilatedRange;
			disc.radius = 0f;
			disc.falloff = FalloffType.Legacy;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0003C500 File Offset: 0x0003A700
		public static void Extract(Light l, out Cookie cookie)
		{
			cookie.instanceID = (l.cookie ? l.cookie.GetInstanceID() : 0);
			cookie.scale = 1f;
			cookie.sizes = ((l.type == LightType.Directional && l.cookie) ? new Vector2(l.cookieSize, l.cookieSize) : new Vector2(1f, 1f));
		}
	}
}
