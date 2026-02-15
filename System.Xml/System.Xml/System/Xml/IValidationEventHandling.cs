using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000034 RID: 52
	internal interface IValidationEventHandling
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B4 RID: 436
		object EventHandler { get; }

		// Token: 0x060001B5 RID: 437
		void SendEvent(Exception exception, XmlSeverityType severity);
	}
}
