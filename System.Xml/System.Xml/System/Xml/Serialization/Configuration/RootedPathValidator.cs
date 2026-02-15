using System;
using System.Configuration;
using System.IO;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Validates the rules governing the use of the tempFilesLocation configuration switch. </summary>
	// Token: 0x020001FC RID: 508
	public class RootedPathValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a valid XMLSerializer object; otherwise, false.</returns>
		/// <param name="type">The type of the object.</param>
		// Token: 0x060019B9 RID: 6585 RVA: 0x0009778D File Offset: 0x0009598D
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x060019BA RID: 6586 RVA: 0x000977A0 File Offset: 0x000959A0
		public override void Validate(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!Path.IsPathRooted(text))
			{
				throw new ConfigurationErrorsException();
			}
			char c = text[0];
			if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
			{
				throw new ConfigurationErrorsException();
			}
		}
	}
}
