using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the array elements when an array is marshaled from managed to unmanaged code as a <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />. </summary>
	// Token: 0x0200052F RID: 1327
	[ComVisible(true)]
	[Serializable]
	public enum VarEnum
	{
		/// <summary>Indicates that a value was not specified.</summary>
		// Token: 0x04001519 RID: 5401
		VT_EMPTY,
		/// <summary>Indicates a null value, similar to a null value in SQL.</summary>
		// Token: 0x0400151A RID: 5402
		VT_NULL,
		/// <summary>Indicates a short integer.</summary>
		// Token: 0x0400151B RID: 5403
		VT_I2,
		/// <summary>Indicates a long integer.</summary>
		// Token: 0x0400151C RID: 5404
		VT_I4,
		/// <summary>Indicates a float value.</summary>
		// Token: 0x0400151D RID: 5405
		VT_R4,
		/// <summary>Indicates a double value.</summary>
		// Token: 0x0400151E RID: 5406
		VT_R8,
		/// <summary>Indicates a currency value.</summary>
		// Token: 0x0400151F RID: 5407
		VT_CY,
		/// <summary>Indicates a DATE value.</summary>
		// Token: 0x04001520 RID: 5408
		VT_DATE,
		/// <summary>Indicates a BSTR string.</summary>
		// Token: 0x04001521 RID: 5409
		VT_BSTR,
		/// <summary>Indicates an IDispatch pointer.</summary>
		// Token: 0x04001522 RID: 5410
		VT_DISPATCH,
		/// <summary>Indicates an SCODE.</summary>
		// Token: 0x04001523 RID: 5411
		VT_ERROR,
		/// <summary>Indicates a Boolean value.</summary>
		// Token: 0x04001524 RID: 5412
		VT_BOOL,
		/// <summary>Indicates a VARIANT far pointer.</summary>
		// Token: 0x04001525 RID: 5413
		VT_VARIANT,
		/// <summary>Indicates an IUnknown pointer.</summary>
		// Token: 0x04001526 RID: 5414
		VT_UNKNOWN,
		/// <summary>Indicates a decimal value.</summary>
		// Token: 0x04001527 RID: 5415
		VT_DECIMAL,
		/// <summary>Indicates a char value.</summary>
		// Token: 0x04001528 RID: 5416
		VT_I1 = 16,
		/// <summary>Indicates a byte.</summary>
		// Token: 0x04001529 RID: 5417
		VT_UI1,
		/// <summary>Indicates an unsignedshort.</summary>
		// Token: 0x0400152A RID: 5418
		VT_UI2,
		/// <summary>Indicates an unsignedlong.</summary>
		// Token: 0x0400152B RID: 5419
		VT_UI4,
		/// <summary>Indicates a 64-bit integer.</summary>
		// Token: 0x0400152C RID: 5420
		VT_I8,
		/// <summary>Indicates an 64-bit unsigned integer.</summary>
		// Token: 0x0400152D RID: 5421
		VT_UI8,
		/// <summary>Indicates an integer value.</summary>
		// Token: 0x0400152E RID: 5422
		VT_INT,
		/// <summary>Indicates an unsigned integer value.</summary>
		// Token: 0x0400152F RID: 5423
		VT_UINT,
		/// <summary>Indicates a C style void.</summary>
		// Token: 0x04001530 RID: 5424
		VT_VOID,
		/// <summary>Indicates an HRESULT.</summary>
		// Token: 0x04001531 RID: 5425
		VT_HRESULT,
		/// <summary>Indicates a pointer type.</summary>
		// Token: 0x04001532 RID: 5426
		VT_PTR,
		/// <summary>Indicates a SAFEARRAY. Not valid in a VARIANT.</summary>
		// Token: 0x04001533 RID: 5427
		VT_SAFEARRAY,
		/// <summary>Indicates a C style array.</summary>
		// Token: 0x04001534 RID: 5428
		VT_CARRAY,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04001535 RID: 5429
		VT_USERDEFINED,
		/// <summary>Indicates a null-terminated string.</summary>
		// Token: 0x04001536 RID: 5430
		VT_LPSTR,
		/// <summary>Indicates a wide string terminated by null.</summary>
		// Token: 0x04001537 RID: 5431
		VT_LPWSTR,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04001538 RID: 5432
		VT_RECORD = 36,
		/// <summary>Indicates a FILETIME value.</summary>
		// Token: 0x04001539 RID: 5433
		VT_FILETIME = 64,
		/// <summary>Indicates length prefixed bytes.</summary>
		// Token: 0x0400153A RID: 5434
		VT_BLOB,
		/// <summary>Indicates that the name of a stream follows.</summary>
		// Token: 0x0400153B RID: 5435
		VT_STREAM,
		/// <summary>Indicates that the name of a storage follows.</summary>
		// Token: 0x0400153C RID: 5436
		VT_STORAGE,
		/// <summary>Indicates that a stream contains an object.</summary>
		// Token: 0x0400153D RID: 5437
		VT_STREAMED_OBJECT,
		/// <summary>Indicates that a storage contains an object.</summary>
		// Token: 0x0400153E RID: 5438
		VT_STORED_OBJECT,
		/// <summary>Indicates that a blob contains an object.</summary>
		// Token: 0x0400153F RID: 5439
		VT_BLOB_OBJECT,
		/// <summary>Indicates the clipboard format.</summary>
		// Token: 0x04001540 RID: 5440
		VT_CF,
		/// <summary>Indicates a class ID.</summary>
		// Token: 0x04001541 RID: 5441
		VT_CLSID,
		/// <summary>Indicates a simple, counted array.</summary>
		// Token: 0x04001542 RID: 5442
		VT_VECTOR = 4096,
		/// <summary>Indicates a SAFEARRAY pointer.</summary>
		// Token: 0x04001543 RID: 5443
		VT_ARRAY = 8192,
		/// <summary>Indicates that a value is a reference.</summary>
		// Token: 0x04001544 RID: 5444
		VT_BYREF = 16384
	}
}
