using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MDPro3.Utility;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace MDPro3
{
	// Token: 0x020012A7 RID: 4775
	public static class Tools
	{
		// Token: 0x06008BFC RID: 35836 RVA: 0x0012011C File Offset: 0x0011E31C
		public static void ChangeLayer(GameObject go, string layer, bool setAllChildrenActivate = false)
		{
			foreach (Transform t in go.transform.GetComponentsInChildren<Transform>(true))
			{
				if (setAllChildrenActivate)
				{
					t.gameObject.SetActive(true);
				}
				t.gameObject.layer = LayerMask.NameToLayer(layer);
			}
		}

		// Token: 0x06008BFD RID: 35837 RVA: 0x00120168 File Offset: 0x0011E368
		public static void ChangeLayer(GameObject go, int layerMask, bool setAllChildrenActivate = false)
		{
			foreach (Transform t in go.transform.GetComponentsInChildren<Transform>(true))
			{
				if (setAllChildrenActivate)
				{
					t.gameObject.SetActive(true);
				}
				t.gameObject.layer = layerMask;
			}
		}

		// Token: 0x06008BFE RID: 35838 RVA: 0x001201B0 File Offset: 0x0011E3B0
		public static void ChangeSortingLayer(GameObject go, string sortingLayer)
		{
			Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingLayerName = sortingLayer;
			}
		}

		// Token: 0x06008BFF RID: 35839 RVA: 0x001201DC File Offset: 0x0011E3DC
		public static void ChangeMaterialRenderQueue(GameObject root, int queue)
		{
			Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material.renderQueue = queue;
			}
		}

		// Token: 0x06008C00 RID: 35840 RVA: 0x00120210 File Offset: 0x0011E410
		public static void PlayAnimation(Transform animationContainer, string animationName)
		{
			if (animationContainer == null)
			{
				return;
			}
			Animator[] componentsInChildren = animationContainer.GetComponentsInChildren<Animator>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetTrigger(animationName);
			}
		}

		// Token: 0x06008C01 RID: 35841 RVA: 0x00120248 File Offset: 0x0011E448
		public static void PlayParticle(Transform particleContainer, string particleName)
		{
			if (particleContainer == null)
			{
				return;
			}
			foreach (Transform child in particleContainer.GetComponentsInChildren<Transform>(true))
			{
				if (child.name.ToLower().Contains(particleName.ToLower()))
				{
					ParticleSystem[] componentsInChildren2 = child.GetComponentsInChildren<ParticleSystem>(true);
					for (int j = 0; j < componentsInChildren2.Length; j++)
					{
						componentsInChildren2[j].Play();
					}
				}
			}
		}

		// Token: 0x06008C02 RID: 35842 RVA: 0x001202B4 File Offset: 0x0011E4B4
		public static void SetAnimatorTimescale(Transform container, float timeScale)
		{
			Animator[] componentsInChildren = container.GetComponentsInChildren<Animator>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].speed = timeScale;
			}
		}

		// Token: 0x06008C03 RID: 35843 RVA: 0x001202E0 File Offset: 0x0011E4E0
		public static void SetParticleSystemSimulationSpeed(Transform container, float timeScale)
		{
			ParticleSystem[] componentsInChildren = container.GetComponentsInChildren<ParticleSystem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].main.simulationSpeed = timeScale;
			}
		}

		// Token: 0x06008C04 RID: 35844 RVA: 0x00120314 File Offset: 0x0011E514
		public static void SetPlayableDirectorUnscaledGameTime(Transform container)
		{
			PlayableDirector[] componentsInChildren = container.GetComponentsInChildren<PlayableDirector>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
			}
		}

		// Token: 0x06008C05 RID: 35845 RVA: 0x00120340 File Offset: 0x0011E540
		public static PlayableDirector GetPlayableDirectorInChildren(Transform container)
		{
			PlayableDirector returnValue = null;
			for (int i = 0; i < container.childCount; i++)
			{
				if (container.GetChild(i).GetComponent<PlayableDirector>() != null)
				{
					returnValue = container.GetChild(i).GetComponent<PlayableDirector>();
				}
				else
				{
					global::UnityEngine.Object.Destroy(container.GetChild(i).gameObject);
				}
			}
			return returnValue;
		}

		// Token: 0x06008C06 RID: 35846 RVA: 0x00120398 File Offset: 0x0011E598
		public static int CompareTime(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo xInfo = (FileInfo)x;
			return ((FileInfo)y).LastWriteTime.CompareTo(xInfo.LastWriteTime);
		}

		// Token: 0x06008C07 RID: 35847 RVA: 0x001203D8 File Offset: 0x0011E5D8
		public static int CompareName(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileSystemInfo fileSystemInfo = (FileInfo)x;
			FileInfo yInfo = (FileInfo)y;
			return fileSystemInfo.FullName.CompareTo(yInfo.FullName);
		}

		// Token: 0x06008C08 RID: 35848 RVA: 0x00120414 File Offset: 0x0011E614
		public static string GetTimeString()
		{
			return DateTime.Now.ToString("MM-dd「HH：mm：ss」");
		}

		// Token: 0x06008C09 RID: 35849 RVA: 0x00120434 File Offset: 0x0011E634
		public static List<string> GetLocalIPv4()
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			List<string> returnValue = new List<string>();
			foreach (IPAddress address in hostEntry.AddressList)
			{
				if (address.AddressFamily == AddressFamily.InterNetwork)
				{
					returnValue.Add(address.ToString() ?? "127.0.0.1");
				}
			}
			return returnValue;
		}

		// Token: 0x06008C0A RID: 35850 RVA: 0x00120488 File Offset: 0x0011E688
		public static string[] SplitWithPreservedQuotes(string input)
		{
			List<string> result = new List<string>();
			int start = 0;
			bool inQuotes = false;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '"')
				{
					inQuotes = !inQuotes;
				}
				else if (input[i] == ' ' && !inQuotes)
				{
					result.Add(input.Substring(start, i - start));
					start = i + 1;
				}
			}
			result.Add(input.Substring(start));
			for (int j = 0; j < result.Count; j++)
			{
				result[j] = result[j].Replace("\"", "");
			}
			return result.ToArray();
		}

		// Token: 0x06008C0B RID: 35851 RVA: 0x0012052C File Offset: 0x0011E72C
		public static KeyValuePair<TKey, TValue> GetNthDictionaryElement<TKey, TValue>(Dictionary<TKey, TValue> dic, int n)
		{
			if (n < 0)
			{
				n = 0;
			}
			if (n >= dic.Count)
			{
				n = dic.Count - 1;
			}
			Dictionary<TKey, TValue>.Enumerator enumerator = dic.GetEnumerator();
			for (int i = 0; i < n + 1; i++)
			{
				enumerator.MoveNext();
			}
			return enumerator.Current;
		}

		// Token: 0x06008C0C RID: 35852 RVA: 0x00120577 File Offset: 0x0011E777
		public static KeyValuePair<TKey, TValue> GetRandomDictionaryElement<TKey, TValue>(Dictionary<TKey, TValue> dic)
		{
			return Tools.GetNthDictionaryElement<TKey, TValue>(dic, global::UnityEngine.Random.Range(0, dic.Count));
		}

		// Token: 0x06008C0D RID: 35853 RVA: 0x0012058B File Offset: 0x0011E78B
		public static bool StringIsAlphaNumeric(string input)
		{
			return new Regex("^[A-Za-z0-9]+$").IsMatch(input);
		}

		// Token: 0x06008C0E RID: 35854 RVA: 0x0012059D File Offset: 0x0011E79D
		public static bool StringIsLowerAlphaNumeric(string input)
		{
			return new Regex("^[a-z0-9]+$").IsMatch(input);
		}

		// Token: 0x06008C0F RID: 35855 RVA: 0x001205AF File Offset: 0x0011E7AF
		public static int GetLocalDeckCount()
		{
			return Directory.GetFiles("Deck/", "*.ydk").Length;
		}

		// Token: 0x06008C10 RID: 35856 RVA: 0x001205C4 File Offset: 0x0011E7C4
		public static DateTime GetLocalDeckLastEditTime()
		{
			DateTime dateTime = DateTime.MinValue;
			string[] files = Directory.GetFiles("Deck/", "*.ydk");
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fileInfo = new FileInfo(files[i]);
				if (fileInfo.LastWriteTime > dateTime)
				{
					dateTime = fileInfo.LastWriteTime;
				}
			}
			return dateTime;
		}

		// Token: 0x06008C11 RID: 35857 RVA: 0x00120614 File Offset: 0x0011E814
		public static async Task<Texture2D> DownloadImageAsync(string url)
		{
			Texture2D texture2D;
			using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
			{
				request.SetRequestHeader("User-Agent", string.Concat(new string[]
				{
					"MDPro3/",
					Application.version,
					" (",
					Environment.OSVersion.ToString(),
					"); Unity/",
					Application.unityVersion
				}));
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					texture2D = null;
				}
				else if (request.result == UnityWebRequest.Result.Success)
				{
					texture2D = DownloadHandlerTexture.GetContent(request);
				}
				else
				{
					Debug.LogErrorFormat(string.Format("Image [{0}]: {1}", 0, 1), new object[] { url, request.error });
					texture2D = null;
				}
			}
			return texture2D;
		}

		// Token: 0x06008C12 RID: 35858 RVA: 0x00120658 File Offset: 0x0011E858
		public static Vector3 GetDeckModelTopPosition(ElementObjectManager manager)
		{
			ElementObjectManager element = manager.GetElement<ElementObjectManager>("CardShuffleTop");
			Vector3 returnValue = element.GetElement<Transform>("CardModel01_back").position;
			Vector3 position = element.GetElement<Transform>("CardModel02_back").position;
			if (position.y > returnValue.y)
			{
				returnValue = position;
			}
			position = element.GetElement<Transform>("CardModel03_back").position;
			if (position.y > returnValue.y)
			{
				returnValue = position;
			}
			position = element.GetElement<Transform>("CardModel04_back").position;
			if (position.y > returnValue.y)
			{
				returnValue = position;
			}
			return returnValue;
		}

		// Token: 0x06008C13 RID: 35859 RVA: 0x001206E4 File Offset: 0x0011E8E4
		public static void ClearDirectoryRecursively(DirectoryInfo directory)
		{
			FileInfo[] files = directory.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
			foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
			{
				Tools.ClearDirectoryRecursively(directoryInfo);
				directoryInfo.Delete();
			}
		}

		// Token: 0x06008C14 RID: 35860 RVA: 0x00120734 File Offset: 0x0011E934
		public static float CalculateWeightedDistance(Vector3 a, Vector3 b, char priorityAxis)
		{
			bool flag = char.ToLower(priorityAxis) == 'x';
			bool isPriorityY = char.ToLower(priorityAxis) == 'y';
			bool isPriorityZ = char.ToLower(priorityAxis) == 'z';
			float deltaX = Mathf.Abs(a.x - b.x);
			float deltaY = Mathf.Abs(a.y - b.y);
			float deltaZ = Mathf.Abs(a.z - b.z);
			if (flag)
			{
				deltaY *= 10f;
				deltaZ *= 10f;
			}
			else if (isPriorityY)
			{
				deltaX *= 10f;
				deltaZ *= 10f;
			}
			else if (isPriorityZ)
			{
				deltaX *= 10f;
				deltaY *= 10f;
			}
			return Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
		}

		// Token: 0x06008C15 RID: 35861 RVA: 0x001207F0 File Offset: 0x0011E9F0
		public static bool InLastRow(int index, int counts, int columes)
		{
			if (columes <= 0)
			{
				throw new ArgumentException("columes must be greater than 0");
			}
			int lastRowStartIndex = counts - counts % columes;
			if (counts % columes == 0)
			{
				lastRowStartIndex = counts - columes;
			}
			return index >= lastRowStartIndex;
		}

		// Token: 0x06008C16 RID: 35862 RVA: 0x00120824 File Offset: 0x0011EA24
		public static MemoryStream GetStream()
		{
			MemoryStream stream;
			if (Tools.streamPool.TryTake(out stream))
			{
				stream.SetLength(0L);
				return stream;
			}
			return new MemoryStream();
		}

		// Token: 0x06008C17 RID: 35863 RVA: 0x0012084E File Offset: 0x0011EA4E
		public static void ReturnStream(MemoryStream stream)
		{
			Tools.streamPool.Add(stream);
		}

		// Token: 0x06008C18 RID: 35864 RVA: 0x0012085B File Offset: 0x0011EA5B
		public static string FormatPlatformUrl(string filePath)
		{
			return "file://" + filePath.Replace("\\", "/");
		}

		// Token: 0x06008C19 RID: 35865 RVA: 0x00120877 File Offset: 0x0011EA77
		public static string GetPlatformPath(string path)
		{
			return Path.Combine(Environment.CurrentDirectory, path);
		}

		// Token: 0x06008C1A RID: 35866 RVA: 0x00120884 File Offset: 0x0011EA84
		public static string FormatJsonString(string jsonString)
		{
			string text;
			try
			{
				text = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(jsonString), Formatting.Indented);
			}
			catch
			{
				text = jsonString;
			}
			return text;
		}

		// Token: 0x06008C1B RID: 35867 RVA: 0x001208B8 File Offset: 0x0011EAB8
		public static float GetScreenAspectRatio()
		{
			return (float)Screen.width / (float)Screen.height;
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x001208C7 File Offset: 0x0011EAC7
		public static bool IsAspectRatioWidescreen()
		{
			return Tools.GetScreenAspectRatio() >= 1.77f;
		}

		// Token: 0x0400C9B9 RID: 51641
		private static readonly ConcurrentBag<MemoryStream> streamPool = new ConcurrentBag<MemoryStream>();
	}
}
