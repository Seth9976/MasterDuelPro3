using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Friend
{
	// Token: 0x02000C0C RID: 3084
	public class FollowerContextCollection : PlayerContextCollection
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06005784 RID: 22404 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005785 RID: 22405 RVA: 0x0000216D File Offset: 0x0000036D
		public long headPcode
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06005786 RID: 22406 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005787 RID: 22407 RVA: 0x0000216D File Offset: 0x0000036D
		public long headDate
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005789 RID: 22409 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isHeadTerminal
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x0600578B RID: 22411 RVA: 0x0000216D File Offset: 0x0000036D
		public long tailPcode
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x0600578D RID: 22413 RVA: 0x0000216D File Offset: 0x0000036D
		public long tailDate
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600578E RID: 22414 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600578F RID: 22415 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTailTerminal
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06005790 RID: 22416 RVA: 0x0000216A File Offset: 0x0000036A
		private FollowerPlayerContext System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowerPlayerContext_003E_002EItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06005791 RID: 22417 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<long> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowerPlayerContext_003E_002EKeys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerable<FollowerPlayerContext> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowerPlayerContext_003E_002EValues
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x000029CC File Offset: 0x00000BCC
		private int System_002ECollections_002EGeneric_002EIReadOnlyCollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowerPlayerContext_003E_003E_002ECount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(ValueTuple<List<object>, bool> followerDatas, FollowerContextCollection.Dir dir, IReadOnlyDictionary<string, object> followPlayers, Action onReleasedCallback = null)
		{
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(IReadOnlyList<object> friendDatas, bool isTerminal, FollowerContextCollection.Dir dir, IReadOnlyDictionary<string, object> followPlayers, Action onReleasedCallback = null)
		{
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportFollows(IReadOnlyDictionary<string, object> followPlayers)
		{
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainsKey(long key)
		{
			return false;
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool TryGetValue(long key, out FollowerPlayerContext value)
		{
			value = null;
			return false;
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator<KeyValuePair<long, FollowerPlayerContext>> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CSystem_002EInt64_002CYgomGame_002EFriend_002EFollowerPlayerContext_003E_003E_002EGetEnumerator()
		{
			return null;
		}

		// Token: 0x04009447 RID: 37959
		public int cacheReleaseLine;

		// Token: 0x04009448 RID: 37960
		private Dictionary<long, FollowerPlayerContext> m_FollowerContextMap;

		// Token: 0x04009449 RID: 37961
		private List<FollowerPlayerContext> m_ImportTmpList;

		// Token: 0x02000C0D RID: 3085
		public enum Dir
		{
			// Token: 0x0400944B RID: 37963
			Next,
			// Token: 0x0400944C RID: 37964
			Back
		}
	}
}
