using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores a set of four floating-point numbers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region" /> object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000022 RID: 34
	[Serializable]
	public struct RectangleF : IEquatable<RectangleF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="width">The width of the rectangle. </param>
		/// <param name="height">The height of the rectangle. </param>
		// Token: 0x06000170 RID: 368 RVA: 0x00005AF5 File Offset: 0x00003CF5
		public RectangleF(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of the rectangular region. </param>
		/// <param name="size">A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of the rectangular region. </param>
		// Token: 0x06000171 RID: 369 RVA: 0x00005B14 File Offset: 0x00003D14
		public RectangleF(PointF location, SizeF size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D8 RID: 216
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005B4A File Offset: 0x00003D4A
		[Browsable(false)]
		public PointF Location
		{
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005B66 File Offset: 0x00003D66
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005B6E File Offset: 0x00003D6E
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005B77 File Offset: 0x00003D77
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005B7F File Offset: 0x00003D7F
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

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005B88 File Offset: 0x00003D88
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005B90 File Offset: 0x00003D90
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005B99 File Offset: 0x00003D99
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005BAA File Offset: 0x00003DAA
		[Browsable(false)]
		public float Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets the y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005BB9 File Offset: 0x00003DB9
		[Browsable(false)]
		public float Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> with the same location and size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> and its X, Y, Width, and Height properties are equal to the corresponding properties of this <see cref="T:System.Drawing.RectangleF" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600017D RID: 381 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public override bool Equals(object obj)
		{
			return obj is RectangleF && this.Equals((RectangleF)obj);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public bool Equals(RectangleF other)
		{
			return this == other;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures have equal location and size.</summary>
		/// <returns>This operator returns true if the two specified <see cref="T:System.Drawing.RectangleF" /> structures have equal <see cref="P:System.Drawing.RectangleF.X" />, <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, and <see cref="P:System.Drawing.RectangleF.Height" /> properties.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600017F RID: 383 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public static bool operator ==(RectangleF left, RectangleF right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.RectangleF" /> structure. For information about the use of hash codes, see Object.GetHashCode.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.RectangleF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000180 RID: 384 RVA: 0x00005C40 File Offset: 0x00003E40
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode()), this.Width.GetHashCode()), this.Height.GetHashCode());
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.RectangleF" /> structure that is converted from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> structure to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000181 RID: 385 RVA: 0x00005C94 File Offset: 0x00003E94
		public static implicit operator RectangleF(Rectangle r)
		{
			return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
		}

		/// <summary>Converts the Location and <see cref="T:System.Drawing.Size" /> of this <see cref="T:System.Drawing.RectangleF" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.RectangleF" /> structure. For example, "{X=20, Y=20, Width=100, Height=50}".</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000182 RID: 386 RVA: 0x00005CBC File Offset: 0x00003EBC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(),
				",Y=",
				this.Y.ToString(),
				",Width=",
				this.Width.ToString(),
				",Height=",
				this.Height.ToString(),
				"}"
			});
		}

		// Token: 0x040000FE RID: 254
		private float x;

		// Token: 0x040000FF RID: 255
		private float y;

		// Token: 0x04000100 RID: 256
		private float width;

		// Token: 0x04000101 RID: 257
		private float height;
	}
}
