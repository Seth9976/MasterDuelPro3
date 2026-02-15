using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores sink provider data for sink providers.</summary>
	// Token: 0x02000460 RID: 1120
	[ComVisible(true)]
	public class SinkProviderData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> class.</summary>
		/// <param name="name">The name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with. </param>
		// Token: 0x06002492 RID: 9362 RVA: 0x000960CD File Offset: 0x000942CD
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		/// <summary>Gets a list of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x000960F2 File Offset: 0x000942F2
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		/// <summary>Gets a dictionary through which properties on the sink provider can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink provider can be accessed.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000960FA File Offset: 0x000942FA
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001199 RID: 4505
		private string sinkName;

		// Token: 0x0400119A RID: 4506
		private ArrayList children;

		// Token: 0x0400119B RID: 4507
		private Hashtable properties;
	}
}
