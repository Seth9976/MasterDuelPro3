using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Persists an 8-byte <see cref="T:System.DateTime" /> constant for a field or parameter.</summary>
	// Token: 0x02000584 RID: 1412
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of 100-nanosecond ticks that represent the date and time of this instance.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000AAB1F File Offset: 0x000A8D1F
		public override object Value
		{
			get
			{
				return this._date;
			}
		}

		// Token: 0x040015CD RID: 5581
		private DateTime _date;
	}
}
