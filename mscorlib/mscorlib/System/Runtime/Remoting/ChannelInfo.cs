using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x0200040E RID: 1038
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x0008F454 File Offset: 0x0008D654
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0008F467 File Offset: 0x0008D667
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[] { remoteChannelData };
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0008F47F File Offset: 0x0008D67F
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
		}

		// Token: 0x040010DC RID: 4316
		private object[] channelData;
	}
}
