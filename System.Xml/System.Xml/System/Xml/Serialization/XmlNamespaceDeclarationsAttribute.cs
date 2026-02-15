using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the target property, parameter, return value, or class member contains prefixes associated with namespaces that are used within an XML document.</summary>
	// Token: 0x020001B6 RID: 438
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlNamespaceDeclarationsAttribute : Attribute
	{
	}
}
