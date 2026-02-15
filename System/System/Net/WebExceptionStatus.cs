using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x020003BC RID: 956
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x04000EE9 RID: 3817
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x04000EEA RID: 3818
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x04000EEB RID: 3819
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04000EEC RID: 3820
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04000EED RID: 3821
		SendFailure,
		/// <summary>The request was a piplined request and the connection was closed before the response was received.</summary>
		// Token: 0x04000EEE RID: 3822
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04000EEF RID: 3823
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04000EF0 RID: 3824
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04000EF1 RID: 3825
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x04000EF2 RID: 3826
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x04000EF3 RID: 3827
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x04000EF4 RID: 3828
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x04000EF5 RID: 3829
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x04000EF6 RID: 3830
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x04000EF7 RID: 3831
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x04000EF8 RID: 3832
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x04000EF9 RID: 3833
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x04000EFA RID: 3834
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x04000EFB RID: 3835
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04000EFC RID: 3836
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04000EFD RID: 3837
		RequestProhibitedByProxy
	}
}
