using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents the class that describes how to marshal a field from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x02000685 RID: 1669
	[ComVisible(true)]
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x0600343E RID: 13374 RVA: 0x000176B9 File Offset: 0x000158B9
		internal UnmanagedMarshal()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B24 RID: 6948
		private int count;

		// Token: 0x04001B25 RID: 6949
		private UnmanagedType t;

		// Token: 0x04001B26 RID: 6950
		private UnmanagedType tbase;

		// Token: 0x04001B27 RID: 6951
		private string guid;

		// Token: 0x04001B28 RID: 6952
		private string mcookie;

		// Token: 0x04001B29 RID: 6953
		private string marshaltype;

		// Token: 0x04001B2A RID: 6954
		internal Type marshaltyperef;

		// Token: 0x04001B2B RID: 6955
		private int param_num;

		// Token: 0x04001B2C RID: 6956
		private bool has_size;
	}
}
