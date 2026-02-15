using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the data between managed and unmanaged code.</summary>
	// Token: 0x0200054D RID: 1357
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MarshalAsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> enumeration member.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as. </param>
		// Token: 0x06002A69 RID: 10857 RVA: 0x000AA43A File Offset: 0x000A863A
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000AA449 File Offset: 0x000A8649
		public UnmanagedType Value
		{
			get
			{
				return this.utype;
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000AA451 File Offset: 0x000A8651
		internal MarshalAsAttribute Copy()
		{
			return (MarshalAsAttribute)base.MemberwiseClone();
		}

		/// <summary>Provides additional information to a custom marshaler.</summary>
		// Token: 0x0400159B RID: 5531
		public string MarshalCookie;

		/// <summary>Specifies the fully qualified name of a custom marshaler.</summary>
		// Token: 0x0400159C RID: 5532
		[ComVisible(true)]
		public string MarshalType;

		/// <summary>Implements <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> as a type.</summary>
		// Token: 0x0400159D RID: 5533
		[ComVisible(true)]
		public Type MarshalTypeRef;

		/// <summary>Indicates the user-defined element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x0400159E RID: 5534
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x0400159F RID: 5535
		private UnmanagedType utype;

		/// <summary>Specifies the element type of the unmanaged <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> or <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.</summary>
		// Token: 0x040015A0 RID: 5536
		public UnmanagedType ArraySubType;

		/// <summary>Indicates the element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x040015A1 RID: 5537
		public VarEnum SafeArraySubType;

		/// <summary>Indicates the number of elements in the fixed-length array or the number of characters (not bytes) in a string to import.</summary>
		// Token: 0x040015A2 RID: 5538
		public int SizeConst;

		/// <summary>Specifies the parameter index of the unmanaged iid_is attribute used by COM.</summary>
		// Token: 0x040015A3 RID: 5539
		public int IidParameterIndex;

		/// <summary>Indicates the zero-based parameter that contains the count of array elements, similar to size_is in COM.</summary>
		// Token: 0x040015A4 RID: 5540
		public short SizeParamIndex;
	}
}
