using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E4 RID: 996
	public struct LinearColor
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0003B8B0 File Offset: 0x00039AB0
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0003B8C8 File Offset: 0x00039AC8
		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0003B910 File Offset: 0x00039B10
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0003B928 File Offset: 0x00039B28
		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0003B970 File Offset: 0x00039B70
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0003B988 File Offset: 0x00039B88
		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0003B9D0 File Offset: 0x00039BD0
		public static LinearColor Convert(Color color, float intensity)
		{
			Color lc = (GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear);
			float mcc = lc.maxColorComponent;
			bool flag = lc.r < 0f || lc.g < 0f || lc.b < 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new string[]
				{
					"The input color to be converted must not contain negative values (red: ",
					lc.r.ToString(),
					", green: ",
					lc.g.ToString(),
					", blue: ",
					lc.b.ToString(),
					")."
				}));
			}
			bool flag2 = mcc <= 1E-20f;
			LinearColor linearColor;
			if (flag2)
			{
				linearColor = LinearColor.Black();
			}
			else
			{
				float mcc_rcp = 1f / lc.maxColorComponent;
				LinearColor c;
				c.m_red = lc.r * mcc_rcp;
				c.m_green = lc.g * mcc_rcp;
				c.m_blue = lc.b * mcc_rcp;
				c.m_intensity = mcc;
				linearColor = c;
			}
			return linearColor;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0003BB04 File Offset: 0x00039D04
		public static LinearColor Black()
		{
			LinearColor c;
			c.m_red = (c.m_green = (c.m_blue = (c.m_intensity = 0f)));
			return c;
		}

		// Token: 0x04000D31 RID: 3377
		private float m_red;

		// Token: 0x04000D32 RID: 3378
		private float m_green;

		// Token: 0x04000D33 RID: 3379
		private float m_blue;

		// Token: 0x04000D34 RID: 3380
		private float m_intensity;
	}
}
