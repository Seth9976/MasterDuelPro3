using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Contains information about the country/region.</summary>
	// Token: 0x020006D0 RID: 1744
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RegionInfo
	{
		/// <summary>Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</returns>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000DA930 File Offset: 0x000D8B30
		public static RegionInfo CurrentRegion
		{
			get
			{
				RegionInfo regionInfo = RegionInfo.currentRegion;
				if (regionInfo == null)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (currentCulture != null)
					{
						regionInfo = new RegionInfo(currentCulture);
					}
					if (Interlocked.CompareExchange<RegionInfo>(ref RegionInfo.currentRegion, regionInfo, null) != null)
					{
						regionInfo = RegionInfo.currentRegion;
					}
				}
				return regionInfo;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region associated with the specified culture identifier.</summary>
		/// <param name="culture">A culture identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
		// Token: 0x06003764 RID: 14180 RVA: 0x000DA96B File Offset: 0x000D8B6B
		public RegionInfo(int culture)
		{
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(culture)))
			{
				throw new ArgumentException(string.Format("Region ID {0} (0x{0:X4}) is not a supported region.", culture), "culture");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region or specific culture, specified by name.</summary>
		/// <param name="name">A string that contains a two-letter code defined in ISO 3166 for country/region.-or-A string that contains the culture name for a specific culture, custom culture, or Windows-only culture. If the culture name is not in RFC 4646 format, your application should specify the entire culture name instead of just the country/region. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
		// Token: 0x06003765 RID: 14181 RVA: 0x000DA99C File Offset: 0x000D8B9C
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			if (this.construct_internal_region_from_name(name.ToUpperInvariant()))
			{
				return;
			}
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(name)))
			{
				throw new ArgumentException(string.Format("Region name {0} is not supported.", name), "name");
			}
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000DA9EC File Offset: 0x000D8BEC
		private RegionInfo(CultureInfo ci)
		{
			if (ci.LCID == 127)
			{
				this.regionId = 244;
				this.iso2Name = "IV";
				this.iso3Name = "ivc";
				this.win3Name = "IVC";
				this.nativeName = (this.englishName = "Invariant Country");
				this.currencySymbol = "¤";
				this.isoCurrencySymbol = "XDR";
				this.currencyEnglishName = (this.currencyNativeName = "International Monetary Fund");
				return;
			}
			if (ci.Territory == null)
			{
				throw new NotImplementedException("Neutral region info");
			}
			this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000DAA99 File Offset: 0x000D8C99
		private bool GetByTerritory(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new Exception("INTERNAL ERROR: should not happen.");
			}
			return !ci.IsNeutralCulture && ci.Territory != null && this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06003768 RID: 14184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_region_from_name(string name);

		/// <summary>Gets the name, in English, of the currency used in the country/region.</summary>
		/// <returns>The name, in English, of the currency used in the country/region.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x000DAACC File Offset: 0x000D8CCC
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this.currencyEnglishName;
			}
		}

		/// <summary>Gets the currency symbol associated with the country/region.</summary>
		/// <returns>The currency symbol associated with the country/region.</returns>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000DAAD4 File Offset: 0x000D8CD4
		public virtual string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		/// <summary>Gets the full name of the country/region in the language of the localized version of .NET Framework.</summary>
		/// <returns>The full name of the country/region in the language of the localized version of .NET Framework.</returns>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000DAADC File Offset: 0x000D8CDC
		[MonoTODO("DisplayName currently only returns the EnglishName")]
		public virtual string DisplayName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets the full name of the country/region in English.</summary>
		/// <returns>The full name of the country/region in English.</returns>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000DAADC File Offset: 0x000D8CDC
		public virtual string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets a unique identification number for a geographical region, country, city, or location.</summary>
		/// <returns>A 32-bit signed number that uniquely identifies a geographical location.</returns>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000DAAE4 File Offset: 0x000D8CE4
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.regionId;
			}
		}

		/// <summary>Gets a value indicating whether the country/region uses the metric system for measurements.</summary>
		/// <returns>true if the country/region uses the metric system for measurements; otherwise, false.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000DAAEC File Offset: 0x000D8CEC
		public virtual bool IsMetric
		{
			get
			{
				string text = this.iso2Name;
				return !(text == "US") && !(text == "UK");
			}
		}

		/// <summary>Gets the three-character ISO 4217 currency symbol associated with the country/region.</summary>
		/// <returns>The three-character ISO 4217 currency symbol associated with the country/region.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000DAB1D File Offset: 0x000D8D1D
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this.isoCurrencySymbol;
			}
		}

		/// <summary>Gets the name of a country/region formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the country/region formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000DAB25 File Offset: 0x000D8D25
		[ComVisible(false)]
		public virtual string NativeName
		{
			get
			{
				return this.nativeName;
			}
		}

		/// <summary>Gets the name of the currency used in the country/region, formatted in the native language of the country/region. </summary>
		/// <returns>The native name of the currency used in the country/region, formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000DAB2D File Offset: 0x000D8D2D
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			get
			{
				return this.currencyNativeName;
			}
		}

		/// <summary>Gets the name or ISO 3166 two-letter country/region code for the current <see cref="T:System.Globalization.RegionInfo" /> object.</summary>
		/// <returns>The value specified by the <paramref name="name" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.-or-The two-letter code defined in ISO 3166 for the country/region specified by the <paramref name="culture" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value is in uppercase.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000DAB35 File Offset: 0x000D8D35
		public virtual string Name
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Gets the three-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The three-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000DAB3D File Offset: 0x000D8D3D
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this.iso3Name;
			}
		}

		/// <summary>Gets the three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>The three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000DAB45 File Offset: 0x000D8D45
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.win3Name;
			}
		}

		/// <summary>Gets the two-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The two-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000DAB35 File Offset: 0x000D8D35
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Determines whether the specified object is the same instance as the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" /> object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />. </param>
		// Token: 0x06003776 RID: 14198 RVA: 0x000DAB50 File Offset: 0x000D8D50
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name == regionInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06003777 RID: 14199 RVA: 0x000DAB7A File Offset: 0x000D8D7A
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified for the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>A string containing the culture name or ISO 3166 two-letter country/region codes defined for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06003778 RID: 14200 RVA: 0x000DAB87 File Offset: 0x000D8D87
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000DAB8F File Offset: 0x000D8D8F
		internal static void ClearCachedData()
		{
			RegionInfo.currentRegion = null;
		}

		// Token: 0x04001DD4 RID: 7636
		private static RegionInfo currentRegion;

		// Token: 0x04001DD5 RID: 7637
		private int regionId;

		// Token: 0x04001DD6 RID: 7638
		private string iso2Name;

		// Token: 0x04001DD7 RID: 7639
		private string iso3Name;

		// Token: 0x04001DD8 RID: 7640
		private string win3Name;

		// Token: 0x04001DD9 RID: 7641
		private string englishName;

		// Token: 0x04001DDA RID: 7642
		private string nativeName;

		// Token: 0x04001DDB RID: 7643
		private string currencySymbol;

		// Token: 0x04001DDC RID: 7644
		private string isoCurrencySymbol;

		// Token: 0x04001DDD RID: 7645
		private string currencyEnglishName;

		// Token: 0x04001DDE RID: 7646
		private string currencyNativeName;
	}
}
