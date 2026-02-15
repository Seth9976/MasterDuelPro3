using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlAttribute" /> objects) can contain any XML attributes.</summary>
	// Token: 0x020001A0 RID: 416
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlAnyAttributeAttribute : Attribute
	{
	}
}
