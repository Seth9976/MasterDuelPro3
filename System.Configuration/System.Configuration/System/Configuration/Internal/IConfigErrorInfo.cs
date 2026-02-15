using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support creating error configuration records.</summary>
	// Token: 0x02000048 RID: 72
	public interface IConfigErrorInfo
	{
		/// <summary>Gets a string specifying the file name related to the configuration details.</summary>
		/// <returns>A string specifying a filename.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DD RID: 477
		string Filename { get; }

		/// <summary>Gets an integer specifying the line number related to the configuration details.</summary>
		/// <returns>An integer specifying a line number.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001DE RID: 478
		int LineNumber { get; }
	}
}
