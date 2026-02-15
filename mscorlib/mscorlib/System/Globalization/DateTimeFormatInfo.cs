using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information about the format of date and time values.</summary>
	// Token: 0x02000693 RID: 1683
	[Serializable]
	public sealed class DateTimeFormatInfo : IFormatProvider, ICloneable
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000C9925 File Offset: 0x000C7B25
		private string CultureName
		{
			get
			{
				if (this._name == null)
				{
					this._name = this._cultureData.CultureName;
				}
				return this._name;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000C9946 File Offset: 0x000C7B46
		private CultureInfo Culture
		{
			get
			{
				if (this._cultureInfo == null)
				{
					this._cultureInfo = CultureInfo.GetCultureInfo(this.CultureName);
				}
				return this._cultureInfo;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000C9967 File Offset: 0x000C7B67
		private string LanguageName
		{
			get
			{
				if (this._langName == null)
				{
					this._langName = this._cultureData.SISO639LANGNAME;
				}
				return this._langName;
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000C9988 File Offset: 0x000C7B88
		private string[] internalGetAbbreviatedDayOfWeekNames()
		{
			return this.abbreviatedDayNames ?? this.internalGetAbbreviatedDayOfWeekNamesCore();
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000C999A File Offset: 0x000C7B9A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedDayOfWeekNamesCore()
		{
			this.abbreviatedDayNames = this._cultureData.AbbreviatedDayNames(this.Calendar.ID);
			return this.abbreviatedDayNames;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000C99BE File Offset: 0x000C7BBE
		private string[] internalGetDayOfWeekNames()
		{
			return this.dayNames ?? this.internalGetDayOfWeekNamesCore();
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000C99D0 File Offset: 0x000C7BD0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetDayOfWeekNamesCore()
		{
			this.dayNames = this._cultureData.DayNames(this.Calendar.ID);
			return this.dayNames;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000C99F4 File Offset: 0x000C7BF4
		private string[] internalGetAbbreviatedMonthNames()
		{
			return this.abbreviatedMonthNames ?? this.internalGetAbbreviatedMonthNamesCore();
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000C9A06 File Offset: 0x000C7C06
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedMonthNamesCore()
		{
			this.abbreviatedMonthNames = this._cultureData.AbbreviatedMonthNames(this.Calendar.ID);
			return this.abbreviatedMonthNames;
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000C9A2A File Offset: 0x000C7C2A
		private string[] internalGetMonthNames()
		{
			return this.monthNames ?? this.internalGetMonthNamesCore();
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000C9A3C File Offset: 0x000C7C3C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetMonthNamesCore()
		{
			this.monthNames = this._cultureData.MonthNames(this.Calendar.ID);
			return this.monthNames;
		}

		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x060034C6 RID: 13510 RVA: 0x000C9A60 File Offset: 0x000C7C60
		public DateTimeFormatInfo()
		{
			this._cultureData = CultureInfo.InvariantCulture._cultureData;
			this.calendar = GregorianCalendar.GetDefaultInstance();
			this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000C9ABA File Offset: 0x000C7CBA
		internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
		{
			this._cultureData = cultureData;
			this.Calendar = cal;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000C9AE8 File Offset: 0x000C7CE8
		private void InitializeOverridableProperties(CultureData cultureData, int calendarId)
		{
			if (this.firstDayOfWeek == -1)
			{
				this.firstDayOfWeek = cultureData.IFIRSTDAYOFWEEK;
			}
			if (this.calendarWeekRule == -1)
			{
				this.calendarWeekRule = cultureData.IFIRSTWEEKOFYEAR;
			}
			if (this.amDesignator == null)
			{
				this.amDesignator = cultureData.SAM1159;
			}
			if (this.pmDesignator == null)
			{
				this.pmDesignator = cultureData.SPM2359;
			}
			if (this.timeSeparator == null)
			{
				this.timeSeparator = cultureData.TimeSeparator;
			}
			if (this.dateSeparator == null)
			{
				this.dateSeparator = cultureData.DateSeparator(calendarId);
			}
			this.allLongTimePatterns = this._cultureData.LongTimes;
			this.allShortTimePatterns = this._cultureData.ShortTimes;
			this.allLongDatePatterns = cultureData.LongDates(calendarId);
			this.allShortDatePatterns = cultureData.ShortDates(calendarId);
			this.allYearMonthPatterns = cultureData.YearMonths(calendarId);
		}

		/// <summary>Gets the default read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only object that is culture-independent (invariant).</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000C9BB9 File Offset: 0x000C7DB9
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.s_invariantInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
					dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
					dateTimeFormatInfo._isReadOnly = true;
					DateTimeFormatInfo.s_invariantInfo = dateTimeFormatInfo;
				}
				return DateTimeFormatInfo.s_invariantInfo;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object based on the <see cref="T:System.Globalization.CultureInfo" /> object for the current thread.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000C9BEC File Offset: 0x000C7DEC
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (!currentCulture._isInherited)
				{
					DateTimeFormatInfo dateTimeInfo = currentCulture.dateTimeInfo;
					if (dateTimeInfo != null)
					{
						return dateTimeInfo;
					}
				}
				return (DateTimeFormatInfo)currentCulture.GetFormat(typeof(DateTimeFormatInfo));
			}
		}

		/// <summary>Returns the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> that gets the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.-or- null to get <see cref="P:System.Globalization.DateTimeFormatInfo.CurrentInfo" />. </param>
		// Token: 0x060034CB RID: 13515 RVA: 0x000C9C2C File Offset: 0x000C7E2C
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			if (provider == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo._isInherited)
			{
				return cultureInfo.DateTimeFormat;
			}
			DateTimeFormatInfo dateTimeFormatInfo = provider as DateTimeFormatInfo;
			if (dateTimeFormatInfo != null)
			{
				return dateTimeFormatInfo;
			}
			DateTimeFormatInfo dateTimeFormatInfo2 = provider.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo;
			if (dateTimeFormatInfo2 == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			return dateTimeFormatInfo2;
		}

		/// <summary>Returns an object of the specified type that provides a date and time  formatting service.</summary>
		/// <returns>The current  object, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.DateTimeFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The type of the required formatting service. </param>
		// Token: 0x060034CC RID: 13516 RVA: 0x000C9C87 File Offset: 0x000C7E87
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(DateTimeFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.DateTimeFormatInfo" /> object copied from the original <see cref="T:System.Globalization.DateTimeFormatInfo" />.</returns>
		// Token: 0x060034CD RID: 13517 RVA: 0x000C9C9E File Offset: 0x000C7E9E
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.calendar = (Calendar)this.Calendar.Clone();
			dateTimeFormatInfo._isReadOnly = false;
			return dateTimeFormatInfo;
		}

		/// <summary>Gets or sets the string designator for hours that are "ante meridiem" (before noon).</summary>
		/// <returns>The string designator for hours that are ante meridiem. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "AM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000C9CC8 File Offset: 0x000C7EC8
		public string AMDesignator
		{
			get
			{
				if (this.amDesignator == null)
				{
					this.amDesignator = this._cultureData.SAM1159;
				}
				return this.amDesignator;
			}
		}

		/// <summary>Gets or sets the calendar to use for the current culture.</summary>
		/// <returns>The calendar to use for the current culture. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is a <see cref="T:System.Globalization.GregorianCalendar" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a <see cref="T:System.Globalization.Calendar" /> object that is not valid for the current culture. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000C9CE9 File Offset: 0x000C7EE9
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x000C9CF4 File Offset: 0x000C7EF4
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			set
			{
				if (GlobalizationMode.Invariant)
				{
					throw new PlatformNotSupportedException();
				}
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Object cannot be null.");
				}
				if (value == this.calendar)
				{
					return;
				}
				for (int i = 0; i < this.OptionalCalendars.Length; i++)
				{
					if (this.OptionalCalendars[i] == (CalendarId)value.ID)
					{
						if (this.calendar != null)
						{
							this.m_eraNames = null;
							this.m_abbrevEraNames = null;
							this.m_abbrevEnglishEraNames = null;
							this.monthDayPattern = null;
							this.dayNames = null;
							this.abbreviatedDayNames = null;
							this.m_superShortDayNames = null;
							this.monthNames = null;
							this.abbreviatedMonthNames = null;
							this.genitiveMonthNames = null;
							this.m_genitiveAbbreviatedMonthNames = null;
							this.leapYearMonthNames = null;
							this.formatFlags = DateTimeFormatFlags.NotInitialized;
							this.allShortDatePatterns = null;
							this.allLongDatePatterns = null;
							this.allYearMonthPatterns = null;
							this.dateTimeOffsetPattern = null;
							this.longDatePattern = null;
							this.shortDatePattern = null;
							this.yearMonthPattern = null;
							this.fullDateTimePattern = null;
							this.generalShortTimePattern = null;
							this.generalLongTimePattern = null;
							this.dateSeparator = null;
							this.ClearTokenHashTable();
						}
						this.calendar = value;
						this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value", "Not a valid calendar for the given culture.");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000C9E52 File Offset: 0x000C8052
		private CalendarId[] OptionalCalendars
		{
			get
			{
				if (this.optionalCalendars == null)
				{
					this.optionalCalendars = this._cultureData.GetCalendarIds();
				}
				return this.optionalCalendars;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000C9E73 File Offset: 0x000C8073
		internal string[] EraNames
		{
			get
			{
				if (this.m_eraNames == null)
				{
					this.m_eraNames = this._cultureData.EraNames(this.Calendar.ID);
				}
				return this.m_eraNames;
			}
		}

		/// <summary>Returns the string containing the name of the specified era.</summary>
		/// <returns>A string containing the name of the era.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x060034D3 RID: 13523 RVA: 0x000C9E9F File Offset: 0x000C809F
		public string GetEraName(int era)
		{
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.EraNames.Length && era >= 0)
			{
				return this.m_eraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000C9EDD File Offset: 0x000C80DD
		internal string[] AbbreviatedEraNames
		{
			get
			{
				if (this.m_abbrevEraNames == null)
				{
					this.m_abbrevEraNames = this._cultureData.AbbrevEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEraNames;
			}
		}

		/// <summary>Returns the string containing the abbreviated name of the specified era, if an abbreviation exists.</summary>
		/// <returns>A string containing the abbreviated name of the specified era, if an abbreviation exists.-or- A string containing the full name of the era, if an abbreviation does not exist.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x060034D5 RID: 13525 RVA: 0x000C9F0C File Offset: 0x000C810C
		public string GetAbbreviatedEraName(int era)
		{
			if (this.AbbreviatedEraNames.Length == 0)
			{
				return this.GetEraName(era);
			}
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.m_abbrevEraNames.Length && era >= 0)
			{
				return this.m_abbrevEraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000C9F66 File Offset: 0x000C8166
		internal string[] AbbreviatedEnglishEraNames
		{
			get
			{
				if (this.m_abbrevEnglishEraNames == null)
				{
					this.m_abbrevEnglishEraNames = this._cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEnglishEraNames;
			}
		}

		/// <summary>Gets or sets the string that separates the components of a date, that is, the year, month, and day.</summary>
		/// <returns>The string that separates the components of a date, that is, the year, month, and day. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "/".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000C9F92 File Offset: 0x000C8192
		public string DateSeparator
		{
			get
			{
				if (this.dateSeparator == null)
				{
					this.dateSeparator = this._cultureData.DateSeparator(this.Calendar.ID);
				}
				return this.dateSeparator;
			}
		}

		/// <summary>Gets or sets the custom format string for a long date and long time value.</summary>
		/// <returns>The custom format string for a long date and long time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000C9FBE File Offset: 0x000C81BE
		public string FullDateTimePattern
		{
			get
			{
				if (this.fullDateTimePattern == null)
				{
					this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern;
				}
				return this.fullDateTimePattern;
			}
		}

		/// <summary>Gets or sets the custom format string for a long date value.</summary>
		/// <returns>The custom format string for a long date value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000C9FEA File Offset: 0x000C81EA
		public string LongDatePattern
		{
			get
			{
				if (this.longDatePattern == null)
				{
					this.longDatePattern = this.UnclonedLongDatePatterns[0];
				}
				return this.longDatePattern;
			}
		}

		/// <summary>Gets or sets the custom format string for a long time value.</summary>
		/// <returns>The format pattern for a long time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x000CA008 File Offset: 0x000C8208
		public string LongTimePattern
		{
			get
			{
				if (this.longTimePattern == null)
				{
					this.longTimePattern = this.UnclonedLongTimePatterns[0];
				}
				return this.longTimePattern;
			}
		}

		/// <summary>Gets or sets the custom format string for a month and day value.</summary>
		/// <returns>The custom format string for a month and day value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000CA026 File Offset: 0x000C8226
		public string MonthDayPattern
		{
			get
			{
				if (this.monthDayPattern == null)
				{
					this.monthDayPattern = this._cultureData.MonthDay(this.Calendar.ID);
				}
				return this.monthDayPattern;
			}
		}

		/// <summary>Gets or sets the string designator for hours that are "post meridiem" (after noon).</summary>
		/// <returns>The string designator for hours that are "post meridiem" (after noon). The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "PM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000CA052 File Offset: 0x000C8252
		public string PMDesignator
		{
			get
			{
				if (this.pmDesignator == null)
				{
					this.pmDesignator = this._cultureData.SPM2359;
				}
				return this.pmDesignator;
			}
		}

		/// <summary>Gets the custom format string for a time value that is based on the Internet Engineering Task Force (IETF) Request for Comments (RFC) 1123 specification.</summary>
		/// <returns>The custom format string for a time value that is based on the IETF RFC 1123 specification.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000CA073 File Offset: 0x000C8273
		public string RFC1123Pattern
		{
			get
			{
				return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			}
		}

		/// <summary>Gets or sets the custom format string for a short date value.</summary>
		/// <returns>The custom format string for a short date value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000CA07A File Offset: 0x000C827A
		public string ShortDatePattern
		{
			get
			{
				if (this.shortDatePattern == null)
				{
					this.shortDatePattern = this.UnclonedShortDatePatterns[0];
				}
				return this.shortDatePattern;
			}
		}

		/// <summary>Gets or sets the custom format string for a short time value.</summary>
		/// <returns>The custom format string for a short time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000CA098 File Offset: 0x000C8298
		public string ShortTimePattern
		{
			get
			{
				if (this.shortTimePattern == null)
				{
					this.shortTimePattern = this.UnclonedShortTimePatterns[0];
				}
				return this.shortTimePattern;
			}
		}

		/// <summary>Gets the custom format string for a sortable date and time value.</summary>
		/// <returns>The custom format string for a sortable date and time value.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000CA0B6 File Offset: 0x000C82B6
		public string SortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000CA0BD File Offset: 0x000C82BD
		internal string GeneralShortTimePattern
		{
			get
			{
				if (this.generalShortTimePattern == null)
				{
					this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern;
				}
				return this.generalShortTimePattern;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000CA0E9 File Offset: 0x000C82E9
		internal string GeneralLongTimePattern
		{
			get
			{
				if (this.generalLongTimePattern == null)
				{
					this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern;
				}
				return this.generalLongTimePattern;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000CA118 File Offset: 0x000C8318
		internal string DateTimeOffsetPattern
		{
			get
			{
				if (this.dateTimeOffsetPattern == null)
				{
					string text = this.ShortDatePattern + " " + this.LongTimePattern;
					bool flag = false;
					bool flag2 = false;
					char c = '\'';
					int num = 0;
					while (!flag && num < this.LongTimePattern.Length)
					{
						char c2 = this.LongTimePattern[num];
						if (c2 <= '%')
						{
							if (c2 == '"')
							{
								goto IL_006A;
							}
							if (c2 == '%')
							{
								goto IL_0096;
							}
						}
						else
						{
							if (c2 == '\'')
							{
								goto IL_006A;
							}
							if (c2 == '\\')
							{
								goto IL_0096;
							}
							if (c2 == 'z')
							{
								flag = !flag2;
							}
						}
						IL_009C:
						num++;
						continue;
						IL_006A:
						if (flag2 && c == this.LongTimePattern[num])
						{
							flag2 = false;
							goto IL_009C;
						}
						if (!flag2)
						{
							c = this.LongTimePattern[num];
							flag2 = true;
							goto IL_009C;
						}
						goto IL_009C;
						IL_0096:
						num++;
						goto IL_009C;
					}
					if (!flag)
					{
						text += " zzz";
					}
					this.dateTimeOffsetPattern = text;
				}
				return this.dateTimeOffsetPattern;
			}
		}

		/// <summary>Gets or sets the string that separates the components of time, that is, the hour, minutes, and seconds.</summary>
		/// <returns>The string that separates the components of time. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is ":".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000CA1F8 File Offset: 0x000C83F8
		public string TimeSeparator
		{
			get
			{
				if (this.timeSeparator == null)
				{
					this.timeSeparator = this._cultureData.TimeSeparator;
				}
				return this.timeSeparator;
			}
		}

		/// <summary>Gets the custom format string for a universal, sortable date and time string.</summary>
		/// <returns>The custom format string for a universal, sortable date and time string.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000CA219 File Offset: 0x000C8419
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			}
		}

		/// <summary>Gets or sets the custom format string for a year and month value.</summary>
		/// <returns>The custom format string for a year and month value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000CA220 File Offset: 0x000C8420
		public string YearMonthPattern
		{
			get
			{
				if (this.yearMonthPattern == null)
				{
					this.yearMonthPattern = this.UnclonedYearMonthPatterns[0];
				}
				return this.yearMonthPattern;
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", and "Sat".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000CA23E File Offset: 0x000C843E
		public string[] AbbreviatedDayNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedDayOfWeekNames().Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional string array that contains the culture-specific full names of the days of the week.</summary>
		/// <returns>A one-dimensional string array that contains the culture-specific full names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", and "Saturday".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000CA250 File Offset: 0x000C8450
		public string[] DayNames
		{
			get
			{
				return (string[])this.internalGetDayOfWeekNames().Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional string array that contains the culture-specific abbreviated names of the months.</summary>
		/// <returns>A one-dimensional string array with 13 elements that contains the culture-specific abbreviated names of the months. For 12-month calendars, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000CA262 File Offset: 0x000C8462
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedMonthNames().Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months. In a 12-month calendar, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000CA274 File Offset: 0x000C8474
		public string[] MonthNames
		{
			get
			{
				return (string[])this.internalGetMonthNames().Clone();
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000CA286 File Offset: 0x000C8486
		internal bool HasSpacesInMonthNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000CA293 File Offset: 0x000C8493
		internal bool HasSpacesInDayNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000CA2A4 File Offset: 0x000C84A4
		internal string internalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
		{
			string[] array;
			if (style != MonthNameStyles.Genitive)
			{
				if (style != MonthNameStyles.LeapYear)
				{
					array = (abbreviated ? this.internalGetAbbreviatedMonthNames() : this.internalGetMonthNames());
				}
				else
				{
					array = this.internalGetLeapYearMonthNames();
				}
			}
			else
			{
				array = this.internalGetGenitiveMonthNames(abbreviated);
			}
			if (month < 1 || month > array.Length)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, array.Length));
			}
			return array[month - 1];
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000CA314 File Offset: 0x000C8514
		private string[] internalGetGenitiveMonthNames(bool abbreviated)
		{
			if (abbreviated)
			{
				if (this.m_genitiveAbbreviatedMonthNames == null)
				{
					this.m_genitiveAbbreviatedMonthNames = this._cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
				}
				return this.m_genitiveAbbreviatedMonthNames;
			}
			if (this.genitiveMonthNames == null)
			{
				this.genitiveMonthNames = this._cultureData.GenitiveMonthNames(this.Calendar.ID);
			}
			return this.genitiveMonthNames;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000CA379 File Offset: 0x000C8579
		internal string[] internalGetLeapYearMonthNames()
		{
			if (this.leapYearMonthNames == null)
			{
				this.leapYearMonthNames = this._cultureData.LeapYearMonthNames(this.Calendar.ID);
			}
			return this.leapYearMonthNames;
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x060034F0 RID: 13552 RVA: 0x000CA3A5 File Offset: 0x000C85A5
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetAbbreviatedDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000CA3D8 File Offset: 0x000C85D8
		private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
		{
			string[] array = new string[patterns1.Length * patterns2.Length];
			int num = 0;
			for (int i = 0; i < patterns1.Length; i++)
			{
				for (int j = 0; j < patterns2.Length; j++)
				{
					array[num++] = patterns1[i] + connectString + patterns2[j];
				}
			}
			return array;
		}

		/// <summary>Returns all the patterns in which date and time values can be formatted using the specified standard format string.</summary>
		/// <returns>An array containing the standard patterns in which date and time values can be formatted using the specified format string.</returns>
		/// <param name="format">A standard format string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="format" /> is not a valid standard format string. </exception>
		// Token: 0x060034F2 RID: 13554 RVA: 0x000CA424 File Offset: 0x000C8624
		public string[] GetAllDateTimePatterns(char format)
		{
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
					return this.AllLongDatePatterns;
				case 'E':
					goto IL_01AF;
				case 'F':
					break;
				case 'G':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
				default:
					switch (format)
					{
					case 'M':
						goto IL_013D;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_01AF;
					case 'O':
						goto IL_014F;
					case 'R':
						goto IL_0160;
					case 'T':
						return this.AllLongTimePatterns;
					case 'U':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
				return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
			}
			if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
					return this.AllShortDatePatterns;
				case 'e':
					goto IL_01AF;
				case 'f':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
				case 'g':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
				default:
					switch (format)
					{
					case 'm':
						goto IL_013D;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_01AF;
					case 'o':
						goto IL_014F;
					case 'r':
						goto IL_0160;
					case 's':
						return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss" };
					case 't':
						return this.AllShortTimePatterns;
					case 'u':
						return new string[] { this.UniversalSortableDateTimePattern };
					case 'y':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
			}
			return this.AllYearMonthPatterns;
			IL_013D:
			return new string[] { this.MonthDayPattern };
			IL_014F:
			return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK" };
			IL_0160:
			return new string[] { "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'" };
			IL_01AF:
			throw new ArgumentException(SR.Format("Format specifier '{0}' was invalid.", format), "format");
		}

		/// <summary>Returns the culture-specific full name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x060034F3 RID: 13555 RVA: 0x000CA5FC File Offset: 0x000C87FC
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetDayOfWeekNames()[(int)dayofweek];
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x060034F4 RID: 13556 RVA: 0x000CA62F File Offset: 0x000C882F
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetAbbreviatedMonthNames()[month - 1];
		}

		/// <summary>Returns the culture-specific full name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x060034F5 RID: 13557 RVA: 0x000CA666 File Offset: 0x000C8866
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetMonthNames()[month - 1];
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000CA6A0 File Offset: 0x000C88A0
		private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
		{
			if (defaultPattern == patterns[0])
			{
				return (string[])patterns.Clone();
			}
			int num = 0;
			while (num < patterns.Length && !(defaultPattern == patterns[num]))
			{
				num++;
			}
			string[] array;
			if (num < patterns.Length)
			{
				array = (string[])patterns.Clone();
				array[num] = array[0];
			}
			else
			{
				array = new string[patterns.Length + 1];
				Array.Copy(patterns, 0, array, 1, patterns.Length);
			}
			array[0] = defaultPattern;
			return array;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000CA713 File Offset: 0x000C8913
		private string[] AllYearMonthPatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000CA726 File Offset: 0x000C8926
		private string[] AllShortDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000CA739 File Offset: 0x000C8939
		private string[] AllShortTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000CA74C File Offset: 0x000C894C
		private string[] AllLongDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000CA75F File Offset: 0x000C895F
		private string[] AllLongTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000CA772 File Offset: 0x000C8972
		private string[] UnclonedYearMonthPatterns
		{
			get
			{
				if (this.allYearMonthPatterns == null)
				{
					this.allYearMonthPatterns = this._cultureData.YearMonths(this.Calendar.ID);
				}
				return this.allYearMonthPatterns;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000CA79E File Offset: 0x000C899E
		private string[] UnclonedShortDatePatterns
		{
			get
			{
				if (this.allShortDatePatterns == null)
				{
					this.allShortDatePatterns = this._cultureData.ShortDates(this.Calendar.ID);
				}
				return this.allShortDatePatterns;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000CA7CA File Offset: 0x000C89CA
		private string[] UnclonedLongDatePatterns
		{
			get
			{
				if (this.allLongDatePatterns == null)
				{
					this.allLongDatePatterns = this._cultureData.LongDates(this.Calendar.ID);
				}
				return this.allLongDatePatterns;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000CA7F6 File Offset: 0x000C89F6
		private string[] UnclonedShortTimePatterns
		{
			get
			{
				if (this.allShortTimePatterns == null)
				{
					this.allShortTimePatterns = this._cultureData.ShortTimes;
				}
				return this.allShortTimePatterns;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000CA817 File Offset: 0x000C8A17
		private string[] UnclonedLongTimePatterns
		{
			get
			{
				if (this.allLongTimePatterns == null)
				{
					this.allLongTimePatterns = this._cultureData.LongTimes;
				}
				return this.allLongTimePatterns;
			}
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.</returns>
		/// <param name="dtfi">The <see cref="T:System.Globalization.DateTimeFormatInfo" /> object to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dtfi" /> is null. </exception>
		// Token: 0x06003501 RID: 13569 RVA: 0x000CA838 File Offset: 0x000C8A38
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			if (dtfi == null)
			{
				throw new ArgumentNullException("dtfi", "Object cannot be null.");
			}
			if (dtfi.IsReadOnly)
			{
				return dtfi;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.MemberwiseClone();
			dateTimeFormatInfo.calendar = Calendar.ReadOnly(dtfi.Calendar);
			dateTimeFormatInfo._isReadOnly = true;
			return dateTimeFormatInfo;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000CA885 File Offset: 0x000C8A85
		public bool IsReadOnly
		{
			get
			{
				return GlobalizationMode.Invariant || this._isReadOnly;
			}
		}

		/// <summary>Gets or sets a string array of month names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of month names.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the array is multidimensional or has a length that is not exactly 13.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the array or one of its elements is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000CA896 File Offset: 0x000C8A96
		public string[] MonthGenitiveNames
		{
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(false).Clone();
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000CA8AC File Offset: 0x000C8AAC
		internal string FullTimeSpanPositivePattern
		{
			get
			{
				if (this._fullTimeSpanPositivePattern == null)
				{
					CultureData cultureData;
					if (this._cultureData.UseUserOverride)
					{
						cultureData = CultureData.GetCultureData(this._cultureData.CultureName, false);
					}
					else
					{
						cultureData = this._cultureData;
					}
					string numberDecimalSeparator = new NumberFormatInfo(cultureData).NumberDecimalSeparator;
					this._fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + numberDecimalSeparator + "'FFFFFFF";
				}
				return this._fullTimeSpanPositivePattern;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000CA911 File Offset: 0x000C8B11
		internal string FullTimeSpanNegativePattern
		{
			get
			{
				if (this._fullTimeSpanNegativePattern == null)
				{
					this._fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern;
				}
				return this._fullTimeSpanNegativePattern;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000CA937 File Offset: 0x000C8B37
		internal CompareInfo CompareInfo
		{
			get
			{
				if (this._compareInfo == null)
				{
					this._compareInfo = CompareInfo.GetCompareInfo(this._cultureData.SCOMPAREINFO);
				}
				return this._compareInfo;
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000CA960 File Offset: 0x000C8B60
		internal static void ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException("An undefined DateTimeStyles value is being used.", parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.", parameterName);
			}
			if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles value RoundtripKind cannot be used with the values AssumeLocal, AssumeUniversal or AdjustToUniversal.", parameterName);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000CA9B5 File Offset: 0x000C8BB5
		internal DateTimeFormatFlags FormatFlags
		{
			get
			{
				if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
				{
					return this.InitializeFormatFlags();
				}
				return this.formatFlags;
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CA9D0 File Offset: 0x000C8BD0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DateTimeFormatFlags InitializeFormatFlags()
		{
			this.formatFlags = (DateTimeFormatFlags)(DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames) | DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar(this.Calendar.ID));
			return this.formatFlags;
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000CAA4C File Offset: 0x000C8C4C
		internal bool HasForceTwoDigitYears
		{
			get
			{
				CalendarId calendarId = (CalendarId)this.calendar.ID;
				return calendarId - CalendarId.JAPAN <= 1;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000CAA6F File Offset: 0x000C8C6F
		internal bool HasYearMonthAdjustment
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000CAA7C File Offset: 0x000C8C7C
		internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
		{
			if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
			{
				if (year < 1000)
				{
					year += 5000;
				}
				if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
				{
					return false;
				}
				if (parsedMonthName && !this.Calendar.IsLeapYear(year))
				{
					if (month >= 8)
					{
						month--;
					}
					else if (month == 7)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000CAB04 File Offset: 0x000C8D04
		internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_jajpDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
				dateTimeFormat.Calendar = JapaneseCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_jajpDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000CAB48 File Offset: 0x000C8D48
		internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_zhtwDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("zh-TW", false).DateTimeFormat;
				dateTimeFormat.Calendar = TaiwanCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000CAB8C File Offset: 0x000C8D8C
		private void ClearTokenHashTable()
		{
			this._dtfiTokenHash = null;
			this.formatFlags = DateTimeFormatFlags.NotInitialized;
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000CAB9C File Offset: 0x000C8D9C
		internal DateTimeFormatInfo.TokenHashValue[] CreateTokenHashTable()
		{
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = new DateTimeFormatInfo.TokenHashValue[199];
				if (!GlobalizationMode.Invariant)
				{
					this.LanguageName.Equals("ko");
				}
				string text = this.TimeSeparator.Trim();
				if ("," != text)
				{
					this.InsertHash(array, ",", TokenType.IgnorableSymbol, 0);
				}
				if ("." != text)
				{
					this.InsertHash(array, ".", TokenType.IgnorableSymbol, 0);
				}
				if (!GlobalizationMode.Invariant && "시" != text && "時" != text && "时" != text)
				{
					this.InsertHash(array, this.TimeSeparator, TokenType.SEP_Time, 0);
				}
				this.InsertHash(array, this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, this.PMDesignator, (TokenType)1284, 1);
				bool flag = false;
				if (!GlobalizationMode.Invariant)
				{
					this.PopulateSpecialTokenHashTable(array, ref flag);
				}
				if (!GlobalizationMode.Invariant && this.LanguageName.Equals("ky"))
				{
					this.InsertHash(array, "-", TokenType.IgnorableSymbol, 0);
				}
				else
				{
					this.InsertHash(array, "-", TokenType.SEP_DateOrOffset, 0);
				}
				if (!flag)
				{
					this.InsertHash(array, this.DateSeparator, TokenType.SEP_Date, 0);
				}
				this.AddMonthNames(array, null);
				for (int i = 1; i <= 13; i++)
				{
					this.InsertHash(array, this.GetAbbreviatedMonthName(i), TokenType.MonthToken, i);
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					for (int j = 1; j <= 13; j++)
					{
						string text2 = this.internalGetMonthName(j, MonthNameStyles.Genitive, false);
						this.InsertHash(array, text2, TokenType.MonthToken, j);
					}
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					for (int k = 1; k <= 13; k++)
					{
						string text3 = this.internalGetMonthName(k, MonthNameStyles.LeapYear, false);
						this.InsertHash(array, text3, TokenType.MonthToken, k);
					}
				}
				for (int l = 0; l < 7; l++)
				{
					string text4 = this.GetDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
					text4 = this.GetAbbreviatedDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
				}
				int[] eras = this.calendar.Eras;
				for (int m = 1; m <= eras.Length; m++)
				{
					this.InsertHash(array, this.GetEraName(m), TokenType.EraToken, m);
					this.InsertHash(array, this.GetAbbreviatedEraName(m), TokenType.EraToken, m);
				}
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.PMDesignator, (TokenType)1284, 1);
				for (int n = 1; n <= 12; n++)
				{
					string text5 = DateTimeFormatInfo.InvariantInfo.GetMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
					text5 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
				}
				for (int num = 0; num < 7; num++)
				{
					string text6 = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
					text6 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
				}
				for (int num2 = 0; num2 < this.AbbreviatedEnglishEraNames.Length; num2++)
				{
					this.InsertHash(array, this.AbbreviatedEnglishEraNames[num2], TokenType.EraToken, num2 + 1);
				}
				this.InsertHash(array, "T", TokenType.SEP_LocalTimeMark, 0);
				this.InsertHash(array, "GMT", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "Z", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "/", TokenType.SEP_Date, 0);
				this.InsertHash(array, ":", TokenType.SEP_Time, 0);
				this._dtfiTokenHash = array;
			}
			return array;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000CAF3C File Offset: 0x000C913C
		private void PopulateSpecialTokenHashTable(DateTimeFormatInfo.TokenHashValue[] temp, ref bool useDateSepAsIgnorableSymbol)
		{
			if (this.LanguageName.Equals("sq"))
			{
				this.InsertHash(temp, "." + this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(temp, "." + this.PMDesignator, (TokenType)1284, 1);
			}
			this.InsertHash(temp, "年", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "년", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "月", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "월", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "日", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "일", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "時", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "时", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "分", TokenType.SEP_MinuteSuff, 0);
			this.InsertHash(temp, "秒", TokenType.SEP_SecondSuff, 0);
			if (!AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3)
			{
				this.InsertHash(temp, "元", TokenType.YearNumberToken, 1);
				this.InsertHash(temp, "(", TokenType.IgnorableSymbol, 0);
				this.InsertHash(temp, ")", TokenType.IgnorableSymbol, 0);
			}
			if (this.LanguageName.Equals("ko"))
			{
				this.InsertHash(temp, "시", TokenType.SEP_HourSuff, 0);
				this.InsertHash(temp, "분", TokenType.SEP_MinuteSuff, 0);
				this.InsertHash(temp, "초", TokenType.SEP_SecondSuff, 0);
			}
			string[] dateWordsOfDTFI = new DateTimeFormatInfoScanner().GetDateWordsOfDTFI(this);
			DateTimeFormatFlags dateTimeFormatFlags = this.FormatFlags;
			if (dateWordsOfDTFI != null)
			{
				for (int i = 0; i < dateWordsOfDTFI.Length; i++)
				{
					char c = dateWordsOfDTFI[i][0];
					if (c != '\ue000')
					{
						if (c != '\ue001')
						{
							this.InsertHash(temp, dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							if (this.LanguageName.Equals("eu"))
							{
								this.InsertHash(temp, "." + dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							}
						}
						else
						{
							string text = dateWordsOfDTFI[i].Substring(1);
							this.InsertHash(temp, text, TokenType.IgnorableSymbol, 0);
							if (this.DateSeparator.Trim(null).Equals(text))
							{
								useDateSepAsIgnorableSymbol = true;
							}
						}
					}
					else
					{
						string text2 = dateWordsOfDTFI[i].Substring(1);
						this.AddMonthNames(temp, text2);
					}
				}
			}
			if (this.LanguageName.Equals("ja"))
			{
				for (int j = 0; j < 7; j++)
				{
					string text3 = "(" + this.GetAbbreviatedDayName((DayOfWeek)j) + ")";
					this.InsertHash(temp, text3, TokenType.DayOfWeekToken, j);
				}
				if (!DateTimeFormatInfo.IsJapaneseCalendar(this.Calendar))
				{
					DateTimeFormatInfo japaneseCalendarDTFI = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
					for (int k = 1; k <= japaneseCalendarDTFI.Calendar.Eras.Length; k++)
					{
						this.InsertHash(temp, japaneseCalendarDTFI.GetEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.GetAbbreviatedEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.AbbreviatedEnglishEraNames[k - 1], TokenType.JapaneseEraToken, k);
					}
					return;
				}
			}
			else if (this.CultureName.Equals("zh-TW"))
			{
				DateTimeFormatInfo taiwanCalendarDTFI = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
				for (int l = 1; l <= taiwanCalendarDTFI.Calendar.Eras.Length; l++)
				{
					if (taiwanCalendarDTFI.GetEraName(l).Length > 0)
					{
						this.InsertHash(temp, taiwanCalendarDTFI.GetEraName(l), TokenType.TEraToken, l);
					}
				}
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000CB2B1 File Offset: 0x000C94B1
		private static bool IsJapaneseCalendar(Calendar calendar)
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return calendar.GetType() == typeof(JapaneseCalendar);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000CB2D8 File Offset: 0x000C94D8
		private void AddMonthNames(DateTimeFormatInfo.TokenHashValue[] temp, string monthPostfix)
		{
			for (int i = 1; i <= 13; i++)
			{
				string text = this.GetMonthName(i);
				if (text.Length > 0)
				{
					if (monthPostfix != null)
					{
						this.InsertHash(temp, text + monthPostfix, TokenType.MonthToken, i);
					}
					else
					{
						this.InsertHash(temp, text, TokenType.MonthToken, i);
					}
				}
				text = this.GetAbbreviatedMonthName(i);
				this.InsertHash(temp, text, TokenType.MonthToken, i);
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000CB334 File Offset: 0x000C9534
		private unsafe static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
		{
			number = -1;
			badFormat = false;
			int index = str.Index;
			if (!HebrewNumber.IsDigit((char)(*str.Value[index])))
			{
				return false;
			}
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState;
			for (;;)
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar((char)(*str.Value[index++]), ref hebrewNumberParsingContext);
				if (hebrewNumberParsingState <= HebrewNumberParsingState.NotHebrewDigit)
				{
					break;
				}
				if (index >= str.Value.Length || hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
				{
					goto IL_005C;
				}
			}
			return false;
			IL_005C:
			if (hebrewNumberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				return false;
			}
			str.Advance(index - str.Index);
			number = hebrewNumberParsingContext.result;
			return true;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000CB3BB File Offset: 0x000C95BB
		private static bool IsHebrewChar(char ch)
		{
			return ch >= '\u0590' && ch <= '\u05ff';
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000CB3D4 File Offset: 0x000C95D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsAllowedJapaneseTokenFollowedByNonSpaceLetter(string tokenString, char nextCh)
		{
			return !AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3 && (nextCh == "元"[0] || (tokenString == "元" && nextCh == "年"[0]));
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000CB424 File Offset: 0x000C9624
		internal unsafe bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
		{
			tokenType = TokenType.UnknownToken;
			tokenValue = 0;
			char c = str.m_current;
			bool flag = char.IsLetter(c);
			if (flag)
			{
				c = this.Culture.TextInfo.ToLower(c);
				bool flag2;
				if (!GlobalizationMode.Invariant && DateTimeFormatInfo.IsHebrewChar(c) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out flag2, out tokenValue))
				{
					if (flag2)
					{
						tokenType = TokenType.UnknownToken;
						return false;
					}
					tokenType = TokenType.HebrewNumber;
					return true;
				}
			}
			int num = (int)(c % 'Ç');
			int num2 = (int)('\u0001' + c % 'Å');
			int num3 = str.Length - str.Index;
			int num4 = 0;
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = this.CreateTokenHashTable();
			}
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			int num6;
			for (;;)
			{
				tokenHashValue = array[num];
				if (tokenHashValue == null)
				{
					return false;
				}
				if ((tokenHashValue.tokenType & TokenMask) > (TokenType)0 && tokenHashValue.tokenString.Length <= num3)
				{
					bool flag3 = true;
					if (flag)
					{
						int num5 = str.Index + tokenHashValue.tokenString.Length;
						if (num5 > str.Length)
						{
							flag3 = false;
						}
						else if (num5 < str.Length)
						{
							char c2 = (char)(*str.Value[num5]);
							flag3 = !char.IsLetter(c2) || this.IsAllowedJapaneseTokenFollowedByNonSpaceLetter(tokenHashValue.tokenString, c2);
						}
					}
					if (flag3 && ((tokenHashValue.tokenString.Length == 1 && *str.Value[str.Index] == (ushort)tokenHashValue.tokenString[0]) || this.Culture.CompareInfo.Compare(str.Value.Slice(str.Index, tokenHashValue.tokenString.Length), tokenHashValue.tokenString, CompareOptions.IgnoreCase) == 0))
					{
						break;
					}
					if ((tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames) || (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames))
					{
						num6 = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref num6))
						{
							goto Block_19;
						}
					}
				}
				num4++;
				num += num2;
				if (num >= 199)
				{
					num -= 199;
				}
				if (num4 >= 199)
				{
					return false;
				}
			}
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(tokenHashValue.tokenString.Length);
			return true;
			Block_19:
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(num6);
			return true;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000CB66C File Offset: 0x000C986C
		private void InsertAtCurrentHashNode(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
		{
			DateTimeFormatInfo.TokenHashValue tokenHashValue = hashTable[hashcode];
			hashTable[hashcode] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			while (++pos < 199)
			{
				hashcode += hashProbe;
				if (hashcode >= 199)
				{
					hashcode -= 199;
				}
				DateTimeFormatInfo.TokenHashValue tokenHashValue2 = hashTable[hashcode];
				if (tokenHashValue2 == null || this.Culture.TextInfo.ToLower(tokenHashValue2.tokenString[0]) == ch)
				{
					hashTable[hashcode] = tokenHashValue;
					if (tokenHashValue2 == null)
					{
						return;
					}
					tokenHashValue = tokenHashValue2;
				}
			}
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000CB6E8 File Offset: 0x000C98E8
		private void InsertHash(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
		{
			if (str == null || str.Length == 0)
			{
				return;
			}
			int num = 0;
			if (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[str.Length - 1]))
			{
				str = str.Trim(null);
				if (str.Length == 0)
				{
					return;
				}
			}
			char c = this.Culture.TextInfo.ToLower(str[0]);
			int num2 = (int)(c % 'Ç');
			int num3 = (int)('\u0001' + c % 'Å');
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			for (;;)
			{
				tokenHashValue = hashTable[num2];
				if (tokenHashValue == null)
				{
					break;
				}
				if (str.Length >= tokenHashValue.tokenString.Length && this.CompareStringIgnoreCaseOptimized(str, 0, tokenHashValue.tokenString.Length, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length))
				{
					goto Block_6;
				}
				num++;
				num2 += num3;
				if (num2 >= 199)
				{
					num2 -= 199;
				}
				if (num >= 199)
				{
					return;
				}
			}
			hashTable[num2] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			return;
			Block_6:
			if (str.Length > tokenHashValue.tokenString.Length)
			{
				this.InsertAtCurrentHashNode(hashTable, str, c, tokenType, tokenValue, num, num2, num3);
				return;
			}
			int tokenType2 = (int)tokenHashValue.tokenType;
			if (((tokenType2 & 255) == 0 && (tokenType & TokenType.RegularTokenMask) != (TokenType)0) || ((tokenType2 & 65280) == 0 && (tokenType & TokenType.SeparatorTokenMask) != (TokenType)0))
			{
				tokenHashValue.tokenType |= tokenType;
				if (tokenValue != 0)
				{
					tokenHashValue.tokenValue = tokenValue;
				}
			}
			return;
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000CB84A File Offset: 0x000C9A4A
		private bool CompareStringIgnoreCaseOptimized(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return (length1 == 1 && length2 == 1 && string1[offset1] == string2[offset2]) || this.Culture.CompareInfo.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x04001BA6 RID: 7078
		private static volatile DateTimeFormatInfo s_invariantInfo;

		// Token: 0x04001BA7 RID: 7079
		[NonSerialized]
		private CultureData _cultureData;

		// Token: 0x04001BA8 RID: 7080
		private string _name;

		// Token: 0x04001BA9 RID: 7081
		[NonSerialized]
		private string _langName;

		// Token: 0x04001BAA RID: 7082
		[NonSerialized]
		private CompareInfo _compareInfo;

		// Token: 0x04001BAB RID: 7083
		[NonSerialized]
		private CultureInfo _cultureInfo;

		// Token: 0x04001BAC RID: 7084
		private string amDesignator;

		// Token: 0x04001BAD RID: 7085
		private string pmDesignator;

		// Token: 0x04001BAE RID: 7086
		private string dateSeparator;

		// Token: 0x04001BAF RID: 7087
		private string generalShortTimePattern;

		// Token: 0x04001BB0 RID: 7088
		private string generalLongTimePattern;

		// Token: 0x04001BB1 RID: 7089
		private string timeSeparator;

		// Token: 0x04001BB2 RID: 7090
		private string monthDayPattern;

		// Token: 0x04001BB3 RID: 7091
		private string dateTimeOffsetPattern;

		// Token: 0x04001BB4 RID: 7092
		private const string rfc1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";

		// Token: 0x04001BB5 RID: 7093
		private const string sortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		// Token: 0x04001BB6 RID: 7094
		private const string universalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";

		// Token: 0x04001BB7 RID: 7095
		private Calendar calendar;

		// Token: 0x04001BB8 RID: 7096
		private int firstDayOfWeek = -1;

		// Token: 0x04001BB9 RID: 7097
		private int calendarWeekRule = -1;

		// Token: 0x04001BBA RID: 7098
		private string fullDateTimePattern;

		// Token: 0x04001BBB RID: 7099
		private string[] abbreviatedDayNames;

		// Token: 0x04001BBC RID: 7100
		private string[] m_superShortDayNames;

		// Token: 0x04001BBD RID: 7101
		private string[] dayNames;

		// Token: 0x04001BBE RID: 7102
		private string[] abbreviatedMonthNames;

		// Token: 0x04001BBF RID: 7103
		private string[] monthNames;

		// Token: 0x04001BC0 RID: 7104
		private string[] genitiveMonthNames;

		// Token: 0x04001BC1 RID: 7105
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x04001BC2 RID: 7106
		private string[] leapYearMonthNames;

		// Token: 0x04001BC3 RID: 7107
		private string longDatePattern;

		// Token: 0x04001BC4 RID: 7108
		private string shortDatePattern;

		// Token: 0x04001BC5 RID: 7109
		private string yearMonthPattern;

		// Token: 0x04001BC6 RID: 7110
		private string longTimePattern;

		// Token: 0x04001BC7 RID: 7111
		private string shortTimePattern;

		// Token: 0x04001BC8 RID: 7112
		private string[] allYearMonthPatterns;

		// Token: 0x04001BC9 RID: 7113
		private string[] allShortDatePatterns;

		// Token: 0x04001BCA RID: 7114
		private string[] allLongDatePatterns;

		// Token: 0x04001BCB RID: 7115
		private string[] allShortTimePatterns;

		// Token: 0x04001BCC RID: 7116
		private string[] allLongTimePatterns;

		// Token: 0x04001BCD RID: 7117
		private string[] m_eraNames;

		// Token: 0x04001BCE RID: 7118
		private string[] m_abbrevEraNames;

		// Token: 0x04001BCF RID: 7119
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x04001BD0 RID: 7120
		private CalendarId[] optionalCalendars;

		// Token: 0x04001BD1 RID: 7121
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x04001BD2 RID: 7122
		internal bool _isReadOnly;

		// Token: 0x04001BD3 RID: 7123
		private DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;

		// Token: 0x04001BD4 RID: 7124
		private static readonly char[] s_monthSpaces = new char[] { ' ', '\u00a0' };

		// Token: 0x04001BD5 RID: 7125
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x04001BD6 RID: 7126
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x04001BD7 RID: 7127
		private string _fullTimeSpanPositivePattern;

		// Token: 0x04001BD8 RID: 7128
		private string _fullTimeSpanNegativePattern;

		// Token: 0x04001BD9 RID: 7129
		internal const DateTimeStyles InvalidDateTimeStyles = ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind);

		// Token: 0x04001BDA RID: 7130
		[NonSerialized]
		private DateTimeFormatInfo.TokenHashValue[] _dtfiTokenHash;

		// Token: 0x04001BDB RID: 7131
		private const int TOKEN_HASH_SIZE = 199;

		// Token: 0x04001BDC RID: 7132
		private const int SECOND_PRIME = 197;

		// Token: 0x04001BDD RID: 7133
		private const string dateSeparatorOrTimeZoneOffset = "-";

		// Token: 0x04001BDE RID: 7134
		private const string invariantDateSeparator = "/";

		// Token: 0x04001BDF RID: 7135
		private const string invariantTimeSeparator = ":";

		// Token: 0x04001BE0 RID: 7136
		internal const string IgnorablePeriod = ".";

		// Token: 0x04001BE1 RID: 7137
		internal const string IgnorableComma = ",";

		// Token: 0x04001BE2 RID: 7138
		internal const string CJKYearSuff = "年";

		// Token: 0x04001BE3 RID: 7139
		internal const string CJKMonthSuff = "月";

		// Token: 0x04001BE4 RID: 7140
		internal const string CJKDaySuff = "日";

		// Token: 0x04001BE5 RID: 7141
		internal const string KoreanYearSuff = "년";

		// Token: 0x04001BE6 RID: 7142
		internal const string KoreanMonthSuff = "월";

		// Token: 0x04001BE7 RID: 7143
		internal const string KoreanDaySuff = "일";

		// Token: 0x04001BE8 RID: 7144
		internal const string KoreanHourSuff = "시";

		// Token: 0x04001BE9 RID: 7145
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x04001BEA RID: 7146
		internal const string KoreanSecondSuff = "초";

		// Token: 0x04001BEB RID: 7147
		internal const string CJKHourSuff = "時";

		// Token: 0x04001BEC RID: 7148
		internal const string ChineseHourSuff = "时";

		// Token: 0x04001BED RID: 7149
		internal const string CJKMinuteSuff = "分";

		// Token: 0x04001BEE RID: 7150
		internal const string CJKSecondSuff = "秒";

		// Token: 0x04001BEF RID: 7151
		internal const string JapaneseEraStart = "元";

		// Token: 0x04001BF0 RID: 7152
		internal const string LocalTimeMark = "T";

		// Token: 0x04001BF1 RID: 7153
		internal const string GMTName = "GMT";

		// Token: 0x04001BF2 RID: 7154
		internal const string ZuluName = "Z";

		// Token: 0x04001BF3 RID: 7155
		internal const string KoreanLangName = "ko";

		// Token: 0x04001BF4 RID: 7156
		internal const string JapaneseLangName = "ja";

		// Token: 0x04001BF5 RID: 7157
		internal const string EnglishLangName = "en";

		// Token: 0x04001BF6 RID: 7158
		private static volatile DateTimeFormatInfo s_jajpDTFI;

		// Token: 0x04001BF7 RID: 7159
		private static volatile DateTimeFormatInfo s_zhtwDTFI;

		// Token: 0x02000694 RID: 1684
		internal class TokenHashValue
		{
			// Token: 0x0600351C RID: 13596 RVA: 0x000CB8A0 File Offset: 0x000C9AA0
			internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
			{
				this.tokenString = tokenString;
				this.tokenType = tokenType;
				this.tokenValue = tokenValue;
			}

			// Token: 0x04001BF8 RID: 7160
			internal string tokenString;

			// Token: 0x04001BF9 RID: 7161
			internal TokenType tokenType;

			// Token: 0x04001BFA RID: 7162
			internal int tokenValue;
		}
	}
}
