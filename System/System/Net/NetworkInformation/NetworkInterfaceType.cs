using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies types of network interfaces.</summary>
	// Token: 0x0200045F RID: 1119
	public enum NetworkInterfaceType
	{
		/// <summary>The interface type is not known.</summary>
		// Token: 0x040012E4 RID: 4836
		Unknown = 1,
		/// <summary>The network interface uses an Ethernet connection. Ethernet is defined in IEEE standard 802.3.</summary>
		// Token: 0x040012E5 RID: 4837
		Ethernet = 6,
		/// <summary>The network interface uses a Token-Ring connection. Token-Ring is defined in IEEE standard 802.5.</summary>
		// Token: 0x040012E6 RID: 4838
		TokenRing = 9,
		/// <summary>The network interface uses a Fiber Distributed Data Interface (FDDI) connection. FDDI is a set of standards for data transmission on fiber optic lines in a local area network.</summary>
		// Token: 0x040012E7 RID: 4839
		Fddi = 15,
		/// <summary>The network interface uses a basic rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x040012E8 RID: 4840
		BasicIsdn = 20,
		/// <summary>The network interface uses a primary rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x040012E9 RID: 4841
		PrimaryIsdn,
		/// <summary>The network interface uses a Point-To-Point protocol (PPP) connection. PPP is a protocol for data transmission using a serial device.</summary>
		// Token: 0x040012EA RID: 4842
		Ppp = 23,
		/// <summary>The network interface is a loopback adapter. Such interfaces are often used for testing; no traffic is sent over the wire.</summary>
		// Token: 0x040012EB RID: 4843
		Loopback,
		/// <summary>The network interface uses an Ethernet 3 megabit/second connection. This version of Ethernet is defined in IETF RFC 895.</summary>
		// Token: 0x040012EC RID: 4844
		Ethernet3Megabit = 26,
		/// <summary>The network interface uses a Serial Line Internet Protocol (SLIP) connection. SLIP is defined in IETF RFC 1055.</summary>
		// Token: 0x040012ED RID: 4845
		Slip = 28,
		/// <summary>The network interface uses asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040012EE RID: 4846
		Atm = 37,
		/// <summary>The network interface uses a modem.</summary>
		// Token: 0x040012EF RID: 4847
		GenericModem = 48,
		/// <summary>The network interface uses a Fast Ethernet connection over twisted pair and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-T.</summary>
		// Token: 0x040012F0 RID: 4848
		FastEthernetT = 62,
		/// <summary>The network interface uses a connection configured for ISDN and the X.25 protocol. X.25 allows computers on public networks to communicate using an intermediary computer.</summary>
		// Token: 0x040012F1 RID: 4849
		Isdn,
		/// <summary>The network interface uses a Fast Ethernet connection over optical fiber and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-FX.</summary>
		// Token: 0x040012F2 RID: 4850
		FastEthernetFx = 69,
		/// <summary>The network interface uses a wireless LAN connection (IEEE 802.11 standard).</summary>
		// Token: 0x040012F3 RID: 4851
		Wireless80211 = 71,
		/// <summary>The network interface uses an Asymmetric Digital Subscriber Line (ADSL).</summary>
		// Token: 0x040012F4 RID: 4852
		AsymmetricDsl = 94,
		/// <summary>The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).</summary>
		// Token: 0x040012F5 RID: 4853
		RateAdaptDsl,
		/// <summary>The network interface uses a Symmetric Digital Subscriber Line (SDSL).</summary>
		// Token: 0x040012F6 RID: 4854
		SymmetricDsl,
		/// <summary>The network interface uses a Very High Data Rate Digital Subscriber Line (VDSL).</summary>
		// Token: 0x040012F7 RID: 4855
		VeryHighSpeedDsl,
		/// <summary>The network interface uses the Internet Protocol (IP) in combination with asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040012F8 RID: 4856
		IPOverAtm = 114,
		/// <summary>The network interface uses a gigabit Ethernet connection and provides a data rate of 1,000 megabits per second (1 gigabit per second).</summary>
		// Token: 0x040012F9 RID: 4857
		GigabitEthernet = 117,
		/// <summary>The network interface uses a tunnel connection.</summary>
		// Token: 0x040012FA RID: 4858
		Tunnel = 131,
		/// <summary>The network interface uses a Multirate Digital Subscriber Line.</summary>
		// Token: 0x040012FB RID: 4859
		MultiRateSymmetricDsl = 143,
		/// <summary>The network interface uses a High Performance Serial Bus.</summary>
		// Token: 0x040012FC RID: 4860
		HighPerformanceSerialBus,
		/// <summary>The network interface uses a mobile broadband interface for WiMax devices.</summary>
		// Token: 0x040012FD RID: 4861
		Wman = 237,
		/// <summary>The network interface uses a mobile broadband interface for GSM-based devices.</summary>
		// Token: 0x040012FE RID: 4862
		Wwanpp = 243,
		/// <summary>The network interface uses a mobile broadband interface for CDMA-based devices.</summary>
		// Token: 0x040012FF RID: 4863
		Wwanpp2
	}
}
