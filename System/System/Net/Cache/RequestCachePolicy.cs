using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.WebRequest" /> objects.</summary>
	// Token: 0x020004A2 RID: 1186
	public class RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. using the specified cache policy.</summary>
		/// <param name="level">A <see cref="T:System.Net.Cache.RequestCacheLevel" /> that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">level is not a valid <see cref="T:System.Net.Cache.RequestCacheLevel" />.value.</exception>
		// Token: 0x06001CD0 RID: 7376 RVA: 0x0007D3F6 File Offset: 0x0007B5F6
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.RequestCacheLevel" /> value specified when this instance was constructed.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</returns>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0007D418 File Offset: 0x0007B618
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the <see cref="P:System.Net.Cache.RequestCachePolicy.Level" /> for this instance.</returns>
		// Token: 0x06001CD2 RID: 7378 RVA: 0x0007D420 File Offset: 0x0007B620
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x040013E2 RID: 5090
		private RequestCacheLevel m_Level;
	}
}
