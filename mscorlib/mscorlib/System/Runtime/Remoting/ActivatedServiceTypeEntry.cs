using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as one that can be activated on request from a client.</summary>
	// Token: 0x02000410 RID: 1040
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given type name and assembly name.</summary>
		/// <param name="typeName">The type name of the client-activated service type. </param>
		/// <param name="assemblyName">The assembly name of the client-activated service type. </param>
		// Token: 0x060022C3 RID: 8899 RVA: 0x0008F514 File Offset: 0x0008D714
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the client-activated service type.</returns>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0008F56E File Offset: 0x0008D76E
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060022C5 RID: 8901 RVA: 0x0008F576 File Offset: 0x0008D776
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x040010DF RID: 4319
		private Type obj_type;
	}
}
