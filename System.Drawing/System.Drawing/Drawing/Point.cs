using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001F RID: 31
	[TypeConverter(typeof(PointConverter))]
	[Serializable]
	public struct Point : IEquatable<Point>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point. </param>
		/// <param name="y">The vertical position of the point. </param>
		// Token: 0x06000133 RID: 307 RVA: 0x00005311 File Offset: 0x00003511
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005321 File Offset: 0x00003521
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005329 File Offset: 0x00003529
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005332 File Offset: 0x00003532
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000533A File Offset: 0x0000353A
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000138 RID: 312 RVA: 0x00005343 File Offset: 0x00003543
		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate. </param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000139 RID: 313 RVA: 0x0000535A File Offset: 0x0000355A
		public static Point operator +(Point pt, Size sz)
		{
			return Point.Add(pt, sz);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are equal.</summary>
		/// <returns>true if the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600013A RID: 314 RVA: 0x00005363 File Offset: 0x00003563
		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> or <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are unequal.</summary>
		/// <returns>true if the values of either the <see cref="P:System.Drawing.Point.X" /> properties or the <see cref="P:System.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600013B RID: 315 RVA: 0x00005387 File Offset: 0x00003587
		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		/// <summary>Adds the specified <see cref="T:System.Drawing.Size" /> to the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the addition operation.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to add.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to add</param>
		// Token: 0x0600013C RID: 316 RVA: 0x00005393 File Offset: 0x00003593
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Point" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600013D RID: 317 RVA: 0x000053B8 File Offset: 0x000035B8
		public override bool Equals(object obj)
		{
			return obj is Point && this.Equals((Point)obj);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000053D0 File Offset: 0x000035D0
		public bool Equals(Point other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600013F RID: 319 RVA: 0x000053DE File Offset: 0x000035DE
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.X, this.Y);
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset the x-coordinate. </param>
		/// <param name="dy">The amount to offset the y-coordinate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000140 RID: 320 RVA: 0x000053F1 File Offset: 0x000035F1
		public void Offset(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Point" /> to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000141 RID: 321 RVA: 0x00005410 File Offset: 0x00003610
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(),
				",Y=",
				this.Y.ToString(),
				"}"
			});
		}

		/// <summary>Represents a <see cref="T:System.Drawing.Point" /> that has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values set to zero. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040000F4 RID: 244
		public static readonly Point Empty;

		// Token: 0x040000F5 RID: 245
		private int x;

		// Token: 0x040000F6 RID: 246
		private int y;
	}
}
