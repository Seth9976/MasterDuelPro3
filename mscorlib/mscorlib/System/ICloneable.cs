using System;

namespace System
{
	/// <summary>Supports cloning, which creates a new instance of a class with the same value as an existing instance.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FF RID: 255
	public interface ICloneable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000883 RID: 2179
		object Clone();
	}
}
