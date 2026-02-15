using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003F7 RID: 1015
	internal class ServerCertValidationCallback
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0006C254 File Offset: 0x0006A454
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0006C26E File Offset: 0x0006A46E
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0006C278 File Offset: 0x0006A478
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0006C2B8 File Offset: 0x0006A4B8
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x0400100D RID: 4109
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x0400100E RID: 4110
		private readonly ExecutionContext m_Context;

		// Token: 0x020003F8 RID: 1016
		private class CallbackContext
		{
			// Token: 0x0600194D RID: 6477 RVA: 0x0006C30C File Offset: 0x0006A50C
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x0400100F RID: 4111
			internal readonly object request;

			// Token: 0x04001010 RID: 4112
			internal readonly X509Certificate certificate;

			// Token: 0x04001011 RID: 4113
			internal readonly X509Chain chain;

			// Token: 0x04001012 RID: 4114
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x04001013 RID: 4115
			internal bool result;
		}
	}
}
