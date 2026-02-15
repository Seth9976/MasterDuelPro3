using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200018A RID: 394
	public class UnityWebRequestException : Exception
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002A012 File Offset: 0x00028212
		public UnityWebRequest UnityWebRequest { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002A01A File Offset: 0x0002821A
		public UnityWebRequest.Result Result { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002A022 File Offset: 0x00028222
		public string Error { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002A02A File Offset: 0x0002822A
		public string Text { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0002A032 File Offset: 0x00028232
		public long ResponseCode { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0002A03A File Offset: 0x0002823A
		public Dictionary<string, string> ResponseHeaders { get; }

		// Token: 0x0600095F RID: 2399 RVA: 0x0002A044 File Offset: 0x00028244
		public UnityWebRequestException(UnityWebRequest unityWebRequest)
		{
			this.UnityWebRequest = unityWebRequest;
			this.Result = unityWebRequest.result;
			this.Error = unityWebRequest.error;
			this.ResponseCode = unityWebRequest.responseCode;
			if (this.UnityWebRequest.downloadHandler != null)
			{
				DownloadHandlerBuffer dhb = unityWebRequest.downloadHandler as DownloadHandlerBuffer;
				if (dhb != null)
				{
					this.Text = dhb.text;
				}
			}
			this.ResponseHeaders = unityWebRequest.GetResponseHeaders();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0002A0B8 File Offset: 0x000282B8
		public override string Message
		{
			get
			{
				if (this.msg == null)
				{
					if (!string.IsNullOrWhiteSpace(this.Text))
					{
						this.msg = this.Error + Environment.NewLine + this.Text;
					}
					else
					{
						this.msg = this.Error;
					}
				}
				return this.msg;
			}
		}

		// Token: 0x04000637 RID: 1591
		private string msg;
	}
}
