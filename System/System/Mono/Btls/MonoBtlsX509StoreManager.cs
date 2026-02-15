using System;
using System.IO;
using Mono.Security.X509;

namespace Mono.Btls
{
	// Token: 0x020000CE RID: 206
	internal static class MonoBtlsX509StoreManager
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		private static void Initialize()
		{
			if (MonoBtlsX509StoreManager.initialized)
			{
				return;
			}
			try
			{
				MonoBtlsX509StoreManager.DoInitialize();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("MonoBtlsX509StoreManager.Initialize() threw exception: {0}", ex);
			}
			finally
			{
				MonoBtlsX509StoreManager.initialized = true;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D030 File Offset: 0x0000B230
		private static void DoInitialize()
		{
			string newCurrentUserPath = X509StoreManager.NewCurrentUserPath;
			MonoBtlsX509StoreManager.userTrustedRootPath = Path.Combine(newCurrentUserPath, "Trust");
			MonoBtlsX509StoreManager.userIntermediateCAPath = Path.Combine(newCurrentUserPath, "CA");
			MonoBtlsX509StoreManager.userUntrustedPath = Path.Combine(newCurrentUserPath, "Disallowed");
			string newLocalMachinePath = X509StoreManager.NewLocalMachinePath;
			MonoBtlsX509StoreManager.machineTrustedRootPath = Path.Combine(newLocalMachinePath, "Trust");
			MonoBtlsX509StoreManager.machineIntermediateCAPath = Path.Combine(newLocalMachinePath, "CA");
			MonoBtlsX509StoreManager.machineUntrustedPath = Path.Combine(newLocalMachinePath, "Disallowed");
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public static string GetStorePath(MonoBtlsX509StoreType type)
		{
			MonoBtlsX509StoreManager.Initialize();
			switch (type)
			{
			case MonoBtlsX509StoreType.MachineTrustedRoots:
				return MonoBtlsX509StoreManager.machineTrustedRootPath;
			case MonoBtlsX509StoreType.MachineIntermediateCA:
				return MonoBtlsX509StoreManager.machineIntermediateCAPath;
			case MonoBtlsX509StoreType.MachineUntrusted:
				return MonoBtlsX509StoreManager.machineUntrustedPath;
			case MonoBtlsX509StoreType.UserTrustedRoots:
				return MonoBtlsX509StoreManager.userTrustedRootPath;
			case MonoBtlsX509StoreType.UserIntermediateCA:
				return MonoBtlsX509StoreManager.userIntermediateCAPath;
			case MonoBtlsX509StoreType.UserUntrusted:
				return MonoBtlsX509StoreManager.userUntrustedPath;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000317 RID: 791
		private static bool initialized;

		// Token: 0x04000318 RID: 792
		private static string machineTrustedRootPath;

		// Token: 0x04000319 RID: 793
		private static string machineIntermediateCAPath;

		// Token: 0x0400031A RID: 794
		private static string machineUntrustedPath;

		// Token: 0x0400031B RID: 795
		private static string userTrustedRootPath;

		// Token: 0x0400031C RID: 796
		private static string userIntermediateCAPath;

		// Token: 0x0400031D RID: 797
		private static string userUntrustedPath;
	}
}
