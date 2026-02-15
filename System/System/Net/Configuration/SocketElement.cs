using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000499 RID: 1177
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class. </summary>
		// Token: 0x06001CB7 RID: 7351 RVA: 0x0007D1EC File Offset: 0x0007B3EC
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0007D265 File Offset: 0x0007B465
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x040013CD RID: 5069
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040013CE RID: 5070
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x040013CF RID: 5071
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
