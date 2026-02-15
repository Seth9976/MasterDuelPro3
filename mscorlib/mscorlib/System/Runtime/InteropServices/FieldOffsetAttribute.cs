using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the physical position of fields within the unmanaged representation of a class or structure.</summary>
	// Token: 0x02000538 RID: 1336
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> class with the offset in the structure to the beginning of the field.</summary>
		/// <param name="offset">The offset in bytes from the beginning of the structure to the beginning of the field. </param>
		// Token: 0x06002923 RID: 10531 RVA: 0x000A80E7 File Offset: 0x000A62E7
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		// Token: 0x04001576 RID: 5494
		internal int _val;
	}
}
