using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the minimum allowed value for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the allowed minimum length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.StringValidatorAttribute.MaxLength" />.</exception>
		// Token: 0x1700008A RID: 138
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000794D File Offset: 0x00005B4D
		public int MinLength
		{
			set
			{
				this.minLength = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.StringValidator" /> class.</summary>
		/// <returns>A current <see cref="T:System.Configuration.StringValidator" /> settings in a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000795D File Offset: 0x00005B5D
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new StringValidator(this.minLength, this.maxLength, this.invalidCharacters);
				}
				return this.instance;
			}
		}

		// Token: 0x040000DF RID: 223
		private string invalidCharacters;

		// Token: 0x040000E0 RID: 224
		private int maxLength = int.MaxValue;

		// Token: 0x040000E1 RID: 225
		private int minLength;

		// Token: 0x040000E2 RID: 226
		private ConfigurationValidatorBase instance;
	}
}
