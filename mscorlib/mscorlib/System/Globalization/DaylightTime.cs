using System;

namespace System.Globalization
{
	/// <summary>Defines the period of daylight saving time.</summary>
	// Token: 0x0200069A RID: 1690
	[Serializable]
	public class DaylightTime
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.DaylightTime" /> class with the specified start, end, and time difference information.</summary>
		/// <param name="start">The object that represents the date and time when daylight saving time begins. The value must be in local time. </param>
		/// <param name="end">The object that represents the date and time when daylight saving time ends. The value must be in local time. </param>
		/// <param name="delta">The object that represents the difference between standard time and daylight saving time, in ticks. </param>
		// Token: 0x0600352D RID: 13613 RVA: 0x000CC0B4 File Offset: 0x000CA2B4
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this._start = start;
			this._end = end;
			this._delta = delta;
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period begins.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period begins. The value is in local time.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000CC0D1 File Offset: 0x000CA2D1
		public DateTime Start
		{
			get
			{
				return this._start;
			}
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period ends.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period ends. The value is in local time.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000CC0D9 File Offset: 0x000CA2D9
		public DateTime End
		{
			get
			{
				return this._end;
			}
		}

		/// <summary>Gets the time interval that represents the difference between standard time and daylight saving time.</summary>
		/// <returns>The time interval that represents the difference between standard time and daylight saving time.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000CC0E1 File Offset: 0x000CA2E1
		public TimeSpan Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x04001C31 RID: 7217
		private readonly DateTime _start;

		// Token: 0x04001C32 RID: 7218
		private readonly DateTime _end;

		// Token: 0x04001C33 RID: 7219
		private readonly TimeSpan _delta;
	}
}
