using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E1 RID: 1249
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x06002741 RID: 10049 RVA: 0x0009E1C9 File Offset: 0x0009C3C9
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0009E1D8 File Offset: 0x0009C3D8
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Unable to find assembly '{0}'.", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x04001326 RID: 4902
		internal string assemblyString;

		// Token: 0x04001327 RID: 4903
		private Assembly assembly;
	}
}
