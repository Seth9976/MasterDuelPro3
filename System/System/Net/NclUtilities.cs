using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003B1 RID: 945
	internal static class NclUtilities
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x000646E0 File Offset: 0x000628E0
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00064704 File Offset: 0x00062904
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00064734 File Offset: 0x00062934
		private static IPHostEntry GetLocalHost()
		{
			return Dns.GetHostByName(Dns.GetHostName());
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00064740 File Offset: 0x00062940
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				IPAddress[] array = NclUtilities._LocalAddresses;
				if (array != null)
				{
					return array;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] array2;
				lock (localAddressesLock)
				{
					array = NclUtilities._LocalAddresses;
					if (array != null)
					{
						array2 = array;
					}
					else
					{
						List<IPAddress> list = new List<IPAddress>();
						try
						{
							IPHostEntry localHost = NclUtilities.GetLocalHost();
							if (localHost != null)
							{
								if (localHost.HostName != null)
								{
									int num = localHost.HostName.IndexOf('.');
									if (num != -1)
									{
										NclUtilities._LocalDomainName = localHost.HostName.Substring(num);
									}
								}
								IPAddress[] addressList = localHost.AddressList;
								if (addressList != null)
								{
									foreach (IPAddress ipaddress in addressList)
									{
										list.Add(ipaddress);
									}
								}
							}
						}
						catch
						{
						}
						array = new IPAddress[list.Count];
						int num2 = 0;
						foreach (IPAddress ipaddress2 in list)
						{
							array[num2] = ipaddress2;
							num2++;
						}
						NclUtilities._LocalAddresses = array;
						array2 = array;
					}
				}
				return array2;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00064880 File Offset: 0x00062A80
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x04000ECC RID: 3788
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x04000ECD RID: 3789
		private static object _LocalAddressesLock;

		// Token: 0x04000ECE RID: 3790
		internal static string _LocalDomainName;
	}
}
