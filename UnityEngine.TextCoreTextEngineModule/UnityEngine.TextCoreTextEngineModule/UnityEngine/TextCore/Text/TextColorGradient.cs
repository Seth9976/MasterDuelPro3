using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000036 RID: 54
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextColorGradient : ScriptableObject
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public TextColorGradient()
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = TextColorGradient.k_DefaultColor;
			this.topRight = TextColorGradient.k_DefaultColor;
			this.bottomLeft = TextColorGradient.k_DefaultColor;
			this.bottomRight = TextColorGradient.k_DefaultColor;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000ADCB File Offset: 0x00008FCB
		public TextColorGradient(Color color)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000ADFF File Offset: 0x00008FFF
		public TextColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x0400015C RID: 348
		public ColorGradientMode colorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x0400015D RID: 349
		public Color topLeft;

		// Token: 0x0400015E RID: 350
		public Color topRight;

		// Token: 0x0400015F RID: 351
		public Color bottomLeft;

		// Token: 0x04000160 RID: 352
		public Color bottomRight;

		// Token: 0x04000161 RID: 353
		private const ColorGradientMode k_DefaultColorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x04000162 RID: 354
		private static readonly Color k_DefaultColor = Color.white;
	}
}
