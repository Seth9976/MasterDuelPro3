using System;
using System.Security.Principal;
using System.Text;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200040F RID: 1039
	public sealed class HttpListenerContext
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x0006F392 File Offset: 0x0006D592
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0006F3C4 File Offset: 0x0006D5C4
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x0006F3CC File Offset: 0x0006D5CC
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x0006F3D5 File Offset: 0x0006D5D5
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x0006F3DD File Offset: 0x0006D5DD
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x0006F3E6 File Offset: 0x0006D5E6
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0006F3F1 File Offset: 0x0006D5F1
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0006F3F9 File Offset: 0x0006D5F9
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request. </summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0006F401 File Offset: 0x0006D601
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0006F40C File Offset: 0x0006D60C
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[] { ' ' }, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0006F478 File Offset: 0x0006D678
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal principal;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string text3;
				if (num > 0)
				{
					text3 = text.Substring(num);
				}
				else
				{
					text3 = text;
				}
				principal = new GenericPrincipal(new HttpListenerBasicIdentity(text3, text2), new string[0]);
			}
			catch (Exception)
			{
				principal = null;
			}
			return principal;
		}

		// Token: 0x04001082 RID: 4226
		private HttpListenerRequest request;

		// Token: 0x04001083 RID: 4227
		private HttpListenerResponse response;

		// Token: 0x04001084 RID: 4228
		private IPrincipal user;

		// Token: 0x04001085 RID: 4229
		private HttpConnection cnc;

		// Token: 0x04001086 RID: 4230
		private string error;

		// Token: 0x04001087 RID: 4231
		private int err_status = 400;

		// Token: 0x04001088 RID: 4232
		internal HttpListener Listener;
	}
}
