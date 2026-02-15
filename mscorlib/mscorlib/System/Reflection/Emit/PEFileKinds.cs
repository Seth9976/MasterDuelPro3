using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies the type of the portable executable (PE) file.</summary>
	// Token: 0x0200067B RID: 1659
	[ComVisible(true)]
	[Serializable]
	public enum PEFileKinds
	{
		/// <summary>The portable executable (PE) file is a DLL.</summary>
		// Token: 0x04001AD9 RID: 6873
		Dll = 1,
		/// <summary>The application is a console (not a Windows-based) application.</summary>
		// Token: 0x04001ADA RID: 6874
		ConsoleApplication,
		/// <summary>The application is a Windows-based application.</summary>
		// Token: 0x04001ADB RID: 6875
		WindowApplication
	}
}
