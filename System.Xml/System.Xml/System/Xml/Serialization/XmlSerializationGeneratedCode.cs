using System;
using System.Reflection;
using System.Threading;

namespace System.Xml.Serialization
{
	/// <summary>An abstract class that is the base class for <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> and <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> and that contains methods common to both of these types.</summary>
	// Token: 0x020001C5 RID: 453
	public abstract class XmlSerializationGeneratedCode
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x000709EC File Offset: 0x0006EBEC
		internal void Init(TempAssembly tempAssembly)
		{
			this.tempAssembly = tempAssembly;
			if (tempAssembly != null && tempAssembly.NeedAssembyResolve)
			{
				this.threadCode = Thread.CurrentThread.GetHashCode();
				this.assemblyResolver = new ResolveEventHandler(this.OnAssemblyResolve);
				AppDomain.CurrentDomain.AssemblyResolve += this.assemblyResolver;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00070A3D File Offset: 0x0006EC3D
		internal void Dispose()
		{
			if (this.assemblyResolver != null)
			{
				AppDomain.CurrentDomain.AssemblyResolve -= this.assemblyResolver;
			}
			this.assemblyResolver = null;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00070A5E File Offset: 0x0006EC5E
		internal Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (this.tempAssembly != null && Thread.CurrentThread.GetHashCode() == this.threadCode)
			{
				return this.tempAssembly.GetReferencedAssembly(args.Name);
			}
			return null;
		}

		// Token: 0x040009C3 RID: 2499
		private TempAssembly tempAssembly;

		// Token: 0x040009C4 RID: 2500
		private int threadCode;

		// Token: 0x040009C5 RID: 2501
		private ResolveEventHandler assemblyResolver;
	}
}
