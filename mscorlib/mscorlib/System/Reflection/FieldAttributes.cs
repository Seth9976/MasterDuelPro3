using System;

namespace System.Reflection
{
	/// <summary>Specifies flags that describe the attributes of a field.</summary>
	// Token: 0x020005FD RID: 1533
	[Flags]
	public enum FieldAttributes
	{
		/// <summary>Specifies the access level of a given field.</summary>
		// Token: 0x040016F6 RID: 5878
		FieldAccessMask = 7,
		/// <summary>Specifies that the field cannot be referenced.</summary>
		// Token: 0x040016F7 RID: 5879
		PrivateScope = 0,
		/// <summary>Specifies that the field is accessible only by the parent type.</summary>
		// Token: 0x040016F8 RID: 5880
		Private = 1,
		/// <summary>Specifies that the field is accessible only by subtypes in this assembly.</summary>
		// Token: 0x040016F9 RID: 5881
		FamANDAssem = 2,
		/// <summary>Specifies that the field is accessible throughout the assembly.</summary>
		// Token: 0x040016FA RID: 5882
		Assembly = 3,
		/// <summary>Specifies that the field is accessible only by type and subtypes.</summary>
		// Token: 0x040016FB RID: 5883
		Family = 4,
		/// <summary>Specifies that the field is accessible by subtypes anywhere, as well as throughout this assembly.</summary>
		// Token: 0x040016FC RID: 5884
		FamORAssem = 5,
		/// <summary>Specifies that the field is accessible by any member for whom this scope is visible.</summary>
		// Token: 0x040016FD RID: 5885
		Public = 6,
		/// <summary>Specifies that the field represents the defined type, or else it is per-instance.</summary>
		// Token: 0x040016FE RID: 5886
		Static = 16,
		/// <summary>Specifies that the field is initialized only, and can be set only in the body of a constructor. </summary>
		// Token: 0x040016FF RID: 5887
		InitOnly = 32,
		/// <summary>Specifies that the field's value is a compile-time (static or early bound) constant. Any attempt to set it throws a <see cref="T:System.FieldAccessException" />.</summary>
		// Token: 0x04001700 RID: 5888
		Literal = 64,
		/// <summary>Specifies that the field does not have to be serialized when the type is remoted.</summary>
		// Token: 0x04001701 RID: 5889
		NotSerialized = 128,
		/// <summary>Specifies a special method, with the name describing how the method is special.</summary>
		// Token: 0x04001702 RID: 5890
		SpecialName = 512,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001703 RID: 5891
		PinvokeImpl = 8192,
		/// <summary>Specifies that the common language runtime (metadata internal APIs) should check the name encoding.</summary>
		// Token: 0x04001704 RID: 5892
		RTSpecialName = 1024,
		/// <summary>Specifies that the field has marshaling information.</summary>
		// Token: 0x04001705 RID: 5893
		HasFieldMarshal = 4096,
		/// <summary>Specifies that the field has a default value.</summary>
		// Token: 0x04001706 RID: 5894
		HasDefault = 32768,
		/// <summary>Specifies that the field has a relative virtual address (RVA). The RVA is the location of the method body in the current image, as an address relative to the start of the image file in which it is located.</summary>
		// Token: 0x04001707 RID: 5895
		HasFieldRVA = 256,
		/// <summary>Reserved.</summary>
		// Token: 0x04001708 RID: 5896
		ReservedMask = 38144
	}
}
