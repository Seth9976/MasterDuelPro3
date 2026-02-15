using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015C RID: 348
	[TypeConverter(typeof(PaddingConverter))]
	[Serializable]
	public struct Padding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using the supplied padding size for all edges.</summary>
		/// <param name="all">The number of pixels to be used for padding for all edges.</param>
		// Token: 0x06000D72 RID: 3442 RVA: 0x0003B06E File Offset: 0x0003926E
		public Padding(int all)
		{
			this._left = all;
			this._right = all;
			this._top = all;
			this._bottom = all;
			this._all = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using a separate padding size for each edge.</summary>
		/// <param name="left">The padding size, in pixels, for the left edge.</param>
		/// <param name="top">The padding size, in pixels, for the top edge.</param>
		/// <param name="right">The padding size, in pixels, for the right edge.</param>
		/// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
		// Token: 0x06000D73 RID: 3443 RVA: 0x0003B094 File Offset: 0x00039294
		public Padding(int left, int top, int right, int bottom)
		{
			this._left = left;
			this._right = right;
			this._top = top;
			this._bottom = bottom;
			this._all = this._left == this._top && this._left == this._right && this._left == this._bottom;
		}

		/// <summary>Gets or sets the padding value for all the edges.</summary>
		/// <returns>The padding, in pixels, for all edges if the same; otherwise, -1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0003B0F1 File Offset: 0x000392F1
		[RefreshProperties(RefreshProperties.All)]
		public int All
		{
			get
			{
				if (!this._all)
				{
					return -1;
				}
				return this._top;
			}
		}

		/// <summary>Gets or sets the padding value for the bottom edge.</summary>
		/// <returns>The padding, in pixels, for the bottom edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0003B103 File Offset: 0x00039303
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0003B10B File Offset: 0x0003930B
		[RefreshProperties(RefreshProperties.All)]
		public int Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				this._bottom = value;
				this._all = false;
			}
		}

		/// <summary>Gets the combined padding for the right and left edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left" /> and <see cref="P:System.Windows.Forms.Padding.Right" /> padding values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0003B11B File Offset: 0x0003931B
		[Browsable(false)]
		public int Horizontal
		{
			get
			{
				return this._left + this._right;
			}
		}

		/// <summary>Gets or sets the padding value for the left edge.</summary>
		/// <returns>The padding, in pixels, for the left edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003B12A File Offset: 0x0003932A
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0003B132 File Offset: 0x00039332
		[RefreshProperties(RefreshProperties.All)]
		public int Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
				this._all = false;
			}
		}

		/// <summary>Gets or sets the padding value for the right edge.</summary>
		/// <returns>The padding, in pixels, for the right edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0003B142 File Offset: 0x00039342
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0003B14A File Offset: 0x0003934A
		[RefreshProperties(RefreshProperties.All)]
		public int Right
		{
			get
			{
				return this._right;
			}
			set
			{
				this._right = value;
				this._all = false;
			}
		}

		/// <summary>Gets or sets the padding value for the top edge.</summary>
		/// <returns>The padding, in pixels, for the top edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003B15A File Offset: 0x0003935A
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0003B162 File Offset: 0x00039362
		[RefreshProperties(RefreshProperties.All)]
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this._top = value;
				this._all = false;
			}
		}

		/// <summary>Gets the combined padding for the top and bottom edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top" /> and <see cref="P:System.Windows.Forms.Padding.Bottom" /> padding values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0003B172 File Offset: 0x00039372
		[Browsable(false)]
		public int Vertical
		{
			get
			{
				return this._top + this._bottom;
			}
		}

		/// <summary>Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent; otherwise, false.</returns>
		/// <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D7F RID: 3455 RVA: 0x0003B184 File Offset: 0x00039384
		public override bool Equals(object other)
		{
			if (other is Padding)
			{
				Padding padding = (Padding)other;
				return this._left == padding.Left && this._top == padding.Top && this._right == padding.Right && this._bottom == padding.Bottom;
			}
			return false;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding" />. </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D80 RID: 3456 RVA: 0x0003B1E0 File Offset: 0x000393E0
		public override int GetHashCode()
		{
			return this._top ^ this._bottom ^ this._left ^ this._right;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are not equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are different; otherwise, false.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003B1FD File Offset: 0x000393FD
		public static bool operator !=(Padding p1, Padding p2)
		{
			return !p1.Equals(p2);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Padding" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D82 RID: 3458 RVA: 0x0003B218 File Offset: 0x00039418
		public override string ToString()
		{
			return string.Concat(new object[] { "{Left=", this.Left, ",Top=", this.Top, ",Right=", this.Right, ",Bottom=", this.Bottom, "}" });
		}

		// Token: 0x04000879 RID: 2169
		private int _bottom;

		// Token: 0x0400087A RID: 2170
		private int _left;

		// Token: 0x0400087B RID: 2171
		private int _right;

		// Token: 0x0400087C RID: 2172
		private int _top;

		// Token: 0x0400087D RID: 2173
		private bool _all;

		/// <summary>Provides a <see cref="T:System.Windows.Forms.Padding" /> object with no padding.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400087E RID: 2174
		public static readonly Padding Empty = new Padding(0);
	}
}
