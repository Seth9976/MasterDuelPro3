using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the relative maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents less than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValue" />.</exception>
		// Token: 0x1700008C RID: 140
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public string MaxValueString
		{
			set
			{
				this.maxValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the relative minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The minimum allowed <see cref="T:System.TimeSpan" /> value. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents more than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x1700008D RID: 141
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007B08 File Offset: 0x00005D08
		public string MinValueString
		{
			set
			{
				this.minValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets the absolute maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value. </returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007B18 File Offset: 0x00005D18
		public TimeSpan MaxValue
		{
			get
			{
				return TimeSpan.Parse(this.maxValueString);
			}
		}

		/// <summary>Gets the absolute minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed minimum <see cref="T:System.TimeSpan" /> value. </returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007B25 File Offset: 0x00005D25
		public TimeSpan MinValue
		{
			get
			{
				return TimeSpan.Parse(this.minValueString);
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance. </returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007B32 File Offset: 0x00005D32
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new TimeSpanValidator(this.MinValue, this.MaxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x040000E7 RID: 231
		private bool excludeRange;

		// Token: 0x040000E8 RID: 232
		private string maxValueString = "10675199.02:48:05.4775807";

		// Token: 0x040000E9 RID: 233
		private string minValueString = "-10675199.02:48:05.4775808";

		// Token: 0x040000EA RID: 234
		private ConfigurationValidatorBase instance;
	}
}
