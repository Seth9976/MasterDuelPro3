using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200033B RID: 827
	internal static class DefaultPolicies
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00072FF1 File Offset: 0x000711F1
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x00073009 File Offset: 0x00071209
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00073021 File Offset: 0x00071221
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00073039 File Offset: 0x00071239
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00073051 File Offset: 0x00071251
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x00073069 File Offset: 0x00071269
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00073081 File Offset: 0x00071281
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0007309C File Offset: 0x0007129C
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					strongNamePublicKeyBlob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				strongNamePublicKeyBlob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("4.0.0.0");
			}
			return new StrongNameMembershipCondition(strongNamePublicKeyBlob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00073116 File Offset: 0x00071316
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00073124 File Offset: 0x00071324
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000731D4 File Offset: 0x000713D4
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0007324D File Offset: 0x0007144D
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00073267 File Offset: 0x00071467
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00073281 File Offset: 0x00071481
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00073290 File Offset: 0x00071490
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000733EE File Offset: 0x000715EE
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00073426 File Offset: 0x00071626
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
				0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
				82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
				1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
				163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
				253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
				76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
				220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
				11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
				16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
				146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
				232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
				40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
				61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
				15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
				24, 32, 109, 192, 147, 52, 77, 90, 210, 147
			};
		}

		// Token: 0x04000D89 RID: 3465
		private static Version _fxVersion;

		// Token: 0x04000D8A RID: 3466
		private static byte[] _ecmaKey;

		// Token: 0x04000D8B RID: 3467
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04000D8C RID: 3468
		private static byte[] _msFinalKey;

		// Token: 0x04000D8D RID: 3469
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04000D8E RID: 3470
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04000D8F RID: 3471
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04000D90 RID: 3472
		private static NamedPermissionSet _internet;

		// Token: 0x04000D91 RID: 3473
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04000D92 RID: 3474
		private static NamedPermissionSet _execution;

		// Token: 0x04000D93 RID: 3475
		private static NamedPermissionSet _nothing;

		// Token: 0x04000D94 RID: 3476
		private static NamedPermissionSet _everything;

		// Token: 0x0200033C RID: 828
		public enum Key
		{
			// Token: 0x04000D96 RID: 3478
			Ecma,
			// Token: 0x04000D97 RID: 3479
			MsFinal
		}
	}
}
