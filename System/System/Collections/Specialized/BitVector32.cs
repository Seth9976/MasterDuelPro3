using System;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Provides a simple structure that stores Boolean values and small integers in 32 bits of memory.</summary>
	// Token: 0x020002F9 RID: 761
	public struct BitVector32
	{
		/// <summary>Gets or sets the state of the bit flag indicated by the specified mask.</summary>
		/// <returns>true if the specified bit flag is on (1); otherwise, false.</returns>
		/// <param name="bit">A mask that indicates the bit to get or set. </param>
		// Token: 0x170003CE RID: 974
		public bool this[int bit]
		{
			get
			{
				return ((ulong)this._data & (ulong)((long)bit)) == (ulong)bit;
			}
			set
			{
				if (value)
				{
					this._data |= (uint)bit;
					return;
				}
				this._data &= (uint)(~(uint)bit);
			}
		}

		/// <summary>Creates the first mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the first bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001256 RID: 4694 RVA: 0x00052FF0 File Offset: 0x000511F0
		public static int CreateMask()
		{
			return BitVector32.CreateMask(0);
		}

		/// <summary>Creates an additional mask following the specified mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the bit flag following the one that <paramref name="previous" /> points to in <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="previous">The mask that indicates the previous bit flag. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> indicates the last bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />. </exception>
		// Token: 0x06001257 RID: 4695 RVA: 0x00052FF8 File Offset: 0x000511F8
		public static int CreateMask(int previous)
		{
			if (previous == 0)
			{
				return 1;
			}
			if (previous == -2147483648)
			{
				throw new InvalidOperationException("Bit vector is full.");
			}
			return previous << 1;
		}

		/// <summary>Determines whether the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>true if the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		// Token: 0x06001258 RID: 4696 RVA: 0x00053015 File Offset: 0x00051215
		public override bool Equals(object o)
		{
			return o is BitVector32 && this._data == ((BitVector32)o)._data;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A hash code for the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x06001259 RID: 4697 RVA: 0x00053034 File Offset: 0x00051234
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32" /> to represent. </param>
		// Token: 0x0600125A RID: 4698 RVA: 0x00053048 File Offset: 0x00051248
		public static string ToString(BitVector32 value)
		{
			StringBuilder stringBuilder = new StringBuilder(45);
			stringBuilder.Append("BitVector32{");
			int num = (int)value._data;
			for (int i = 0; i < 32; i++)
			{
				if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					stringBuilder.Append('1');
				}
				else
				{
					stringBuilder.Append('0');
				}
				num <<= 1;
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x0600125B RID: 4699 RVA: 0x000530AF File Offset: 0x000512AF
		public override string ToString()
		{
			return BitVector32.ToString(this);
		}

		// Token: 0x04000B4D RID: 2893
		private uint _data;

		/// <summary>Represents a section of the vector that can contain an integer number.</summary>
		// Token: 0x020002FA RID: 762
		public readonly struct Section
		{
			/// <summary>Gets a mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>A mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x170003CF RID: 975
			// (get) Token: 0x0600125C RID: 4700 RVA: 0x000530BC File Offset: 0x000512BC
			public short Mask
			{
				get
				{
					return this._mask;
				}
			}

			/// <summary>Gets the offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>The offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x0600125D RID: 4701 RVA: 0x000530C4 File Offset: 0x000512C4
			public short Offset
			{
				get
				{
					return this._offset;
				}
			}

			/// <summary>Determines whether the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise, false.</returns>
			/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
			// Token: 0x0600125E RID: 4702 RVA: 0x000530CC File Offset: 0x000512CC
			public override bool Equals(object o)
			{
				return o is BitVector32.Section && this.Equals((BitVector32.Section)o);
			}

			/// <summary>Determines whether the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the <paramref name="obj" /> parameter is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise false.</returns>
			/// <param name="obj">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x0600125F RID: 4703 RVA: 0x000530E4 File Offset: 0x000512E4
			public bool Equals(BitVector32.Section obj)
			{
				return obj._mask == this._mask && obj._offset == this._offset;
			}

			/// <summary>Serves as a hash function for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
			/// <returns>A hash code for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x06001260 RID: 4704 RVA: 0x00053104 File Offset: 0x00051304
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> to represent.</param>
			// Token: 0x06001261 RID: 4705 RVA: 0x00053118 File Offset: 0x00051318
			public static string ToString(BitVector32.Section value)
			{
				return string.Concat(new string[]
				{
					"Section{0x",
					Convert.ToString(value.Mask, 16),
					", 0x",
					Convert.ToString(value.Offset, 16),
					"}"
				});
			}

			/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x06001262 RID: 4706 RVA: 0x0005316A File Offset: 0x0005136A
			public override string ToString()
			{
				return BitVector32.Section.ToString(this);
			}

			// Token: 0x04000B4E RID: 2894
			private readonly short _mask;

			// Token: 0x04000B4F RID: 2895
			private readonly short _offset;
		}
	}
}
