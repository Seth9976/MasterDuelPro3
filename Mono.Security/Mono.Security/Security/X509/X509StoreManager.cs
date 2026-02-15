using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x02000021 RID: 33
	public sealed class X509StoreManager
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00008AF8 File Offset: 0x00006CF8
		internal static string CurrentUserPath
		{
			get
			{
				if (X509StoreManager._userPath == null)
				{
					X509StoreManager._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					X509StoreManager._userPath = Path.Combine(X509StoreManager._userPath, "certs");
				}
				return X509StoreManager._userPath;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00008B30 File Offset: 0x00006D30
		internal static string LocalMachinePath
		{
			get
			{
				if (X509StoreManager._localMachinePath == null)
				{
					X509StoreManager._localMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					X509StoreManager._localMachinePath = Path.Combine(X509StoreManager._localMachinePath, "certs");
				}
				return X509StoreManager._localMachinePath;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00008B68 File Offset: 0x00006D68
		internal static string NewCurrentUserPath
		{
			get
			{
				if (X509StoreManager._newUserPath == null)
				{
					X509StoreManager._newUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					X509StoreManager._newUserPath = Path.Combine(X509StoreManager._newUserPath, "new-certs");
				}
				return X509StoreManager._newUserPath;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008BA0 File Offset: 0x00006DA0
		internal static string NewLocalMachinePath
		{
			get
			{
				if (X509StoreManager._newLocalMachinePath == null)
				{
					X509StoreManager._newLocalMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					X509StoreManager._newLocalMachinePath = Path.Combine(X509StoreManager._newLocalMachinePath, "new-certs");
				}
				return X509StoreManager._newLocalMachinePath;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public static X509Stores CurrentUser
		{
			get
			{
				if (X509StoreManager._userStore == null)
				{
					X509StoreManager._userStore = new X509Stores(X509StoreManager.CurrentUserPath, false);
				}
				return X509StoreManager._userStore;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00008BF6 File Offset: 0x00006DF6
		public static X509Stores LocalMachine
		{
			get
			{
				if (X509StoreManager._machineStore == null)
				{
					X509StoreManager._machineStore = new X509Stores(X509StoreManager.LocalMachinePath, false);
				}
				return X509StoreManager._machineStore;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00008C14 File Offset: 0x00006E14
		public static X509CertificateCollection TrustedRootCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.TrustedRoot.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.TrustedRoot.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x04000089 RID: 137
		private static string _userPath;

		// Token: 0x0400008A RID: 138
		private static string _localMachinePath;

		// Token: 0x0400008B RID: 139
		private static string _newUserPath;

		// Token: 0x0400008C RID: 140
		private static string _newLocalMachinePath;

		// Token: 0x0400008D RID: 141
		private static X509Stores _userStore;

		// Token: 0x0400008E RID: 142
		private static X509Stores _machineStore;
	}
}
