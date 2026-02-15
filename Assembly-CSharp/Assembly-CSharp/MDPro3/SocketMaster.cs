using System;
using System.Net.Sockets;

namespace MDPro3
{
	// Token: 0x0200121E RID: 4638
	public class SocketMaster
	{
		// Token: 0x0600897F RID: 35199 RVA: 0x0010AB64 File Offset: 0x00108D64
		private static byte[] ReadFull(NetworkStream stream, int length)
		{
			byte[] buf = new byte[length];
			int rlen = 0;
			while (rlen < buf.Length)
			{
				int currentLength = stream.Read(buf, rlen, buf.Length - rlen);
				rlen += currentLength;
				if (currentLength == 0)
				{
					TcpHelper.onDisConnected = true;
					break;
				}
			}
			return buf;
		}

		// Token: 0x06008980 RID: 35200 RVA: 0x0010ABA0 File Offset: 0x00108DA0
		public static byte[] ReadPacket(NetworkStream stream)
		{
			ushort plen = BitConverter.ToUInt16(SocketMaster.ReadFull(stream, 2), 0);
			return SocketMaster.ReadFull(stream, (int)plen);
		}
	}
}
