using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the reader.</summary>
	// Token: 0x0200003F RID: 63
	public enum ReadState
	{
		/// <summary>The Read method has not been called.</summary>
		// Token: 0x04000156 RID: 342
		Initial,
		/// <summary>The Read method has been called. Additional methods may be called on the reader.</summary>
		// Token: 0x04000157 RID: 343
		Interactive,
		/// <summary>An error occurred that prevents the read operation from continuing.</summary>
		// Token: 0x04000158 RID: 344
		Error,
		/// <summary>The end of the file has been reached successfully.</summary>
		// Token: 0x04000159 RID: 345
		EndOfFile,
		/// <summary>The <see cref="M:System.Xml.XmlReader.Close" /> method has been called.</summary>
		// Token: 0x0400015A RID: 346
		Closed
	}
}
