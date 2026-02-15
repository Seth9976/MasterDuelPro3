using System;
using UnityEngine.Networking;

namespace MDPro3.Net
{
	// Token: 0x02001333 RID: 4915
	public static class NetUtil
	{
		// Token: 0x06008F33 RID: 36659 RVA: 0x00134580 File Offset: 0x00132780
		public static bool IsValidUrl(string inputUrl)
		{
			if (string.IsNullOrEmpty(inputUrl))
			{
				return false;
			}
			if (!inputUrl.StartsWith("http://") && !inputUrl.StartsWith("https://"))
			{
				inputUrl = "http://" + inputUrl;
			}
			Uri uriResult;
			return Uri.TryCreate(inputUrl, UriKind.Absolute, out uriResult) && (!(uriResult.Scheme != Uri.UriSchemeHttp) || !(uriResult.Scheme != Uri.UriSchemeHttps));
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x001345F4 File Offset: 0x001327F4
		public static bool IsValidDownloadUrl(string inputUrl, string[] supportExtensions)
		{
			bool hasValidExtension = false;
			foreach (string ext in supportExtensions)
			{
				if (inputUrl.ToLower().Contains(ext.ToLower()))
				{
					hasValidExtension = true;
					break;
				}
			}
			return hasValidExtension && NetUtil.IsValidUrl(inputUrl);
		}

		// Token: 0x02001334 RID: 4916
		public class AcceptAllCertificateHandler : CertificateHandler
		{
			// Token: 0x06008F35 RID: 36661 RVA: 0x0000763C File Offset: 0x0000583C
			protected override bool ValidateCertificate(byte[] certificateData)
			{
				return true;
			}
		}
	}
}
