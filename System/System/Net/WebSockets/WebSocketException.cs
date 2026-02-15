using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.WebSockets
{
	/// <summary>Represents an exception that occurred when performing an operation on a WebSocket connection.</summary>
	// Token: 0x020004E7 RID: 1255
	[Serializable]
	public sealed class WebSocketException : Win32Exception
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x000871C2 File Offset: 0x000853C2
		public WebSocketException()
			: this(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		// Token: 0x06001EB9 RID: 7865 RVA: 0x000871CF File Offset: 0x000853CF
		public WebSocketException(WebSocketError error)
			: this(error, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x06001EBA RID: 7866 RVA: 0x000871DE File Offset: 0x000853DE
		public WebSocketException(WebSocketError error, string message)
			: base(message)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001EBB RID: 7867 RVA: 0x000871EE File Offset: 0x000853EE
		public WebSocketException(WebSocketError error, Exception innerException)
			: this(error, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001EBC RID: 7868 RVA: 0x000871FE File Offset: 0x000853FE
		public WebSocketException(WebSocketError error, string message, Exception innerException)
			: base(message, innerException)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x06001EBD RID: 7869 RVA: 0x0008720F File Offset: 0x0008540F
		public WebSocketException(int nativeError)
			: base(nativeError)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		// Token: 0x06001EBE RID: 7870 RVA: 0x00087231 File Offset: 0x00085431
		public WebSocketException(string message)
			: base(message)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001EBF RID: 7871 RVA: 0x0008723A File Offset: 0x0008543A
		public WebSocketException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00063F1A File Offset: 0x0006211A
		private WebSocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Sets the SerializationInfo object with the file name and line number where the exception occurred.</summary>
		/// <param name="info">A SerializationInfo object.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001EC1 RID: 7873 RVA: 0x00087244 File Offset: 0x00085444
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WebSocketErrorCode", this._webSocketErrorCode);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00087264 File Offset: 0x00085464
		private static string GetErrorMessage(WebSocketError error)
		{
			switch (error)
			{
			case WebSocketError.InvalidMessageType:
				return SR.Format("The received  message type is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.", "WebSocket.CloseAsync", "WebSocket.CloseOutputAsync");
			case WebSocketError.Faulted:
				return "An exception caused the WebSocket to enter the Aborted state. Please see the InnerException, if present, for more details.";
			case WebSocketError.NotAWebSocket:
				return "A WebSocket operation was called on a request or response that is not a WebSocket.";
			case WebSocketError.UnsupportedVersion:
				return "Unsupported WebSocket version.";
			case WebSocketError.UnsupportedProtocol:
				return "The WebSocket request or response operation was called with unsupported protocol(s).";
			case WebSocketError.HeaderError:
				return "The WebSocket request or response contained unsupported header(s).";
			case WebSocketError.ConnectionClosedPrematurely:
				return "The remote party closed the WebSocket connection without completing the close handshake.";
			case WebSocketError.InvalidState:
				return "The WebSocket instance cannot be used for communication because it has been transitioned into an invalid state.";
			}
			return "An internal WebSocket error occurred. Please see the innerException, if present, for more details.";
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000872E3 File Offset: 0x000854E3
		private void SetErrorCodeOnError(int nativeError)
		{
			if (!WebSocketException.Succeeded(nativeError))
			{
				base.HResult = nativeError;
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000872F4 File Offset: 0x000854F4
		private static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x04001650 RID: 5712
		private readonly WebSocketError _webSocketErrorCode;
	}
}
