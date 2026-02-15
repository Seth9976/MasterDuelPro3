using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines the interfaces used by the internal design time API to create a <see cref="T:System.Configuration.Configuration" /> object.</summary>
	// Token: 0x0200004A RID: 74
	[ComVisible(false)]
	public interface IInternalConfigConfigurationFactory
	{
		/// <summary>Creates and initializes a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="typeConfigHost">The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.Configuration" /> object to be created.</param>
		/// <param name="hostInitConfigurationParams">A parameter array of <see cref="T:System.Object" /> that contains the parameters to be applied to the created <see cref="T:System.Configuration.Configuration" /> object.</param>
		// Token: 0x060001E0 RID: 480
		Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams);
	}
}
