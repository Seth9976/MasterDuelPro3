using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an object. This class cannot be inherited.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class DefaultValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether an object can be validated, based on type.</summary>
		/// <returns>true for all types being validated. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x0600012D RID: 301 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public override bool CanValidate(Type type)
		{
			return true;
		}

		/// <summary>Determines whether the value of an object is valid. </summary>
		/// <param name="value">The object value.</param>
		// Token: 0x0600012E RID: 302 RVA: 0x000029FD File Offset: 0x00000BFD
		public override void Validate(object value)
		{
		}
	}
}
