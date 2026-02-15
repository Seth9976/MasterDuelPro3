using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200001C RID: 28
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_ColorGradient : ScriptableObject
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public TMP_ColorGradient()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = TMP_ColorGradient.k_DefaultColor;
			this.topRight = TMP_ColorGradient.k_DefaultColor;
			this.bottomLeft = TMP_ColorGradient.k_DefaultColor;
			this.bottomRight = TMP_ColorGradient.k_DefaultColor;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public TMP_ColorGradient(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002D23 File Offset: 0x00000F23
		public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x04000073 RID: 115
		public ColorMode colorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000074 RID: 116
		public Color topLeft;

		// Token: 0x04000075 RID: 117
		public Color topRight;

		// Token: 0x04000076 RID: 118
		public Color bottomLeft;

		// Token: 0x04000077 RID: 119
		public Color bottomRight;

		// Token: 0x04000078 RID: 120
		private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000079 RID: 121
		private static readonly Color k_DefaultColor = Color.white;
	}
}
