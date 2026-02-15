using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x17000065 RID: 101
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000603B File Offset: 0x0000423B
		public int MinValue
		{
			set
			{
				this.minValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000604B File Offset: 0x0000424B
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new IntegerValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x040000A5 RID: 165
		private bool excludeRange;

		// Token: 0x040000A6 RID: 166
		private int maxValue;

		// Token: 0x040000A7 RID: 167
		private int minValue;

		// Token: 0x040000A8 RID: 168
		private ConfigurationValidatorBase instance;
	}
}
