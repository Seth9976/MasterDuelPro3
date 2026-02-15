using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a field should be treated as containing a fixed number of elements of the specified primitive type. This class cannot be inherited. </summary>
	// Token: 0x02000587 RID: 1415
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> class. </summary>
		/// <param name="elementType">The type of the elements contained in the buffer.</param>
		/// <param name="length">The number of elements in the buffer.</param>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000AAB51 File Offset: 0x000A8D51
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.ElementType = elementType;
			this.Length = length;
		}

		/// <summary>Gets the type of the elements contained in the fixed buffer. </summary>
		/// <returns>The type of the elements.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000AAB67 File Offset: 0x000A8D67
		public Type ElementType { get; }

		/// <summary>Gets the number of elements in the fixed buffer. </summary>
		/// <returns>The number of elements in the fixed buffer.</returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000AAB6F File Offset: 0x000A8D6F
		public int Length { get; }
	}
}
