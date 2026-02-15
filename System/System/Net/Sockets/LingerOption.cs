using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020004B4 RID: 1204
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">true to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, false. </param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called. </param>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x00081C46 File Offset: 0x0007FE46
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, false.</returns>
		// Token: 0x1700068E RID: 1678
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x00081C5C File Offset: 0x0007FE5C
		public bool Enabled
		{
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x1700068F RID: 1679
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x00081C65 File Offset: 0x0007FE65
		public int LingerTime
		{
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x04001485 RID: 5253
		private bool enabled;

		// Token: 0x04001486 RID: 5254
		private int lingerTime;
	}
}
