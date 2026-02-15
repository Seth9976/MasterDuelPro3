using System;

namespace System.Net
{
	/// <summary>The HTTP headers that may be specified in a client request.</summary>
	// Token: 0x020003AA RID: 938
	public enum HttpRequestHeader
	{
		/// <summary>The Cache-Control header, which specifies directives that must be obeyed by all cache control mechanisms along the request/response chain.</summary>
		// Token: 0x04000E9F RID: 3743
		CacheControl,
		/// <summary>The Connection header, which specifies options that are desired for a particular connection.</summary>
		// Token: 0x04000EA0 RID: 3744
		Connection,
		/// <summary>The Date header, which specifies the date and time at which the request originated.</summary>
		// Token: 0x04000EA1 RID: 3745
		Date,
		/// <summary>The Keep-Alive header, which specifies a parameter used into order to maintain a persistent connection.</summary>
		// Token: 0x04000EA2 RID: 3746
		KeepAlive,
		/// <summary>The Pragma header, which specifies implementation-specific directives that might apply to any agent along the request/response chain.</summary>
		// Token: 0x04000EA3 RID: 3747
		Pragma,
		/// <summary>The Trailer header, which specifies the header fields present in the trailer of a message encoded with chunked transfer-coding.</summary>
		// Token: 0x04000EA4 RID: 3748
		Trailer,
		/// <summary>The Transfer-Encoding header, which specifies what (if any) type of transformation that has been applied to the message body.</summary>
		// Token: 0x04000EA5 RID: 3749
		TransferEncoding,
		/// <summary>The Upgrade header, which specifies additional communications protocols that the client supports.</summary>
		// Token: 0x04000EA6 RID: 3750
		Upgrade,
		/// <summary>The Via header, which specifies intermediate protocols to be used by gateway and proxy agents.</summary>
		// Token: 0x04000EA7 RID: 3751
		Via,
		/// <summary>The Warning header, which specifies additional information about that status or transformation of a message that might not be reflected in the message.</summary>
		// Token: 0x04000EA8 RID: 3752
		Warning,
		/// <summary>The Allow header, which specifies the set of HTTP methods supported.</summary>
		// Token: 0x04000EA9 RID: 3753
		Allow,
		/// <summary>The Content-Length header, which specifies the length, in bytes, of the accompanying body data.</summary>
		// Token: 0x04000EAA RID: 3754
		ContentLength,
		/// <summary>The Content-Type header, which specifies the MIME type of the accompanying body data.</summary>
		// Token: 0x04000EAB RID: 3755
		ContentType,
		/// <summary>The Content-Encoding header, which specifies the encodings that have been applied to the accompanying body data.</summary>
		// Token: 0x04000EAC RID: 3756
		ContentEncoding,
		/// <summary>The Content-Langauge header, which specifies the natural language(s) of the accompanying body data.</summary>
		// Token: 0x04000EAD RID: 3757
		ContentLanguage,
		/// <summary>The Content-Location header, which specifies a URI from which the accompanying body may be obtained.</summary>
		// Token: 0x04000EAE RID: 3758
		ContentLocation,
		/// <summary>The Content-MD5 header, which specifies the MD5 digest of the accompanying body data, for the purpose of providing an end-to-end message integrity check.</summary>
		// Token: 0x04000EAF RID: 3759
		ContentMd5,
		/// <summary>The Content-Range header, which specifies where in the full body the accompanying partial body data should be applied.</summary>
		// Token: 0x04000EB0 RID: 3760
		ContentRange,
		/// <summary>The Expires header, which specifies the date and time after which the accompanying body data should be considered stale.</summary>
		// Token: 0x04000EB1 RID: 3761
		Expires,
		/// <summary>The Last-Modified header, which specifies the date and time at which the accompanying body data was last modified.</summary>
		// Token: 0x04000EB2 RID: 3762
		LastModified,
		/// <summary>The Accept header, which specifies the MIME types that are acceptable for the response.</summary>
		// Token: 0x04000EB3 RID: 3763
		Accept,
		/// <summary>The Accept-Charset header, which specifies the character sets that are acceptable for the response.</summary>
		// Token: 0x04000EB4 RID: 3764
		AcceptCharset,
		/// <summary>The Accept-Encoding header, which specifies the content encodings that are acceptable for the response.</summary>
		// Token: 0x04000EB5 RID: 3765
		AcceptEncoding,
		/// <summary>The Accept-Langauge header, which specifies that natural languages that are preferred for the response.</summary>
		// Token: 0x04000EB6 RID: 3766
		AcceptLanguage,
		/// <summary>The Authorization header, which specifies the credentials that the client presents in order to authenticate itself to the server.</summary>
		// Token: 0x04000EB7 RID: 3767
		Authorization,
		/// <summary>The Cookie header, which specifies cookie data presented to the server.</summary>
		// Token: 0x04000EB8 RID: 3768
		Cookie,
		/// <summary>The Expect header, which specifies particular server behaviors that are required by the client.</summary>
		// Token: 0x04000EB9 RID: 3769
		Expect,
		/// <summary>The From header, which specifies an Internet E-mail address for the human user who controls the requesting user agent.</summary>
		// Token: 0x04000EBA RID: 3770
		From,
		/// <summary>The Host header, which specifies the host name and port number of the resource being requested.</summary>
		// Token: 0x04000EBB RID: 3771
		Host,
		/// <summary>The If-Match header, which specifies that the requested operation should be performed only if the client's cached copy of the indicated resource is current.</summary>
		// Token: 0x04000EBC RID: 3772
		IfMatch,
		/// <summary>The If-Modified-Since header, which specifies that the requested operation should be performed only if the requested resource has been modified since the indicated data and time.</summary>
		// Token: 0x04000EBD RID: 3773
		IfModifiedSince,
		/// <summary>The If-None-Match header, which specifies that the requested operation should be performed only if none of client's cached copies of the indicated resources are current.</summary>
		// Token: 0x04000EBE RID: 3774
		IfNoneMatch,
		/// <summary>The If-Range header, which specifies that only the specified range of the requested resource should be sent, if the client's cached copy is current.</summary>
		// Token: 0x04000EBF RID: 3775
		IfRange,
		/// <summary>The If-Unmodified-Since header, which specifies that the requested operation should be performed only if the requested resource has not been modified since the indicated date and time.</summary>
		// Token: 0x04000EC0 RID: 3776
		IfUnmodifiedSince,
		/// <summary>The Max-Forwards header, which specifies an integer indicating the remaining number of times that this request may be forwarded.</summary>
		// Token: 0x04000EC1 RID: 3777
		MaxForwards,
		/// <summary>The Proxy-Authorization header, which specifies the credentials that the client presents in order to authenticate itself to a proxy.</summary>
		// Token: 0x04000EC2 RID: 3778
		ProxyAuthorization,
		/// <summary>The Referer header, which specifies the URI of the resource from which the request URI was obtained.</summary>
		// Token: 0x04000EC3 RID: 3779
		Referer,
		/// <summary>The Range header, which specifies the the sub-range(s) of the response that the client requests be returned in lieu of the entire response.</summary>
		// Token: 0x04000EC4 RID: 3780
		Range,
		/// <summary>The TE header, which specifies the transfer encodings that are acceptable for the response.</summary>
		// Token: 0x04000EC5 RID: 3781
		Te,
		/// <summary>The Translate header, a Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.</summary>
		// Token: 0x04000EC6 RID: 3782
		Translate,
		/// <summary>The User-Agent header, which specifies information about the client agent.</summary>
		// Token: 0x04000EC7 RID: 3783
		UserAgent
	}
}
