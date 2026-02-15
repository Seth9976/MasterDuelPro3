using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200017D RID: 381
	internal interface INameScope
	{
		// Token: 0x1700043F RID: 1087
		object this[string name, string ns] { get; set; }
	}
}
