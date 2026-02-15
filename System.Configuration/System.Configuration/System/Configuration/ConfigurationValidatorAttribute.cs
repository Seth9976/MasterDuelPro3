using System;

namespace System.Configuration
{
	/// <summary>Serves as the base class for the <see cref="N:System.Configuration" /> validator attribute types.</summary>
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigurationValidatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class.</summary>
		// Token: 0x0600011D RID: 285 RVA: 0x00002094 File Offset: 0x00000294
		protected ConfigurationValidatorAttribute()
		{
		}

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005C76 File Offset: 0x00003E76
		public virtual ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = (ConfigurationValidatorBase)Activator.CreateInstance(this.validatorType);
				}
				return this.instance;
			}
		}

		// Token: 0x04000094 RID: 148
		private Type validatorType;

		// Token: 0x04000095 RID: 149
		private ConfigurationValidatorBase instance;
	}
}
