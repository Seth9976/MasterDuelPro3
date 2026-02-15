using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines the basic operations of cryptographic transformations.</summary>
	// Token: 0x0200039A RID: 922
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001FA6 RID: 8102
		int InputBlockSize { get; }

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001FA7 RID: 8103
		int OutputBlockSize { get; }

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001FA8 RID: 8104
		bool CanTransformMultipleBlocks { get; }

		/// <summary>Transforms the specified region of the input byte array and copies the resulting transform to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the transform. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		// Token: 0x06001FA9 RID: 8105
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		/// <summary>Transforms the specified region of the specified byte array.</summary>
		/// <returns>The computed transform.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		// Token: 0x06001FAA RID: 8106
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}
