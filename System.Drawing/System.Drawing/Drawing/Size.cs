using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of integers, which specify a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000023 RID: 35
	[TypeConverter(typeof(SizeConverter))]
	[Serializable]
	public struct Size : IEquatable<Size>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.Size" />. </param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.Size" />. </param>
		// Token: 0x06000183 RID: 387 RVA: 0x00005D41 File Offset: 0x00003F41
		public Size(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.SizeF" /> structure to which this operator converts.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Size" /> structure to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000184 RID: 388 RVA: 0x00005D51 File Offset: 0x00003F51
		public static implicit operator SizeF(Size p)
		{
			return new SizeF((float)p.Width, (float)p.Height);
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> to add. </param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> to add. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000185 RID: 389 RVA: 0x00005D68 File Offset: 0x00003F68
		public static Size operator +(Size sz1, Size sz2)
		{
			return Size.Add(sz1, sz2);
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are equal.</summary>
		/// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the equality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000186 RID: 390 RVA: 0x00005D71 File Offset: 0x00003F71
		public static bool operator ==(Size sz1, Size sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are different.</summary>
		/// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left of the inequality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000187 RID: 391 RVA: 0x00005D95 File Offset: 0x00003F95
		public static bool operator !=(Size sz1, Size sz2)
		{
			return !(sz1 == sz2);
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Size" /> structure has width and height of 0.</summary>
		/// <returns>This property returns true when this <see cref="T:System.Drawing.Size" /> structure has both a width and height of 0; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005DA1 File Offset: 0x00003FA1
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0 && this.height == 0;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005DB6 File Offset: 0x00003FB6
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005DBE File Offset: 0x00003FBE
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005DC7 File Offset: 0x00003FC7
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005DCF File Offset: 0x00003FCF
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

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> structure to add.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static Size Add(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.Size" /> structure to the next higher integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600018E RID: 398 RVA: 0x00005DFD File Offset: 0x00003FFD
		public static Size Ceiling(SizeF value)
		{
			return new Size((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by truncating the values of the <see cref="T:System.Drawing.SizeF" /> structure to the next lower integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600018F RID: 399 RVA: 0x00005E20 File Offset: 0x00004020
		public static Size Truncate(SizeF value)
		{
			return new Size((int)value.Width, (int)value.Height);
		}

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.Size" /> structure with the same dimensions as this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Size" /> and has the same width and height as this <see cref="T:System.Drawing.Size" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000190 RID: 400 RVA: 0x00005E37 File Offset: 0x00004037
		public override bool Equals(object obj)
		{
			return obj is Size && this.Equals((Size)obj);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005E4F File Offset: 0x0000404F
		public bool Equals(Size other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000192 RID: 402 RVA: 0x00005E5D File Offset: 0x0000405D
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.Width, this.Height);
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Size" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000193 RID: 403 RVA: 0x00005E70 File Offset: 0x00004070
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Width=",
				this.width.ToString(),
				", Height=",
				this.height.ToString(),
				"}"
			});
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Size" /> structure that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0. </summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000102 RID: 258
		public static readonly Size Empty;

		// Token: 0x04000103 RID: 259
		private int width;

		// Token: 0x04000104 RID: 260
		private int height;
	}
}
