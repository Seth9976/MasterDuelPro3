using System;

namespace System.Net
{
	// Token: 0x020003B3 RID: 947
	internal static class ExceptionHelper
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x000648E9 File Offset: 0x00062AE9
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This method is not implemented by this class."));
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000648FA File Offset: 0x00062AFA
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This property is not implemented by this class."));
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0006490B File Offset: 0x00062B0B
		internal static WebException TimeoutException
		{
			get
			{
				return new WebException("The operation has timed out.");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00064917 File Offset: 0x00062B17
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This property is not supported by this class."));
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00064928 File Offset: 0x00062B28
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}
	}
}
