using System;

namespace System
{
	/// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000121 RID: 289
	public enum MidpointRounding
	{
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest even number.</summary>
		// Token: 0x0400044E RID: 1102
		ToEven,
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest number that is away from zero.</summary>
		// Token: 0x0400044F RID: 1103
		AwayFromZero
	}
}
