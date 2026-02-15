using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Mono.Net.Dns;

namespace System.Net
{
	/// <summary>Provides simple domain name resolution functionality.</summary>
	// Token: 0x02000403 RID: 1027
	public static class Dns
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x0006CCAD File Offset: 0x0006AEAD
		static Dns()
		{
			if (Environment.GetEnvironmentVariable("MONO_DNS") != null)
			{
				Dns.resolver = new SimpleResolver();
				Dns.use_mono_dns = true;
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0006CCCC File Offset: 0x0006AECC
		private static void OnCompleted(object sender, SimpleResolverEventArgs e)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)e.UserToken;
			IPHostEntry hostEntry = e.HostEntry;
			if (hostEntry == null || e.ResolverError != ResolverError.NoError)
			{
				dnsAsyncResult.SetCompleted(false, new Exception("Error: " + e.ResolverError.ToString()));
				return;
			}
			dnsAsyncResult.SetCompleted(false, hostEntry);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006CD2C File Offset: 0x0006AF2C
		private static IAsyncResult BeginAsyncCallAddresses(string host, AsyncCallback callback, object state)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = new SimpleResolverEventArgs();
			simpleResolverEventArgs.Completed += Dns.OnCompleted;
			simpleResolverEventArgs.HostName = host;
			DnsAsyncResult dnsAsyncResult = new DnsAsyncResult(callback, state);
			simpleResolverEventArgs.UserToken = dnsAsyncResult;
			if (!Dns.resolver.GetHostAddressesAsync(simpleResolverEventArgs))
			{
				dnsAsyncResult.SetCompleted(true, simpleResolverEventArgs.HostEntry);
			}
			return dnsAsyncResult;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0006CD84 File Offset: 0x0006AF84
		private static IPHostEntry EndAsyncCall(DnsAsyncResult ares)
		{
			if (ares == null)
			{
				throw new ArgumentException("Invalid asyncResult");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (ares.Exception != null)
			{
				throw ares.Exception;
			}
			IPHostEntry hostEntry = ares.HostEntry;
			if (hostEntry == null || hostEntry.AddressList == null || hostEntry.AddressList.Length == 0)
			{
				Dns.Error_11001(hostEntry.HostName);
			}
			return hostEntry;
		}

		/// <summary>Asynchronously returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x0600196A RID: 6506 RVA: 0x0006CDEC File Offset: 0x0006AFEC
		public static IAsyncResult BeginGetHostAddresses(string hostNameOrAddress, AsyncCallback requestCallback, object state)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCallAddresses(hostNameOrAddress, requestCallback, state);
			}
			return new Dns.GetHostAddressesCallback(Dns.GetHostAddresses).BeginInvoke(hostNameOrAddress, requestCallback, state);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host specified by the <paramref name="hostNameOrAddress" /> parameter of <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		// Token: 0x0600196B RID: 6507 RVA: 0x0006CE58 File Offset: 0x0006B058
		public static IPAddress[] EndGetHostAddresses(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!Dns.use_mono_dns)
			{
				return ((Dns.GetHostAddressesCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
			}
			IPHostEntry iphostEntry = Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			if (iphostEntry == null)
			{
				return null;
			}
			return iphostEntry.AddressList;
		}

		// Token: 0x0600196C RID: 6508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByName_icall(string host, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x0600196D RID: 6509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByAddr_icall(string addr, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x0600196E RID: 6510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostName_icall(out string h_name);

		// Token: 0x0600196F RID: 6511 RVA: 0x0006CEA8 File Offset: 0x0006B0A8
		private static void Error_11001(string hostName)
		{
			throw new SocketException(11001, string.Format("Could not resolve host '{0}'", hostName));
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006CEC0 File Offset: 0x0006B0C0
		private static IPHostEntry hostent_to_IPHostEntry(string originalHostName, string h_name, string[] h_aliases, string[] h_addrlist)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			ArrayList arrayList = new ArrayList();
			iphostEntry.HostName = h_name;
			iphostEntry.Aliases = h_aliases;
			for (int i = 0; i < h_addrlist.Length; i++)
			{
				try
				{
					IPAddress ipaddress = IPAddress.Parse(h_addrlist[i]);
					if ((Socket.OSSupportsIPv6 && ipaddress.AddressFamily == AddressFamily.InterNetworkV6) || (Socket.OSSupportsIPv4 && ipaddress.AddressFamily == AddressFamily.InterNetwork))
					{
						arrayList.Add(ipaddress);
					}
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (arrayList.Count == 0)
			{
				Dns.Error_11001(originalHostName);
			}
			iphostEntry.AddressList = arrayList.ToArray(typeof(IPAddress)) as IPAddress[];
			return iphostEntry;
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from an IP address.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <param name="address">An IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not a valid IP address. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001971 RID: 6513 RVA: 0x0006CF68 File Offset: 0x0006B168
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address, true);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006CF80 File Offset: 0x0006B180
		private static IPHostEntry GetHostByAddressFromString(string address, bool parse)
		{
			if (address.Equals("0.0.0.0"))
			{
				address = "127.0.0.1";
				parse = false;
			}
			if (parse)
			{
				IPAddress.Parse(address);
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByAddr_icall(address, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(address);
			}
			return Dns.hostent_to_IPHostEntry(address, text, array, array2);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address. </exception>
		// Token: 0x06001973 RID: 6515 RVA: 0x0006CFD0 File Offset: 0x0006B1D0
		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return Dns.GetHostEntry(ipaddress);
			}
			return Dns.GetHostByName(hostNameOrAddress);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <param name="address">An IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x06001974 RID: 6516 RVA: 0x0006D035 File Offset: 0x0006B235
		public static IPHostEntry GetHostEntry(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06001975 RID: 6517 RVA: 0x0006D054 File Offset: 0x0006B254
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return new IPAddress[] { ipaddress };
			}
			return Dns.GetHostEntry(hostNameOrAddress).AddressList;
		}

		/// <summary>Gets the DNS information for the specified DNS host name.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains host information for the address specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001976 RID: 6518 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByName_icall(hostName, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(hostName);
			}
			return Dns.hostent_to_IPHostEntry(hostName, text, array, array2);
		}

		/// <summary>Gets the host name of the local computer.</summary>
		/// <returns>A string that contains the DNS host name of the local computer.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001977 RID: 6519 RVA: 0x0006D104 File Offset: 0x0006B304
		public static string GetHostName()
		{
			string text;
			if (!Dns.GetHostName_icall(out text))
			{
				Dns.Error_11001(text);
			}
			return text;
		}

		/// <summary>Resolves a DNS host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">A DNS-style host name or IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001978 RID: 6520 RVA: 0x0006D124 File Offset: 0x0006B324
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry Resolve(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.GetHostByAddress(hostName);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				iphostEntry = Dns.GetHostByName(hostName);
			}
			return iphostEntry;
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06001979 RID: 6521 RVA: 0x0006D168 File Offset: 0x0006B368
		public static Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress)
		{
			return Task<IPAddress[]>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostAddresses), new Func<IAsyncResult, IPAddress[]>(Dns.EndGetHostAddresses), hostNameOrAddress, null);
		}

		// Token: 0x04001031 RID: 4145
		private static bool use_mono_dns;

		// Token: 0x04001032 RID: 4146
		private static SimpleResolver resolver;

		// Token: 0x02000404 RID: 1028
		// (Invoke) Token: 0x0600197B RID: 6523
		private delegate IPAddress[] GetHostAddressesCallback(string hostName);
	}
}
