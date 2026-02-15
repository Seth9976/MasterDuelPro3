using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Provides a wrapper class for pointers.</summary>
	// Token: 0x02000614 RID: 1556
	[CLSCompliant(false)]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x06002D64 RID: 11620 RVA: 0x000B253B File Offset: 0x000B073B
		private unsafe Pointer(void* ptr, Type ptrType)
		{
			this._ptr = ptr;
			this._ptrType = ptrType;
		}

		/// <summary>Boxes the supplied unmanaged memory pointer and the type associated with that pointer into a managed <see cref="T:System.Reflection.Pointer" /> wrapper object. The value and the type are saved so they can be accessed from the native code during an invocation.</summary>
		/// <returns>A pointer object.</returns>
		/// <param name="ptr">The supplied unmanaged memory pointer. </param>
		/// <param name="type">The type associated with the <paramref name="ptr" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a pointer. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x06002D65 RID: 11621 RVA: 0x000B2554 File Offset: 0x000B0754
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "ptr");
			}
			return new Pointer(ptr, type);
		}

		/// <summary>Returns the stored pointer.</summary>
		/// <returns>This method returns void.</returns>
		/// <param name="ptr">The stored pointer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is not a pointer. </exception>
		// Token: 0x06002D66 RID: 11622 RVA: 0x000B25AC File Offset: 0x000B07AC
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, fusion log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002D67 RID: 11623 RVA: 0x000145B3 File Offset: 0x000127B3
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0400176B RID: 5995
		private unsafe readonly void* _ptr;

		// Token: 0x0400176C RID: 5996
		private readonly Type _ptrType;
	}
}
