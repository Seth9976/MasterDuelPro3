using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines a rectangular brush with a hatch style, a foreground color, and a background color. This class cannot be inherited.</summary>
	// Token: 0x02000099 RID: 153
	public sealed class HatchBrush : Brush
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration, foreground color, and background color.</summary>
		/// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of spaces between the lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E864 File Offset: 0x0000CA64
		public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
		{
			if (hatchstyle < HatchStyle.Horizontal || hatchstyle > HatchStyle.SolidDiamond)
			{
				throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", new object[] { "hatchstyle", hatchstyle, "HatchStyle" }), "hatchstyle");
			}
			IntPtr intPtr;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateHatchBrush((int)hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out intPtr));
			base.SetNativeBrushInternal(intPtr);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00004911 File Offset: 0x00002B11
		internal HatchBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new HatchBrush(zero);
		}
	}
}
