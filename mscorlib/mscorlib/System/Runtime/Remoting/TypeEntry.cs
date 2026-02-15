using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Implements a base class that holds the configuration information used to activate an instance of a remote type.</summary>
	// Token: 0x0200042A RID: 1066
	[ComVisible(true)]
	public class TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.TypeEntry" /> class.</summary>
		// Token: 0x0600239B RID: 9115 RVA: 0x00003CE1 File Offset: 0x00001EE1
		protected TypeEntry()
		{
		}

		/// <summary>Gets the assembly name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The assembly name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x00092DFE File Offset: 0x00090FFE
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x00092E06 File Offset: 0x00091006
		public string AssemblyName
		{
			get
			{
				return this.assembly_name;
			}
			set
			{
				this.assembly_name = value;
			}
		}

		/// <summary>Gets the full type name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The full type name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x00092E0F File Offset: 0x0009100F
		// (set) Token: 0x0600239F RID: 9119 RVA: 0x00092E17 File Offset: 0x00091017
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x04001132 RID: 4402
		private string assembly_name;

		// Token: 0x04001133 RID: 4403
		private string type_name;
	}
}
