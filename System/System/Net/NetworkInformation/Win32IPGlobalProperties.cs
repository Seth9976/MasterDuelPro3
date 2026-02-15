using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000466 RID: 1126
	internal class Win32IPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06001C4C RID: 7244 RVA: 0x0007C04C File Offset: 0x0007A24C
		private unsafe void FillTcpTable(out List<Win32IPGlobalProperties.Win32_MIB_TCPROW> tab4, out List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> tab6)
		{
			tab4 = new List<Win32IPGlobalProperties.Win32_MIB_TCPROW>();
			int num = 0;
			Win32IPGlobalProperties.GetTcpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetTcpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_TCPROW win32_MIB_TCPROW = new Win32IPGlobalProperties.Win32_MIB_TCPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_TCPROW);
					tab4.Add(win32_MIB_TCPROW);
				}
			}
			tab6 = new List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetTcp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetTcp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_TCP6ROW win32_MIB_TCP6ROW = new Win32IPGlobalProperties.Win32_MIB_TCP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_TCP6ROW);
						tab6.Add(win32_MIB_TCP6ROW);
					}
				}
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0007C192 File Offset: 0x0007A392
		private bool IsListenerState(TcpState state)
		{
			return state - TcpState.Listen <= 1 || state - TcpState.FinWait1 <= 2;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0007C1A4 File Offset: 0x0007A3A4
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			List<IPEndPoint> list3 = new List<IPEndPoint>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (this.IsListenerState(list[i].State))
				{
					list3.Add(list[i].LocalEndPoint);
				}
				i++;
			}
			int j = 0;
			int count2 = list2.Count;
			while (j < count2)
			{
				if (this.IsListenerState(list2[j].State))
				{
					list3.Add(list2[j].LocalEndPoint);
				}
				j++;
			}
			return list3.ToArray();
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0007C247 File Offset: 0x0007A447
		public override string DomainName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.DomainName;
			}
		}

		// Token: 0x06001C50 RID: 7248
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpTable(byte[] pTcpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06001C51 RID: 7249
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcp6Table(byte[] TcpTable, ref int SizePointer, bool Order);

		// Token: 0x06001C52 RID: 7250
		[DllImport("Ws2_32.dll")]
		private static extern ushort ntohs(ushort netshort);

		// Token: 0x02000467 RID: 1127
		[StructLayout(LayoutKind.Explicit)]
		private struct Win32_IN6_ADDR
		{
			// Token: 0x04001308 RID: 4872
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Bytes;
		}

		// Token: 0x02000468 RID: 1128
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCPROW
		{
			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0007C25B File Offset: 0x0007A45B
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x04001309 RID: 4873
			public TcpState State;

			// Token: 0x0400130A RID: 4874
			public uint LocalAddr;

			// Token: 0x0400130B RID: 4875
			public uint LocalPort;

			// Token: 0x0400130C RID: 4876
			public uint RemoteAddr;

			// Token: 0x0400130D RID: 4877
			public uint RemotePort;
		}

		// Token: 0x02000469 RID: 1129
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCP6ROW
		{
			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0007C275 File Offset: 0x0007A475
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x0400130E RID: 4878
			public TcpState State;

			// Token: 0x0400130F RID: 4879
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04001310 RID: 4880
			public uint LocalScopeId;

			// Token: 0x04001311 RID: 4881
			public uint LocalPort;

			// Token: 0x04001312 RID: 4882
			public Win32IPGlobalProperties.Win32_IN6_ADDR RemoteAddr;

			// Token: 0x04001313 RID: 4883
			public uint RemoteScopeId;

			// Token: 0x04001314 RID: 4884
			public uint RemotePort;
		}
	}
}
