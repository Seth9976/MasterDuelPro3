using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000021 RID: 33
	[TypeConverter(typeof(RectangleConverter))]
	[Serializable]
	public struct Rectangle : IEquatable<Rectangle>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="width">The width of the rectangle. </param>
		/// <param name="height">The height of the rectangle. </param>
		// Token: 0x0600014C RID: 332 RVA: 0x0000555C File Offset: 0x0000375C
		public Rectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region. </param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region. </param>
		// Token: 0x0600014D RID: 333 RVA: 0x0000557B File Offset: 0x0000377B
		public Rectangle(Point location, Size size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
		/// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014E RID: 334 RVA: 0x000055B1 File Offset: 0x000037B1
		public static Rectangle FromLTRB(int left, int top, int right, int bottom)
		{
			return new Rectangle(left, top, right - left, bottom - top);
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000055C0 File Offset: 0x000037C0
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000055D3 File Offset: 0x000037D3
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000055EF File Offset: 0x000037EF
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00005602 File Offset: 0x00003802
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000561E File Offset: 0x0000381E
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00005626 File Offset: 0x00003826
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000562F File Offset: 0x0000382F
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005637 File Offset: 0x00003837
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

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005640 File Offset: 0x00003840
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005648 File Offset: 0x00003848
		public int Width
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

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005651 File Offset: 0x00003851
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005659 File Offset: 0x00003859
		public int Height
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

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005662 File Offset: 0x00003862
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000566A File Offset: 0x0000386A
		[Browsable(false)]
		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005672 File Offset: 0x00003872
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005681 File Offset: 0x00003881
		[Browsable(false)]
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
		/// <returns>This property returns true if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005690 File Offset: 0x00003890
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.height == 0 && this.width == 0 && this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000160 RID: 352 RVA: 0x000056B5 File Offset: 0x000038B5
		public override bool Equals(object obj)
		{
			return obj is Rectangle && this.Equals((Rectangle)obj);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000056CD File Offset: 0x000038CD
		public bool Equals(Rectangle other)
		{
			return this == other;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000162 RID: 354 RVA: 0x000056DC File Offset: 0x000038DC
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
		/// <returns>This operator returns true if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000163 RID: 355 RVA: 0x0000572B File Offset: 0x0000392B
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !(left == right);
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000164 RID: 356 RVA: 0x00005737 File Offset: 0x00003937
		public bool Contains(int x, int y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000165 RID: 357 RVA: 0x0000576D File Offset: 0x0000396D
		public bool Contains(Point pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000166 RID: 358 RVA: 0x00005784 File Offset: 0x00003984
		public bool Contains(Rectangle rect)
		{
			return this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
		/// <returns>An integer that represents the hash code for this rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000167 RID: 359 RVA: 0x000057F0 File Offset: 0x000039F0
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(this.X, this.Y), this.Width), this.Height);
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
		/// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000168 RID: 360 RVA: 0x00005819 File Offset: 0x00003A19
		public void Inflate(int width, int height)
		{
			this.X -= width;
			this.Y -= height;
			this.Width += 2 * width;
			this.Height += 2 * height;
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
		/// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified. </param>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000169 RID: 361 RVA: 0x00005858 File Offset: 0x00003A58
		public static Rectangle Inflate(Rectangle rect, int x, int y)
		{
			Rectangle rectangle = rect;
			rectangle.Inflate(x, y);
			return rectangle;
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600016A RID: 362 RVA: 0x00005874 File Offset: 0x00003A74
		public void Intersect(Rectangle rect)
		{
			Rectangle rectangle = Rectangle.Intersect(rect, this);
			this.X = rectangle.X;
			this.Y = rectangle.Y;
			this.Width = rectangle.Width;
			this.Height = rectangle.Height;
		}

		/// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
		/// <param name="a">A rectangle to intersect. </param>
		/// <param name="b">A rectangle to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600016B RID: 363 RVA: 0x000058C4 File Offset: 0x00003AC4
		public static Rectangle Intersect(Rectangle a, Rectangle b)
		{
			int num = Math.Max(a.X, b.X);
			int num2 = Math.Min(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Max(a.Y, b.Y);
			int num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
			if (num2 >= num && num4 >= num3)
			{
				return new Rectangle(num, num3, num2 - num, num4 - num3);
			}
			return Rectangle.Empty;
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <returns>This method returns true if there is any intersection, otherwise false.</returns>
		/// <param name="rect">The rectangle to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600016C RID: 364 RVA: 0x0000595C File Offset: 0x00003B5C
		public bool IntersectsWith(Rectangle rect)
		{
			return rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
		/// <param name="a">A rectangle to union. </param>
		/// <param name="b">A rectangle to union. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600016D RID: 365 RVA: 0x000059C8 File Offset: 0x00003BC8
		public static Rectangle Union(Rectangle a, Rectangle b)
		{
			int num = Math.Min(a.X, b.X);
			int num2 = Math.Max(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Min(a.Y, b.Y);
			int num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
			return new Rectangle(num, num3, num2 - num, num4 - num3);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The horizontal offset. </param>
		/// <param name="y">The vertical offset. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600016E RID: 366 RVA: 0x00005A52 File Offset: 0x00003C52
		public void Offset(int x, int y)
		{
			this.X += x;
			this.Y += y;
		}

		/// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50} </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600016F RID: 367 RVA: 0x00005A70 File Offset: 0x00003C70
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

		/// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040000F9 RID: 249
		public static readonly Rectangle Empty;

		// Token: 0x040000FA RID: 250
		private int x;

		// Token: 0x040000FB RID: 251
		private int y;

		// Token: 0x040000FC RID: 252
		private int width;

		// Token: 0x040000FD RID: 253
		private int height;
	}
}
