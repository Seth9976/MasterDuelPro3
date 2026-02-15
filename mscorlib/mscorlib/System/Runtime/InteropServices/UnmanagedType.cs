using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to marshal parameters or fields to unmanaged code.</summary>
	// Token: 0x02000530 RID: 1328
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		/// <summary>A 4-byte Boolean value (true != 0, false = 0). This is the Win32 BOOL type.</summary>
		// Token: 0x04001546 RID: 5446
		Bool = 2,
		/// <summary>A 1-byte signed integer. You can use this member to transform a Boolean value into a 1-byte, C-style bool (true = 1, false = 0).</summary>
		// Token: 0x04001547 RID: 5447
		I1,
		/// <summary>A 1-byte unsigned integer.</summary>
		// Token: 0x04001548 RID: 5448
		U1,
		/// <summary>A 2-byte signed integer.</summary>
		// Token: 0x04001549 RID: 5449
		I2,
		/// <summary>A 2-byte unsigned integer.</summary>
		// Token: 0x0400154A RID: 5450
		U2,
		/// <summary>A 4-byte signed integer.</summary>
		// Token: 0x0400154B RID: 5451
		I4,
		/// <summary>A 4-byte unsigned integer.</summary>
		// Token: 0x0400154C RID: 5452
		U4,
		/// <summary>An 8-byte signed integer.</summary>
		// Token: 0x0400154D RID: 5453
		I8,
		/// <summary>An 8-byte unsigned integer.</summary>
		// Token: 0x0400154E RID: 5454
		U8,
		/// <summary>A 4-byte floating-point number.</summary>
		// Token: 0x0400154F RID: 5455
		R4,
		/// <summary>An 8-byte floating-point number.</summary>
		// Token: 0x04001550 RID: 5456
		R8,
		/// <summary>A currency type. Used on a <see cref="T:System.Decimal" /> to marshal the decimal value as a COM currency type instead of as a Decimal.</summary>
		// Token: 0x04001551 RID: 5457
		Currency = 15,
		/// <summary>A Unicode character string that is a length-prefixed double byte. You can use this member, which is the default string in COM, on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04001552 RID: 5458
		BStr = 19,
		/// <summary>A single byte, null-terminated ANSI character string. You can use this member on the <see cref="T:System.String" /> and <see cref="T:System.Text.StringBuilder" /> data types.</summary>
		// Token: 0x04001553 RID: 5459
		LPStr,
		/// <summary>A 2-byte, null-terminated Unicode character string.</summary>
		// Token: 0x04001554 RID: 5460
		LPWStr,
		/// <summary>A platform-dependent character string: ANSI on Windows 98, and Unicode on Windows NT and Windows XP. This value is supported only for platform invoke and not for COM interop, because exporting a string of type LPTStr is not supported.</summary>
		// Token: 0x04001555 RID: 5461
		LPTStr,
		/// <summary>Used for in-line, fixed-length character arrays that appear within a structure. The character type used with <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValTStr" /> is determined by the <see cref="T:System.Runtime.InteropServices.CharSet" /> argument of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> attribute applied to the containing structure. Always use the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field to indicate the size of the array.</summary>
		// Token: 0x04001556 RID: 5462
		ByValTStr,
		/// <summary>A COM IUnknown pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04001557 RID: 5463
		IUnknown = 25,
		/// <summary>A COM IDispatch pointer (Object in Microsoft Visual Basic 6.0).</summary>
		// Token: 0x04001558 RID: 5464
		IDispatch,
		/// <summary>A VARIANT, which is used to marshal managed formatted classes and value types.</summary>
		// Token: 0x04001559 RID: 5465
		Struct,
		/// <summary>A COM interface pointer. The <see cref="T:System.Guid" /> of the interface is obtained from the class metadata. Use this member to specify the exact interface type or the default interface type if you apply it to a class. This member produces the same behavior as <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> when you apply it to the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x0400155A RID: 5466
		Interface,
		/// <summary>A SafeArray, which is a self-describing array that carries the type, rank, and bounds of the associated array data. You can use this member with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SafeArraySubType" /> field to override the default element type.</summary>
		// Token: 0x0400155B RID: 5467
		SafeArray,
		/// <summary>When the <see cref="P:System.Runtime.InteropServices.MarshalAsAttribute.Value" /> property is set to ByValArray, the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field must be set to indicate the number of elements in the array. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.ArraySubType" /> field can optionally contain the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> of the array elements when it is necessary to differentiate among string types. You can use this <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> only on an array that whose elements appear as fields in a structure.</summary>
		// Token: 0x0400155C RID: 5468
		ByValArray,
		/// <summary>A platform-dependent, signed integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x0400155D RID: 5469
		SysInt,
		/// <summary>A platform-dependent, unsigned integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x0400155E RID: 5470
		SysUInt,
		/// <summary>A value that enables Visual Basic to change a string in unmanaged code and have the results reflected in managed code. This value is only supported for platform invoke.</summary>
		// Token: 0x0400155F RID: 5471
		VBByRefStr = 34,
		/// <summary>An ANSI character string that is a length-prefixed single byte. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04001560 RID: 5472
		AnsiBStr,
		/// <summary>A length-prefixed, platform-dependent char string: ANSI on Windows 98, Unicode on Windows NT. You rarely use this BSTR-like member.</summary>
		// Token: 0x04001561 RID: 5473
		TBStr,
		/// <summary>A 2-byte, OLE-defined VARIANT_BOOL type (true = -1, false = 0).</summary>
		// Token: 0x04001562 RID: 5474
		VariantBool,
		/// <summary>An integer that can be used as a C-style function pointer. You can use this member on a <see cref="T:System.Delegate" /> data type or on a type that inherits from a <see cref="T:System.Delegate" />.</summary>
		// Token: 0x04001563 RID: 5475
		FunctionPtr,
		/// <summary>A dynamic type that determines the type of an object at run time and marshals the object as that type. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04001564 RID: 5476
		AsAny = 40,
		/// <summary>A pointer to the first element of a C-style array. When marshaling from managed to unmanaged code, the length of the array is determined by the length of the managed array. When marshaling from unmanaged to managed code, the length of the array is determined from the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> and <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeParamIndex" /> fields, optionally followed by the unmanaged type of the elements within the array when it is necessary to differentiate among string types.</summary>
		// Token: 0x04001565 RID: 5477
		LPArray = 42,
		/// <summary>A pointer to a C-style structure that you use to marshal managed formatted classes. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04001566 RID: 5478
		LPStruct,
		/// <summary>Specifies the custom marshaler class when used with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> or <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalTypeRef" /> field. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalCookie" /> field can be used to pass additional information to the custom marshaler. You can use this member on any reference type.</summary>
		// Token: 0x04001567 RID: 5479
		CustomMarshaler,
		/// <summary>A native type that is associated with an <see cref="F:System.Runtime.InteropServices.UnmanagedType.I4" /> or an <see cref="F:System.Runtime.InteropServices.UnmanagedType.U4" /> and that causes the parameter to be exported as an HRESULT in the exported type library.</summary>
		// Token: 0x04001568 RID: 5480
		Error,
		/// <summary>A Windows Runtime interface pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04001569 RID: 5481
		[ComVisible(false)]
		IInspectable,
		/// <summary>A Windows Runtime string. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x0400156A RID: 5482
		[ComVisible(false)]
		HString,
		// Token: 0x0400156B RID: 5483
		[ComVisible(false)]
		LPUTF8Str
	}
}
