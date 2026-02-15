using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents a missing <see cref="T:System.Object" />. This class cannot be inherited.</summary>
	// Token: 0x0200060E RID: 1550
	public sealed class Missing : ISerializable
	{
		// Token: 0x06002D2B RID: 11563 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private Missing()
		{
		}

		/// <summary>Sets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate the sole instance of the <see cref="T:System.Reflection.Missing" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06002D2C RID: 11564 RVA: 0x000145B3 File Offset: 0x000127B3
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.Reflection.Missing" /> class.</summary>
		// Token: 0x04001753 RID: 5971
		public static readonly Missing Value = new Missing();
	}
}
