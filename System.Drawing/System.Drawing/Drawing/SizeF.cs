using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000024 RID: 36
	[TypeConverter(typeof(SizeFConverter))]
	[Serializable]
	public struct SizeF : IEquatable<SizeF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
		// Token: 0x06000194 RID: 404 RVA: 0x00005EBC File Offset: 0x000040BC
		public SizeF(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are equal.</summary>
		/// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the equality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000195 RID: 405 RVA: 0x00005ECC File Offset: 0x000040CC
		public static bool operator ==(SizeF sz1, SizeF sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are different.</summary>
		/// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left of the inequality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000196 RID: 406 RVA: 0x00005EF0 File Offset: 0x000040F0
		public static bool operator !=(SizeF sz1, SizeF sz2)
		{
			return !(sz1 == sz2);
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.SizeF" /> structure has zero width and height.</summary>
		/// <returns>This property returns true when this <see cref="T:System.Drawing.SizeF" /> structure has both a width and height of zero; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005EFC File Offset: 0x000040FC
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0f && this.height == 0f;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005F1A File Offset: 0x0000411A
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005F22 File Offset: 0x00004122
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005F2B File Offset: 0x0000412B
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005F33 File Offset: 0x00004133
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

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.SizeF" /> structure with the same dimensions as this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.SizeF" /> and has the same width and height as this <see cref="T:System.Drawing.SizeF" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600019C RID: 412 RVA: 0x00005F3C File Offset: 0x0000413C
		public override bool Equals(object obj)
		{
			return obj is SizeF && this.Equals((SizeF)obj);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005F54 File Offset: 0x00004154
		public bool Equals(SizeF other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600019E RID: 414 RVA: 0x00005F64 File Offset: 0x00004164
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.Width.GetHashCode(), this.Height.GetHashCode());
		}

		/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>Returns a <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600019F RID: 415 RVA: 0x00005F92 File Offset: 0x00004192
		public Size ToSize()
		{
			return Size.Truncate(this);
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001A0 RID: 416 RVA: 0x00005FA0 File Offset: 0x000041A0
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

		/// <summary>Gets a <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0. </summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000105 RID: 261
		public static readonly SizeF Empty;

		// Token: 0x04000106 RID: 262
		private float width;

		// Token: 0x04000107 RID: 263
		private float height;
	}
}
