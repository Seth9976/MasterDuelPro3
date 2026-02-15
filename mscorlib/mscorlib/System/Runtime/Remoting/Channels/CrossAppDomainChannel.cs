using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000454 RID: 1108
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x06002473 RID: 9331 RVA: 0x00095C7C File Offset: 0x00093E7C
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				ChannelServices.RegisterChannel(new CrossAppDomainChannel());
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00095CC0 File Offset: 0x00093EC0
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00095CC7 File Offset: 0x00093EC7
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x00095CCB File Offset: 0x00093ECB
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00095CD8 File Offset: 0x00093ED8
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x04001191 RID: 4497
		private static object s_lock = new object();
	}
}
