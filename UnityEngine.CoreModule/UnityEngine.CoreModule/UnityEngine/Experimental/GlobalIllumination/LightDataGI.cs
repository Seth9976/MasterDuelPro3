using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003EB RID: 1003
	[UsedByNativeCode]
	public struct LightDataGI
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x0003BB84 File Offset: 0x00039D84
		public void Init(ref DirectionalLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = 0f;
			this.coneAngle = cookie.sizes.x;
			this.innerConeAngle = cookie.sizes.y;
			this.shape0 = light.penumbraWidthRadian;
			this.shape1 = 0f;
			this.type = LightType.Directional;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = FalloffType.Undefined;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0003BC58 File Offset: 0x00039E58
		public void Init(ref PointLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.sphereRadius;
			this.shape1 = 0f;
			this.type = LightType.Point;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0003BD28 File Offset: 0x00039F28
		public void Init(ref SpotLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = light.coneAngle;
			this.innerConeAngle = light.innerConeAngle;
			this.shape0 = light.sphereRadius;
			this.shape1 = (float)light.angularFalloff;
			this.type = LightType.Spot;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0003BDFC File Offset: 0x00039FFC
		public void Init(ref RectangleLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.width;
			this.shape1 = light.height;
			this.type = LightType.Rectangle;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0003BECC File Offset: 0x0003A0CC
		public void Init(ref DiscLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.radius;
			this.shape1 = 0f;
			this.type = LightType.Disc;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0003BF9C File Offset: 0x0003A19C
		public void Init(ref DirectionalLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0003BFBC File Offset: 0x0003A1BC
		public void Init(ref PointLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		public void Init(ref SpotLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0003BFFA File Offset: 0x0003A1FA
		public void InitNoBake(int lightInstanceID)
		{
			this.instanceID = lightInstanceID;
			this.mode = LightMode.Unknown;
		}

		// Token: 0x04000D6D RID: 3437
		public int instanceID;

		// Token: 0x04000D6E RID: 3438
		public int cookieID;

		// Token: 0x04000D6F RID: 3439
		public float cookieScale;

		// Token: 0x04000D70 RID: 3440
		public LinearColor color;

		// Token: 0x04000D71 RID: 3441
		public LinearColor indirectColor;

		// Token: 0x04000D72 RID: 3442
		public Quaternion orientation;

		// Token: 0x04000D73 RID: 3443
		public Vector3 position;

		// Token: 0x04000D74 RID: 3444
		public float range;

		// Token: 0x04000D75 RID: 3445
		public float coneAngle;

		// Token: 0x04000D76 RID: 3446
		public float innerConeAngle;

		// Token: 0x04000D77 RID: 3447
		public float shape0;

		// Token: 0x04000D78 RID: 3448
		public float shape1;

		// Token: 0x04000D79 RID: 3449
		public LightType type;

		// Token: 0x04000D7A RID: 3450
		public LightMode mode;

		// Token: 0x04000D7B RID: 3451
		public byte shadow;

		// Token: 0x04000D7C RID: 3452
		public FalloffType falloff;
	}
}
