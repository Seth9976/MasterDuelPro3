using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x02000387 RID: 903
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005F872 File Offset: 0x0005DA72
		public override AddressFamily AddressFamily
		{
			get
			{
				return this._address.AddressFamily;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001685 RID: 5765 RVA: 0x0005F87F File Offset: 0x0005DA7F
		public IPEndPoint(long address, int port)
		{
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = new IPAddress(address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001686 RID: 5766 RVA: 0x0005F8AD File Offset: 0x0005DAAD
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = address;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0005F8EC File Offset: 0x0005DAEC
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001689 RID: 5769 RVA: 0x0005F8F4 File Offset: 0x0005DAF4
		public override string ToString()
		{
			return string.Format((this._address.AddressFamily == AddressFamily.InterNetworkV6) ? "[{0}]:{1}" : "{0}:{1}", this._address.ToString(), this.Port.ToString(NumberFormatInfo.InvariantInfo));
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x0600168A RID: 5770 RVA: 0x0005F93F File Offset: 0x0005DB3F
		public override SocketAddress Serialize()
		{
			return new SocketAddress(this.Address, this.Port);
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint. </param>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.-or- <paramref name="socketAddress" />.Size &lt; 8. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600168B RID: 5771 RVA: 0x0005F954 File Offset: 0x0005DB54
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(SR.Format("The AddressFamily {0} is not valid for the {1} end point, use {2} instead.", socketAddress.Family.ToString(), base.GetType().FullName, this.AddressFamily.ToString()), "socketAddress");
			}
			if (socketAddress.Size < 8)
			{
				throw new ArgumentException(SR.Format("The supplied {0} is an invalid size for the {1} end point.", socketAddress.GetType().FullName, base.GetType().FullName), "socketAddress");
			}
			return socketAddress.GetIPEndPoint();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.IPEndPoint" /> instance.</param>
		// Token: 0x0600168C RID: 5772 RVA: 0x0005F9F4 File Offset: 0x0005DBF4
		public override bool Equals(object comparand)
		{
			IPEndPoint ipendPoint = comparand as IPEndPoint;
			return ipendPoint != null && ipendPoint._address.Equals(this._address) && ipendPoint._port == this._port;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600168D RID: 5773 RVA: 0x0005FA2E File Offset: 0x0005DC2E
		public override int GetHashCode()
		{
			return this._address.GetHashCode() ^ this._port;
		}

		// Token: 0x04000DA9 RID: 3497
		private IPAddress _address;

		// Token: 0x04000DAA RID: 3498
		private int _port;

		// Token: 0x04000DAB RID: 3499
		internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);

		// Token: 0x04000DAC RID: 3500
		internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
	}
}
