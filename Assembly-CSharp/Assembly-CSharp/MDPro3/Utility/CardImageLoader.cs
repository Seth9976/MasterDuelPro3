using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Ionic.Zip;
using MDPro3.Duel.YGOSharp;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace MDPro3.Utility
{
	// Token: 0x020012B2 RID: 4786
	public static class CardImageLoader
	{
		// Token: 0x06008C3A RID: 35898 RVA: 0x00121F54 File Offset: 0x00120154
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			CardImageLoader.maxLoads = CardImageLoader.GetOptimalConcurrency();
			CardImageLoader.artSemaphore = new SemaphoreSlim(CardImageLoader.maxLoads, CardImageLoader.maxLoads);
			CardImageLoader.cardSemaphore = new SemaphoreSlim(CardImageLoader.maxLoads, CardImageLoader.maxLoads);
			CardImageLoader.InitializeArtFileListAsync();
			CardImageLoader.InitializeVideoArtFileListAsync();
			SystemEvent.OnVideoCardConfigChange += CardImageLoader.CheckArtVideoConfig;
		}

		// Token: 0x06008C3B RID: 35899 RVA: 0x00121FB0 File Offset: 0x001201B0
		private static int GetOptimalConcurrency()
		{
			if (DeviceInfo.OnMobile())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008C3C RID: 35900 RVA: 0x00121FBC File Offset: 0x001201BC
		public static async Task<Texture2D> LoadArtAsync(int code, bool persistent = false, CancellationToken token = default(CancellationToken))
		{
			SemaphoreSlim lockObj = CardImageLoader.artLoadingLocks.GetOrAdd(code, (int _) => new SemaphoreSlim(1, 1));
			await lockObj.WaitAsync(token);
			Texture2D texture2D;
			try
			{
				CardImageLoader.CacheEntry entry;
				if (CardImageLoader.cachedArts.TryGetValue(code, out entry))
				{
					if (entry.LoadingTask != null)
					{
						await entry.LoadingTask.AsUniTask(true).AttachExternalCancellation(token);
					}
					if (entry.Texture != null)
					{
						Interlocked.Increment(ref entry.ReferenceCount);
						entry.IsPersistent = persistent;
					}
					else
					{
						Debug.LogError(string.Format("Art texture is null for code {0}", code));
					}
					texture2D = entry.Texture;
				}
				else
				{
					CardImageLoader.CacheEntry newEntry = new CardImageLoader.CacheEntry
					{
						LoadingTask = CardImageLoader.InternalLoadArtAsync(code, token)
					};
					if (CardImageLoader.cachedArts.TryAdd(code, newEntry))
					{
						newEntry.ReferenceCount = 1;
						newEntry.IsPersistent = persistent;
						try
						{
							CardImageLoader.CacheEntry cacheEntry = newEntry;
							cacheEntry.Texture = await newEntry.LoadingTask;
							cacheEntry = null;
						}
						catch (OperationCanceledException ex)
						{
							CardImageLoader.CacheEntry cacheEntry2;
							CardImageLoader.cachedArts.TryRemove(code, out cacheEntry2);
							throw ex;
						}
						if (newEntry.Texture == null)
						{
							Debug.LogError(string.Format("{0} art is null", code));
						}
						newEntry.LoadingTask = null;
						texture2D = newEntry.Texture;
					}
					else
					{
						Debug.LogError("CardImageLoader: Unexpected Errror.");
						texture2D = null;
					}
				}
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			finally
			{
				lockObj.Release();
				SemaphoreSlim semaphoreSlim;
				CardImageLoader.artLoadingLocks.TryRemove(code, out semaphoreSlim);
			}
			return texture2D;
		}

		// Token: 0x06008C3D RID: 35901 RVA: 0x00122010 File Offset: 0x00120210
		public static void ReleaseArt(int code)
		{
			CardImageLoader.CacheEntry entry;
			if (!CardImageLoader.cachedArts.TryGetValue(code, out entry))
			{
				return;
			}
			int num = Interlocked.Decrement(ref entry.ReferenceCount);
			if (num < 0)
			{
				Debug.LogError(string.Format("Art reference count for code {0} is less than zero.", code));
			}
			CardImageLoader.CacheEntry cacheEntry;
			if (num == 0 && !entry.IsPersistent && CardImageLoader.cachedArts.TryRemove(code, out cacheEntry))
			{
				global::UnityEngine.Object.Destroy(entry.Texture);
			}
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x00122078 File Offset: 0x00120278
		public static async Task<Texture> LoadCardAsync(int code, bool persistent = false, CancellationToken token = default(CancellationToken), bool forceTexture = false)
		{
			SemaphoreSlim lockObj = CardImageLoader.cardLoadingLocks.GetOrAdd(code, (int _) => new SemaphoreSlim(1, 1));
			await lockObj.WaitAsync(token);
			Texture texture2;
			try
			{
				Texture tex;
				if (!CardRenderer.CardHasVideoArt(code) || forceTexture)
				{
					CardImageLoader.CacheEntry entry;
					if (CardImageLoader.cachedCards.TryGetValue(code, out entry))
					{
						if (entry.LoadingTask != null)
						{
							await entry.LoadingTask.AsUniTask(true).AttachExternalCancellation(token);
						}
						if (entry.Texture != null)
						{
							Interlocked.Increment(ref entry.ReferenceCount);
							entry.IsPersistent = persistent;
						}
						else
						{
							Debug.LogError(string.Format("Card texture is null for code {0}", code));
						}
						texture2 = entry.Texture;
					}
					else
					{
						CardImageLoader.CacheEntry newEntry = new CardImageLoader.CacheEntry
						{
							LoadingTask = CardImageLoader.InternalLoadCardAsync(code, token)
						};
						if (CardImageLoader.cachedCards.TryAdd(code, newEntry))
						{
							newEntry.ReferenceCount = 1;
							newEntry.IsPersistent = persistent;
							try
							{
								CardImageLoader.CacheEntry cacheEntry = newEntry;
								cacheEntry.Texture = await newEntry.LoadingTask;
								cacheEntry = null;
							}
							catch (OperationCanceledException ex)
							{
								CardImageLoader.CacheEntry cacheEntry2;
								CardImageLoader.cachedCards.TryRemove(code, out cacheEntry2);
								throw ex;
							}
							newEntry.LoadingTask = null;
							texture2 = newEntry.Texture;
						}
						else
						{
							Debug.LogError("CardImageLoader: Unexpected Error.");
							texture2 = null;
						}
					}
				}
				else if (CardImageLoader.cachedVideoCards.TryGetValue(code, out tex))
				{
					texture2 = tex;
				}
				else
				{
					Texture texture = await CardImageLoader.InternalLoadVideoCardAsync(code, token);
					CardImageLoader.cachedVideoCards[code] = texture;
					texture2 = texture;
				}
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			finally
			{
				lockObj.Release();
				SemaphoreSlim semaphoreSlim;
				CardImageLoader.cardLoadingLocks.TryRemove(code, out semaphoreSlim);
			}
			return texture2;
		}

		// Token: 0x06008C3F RID: 35903 RVA: 0x001220D4 File Offset: 0x001202D4
		public static void ReleaseCard(int code)
		{
			CardImageLoader.CacheEntry entry;
			if (!CardImageLoader.cachedCards.TryGetValue(code, out entry))
			{
				return;
			}
			CardImageLoader.CacheEntry cacheEntry;
			if (Interlocked.Decrement(ref entry.ReferenceCount) == 0 && !entry.IsPersistent && CardImageLoader.cachedCards.TryRemove(code, out cacheEntry))
			{
				global::UnityEngine.Object.DestroyImmediate(entry.Texture);
			}
		}

		// Token: 0x06008C40 RID: 35904 RVA: 0x00122120 File Offset: 0x00120320
		public static Texture2D LoadCardName(int code)
		{
			Texture2D tex;
			if (CardImageLoader.cachedCardNames.TryGetValue(code, out tex))
			{
				return tex;
			}
			tex = CardImageLoader.InternalLoadCardName(code);
			CardImageLoader.cachedCardNames.TryAdd(code, tex);
			return tex;
		}

		// Token: 0x06008C41 RID: 35905 RVA: 0x00122154 File Offset: 0x00120354
		public static void ClearCache()
		{
			foreach (CardImageLoader.CacheEntry cacheEntry in CardImageLoader.cachedCards.Values)
			{
				global::UnityEngine.Object.Destroy(cacheEntry.Texture);
			}
			CardImageLoader.cachedCards.Clear();
			foreach (Texture2D texture2D in CardImageLoader.cachedCardNames.Values)
			{
				global::UnityEngine.Object.Destroy(texture2D);
			}
			CardImageLoader.cachedCardNames.Clear();
			CardImageLoader.ClearArtVideos();
		}

		// Token: 0x06008C42 RID: 35906 RVA: 0x00122200 File Offset: 0x00120400
		private static async Task<Texture2D> InternalLoadArtAsync(int code, CancellationToken token)
		{
			await CardImageLoader.artSemaphore.WaitAsync(token);
			CardImageLoader.lastCardFoundArt = true;
			Texture2D texture2D;
			try
			{
				string path = CardImageLoader.GetArtFilePath(code);
				if (string.IsNullOrEmpty(path))
				{
					bool needCrop = false;
					Texture2D art = await CardImageLoader.LoadArtFromZipAsync("art", code, token);
					if (art == null)
					{
						needCrop = true;
						art = await CardImageLoader.LoadArtFromZipAsync("pics", code, token);
					}
					if (art == null)
					{
						CardImageLoader.lastCardFoundArt = false;
						texture2D = null;
					}
					else
					{
						if (needCrop)
						{
							art = CardImageLoader.CropCardToArt(art, code);
						}
						texture2D = art;
					}
				}
				else
				{
					using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(path))
					{
						try
						{
							await request.SendWebRequest().ToUniTask(null, PlayerLoopTiming.Update, token, false);
						}
						catch (OperationCanceledException ex2) when (token.IsCancellationRequested)
						{
							request.Abort();
							throw ex2;
						}
						catch (Exception ex)
						{
							Debug.LogError("加载失败: " + ex.Message);
							CardImageLoader.lastCardFoundArt = false;
							throw ex;
						}
						if (request.result != UnityWebRequest.Result.Success)
						{
							Debug.LogError("加载失败: " + request.error);
							CardImageLoader.lastCardFoundArt = false;
							texture2D = null;
						}
						else
						{
							texture2D = DownloadHandlerTexture.GetContent(request);
						}
					}
				}
			}
			finally
			{
				CardImageLoader.artSemaphore.Release();
			}
			return texture2D;
		}

		// Token: 0x06008C43 RID: 35907 RVA: 0x0012224C File Offset: 0x0012044C
		private static async Task<Texture2D> InternalLoadCardAsync(int code, CancellationToken token)
		{
			await UniTask.WaitUntil(() => TextureManager.container != null, PlayerLoopTiming.Update, token, false);
			await CardImageLoader.cardSemaphore.WaitAsync(token);
			CardImageLoader.lastCardRenderSucceed = true;
			Texture2D texture2D;
			try
			{
				Card data = CardsManager.Get(code, true);
				if (data.Id == 0)
				{
					CardImageLoader.lastCardRenderSucceed = false;
					texture2D = TextureManager.container.unknownCard.texture;
				}
				else
				{
					Texture2D art = await CardImageLoader.LoadArtAsync(code, false, token).AsUniTask(true).AttachExternalCancellation(token);
					if (token.IsCancellationRequested)
					{
						throw new OperationCanceledException(token);
					}
					if (art == null)
					{
						Debug.LogError(string.Format("Get null from ArtLoad for Card {0}:", data.Id));
						art = TextureManager.container.unknownArt.texture;
					}
					if (!Program.instance.cardRenderer.RenderCard(code, art))
					{
						CardImageLoader.lastCardRenderSucceed = false;
						texture2D = TextureManager.container.unknownCard.texture;
					}
					else
					{
						RenderTexture.active = Program.instance.cardRenderer.renderTexture;
						Texture2D texture2D2 = new Texture2D(RenderTexture.active.width, RenderTexture.active.height, TextureFormat.RGB24, true);
						texture2D2.ReadPixels(new Rect(0f, 0f, (float)RenderTexture.active.width, (float)RenderTexture.active.height), 0, 0);
						texture2D2.Apply();
						texture2D2.name = "Card_" + code.ToString();
						CardImageLoader.ReleaseArt(code);
						texture2D = texture2D2;
					}
				}
			}
			finally
			{
				CardImageLoader.cardSemaphore.Release();
			}
			return texture2D;
		}

		// Token: 0x06008C44 RID: 35908 RVA: 0x00122298 File Offset: 0x00120498
		private static async Task<Texture> InternalLoadVideoCardAsync(int code, CancellationToken token)
		{
			await UniTask.WaitUntil(() => TextureManager.container != null, PlayerLoopTiming.Update, token, false);
			await CardImageLoader.cardSemaphore.WaitAsync(token);
			CardImageLoader.lastCardRenderSucceed = true;
			Texture texture;
			try
			{
				Card data = CardsManager.Get(code, true);
				if (data.Id == 0)
				{
					CardImageLoader.lastCardRenderSucceed = false;
					texture = TextureManager.container.unknownCard.texture;
				}
				else
				{
					global::UnityEngine.Object @object = await CardImageLoader.LoadArtAsync(code, false, token).AsUniTask(true).AttachExternalCancellation(token);
					if (token.IsCancellationRequested)
					{
						throw new OperationCanceledException(token);
					}
					if (@object == null)
					{
						Debug.LogError(string.Format("Get null from ArtLoad for Card {0}:", data.Id));
						Texture2D texture2 = TextureManager.container.unknownArt.texture;
					}
					CardRenderer cardRenderer = (await Addressables.InstantiateAsync("Prefab/CardRenderer.prefab", null, false, true)).GetComponent<CardRenderer>();
					CardImageLoader.videos.Add(cardRenderer);
					texture = await cardRenderer.GetVideoCardAsync(code);
				}
			}
			finally
			{
				CardImageLoader.cardSemaphore.Release();
			}
			return texture;
		}

		// Token: 0x06008C45 RID: 35909 RVA: 0x001222E4 File Offset: 0x001204E4
		private static Texture2D InternalLoadCardName(int code)
		{
			Program.instance.cardRenderer.RenderName(code);
			RenderTexture.active = Program.instance.cardRenderer.renderTexture;
			Texture2D texture2D = new Texture2D(RenderTexture.active.width, 203, TextureFormat.RGBA32, false);
			Rect rect = new Rect(0f, (float)(Program.instance.cardRenderer.renderTexture.height - 203), (float)Program.instance.cardRenderer.renderTexture.width, 203f);
			texture2D.ReadPixels(rect, 0, 0);
			texture2D.Apply();
			texture2D.wrapMode = TextureWrapMode.Clamp;
			return texture2D;
		}

		// Token: 0x06008C46 RID: 35910 RVA: 0x00122384 File Offset: 0x00120584
		private static async UniTask<Texture2D> LoadArtFromZipAsync(string folder, int code, CancellationToken token)
		{
			MemoryStream stream = null;
			string targetPNG = string.Format("{0}/{1}{2}", folder.ToLower(), code, ".png".ToLower());
			string targetJPG = string.Format("{0}/{1}{2}", folder.ToLower(), code, ".jpg".ToLower());
			Texture2D texture2D2;
			try
			{
				foreach (ZipFile zip in ZipHelper.zips)
				{
					if (!zip.Name.ToLower().EndsWith("script.zip"))
					{
						foreach (string file in zip.EntryFileNames)
						{
							if (file.ToLower().Replace("\\", "/") == targetPNG || file.ToLower().Replace("\\", "/") == targetJPG)
							{
								stream = Tools.GetStream();
								zip[file].Extract(stream);
								break;
							}
						}
					}
				}
				await UniTask.Yield(token, false);
				if (stream != null)
				{
					Texture2D texture2D = new Texture2D(0, 0);
					texture2D.LoadImage(stream.ToArray());
					texture2D2 = texture2D;
				}
				else
				{
					texture2D2 = null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (stream != null)
				{
					Tools.ReturnStream(stream);
				}
			}
			return texture2D2;
		}

		// Token: 0x06008C47 RID: 35911 RVA: 0x001223D8 File Offset: 0x001205D8
		private static Texture2D CropCardToArt(Texture2D pic, int code)
		{
			Card data = CardsManager.Get(code, false);
			if (code >= 120000000 && code < 130000000)
			{
				if (data.HasType(CardType.Monster))
				{
					return CardImageLoader.GetArtFromRushDuelMonsterCard(pic);
				}
				return CardImageLoader.GetArtFromRushDuelSpellCard(pic);
			}
			else
			{
				if (data.HasType(CardType.Pendulum))
				{
					return CardImageLoader.GetArtFromPendulumCard(pic);
				}
				return CardImageLoader.GetArtFromCard(pic);
			}
		}

		// Token: 0x06008C48 RID: 35912 RVA: 0x0012242E File Offset: 0x0012062E
		private static void CheckArtVideoConfig()
		{
			if (!Config.GetBool("VideoCard", true))
			{
				CardImageLoader.ClearArtVideos();
			}
		}

		// Token: 0x06008C49 RID: 35913 RVA: 0x00122444 File Offset: 0x00120644
		private static void ClearArtVideos()
		{
			foreach (CardRenderer cardRenderer in CardImageLoader.videos)
			{
				cardRenderer.Dispose();
			}
			CardImageLoader.videos.Clear();
			CardImageLoader.cachedVideoCards.Clear();
		}

		// Token: 0x06008C4A RID: 35914 RVA: 0x001224A8 File Offset: 0x001206A8
		private static Texture2D GetArtFromCard(Texture2D cardPic)
		{
			int startX = Mathf.CeilToInt((float)cardPic.width * 0.13f);
			int startY = Mathf.CeilToInt((float)cardPic.height * 0.3f);
			int width = Mathf.CeilToInt((float)cardPic.width * 0.87f);
			int height = Mathf.CeilToInt((float)cardPic.height * 0.81f);
			return CardImageLoader.GetCroppingTex(cardPic, startX, startY, width, height);
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x0012250C File Offset: 0x0012070C
		private static Texture2D GetArtFromPendulumCard(Texture2D cardPic)
		{
			int startX = Mathf.CeilToInt((float)cardPic.width * 0.067f);
			int startY = Mathf.CeilToInt((float)cardPic.height * 0.38f);
			int width = Mathf.CeilToInt((float)cardPic.width * 0.933f);
			int height = Mathf.CeilToInt((float)cardPic.height * 0.81f);
			return CardImageLoader.GetCroppingTex(cardPic, startX, startY, width, height);
		}

		// Token: 0x06008C4C RID: 35916 RVA: 0x00122570 File Offset: 0x00120770
		private static Texture2D GetArtFromRushDuelMonsterCard(Texture2D cardPic)
		{
			int startX = Mathf.CeilToInt((float)cardPic.width * 0.067f);
			int startY = Mathf.CeilToInt((float)cardPic.height * 0.29f);
			int width = Mathf.CeilToInt((float)cardPic.width * 0.933f);
			int height = Mathf.CeilToInt((float)cardPic.height * 0.9f);
			return CardImageLoader.GetCroppingTex(cardPic, startX, startY, width, height);
		}

		// Token: 0x06008C4D RID: 35917 RVA: 0x001225D4 File Offset: 0x001207D4
		private static Texture2D GetArtFromRushDuelSpellCard(Texture2D cardPic)
		{
			int startX = Mathf.CeilToInt((float)cardPic.width * 0.067f);
			int startY = Mathf.CeilToInt((float)cardPic.height * 0.29f);
			int width = Mathf.CeilToInt((float)cardPic.width * 0.933f);
			int height = Mathf.CeilToInt((float)cardPic.height * 0.9f);
			return CardImageLoader.GetCroppingTex(cardPic, startX, startY, width, height);
		}

		// Token: 0x06008C4E RID: 35918 RVA: 0x00122638 File Offset: 0x00120838
		private static Texture2D GetCroppingTex(Texture2D texture, int startX, int startY, int width, int height)
		{
			Texture2D returnValue = new Texture2D(width - startX, height - startY);
			Color32[] pix = new Color32[returnValue.width * returnValue.height];
			int index = 0;
			for (int y = startY; y < height; y++)
			{
				for (int x = startX; x < width; x++)
				{
					pix[index++] = texture.GetPixel(x, y);
				}
			}
			returnValue.SetPixels32(pix);
			returnValue.Apply();
			return returnValue;
		}

		// Token: 0x06008C4F RID: 35919 RVA: 0x001226AC File Offset: 0x001208AC
		private static async UniTask InitializeArtFileListAsync()
		{
			if (!CardImageLoader.artFileListInitialized)
			{
				await UniTask.Yield();
				string path = "Picture/Art/";
				path = Path.Combine(Environment.CurrentDirectory, path);
				if (Directory.Exists(path))
				{
					string[] array = Directory.GetFiles(path, "*.jpg");
					for (int i = 0; i < array.Length; i++)
					{
						int code;
						if (int.TryParse(Path.GetFileNameWithoutExtension(array[i]), out code))
						{
							CardImageLoader.artFileList.Add(code);
						}
					}
				}
				path = "Picture/Art2/";
				path = Path.Combine(Environment.CurrentDirectory, path);
				if (Directory.Exists(path))
				{
					string[] array = Directory.GetFiles(path, "*.jpg");
					for (int i = 0; i < array.Length; i++)
					{
						int code2;
						if (int.TryParse(Path.GetFileNameWithoutExtension(array[i]), out code2))
						{
							CardImageLoader.artAltFileList.Add(code2, ".jpg");
						}
					}
					array = Directory.GetFiles(path, "*.png");
					for (int i = 0; i < array.Length; i++)
					{
						int code3;
						if (int.TryParse(Path.GetFileNameWithoutExtension(array[i]), out code3) && !CardImageLoader.artAltFileList.ContainsKey(code3))
						{
							CardImageLoader.artAltFileList.Add(code3, ".png");
						}
					}
				}
				CardImageLoader.artFileListInitialized = true;
			}
		}

		// Token: 0x06008C50 RID: 35920 RVA: 0x001226E8 File Offset: 0x001208E8
		private static string GetArtFilePath(int code)
		{
			string path = string.Empty;
			if (CardImageLoader.artAltFileList.ContainsKey(code))
			{
				path = "Picture/Art2/" + code.ToString() + CardImageLoader.artAltFileList[code];
			}
			else if (CardImageLoader.artFileList.Contains(code))
			{
				path = "Picture/Art/" + code.ToString() + ".jpg";
			}
			if (!string.IsNullOrEmpty(path))
			{
				path = Path.Combine(Environment.CurrentDirectory, path);
			}
			return path;
		}

		// Token: 0x06008C51 RID: 35921 RVA: 0x00122760 File Offset: 0x00120960
		private static async UniTask InitializeVideoArtFileListAsync()
		{
			if (!CardImageLoader.videoArtFileListInitialized)
			{
				string path = "Video/Art/";
				path = Path.Combine(Environment.CurrentDirectory, path);
				if (Directory.Exists(path))
				{
					await UniTask.Yield();
					string[] files = Directory.GetFiles(path, "*.mp4");
					for (int i = 0; i < files.Length; i++)
					{
						int code;
						if (int.TryParse(Path.GetFileNameWithoutExtension(files[i]), out code))
						{
							CardImageLoader.videoArtFileList.Add(code);
						}
					}
				}
				CardImageLoader.videoArtFileListInitialized = true;
			}
		}

		// Token: 0x06008C52 RID: 35922 RVA: 0x0012279B File Offset: 0x0012099B
		public static bool CardHasVideoArt(int code)
		{
			return CardImageLoader.videoArtFileList.Contains(code);
		}

		// Token: 0x06008C53 RID: 35923 RVA: 0x001227A8 File Offset: 0x001209A8
		public static void ReloadArtVideos()
		{
			CardImageLoader.videoArtFileListInitialized = false;
			CardImageLoader.InitializeVideoArtFileListAsync();
			CardImageLoader.ClearArtVideos();
		}

		// Token: 0x0400C9F3 RID: 51699
		private static readonly ConcurrentDictionary<int, CardImageLoader.CacheEntry> cachedArts = new ConcurrentDictionary<int, CardImageLoader.CacheEntry>();

		// Token: 0x0400C9F4 RID: 51700
		private static readonly ConcurrentDictionary<int, CardImageLoader.CacheEntry> cachedCards = new ConcurrentDictionary<int, CardImageLoader.CacheEntry>();

		// Token: 0x0400C9F5 RID: 51701
		private static readonly ConcurrentDictionary<int, Texture2D> cachedCardNames = new ConcurrentDictionary<int, Texture2D>();

		// Token: 0x0400C9F6 RID: 51702
		private static readonly ConcurrentDictionary<int, Texture> cachedVideoCards = new ConcurrentDictionary<int, Texture>();

		// Token: 0x0400C9F7 RID: 51703
		private static readonly ConcurrentDictionary<int, SemaphoreSlim> artLoadingLocks = new ConcurrentDictionary<int, SemaphoreSlim>();

		// Token: 0x0400C9F8 RID: 51704
		private static readonly ConcurrentDictionary<int, SemaphoreSlim> cardLoadingLocks = new ConcurrentDictionary<int, SemaphoreSlim>();

		// Token: 0x0400C9F9 RID: 51705
		private static readonly List<CardRenderer> videos = new List<CardRenderer>();

		// Token: 0x0400C9FA RID: 51706
		public static bool lastCardFoundArt;

		// Token: 0x0400C9FB RID: 51707
		public static bool lastCardRenderSucceed;

		// Token: 0x0400C9FC RID: 51708
		private static SemaphoreSlim artSemaphore;

		// Token: 0x0400C9FD RID: 51709
		private static SemaphoreSlim cardSemaphore;

		// Token: 0x0400C9FE RID: 51710
		private static int maxLoads;

		// Token: 0x0400C9FF RID: 51711
		private static readonly List<int> artFileList = new List<int>();

		// Token: 0x0400CA00 RID: 51712
		private static readonly Dictionary<int, string> artAltFileList = new Dictionary<int, string>();

		// Token: 0x0400CA01 RID: 51713
		private static bool artFileListInitialized;

		// Token: 0x0400CA02 RID: 51714
		private static readonly List<int> videoArtFileList = new List<int>();

		// Token: 0x0400CA03 RID: 51715
		private static bool videoArtFileListInitialized;

		// Token: 0x020012B3 RID: 4787
		private class CacheEntry
		{
			// Token: 0x0400CA04 RID: 51716
			public Texture2D Texture;

			// Token: 0x0400CA05 RID: 51717
			public int ReferenceCount;

			// Token: 0x0400CA06 RID: 51718
			public bool IsPersistent;

			// Token: 0x0400CA07 RID: 51719
			public Task<Texture2D> LoadingTask;
		}
	}
}
