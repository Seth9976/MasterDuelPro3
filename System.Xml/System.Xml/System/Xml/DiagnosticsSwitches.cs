using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x020000CC RID: 204
	internal static class DiagnosticsSwitches
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0003895C File Offset: 0x00036B5C
		public static BooleanSwitch KeepTempFiles
		{
			get
			{
				if (DiagnosticsSwitches.keepTempFiles == null)
				{
					DiagnosticsSwitches.keepTempFiles = new BooleanSwitch("XmlSerialization.Compilation", "Keep XmlSerialization generated (temp) files.");
				}
				return DiagnosticsSwitches.keepTempFiles;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00038984 File Offset: 0x00036B84
		public static BooleanSwitch PregenEventLog
		{
			get
			{
				if (DiagnosticsSwitches.pregenEventLog == null)
				{
					DiagnosticsSwitches.pregenEventLog = new BooleanSwitch("XmlSerialization.PregenEventLog", "Log failures while loading pre-generated XmlSerialization assembly.");
				}
				return DiagnosticsSwitches.pregenEventLog;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000389AC File Offset: 0x00036BAC
		public static BooleanSwitch NonRecursiveTypeLoading
		{
			get
			{
				if (DiagnosticsSwitches.nonRecursiveTypeLoading == null)
				{
					DiagnosticsSwitches.nonRecursiveTypeLoading = new BooleanSwitch("XmlSerialization.NonRecursiveTypeLoading", "Turn on non-recursive algorithm generating XmlMappings for CLR types.");
				}
				return DiagnosticsSwitches.nonRecursiveTypeLoading;
			}
		}

		// Token: 0x040005DC RID: 1500
		private static volatile BooleanSwitch keepTempFiles;

		// Token: 0x040005DD RID: 1501
		private static volatile BooleanSwitch pregenEventLog;

		// Token: 0x040005DE RID: 1502
		private static volatile BooleanSwitch nonRecursiveTypeLoading;
	}
}
