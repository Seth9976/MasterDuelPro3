using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client end as a type that can be activated on the server.</summary>
	// Token: 0x0200040F RID: 1039
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given type name, assembly name, and application URL.</summary>
		/// <param name="typeName">The type name of the client activated type. </param>
		/// <param name="assemblyName">The assembly name of the client activated type. </param>
		/// <param name="appUrl">The URL of the application to activate the type in. </param>
		// Token: 0x060022BE RID: 8894 RVA: 0x0008F488 File Offset: 0x0008D688
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0008F4E9 File Offset: 0x0008D6E9
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated type.</summary>
		/// <returns>The context attributes for the client activated type.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x000082D2 File Offset: 0x000064D2
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the client-activated type.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0008F4F1 File Offset: 0x0008D6F1
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060022C2 RID: 8898 RVA: 0x0008F4F9 File Offset: 0x0008D6F9
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x040010DD RID: 4317
		private string applicationUrl;

		// Token: 0x040010DE RID: 4318
		private Type obj_type;
	}
}
