using System;

namespace Mono
{
	// Token: 0x02000010 RID: 16
	internal class SystemDependencyProvider : ISystemDependencyProvider
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000026F9 File Offset: 0x000008F9
		public static SystemDependencyProvider Instance
		{
			get
			{
				SystemDependencyProvider.Initialize();
				return SystemDependencyProvider.instance;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002708 File Offset: 0x00000908
		internal static void Initialize()
		{
			object obj = SystemDependencyProvider.syncRoot;
			lock (obj)
			{
				if (SystemDependencyProvider.instance == null)
				{
					SystemDependencyProvider.instance = new SystemDependencyProvider();
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002754 File Offset: 0x00000954
		ISystemCertificateProvider ISystemDependencyProvider.CertificateProvider
		{
			get
			{
				return this.CertificateProvider;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000275C File Offset: 0x0000095C
		public SystemCertificateProvider CertificateProvider { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002764 File Offset: 0x00000964
		public X509PalImpl X509Pal
		{
			get
			{
				return this.CertificateProvider.X509Pal;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002771 File Offset: 0x00000971
		private SystemDependencyProvider()
		{
			this.CertificateProvider = new SystemCertificateProvider();
			DependencyInjector.Register(this);
		}

		// Token: 0x04000020 RID: 32
		private static SystemDependencyProvider instance;

		// Token: 0x04000021 RID: 33
		private static object syncRoot = new object();
	}
}
