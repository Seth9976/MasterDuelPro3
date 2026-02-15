using System;
using System.Collections.Generic;

namespace YgomGame.Friend
{
	// Token: 0x02000C07 RID: 3079
	public class BlockContextCollection : PlayerContextCollection
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<long> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EBlockPlayerContext_003E_002EKeys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600576B RID: 22379 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<BlockPlayerContext> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EBlockPlayerContext_003E_002EValues
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x000029CC File Offset: 0x00000BCC
		private int System_002ECollections_002EGeneric_002EIReadOnlyCollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EBlockPlayerContext_003E_003E_002ECount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600576D RID: 22381 RVA: 0x0000216A File Offset: 0x0000036A
		private BlockPlayerContext System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EBlockPlayerContext_003E_002EItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(IReadOnlyDictionary<string, object> followPlayers)
		{
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x0000216D File Offset: 0x0000036D
		public void Sort()
		{
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainsKey(long key)
		{
			return false;
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool TryGetValue(long key, out BlockPlayerContext value)
		{
			value = null;
			return false;
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator<KeyValuePair<long, BlockPlayerContext>> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EBlockPlayerContext_003E_003E_002EGetEnumerator()
		{
			return null;
		}

		// Token: 0x0400943F RID: 37951
		private Dictionary<long, BlockPlayerContext> m_PlayerContextMap;
	}
}
