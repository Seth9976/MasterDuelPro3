using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>The exception thrown when using invalid arguments that are enumerators.</summary>
	// Token: 0x0200024C RID: 588
	[Serializable]
	public class InvalidEnumArgumentException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class without a message.</summary>
		// Token: 0x06000E16 RID: 3606 RVA: 0x0003E92A File Offset: 0x0003CB2A
		public InvalidEnumArgumentException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified message.</summary>
		/// <param name="message">The message to display with this exception. </param>
		// Token: 0x06000E17 RID: 3607 RVA: 0x0003E933 File Offset: 0x0003CB33
		public InvalidEnumArgumentException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with a message generated from the argument, the invalid value, and an enumeration class.</summary>
		/// <param name="argumentName">The name of the argument that caused the exception. </param>
		/// <param name="invalidValue">The value of the argument that failed. </param>
		/// <param name="enumClass">A <see cref="T:System.Type" /> that represents the enumeration class with the valid values. </param>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0003E93C File Offset: 0x0003CB3C
		public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass)
			: base(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", argumentName, invalidValue.ToString(CultureInfo.CurrentCulture), enumClass.Name), argumentName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000E19 RID: 3609 RVA: 0x0003E962 File Offset: 0x0003CB62
		protected InvalidEnumArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
