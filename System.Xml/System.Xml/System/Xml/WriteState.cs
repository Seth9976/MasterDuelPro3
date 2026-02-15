using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the <see cref="T:System.Xml.XmlWriter" />.</summary>
	// Token: 0x020000C0 RID: 192
	public enum WriteState
	{
		/// <summary>Indicates that a Write method has not yet been called.</summary>
		// Token: 0x04000561 RID: 1377
		Start,
		/// <summary>Indicates that the prolog is being written.</summary>
		// Token: 0x04000562 RID: 1378
		Prolog,
		/// <summary>Indicates that an element start tag is being written.</summary>
		// Token: 0x04000563 RID: 1379
		Element,
		/// <summary>Indicates that an attribute value is being written.</summary>
		// Token: 0x04000564 RID: 1380
		Attribute,
		/// <summary>Indicates that element content is being written.</summary>
		// Token: 0x04000565 RID: 1381
		Content,
		/// <summary>Indicates that the <see cref="M:System.Xml.XmlWriter.Close" /> method has been called.</summary>
		// Token: 0x04000566 RID: 1382
		Closed,
		/// <summary>An exception has been thrown, which has left the <see cref="T:System.Xml.XmlWriter" /> in an invalid state. You can call the <see cref="M:System.Xml.XmlWriter.Close" /> method to put the <see cref="T:System.Xml.XmlWriter" /> in the <see cref="F:System.Xml.WriteState.Closed" /> state. Any other <see cref="T:System.Xml.XmlWriter" /> method calls results in an <see cref="T:System.InvalidOperationException" />.</summary>
		// Token: 0x04000567 RID: 1383
		Error
	}
}
