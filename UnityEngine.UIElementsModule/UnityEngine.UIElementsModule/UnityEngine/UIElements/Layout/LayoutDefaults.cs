using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000591 RID: 1425
	internal struct LayoutDefaults
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x0009AF9C File Offset: 0x0009919C
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static LayoutDefaults()
		{
			FixedBuffer9<LayoutValue> fixedBuffer = default(FixedBuffer9<LayoutValue>);
			*fixedBuffer[0] = LayoutValue.Undefined();
			*fixedBuffer[1] = LayoutValue.Undefined();
			*fixedBuffer[2] = LayoutValue.Undefined();
			*fixedBuffer[3] = LayoutValue.Undefined();
			*fixedBuffer[4] = LayoutValue.Undefined();
			*fixedBuffer[5] = LayoutValue.Undefined();
			*fixedBuffer[6] = LayoutValue.Undefined();
			*fixedBuffer[7] = LayoutValue.Undefined();
			*fixedBuffer[8] = LayoutValue.Undefined();
			LayoutDefaults.EdgeValuesUnit = fixedBuffer;
			LayoutDefaults.DimensionValues = new float[] { float.NaN, float.NaN };
			FixedBuffer2<LayoutValue> fixedBuffer2 = default(FixedBuffer2<LayoutValue>);
			*fixedBuffer2[0] = LayoutValue.Undefined();
			*fixedBuffer2[1] = LayoutValue.Undefined();
			LayoutDefaults.DimensionValuesUnit = fixedBuffer2;
			fixedBuffer2 = default(FixedBuffer2<LayoutValue>);
			*fixedBuffer2[0] = LayoutValue.Auto();
			*fixedBuffer2[1] = LayoutValue.Auto();
			LayoutDefaults.DimensionValuesAutoUnit = fixedBuffer2;
		}

		// Token: 0x040013EC RID: 5100
		public static readonly FixedBuffer9<LayoutValue> EdgeValuesUnit;

		// Token: 0x040013ED RID: 5101
		public static readonly float[] DimensionValues;

		// Token: 0x040013EE RID: 5102
		public static readonly FixedBuffer2<LayoutValue> DimensionValuesUnit;

		// Token: 0x040013EF RID: 5103
		public static readonly FixedBuffer2<LayoutValue> DimensionValuesAutoUnit;
	}
}
