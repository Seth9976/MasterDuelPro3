using System;

namespace System.Drawing
{
	/// <summary>Specifies the unit of measure for the given data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000045 RID: 69
	public enum GraphicsUnit
	{
		/// <summary>Specifies the world coordinate system unit as the unit of measure.</summary>
		// Token: 0x04000151 RID: 337
		World,
		/// <summary>Specifies the unit of measure of the display device. Typically pixels for video displays, and 1/100 inch for printers.</summary>
		// Token: 0x04000152 RID: 338
		Display,
		/// <summary>Specifies a device pixel as the unit of measure.</summary>
		// Token: 0x04000153 RID: 339
		Pixel,
		/// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
		// Token: 0x04000154 RID: 340
		Point,
		/// <summary>Specifies the inch as the unit of measure.</summary>
		// Token: 0x04000155 RID: 341
		Inch,
		/// <summary>Specifies the document unit (1/300 inch) as the unit of measure.</summary>
		// Token: 0x04000156 RID: 342
		Document,
		/// <summary>Specifies the millimeter as the unit of measure.</summary>
		// Token: 0x04000157 RID: 343
		Millimeter
	}
}
