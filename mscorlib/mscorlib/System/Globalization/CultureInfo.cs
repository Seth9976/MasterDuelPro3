using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Interop;

namespace System.Globalization
{
	/// <summary>Provides information about a specific culture (called a locale for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, and formatting for dates and sort strings.</summary>
	// Token: 0x020006CA RID: 1738
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000D6B39 File Offset: 0x000D4D39
		internal CultureData _cultureData
		{
			get
			{
				return this.m_cultureData;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000D6B41 File Offset: 0x000D4D41
		internal bool _isInherited
		{
			get
			{
				return this.m_isInherited;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>The object that is culture-independent (invariant).</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000D6B49 File Offset: 0x000D4D49
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture used by the current thread.</summary>
		/// <returns>An object that represents the culture used by the current thread.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000D6B52 File Offset: 0x000D4D52
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000D6B5E File Offset: 0x000D4D5E
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the current user interface culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>The culture used by the Resource Manager to look up culture-specific resources at run time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000D6B6B File Offset: 0x000D4D6B
		// (set) Token: 0x060036F8 RID: 14072 RVA: 0x000D6B77 File Offset: 0x000D4D77
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000D6B84 File Offset: 0x000D4D84
		internal static CultureInfo ConstructCurrentCulture()
		{
			if (CultureInfo.default_current_culture != null)
			{
				return CultureInfo.default_current_culture;
			}
			if (GlobalizationMode.Invariant)
			{
				return CultureInfo.InvariantCulture;
			}
			string current_locale_name = CultureInfo.get_current_locale_name();
			CultureInfo cultureInfo = null;
			if (current_locale_name != null)
			{
				try
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(current_locale_name);
				}
				catch
				{
				}
			}
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else
			{
				cultureInfo.m_isReadOnly = true;
				cultureInfo.m_useUserOverride = true;
			}
			CultureInfo.default_current_culture = cultureInfo;
			return cultureInfo;
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000D6BFB File Offset: 0x000D4DFB
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000D6C03 File Offset: 0x000D4E03
		internal string _name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture types that pertain to the current <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>A bitwise combination of one or more <see cref="T:System.Globalization.CultureTypes" /> values. There is no default value.</returns>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000D6C0C File Offset: 0x000D4E0C
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				foreach (object obj in Enum.GetValues(typeof(CultureTypes)))
				{
					CultureTypes cultureTypes2 = (CultureTypes)obj;
					if (Array.IndexOf<CultureInfo>(CultureInfo.GetCultures(cultureTypes2), this) >= 0)
					{
						cultureTypes |= cultureTypes2;
					}
				}
				return cultureTypes;
			}
		}

		/// <summary>Gets an alternate user interface culture suitable for console applications when the default graphic user interface culture is unsuitable.</summary>
		/// <returns>An alternate culture that is used to read and display text on the console.</returns>
		// Token: 0x060036FE RID: 14078 RVA: 0x000D6C80 File Offset: 0x000D4E80
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			string name = this.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1260172255U)
			{
				if (num <= 939759947U)
				{
					if (num <= 249681006U)
					{
						if (num <= 198587497U)
						{
							if (num != 64366545U)
							{
								if (num != 77939050U)
								{
									if (num != 198587497U)
									{
										goto IL_06C2;
									}
									if (!(name == "ar-SA"))
									{
										goto IL_06C2;
									}
								}
								else if (!(name == "mr-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-SY"))
							{
								goto IL_06C2;
							}
						}
						else if (num != 233820021U)
						{
							if (num != 236085687U)
							{
								if (num != 249681006U)
								{
									goto IL_06C2;
								}
								if (!(name == "hi-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-KW"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-EG"))
						{
							goto IL_06C2;
						}
					}
					else if (num <= 469295067U)
					{
						if (num != 419506663U)
						{
							if (num != 434712723U)
							{
								if (num != 469295067U)
								{
									goto IL_06C2;
								}
								if (!(name == "ar-AE"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-BH"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 511763911U)
					{
						if (num != 907337542U)
						{
							if (num != 939759947U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-MA"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "ar-JO"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi-VN"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1074569279U)
				{
					if (num <= 1011170994U)
					{
						if (num != 944060518U)
						{
							if (num != 944899161U)
							{
								if (num != 1011170994U)
								{
									goto IL_06C2;
								}
								if (!(name == "te"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ta"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1011465184U)
					{
						if (num != 1070729495U)
						{
							if (num != 1074569279U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-IQ"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-QA"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1123180923U)
				{
					if (num != 1094514636U)
					{
						if (num != 1095059089U)
						{
							if (num != 1123180923U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-DZ"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "th"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "kn"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1141238470U)
				{
					if (num != 1162022470U)
					{
						if (num != 1260172255U)
						{
							goto IL_06C2;
						}
						if (!(name == "dv"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "ur"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ar-LY"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 1756775346U)
			{
				if (num <= 1527123707U)
				{
					if (num <= 1429081278U)
					{
						if (num != 1277200137U)
						{
							if (num != 1347311754U)
							{
								if (num != 1429081278U)
								{
									goto IL_06C2;
								}
								if (!(name == "mr"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "pa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1456070279U)
					{
						if (num != 1458211363U)
						{
							if (num != 1527123707U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-LB"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu-IN"))
						{
							goto IL_06C2;
						}
					}
					else
					{
						if (!(name == "ar-TN"))
						{
							goto IL_06C2;
						}
						goto IL_06B7;
					}
				}
				else if (num <= 1622153968U)
				{
					if (num != 1547363254U)
					{
						if (num != 1562713850U)
						{
							if (num != 1622153968U)
							{
								goto IL_06C2;
							}
							if (!(name == "kok-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "he"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1680010088U)
				{
					if (num != 1748694682U)
					{
						if (num != 1756775346U)
						{
							goto IL_06C2;
						}
						if (!(name == "ta-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "hi"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "fa"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3073845542U)
			{
				if (num <= 2153224060U)
				{
					if (num != 1846834581U)
					{
						if (num != 2046577884U)
						{
							if (num != 2153224060U)
							{
								goto IL_06C2;
							}
							if (!(name == "he-IL"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "kok"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "dv-MV"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 2902799296U)
				{
					if (num != 3060605246U)
					{
						if (num != 3073845542U)
						{
							goto IL_06C2;
						}
						if (!(name == "te-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "pa-IN"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "kn-IN"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3477219856U)
			{
				if (num != 3294142633U)
				{
					if (num != 3311105148U)
					{
						if (num != 3477219856U)
						{
							goto IL_06C2;
						}
						if (!(name == "fa-IR"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "syr-SY"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "syr"))
				{
					goto IL_06C2;
				}
			}
			else if (num != 3957656723U)
			{
				if (num != 4027935912U)
				{
					if (num != 4091062904U)
					{
						goto IL_06C2;
					}
					if (!(name == "th-TH"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ur-PK"))
				{
					goto IL_06C2;
				}
			}
			else if (!(name == "ar-YE"))
			{
				goto IL_06C2;
			}
			return CultureInfo.GetCultureInfo("en");
			IL_06B7:
			return CultureInfo.GetCultureInfo("fr");
			IL_06C2:
			if ((this.CultureTypes & CultureTypes.WindowsOnlyCultures) == (CultureTypes)0)
			{
				return this;
			}
			return CultureInfo.InvariantCulture;
		}

		/// <summary>Deprecated. Gets the RFC 4646 standard identification for a language. </summary>
		/// <returns>A string that is the RFC 4646 standard identification for a language.</returns>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000D7364 File Offset: 0x000D5564
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHS")
				{
					return "zh-Hans";
				}
				if (!(name == "zh-CHT"))
				{
					return this.Name;
				}
				return "zh-Hant";
			}
		}

		/// <summary>Gets the active input locale identifier.</summary>
		/// <returns>A 32-bit signed number that specifies an input locale identifier.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000D73A8 File Offset: 0x000D55A8
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				int lcid = this.LCID;
				if (lcid <= 1034)
				{
					if (lcid == 4)
					{
						return 2052;
					}
					if (lcid == 1034)
					{
						return 3082;
					}
				}
				else
				{
					if (lcid == 31748)
					{
						return 1028;
					}
					if (lcid == 31770)
					{
						return 2074;
					}
				}
				if (this.LCID >= 1024)
				{
					return this.LCID;
				}
				return this.LCID + 1024;
			}
		}

		/// <summary>Gets the culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000D741C File Offset: 0x000D561C
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		/// <summary>Gets the culture name in the format languagecode2-country/regioncode2.</summary>
		/// <returns>The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000D6C03 File Offset: 0x000D4E03
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture name, consisting of the language, the country/region, and the optional script, that the culture is set to display.</summary>
		/// <returns>The culture name. consisting of the full name of the language, the full name of the country/region, and the optional script. The format is discussed in the description of the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000D7424 File Offset: 0x000D5624
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000D743A File Offset: 0x000D563A
		internal string NativeCalendarName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.native_calendar_names[(this.default_calendar_type >> 8) - 1];
			}
		}

		/// <summary>Gets the default calendar used by the culture.</summary>
		/// <returns>A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used by the culture.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000D745B File Offset: 0x000D565B
		public virtual Calendar Calendar
		{
			get
			{
				if (this.calendar == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					this.calendar = CultureInfo.CreateCalendar(this.default_calendar_type);
				}
				return this.calendar;
			}
		}

		/// <summary>Gets the list of calendars that can be used by the culture.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000D748A File Offset: 0x000D568A
		[MonoLimitation("Optional calendars are not supported only default calendar is returned")]
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				return new Calendar[] { this.Calendar };
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000D749C File Offset: 0x000D569C
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						if (this.parent_lcid == 31748 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hant");
						}
						if (this.parent_lcid == 4 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hans");
						}
						return null;
					}
					else if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else if (this.cultureID == 1028)
					{
						this.parent_culture = new CultureInfo("zh-CHT");
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000D75A8 File Offset: 0x000D57A8
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		/// <summary>Gets the ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000D761C File Offset: 0x000D581C
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		/// <summary>Gets the three-letter code for the language as defined in the Windows API.</summary>
		/// <returns>The three-letter code for the language as defined in the Windows API.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000D7632 File Offset: 0x000D5832
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		/// <summary>Gets the ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000D7648 File Offset: 0x000D5848
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings; otherwise, false.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000D765E File Offset: 0x000D585E
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		/// <summary>Refreshes cached culture-related information.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600370D RID: 14093 RVA: 0x000D7668 File Offset: 0x000D5868
		public void ClearCachedData()
		{
			object obj = CultureInfo.shared_table_lock;
			lock (obj)
			{
				CultureInfo.shared_by_number = null;
				CultureInfo.shared_by_name = null;
			}
			CultureInfo.default_current_culture = null;
			RegionInfo.ClearCachedData();
			TimeZone.ClearCachedData();
			TimeZoneInfo.ClearCachedData();
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A copy of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x0600370E RID: 14094 RVA: 0x000D76C4 File Offset: 0x000D58C4
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		/// <summary>Determines whether the specified object is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>true if <paramref name="value" /> is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />. </param>
		// Token: 0x0600370F RID: 14095 RVA: 0x000D7730 File Offset: 0x000D5930
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID && cultureInfo.m_name == this.m_name;
		}

		/// <summary>Gets the list of supported cultures filtered by the specified <see cref="T:System.Globalization.CultureTypes" /> parameter.</summary>
		/// <returns>An array that contains the cultures specified by the <paramref name="types" /> parameter. The array of cultures is unsorted.</returns>
		/// <param name="types">A bitwise combination of the enumeration values that filter the cultures to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="types" /> specifies an invalid combination of <see cref="T:System.Globalization.CultureTypes" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003710 RID: 14096 RVA: 0x000D7768 File Offset: 0x000D5968
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) > (CultureTypes)0;
			bool flag2 = (types & CultureTypes.SpecificCultures) > (CultureTypes)0;
			bool flag3 = (types & CultureTypes.InstalledWin32Cultures) > (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, flag2, flag3);
			int i = 0;
			if (flag && array.Length != 0 && array[0] == null)
			{
				array[i++] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			while (i < array.Length)
			{
				CultureInfo cultureInfo = array[i];
				CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
				CultureInfo cultureInfo2 = array[i];
				string name = cultureInfo.m_name;
				bool flag4 = false;
				int num = cultureInfo.datetime_index;
				int calendarType = cultureInfo.CalendarType;
				int num2 = cultureInfo.number_index;
				string text = cultureInfo.iso2lang;
				int ansi = textInfoData.ansi;
				int oem = textInfoData.oem;
				int mac = textInfoData.mac;
				int ebcdic = textInfoData.ebcdic;
				bool right_to_left = textInfoData.right_to_left;
				char list_sep = (char)textInfoData.list_sep;
				cultureInfo2.m_cultureData = CultureData.GetCultureData(name, flag4, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
				i++;
			}
			return array;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000D783D File Offset: 0x000D5A3D
		private unsafe CultureInfo.Data GetTextInfoData()
		{
			return *(CultureInfo.Data*)this.textinfo_data;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06003712 RID: 14098 RVA: 0x000D784A File Offset: 0x000D5A4A
		public override int GetHashCode()
		{
			return this.cultureID.GetHashCode();
		}

		/// <summary>Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.</returns>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ci" /> is null. </exception>
		// Token: 0x06003713 RID: 14099 RVA: 0x000D7858 File Offset: 0x000D5A58
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		/// <summary>Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" /> in the format languagecode2-country/regioncode2.</summary>
		/// <returns>A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06003714 RID: 14100 RVA: 0x000D6C03 File Offset: 0x000D4E03
		public override string ToString()
		{
			return this.m_name;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000D78F0 File Offset: 0x000D5AF0
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture; otherwise, false.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000D7960 File Offset: 0x000D5B60
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (this.cultureID == 127)
				{
					return false;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.territory == null;
			}
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x00002C89 File Offset: 0x00000E89
		private void CheckNeutral()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</summary>
		/// <returns>A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x000D7988 File Offset: 0x000D5B88
		// (set) Token: 0x06003719 RID: 14105 RVA: 0x000D79C8 File Offset: 0x000D5BC8
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000D7A04 File Offset: 0x000D5C04
		// (set) Token: 0x0600371B RID: 14107 RVA: 0x000D7A7B File Offset: 0x000D5C7B
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (this.dateTimeInfo != null)
				{
					return this.dateTimeInfo;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				DateTimeFormatInfo dateTimeFormatInfo;
				if (GlobalizationMode.Invariant)
				{
					dateTimeFormatInfo = new DateTimeFormatInfo();
				}
				else
				{
					dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
				}
				dateTimeFormatInfo._isReadOnly = this.m_isReadOnly;
				Thread.MemoryBarrier();
				this.dateTimeInfo = dateTimeFormatInfo;
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		/// <summary>Gets the full localized culture name. </summary>
		/// <returns>The full localized culture name in the format languagefull [country/regionfull], where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000D7AB5 File Offset: 0x000D5CB5
		public virtual string DisplayName
		{
			get
			{
				return this.EnglishName;
			}
		}

		/// <summary>Gets the culture name in the format languagefull [country/regionfull] in English.</summary>
		/// <returns>The culture name in the format languagefull [country/regionfull] in English, where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000D7ABD File Offset: 0x000D5CBD
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000D7AD3 File Offset: 0x000D5CD3
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an object that defines how to format the specified type.</summary>
		/// <returns>The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property, which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" /> class.-or- The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.-or- null, if <paramref name="formatType" /> is any other object.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports the <see cref="T:System.Globalization.NumberFormatInfo" /> and <see cref="T:System.Globalization.DateTimeFormatInfo" /> types. </param>
		// Token: 0x06003720 RID: 14112 RVA: 0x000D7ADC File Offset: 0x000D5CDC
		public virtual object GetFormat(Type formatType)
		{
			object obj = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				obj = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				obj = this.DateTimeFormat;
			}
			return obj;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000D7B20 File Offset: 0x000D5D20
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x06003722 RID: 14114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x06003723 RID: 14115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x06003724 RID: 14116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_current_locale_name();

		// Token: 0x06003725 RID: 14117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x06003726 RID: 14118 RVA: 0x000D7B38 File Offset: 0x000D5D38
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
			}
			this.textInfo = TextInfo.Invariant;
			this.m_name = string.Empty;
			this.englishname = (this.nativename = "Invariant Language (Invariant Country)");
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.win3lang = "IVL";
			this.default_calendar_type = 257;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D7BCF File Offset: 0x000D5DCF
		private TextInfo CreateTextInfo(bool readOnly)
		{
			TextInfo textInfo = new TextInfo(this.m_cultureData);
			textInfo.SetReadOnlyState(readOnly);
			return textInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. </exception>
		// Token: 0x06003728 RID: 14120 RVA: 0x000D7BE3 File Offset: 0x000D5DE3
		public CultureInfo(int culture)
			: this(culture, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier.</exception>
		// Token: 0x06003729 RID: 14121 RVA: 0x000D7BED File Offset: 0x000D5DED
		public CultureInfo(int culture, bool useUserOverride)
			: this(culture, useUserOverride, false)
		{
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000D7BF8 File Offset: 0x000D5DF8
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_lcid(culture))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Culture ID {0} (0x{1}) is not a supported culture.", culture.ToString(CultureInfo.InvariantCulture), culture.ToString("X4", CultureInfo.InvariantCulture));
				throw new CultureNotFoundException("culture", text);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name = this.m_name;
			bool useUserOverride2 = this.m_useUserOverride;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text2 = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride2, num, calendarType, num2, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.</exception>
		// Token: 0x0600372B RID: 14123 RVA: 0x000D7CF0 File Offset: 0x000D5EF0
		public CultureInfo(string name)
			: this(name, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.</exception>
		// Token: 0x0600372C RID: 14124 RVA: 0x000D7CFA File Offset: 0x000D5EFA
		public CultureInfo(string name, bool useUserOverride)
			: this(name, useUserOverride, false)
		{
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D7D08 File Offset: 0x000D5F08
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
			if (name.Length == 0)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructLocaleFromName(name.ToLowerInvariant()))
			{
				throw CultureInfo.CreateNotFoundException(name);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name2 = this.m_name;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name2, useUserOverride, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D7DE6 File Offset: 0x000D5FE6
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D7DF5 File Offset: 0x000D5FF5
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Dictionary<int, CultureInfo>();
				CultureInfo.shared_by_name = new Dictionary<string, CultureInfo>();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture by using the specified culture identifier.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="culture">A locale identifier (LCID).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003730 RID: 14128 RVA: 0x000D7E34 File Offset: 0x000D6034
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture < 1)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null && CultureInfo.shared_by_number.TryGetValue(culture, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(culture, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture name. </summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003731 RID: 14129 RVA: 0x000D7EB0 File Offset: 0x000D60B0
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null && CultureInfo.shared_by_name.TryGetValue(name, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(name, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture. Parameters specify a culture that is initialized with the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects specified by another culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="altName">The name of a culture that supplies the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects used to initialize <paramref name="name" />. <paramref name="altName" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="altName" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> or <paramref name="altName" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003732 RID: 14130 RVA: 0x000D7F24 File Offset: 0x000D6124
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		/// <summary>Deprecated. Retrieves a read-only <see cref="T:System.Globalization.CultureInfo" /> object having linguistic characteristics that are identified by the specified RFC 4646 language tag.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a language as specified by the RFC 4646 standard.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> does not correspond to a supported culture.</exception>
		// Token: 0x06003733 RID: 14131 RVA: 0x000D7F48 File Offset: 0x000D6148
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-Hans")
			{
				return CultureInfo.GetCultureInfo("zh-CHS");
			}
			if (!(name == "zh-Hant"))
			{
				return CultureInfo.GetCultureInfo(name);
			}
			return CultureInfo.GetCultureInfo("zh-CHT");
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D7F84 File Offset: 0x000D6184
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool flag2;
			bool flag3;
			if (reference)
			{
				flag2 = !flag;
				flag3 = false;
			}
			else
			{
				flag3 = false;
				flag2 = !flag;
			}
			return new CultureInfo(name, flag2, flag3);
		}

		/// <summary>Creates a <see cref="T:System.Globalization.CultureInfo" /> that represents the specific culture that is associated with the specified name.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents:The invariant culture, if <paramref name="name" /> is an empty string ("").-or- The specific culture associated with <paramref name="name" />, if <paramref name="name" /> is a neutral culture.-or- The culture specified by <paramref name="name" />, if <paramref name="name" /> is already a specific culture.</returns>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name or the name of an existing <see cref="T:System.Globalization.CultureInfo" /> object. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.-or- The culture specified by <paramref name="name" /> does not have a specific culture associated with it. </exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003735 RID: 14133 RVA: 0x000D7FBC File Offset: 0x000D61BC
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			string text = name;
			name = name.ToLowerInvariant();
			CultureInfo cultureInfo = new CultureInfo();
			if (!cultureInfo.ConstructLocaleFromName(name))
			{
				throw CultureInfo.CreateNotFoundException(text);
			}
			if (cultureInfo.IsNeutralCulture)
			{
				cultureInfo = CultureInfo.CreateSpecificCultureFromNeutral(cultureInfo.Name);
			}
			CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
			CultureInfo cultureInfo2 = cultureInfo;
			string name2 = cultureInfo.m_name;
			bool flag = false;
			int num = cultureInfo.datetime_index;
			int calendarType = cultureInfo.CalendarType;
			int num2 = cultureInfo.number_index;
			string text2 = cultureInfo.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			cultureInfo2.m_cultureData = CultureData.GetCultureData(name2, flag, num, calendarType, num2, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
			return cultureInfo;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D8078 File Offset: 0x000D6278
		private bool ConstructLocaleFromName(string name)
		{
			if (this.construct_internal_locale_from_name(name))
			{
				return true;
			}
			int num = name.Length - 1;
			if (num > 0)
			{
				while ((num = name.LastIndexOf('-', num - 1)) > 0)
				{
					if (this.construct_internal_locale_from_name(name.Substring(0, num)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D80C4 File Offset: 0x000D62C4
		private static CultureInfo CreateSpecificCultureFromNeutral(string name)
		{
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			int num2;
			if (num <= 1344898993U)
			{
				if (num <= 1128614327U)
				{
					if (num <= 1025408520U)
					{
						if (num <= 975938470U)
						{
							if (num <= 926444256U)
							{
								if (num <= 896475900U)
								{
									if (num != 275533995U)
									{
										if (num == 896475900U)
										{
											if (text == "arn")
											{
												num2 = 1146;
												goto IL_1B49;
											}
										}
									}
									else if (text == "nso")
									{
										num2 = 1132;
										goto IL_1B49;
									}
								}
								else if (num != 925484199U)
								{
									if (num == 926444256U)
									{
										if (text == "id")
										{
											num2 = 1057;
											goto IL_1B49;
										}
									}
								}
								else if (text == "mn-cyrl")
								{
									num2 = 1104;
									goto IL_1B49;
								}
							}
							else if (num <= 944060518U)
							{
								if (num != 942383232U)
								{
									if (num == 944060518U)
									{
										if (text == "ta")
										{
											num2 = 1097;
											goto IL_1B49;
										}
									}
								}
								else if (text == "be")
								{
									num2 = 1059;
									goto IL_1B49;
								}
							}
							else if (num != 944899161U)
							{
								if (num == 975938470U)
								{
									if (text == "bg")
									{
										num2 = 1026;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sa")
							{
								num2 = 1103;
								goto IL_1B49;
							}
						}
						else if (num <= 996684602U)
						{
							if (num <= 977615756U)
							{
								if (num != 976777113U)
								{
									if (num == 977615756U)
									{
										if (text == "tg")
										{
											num2 = 1064;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ig")
								{
									num2 = 1136;
									goto IL_1B49;
								}
							}
							else if (num != 991980614U)
							{
								if (num == 996684602U)
								{
									if (text == "mn-mong")
									{
										num2 = 2128;
										goto IL_1B49;
									}
								}
							}
							else if (text == "gd")
							{
								num2 = 1169;
								goto IL_1B49;
							}
						}
						else if (num <= 1011170994U)
						{
							if (num != 1009493708U)
							{
								if (num == 1011170994U)
								{
									if (text == "te")
									{
										num2 = 1098;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ba")
							{
								num2 = 1133;
								goto IL_1B49;
							}
						}
						else if (num != 1011465184U)
						{
							if (num != 1012009637U)
							{
								if (num == 1025408520U)
								{
									if (text == "tzm-latn")
									{
										num2 = 2143;
										goto IL_1B49;
									}
								}
							}
							else if (text == "se")
							{
								num2 = 1083;
								goto IL_1B49;
							}
						}
						else if (text == "vi")
						{
							num2 = 1066;
							goto IL_1B49;
						}
					}
					else if (num <= 1092248970U)
					{
						if (num <= 1058693732U)
						{
							if (num <= 1044726232U)
							{
								if (num != 1044181779U)
								{
									if (num == 1044726232U)
									{
										if (text == "tk")
										{
											num2 = 1090;
											goto IL_1B49;
										}
									}
								}
								else if (text == "kk")
								{
									num2 = 1087;
									goto IL_1B49;
								}
							}
							else if (num != 1045564875U)
							{
								if (num == 1058693732U)
								{
									if (text == "el")
									{
										num2 = 1032;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sk")
							{
								num2 = 1051;
								goto IL_1B49;
							}
						}
						else if (num <= 1076162899U)
						{
							if (num != 1075868709U)
							{
								if (num == 1076162899U)
								{
									if (text == "am")
									{
										num2 = 1118;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ga")
							{
								num2 = 2108;
								goto IL_1B49;
							}
						}
						else if (num != 1079120113U)
						{
							if (num != 1087741671U)
							{
								if (num == 1092248970U)
								{
									if (text == "en")
									{
										num2 = 1033;
										goto IL_1B49;
									}
								}
							}
							else if (text == "az-cyrl")
							{
								num2 = 2092;
								goto IL_1B49;
							}
						}
						else if (text == "si")
						{
							num2 = 1115;
							goto IL_1B49;
						}
					}
					else if (num <= 1110556780U)
					{
						if (num <= 1095059089U)
						{
							if (num != 1094514636U)
							{
								if (num == 1095059089U)
								{
									if (text == "th")
									{
										num2 = 1054;
										goto IL_1B49;
									}
								}
							}
							else if (text == "kn")
							{
								num2 = 1099;
								goto IL_1B49;
							}
						}
						else if (num != 1110159422U)
						{
							if (num == 1110556780U)
							{
								if (text == "lo")
								{
									num2 = 1108;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bo")
						{
							num2 = 1105;
							goto IL_1B49;
						}
					}
					else if (num <= 1126201566U)
					{
						if (num != 1111292255U)
						{
							if (num == 1126201566U)
							{
								if (text == "gl")
								{
									num2 = 1110;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ko")
						{
							num2 = 1042;
							goto IL_1B49;
						}
					}
					else if (num != 1126937041U)
					{
						if (num != 1128069874U)
						{
							if (num == 1128614327U)
							{
								if (text == "tn")
								{
									num2 = 1074;
									goto IL_1B49;
								}
							}
						}
						else if (text == "kl")
						{
							num2 = 1135;
							goto IL_1B49;
						}
					}
					else if (text == "bn")
					{
						num2 = 1093;
						goto IL_1B49;
					}
				}
				else if (num <= 1213341065U)
				{
					if (num <= 1177122803U)
					{
						if (num <= 1162022470U)
						{
							if (num <= 1144553303U)
							{
								if (num != 1129452970U)
								{
									if (num == 1144553303U)
									{
										if (text == "ii")
										{
											num2 = 1144;
											goto IL_1B49;
										}
									}
								}
								else if (text == "sl")
								{
									num2 = 1060;
									goto IL_1B49;
								}
							}
							else if (num != 1144847493U)
							{
								if (num == 1162022470U)
								{
									if (text == "ur")
									{
										num2 = 1056;
										goto IL_1B49;
									}
								}
							}
							else if (text == "km")
							{
								num2 = 1107;
								goto IL_1B49;
							}
						}
						else if (num <= 1163008208U)
						{
							if (num != 1162757945U)
							{
								if (num == 1163008208U)
								{
									if (text == "sr")
									{
										num2 = 9242;
										goto IL_1B49;
									}
								}
							}
							else if (text == "pl")
							{
								num2 = 1045;
								goto IL_1B49;
							}
						}
						else if (num != 1164435231U)
						{
							if (num != 1176137065U)
							{
								if (num == 1177122803U)
								{
									if (text == "cs")
									{
										num2 = 1029;
										goto IL_1B49;
									}
								}
							}
							else if (text == "es")
							{
								num2 = 3082;
								goto IL_1B49;
							}
						}
						else if (text == "zh")
						{
							num2 = 2052;
							goto IL_1B49;
						}
					}
					else if (num <= 1195724803U)
					{
						if (num <= 1194444875U)
						{
							if (num != 1192914684U)
							{
								if (num == 1194444875U)
								{
									if (text == "lb")
									{
										num2 = 1134;
										goto IL_1B49;
									}
								}
							}
							else if (text == "et")
							{
								num2 = 1061;
								goto IL_1B49;
							}
						}
						else if (num != 1194886160U)
						{
							if (num == 1195724803U)
							{
								if (text == "tr")
								{
									num2 = 1055;
									goto IL_1B49;
								}
							}
						}
						else if (text == "it")
						{
							num2 = 1040;
							goto IL_1B49;
						}
					}
					else if (num <= 1211324057U)
					{
						if (num != 1209692303U)
						{
							if (num == 1211324057U)
							{
								if (text == "iu-cans")
								{
									num2 = 1117;
									goto IL_1B49;
								}
							}
						}
						else if (text == "eu")
						{
							num2 = 1069;
							goto IL_1B49;
						}
					}
					else if (num != 1211663779U)
					{
						if (num != 1211957969U)
						{
							if (num == 1213341065U)
							{
								if (text == "sq")
								{
									num2 = 1052;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ka")
						{
							num2 = 1079;
							goto IL_1B49;
						}
					}
					else if (text == "iu")
					{
						num2 = 2141;
						goto IL_1B49;
					}
				}
				else if (num <= 1277200137U)
				{
					if (num <= 1231251517U)
					{
						if (num <= 1227161470U)
						{
							if (num != 1213488160U)
							{
								if (num == 1227161470U)
								{
									if (text == "af")
									{
										num2 = 1078;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ru")
							{
								num2 = 1049;
								goto IL_1B49;
							}
						}
						else if (num != 1230118684U)
						{
							if (num == 1231251517U)
							{
								if (text == "xh")
								{
									num2 = 1076;
									goto IL_1B49;
								}
							}
						}
						else if (text == "sv")
						{
							num2 = 1053;
							goto IL_1B49;
						}
					}
					else if (num <= 1246896303U)
					{
						if (num != 1237973804U)
						{
							if (num == 1246896303U)
							{
								if (text == "sw")
								{
									num2 = 1089;
									goto IL_1B49;
								}
							}
						}
						else if (text == "uz-latn")
						{
							num2 = 1091;
							goto IL_1B49;
						}
					}
					else if (num != 1247043398U)
					{
						if (num != 1260172255U)
						{
							if (num == 1277200137U)
							{
								if (text == "gu")
								{
									num2 = 1095;
									goto IL_1B49;
								}
							}
						}
						else if (text == "dv")
						{
							num2 = 1125;
							goto IL_1B49;
						}
					}
					else if (text == "rw")
					{
						num2 = 1159;
						goto IL_1B49;
					}
				}
				else if (num <= 1296390517U)
				{
					if (num <= 1278921350U)
					{
						if (num != 1277347232U)
						{
							if (num == 1278921350U)
							{
								if (text == "hu")
								{
									num2 = 1038;
									goto IL_1B49;
								}
							}
						}
						else if (text == "fy")
						{
							num2 = 1122;
							goto IL_1B49;
						}
					}
					else if (num != 1296243422U)
					{
						if (num == 1296390517U)
						{
							if (text == "tt")
							{
								num2 = 1092;
								goto IL_1B49;
							}
						}
					}
					else if (text == "uz")
					{
						num2 = 1091;
						goto IL_1B49;
					}
				}
				else if (num <= 1312329493U)
				{
					if (num != 1311490850U)
					{
						if (num == 1312329493U)
						{
							if (text == "is")
							{
								num2 = 1039;
								goto IL_1B49;
							}
						}
					}
					else if (text == "bs")
					{
						num2 = 5146;
						goto IL_1B49;
					}
				}
				else if (num != 1328268469U)
				{
					if (num != 1329254207U)
					{
						if (num == 1344898993U)
						{
							if (text == "cy")
							{
								num2 = 1106;
								goto IL_1B49;
							}
						}
					}
					else if (text == "hr")
					{
						num2 = 1050;
						goto IL_1B49;
					}
				}
				else if (text == "br")
				{
					num2 = 1150;
					goto IL_1B49;
				}
			}
			else if (num <= 1646454850U)
			{
				if (num <= 1545391778U)
				{
					if (num <= 1462636516U)
					{
						if (num <= 1428492898U)
						{
							if (num <= 1347311754U)
							{
								if (num != 1346178921U)
								{
									if (num == 1347311754U)
									{
										if (text == "pa")
										{
											num2 = 1094;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ky")
								{
									num2 = 1088;
									goto IL_1B49;
								}
							}
							else if (num != 1424802581U)
							{
								if (num == 1428492898U)
								{
									if (text == "az")
									{
										num2 = 1068;
										goto IL_1B49;
									}
								}
							}
							else if (text == "tg-cyrl")
							{
								num2 = 1064;
								goto IL_1B49;
							}
						}
						else if (num <= 1429850248U)
						{
							if (num != 1429081278U)
							{
								if (num == 1429850248U)
								{
									if (text == "gsw")
									{
										num2 = 1156;
										goto IL_1B49;
									}
								}
							}
							else if (text == "mr")
							{
								num2 = 1102;
								goto IL_1B49;
							}
						}
						else if (num != 1445858897U)
						{
							if (num != 1461901041U)
							{
								if (num == 1462636516U)
								{
									if (text == "mt")
									{
										num2 = 1082;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fr")
							{
								num2 = 1036;
								goto IL_1B49;
							}
						}
						else if (text == "ms")
						{
							num2 = 1086;
							goto IL_1B49;
						}
					}
					else if (num <= 1479958588U)
					{
						if (num <= 1478281302U)
						{
							if (num != 1463180969U)
							{
								if (num == 1478281302U)
								{
									if (text == "da")
									{
										num2 = 1030;
										goto IL_1B49;
									}
								}
							}
							else if (text == "nb")
							{
								num2 = 1044;
								goto IL_1B49;
							}
						}
						else if (num != 1479119945U)
						{
							if (num == 1479958588U)
							{
								if (text == "ne")
								{
									num2 = 1121;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ca")
						{
							num2 = 1027;
							goto IL_1B49;
						}
					}
					else if (num <= 1483209992U)
					{
						if (num != 1480252778U)
						{
							if (num == 1483209992U)
							{
								if (text == "zu")
								{
									num2 = 1077;
									goto IL_1B49;
								}
							}
						}
						else if (text == "hy")
						{
							num2 = 1067;
							goto IL_1B49;
						}
					}
					else if (num != 1514352469U)
					{
						if (num != 1529997255U)
						{
							if (num == 1545391778U)
							{
								if (text == "de")
								{
									num2 = 1031;
									goto IL_1B49;
								}
							}
						}
						else if (text == "lv")
						{
							num2 = 1062;
							goto IL_1B49;
						}
					}
					else if (text == "ug")
					{
						num2 = 1152;
						goto IL_1B49;
					}
				}
				else if (num <= 1579491469U)
				{
					if (num <= 1551553596U)
					{
						if (num <= 1546524611U)
						{
							if (num != 1545789136U)
							{
								if (num == 1546524611U)
								{
									if (text == "mi")
									{
										num2 = 1153;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fi")
							{
								num2 = 1035;
								goto IL_1B49;
							}
						}
						else if (num != 1547363254U)
						{
							if (num == 1551553596U)
							{
								if (text == "prs")
								{
									num2 = 1164;
									goto IL_1B49;
								}
							}
						}
						else if (text == "he")
						{
							num2 = 1037;
							goto IL_1B49;
						}
					}
					else if (num <= 1563552493U)
					{
						if (num != 1562713850U)
						{
							if (num == 1563552493U)
							{
								if (text == "lt")
								{
									num2 = 1063;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ar")
						{
							num2 = 1025;
							goto IL_1B49;
						}
					}
					else if (num != 1563699588U)
					{
						if (num != 1565420801U)
						{
							if (num == 1579491469U)
							{
								if (text == "as")
								{
									num2 = 1101;
									goto IL_1B49;
								}
							}
						}
						else if (text == "pt")
						{
							num2 = 1046;
							goto IL_1B49;
						}
					}
					else if (text == "or")
					{
						num2 = 1096;
						goto IL_1B49;
					}
				}
				else if (num <= 1596857468U)
				{
					if (num <= 1581462945U)
					{
						if (num != 1580079849U)
						{
							if (num == 1581462945U)
							{
								if (text == "uk")
								{
									num2 = 1058;
									goto IL_1B49;
								}
							}
						}
						else if (text == "mk")
						{
							num2 = 1071;
							goto IL_1B49;
						}
					}
					else if (num != 1582198420U)
					{
						if (num == 1596857468U)
						{
							if (text == "ml")
							{
								num2 = 1100;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ps")
					{
						num2 = 1123;
						goto IL_1B49;
					}
				}
				else if (num <= 1616151016U)
				{
					if (num != 1614473730U)
					{
						if (num == 1616151016U)
						{
							if (text == "rm")
							{
								num2 = 1047;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ha")
					{
						num2 = 1128;
						goto IL_1B49;
					}
				}
				else if (num != 1630412706U)
				{
					if (num != 1630957159U)
					{
						if (num == 1646454850U)
						{
							if (text == "fo")
							{
								num2 = 1080;
								goto IL_1B49;
							}
						}
					}
					else if (text == "nl")
					{
						num2 = 1043;
						goto IL_1B49;
					}
				}
				else if (text == "mn")
				{
					num2 = 1104;
					goto IL_1B49;
				}
			}
			else if (num <= 3012500870U)
			{
				if (num <= 1748694682U)
				{
					if (num <= 1649706254U)
					{
						if (num <= 1647734778U)
						{
							if (num != 1646896135U)
							{
								if (num == 1647734778U)
								{
									if (text == "no")
									{
										num2 = 1044;
										goto IL_1B49;
									}
								}
							}
							else if (text == "co")
							{
								num2 = 1155;
								goto IL_1B49;
							}
						}
						else if (num != 1648867611U)
						{
							if (num == 1649706254U)
							{
								if (text == "ro")
								{
									num2 = 1048;
									goto IL_1B49;
								}
							}
						}
						else if (text == "wo")
						{
							num2 = 1160;
							goto IL_1B49;
						}
					}
					else if (num <= 1664512397U)
					{
						if (num != 1650441729U)
						{
							if (num == 1664512397U)
							{
								if (text == "nn")
								{
									num2 = 2068;
									goto IL_1B49;
								}
							}
						}
						else if (text == "yo")
						{
							num2 = 1130;
							goto IL_1B49;
						}
					}
					else if (num != 1680010088U)
					{
						if (num != 1680473867U)
						{
							if (num == 1748694682U)
							{
								if (text == "hi")
								{
									num2 = 1081;
									goto IL_1B49;
								}
							}
						}
						else if (text == "iu-latn")
						{
							num2 = 2141;
							goto IL_1B49;
						}
					}
					else if (text == "fa")
					{
						num2 = 1065;
						goto IL_1B49;
					}
				}
				else if (num <= 2046577884U)
				{
					if (num <= 1816099348U)
					{
						if (num != 1790977000U)
						{
							if (num == 1816099348U)
							{
								if (text == "ja")
								{
									num2 = 1041;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-latn")
						{
							num2 = 5146;
							goto IL_1B49;
						}
					}
					else if (num != 1848919111U)
					{
						if (num == 2046577884U)
						{
							if (text == "kok")
							{
								num2 = 1111;
								goto IL_1B49;
							}
						}
					}
					else if (text == "oc")
					{
						num2 = 1154;
						goto IL_1B49;
					}
				}
				else
				{
					if (num <= 2197937899U)
					{
						if (num != 2180460995U)
						{
							if (num != 2197937899U)
							{
								goto IL_1B38;
							}
							if (!(text == "zh-hant"))
							{
								goto IL_1B38;
							}
						}
						else if (!(text == "zh-cht"))
						{
							goto IL_1B38;
						}
						num2 = 3076;
						goto IL_1B49;
					}
					if (num != 2264349090U)
					{
						if (num != 2281825994U)
						{
							if (num != 3012500870U)
							{
								goto IL_1B38;
							}
							if (!(text == "sr-latn"))
							{
								goto IL_1B38;
							}
							num2 = 9242;
							goto IL_1B49;
						}
						else if (!(text == "zh-hans"))
						{
							goto IL_1B38;
						}
					}
					else if (!(text == "zh-chs"))
					{
						goto IL_1B38;
					}
					num2 = 2052;
					goto IL_1B49;
				}
			}
			else if (num <= 3795602801U)
			{
				if (num <= 3294142633U)
				{
					if (num <= 3224459074U)
					{
						if (num != 3174420263U)
						{
							if (num == 3224459074U)
							{
								if (text == "tzm")
								{
									num2 = 2143;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-cyrl")
						{
							num2 = 8218;
							goto IL_1B49;
						}
					}
					else if (num != 3240320582U)
					{
						if (num == 3294142633U)
						{
							if (text == "syr")
							{
								num2 = 1114;
								goto IL_1B49;
							}
						}
					}
					else if (text == "dsb")
					{
						num2 = 2094;
						goto IL_1B49;
					}
				}
				else if (num <= 3659307299U)
				{
					if (num != 3336872436U)
					{
						if (num == 3659307299U)
						{
							if (text == "sah")
							{
								num2 = 1157;
								goto IL_1B49;
							}
						}
					}
					else if (text == "fil")
					{
						num2 = 1124;
						goto IL_1B49;
					}
				}
				else if (num != 3678056394U)
				{
					if (num != 3761944489U)
					{
						if (num == 3795602801U)
						{
							if (text == "sr-cyrl")
							{
								num2 = 10266;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smn")
					{
						num2 = 9275;
						goto IL_1B49;
					}
				}
				else if (text == "sms")
				{
					num2 = 8251;
					goto IL_1B49;
				}
			}
			else if (num <= 3953034599U)
			{
				if (num <= 3912943060U)
				{
					if (num != 3829054965U)
					{
						if (num == 3912943060U)
						{
							if (text == "sma")
							{
								num2 = 7227;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smj")
					{
						num2 = 5179;
						goto IL_1B49;
					}
				}
				else if (num != 3918412059U)
				{
					if (num == 3953034599U)
					{
						if (text == "moh")
						{
							num2 = 1148;
							goto IL_1B49;
						}
					}
				}
				else if (text == "uz-cyrl")
				{
					num2 = 2115;
					goto IL_1B49;
				}
			}
			else if (num <= 4041297251U)
			{
				if (num != 3999162536U)
				{
					if (num == 4041297251U)
					{
						if (text == "quz")
						{
							num2 = 1131;
							goto IL_1B49;
						}
					}
				}
				else if (text == "az-latn")
				{
					num2 = 1068;
					goto IL_1B49;
				}
			}
			else if (num != 4103207754U)
			{
				if (num != 4276183917U)
				{
					if (num == 4280271688U)
					{
						if (text == "ha-latn")
						{
							num2 = 1128;
							goto IL_1B49;
						}
					}
				}
				else if (text == "qut")
				{
					num2 = 1158;
					goto IL_1B49;
				}
			}
			else if (text == "hsb")
			{
				num2 = 1070;
				goto IL_1B49;
			}
			IL_1B38:
			throw new NotImplementedException("Mapping for neutral culture " + name);
			IL_1B49:
			return new CultureInfo(num2);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000D9C20 File Offset: 0x000D7E20
		internal int CalendarType
		{
			get
			{
				switch (this.default_calendar_type >> 8)
				{
				case 1:
					return 1;
				case 2:
					return 7;
				case 3:
					return 23;
				case 4:
					return 6;
				default:
					throw new NotImplementedException("CalendarType");
				}
			}
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D9C64 File Offset: 0x000D7E64
		private static Calendar CreateCalendar(int calendarType)
		{
			string text;
			switch (calendarType >> 8)
			{
			case 1:
				return new GregorianCalendar((GregorianCalendarTypes)(calendarType & 255));
			case 2:
				text = "System.Globalization.ThaiBuddhistCalendar";
				break;
			case 3:
				text = "System.Globalization.UmAlQuraCalendar";
				break;
			case 4:
				text = "System.Globalization.HijriCalendar";
				break;
			default:
				throw new NotImplementedException("Unknown calendar type: " + calendarType.ToString());
			}
			Type type = Type.GetType(text, false);
			if (type == null)
			{
				return new GregorianCalendar(GregorianCalendarTypes.Localized);
			}
			return (Calendar)Activator.CreateInstance(type);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D9CF0 File Offset: 0x000D7EF0
		private static Exception CreateNotFoundException(string name)
		{
			return new CultureNotFoundException("name", "Culture name " + name + " is not supported.");
		}

		/// <summary>Gets or sets the default culture for threads in the current application domain.</summary>
		/// <returns>The default culture for threads in the current application domain, or null if the current system culture is the default thread culture in the application domain.</returns>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000D9D0C File Offset: 0x000D7F0C
		// (set) Token: 0x0600373C RID: 14140 RVA: 0x000D9D15 File Offset: 0x000D7F15
		public static CultureInfo DefaultThreadCurrentCulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the default UI culture for threads in the current application domain.</summary>
		/// <returns>The default UI culture for threads in the current application domain, or null if the current system UI culture is the default thread UI culture in the application domain.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the <see cref="P:System.Globalization.CultureInfo.Name" /> property value is invalid. </exception>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000D9D1F File Offset: 0x000D7F1F
		// (set) Token: 0x0600373E RID: 14142 RVA: 0x000D9D28 File Offset: 0x000D7F28
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000D6C03 File Offset: 0x000D4E03
		internal string SortName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000D9D32 File Offset: 0x000D7F32
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentUICulture();
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		// Token: 0x06003742 RID: 14146
		[DllImport("__Internal")]
		private static extern void InitializeUserPreferredCultureInfoInAppX(CultureInfo.OnCultureInfoChangedDelegate onCultureInfoChangedInAppX);

		// Token: 0x06003743 RID: 14147
		[DllImport("__Internal")]
		private static extern void SetUserPreferredCultureInfoInAppX([MarshalAs(UnmanagedType.LPWStr)] string name);

		// Token: 0x06003744 RID: 14148 RVA: 0x000D9D39 File Offset: 0x000D7F39
		[MonoPInvokeCallback(typeof(CultureInfo.OnCultureInfoChangedDelegate))]
		private static void OnCultureInfoChangedInAppX([MarshalAs(UnmanagedType.LPWStr)] string language)
		{
			if (language != null)
			{
				CultureInfo.s_UserPreferredCultureInfoInAppX = new CultureInfo(language);
				return;
			}
			CultureInfo.s_UserPreferredCultureInfoInAppX = null;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000D9D50 File Offset: 0x000D7F50
		internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
		{
			if (CultureInfo.s_UserPreferredCultureInfoInAppX == null)
			{
				CultureInfo.InitializeUserPreferredCultureInfoInAppX(new CultureInfo.OnCultureInfoChangedDelegate(CultureInfo.OnCultureInfoChangedInAppX));
			}
			return CultureInfo.s_UserPreferredCultureInfoInAppX;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000D9D6F File Offset: 0x000D7F6F
		internal static void SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo cultureInfo)
		{
			if (CultureInfo.s_UserPreferredCultureInfoInAppX == null)
			{
				CultureInfo.InitializeUserPreferredCultureInfoInAppX(new CultureInfo.OnCultureInfoChangedDelegate(CultureInfo.OnCultureInfoChangedInAppX));
			}
			CultureInfo.SetUserPreferredCultureInfoInAppX(cultureInfo.Name);
			CultureInfo.s_UserPreferredCultureInfoInAppX = cultureInfo;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D9D9C File Offset: 0x000D7F9C
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot set sub-classed {0} object to {1} object."), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000D9DF0 File Offset: 0x000D7FF0
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D9E08 File Offset: 0x000D8008
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.", new object[] { cultureName }));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D9E60 File Offset: 0x000D8060
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x04001D9B RID: 7579
		private static volatile CultureInfo invariant_culture_info = new CultureInfo(127, false, true);

		// Token: 0x04001D9C RID: 7580
		private static object shared_table_lock = new object();

		// Token: 0x04001D9D RID: 7581
		private static CultureInfo default_current_culture;

		// Token: 0x04001D9E RID: 7582
		private bool m_isReadOnly;

		// Token: 0x04001D9F RID: 7583
		private int cultureID;

		// Token: 0x04001DA0 RID: 7584
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x04001DA1 RID: 7585
		[NonSerialized]
		private int datetime_index;

		// Token: 0x04001DA2 RID: 7586
		[NonSerialized]
		private int number_index;

		// Token: 0x04001DA3 RID: 7587
		[NonSerialized]
		private int default_calendar_type;

		// Token: 0x04001DA4 RID: 7588
		private bool m_useUserOverride;

		// Token: 0x04001DA5 RID: 7589
		internal volatile NumberFormatInfo numInfo;

		// Token: 0x04001DA6 RID: 7590
		internal volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x04001DA7 RID: 7591
		private volatile TextInfo textInfo;

		// Token: 0x04001DA8 RID: 7592
		internal string m_name;

		// Token: 0x04001DA9 RID: 7593
		[NonSerialized]
		private string englishname;

		// Token: 0x04001DAA RID: 7594
		[NonSerialized]
		private string nativename;

		// Token: 0x04001DAB RID: 7595
		[NonSerialized]
		private string iso3lang;

		// Token: 0x04001DAC RID: 7596
		[NonSerialized]
		private string iso2lang;

		// Token: 0x04001DAD RID: 7597
		[NonSerialized]
		private string win3lang;

		// Token: 0x04001DAE RID: 7598
		[NonSerialized]
		private string territory;

		// Token: 0x04001DAF RID: 7599
		[NonSerialized]
		private string[] native_calendar_names;

		// Token: 0x04001DB0 RID: 7600
		private volatile CompareInfo compareInfo;

		// Token: 0x04001DB1 RID: 7601
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x04001DB2 RID: 7602
		private int m_dataItem;

		// Token: 0x04001DB3 RID: 7603
		private Calendar calendar;

		// Token: 0x04001DB4 RID: 7604
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x04001DB5 RID: 7605
		[NonSerialized]
		private bool constructed;

		// Token: 0x04001DB6 RID: 7606
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x04001DB7 RID: 7607
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x04001DB8 RID: 7608
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x04001DB9 RID: 7609
		internal const int InvariantCultureId = 127;

		// Token: 0x04001DBA RID: 7610
		private const int CalendarTypeBits = 8;

		// Token: 0x04001DBB RID: 7611
		internal const int LOCALE_INVARIANT = 127;

		// Token: 0x04001DBC RID: 7612
		private const string MSG_READONLY = "This instance is read only";

		// Token: 0x04001DBD RID: 7613
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x04001DBE RID: 7614
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x04001DBF RID: 7615
		private static Dictionary<int, CultureInfo> shared_by_number;

		// Token: 0x04001DC0 RID: 7616
		private static Dictionary<string, CultureInfo> shared_by_name;

		// Token: 0x04001DC1 RID: 7617
		private static CultureInfo s_UserPreferredCultureInfoInAppX;

		// Token: 0x04001DC2 RID: 7618
		internal static readonly bool IsTaiwanSku;

		// Token: 0x020006CB RID: 1739
		private struct Data
		{
			// Token: 0x04001DC3 RID: 7619
			public int ansi;

			// Token: 0x04001DC4 RID: 7620
			public int ebcdic;

			// Token: 0x04001DC5 RID: 7621
			public int mac;

			// Token: 0x04001DC6 RID: 7622
			public int oem;

			// Token: 0x04001DC7 RID: 7623
			public bool right_to_left;

			// Token: 0x04001DC8 RID: 7624
			public byte list_sep;
		}

		// Token: 0x020006CC RID: 1740
		// (Invoke) Token: 0x0600374D RID: 14157
		private delegate void OnCultureInfoChangedDelegate([MarshalAs(UnmanagedType.LPWStr)] string language);
	}
}
