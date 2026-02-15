using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object.</summary>
	// Token: 0x02000044 RID: 68
	public class TimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		// Token: 0x060001C9 RID: 457 RVA: 0x0000799D File Offset: 0x00005B9D
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolutionInSeconds">An <see cref="T:System.Int64" /> value that specifies a number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolutionInSeconds" /> is less than 0.- or -<paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x060001CA RID: 458 RVA: 0x000079AA File Offset: 0x00005BAA
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive, long resolutionInSeconds)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolutionInSeconds = resolutionInSeconds;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> value; otherwise, false. </returns>
		/// <param name="type">The type of the object.</param>
		// Token: 0x060001CB RID: 459 RVA: 0x000079CF File Offset: 0x00005BCF
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x060001CC RID: 460 RVA: 0x000079E4 File Offset: 0x00005BE4
		public override void Validate(object value)
		{
			TimeSpan timeSpan = (TimeSpan)value;
			if (!this.rangeIsExclusive)
			{
				if (timeSpan < this.minValue || timeSpan > this.maxValue)
				{
					throw new ArgumentException("The value must be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
				}
			}
			else if (timeSpan >= this.minValue && timeSpan <= this.maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
			}
			if (this.resolutionInSeconds != 0L && timeSpan.Ticks % (10000000L * this.resolutionInSeconds) != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + TimeSpan.FromTicks(10000000L * this.resolutionInSeconds).ToString());
			}
		}

		// Token: 0x040000E3 RID: 227
		private bool rangeIsExclusive;

		// Token: 0x040000E4 RID: 228
		private TimeSpan minValue;

		// Token: 0x040000E5 RID: 229
		private TimeSpan maxValue;

		// Token: 0x040000E6 RID: 230
		private long resolutionInSeconds;
	}
}
