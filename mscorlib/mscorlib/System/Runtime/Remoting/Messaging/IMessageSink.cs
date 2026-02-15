using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the interface for a message sink.</summary>
	// Token: 0x0200048C RID: 1164
	[ComVisible(true)]
	public interface IMessageSink
	{
		/// <summary>Synchronously processes the given message.</summary>
		/// <returns>A reply message in response to the request.</returns>
		/// <param name="msg">The message to process. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002563 RID: 9571
		IMessage SyncProcessMessage(IMessage msg);

		/// <summary>Asynchronously processes the given message.</summary>
		/// <returns>Returns an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> interface that provides a way to control asynchronous messages after they have been dispatched.</returns>
		/// <param name="msg">The message to process. </param>
		/// <param name="replySink">The reply sink for the reply message. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002564 RID: 9572
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);
	}
}
