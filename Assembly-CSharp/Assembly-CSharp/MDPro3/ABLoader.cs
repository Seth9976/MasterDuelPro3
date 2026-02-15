using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using Willow;
using Willow.InGameField;
using YgomGame;

namespace MDPro3
{
	// Token: 0x02001227 RID: 4647
	public class ABLoader
	{
		// Token: 0x06008998 RID: 35224 RVA: 0x0010B19C File Offset: 0x0010939C
		public static async UniTask<AssetBundle> CacheFromFileAsync(string path)
		{
			return await AssetBundle.LoadFromFileAsync(path);
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x0010B1E0 File Offset: 0x001093E0
		public static GameObject LoadFromFile(string path, bool cache, bool instantiate)
		{
			GameObject returnValue;
			if (ABLoader.cachedAB.TryGetValue(path, out returnValue))
			{
				if (instantiate)
				{
					return global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				return returnValue;
			}
			else
			{
				AssetBundle ab = AssetBundle.LoadFromFile(Program.root + path);
				foreach (global::UnityEngine.Object asset in ab.LoadAllAssets())
				{
					if (typeof(GameObject).IsInstanceOfType(asset))
					{
						if (cache && !ABLoader.cachedAB.TryAdd(path, asset as GameObject))
						{
							Debug.LogError("Failed to cache " + path);
						}
						returnValue = asset as GameObject;
						break;
					}
				}
				ab.Unload(false);
				if (instantiate && returnValue != null)
				{
					return global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				return returnValue;
			}
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x0010B294 File Offset: 0x00109494
		public static async UniTask<GameObject> LoadFromFileAsync(string path, bool cache, bool instantiate)
		{
			GameObject returnValue;
			GameObject gameObject;
			if (ABLoader.cachedAB.TryGetValue(path, out returnValue))
			{
				if (instantiate)
				{
					gameObject = global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				else
				{
					gameObject = returnValue;
				}
			}
			else
			{
				AssetBundle ab = await AssetBundle.LoadFromFileAsync(Program.root + path);
				foreach (global::UnityEngine.Object asset in ab.LoadAllAssets())
				{
					if (typeof(GameObject).IsInstanceOfType(asset))
					{
						if (cache && !ABLoader.cachedAB.TryAdd(path, asset as GameObject))
						{
							Debug.LogWarning("Failed to cache " + path);
						}
						returnValue = asset as GameObject;
					}
				}
				ab.Unload(false);
				if (instantiate && returnValue != null)
				{
					gameObject = global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				else
				{
					gameObject = returnValue;
				}
			}
			return gameObject;
		}

		// Token: 0x0600899B RID: 35227 RVA: 0x0010B2E8 File Offset: 0x001094E8
		public static GameObject LoadFromFolder<T>(string path, bool cache, bool instantiate) where T : Component
		{
			GameObject returnValue;
			if (ABLoader.cachedABFolder.TryGetValue(path, out returnValue))
			{
				if (instantiate)
				{
					return global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				return returnValue;
			}
			else
			{
				new DirectoryInfo(Program.root + path);
				FileInfo[] files = new DirectoryInfo(Path.Combine(Application.dataPath, Program.root + path)).GetFiles("*");
				List<AssetBundle> bundles = new List<AssetBundle>();
				for (int i = 0; i < files.Length; i++)
				{
					bundles.Add(AssetBundle.LoadFromFile(files[i].FullName));
				}
				List<GameObject> loadedPrefabs = new List<GameObject>();
				foreach (AssetBundle assetBundle in bundles)
				{
					global::UnityEngine.Object[] prefabs = assetBundle.LoadAllAssets();
					for (int j = 0; j < prefabs.Length; j++)
					{
						if (typeof(GameObject).IsInstanceOfType(prefabs[j]))
						{
							loadedPrefabs.Add(prefabs[j] as GameObject);
						}
					}
				}
				foreach (GameObject prefab in loadedPrefabs)
				{
					T t;
					if (prefab.TryGetComponent<T>(out t))
					{
						returnValue = prefab;
						break;
					}
				}
				foreach (AssetBundle assetBundle2 in bundles)
				{
					assetBundle2.Unload(false);
				}
				if (cache && returnValue != null)
				{
					ABLoader.cachedABFolder.TryAdd(path, returnValue);
				}
				if (returnValue == null)
				{
					Debug.Log("LoadFromFolderAsync get null: " + path);
				}
				if (instantiate)
				{
					if (returnValue != null)
					{
						return global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
					}
					return global::UnityEngine.Object.Instantiate<GameObject>(loadedPrefabs[0]);
				}
				else
				{
					if (returnValue != null)
					{
						return returnValue;
					}
					return loadedPrefabs[0];
				}
			}
		}

		// Token: 0x0600899C RID: 35228 RVA: 0x0010B4DC File Offset: 0x001096DC
		public static GameObject LoadFromFolder(string path)
		{
			new DirectoryInfo(Program.root + path);
			FileInfo[] files = new DirectoryInfo(Path.Combine(Application.dataPath, Program.root + path)).GetFiles("*");
			List<AssetBundle> bundles = new List<AssetBundle>();
			for (int i = 0; i < files.Length; i++)
			{
				bundles.Add(AssetBundle.LoadFromFile(files[i].FullName));
			}
			GameObject go = new GameObject(Path.GetFileName(path));
			foreach (AssetBundle assetBundle in bundles)
			{
				global::UnityEngine.Object[] prefabs = assetBundle.LoadAllAssets();
				for (int j = 0; j < prefabs.Length; j++)
				{
					if (typeof(GameObject).IsInstanceOfType(prefabs[j]))
					{
						global::UnityEngine.Object.Instantiate<GameObject>((GameObject)prefabs[j]).transform.SetParent(go.transform);
					}
				}
			}
			foreach (AssetBundle assetBundle2 in bundles)
			{
				assetBundle2.Unload(false);
			}
			return go;
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x0010B618 File Offset: 0x00109818
		public static async UniTask<GameObject> LoadFromFolderAsync<T>(string path, bool cache, bool instantiate, Action<GameObject, List<global::UnityEngine.Object>> processEvent = null) where T : Component
		{
			GameObject returnValue;
			GameObject gameObject;
			if (ABLoader.cachedABFolder.TryGetValue(path, out returnValue))
			{
				if (instantiate)
				{
					gameObject = global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
				}
				else
				{
					gameObject = returnValue;
				}
			}
			else
			{
				DirectoryInfo dir = new DirectoryInfo(Program.root + path);
				dir = new DirectoryInfo(Path.Combine(Application.dataPath, Program.root + path));
				FileInfo[] files = dir.GetFiles("*");
				List<AssetBundle> bundles = new List<AssetBundle>();
				for (int i = 0; i < files.Length; i++)
				{
					List<AssetBundle> list = bundles;
					AssetBundle assetBundle = await AssetBundle.LoadFromFileAsync(files[i].FullName);
					list.Add(assetBundle);
					list = null;
				}
				List<global::UnityEngine.Object> allAssets = new List<global::UnityEngine.Object>();
				foreach (AssetBundle assetBundle2 in bundles)
				{
					allAssets.AddRange(assetBundle2.LoadAllAssets().ToList<global::UnityEngine.Object>());
				}
				List<GameObject> allPrefabs = new List<GameObject>();
				foreach (global::UnityEngine.Object asset in allAssets)
				{
					if (typeof(GameObject).IsInstanceOfType(asset))
					{
						allPrefabs.Add(asset as GameObject);
					}
				}
				foreach (GameObject prefab in allPrefabs)
				{
					T t;
					if (prefab.TryGetComponent<T>(out t))
					{
						returnValue = prefab;
						break;
					}
				}
				foreach (AssetBundle assetBundle3 in bundles)
				{
					assetBundle3.Unload(false);
				}
				if (returnValue != null)
				{
					if (processEvent != null)
					{
						processEvent(returnValue, allAssets);
					}
				}
				if (cache && returnValue != null)
				{
					ABLoader.cachedABFolder.TryAdd(path, returnValue);
				}
				if (returnValue == null)
				{
					Debug.Log("[ABLoader]: LoadFromFolderAsync get no GameObject: " + path);
				}
				if (instantiate)
				{
					if (returnValue != null)
					{
						gameObject = global::UnityEngine.Object.Instantiate<GameObject>(returnValue);
					}
					else
					{
						gameObject = global::UnityEngine.Object.Instantiate<GameObject>(allPrefabs[0]);
					}
				}
				else if (returnValue != null)
				{
					gameObject = returnValue;
				}
				else
				{
					gameObject = allPrefabs[0];
				}
			}
			return gameObject;
		}

		// Token: 0x0600899E RID: 35230 RVA: 0x0010B674 File Offset: 0x00109874
		public static async UniTask<List<GameObject>> LoadsFromFolderAsync<T>(string path)
		{
			List<GameObject> returnValue = new List<GameObject>();
			DirectoryInfo dir = new DirectoryInfo(Program.root + path);
			dir = new DirectoryInfo(Path.Combine(Application.dataPath, Program.root + path));
			FileInfo[] files = dir.GetFiles("*");
			List<AssetBundle> bundles = new List<AssetBundle>();
			for (int i = 0; i < files.Length; i++)
			{
				List<AssetBundle> list = bundles;
				AssetBundle assetBundle = await AssetBundle.LoadFromFileAsync(files[i].FullName);
				list.Add(assetBundle);
				list = null;
			}
			List<GameObject> loadedPrefabs = new List<GameObject>();
			foreach (AssetBundle assetBundle2 in bundles)
			{
				foreach (global::UnityEngine.Object prefab in assetBundle2.LoadAllAssets())
				{
					if (typeof(GameObject).IsInstanceOfType(prefab))
					{
						loadedPrefabs.Add(prefab as GameObject);
					}
				}
			}
			foreach (GameObject prefab2 in loadedPrefabs)
			{
				T t;
				if (prefab2.TryGetComponent<T>(out t))
				{
					returnValue.Add(prefab2);
				}
			}
			foreach (AssetBundle assetBundle3 in bundles)
			{
				assetBundle3.Unload(false);
			}
			return returnValue;
		}

		// Token: 0x0600899F RID: 35231 RVA: 0x0010B6B8 File Offset: 0x001098B8
		public static async UniTask<GameObject> LoadMonsterCutinAsync(int code, bool cache)
		{
			return await ABLoader.LoadFromFolderAsync<PlayableDirector>(string.Format("MonsterCutin/{0}", code), cache, true, new Action<GameObject, List<global::UnityEngine.Object>>(ABLoader.FindAndSetSkeletonDataAsset));
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x0010B704 File Offset: 0x00109904
		private static void FindAndSetSkeletonDataAsset(GameObject prefab, List<global::UnityEngine.Object> assets)
		{
			SkeletonAnimation sa = prefab.GetComponentInChildren<SkeletonAnimation>();
			if (sa == null)
			{
				return;
			}
			if (sa.skeletonDataAsset == null)
			{
				foreach (global::UnityEngine.Object @object in assets)
				{
					SkeletonDataAsset sda = @object as SkeletonDataAsset;
					if (sda != null)
					{
						sa.skeletonDataAsset = sda;
						break;
					}
				}
			}
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x0010B77C File Offset: 0x0010997C
		public static async UniTask<Material> LoadProtectorMaterial(string code, CancellationToken token)
		{
			await ABLoader.protectorSemaphoreSlim.WaitAsync(token);
			Material material2;
			try
			{
				if (code == 9999.ToString())
				{
					code = Program.items.GetRandomItem(Items.ItemType.Protector).id.ToString();
				}
				Material material;
				if (ABLoader.cachedPMat.TryGetValue(code, out material) && material != null)
				{
					material2 = material;
				}
				else
				{
					string folder = Program.root + "MasterDuel/Protector/" + code;
					folder = Path.Combine(Application.dataPath, folder);
					if (!Directory.Exists(folder))
					{
						material2 = null;
					}
					else
					{
						string[] files = Directory.GetFiles(folder);
						AssetBundle matAB = null;
						List<AssetBundle> abs = new List<AssetBundle>();
						foreach (string file in files)
						{
							AssetBundle ab = await AssetBundle.LoadFromFileAsync(file).WithCancellation(token);
							abs.Add(ab);
							if (Path.GetFileName(file) == code)
							{
								matAB = ab;
							}
							file = null;
						}
						string[] array = null;
						if (matAB == null)
						{
							material2 = null;
						}
						else
						{
							material = matAB.LoadAsset<Material>("PMat");
							material.renderQueue = 3000;
							foreach (AssetBundle assetBundle in abs)
							{
								assetBundle.Unload(false);
							}
							if (ABLoader.cachedPMat.ContainsKey(code))
							{
								material = ABLoader.cachedPMat[code];
							}
							else
							{
								ABLoader.cachedPMat.Add(code, material);
							}
							material2 = material;
						}
					}
				}
			}
			finally
			{
				ABLoader.protectorSemaphoreSlim.Release();
			}
			return material2;
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x0010B7C8 File Offset: 0x001099C8
		public static async UniTask<Material> LoadFrameMaterial(string code)
		{
			if (code == 9999.ToString())
			{
				code = Items.lastRandomFrameID;
			}
			AssetBundle ab = await AssetBundle.LoadFromFileAsync(Program.root + "MasterDuel/Frame/ProfileFrameMat" + code);
			Material material = ab.LoadAsset<Material>("ProfileFrameMat" + code);
			ab.Unload(false);
			TextureManager.ChangeProfileFrameMaterialWrapMode(material);
			return material;
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x0010B80C File Offset: 0x00109A0C
		public static async UniTask<Material> LoadMaterialAsync(string path, CancellationToken token)
		{
			object obj = await AssetBundle.LoadFromFileAsync(Program.root + path).WithCancellation(token);
			Material matetial = obj.LoadAsset<Material>(Path.GetFileName(path));
			obj.Unload(false);
			return matetial;
		}

		// Token: 0x060089A4 RID: 35236 RVA: 0x0010B858 File Offset: 0x00109A58
		public static async UniTask<Shader> LoadShaderAsync(string path, CancellationToken token)
		{
			object obj = await AssetBundle.LoadFromFileAsync(Path.Combine(Program.root, path)).WithCancellation(token);
			Shader shader = obj.LoadAsset<Shader>(Path.GetFileNameWithoutExtension(path));
			obj.Unload(false);
			return shader;
		}

		// Token: 0x060089A5 RID: 35237 RVA: 0x0010B8A4 File Offset: 0x00109AA4
		public static async UniTask<Mate> LoadMateAsync(int code)
		{
			Items.Item item = default(Items.Item);
			foreach (Items.Item mate in Program.items.mates)
			{
				if (mate.id == code)
				{
					item = mate;
					break;
				}
			}
			Mate.MateType type = Mate.MateType.MasterDuel;
			if (item.id == 0 && File.Exists(Program.root + "CrossDuel/" + code.ToString() + ".bundle"))
			{
				type = Mate.MateType.CrossDuel;
			}
			Mate returnValue = null;
			if (type == Mate.MateType.CrossDuel)
			{
				object obj = await AssetBundle.LoadFromFileAsync(Program.root + "CrossDuel/" + code.ToString() + ".bundle");
				global::UnityEngine.Object[] all = obj.LoadAllAssets();
				obj.Unload(false);
				global::UnityEngine.Object[] array = all;
				for (int j = 0; j < array.Length; j++)
				{
					NamedAssetContainer container = array[j] as NamedAssetContainer;
					if (container != null)
					{
						GameObject prefab;
						container.TryGet<GameObject>("prefab", out prefab);
						NamedAssetContainer timelines;
						container.TryGet<NamedAssetContainer>("Timelines", out timelines);
						ParameterContainer settings;
						container.TryGet<ParameterContainer>("Settings", out settings);
						GameObject mateGo = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
						mateGo.AddComponent<FieldParamEventController_AnimationEventReceiver>();
						foreach (string s in timelines.AllNamedAssetNames())
						{
							GameObject timeline;
							timelines.TryGet<GameObject>(s, out timeline);
							GameObject newT = global::UnityEngine.Object.Instantiate<GameObject>(timeline);
							newT.transform.SetParent(mateGo.transform, false);
							newT.SetActive(true);
							for (int i = 0; i < newT.transform.childCount; i++)
							{
								if (newT.transform.GetChild(i).GetComponent<Volume>() != null)
								{
									global::UnityEngine.Object.Destroy(newT.transform.GetChild(i).gameObject);
								}
								if (newT.transform.GetChild(i).name == "UIBattleDownAni")
								{
									global::UnityEngine.Object.Destroy(newT.transform.GetChild(i).gameObject);
								}
							}
							TimelineReplacer.BindTrackInfo[] bindTrackInfo = newT.GetComponent<CustomTimelineController>().checkReplacer.m_bindTrackInfo;
							PlayableDirector director = newT.transform.GetChild(0).GetComponent<PlayableDirector>();
							if (!(director == null))
							{
								new Dictionary<string, PlayableBinding>();
								foreach (PlayableBinding pb in director.playableAsset.outputs)
								{
									foreach (TimelineReplacer.BindTrackInfo bind in bindTrackInfo)
									{
										if (pb.streamName == bind.m_name && director.GetGenericBinding(pb.sourceObject) == null)
										{
											director.SetGenericBinding(pb.sourceObject, mateGo.GetComponent<Animator>());
										}
									}
								}
							}
						}
						returnValue = mateGo.AddComponent<Mate>();
					}
				}
			}
			else
			{
				string matePath = Program.items.GetAssetPath(code.ToString(), Items.ItemType.Mate, 0);
				returnValue = ((!matePath.EndsWith("_Folder")) ? (await ABLoader.LoadFromFileAsync("MasterDuel/" + matePath, false, true)) : (await ABLoader.LoadFromFolderAsync<CharacterCollision>("MasterDuel/" + matePath.Replace("_Folder", string.Empty), false, true, null))).AddComponent<Mate>();
			}
			returnValue.type = type;
			returnValue.code = code;
			return returnValue;
		}

		// Token: 0x060089A6 RID: 35238 RVA: 0x0010B8E8 File Offset: 0x00109AE8
		public static async UniTask CacheMasterDuelOutDuelBundles()
		{
			ABLoader.mdCachedProgress = 0f;
			await ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/shaders");
			ABLoader.mdCachedProgress = 0.3f;
			UniTask<AssetBundle>.Awaiter awaiter = ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/sprites").GetAwaiter();
			UniTask<AssetBundle>.Awaiter awaiter2;
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<AssetBundle>.Awaiter);
			}
			ABLoader.mdBundleSprites = awaiter.GetResult();
			ABLoader.mdCachedProgress = 0.6f;
			awaiter = ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/materials").GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<AssetBundle>.Awaiter);
			}
			ABLoader.mdBundleMaterials = awaiter.GetResult();
			ABLoader.mdCachedProgress = 0.9f;
			awaiter = ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/outduel").GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<AssetBundle>.Awaiter);
			}
			ABLoader.mdBundleOutDuel = awaiter.GetResult();
			ABLoader.mdCachedProgress = 1f;
			ABLoader.mdCached = true;
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x0010B924 File Offset: 0x00109B24
		public static async UniTask CacheMasterDuelBundles()
		{
			if (!ABLoader.mdDuelCached)
			{
				UniTask<AssetBundle>.Awaiter awaiter = ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/textures").GetAwaiter();
				UniTask<AssetBundle>.Awaiter awaiter2;
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<AssetBundle>.Awaiter);
				}
				ABLoader.mdBundleTextures = awaiter.GetResult();
				awaiter = ABLoader.CacheFromFileAsync(Program.root + "MasterDuel/Built-in/duel").GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<AssetBundle>.Awaiter);
				}
				ABLoader.mdBundleDuel = awaiter.GetResult();
				ABLoader.mdDuelCached = true;
			}
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x0010B960 File Offset: 0x00109B60
		public static GameObject LoadMasterDuelGameObject(string oName)
		{
			if (ABLoader.mdBundleDuel == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Duel] is not cached!");
				return null;
			}
			GameObject prefab = ABLoader.mdBundleDuel.LoadAsset<GameObject>(oName);
			if (prefab == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Duel] does not contain [" + oName + "]!");
				return null;
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(prefab);
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x0010B9B8 File Offset: 0x00109BB8
		public static GameObject LoadMasterDuelOutDuelObject(string oName)
		{
			if (ABLoader.mdBundleOutDuel == null)
			{
				Debug.LogError("MasterDuel AssetBundle [OutDuel] is not cached!");
				return null;
			}
			GameObject prefab = ABLoader.mdBundleOutDuel.LoadAsset<GameObject>(oName);
			if (prefab == null)
			{
				Debug.LogError("MasterDuel AssetBundle [OutDuel] does not contain [" + oName + "]!");
				return null;
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(prefab);
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x0010BA10 File Offset: 0x00109C10
		public static Material LoadMasterDuelMaterial(string mName)
		{
			if (ABLoader.mdBundleMaterials == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Materials] is not cached!");
				return null;
			}
			Material mat = ABLoader.mdBundleMaterials.LoadAsset<Material>(mName);
			if (mat == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Materials] does not contain material [" + mName + "]!");
				return null;
			}
			return global::UnityEngine.Object.Instantiate<Material>(mat);
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x0010BA68 File Offset: 0x00109C68
		public static Sprite LoadMasterDuelSprite(string sName)
		{
			if (ABLoader.mdBundleSprites == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Sprites] is not cached!");
				return null;
			}
			Sprite sprite = ABLoader.mdBundleSprites.LoadAsset<Sprite>(sName);
			if (sprite == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Sprites] does not contain sprite [" + sName + "]!");
				return null;
			}
			return sprite;
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x0010BABC File Offset: 0x00109CBC
		public static Texture2D LoadMasterDuelTexture(string tName)
		{
			if (ABLoader.mdBundleTextures == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Textures] is not cached!");
				return null;
			}
			Texture2D tex = ABLoader.mdBundleTextures.LoadAsset<Texture2D>(tName);
			if (tex == null)
			{
				Debug.LogError("MasterDuel AssetBundle [Textures] does not contain texture [" + tName + "]!");
				return null;
			}
			return tex;
		}

		// Token: 0x0400C496 RID: 50326
		public static Dictionary<string, GameObject> cachedAB = new Dictionary<string, GameObject>();

		// Token: 0x0400C497 RID: 50327
		public static Dictionary<string, GameObject> cachedABFolder = new Dictionary<string, GameObject>();

		// Token: 0x0400C498 RID: 50328
		public static Dictionary<string, Material> cachedPMat = new Dictionary<string, Material>();

		// Token: 0x0400C499 RID: 50329
		private static readonly SemaphoreSlim protectorSemaphoreSlim = new SemaphoreSlim(1, 1);

		// Token: 0x0400C49A RID: 50330
		public static bool mdCached;

		// Token: 0x0400C49B RID: 50331
		public static float mdCachedProgress;

		// Token: 0x0400C49C RID: 50332
		private static bool mdDuelCached;

		// Token: 0x0400C49D RID: 50333
		private static AssetBundle mdBundleDuel;

		// Token: 0x0400C49E RID: 50334
		private static AssetBundle mdBundleOutDuel;

		// Token: 0x0400C49F RID: 50335
		private static AssetBundle mdBundleMaterials;

		// Token: 0x0400C4A0 RID: 50336
		private static AssetBundle mdBundleSprites;

		// Token: 0x0400C4A1 RID: 50337
		private static AssetBundle mdBundleTextures;
	}
}
