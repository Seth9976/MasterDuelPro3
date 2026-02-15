using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents an immutable regular expression.</summary>
	// Token: 0x02000130 RID: 304
	public class Regex : ISerializable
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0001D400 File Offset: 0x0001B600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Regex.CachedCodeEntry GetCachedCode(Regex.CachedCodeEntryKey key, bool isToAdd)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			if (cachedCodeEntry != null && cachedCodeEntry.Key == key)
			{
				return cachedCodeEntry;
			}
			if (Regex.s_cacheSize == 0)
			{
				return null;
			}
			return this.GetCachedCodeEntryInternal(key, isToAdd);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001D438 File Offset: 0x0001B638
		private Regex.CachedCodeEntry GetCachedCodeEntryInternal(Regex.CachedCodeEntryKey key, bool isToAdd)
		{
			Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry> dictionary = Regex.s_cache;
			Regex.CachedCodeEntry cachedCodeEntry3;
			lock (dictionary)
			{
				Regex.CachedCodeEntry cachedCodeEntry = Regex.LookupCachedAndPromote(key);
				if (cachedCodeEntry == null && isToAdd && Regex.s_cacheSize != 0)
				{
					cachedCodeEntry = new Regex.CachedCodeEntry(key, this.capnames, this.capslist, this._code, this.caps, this.capsize, this._runnerref, this._replref);
					if (Regex.s_cacheFirst != null)
					{
						Regex.s_cacheFirst.Next = cachedCodeEntry;
						cachedCodeEntry.Previous = Regex.s_cacheFirst;
					}
					Regex.s_cacheFirst = cachedCodeEntry;
					Regex.s_cacheCount++;
					if (Regex.s_cacheCount >= 10)
					{
						if (Regex.s_cacheCount == 10)
						{
							this.FillCacheDictionary();
						}
						else
						{
							Regex.s_cache.Add(key, cachedCodeEntry);
						}
					}
					if (Regex.s_cacheLast == null)
					{
						Regex.s_cacheLast = cachedCodeEntry;
					}
					else if (Regex.s_cacheCount > Regex.s_cacheSize)
					{
						Regex.CachedCodeEntry cachedCodeEntry2 = Regex.s_cacheLast;
						if (Regex.s_cacheCount >= 10)
						{
							Regex.s_cache.Remove(cachedCodeEntry2.Key);
						}
						cachedCodeEntry2.Next.Previous = null;
						Regex.s_cacheLast = cachedCodeEntry2.Next;
						Regex.s_cacheCount--;
					}
				}
				cachedCodeEntry3 = cachedCodeEntry;
			}
			return cachedCodeEntry3;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001D584 File Offset: 0x0001B784
		private void FillCacheDictionary()
		{
			Regex.s_cache.Clear();
			for (Regex.CachedCodeEntry previous = Regex.s_cacheFirst; previous != null; previous = previous.Previous)
			{
				Regex.s_cache.Add(previous.Key, previous);
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001D5BE File Offset: 0x0001B7BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryGetCacheValue(Regex.CachedCodeEntryKey key, out Regex.CachedCodeEntry entry)
		{
			if (Regex.s_cacheCount >= 10)
			{
				return Regex.s_cache.TryGetValue(key, out entry);
			}
			return Regex.TryGetCacheValueSmall(key, out entry);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001D5DD File Offset: 0x0001B7DD
		private static bool TryGetCacheValueSmall(Regex.CachedCodeEntryKey key, out Regex.CachedCodeEntry entry)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			for (entry = ((cachedCodeEntry != null) ? cachedCodeEntry.Previous : null); entry != null; entry = entry.Previous)
			{
				if (entry.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001D614 File Offset: 0x0001B814
		private static Regex.CachedCodeEntry LookupCachedAndPromote(Regex.CachedCodeEntryKey key)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			if (cachedCodeEntry != null && cachedCodeEntry.Key == key)
			{
				return Regex.s_cacheFirst;
			}
			Regex.CachedCodeEntry cachedCodeEntry2;
			if (Regex.TryGetCacheValue(key, out cachedCodeEntry2))
			{
				if (Regex.s_cacheLast == cachedCodeEntry2)
				{
					Regex.s_cacheLast = cachedCodeEntry2.Next;
				}
				else
				{
					cachedCodeEntry2.Previous.Next = cachedCodeEntry2.Next;
				}
				cachedCodeEntry2.Next.Previous = cachedCodeEntry2.Previous;
				Regex.s_cacheFirst.Next = cachedCodeEntry2;
				cachedCodeEntry2.Previous = Regex.s_cacheFirst;
				cachedCodeEntry2.Next = null;
				Regex.s_cacheFirst = cachedCodeEntry2;
			}
			return cachedCodeEntry2;
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null. </exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600060B RID: 1547 RVA: 0x0001D6A5 File Offset: 0x0001B8A5
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options and time-out interval.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x0600060C RID: 1548 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		public static bool IsMatch(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).IsMatch(input);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in a specified input string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600060D RID: 1549 RVA: 0x0001D6C5 File Offset: 0x0001B8C5
		public bool IsMatch(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.IsMatch(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the specified input string, beginning at the specified starting position in the string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600060E RID: 1550 RVA: 0x0001D6ED File Offset: 0x0001B8ED
		public bool IsMatch(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(true, -1, input, 0, input.Length, startat) == null;
		}

		/// <summary>Searches the specified input string for the first occurrence of the specified regular expression.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600060F RID: 1551 RVA: 0x0001D711 File Offset: 0x0001B911
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000610 RID: 1552 RVA: 0x0001D720 File Offset: 0x0001B920
		public static Match Match(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Match(input);
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000611 RID: 1553 RVA: 0x0001D731 File Offset: 0x0001B931
		public Match Match(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Match(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The zero-based character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000612 RID: 1554 RVA: 0x0001D759 File Offset: 0x0001B959
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, 0, input.Length, startat);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position in the input string at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000613 RID: 1555 RVA: 0x0001D77A File Offset: 0x0001B97A
		public MatchCollection Matches(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return new MatchCollection(this, input, 0, input.Length, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000614 RID: 1556 RVA: 0x0001D799 File Offset: 0x0001B999
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000615 RID: 1557 RVA: 0x0001D7A9 File Offset: 0x0001B9A9
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			return Regex.Replace(input, pattern, replacement, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000616 RID: 1558 RVA: 0x0001D7B9 File Offset: 0x0001B9B9
		public static string Replace(string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, replacement);
		}

		/// <summary>In a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000617 RID: 1559 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		public string Replace(string input, string replacement)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">The maximum number of times the replacement can occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000618 RID: 1560 RVA: 0x0001D7F6 File Offset: 0x0001B9F6
		public string Replace(string input, string replacement, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">Maximum number of times the replacement can occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000619 RID: 1561 RVA: 0x0001D820 File Offset: 0x0001BA20
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			return RegexReplacement.GetOrCreate(this._replref, replacement, this.caps, this.capsize, this.capnames, this.roptions).Replace(this, input, count, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600061A RID: 1562 RVA: 0x0001D877 File Offset: 0x0001BA77
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all substrings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x0600061B RID: 1563 RVA: 0x0001D887 File Offset: 0x0001BA87
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, evaluator);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600061C RID: 1564 RVA: 0x0001D89A File Offset: 0x0001BA9A
		public string Replace(string input, MatchEvaluator evaluator)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600061D RID: 1565 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Regex.Replace(evaluator, this, input, count, startat);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		private static string Replace(MatchEvaluator evaluator, Regex regex, string input, int count, int startat)
		{
			if (evaluator == null)
			{
				throw new ArgumentNullException("evaluator");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					stringBuilder.Append(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					list.Add(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600061F RID: 1567 RVA: 0x0001DA78 File Offset: 0x0001BC78
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A string array.</returns>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000620 RID: 1568 RVA: 0x0001DA87 File Offset: 0x0001BC87
		public static string[] Split(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Split(input);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000621 RID: 1569 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public string[] Split(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Split(input, 0, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor. The search for the regular expression pattern starts at a specified character position in the input string.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to be split. </param>
		/// <param name="count">The maximum number of times the split can occur. </param>
		/// <param name="startat">The character position in the input string where the search will begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000622 RID: 1570 RVA: 0x0001DAC1 File Offset: 0x0001BCC1
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Regex.Split(this, input, count, startat);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001DADC File Offset: 0x0001BCDC
		private static string[] Split(Regex regex, string input, int count, int startat)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 1)
			{
				return new string[] { input };
			}
			count--;
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return new string[] { input };
			}
			List<string> list = new List<string>();
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					list.Add(input.Substring(num, match.Index - num));
					num = match.Index + match.Length;
					for (int i = 1; i < match.Groups.Count; i++)
					{
						if (match.IsMatched(i))
						{
							list.Add(match.Groups[i].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(num, input.Length - num));
			}
			else
			{
				int num2 = input.Length;
				do
				{
					list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					num2 = match.Index;
					for (int j = 1; j < match.Groups.Count; j++)
					{
						if (match.IsMatched(j))
						{
							list.Add(match.Groups[j].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(0, num2));
				list.Reverse(0, list.Count);
			}
			return list.ToArray();
		}

		/// <summary>Gets the time-out interval of the current instance.</summary>
		/// <returns>The maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.internalMatchTimeout;
			}
		}

		/// <summary>Checks whether a time-out interval is within an acceptable range.</summary>
		/// <param name="matchTimeout">The time-out interval to check.</param>
		// Token: 0x06000626 RID: 1574 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		protected internal static void ValidateMatchTimeout(TimeSpan matchTimeout)
		{
			if (Regex.InfiniteMatchTimeout == matchTimeout)
			{
				return;
			}
			if (TimeSpan.Zero < matchTimeout && matchTimeout <= Regex.s_maximumMatchTimeout)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("matchTimeout");
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001DD28 File Offset: 0x0001BF28
		private static TimeSpan InitDefaultMatchTimeout()
		{
			object data = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			if (data == null)
			{
				return Regex.InfiniteMatchTimeout;
			}
			if (data is TimeSpan)
			{
				TimeSpan timeSpan = (TimeSpan)data;
				try
				{
					Regex.ValidateMatchTimeout(timeSpan);
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new ArgumentOutOfRangeException(SR.Format("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", "REGEX_DEFAULT_MATCH_TIMEOUT", timeSpan));
				}
				return timeSpan;
			}
			throw new InvalidCastException(SR.Format("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", "REGEX_DEFAULT_MATCH_TIMEOUT", data));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		// Token: 0x06000628 RID: 1576 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public Regex(string pattern)
			: this(pattern, RegexOptions.None, Regex.s_defaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> contains an invalid flag.</exception>
		// Token: 0x06000629 RID: 1577 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public Regex(string pattern, RegexOptions options)
			: this(pattern, options, Regex.s_defaultMatchTimeout, false)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.RegularExpressions.Regex" /> object.</summary>
		/// <param name="si">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. This parameter is reserved for future use.</param>
		// Token: 0x0600062A RID: 1578 RVA: 0x0000D54C File Offset: 0x0000B74C
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		private Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, bool addToCache)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (options < RegexOptions.None || options >> 10 != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			Regex.ValidateMatchTimeout(matchTimeout);
			this.pattern = pattern;
			this.roptions = options;
			this.internalMatchTimeout = matchTimeout;
			string text = (((options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture.ToString() : CultureInfo.CurrentCulture.ToString());
			Regex.CachedCodeEntryKey cachedCodeEntryKey = new Regex.CachedCodeEntryKey(options, text, pattern);
			Regex.CachedCodeEntry cachedCodeEntry = this.GetCachedCode(cachedCodeEntryKey, false);
			if (cachedCodeEntry == null)
			{
				RegexTree regexTree = RegexParser.Parse(pattern, this.roptions);
				this.capnames = regexTree.CapNames;
				this.capslist = regexTree.CapsList;
				this._code = RegexWriter.Write(regexTree);
				this.caps = this._code.Caps;
				this.capsize = this._code.CapSize;
				this.InitializeReferences();
				if (addToCache)
				{
					cachedCodeEntry = this.GetCachedCode(cachedCodeEntryKey, true);
				}
			}
			else
			{
				this.caps = cachedCodeEntry.Caps;
				this.capnames = cachedCodeEntry.Capnames;
				this.capslist = cachedCodeEntry.Capslist;
				this.capsize = cachedCodeEntry.Capsize;
				this._code = cachedCodeEntry.Code;
				this.factory = cachedCodeEntry.Factory;
				this._runnerref = cachedCodeEntry.Runnerref;
				this._replref = cachedCodeEntry.ReplRef;
				this._refsInitialized = true;
			}
			if (this.UseOptionC() && this.factory == null)
			{
				this.factory = this.Compile(this._code, this.roptions);
				if (addToCache && cachedCodeEntry != null)
				{
					cachedCodeEntry.AddCompiled(this.factory);
				}
				this._code = null;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001DF7F File Offset: 0x0001C17F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RegexRunnerFactory Compile(RegexCode code, RegexOptions roptions)
		{
			return RegexCompiler.Compile(code, roptions);
		}

		/// <summary>Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $,., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.</summary>
		/// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
		/// <param name="str">The input string that contains the text to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001DF88 File Offset: 0x0001C188
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Escape(str);
		}

		/// <summary>Gets the options that were passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>One or more members of the <see cref="T:System.Text.RegularExpressions.RegexOptions" /> enumeration that represent options that were passed to the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor </returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001DF9E File Offset: 0x0001C19E
		public RegexOptions Options
		{
			get
			{
				return this.roptions;
			}
		}

		/// <summary>Gets a value that indicates whether the regular expression searches from right to left.</summary>
		/// <returns>true if the regular expression searches from right to left; otherwise, false.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001DFA6 File Offset: 0x0001C1A6
		public bool RightToLeft
		{
			get
			{
				return this.UseOptionR();
			}
		}

		/// <summary>Returns the regular expression pattern that was passed into the Regex constructor.</summary>
		/// <returns>The <paramref name="pattern" /> parameter that was passed into the Regex constructor.</returns>
		// Token: 0x06000630 RID: 1584 RVA: 0x0001DFAE File Offset: 0x0001C1AE
		public override string ToString()
		{
			return this.pattern;
		}

		/// <summary>Gets the group name that corresponds to the specified group number.</summary>
		/// <returns>A string that contains the group name associated with the specified group number. If there is no group name that corresponds to <paramref name="i" />, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="i">The group number to convert to the corresponding group name. </param>
		// Token: 0x06000631 RID: 1585 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public string GroupNameFromNumber(int i)
		{
			if (this.capslist == null)
			{
				if (i >= 0 && i < this.capsize)
				{
					return i.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if (this.caps != null && !this.caps.TryGetValue(i, out i))
				{
					return string.Empty;
				}
				if (i >= 0 && i < this.capslist.Length)
				{
					return this.capslist[i];
				}
				return string.Empty;
			}
		}

		/// <summary>Returns the group number that corresponds to the specified group name.</summary>
		/// <returns>The group number that corresponds to the specified group name, or -1 if <paramref name="name" /> is not a valid group name.</returns>
		/// <param name="name">The group name to convert to the corresponding group number. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000632 RID: 1586 RVA: 0x0001E030 File Offset: 0x0001C230
		public int GroupNumberFromName(string name)
		{
			int num = -1;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.capnames != null)
			{
				if (!this.capnames.TryGetValue(name, out num))
				{
					return -1;
				}
				return num;
			}
			else
			{
				num = 0;
				foreach (char c in name)
				{
					if (c > '9' || c < '0')
					{
						return -1;
					}
					num *= 10;
					num += (int)(c - '0');
				}
				if (num >= 0 && num < this.capsize)
				{
					return num;
				}
				return -1;
			}
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		/// <exception cref="T:System.NotSupportedException">References have already been initialized. </exception>
		// Token: 0x06000633 RID: 1587 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		protected void InitializeReferences()
		{
			if (this._refsInitialized)
			{
				throw new NotSupportedException("This operation is only allowed once per object.");
			}
			this._refsInitialized = true;
			this._runnerref = new ExclusiveReference();
			this._replref = new WeakReference<RegexReplacement>(null);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		internal Match Run(bool quick, int prevlen, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (length < 0 || length > input.Length)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than 0 or exceed input length.");
			}
			RegexRunner regexRunner = this._runnerref.Get();
			if (regexRunner == null)
			{
				if (this.factory != null)
				{
					regexRunner = this.factory.CreateInstance();
				}
				else
				{
					regexRunner = new RegexInterpreter(this._code, this.UseOptionInvariant() ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
				}
			}
			Match match;
			try
			{
				match = regexRunner.Scan(this, input, beginning, beginning + length, startat, prevlen, quick, this.internalMatchTimeout);
			}
			finally
			{
				this._runnerref.Release(regexRunner);
			}
			return match;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> option; otherwise, false.</returns>
		// Token: 0x06000635 RID: 1589 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		protected bool UseOptionC()
		{
			return Environment.GetEnvironmentVariable("MONO_REGEX_COMPILED_ENABLE") != null && (this.roptions & RegexOptions.Compiled) > RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option; otherwise, false.</returns>
		// Token: 0x06000636 RID: 1590 RVA: 0x0001E1C7 File Offset: 0x0001C3C7
		protected internal bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001E1D5 File Offset: 0x0001C3D5
		internal bool UseOptionInvariant()
		{
			return (this.roptions & RegexOptions.CultureInvariant) > RegexOptions.None;
		}

		// Token: 0x040004E6 RID: 1254
		private const int CacheDictionarySwitchLimit = 10;

		// Token: 0x040004E7 RID: 1255
		private static int s_cacheSize = 15;

		// Token: 0x040004E8 RID: 1256
		private static readonly Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry> s_cache = new Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry>(Regex.s_cacheSize);

		// Token: 0x040004E9 RID: 1257
		private static int s_cacheCount = 0;

		// Token: 0x040004EA RID: 1258
		private static Regex.CachedCodeEntry s_cacheFirst;

		// Token: 0x040004EB RID: 1259
		private static Regex.CachedCodeEntry s_cacheLast;

		// Token: 0x040004EC RID: 1260
		private static readonly TimeSpan s_maximumMatchTimeout = TimeSpan.FromMilliseconds(2147483646.0);

		// Token: 0x040004ED RID: 1261
		private const string DefaultMatchTimeout_ConfigKeyName = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x040004EE RID: 1262
		internal static readonly TimeSpan s_defaultMatchTimeout = Regex.InitDefaultMatchTimeout();

		/// <summary>Specifies that a pattern-matching operation should not time out.</summary>
		// Token: 0x040004EF RID: 1263
		public static readonly TimeSpan InfiniteMatchTimeout = Timeout.InfiniteTimeSpan;

		/// <summary>The maximum amount of time that can elapse in a pattern-matching operation before the operation times out.</summary>
		// Token: 0x040004F0 RID: 1264
		protected internal TimeSpan internalMatchTimeout;

		// Token: 0x040004F1 RID: 1265
		internal const int MaxOptionShift = 10;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F2 RID: 1266
		protected internal string pattern;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F3 RID: 1267
		protected internal RegexOptions roptions;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F4 RID: 1268
		protected internal RegexRunnerFactory factory;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F5 RID: 1269
		protected internal Hashtable caps;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F6 RID: 1270
		protected internal Hashtable capnames;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F7 RID: 1271
		protected internal string[] capslist;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x040004F8 RID: 1272
		protected internal int capsize;

		// Token: 0x040004F9 RID: 1273
		internal ExclusiveReference _runnerref;

		// Token: 0x040004FA RID: 1274
		internal WeakReference<RegexReplacement> _replref;

		// Token: 0x040004FB RID: 1275
		internal RegexCode _code;

		// Token: 0x040004FC RID: 1276
		internal bool _refsInitialized;

		// Token: 0x02000131 RID: 305
		internal readonly struct CachedCodeEntryKey : IEquatable<Regex.CachedCodeEntryKey>
		{
			// Token: 0x06000638 RID: 1592 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
			public CachedCodeEntryKey(RegexOptions options, string cultureKey, string pattern)
			{
				this._options = options;
				this._cultureKey = cultureKey;
				this._pattern = pattern;
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0001E1FD File Offset: 0x0001C3FD
			public override bool Equals(object obj)
			{
				return obj is Regex.CachedCodeEntryKey && this.Equals((Regex.CachedCodeEntryKey)obj);
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x0001E215 File Offset: 0x0001C415
			public bool Equals(Regex.CachedCodeEntryKey other)
			{
				return this._pattern.Equals(other._pattern) && this._options == other._options && this._cultureKey.Equals(other._cultureKey);
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x0001E24B File Offset: 0x0001C44B
			public static bool operator ==(Regex.CachedCodeEntryKey left, Regex.CachedCodeEntryKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x0001E255 File Offset: 0x0001C455
			public override int GetHashCode()
			{
				return (int)(this._options ^ (RegexOptions)this._cultureKey.GetHashCode() ^ (RegexOptions)this._pattern.GetHashCode());
			}

			// Token: 0x040004FD RID: 1277
			private readonly RegexOptions _options;

			// Token: 0x040004FE RID: 1278
			private readonly string _cultureKey;

			// Token: 0x040004FF RID: 1279
			private readonly string _pattern;
		}

		// Token: 0x02000132 RID: 306
		internal sealed class CachedCodeEntry
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0001E278 File Offset: 0x0001C478
			public CachedCodeEntry(Regex.CachedCodeEntryKey key, Hashtable capnames, string[] capslist, RegexCode code, Hashtable caps, int capsize, ExclusiveReference runner, WeakReference<RegexReplacement> replref)
			{
				this.Key = key;
				this.Capnames = capnames;
				this.Capslist = capslist;
				this.Code = code;
				this.Caps = caps;
				this.Capsize = capsize;
				this.Runnerref = runner;
				this.ReplRef = replref;
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
			public void AddCompiled(RegexRunnerFactory factory)
			{
				this.Factory = factory;
				this.Code = null;
			}

			// Token: 0x04000500 RID: 1280
			public Regex.CachedCodeEntry Next;

			// Token: 0x04000501 RID: 1281
			public Regex.CachedCodeEntry Previous;

			// Token: 0x04000502 RID: 1282
			public readonly Regex.CachedCodeEntryKey Key;

			// Token: 0x04000503 RID: 1283
			public RegexCode Code;

			// Token: 0x04000504 RID: 1284
			public readonly Hashtable Caps;

			// Token: 0x04000505 RID: 1285
			public readonly Hashtable Capnames;

			// Token: 0x04000506 RID: 1286
			public readonly string[] Capslist;

			// Token: 0x04000507 RID: 1287
			public RegexRunnerFactory Factory;

			// Token: 0x04000508 RID: 1288
			public readonly int Capsize;

			// Token: 0x04000509 RID: 1289
			public readonly ExclusiveReference Runnerref;

			// Token: 0x0400050A RID: 1290
			public readonly WeakReference<RegexReplacement> ReplRef;
		}
	}
}
