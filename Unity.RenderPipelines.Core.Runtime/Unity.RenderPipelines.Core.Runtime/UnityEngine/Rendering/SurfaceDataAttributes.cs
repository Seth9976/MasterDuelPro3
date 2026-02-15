using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000181 RID: 385
	[AttributeUsage(AttributeTargets.Field)]
	public class SurfaceDataAttributes : Attribute
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000273B4 File Offset: 0x000255B4
		public SurfaceDataAttributes(string displayName = "", bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00027402 File Offset: 0x00025602
		public SurfaceDataAttributes(string[] displayNames, bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x0400077B RID: 1915
		public string[] displayNames;

		// Token: 0x0400077C RID: 1916
		public bool isDirection;

		// Token: 0x0400077D RID: 1917
		public bool sRGBDisplay;

		// Token: 0x0400077E RID: 1918
		public FieldPrecision precision;

		// Token: 0x0400077F RID: 1919
		public bool checkIsNormalized;

		// Token: 0x04000780 RID: 1920
		public string preprocessor;
	}
}
