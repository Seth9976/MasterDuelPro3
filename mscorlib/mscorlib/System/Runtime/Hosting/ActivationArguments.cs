using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides data for manifest-based activation of an application. This class cannot be inherited. </summary>
	// Token: 0x0200040D RID: 1037
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context. </summary>
		/// <param name="activationData">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationData" /> is null.</exception>
		// Token: 0x060022BA RID: 8890 RVA: 0x0008F42B File Offset: 0x0008D62B
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		// Token: 0x040010DA RID: 4314
		private ActivationContext _context;

		// Token: 0x040010DB RID: 4315
		private ApplicationIdentity _identity;
	}
}
