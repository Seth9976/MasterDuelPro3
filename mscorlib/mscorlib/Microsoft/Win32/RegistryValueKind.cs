using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies the data types to use when storing values in the registry, or identifies the data type of a value in the registry.</summary>
	// Token: 0x02000083 RID: 131
	public enum RegistryValueKind
	{
		/// <summary>A null-terminated string. This value is equivalent to the Win32 API registry data type REG_SZ.</summary>
		// Token: 0x04000261 RID: 609
		String = 1,
		/// <summary>A null-terminated string that contains unexpanded references to environment variables, such as %PATH%, that are expanded when the value is retrieved. This value is equivalent to the Win32 API registry data type REG_EXPAND_SZ.</summary>
		// Token: 0x04000262 RID: 610
		ExpandString,
		/// <summary>Binary data in any form. This value is equivalent to the Win32 API registry data type REG_BINARY. </summary>
		// Token: 0x04000263 RID: 611
		Binary,
		/// <summary>A 32-bit binary number. This value is equivalent to the Win32 API registry data type REG_DWORD.</summary>
		// Token: 0x04000264 RID: 612
		DWord,
		/// <summary>An array of null-terminated strings, terminated by two null characters. This value is equivalent to the Win32 API registry data type REG_MULTI_SZ.</summary>
		// Token: 0x04000265 RID: 613
		MultiString = 7,
		/// <summary>A 64-bit binary number. This value is equivalent to the Win32 API registry data type REG_QWORD.</summary>
		// Token: 0x04000266 RID: 614
		QWord = 11,
		/// <summary>An unsupported registry data type. For example, the Microsoft Win32 API registry data type REG_RESOURCE_LIST is unsupported. Use this value to specify that the <see cref="M:Microsoft.Win32.RegistryKey.SetValue(System.String,System.Object)" /> method should determine the appropriate registry data type when storing a name/value pair.</summary>
		// Token: 0x04000267 RID: 615
		Unknown = 0,
		/// <summary>No data type.</summary>
		// Token: 0x04000268 RID: 616
		None = -1
	}
}
