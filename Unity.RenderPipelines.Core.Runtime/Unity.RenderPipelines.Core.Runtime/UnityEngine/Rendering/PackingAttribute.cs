using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class PackingAttribute : Attribute
	{
		// Token: 0x06000ADA RID: 2778 RVA: 0x00027450 File Offset: 0x00025650
		public PackingAttribute(string[] displayNames, FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 32, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000274B8 File Offset: 0x000256B8
		public PackingAttribute(string displayName = "", FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 0, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}

		// Token: 0x04000783 RID: 1923
		public string[] displayNames;

		// Token: 0x04000784 RID: 1924
		public float[] range;

		// Token: 0x04000785 RID: 1925
		public FieldPacking packingScheme;

		// Token: 0x04000786 RID: 1926
		public int offsetInSource;

		// Token: 0x04000787 RID: 1927
		public int sizeInBits;

		// Token: 0x04000788 RID: 1928
		public bool isDirection;

		// Token: 0x04000789 RID: 1929
		public bool sRGBDisplay;

		// Token: 0x0400078A RID: 1930
		public bool checkIsNormalized;

		// Token: 0x0400078B RID: 1931
		public string preprocessor;
	}
}
