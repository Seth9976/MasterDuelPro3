using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as a server-activated type object (single call or singleton).</summary>
	// Token: 0x0200042E RID: 1070
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given type name, assembly name, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="typeName">The full type name of the server-activated service type. </param>
		/// <param name="assemblyName">The assembly name of the server-activated service type. </param>
		/// <param name="objectUri">The URI of the server-activated object. </param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated. </param>
		// Token: 0x060023A9 RID: 9129 RVA: 0x00093118 File Offset: 0x00091318
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			this.obj_uri = objectUri;
			this.obj_mode = mode;
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x00093181 File Offset: 0x00091381
		public WellKnownObjectMode Mode
		{
			get
			{
				return this.obj_mode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the server-activated service type.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x00093189 File Offset: 0x00091389
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URI of the well-known service type.</summary>
		/// <returns>The URI of the server-activated service type.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00093191 File Offset: 0x00091391
		public string ObjectUri
		{
			get
			{
				return this.obj_uri;
			}
		}

		/// <summary>Returns the type name, assembly name, object URI and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, object URI, and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060023AD RID: 9133 RVA: 0x00093199 File Offset: 0x00091399
		public override string ToString()
		{
			return string.Concat(new string[] { base.TypeName, ", ", base.AssemblyName, " ", this.ObjectUri });
		}

		// Token: 0x0400113D RID: 4413
		private Type obj_type;

		// Token: 0x0400113E RID: 4414
		private string obj_uri;

		// Token: 0x0400113F RID: 4415
		private WellKnownObjectMode obj_mode;
	}
}
