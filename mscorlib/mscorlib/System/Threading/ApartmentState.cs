using System;

namespace System.Threading
{
	/// <summary>Specifies the apartment state of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200020D RID: 525
	public enum ApartmentState
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a single-threaded apartment.</summary>
		// Token: 0x040009EB RID: 2539
		STA,
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a multithreaded apartment.</summary>
		// Token: 0x040009EC RID: 2540
		MTA,
		/// <summary>The <see cref="P:System.Threading.Thread.ApartmentState" /> property has not been set.</summary>
		// Token: 0x040009ED RID: 2541
		Unknown
	}
}
