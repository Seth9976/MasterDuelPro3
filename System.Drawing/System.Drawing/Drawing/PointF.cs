using System;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000020 RID: 32
	[Serializable]
	public struct PointF : IEquatable<PointF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.PointF" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point. </param>
		/// <param name="y">The vertical position of the point. </param>
		// Token: 0x06000142 RID: 322 RVA: 0x00005462 File Offset: 0x00003662
		public PointF(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005472 File Offset: 0x00003672
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000547A File Offset: 0x0000367A
		public float X
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

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005483 File Offset: 0x00003683
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000548B File Offset: 0x0000368B
		public float Y
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

		/// <summary>Compares two <see cref="T:System.Drawing.PointF" /> structures. The result specifies whether the values of the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> properties of the two <see cref="T:System.Drawing.PointF" /> structures are equal.</summary>
		/// <returns>true if the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of the left and right <see cref="T:System.Drawing.PointF" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000147 RID: 327 RVA: 0x00005494 File Offset: 0x00003694
		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.PointF" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.PointF" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000148 RID: 328 RVA: 0x000054B8 File Offset: 0x000036B8
		public override bool Equals(object obj)
		{
			return obj is PointF && this.Equals((PointF)obj);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000054D0 File Offset: 0x000036D0
		public bool Equals(PointF other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.PointF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014A RID: 330 RVA: 0x000054E0 File Offset: 0x000036E0
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode());
		}

		/// <summary>Converts this <see cref="T:System.Drawing.PointF" /> to a human readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014B RID: 331 RVA: 0x00005510 File Offset: 0x00003710
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.x.ToString(),
				", Y=",
				this.y.ToString(),
				"}"
			});
		}

		// Token: 0x040000F7 RID: 247
		private float x;

		// Token: 0x040000F8 RID: 248
		private float y;
	}
}
