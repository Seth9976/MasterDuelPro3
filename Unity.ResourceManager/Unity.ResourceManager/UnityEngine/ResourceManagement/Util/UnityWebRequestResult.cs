using System;
using System.Text;
using UnityEngine.Networking;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000042 RID: 66
	public class UnityWebRequestResult
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00006D88 File Offset: 0x00004F88
		public UnityWebRequestResult(UnityWebRequest request)
		{
			string error = request.error;
			if (request.result == UnityWebRequest.Result.DataProcessingError && request.downloadHandler != null)
			{
				error = error + " : " + request.downloadHandler.error;
			}
			this.Result = request.result;
			this.Error = error;
			this.ResponseCode = request.responseCode;
			this.Method = request.method;
			this.Url = request.url;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006E04 File Offset: 0x00005004
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("{0} : {1}", this.Result, this.Error));
			if (this.ResponseCode > 0L)
			{
				sb.AppendLine(string.Format("ResponseCode : {0}, Method : {1}", this.ResponseCode, this.Method));
			}
			sb.AppendLine("url : " + this.Url);
			return sb.ToString();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006E82 File Offset: 0x00005082
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006E8A File Offset: 0x0000508A
		public string Error { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006E93 File Offset: 0x00005093
		public long ResponseCode { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006E9B File Offset: 0x0000509B
		public UnityWebRequest.Result Result { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006EA3 File Offset: 0x000050A3
		public string Method { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006EAB File Offset: 0x000050AB
		public string Url { get; }

		// Token: 0x06000175 RID: 373 RVA: 0x00006EB4 File Offset: 0x000050B4
		public bool ShouldRetryDownloadError()
		{
			return string.IsNullOrEmpty(this.Error) || (!(this.Error == "Request aborted") && !(this.Error == "Unable to write data") && !(this.Error == "Malformed URL") && !(this.Error == "Out of memory") && !(this.Error == "Encountered invalid redirect (missing Location header?)") && !(this.Error == "Cannot modify request at this time") && !(this.Error == "Unsupported Protocol") && !(this.Error == "Destination host has an erroneous SSL certificate") && !(this.Error == "Unable to load SSL Cipher for verification") && !(this.Error == "SSL CA certificate error") && !(this.Error == "Unrecognized content-encoding") && !(this.Error == "Request already transmitted") && !(this.Error == "Invalid HTTP Method") && !(this.Error == "Header name contains invalid characters") && !(this.Error == "Header value contains invalid characters") && !(this.Error == "Cannot override system-specified headers") && !(this.Error == "Insecure connection not allowed"));
		}
	}
}
