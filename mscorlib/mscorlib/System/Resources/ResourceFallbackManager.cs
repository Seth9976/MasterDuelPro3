using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
	// Token: 0x020005C5 RID: 1477
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06002BBD RID: 11197 RVA: 0x000AC829 File Offset: 0x000AAA29
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AC856 File Offset: 0x000AAA56
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AC85E File Offset: 0x000AAA5E
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_00CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_00CE;
		}

		// Token: 0x04001623 RID: 5667
		private CultureInfo m_startingCulture;

		// Token: 0x04001624 RID: 5668
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x04001625 RID: 5669
		private bool m_useParents;
	}
}
