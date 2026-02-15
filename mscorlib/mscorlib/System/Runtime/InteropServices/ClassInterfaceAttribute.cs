using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the type of class interface to be generated for a class exposed to COM, if an interface is generated at all.</summary>
	// Token: 0x0200052A RID: 1322
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration member.</summary>
		/// <param name="classInterfaceType">One of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> values that describes the type of interface that is generated for a class. </param>
		// Token: 0x06002913 RID: 10515 RVA: 0x000A7EF3 File Offset: 0x000A60F3
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		// Token: 0x04001505 RID: 5381
		internal ClassInterfaceType _val;
	}
}
