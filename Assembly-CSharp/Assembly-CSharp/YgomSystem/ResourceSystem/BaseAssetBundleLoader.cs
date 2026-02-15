using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006D5 RID: 1749
	public abstract class BaseAssetBundleLoader : BaseLoader, ISpriteTagLoader
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600368B RID: 13963 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual BaseAssetBundleLoader.AssetCache assetCache
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0600368C RID: 13964
		protected abstract List<string> GetWithDependenciesList(Resource res);

		// Token: 0x0600368D RID: 13965
		protected abstract IEnumerator yLoadRequestCache(string loadPath, Action<AssetBundle> callback);

		// Token: 0x0600368E RID: 13966
		protected abstract void RemoveRequestCache(string loadPath);

		// Token: 0x0600368F RID: 13967
		protected abstract void ClearRequestCache();

		// Token: 0x06003690 RID: 13968 RVA: 0x000029CC File Offset: 0x00000BCC
		protected static bool GetUnloadAllOption(string path)
		{
			return false;
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddLoadedList(Resource res, List<string> addList)
		{
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddLoadedList(Resource res, string addPath)
		{
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(Resource res, uint crc)
		{
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator LoadAsync(Resource res, uint key)
		{
			return null;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCancel(Resource res)
		{
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadAction(Resource res)
		{
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadRequest(Resource res)
		{
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x0000216D File Offset: 0x0000036D
		public override void LateUpdate()
		{
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ClearCache()
		{
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddSpriteTagDic(string loadPath)
		{
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPathFromSpriteAtlasTag(string tag)
		{
			return null;
		}

		// Token: 0x04003136 RID: 12598
		private const int kAssetCacheFrame = 10;

		// Token: 0x04003137 RID: 12599
		private Dictionary<string, HashSet<string>> m_loadedDic;

		// Token: 0x04003138 RID: 12600
		private Dictionary<string, int> m_unloadReqDic;

		// Token: 0x04003139 RID: 12601
		private Dictionary<string, string> m_loadingDic;

		// Token: 0x0400313A RID: 12602
		private Dictionary<string, string> m_spriteTagDic;

		// Token: 0x0400313B RID: 12603
		private List<string> m_unloadKeys;

		// Token: 0x020006D6 RID: 1750
		protected internal class AssetBundleCache
		{
			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x0600369D RID: 13981 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsAlive
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x0600369E RID: 13982 RVA: 0x0000216A File Offset: 0x0000036A
			public global::UnityEngine.Object[] Target
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600369F RID: 13983 RVA: 0x00002739 File Offset: 0x00000939
			public AssetBundleCache(AssetBundle asset, bool unload = false)
			{
			}

			// Token: 0x060036A0 RID: 13984 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContains(string name)
			{
				return false;
			}

			// Token: 0x060036A1 RID: 13985 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsSpriteAtlas()
			{
				return false;
			}

			// Token: 0x060036A2 RID: 13986 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release()
			{
			}

			// Token: 0x060036A3 RID: 13987 RVA: 0x0000216D File Offset: 0x0000036D
			public void Load()
			{
			}

			// Token: 0x060036A4 RID: 13988 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator LoadAsync()
			{
				return null;
			}

			// Token: 0x060036A5 RID: 13989 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetObjects(global::UnityEngine.Object[] objects)
			{
			}

			// Token: 0x0400313C RID: 12604
			private List<WeakReference> weakRefs;

			// Token: 0x0400313D RID: 12605
			private AssetBundle bundle;

			// Token: 0x0400313E RID: 12606
			private bool unloadAll;
		}

		// Token: 0x020006D7 RID: 1751
		protected internal class AssetCache : AbstractReferenceCache<BaseAssetBundleLoader.AssetBundleCache>
		{
			// Token: 0x060036A6 RID: 13990 RVA: 0x0000216A File Offset: 0x0000036A
			protected override BaseAssetBundleLoader.AssetBundleCache LoadRequest(string key, params object[] param)
			{
				return null;
			}

			// Token: 0x060036A7 RID: 13991 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void RemoveCacheAction(BaseAssetBundleLoader.AssetBundleCache value)
			{
			}

			// Token: 0x060036A8 RID: 13992 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Clear()
			{
			}

			// Token: 0x060036A9 RID: 13993 RVA: 0x0000216A File Offset: 0x0000036A
			public List<string> ExistsKeys()
			{
				return null;
			}
		}
	}
}
