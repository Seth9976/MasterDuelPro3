using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> with a linear gradient. This class cannot be inherited.</summary>
	// Token: 0x0200009E RID: 158
	public sealed class LinearGradientBrush : Brush
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000E909 File Offset: 0x0000CB09
		internal LinearGradientBrush(IntPtr native)
		{
			Status status = GDIPlus.GdipGetLineRect(native, out this.rectangle);
			base.SetNativeBrush(native);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
		/// <param name="point1">A <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the linear gradient. </param>
		/// <param name="point2">A <see cref="T:System.Drawing.Point" /> structure that represents the endpoint of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient. </param>
		// Token: 0x060004AB RID: 1195 RVA: 0x0000E92C File Offset: 0x0000CB2C
		public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushI(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(intPtr, out this.rectangle));
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and orientation.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, LinearGradientMode.ForwardDiagonal specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle. </param>
		// Token: 0x060004AC RID: 1196 RVA: 0x0000E978 File Offset: 0x0000CB78
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (linearGradientMode < LinearGradientMode.Horizontal || linearGradientMode > LinearGradientMode.BackwardDiagonal)
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectI(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			this.rectangle = rect;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> this method creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public override object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus((Status)GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out intPtr));
			return new LinearGradientBrush(intPtr);
		}

		// Token: 0x04000328 RID: 808
		private RectangleF rectangle;
	}
}
