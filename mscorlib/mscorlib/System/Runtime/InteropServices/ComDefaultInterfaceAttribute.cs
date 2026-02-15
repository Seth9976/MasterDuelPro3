using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies a default interface to expose to COM. This class cannot be inherited.</summary>
	// Token: 0x02000528 RID: 1320
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> class with the specified <see cref="T:System.Type" /> object as the default interface exposed to COM.</summary>
		/// <param name="defaultInterface">A <see cref="T:System.Type" /> value indicating the default interface to expose to COM. </param>
		// Token: 0x06002912 RID: 10514 RVA: 0x000A7EE4 File Offset: 0x000A60E4
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		// Token: 0x04001500 RID: 5376
		internal Type _val;
	}
}
