using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x020004E8 RID: 1256
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x04001652 RID: 5714
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x04001653 RID: 5715
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x04001654 RID: 5716
		Close
	}
}
