using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000169 RID: 361
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0002D55C File Offset: 0x0002B75C
		static TraceSection()
		{
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0002D62E File Offset: 0x0002B82E
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0002D640 File Offset: 0x0002B840
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0002D652 File Offset: 0x0002B852
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0002D664 File Offset: 0x0002B864
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0002D676 File Offset: 0x0002B876
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x04000675 RID: 1653
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000676 RID: 1654
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04000677 RID: 1655
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000678 RID: 1656
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x04000679 RID: 1657
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
