using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x02000384 RID: 900
	[Serializable]
	public class IPAddress
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005EE04 File Offset: 0x0005D004
		private bool IsIPv4
		{
			get
			{
				return this._numbers == null;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005EE0F File Offset: 0x0005D00F
		private bool IsIPv6
		{
			get
			{
				return this._numbers != null;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0005EE1A File Offset: 0x0005D01A
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x0005EE22 File Offset: 0x0005D022
		private uint PrivateAddress
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0005EE1A File Offset: 0x0005D01A
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x0005EE22 File Offset: 0x0005D022
		private uint PrivateScopeId
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36". </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newAddress" /> &lt; 0 or <paramref name="newAddress" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x0600165F RID: 5727 RVA: 0x0005EE39 File Offset: 0x0005D039
		public IPAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("newAddress");
			}
			this.PrivateAddress = (uint)newAddress;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <param name="scopeid">The long value of the scope identifier. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06001660 RID: 5728 RVA: 0x0005EE5E File Offset: 0x0005D05E
		public IPAddress(byte[] address, long scopeid)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()), scopeid)
		{
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0005EE78 File Offset: 0x0005D078
		public unsafe IPAddress(ReadOnlySpan<byte> address, long scopeid)
		{
			if (address.Length != 16)
			{
				throw new ArgumentException("An invalid IP address was specified.", "address");
			}
			if (scopeid < 0L || scopeid > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("scopeid");
			}
			this._numbers = new ushort[8];
			for (int i = 0; i < 8; i++)
			{
				this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
			}
			this.PrivateScopeId = (uint)scopeid;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0005EF04 File Offset: 0x0005D104
		internal unsafe IPAddress(ushort* numbers, int numbersLength, uint scopeid)
		{
			ushort[] array = new ushort[8];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = numbers[i];
			}
			this._numbers = array;
			this.PrivateScopeId = scopeid;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0005EF44 File Offset: 0x0005D144
		private IPAddress(ushort[] numbers, uint scopeid)
		{
			this._numbers = numbers;
			this.PrivateScopeId = scopeid;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		// Token: 0x06001664 RID: 5732 RVA: 0x0005EF5A File Offset: 0x0005D15A
		public IPAddress(byte[] address)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()))
		{
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0005EF74 File Offset: 0x0005D174
		public unsafe IPAddress(ReadOnlySpan<byte> address)
		{
			if (address.Length == 4)
			{
				this.PrivateAddress = (uint)((long)(((int)(*address[3]) << 24) | ((int)(*address[2]) << 16) | ((int)(*address[1]) << 8) | (int)(*address[0])) & (long)((ulong)(-1)));
				return;
			}
			if (address.Length == 16)
			{
				this._numbers = new ushort[8];
				for (int i = 0; i < 8; i++)
				{
					this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
				}
				return;
			}
			throw new ArgumentException("An invalid IP address was specified.", "address");
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <returns>true if <paramref name="ipString" /> is a valid IP address; otherwise, false.</returns>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		// Token: 0x06001666 RID: 5734 RVA: 0x0005F027 File Offset: 0x0005D227
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				address = null;
				return false;
			}
			address = IPAddressParser.Parse(ipString.AsSpan(), true);
			return address != null;
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address. </exception>
		// Token: 0x06001667 RID: 5735 RVA: 0x0005F044 File Offset: 0x0005D244
		public static IPAddress Parse(string ipString)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			return IPAddressParser.Parse(ipString.AsSpan(), false);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005F060 File Offset: 0x0005D260
		public bool TryWriteBytes(Span<byte> destination, out int bytesWritten)
		{
			if (this.IsIPv6)
			{
				if (destination.Length < 16)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv6Bytes(destination);
				bytesWritten = 16;
			}
			else
			{
				if (destination.Length < 4)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv4Bytes(destination);
				bytesWritten = 4;
			}
			return true;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005F0AC File Offset: 0x0005D2AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv6Bytes(Span<byte> destination)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				*destination[num++] = (byte)((this._numbers[i] >> 8) & 255);
				*destination[num++] = (byte)(this._numbers[i] & 255);
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0005F104 File Offset: 0x0005D304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv4Bytes(Span<byte> destination)
		{
			uint privateAddress = this.PrivateAddress;
			*destination[0] = (byte)privateAddress;
			*destination[1] = (byte)(privateAddress >> 8);
			*destination[2] = (byte)(privateAddress >> 16);
			*destination[3] = (byte)(privateAddress >> 24);
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x0600166B RID: 5739 RVA: 0x0005F14C File Offset: 0x0005D34C
		public byte[] GetAddressBytes()
		{
			if (this.IsIPv6)
			{
				byte[] array = new byte[16];
				this.WriteIPv6Bytes(array);
				return array;
			}
			byte[] array2 = new byte[4];
			this.WriteIPv4Bytes(array2);
			return array2;
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0005F18B File Offset: 0x0005D38B
		public AddressFamily AddressFamily
		{
			get
			{
				if (!this.IsIPv4)
				{
					return AddressFamily.InterNetworkV6;
				}
				return AddressFamily.InterNetwork;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">AddressFamily = InterNetwork. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0- or -<paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF  </exception>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0005F199 File Offset: 0x0005D399
		public long ScopeId
		{
			get
			{
				if (this.IsIPv4)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return (long)((ulong)this.PrivateScopeId);
			}
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600166E RID: 5742 RVA: 0x0005F1B5 File Offset: 0x0005D3B5
		public override string ToString()
		{
			if (this._toString == null)
			{
				this._toString = (this.IsIPv4 ? IPAddressParser.IPv4AddressToString(this.PrivateAddress) : IPAddressParser.IPv6AddressToString(this._numbers, this.PrivateScopeId));
			}
			return this._toString;
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <returns>true if <paramref name="address" /> is the loopback address; otherwise, false.</returns>
		/// <param name="address">An IP address. </param>
		// Token: 0x0600166F RID: 5743 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
		public static bool IsLoopback(IPAddress address)
		{
			if (address == null)
			{
				IPAddress.ThrowAddressNullException();
			}
			if (address.IsIPv6)
			{
				return address.Equals(IPAddress.IPv6Loopback);
			}
			return ((ulong)address.PrivateAddress & 255UL) == ((ulong)IPAddress.Loopback.PrivateAddress & 255UL);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005F240 File Offset: 0x0005D440
		internal bool Equals(object comparandObj, bool compareScopeId)
		{
			IPAddress ipaddress = comparandObj as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.IsIPv6)
			{
				for (int i = 0; i < 8; i++)
				{
					if (ipaddress._numbers[i] != this._numbers[i])
					{
						return false;
					}
				}
				return ipaddress.PrivateScopeId == this.PrivateScopeId || !compareScopeId;
			}
			return ipaddress.PrivateAddress == this.PrivateAddress;
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <returns>true if the two addresses are equal; otherwise, false.</returns>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance. </param>
		// Token: 0x06001671 RID: 5745 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
		public override bool Equals(object comparand)
		{
			return this.Equals(comparand, true);
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001672 RID: 5746 RVA: 0x0005F2C0 File Offset: 0x0005D4C0
		public unsafe override int GetHashCode()
		{
			if (this._hashCode != 0)
			{
				return this._hashCode;
			}
			int num;
			if (this.IsIPv6)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
				MemoryMarshal.AsBytes<ushort>(new ReadOnlySpan<ushort>(this._numbers)).CopyTo(span);
				BitConverter.TryWriteBytes(span.Slice(16), this._addressOrScopeId);
				num = Marvin.ComputeHash32(span, Marvin.DefaultSeed);
			}
			else
			{
				num = Marvin.ComputeHash32(MemoryMarshal.AsBytes<uint>(MemoryMarshal.CreateReadOnlySpan<uint>(ref this._addressOrScopeId, 1)), Marvin.DefaultSeed);
			}
			this._hashCode = num;
			return this._hashCode;
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv6 address.</returns>
		// Token: 0x06001673 RID: 5747 RVA: 0x0005F35C File Offset: 0x0005D55C
		public IPAddress MapToIPv6()
		{
			if (this.IsIPv6)
			{
				return this;
			}
			uint privateAddress = this.PrivateAddress;
			return new IPAddress(new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				ushort.MaxValue,
				(ushort)(((privateAddress & 65280U) >> 8) | ((privateAddress & 255U) << 8)),
				(ushort)(((privateAddress & 4278190080U) >> 24) | ((privateAddress & 16711680U) >> 8))
			}, 0U);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005F3BD File Offset: 0x0005D5BD
		private static byte[] ThrowAddressNullException()
		{
			throw new ArgumentNullException("address");
		}

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x04000D9C RID: 3484
		public static readonly IPAddress Any = new IPAddress.ReadOnlyIPAddress(0L);

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x04000D9D RID: 3485
		public static readonly IPAddress Loopback = new IPAddress.ReadOnlyIPAddress(16777343L);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x04000D9E RID: 3486
		public static readonly IPAddress Broadcast = new IPAddress.ReadOnlyIPAddress((long)((ulong)(-1)));

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x04000D9F RID: 3487
		public static readonly IPAddress None = IPAddress.Broadcast;

		// Token: 0x04000DA0 RID: 3488
		internal const long LoopbackMask = 255L;

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x04000DA1 RID: 3489
		public static readonly IPAddress IPv6Any = new IPAddress(new byte[16], 0L);

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x04000DA2 RID: 3490
		public static readonly IPAddress IPv6Loopback = new IPAddress(new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 1
		}, 0L);

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x04000DA3 RID: 3491
		public static readonly IPAddress IPv6None = new IPAddress(new byte[16], 0L);

		// Token: 0x04000DA4 RID: 3492
		private uint _addressOrScopeId;

		// Token: 0x04000DA5 RID: 3493
		private readonly ushort[] _numbers;

		// Token: 0x04000DA6 RID: 3494
		private string _toString;

		// Token: 0x04000DA7 RID: 3495
		private int _hashCode;

		// Token: 0x04000DA8 RID: 3496
		internal const int NumberOfLabels = 8;

		// Token: 0x02000385 RID: 901
		private sealed class ReadOnlyIPAddress : IPAddress
		{
			// Token: 0x06001676 RID: 5750 RVA: 0x0005F449 File Offset: 0x0005D649
			public ReadOnlyIPAddress(long newAddress)
				: base(newAddress)
			{
			}
		}
	}
}
