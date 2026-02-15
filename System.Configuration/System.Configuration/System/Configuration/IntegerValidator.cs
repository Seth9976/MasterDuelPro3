using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an <see cref="T:System.Int32" /> value.</summary>
	// Token: 0x0200002D RID: 45
	public class IntegerValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum length of the integer value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum length of the integer value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int32" /> object that specifies a value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is less than 0.- or -<paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x06000144 RID: 324 RVA: 0x00005F1C File Offset: 0x0000411C
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			if (minValue != 0)
			{
				this.minValue = minValue;
			}
			if (maxValue != 0)
			{
				this.maxValue = maxValue;
			}
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		/// <param name="rangeIsExclusive">true to specify that the validation range is exclusive. Inclusive means the value to be validated must be within the specified range; exclusive means that it must be below the minimum or above the maximum.</param>
		// Token: 0x06000145 RID: 325 RVA: 0x00005F52 File Offset: 0x00004152
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches an <see cref="T:System.Int32" /> value; otherwise, false. </returns>
		/// <param name="type">The type of the object.</param>
		// Token: 0x06000146 RID: 326 RVA: 0x00005F5E File Offset: 0x0000415E
		public override bool CanValidate(Type type)
		{
			return type == typeof(int);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value to be validated.</param>
		// Token: 0x06000147 RID: 327 RVA: 0x00005F70 File Offset: 0x00004170
		public override void Validate(object value)
		{
			int num = (int)value;
			if (!this.rangeIsExclusive)
			{
				if (num < this.minValue || num > this.maxValue)
				{
					throw new ArgumentException("The value must be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
				}
			}
			else if (num >= this.minValue && num <= this.maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
			}
			if (this.resolution != 0 && num % this.resolution != 0)
			{
				throw new ArgumentException("The value must have a resolution of " + this.resolution.ToString());
			}
		}

		// Token: 0x040000A1 RID: 161
		private bool rangeIsExclusive;

		// Token: 0x040000A2 RID: 162
		private int minValue;

		// Token: 0x040000A3 RID: 163
		private int maxValue = int.MaxValue;

		// Token: 0x040000A4 RID: 164
		private int resolution;
	}
}
