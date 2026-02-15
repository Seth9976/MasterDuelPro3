using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x020003AE RID: 942
	public class IPHostEntry
	{
		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0006469E File Offset: 0x0006289E
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x000646A6 File Offset: 0x000628A6
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x170004FF RID: 1279
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x000646AF File Offset: 0x000628AF
		public string[] Aliases
		{
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x000646B8 File Offset: 0x000628B8
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x000646C0 File Offset: 0x000628C0
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x04000EC8 RID: 3784
		private string hostName;

		// Token: 0x04000EC9 RID: 3785
		private string[] aliases;

		// Token: 0x04000ECA RID: 3786
		private IPAddress[] addressList;

		// Token: 0x04000ECB RID: 3787
		internal bool isTrustedHost = true;
	}
}
