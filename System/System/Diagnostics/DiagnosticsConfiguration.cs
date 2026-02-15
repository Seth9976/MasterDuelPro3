using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000150 RID: 336
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002B3C4 File Offset: 0x000295C4
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0002B3EC File Offset: 0x000295EC
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002B41C File Offset: 0x0002961C
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002B450 File Offset: 0x00029650
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002B484 File Offset: 0x00029684
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0002B4B8 File Offset: 0x000296B8
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0002B4E0 File Offset: 0x000296E0
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0002B50D File Offset: 0x0002970D
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002B51C File Offset: 0x0002971C
		private static SystemDiagnosticsSection GetConfigSection()
		{
			object section = PrivilegedConfigurationManager.GetSection("system.diagnostics");
			if (section is SystemDiagnosticsSection)
			{
				return (SystemDiagnosticsSection)section;
			}
			return null;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002B544 File Offset: 0x00029744
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002B550 File Offset: 0x00029750
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002B55C File Offset: 0x0002975C
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002B574 File Offset: 0x00029774
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x0400060E RID: 1550
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x0400060F RID: 1551
		private static volatile InitState initState;
	}
}
