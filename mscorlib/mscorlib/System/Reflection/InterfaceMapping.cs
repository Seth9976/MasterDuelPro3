using System;

namespace System.Reflection
{
	/// <summary>Retrieves the mapping of an interface into the actual methods on a class that implements that interface.</summary>
	// Token: 0x02000603 RID: 1539
	public struct InterfaceMapping
	{
		/// <summary>Represents the type that was used to create the interface mapping.</summary>
		// Token: 0x04001717 RID: 5911
		public Type TargetType;

		/// <summary>Shows the type that represents the interface.</summary>
		// Token: 0x04001718 RID: 5912
		public Type InterfaceType;

		/// <summary>Shows the methods that implement the interface.</summary>
		// Token: 0x04001719 RID: 5913
		public MethodInfo[] TargetMethods;

		/// <summary>Shows the methods that are defined on the interface.</summary>
		// Token: 0x0400171A RID: 5914
		public MethodInfo[] InterfaceMethods;
	}
}
