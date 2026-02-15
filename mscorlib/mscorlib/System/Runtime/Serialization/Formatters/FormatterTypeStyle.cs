using System;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Indicates the format in which type descriptions are laid out in the serialized stream.</summary>
	// Token: 0x020004D0 RID: 1232
	public enum FormatterTypeStyle
	{
		/// <summary>Indicates that types can be stated only for arrays of objects, object members of type <see cref="T:System.Object" />, and <see cref="T:System.Runtime.Serialization.ISerializable" /> non-primitive value types.</summary>
		// Token: 0x040012B1 RID: 4785
		TypesWhenNeeded,
		/// <summary>Indicates that types can be given to all object members and <see cref="T:System.Runtime.Serialization.ISerializable" /> object members.</summary>
		// Token: 0x040012B2 RID: 4786
		TypesAlways,
		/// <summary>Indicates that strings can be given in the XSD format rather than SOAP. No string IDs are transmitted. </summary>
		// Token: 0x040012B3 RID: 4787
		XsdString
	}
}
