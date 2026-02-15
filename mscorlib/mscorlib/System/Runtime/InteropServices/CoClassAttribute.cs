using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the class identifier of a coclass imported from a type library.</summary>
	// Token: 0x0200053A RID: 1338
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class CoClassAttribute : Attribute
	{
		/// <summary>Initializes new instance of the <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> with the class identifier of the original coclass.</summary>
		/// <param name="coClass">A <see cref="T:System.Type" /> that contains the class identifier of the original coclass. </param>
		// Token: 0x06002925 RID: 10533 RVA: 0x000A8105 File Offset: 0x000A6305
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		// Token: 0x04001578 RID: 5496
		internal Type _CoClass;
	}
}
