using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	// Token: 0x02000007 RID: 7
	internal sealed class ComCtlv6ActivationContext : IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public ComCtlv6ActivationContext(bool enable)
		{
			bool flag = enable && NativeMethods.IsWindowsXPOrLater;
			if (flag)
			{
				bool flag2 = ComCtlv6ActivationContext.EnsureActivateContextCreated();
				if (flag2)
				{
					bool flag3 = !NativeMethods.ActivateActCtx(ComCtlv6ActivationContext._activationContext, out this._cookie);
					if (flag3)
					{
						this._cookie = IntPtr.Zero;
					}
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002274 File Offset: 0x00000474
		~ComCtlv6ActivationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A8 File Offset: 0x000004A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022BC File Offset: 0x000004BC
		private void Dispose(bool disposing)
		{
			bool flag = this._cookie != IntPtr.Zero;
			if (flag)
			{
				bool flag2 = NativeMethods.DeactivateActCtx(0U, this._cookie);
				if (flag2)
				{
					this._cookie = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002300 File Offset: 0x00000500
		private static bool EnsureActivateContextCreated()
		{
			object contextCreationLock = ComCtlv6ActivationContext._contextCreationLock;
			bool contextCreationSucceeded;
			lock (contextCreationLock)
			{
				bool flag = !ComCtlv6ActivationContext._contextCreationSucceeded;
				if (flag)
				{
					string location = typeof(object).Assembly.Location;
					string text = null;
					string text2 = null;
					bool flag2 = location != null;
					if (flag2)
					{
						text2 = Path.GetDirectoryName(location);
						text = Path.Combine(text2, "XPThemes.manifest");
					}
					bool flag3 = text != null && text2 != null;
					if (flag3)
					{
						ComCtlv6ActivationContext._enableThemingActivationContext = default(NativeMethods.ACTCTX);
						ComCtlv6ActivationContext._enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(NativeMethods.ACTCTX));
						ComCtlv6ActivationContext._enableThemingActivationContext.lpSource = text;
						ComCtlv6ActivationContext._enableThemingActivationContext.lpAssemblyDirectory = text2;
						ComCtlv6ActivationContext._enableThemingActivationContext.dwFlags = 4U;
						ComCtlv6ActivationContext._activationContext = NativeMethods.CreateActCtx(ref ComCtlv6ActivationContext._enableThemingActivationContext);
						ComCtlv6ActivationContext._contextCreationSucceeded = !ComCtlv6ActivationContext._activationContext.IsInvalid;
					}
				}
				contextCreationSucceeded = ComCtlv6ActivationContext._contextCreationSucceeded;
			}
			return contextCreationSucceeded;
		}

		// Token: 0x04000011 RID: 17
		private IntPtr _cookie;

		// Token: 0x04000012 RID: 18
		private static NativeMethods.ACTCTX _enableThemingActivationContext;

		// Token: 0x04000013 RID: 19
		private static ActivationContextSafeHandle _activationContext;

		// Token: 0x04000014 RID: 20
		private static bool _contextCreationSucceeded;

		// Token: 0x04000015 RID: 21
		private static readonly object _contextCreationLock = new object();
	}
}
