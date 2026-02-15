using System;
using System.Collections.Generic;

namespace YgomGame.Friend
{
	// Token: 0x02000C09 RID: 3081
	public class FollowContextCollection : PlayerContextCollection
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<long> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowPlayerContext_003E_002EKeys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06005778 RID: 22392 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<FollowPlayerContext> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowPlayerContext_003E_002EValues
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x000029CC File Offset: 0x00000BCC
		private int System_002ECollections_002EGeneric_002EIReadOnlyCollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowPlayerContext_003E_003E_002ECount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600577A RID: 22394 RVA: 0x0000216A File Offset: 0x0000036A
		private FollowPlayerContext System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowPlayerContext_003E_002EItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x000029CC File Offset: 0x00000BCC
		public int SearchOnlineSum()
		{
			return 0;
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(IReadOnlyDictionary<string, object> followPlayers)
		{
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x0000216D File Offset: 0x0000036D
		public void Sort()
		{
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainsKey(long key)
		{
			return false;
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool TryGetValue(long key, out FollowPlayerContext value)
		{
			value = null;
			return false;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator<KeyValuePair<long, FollowPlayerContext>> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowPlayerContext_003E_003E_002EGetEnumerator()
		{
			return null;
		}

		// Token: 0x04009441 RID: 37953
		private Dictionary<long, FollowPlayerContext> m_PlayerContextMap;
	}
}
