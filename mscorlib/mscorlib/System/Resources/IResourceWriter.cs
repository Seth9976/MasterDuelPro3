using System;

namespace System.Resources
{
	/// <summary>Provides the base functionality for writing resources to an output file or stream.</summary>
	// Token: 0x020005CB RID: 1483
	public interface IResourceWriter : IDisposable
	{
		/// <summary>Closes the underlying resource file or stream, ensuring all the data has been written to the file.</summary>
		// Token: 0x06002BD4 RID: 11220
		void Close();

		/// <summary>Writes all the resources added by the <see cref="M:System.Resources.IResourceWriter.AddResource(System.String,System.String)" /> method to the output file or stream.</summary>
		// Token: 0x06002BD5 RID: 11221
		void Generate();
	}
}
