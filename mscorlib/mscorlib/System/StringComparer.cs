using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	/// <summary>Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000145 RID: 325
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00031D78 File Offset: 0x0002FF78
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer.s_invariantCulture;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00031D7F File Offset: 0x0002FF7F
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer.s_invariantCultureIgnoreCase;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00031D86 File Offset: 0x0002FF86
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.None);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs case-insensitive string comparisons using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00031D93 File Offset: 0x0002FF93
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00031DA0 File Offset: 0x0002FFA0
		public static StringComparer Ordinal
		{
			get
			{
				return StringComparer.s_ordinal;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00031DA7 File Offset: 0x0002FFA7
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer.s_ordinalIgnoreCase;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		public static StringComparer FromComparison(StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return StringComparer.CurrentCulture;
			case StringComparison.CurrentCultureIgnoreCase:
				return StringComparer.CurrentCultureIgnoreCase;
			case StringComparison.InvariantCulture:
				return StringComparer.InvariantCulture;
			case StringComparison.InvariantCultureIgnoreCase:
				return StringComparer.InvariantCultureIgnoreCase;
			case StringComparison.Ordinal:
				return StringComparer.Ordinal;
			case StringComparison.OrdinalIgnoreCase:
				return StringComparer.OrdinalIgnoreCase;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Creates a <see cref="T:System.StringComparer" /> object that compares strings according to the rules of a specified culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object that performs string comparisons according to the comparison rules used by the <paramref name="culture" /> parameter and the case rule specified by the <paramref name="ignoreCase" /> parameter.</returns>
		/// <param name="culture">A culture whose linguistic rules are used to perform a string comparison.</param>
		/// <param name="ignoreCase">true to specify that comparison operations be case-insensitive; false to specify that comparison operations be case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B12 RID: 2834 RVA: 0x00031E10 File Offset: 0x00030010
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		/// <summary>When overridden in a derived class, compares two objects and returns an indication of their relative sort order.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />. -or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> is a <see cref="T:System.String" /> object, and neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B13 RID: 2835 RVA: 0x00031E30 File Offset: 0x00030030
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		/// <summary>When overridden in a derived class, indicates whether two objects are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are both the same type of object and those objects are equal; otherwise, false.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B14 RID: 2836 RVA: 0x00031E88 File Offset: 0x00030088
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		/// <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">An object.</param>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B15 RID: 2837 RVA: 0x00031EC8 File Offset: 0x000300C8
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		/// <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />.-or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B16 RID: 2838
		public abstract int Compare(string x, string y);

		/// <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B17 RID: 2839
		public abstract bool Equals(string x, string y);

		/// <summary>When overridden in a derived class, gets the hash code for the specified string.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">A string.</param>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B18 RID: 2840
		public abstract int GetHashCode(string obj);

		// Token: 0x04000482 RID: 1154
		private static readonly CultureAwareComparer s_invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.None);

		// Token: 0x04000483 RID: 1155
		private static readonly CultureAwareComparer s_invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);

		// Token: 0x04000484 RID: 1156
		private static readonly OrdinalCaseSensitiveComparer s_ordinal = new OrdinalCaseSensitiveComparer();

		// Token: 0x04000485 RID: 1157
		private static readonly OrdinalIgnoreCaseComparer s_ordinalIgnoreCase = new OrdinalIgnoreCaseComparer();
	}
}
