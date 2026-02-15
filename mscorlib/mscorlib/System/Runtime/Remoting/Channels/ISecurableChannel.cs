using System;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>The <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> contains one property, <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, which gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
	// Token: 0x0200045E RID: 1118
	public interface ISecurableChannel
	{
		/// <summary>Gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
		/// <returns>A Boolean value that indicates whether the current channel is secure.</returns>
		// Token: 0x1700045F RID: 1119
		// (set) Token: 0x06002490 RID: 9360
		bool IsSecured { set; }
	}
}
