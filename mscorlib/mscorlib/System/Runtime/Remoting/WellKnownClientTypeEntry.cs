using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client as a server-activated type (single call or singleton).</summary>
	// Token: 0x0200042C RID: 1068
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type, assembly name, and URL.</summary>
		/// <param name="typeName">The type name of the server-activated type. </param>
		/// <param name="assemblyName">The assembly name of the server-activated type. </param>
		/// <param name="objectUrl">The URL of the server-activated type. </param>
		// Token: 0x060023A4 RID: 9124 RVA: 0x00093060 File Offset: 0x00091260
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			this.obj_url = objectUrl;
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000930C1 File Offset: 0x000912C1
		public string ApplicationUrl
		{
			get
			{
				return this.app_url;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated client type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the server-activated client type.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000930C9 File Offset: 0x000912C9
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URL of the server-activated client object.</summary>
		/// <returns>The URL of the server-activated client object.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000930D1 File Offset: 0x000912D1
		public string ObjectUrl
		{
			get
			{
				return this.obj_url;
			}
		}

		/// <summary>Returns the full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</summary>
		/// <returns>The full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060023A8 RID: 9128 RVA: 0x000930D9 File Offset: 0x000912D9
		public override string ToString()
		{
			if (this.ApplicationUrl != null)
			{
				return base.TypeName + base.AssemblyName + this.ObjectUrl + this.ApplicationUrl;
			}
			return base.TypeName + base.AssemblyName + this.ObjectUrl;
		}

		// Token: 0x04001137 RID: 4407
		private Type obj_type;

		// Token: 0x04001138 RID: 4408
		private string obj_url;

		// Token: 0x04001139 RID: 4409
		private string app_url;
	}
}
