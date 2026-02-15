using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200015E RID: 350
	internal class SystemDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0002C5CC File Offset: 0x0002A7CC
		static SystemDiagnosticsSection()
		{
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propAssert);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propPerfCounters);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSources);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSharedListeners);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSwitches);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propTrace);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0002C6F7 File Offset: 0x0002A8F7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDiagnosticsSection._properties;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0002C6FE File Offset: 0x0002A8FE
		[ConfigurationProperty("sources")]
		public SourceElementsCollection Sources
		{
			get
			{
				return (SourceElementsCollection)base[SystemDiagnosticsSection._propSources];
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0002C710 File Offset: 0x0002A910
		[ConfigurationProperty("sharedListeners")]
		public ListenerElementsCollection SharedListeners
		{
			get
			{
				return (ListenerElementsCollection)base[SystemDiagnosticsSection._propSharedListeners];
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0002C722 File Offset: 0x0002A922
		[ConfigurationProperty("switches")]
		public SwitchElementsCollection Switches
		{
			get
			{
				return (SwitchElementsCollection)base[SystemDiagnosticsSection._propSwitches];
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0002C734 File Offset: 0x0002A934
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base[SystemDiagnosticsSection._propTrace];
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002C746 File Offset: 0x0002A946
		protected override void InitializeDefault()
		{
			this.Trace.Listeners.InitializeDefaultInternal();
		}

		// Token: 0x0400063A RID: 1594
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400063B RID: 1595
		private static readonly ConfigurationProperty _propAssert = new ConfigurationProperty("assert", typeof(AssertSection), new AssertSection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400063C RID: 1596
		private static readonly ConfigurationProperty _propPerfCounters = new ConfigurationProperty("performanceCounters", typeof(PerfCounterSection), new PerfCounterSection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400063D RID: 1597
		private static readonly ConfigurationProperty _propSources = new ConfigurationProperty("sources", typeof(SourceElementsCollection), new SourceElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400063E RID: 1598
		private static readonly ConfigurationProperty _propSharedListeners = new ConfigurationProperty("sharedListeners", typeof(SharedListenerElementsCollection), new SharedListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400063F RID: 1599
		private static readonly ConfigurationProperty _propSwitches = new ConfigurationProperty("switches", typeof(SwitchElementsCollection), new SwitchElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04000640 RID: 1600
		private static readonly ConfigurationProperty _propTrace = new ConfigurationProperty("trace", typeof(TraceSection), new TraceSection(), ConfigurationPropertyOptions.None);
	}
}
