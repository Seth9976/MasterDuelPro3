using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This class cannot be inherited.</summary>
	// Token: 0x02000080 RID: 128
	[DefaultMember("Item")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ColorMatrix
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class using the elements in the specified matrix <paramref name="newColorMatrix" />.</summary>
		/// <param name="newColorMatrix">The values of the elements for the new <see cref="T:System.Drawing.Imaging.ColorMatrix" />. </param>
		// Token: 0x06000450 RID: 1104 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		[CLSCompliant(false)]
		public ColorMatrix(float[][] newColorMatrix)
		{
			this.SetMatrix(newColorMatrix);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		internal void SetMatrix(float[][] newColorMatrix)
		{
			this._matrix00 = newColorMatrix[0][0];
			this._matrix01 = newColorMatrix[0][1];
			this._matrix02 = newColorMatrix[0][2];
			this._matrix03 = newColorMatrix[0][3];
			this._matrix04 = newColorMatrix[0][4];
			this._matrix10 = newColorMatrix[1][0];
			this._matrix11 = newColorMatrix[1][1];
			this._matrix12 = newColorMatrix[1][2];
			this._matrix13 = newColorMatrix[1][3];
			this._matrix14 = newColorMatrix[1][4];
			this._matrix20 = newColorMatrix[2][0];
			this._matrix21 = newColorMatrix[2][1];
			this._matrix22 = newColorMatrix[2][2];
			this._matrix23 = newColorMatrix[2][3];
			this._matrix24 = newColorMatrix[2][4];
			this._matrix30 = newColorMatrix[3][0];
			this._matrix31 = newColorMatrix[3][1];
			this._matrix32 = newColorMatrix[3][2];
			this._matrix33 = newColorMatrix[3][3];
			this._matrix34 = newColorMatrix[3][4];
			this._matrix40 = newColorMatrix[4][0];
			this._matrix41 = newColorMatrix[4][1];
			this._matrix42 = newColorMatrix[4][2];
			this._matrix43 = newColorMatrix[4][3];
			this._matrix44 = newColorMatrix[4][4];
		}

		// Token: 0x04000236 RID: 566
		private float _matrix00;

		// Token: 0x04000237 RID: 567
		private float _matrix01;

		// Token: 0x04000238 RID: 568
		private float _matrix02;

		// Token: 0x04000239 RID: 569
		private float _matrix03;

		// Token: 0x0400023A RID: 570
		private float _matrix04;

		// Token: 0x0400023B RID: 571
		private float _matrix10;

		// Token: 0x0400023C RID: 572
		private float _matrix11;

		// Token: 0x0400023D RID: 573
		private float _matrix12;

		// Token: 0x0400023E RID: 574
		private float _matrix13;

		// Token: 0x0400023F RID: 575
		private float _matrix14;

		// Token: 0x04000240 RID: 576
		private float _matrix20;

		// Token: 0x04000241 RID: 577
		private float _matrix21;

		// Token: 0x04000242 RID: 578
		private float _matrix22;

		// Token: 0x04000243 RID: 579
		private float _matrix23;

		// Token: 0x04000244 RID: 580
		private float _matrix24;

		// Token: 0x04000245 RID: 581
		private float _matrix30;

		// Token: 0x04000246 RID: 582
		private float _matrix31;

		// Token: 0x04000247 RID: 583
		private float _matrix32;

		// Token: 0x04000248 RID: 584
		private float _matrix33;

		// Token: 0x04000249 RID: 585
		private float _matrix34;

		// Token: 0x0400024A RID: 586
		private float _matrix40;

		// Token: 0x0400024B RID: 587
		private float _matrix41;

		// Token: 0x0400024C RID: 588
		private float _matrix42;

		// Token: 0x0400024D RID: 589
		private float _matrix43;

		// Token: 0x0400024E RID: 590
		private float _matrix44;
	}
}
