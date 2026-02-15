using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x0200045D RID: 1117
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x040012CE RID: 4814
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x040012CF RID: 4815
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x040012D0 RID: 4816
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x040012D1 RID: 4817
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x040012D2 RID: 4818
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x040012D3 RID: 4819
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x040012D4 RID: 4820
		LowerLayerDown
	}
}
