using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using AssetStudio;
using MDPro3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class AssetBundleRobber : MonoBehaviour
{
	// Token: 0x060001B7 RID: 439 RVA: 0x00002A36 File Offset: 0x00000C36
	public static void SetHint(string hint)
	{
		if (AssetBundleRobber.sText == null)
		{
			AssetBundleRobber.sText = global::UnityEngine.GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
		}
		AssetBundleRobber.sText.text = hint;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00002A70 File Offset: 0x00000C70
	private void Start()
	{
		AssetBundleRobber.sText = this.text;
		this.assetManager = base.GetComponent<global::AssetStudio.AssetsManager>();
		Application.targetFrameRate = 0;
		if (this.mode_Android)
		{
			this.pathAB = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/Android/0000/";
			this.pathStore = "Platforms/Android/Robber/";
		}
		else if (this.mode_IOS)
		{
			this.pathAB = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/iOS/0000/";
			this.pathStore = "Platforms/iOS/Robber/";
		}
		else if (this.mode_Swtich)
		{
			this.pathAB = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/Swtich/";
			this.pathStore = "Platforms/Switch/Robber/";
		}
		else
		{
			this.pathAB = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/16165626/0000/";
			this.pathStore = "Platforms/StandaloneWindows64/Robber/";
		}
		this.copyAssetType = AssetBundleRobber.AssetType.All;
		this.Initialize();
		base.StartCoroutine(this.RefreshFileResources());
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00002B2A File Offset: 0x00000D2A
	public void CopyAB()
	{
		this.Copy(this.input.text);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00002B40 File Offset: 0x00000D40
	private void Copy(string path)
	{
		foreach (AssetBundleRobber.AssetbundleInfo file in AssetBundleRobber.files)
		{
			if (file.path == path)
			{
				Directory.CreateDirectory(this.pathStore + path);
				File.Copy(this.GetFullPath(file.path), this.pathStore + path + "/" + file.path);
				foreach (string dep in file.dependencies)
				{
					File.Copy(this.GetFullPath(dep), this.pathStore + path + "/" + dep);
				}
			}
		}
		Debug.Log(path + ": Copy Done!");
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00002C48 File Offset: 0x00000E48
	private void Initialize()
	{
		if (!Directory.Exists(this.pathStore))
		{
			Directory.CreateDirectory(this.pathStore);
		}
		string fullText = "";
		if (File.Exists(this.pathStore + "FileList.txt"))
		{
			fullText = File.ReadAllText(this.pathStore + "FileList.txt");
		}
		else
		{
			this.noSave = true;
		}
		string[] array = fullText.Replace("\r", "").Split('\n', StringSplitOptions.None);
		AssetBundleRobber.AssetbundleInfo file = default(AssetBundleRobber.AssetbundleInfo);
		file.dependencies = new List<string>();
		foreach (string line in array)
		{
			if (!line.StartsWith("-"))
			{
				if (file.name != null)
				{
					AssetBundleRobber.files.Add(file);
					file = new AssetBundleRobber.AssetbundleInfo
					{
						dependencies = new List<string>(),
						path = line
					};
				}
				else
				{
					file.path = line;
				}
			}
			else if (line.StartsWith("--"))
			{
				file.dependencies.Add(line.Replace("--", ""));
			}
			else
			{
				file.name = line.Replace("-", "");
			}
		}
		if (!this.noSave)
		{
			Debug.Log("Preloged：" + AssetBundleRobber.files.Count.ToString());
			return;
		}
		Debug.Log("No FileList to load.");
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00002DB8 File Offset: 0x00000FB8
	private void AddLog(int i)
	{
		SerializedFile file = this.assetManager.assetsFileList[i];
		this.count++;
		string filePath = file.originalPath.Substring(file.originalPath.Length - 8);
		string fileName = "";
		foreach (global::AssetStudio.Object @object in file.Objects)
		{
			global::AssetStudio.AssetBundle assetBundle = @object as global::AssetStudio.AssetBundle;
			if (assetBundle != null)
			{
				KeyValuePair<string, AssetInfo>[] container = assetBundle.m_Container;
				int num = 0;
				if (num < container.Length)
				{
					KeyValuePair<string, AssetInfo> pair = container[num];
					fileName = pair.Key;
				}
			}
		}
		AssetBundleRobber.AssetbundleInfo fileStruct = default(AssetBundleRobber.AssetbundleInfo);
		fileStruct.path = filePath;
		fileStruct.name = fileName;
		fileStruct.dependencies = this.GetDependencies(filePath, null);
		object @lock = this._lock;
		lock (@lock)
		{
			AssetBundleRobber.files.Add(fileStruct);
			AssetBundleRobber.newFiles.Add(fileStruct);
		}
		string content = string.Empty;
		content = content + fileStruct.path + "\r\n";
		content = content + "-" + fileStruct.name + "\r\n";
		foreach (string depend in fileStruct.dependencies)
		{
			content = content + "--" + depend + "\r\n";
		}
		this.logQueue.Enqueue(content);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00002F78 File Offset: 0x00001178
	private IEnumerator RefreshFileResources()
	{
		IEnumerator ie = this.assetManager.LoadFolderAsync(this.pathAB);
		base.StartCoroutine(ie);
		while (ie.MoveNext())
		{
			yield return null;
		}
		Debug.Log("new files: " + this.assetManager.assetsFileList.Count.ToString());
		this.indexQueue = new ConcurrentQueue<int>();
		this.workerThreads = new List<Thread>();
		this.isProcessing = true;
		for (int i = 0; i < this.threads; i++)
		{
			Thread workerThread = new Thread(new ThreadStart(this.ProcessLogs));
			this.workerThreads.Add(workerThread);
			workerThread.Start();
		}
		ie = this.EnqueueLogs(this.assetManager.assetsFileList.Count);
		while (ie.MoveNext())
		{
			this.text.text = "Logging: " + this.count.ToString() + "/" + this.assetManager.assetsFileList.Count.ToString();
			string log;
			if (this.logQueue.TryDequeue(out log))
			{
				File.AppendAllText(this.pathStore + "FileList.txt", log);
			}
			yield return null;
		}
		string log2;
		while (this.logQueue.TryDequeue(out log2))
		{
			File.AppendAllText(this.pathStore + "FileList.txt", log2);
			this.text.text = "Writing Left: " + this.logQueue.Count.ToString();
			yield return null;
		}
		base.StartCoroutine(this.CopyBundles());
		yield break;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00002F88 File Offset: 0x00001188
	public void StopProcessingLogs()
	{
		this.isProcessing = false;
		foreach (Thread thread in this.workerThreads)
		{
			thread.Join();
		}
		this.workerThreads.Clear();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00002FEC File Offset: 0x000011EC
	private IEnumerator EnqueueLogs(int count)
	{
		int processedIndexCount = 0;
		int indexesPerFrame = 32;
		int num;
		for (int i = 0; i < count; i = num + 1)
		{
			this.indexQueue.Enqueue(i);
			num = processedIndexCount;
			processedIndexCount = num + 1;
			if (processedIndexCount >= indexesPerFrame)
			{
				processedIndexCount = 0;
				yield return null;
			}
			num = i;
		}
		while (!this.indexQueue.IsEmpty)
		{
			yield return null;
		}
		this.StopProcessingLogs();
		yield break;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00003004 File Offset: 0x00001204
	private void ProcessLogs()
	{
		while (this.isProcessing)
		{
			int index;
			if (this.indexQueue.TryDequeue(out index))
			{
				this.AddLog(index);
			}
			else
			{
				Thread.Sleep(10);
			}
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000303A File Offset: 0x0000123A
	public void OnApplicationQuit()
	{
		this.StopProcessingLogs();
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00003042 File Offset: 0x00001242
	private IEnumerator CopyBundles()
	{
		AssetBundleRobber.fileCount = AssetBundleRobber.files.Count;
		AssetBundleRobber.currentFileCount = 0;
		List<AssetBundleRobber.AssetbundleInfo> targetFiles;
		if (this.fullCopy)
		{
			targetFiles = AssetBundleRobber.files;
		}
		else
		{
			targetFiles = AssetBundleRobber.newFiles;
		}
		foreach (AssetBundleRobber.AssetbundleInfo file in targetFiles)
		{
			AssetBundleRobber.currentFileCount++;
			AssetBundleRobber.AssetType type = this.GetAssetType(file.name);
			if ((this.copyAssetType == AssetBundleRobber.AssetType.All || type == this.copyAssetType) && (this.pathStore.Contains("Windows") || !this.AssetIsWindowsOnly(type)))
			{
				if (type == AssetBundleRobber.AssetType.AvatarStand)
				{
					if (!Directory.Exists(this.pathStore + "AvatarStand"))
					{
						Directory.CreateDirectory(this.pathStore + "AvatarStand");
					}
					string targetName = this.pathStore + "AvatarStand/" + Path.GetFileName(file.name).Replace(".prefab", string.Empty).Replace("avatarstand_", "AvatarStand_");
					if (!File.Exists(targetName))
					{
						File.Copy(this.GetFullPath(file.path), targetName, true);
					}
				}
				else if (type == AssetBundleRobber.AssetType.FrameMat)
				{
					if (!Directory.Exists(this.pathStore + "Frame"))
					{
						Directory.CreateDirectory(this.pathStore + "Frame");
					}
					string targetName2 = this.pathStore + "Frame/" + Path.GetFileName(file.name).Replace(".mat", string.Empty).Replace("profileframemat", "ProfileFrameMat");
					if (!File.Exists(targetName2))
					{
						File.Copy(this.GetFullPath(file.path), targetName2, true);
					}
				}
				else if (type == AssetBundleRobber.AssetType.Grave)
				{
					if (!Directory.Exists(this.pathStore + "Grave"))
					{
						Directory.CreateDirectory(this.pathStore + "Grave");
					}
					string targetName3 = this.pathStore + "Grave/" + Path.GetFileName(file.name).Replace(".prefab", string.Empty).Replace("grave_", "Grave_");
					if (!File.Exists(targetName3))
					{
						File.Copy(this.GetFullPath(file.path), targetName3, true);
					}
				}
				else if (type == AssetBundleRobber.AssetType.Mat)
				{
					if (!Directory.Exists(this.pathStore + "Mat"))
					{
						Directory.CreateDirectory(this.pathStore + "Mat");
					}
					string targetName4 = this.pathStore + "Mat/" + Path.GetFileName(file.name).Replace(".prefab", string.Empty).Replace("mat_", "Mat_");
					if (!File.Exists(targetName4))
					{
						File.Copy(this.GetFullPath(file.path), targetName4, true);
					}
				}
				else
				{
					if (type == AssetBundleRobber.AssetType.Mate)
					{
						if (!Directory.Exists(this.pathStore + "Mate"))
						{
							Directory.CreateDirectory(this.pathStore + "Mate");
						}
						string targetName5 = this.pathStore + "Mate/" + Path.GetFileName(file.name).Replace(".prefab", string.Empty).Replace("_model", "_Model")
							.Replace("_sd_", "_SD_")
							.Replace("m", "M")
							.Replace("v", "V");
						if (file.dependencies.Count == 0)
						{
							if (!File.Exists(targetName5))
							{
								File.Copy(this.GetFullPath(file.path), targetName5, true);
								goto IL_1A77;
							}
							goto IL_1A77;
						}
						else
						{
							string targetFolder = targetName5;
							if (!Directory.Exists(targetFolder))
							{
								Directory.CreateDirectory(targetFolder);
							}
							File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder, file.path), true);
							using (List<string>.Enumerator enumerator2 = file.dependencies.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen)))
									{
										if (!File.Exists(targetFolder + "/" + depen))
										{
											File.Copy(this.GetFullPath(depen), targetFolder + "/" + depen, true);
										}
									}
									else
									{
										Debug.Log(string.Concat(new string[]
										{
											"未找到",
											file.path,
											"的依赖：",
											depen,
											": ",
											this.GetFullPath(depen)
										}));
									}
								}
								goto IL_1A77;
							}
						}
					}
					if (type == AssetBundleRobber.AssetType.Protector)
					{
						if (!Directory.Exists(this.pathStore + "Protector"))
						{
							Directory.CreateDirectory(this.pathStore + "Protector");
						}
						string subDir = "107" + Regex.Split(file.name, "/")[4];
						string targetFolder2 = this.pathStore + "Protector/" + subDir;
						string targetName6 = targetFolder2 + "/" + Path.GetFileName(file.name).Replace("pmat.mat", subDir).Replace("protectoricon", "ProtectorIcon")
							.Replace(".png", string.Empty);
						if (!Directory.Exists(targetFolder2))
						{
							Directory.CreateDirectory(targetFolder2);
						}
						if (!File.Exists(targetName6))
						{
							File.Copy(this.GetFullPath(file.path), targetName6, true);
						}
					}
					else
					{
						if (type == AssetBundleRobber.AssetType.Wallpaper)
						{
							if (!Directory.Exists(this.pathStore + "Wallpaper"))
							{
								Directory.CreateDirectory(this.pathStore + "Wallpaper");
							}
							string subDir2 = Path.GetFileName(file.name).Replace(".prefab", string.Empty).Replace("front", "Front");
							string targetFolder3 = this.pathStore + "Wallpaper/" + subDir2;
							if (!Directory.Exists(targetFolder3))
							{
								Directory.CreateDirectory(targetFolder3);
							}
							File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder3, file.path), true);
							using (List<string>.Enumerator enumerator2 = new List<string>(file.dependencies).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen2 = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen2)))
									{
										File.Copy(this.GetFullPath(depen2), targetFolder3 + "/" + depen2, true);
									}
									else
									{
										Debug.Log("未找到" + file.path + "的依赖：" + depen2);
									}
								}
								goto IL_1A77;
							}
						}
						if (type == AssetBundleRobber.AssetType.Background)
						{
							if (!Directory.Exists(this.pathStore + "Background"))
							{
								Directory.CreateDirectory(this.pathStore + "Background");
							}
							string subDir3 = Path.GetFileName(file.name).Replace("back", "Back").Replace(".prefab", "");
							string targetFolder4 = this.pathStore + "Background/" + subDir3;
							if (!Directory.Exists(targetFolder4))
							{
								Directory.CreateDirectory(targetFolder4);
							}
							if (!File.Exists(targetFolder4 + "/" + subDir3))
							{
								File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder4, file.path), true);
							}
							else
							{
								Debug.LogError("Background File " + file.path + " already exist.");
							}
							using (List<string>.Enumerator enumerator2 = new List<string>(file.dependencies).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen3 = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen3)))
									{
										File.Copy(this.GetFullPath(depen3), targetFolder4 + "/" + depen3, true);
									}
									else
									{
										Debug.LogError("未找到" + file.path + "的依赖：" + depen3);
									}
								}
								goto IL_1A77;
							}
						}
						if (type == AssetBundleRobber.AssetType.Card)
						{
							if (!Directory.Exists(this.pathStore + "Card"))
							{
								Directory.CreateDirectory(this.pathStore + "Card");
							}
							string subDir4 = int.Parse(Regex.Split(file.name, "/")[6].Replace("ef", string.Empty)).ToString();
							subDir4 = this.GetYdkID(subDir4);
							if (file.name.Contains("/highend_hd/"))
							{
								subDir4 = "HD" + subDir4;
							}
							else if (file.name.Contains("/sd/"))
							{
								subDir4 = "SD" + subDir4;
							}
							string targetFolder5 = this.pathStore + "Card/" + subDir4;
							if (!Directory.Exists(targetFolder5))
							{
								Directory.CreateDirectory(targetFolder5);
							}
							File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder5, file.path), true);
							using (List<string>.Enumerator enumerator2 = new List<string>(file.dependencies).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen4 = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen4)))
									{
										if (!File.Exists(targetFolder5 + "/" + depen4))
										{
											File.Copy(this.GetFullPath(depen4), targetFolder5 + "/" + depen4, true);
										}
									}
									else
									{
										Debug.LogError("未找到 " + file.path + " 的依赖：" + depen4);
									}
								}
								goto IL_1A77;
							}
						}
						if (type == AssetBundleRobber.AssetType.MonsterCutin)
						{
							if (file.name.Contains("/sd/") && this.pathStore.Contains("Windows"))
							{
								continue;
							}
							if (!Directory.Exists(this.pathStore + "MonsterCutin"))
							{
								Directory.CreateDirectory(this.pathStore + "MonsterCutin");
							}
							string subDir5 = Regex.Split(file.name, "/")[7].Replace("p", "");
							subDir5 = this.GetYdkID(subDir5);
							string targetFolder6 = this.pathStore + "MonsterCutin/" + subDir5;
							if (!Directory.Exists(targetFolder6))
							{
								Directory.CreateDirectory(targetFolder6);
							}
							File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder6, file.path), true);
							using (List<string>.Enumerator enumerator2 = new List<string>(file.dependencies).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen5 = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen5)))
									{
										if (!File.Exists(targetFolder6 + "/" + depen5))
										{
											File.Copy(this.GetFullPath(depen5), targetFolder6 + "/" + depen5, true);
										}
									}
									else
									{
										Debug.LogError("未找到 " + file.path + " 的依赖：" + depen5);
									}
								}
								goto IL_1A77;
							}
						}
						if (type == AssetBundleRobber.AssetType.SpecialWin)
						{
							if (file.name.Contains("/sd/") && this.pathStore.Contains("Windows"))
							{
								continue;
							}
							if (!Directory.Exists(this.pathStore + "SpecialWin"))
							{
								Directory.CreateDirectory(this.pathStore + "SpecialWin");
							}
							string subDir6 = Regex.Split(file.name, "/")[8];
							if (subDir6.Contains(".prefab"))
							{
								subDir6 = subDir6.Replace(".prefab", string.Empty).Replace("summonspecialwin", string.Empty);
							}
							else
							{
								subDir6 = subDir6.Replace("p", string.Empty);
							}
							subDir6 = this.GetYdkID(subDir6);
							string targetFolder7 = this.pathStore + "SpecialWin/" + subDir6;
							if (!Directory.Exists(targetFolder7))
							{
								Directory.CreateDirectory(targetFolder7);
							}
							File.Copy(this.GetFullPath(file.path), Path.Combine(targetFolder7, file.path), true);
							using (List<string>.Enumerator enumerator2 = new List<string>(file.dependencies).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string depen6 = enumerator2.Current;
									if (File.Exists(this.GetFullPath(depen6)))
									{
										if (!File.Exists(targetFolder7 + "/" + depen6))
										{
											File.Copy(this.GetFullPath(depen6), targetFolder7 + "/" + depen6, true);
										}
									}
									else
									{
										Debug.LogError("未找到 " + file.path + " 的依赖：" + depen6);
									}
								}
								goto IL_1A77;
							}
						}
						if (type == AssetBundleRobber.AssetType.BGM)
						{
							if (!Directory.Exists(this.pathStore + "Sound/BGM"))
							{
								Directory.CreateDirectory(this.pathStore + "Sound/BGM");
							}
							string targetName7 = this.pathStore + "Sound/BGM/" + Path.GetFileName(file.name).Replace(".wav", string.Empty);
							File.Copy(this.GetFullPath(file.path), targetName7, true);
						}
						else if (type == AssetBundleRobber.AssetType.SE_DUEL)
						{
							if (!Directory.Exists(this.pathStore + "Sound/SE_DUEL"))
							{
								Directory.CreateDirectory(this.pathStore + "Sound/SE_DUEL");
							}
							string targetName8 = this.pathStore + "Sound/SE_DUEL/" + Path.GetFileName(file.name).Replace(".wav", string.Empty);
							File.Copy(this.GetFullPath(file.path), targetName8, true);
						}
						else if (type == AssetBundleRobber.AssetType.SE_FIELD)
						{
							if (!Directory.Exists(this.pathStore + "Sound/SE_FIELD"))
							{
								Directory.CreateDirectory(this.pathStore + "Sound/SE_FIELD");
							}
							string targetName9 = this.pathStore + "Sound/SE_FIELD/" + Path.GetFileName(file.name).Replace(".wav", string.Empty);
							File.Copy(this.GetFullPath(file.path), targetName9, true);
						}
						else if (type == AssetBundleRobber.AssetType.SE_MATE)
						{
							if (!Directory.Exists(this.pathStore + "Sound/SE_MATE"))
							{
								Directory.CreateDirectory(this.pathStore + "Sound/SE_MATE");
							}
							string targetName10 = this.pathStore + "Sound/SE_MATE/" + Path.GetFileName(file.name).Replace(".wav", string.Empty);
							File.Copy(this.GetFullPath(file.path), targetName10, true);
						}
						else
						{
							if (type == AssetBundleRobber.AssetType.SE_SYS)
							{
								if (!Directory.Exists(this.pathStore + "Sound/SE_SYS"))
								{
									Directory.CreateDirectory(this.pathStore + "Sound/SE_SYS");
								}
								string targetName11 = this.pathStore + "Sound/SE_SYS/" + Path.GetFileName(file.name).Replace(".wav", string.Empty);
								try
								{
									File.Copy(this.GetFullPath(file.path), targetName11, true);
									goto IL_1A77;
								}
								catch (Exception ex)
								{
									Debug.LogException(ex);
									goto IL_1A77;
								}
							}
							if (type == AssetBundleRobber.AssetType.AvatarStandIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/AvatarBase/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/AvatarBase/SD");
								}
								string targetName12 = this.pathStore + "Icon/AvatarBase/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldavatarbaseicon", "FieldAvatarBaseIcon");
								if (!File.Exists(targetName12))
								{
									File.Copy(this.GetFullPath(file.path), targetName12, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.AvatarStandIconHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/AvatarBase/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/AvatarBase/HD");
								}
								string targetName13 = this.pathStore + "Icon/AvatarBase/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldavatarbaseicon", "FieldAvatarBaseIcon");
								if (!File.Exists(targetName13))
								{
									File.Copy(this.GetFullPath(file.path), targetName13, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.DeckCaseIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/DeckCase/S"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/DeckCase/S");
								}
								string targetName14 = this.pathStore + "Icon/DeckCase/S/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("deckcase", "DeckCase");
								if (!File.Exists(targetName14))
								{
									File.Copy(this.GetFullPath(file.path), targetName14, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.DeckCaseL || type == AssetBundleRobber.AssetType.DeckCaseOpen || type == AssetBundleRobber.AssetType.DeckCaseReverse)
							{
								if (!Directory.Exists(this.pathStore + "Icon/DeckCase/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/DeckCase/SD");
								}
								string targetName15 = this.pathStore + "Icon/DeckCase/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("deckcase", "DeckCase")
									.Replace("_open", "_Open")
									.Replace("_l", "_L");
								if (!File.Exists(targetName15))
								{
									File.Copy(this.GetFullPath(file.path), targetName15, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.DeckCaseLHD || type == AssetBundleRobber.AssetType.DeckCaseOpenHD || type == AssetBundleRobber.AssetType.DeckCaseReverseHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/DeckCase/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/DeckCase/HD");
								}
								string targetName16 = this.pathStore + "Icon/DeckCase/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("deckcase", "DeckCase")
									.Replace("_open", "_Open")
									.Replace("_l", "_L");
								if (!File.Exists(targetName16))
								{
									File.Copy(this.GetFullPath(file.path), targetName16, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.MatIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Field/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Field/SD");
								}
								string targetName17 = this.pathStore + "Icon/Field/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldicon", "FieldIcon");
								if (!File.Exists(targetName17))
								{
									File.Copy(this.GetFullPath(file.path), targetName17, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.MatIconHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Field/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Field/HD");
								}
								string targetName18 = this.pathStore + "Icon/Field/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldicon", "FieldIcon");
								if (!File.Exists(targetName18))
								{
									File.Copy(this.GetFullPath(file.path), targetName18, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.GraveIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Grave/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Grave/SD");
								}
								string targetName19 = this.pathStore + "Icon/Grave/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldobjicon", "FieldObjIcon");
								if (!File.Exists(targetName19))
								{
									File.Copy(this.GetFullPath(file.path), targetName19, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.GraveIconHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Grave/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Grave/HD");
								}
								string targetName20 = this.pathStore + "Icon/Grave/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("fieldobjicon", "FieldObjIcon");
								if (!File.Exists(targetName20))
								{
									File.Copy(this.GetFullPath(file.path), targetName20, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.MateIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Mate"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Mate");
								}
								string targetName21 = this.pathStore + "Icon/Mate/" + Path.GetFileName(file.name).Replace(".png", string.Empty);
								if (!File.Exists(targetName21))
								{
									File.Copy(this.GetFullPath(file.path), targetName21, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.ProfileIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileIcon/S"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileIcon/S");
								}
								string targetName22 = this.pathStore + "Icon/ProfileIcon/S/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileicon", "ProfileIcon");
								if (!File.Exists(targetName22))
								{
									File.Copy(this.GetFullPath(file.path), targetName22, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.ProfileIconL)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileIcon/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileIcon/SD");
								}
								string targetName23 = this.pathStore + "Icon/ProfileIcon/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileicon", "ProfileIcon")
									.Replace("_l", "_L");
								if (!File.Exists(targetName23))
								{
									File.Copy(this.GetFullPath(file.path), targetName23, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.ProfileIconLHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileIcon/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileIcon/HD");
								}
								string targetName24 = this.pathStore + "Icon/ProfileIcon/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileicon", "ProfileIcon")
									.Replace("_l", "_L");
								if (!File.Exists(targetName24))
								{
									File.Copy(this.GetFullPath(file.path), targetName24, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.WallpaperIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/Wallpaper"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/Wallpaper");
								}
								string targetName25 = this.pathStore + "Icon/Wallpaper/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("wallpapericon", "WallPaperIcon");
								if (!File.Exists(targetName25))
								{
									File.Copy(this.GetFullPath(file.path), targetName25, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.FrameIcon)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileFrame/S"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileFrame/S");
								}
								string targetName26 = this.pathStore + "Icon/ProfileFrame/S/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileframe", "ProfileFrame");
								if (!File.Exists(targetName26))
								{
									File.Copy(this.GetFullPath(file.path), targetName26, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.FrameL)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileFrame/SD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileFrame/SD");
								}
								string targetName27 = this.pathStore + "Icon/ProfileFrame/SD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileframe", "ProfileFrame")
									.Replace("_l", "_L");
								if (!File.Exists(targetName27))
								{
									File.Copy(this.GetFullPath(file.path), targetName27, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.FrameLHD)
							{
								if (!Directory.Exists(this.pathStore + "Icon/ProfileFrame/HD"))
								{
									Directory.CreateDirectory(this.pathStore + "Icon/ProfileFrame/HD");
								}
								string targetName28 = this.pathStore + "Icon/ProfileFrame/HD/" + Path.GetFileName(file.name).Replace(".png", string.Empty).Replace("profileframe", "ProfileFrame")
									.Replace("_l", "_L");
								if (!File.Exists(targetName28))
								{
									File.Copy(this.GetFullPath(file.path), targetName28, true);
								}
							}
							else if (type == AssetBundleRobber.AssetType.XML)
							{
								if (!Directory.Exists(this.pathStore + "XML"))
								{
									Directory.CreateDirectory(this.pathStore + "XML");
								}
								string targetName29 = this.pathStore + "XML/" + Path.GetFileName(file.name).Replace(".xml", string.Empty);
								if (!File.Exists(targetName29))
								{
									File.Copy(this.GetFullPath(file.path), targetName29, true);
								}
							}
						}
					}
				}
				IL_1A77:
				this.text.text = "Copying: " + AssetBundleRobber.currentFileCount.ToString() + "/" + AssetBundleRobber.fileCount.ToString();
				yield return null;
			}
		}
		List<AssetBundleRobber.AssetbundleInfo>.Enumerator enumerator = default(List<AssetBundleRobber.AssetbundleInfo>.Enumerator);
		this.text.text = "Copy Complete.";
		yield break;
		yield break;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00003054 File Offset: 0x00001254
	private AssetBundleRobber.AssetType GetAssetType(string name)
	{
		if (!name.StartsWith("assets/resourcesassetbundle"))
		{
			return AssetBundleRobber.AssetType.None;
		}
		if (name.Contains("/duel/bg/avatarstand/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.AvatarStand;
			}
		}
		else if (name.Contains("/images/profileframe/"))
		{
			if (name.EndsWith(".mat"))
			{
				return AssetBundleRobber.AssetType.FrameMat;
			}
			if (name.EndsWith(".png"))
			{
				if (name.Contains("/highend_hd/"))
				{
					return AssetBundleRobber.AssetType.FrameLHD;
				}
				if (name.Contains("/sd/"))
				{
					return AssetBundleRobber.AssetType.FrameL;
				}
				return AssetBundleRobber.AssetType.FrameIcon;
			}
		}
		else if (name.Contains("/duel/bg/grave/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Grave;
			}
		}
		else if (name.Contains("/duel/bg/mat/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Mat;
			}
		}
		else if (name.Contains("/mate/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Mate;
			}
			if (name.EndsWith(".png"))
			{
				return AssetBundleRobber.AssetType.MateIcon;
			}
		}
		else if (name.Contains("/protector/"))
		{
			if (!name.Contains("/protector/shaders/"))
			{
				return AssetBundleRobber.AssetType.Protector;
			}
		}
		else if (name.Contains("/wallpaper/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Wallpaper;
			}
			if (name.EndsWith(".png") && name.Contains("wallpapericon"))
			{
				return AssetBundleRobber.AssetType.WallpaperIcon;
			}
		}
		else if (name.Contains("/prefabs/outgamebg/back/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Background;
			}
		}
		else if (name.Contains("/duel/timeline/card/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.Card;
			}
		}
		else if (name.Contains("/duel/timeline/duel/monstercutin/"))
		{
			if (name.EndsWith(".prefab"))
			{
				return AssetBundleRobber.AssetType.MonsterCutin;
			}
		}
		else if (name.Contains("/duel/timeline/duel/universal/summon/summonspecialwin/"))
		{
			if (name.EndsWith(".prefab") && Path.GetFileName(name).Contains("summonspecialwin"))
			{
				return AssetBundleRobber.AssetType.SpecialWin;
			}
		}
		else if (name.Contains("/bgm/"))
		{
			if (name.EndsWith(".wav"))
			{
				return AssetBundleRobber.AssetType.BGM;
			}
		}
		else if (name.Contains("/se_duel/"))
		{
			if (name.EndsWith(".wav"))
			{
				return AssetBundleRobber.AssetType.SE_DUEL;
			}
		}
		else if (name.Contains("/se_field/"))
		{
			if (name.EndsWith(".wav"))
			{
				return AssetBundleRobber.AssetType.SE_FIELD;
			}
		}
		else if (name.Contains("/se_mate/"))
		{
			if (name.EndsWith(".wav"))
			{
				return AssetBundleRobber.AssetType.SE_MATE;
			}
		}
		else if (name.Contains("/se_sys/"))
		{
			if (name.EndsWith(".wav"))
			{
				return AssetBundleRobber.AssetType.SE_SYS;
			}
		}
		else if (name.StartsWith("assets/resourcesassetbundle/images/fieldavatarbase/"))
		{
			if (name.EndsWith(".png"))
			{
				if (name.Contains("/highend_hd/"))
				{
					return AssetBundleRobber.AssetType.AvatarStandIconHD;
				}
				if (name.Contains("/sd/"))
				{
					return AssetBundleRobber.AssetType.AvatarStandIcon;
				}
			}
		}
		else if (name.StartsWith("assets/resourcesassetbundle/images/deckcase/"))
		{
			if (name.EndsWith(".png"))
			{
				if (name.Contains("/highend_hd/"))
				{
					if (name.EndsWith("_l_reverse.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseReverseHD;
					}
					if (name.EndsWith("_open_l.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseOpenHD;
					}
					if (name.EndsWith("_l.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseLHD;
					}
				}
				else
				{
					if (!name.Contains("/sd/"))
					{
						return AssetBundleRobber.AssetType.DeckCaseIcon;
					}
					if (name.EndsWith("_l_reverse.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseReverse;
					}
					if (name.EndsWith("_open_l.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseOpen;
					}
					if (name.EndsWith("_l.png"))
					{
						return AssetBundleRobber.AssetType.DeckCaseL;
					}
				}
			}
		}
		else if (name.StartsWith("assets/resourcesassetbundle/images/field/"))
		{
			if (name.EndsWith(".png"))
			{
				if (name.Contains("/highend_hd/"))
				{
					return AssetBundleRobber.AssetType.MatIconHD;
				}
				if (name.Contains("/sd/"))
				{
					return AssetBundleRobber.AssetType.MatIcon;
				}
			}
		}
		else if (name.StartsWith("assets/resourcesassetbundle/images/fieldobj/"))
		{
			if (name.EndsWith(".png"))
			{
				if (name.Contains("/highend_hd/"))
				{
					return AssetBundleRobber.AssetType.GraveIconHD;
				}
				if (name.Contains("/sd/"))
				{
					return AssetBundleRobber.AssetType.GraveIcon;
				}
			}
		}
		else if (name.StartsWith("assets/resourcesassetbundle/images/profileicon/"))
		{
			if (name.EndsWith(".png"))
			{
				if (name.EndsWith("_l.png") && name.Contains("/highend_hd/"))
				{
					return AssetBundleRobber.AssetType.ProfileIconLHD;
				}
				if (name.EndsWith("_l.png") && name.Contains("/sd/"))
				{
					return AssetBundleRobber.AssetType.ProfileIconL;
				}
				return AssetBundleRobber.AssetType.ProfileIcon;
			}
		}
		else if (name.StartsWith("/sound/xml/"))
		{
			return AssetBundleRobber.AssetType.XML;
		}
		return AssetBundleRobber.AssetType.None;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000034A8 File Offset: 0x000016A8
	private bool AssetIsWindowsOnly(AssetBundleRobber.AssetType type)
	{
		switch (type)
		{
		case AssetBundleRobber.AssetType.AvatarStandIcon:
			return true;
		case AssetBundleRobber.AssetType.AvatarStandIconHD:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseIcon:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseL:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseLHD:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseReverse:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseReverseHD:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseOpen:
			return true;
		case AssetBundleRobber.AssetType.DeckCaseOpenHD:
			return true;
		case AssetBundleRobber.AssetType.FrameIcon:
			return true;
		case AssetBundleRobber.AssetType.FrameL:
			return true;
		case AssetBundleRobber.AssetType.FrameLHD:
			return true;
		case AssetBundleRobber.AssetType.GraveIcon:
			return true;
		case AssetBundleRobber.AssetType.GraveIconHD:
			return true;
		case AssetBundleRobber.AssetType.MatIcon:
			return true;
		case AssetBundleRobber.AssetType.MatIconHD:
			return true;
		case AssetBundleRobber.AssetType.MateIcon:
			return true;
		case AssetBundleRobber.AssetType.ProfileIcon:
			return true;
		case AssetBundleRobber.AssetType.ProfileIconL:
			return true;
		case AssetBundleRobber.AssetType.ProfileIconLHD:
			return true;
		case AssetBundleRobber.AssetType.WallpaperIcon:
			return true;
		case AssetBundleRobber.AssetType.BGM:
			return true;
		case AssetBundleRobber.AssetType.SE_DUEL:
			return true;
		case AssetBundleRobber.AssetType.SE_FIELD:
			return true;
		case AssetBundleRobber.AssetType.SE_MATE:
			return true;
		case AssetBundleRobber.AssetType.SE_SYS:
			return true;
		case AssetBundleRobber.AssetType.XML:
			return true;
		}
		return false;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000035C4 File Offset: 0x000017C4
	private List<string> GetDependencies(string fileName, List<string> parentDepends = null)
	{
		byte[] bytes = this.Decompress(fileName);
		List<int> dependencyPositions = new List<int>();
		for (int i = 0; i < bytes.Length; i++)
		{
			if (bytes[i] == 47 && i + 9 < bytes.Length && bytes[i + 2] == bytes[i - 1] && bytes[i + 1] == bytes[i - 2])
			{
				bool check = true;
				if (bytes[i + 9] != 0)
				{
					check = false;
				}
				if (check)
				{
					for (int j = 1; j < 9; j++)
					{
						if ((bytes[i + j] < 48 || bytes[i + j] > 57) && (bytes[i + j] < 65 || bytes[i + j] > 90) && (bytes[i + j] < 97 || bytes[i + j] > 122))
						{
							check = false;
						}
					}
				}
				if (check)
				{
					dependencyPositions.Add(i);
				}
			}
		}
		List<string> dependencies = new List<string>();
		for (int k = 0; k < dependencyPositions.Count; k++)
		{
			List<byte> temp = new List<byte>();
			for (int l = dependencyPositions[k] + 1; l < dependencyPositions[k] + 9; l++)
			{
				temp.Add(bytes[l]);
			}
			string s = Encoding.UTF8.GetString(temp.ToArray());
			if (s != fileName)
			{
				if (parentDepends != null)
				{
					if (!parentDepends.Contains(s))
					{
						dependencies.Add(s);
					}
				}
				else
				{
					dependencies.Add(s);
				}
			}
		}
		List<string> newParentDepends = new List<string>(dependencies);
		if (parentDepends != null)
		{
			foreach (string dependency in parentDepends)
			{
				if (!newParentDepends.Contains(dependency))
				{
					newParentDepends.Add(dependency);
				}
			}
		}
		List<string> subdepends = new List<string>();
		foreach (string value in dependencies)
		{
			foreach (string s2 in this.GetDependencies(value, newParentDepends))
			{
				if (!subdepends.Contains(s2))
				{
					subdepends.Add(s2);
				}
			}
		}
		foreach (string value2 in subdepends)
		{
			if (!dependencies.Contains(value2))
			{
				dependencies.Add(value2);
			}
		}
		return dependencies;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000386C File Offset: 0x00001A6C
	private byte[] Decompress(string path)
	{
		AssetsTools.NET.Extra.AssetsManager manager = new AssetsTools.NET.Extra.AssetsManager();
		if (!File.Exists(this.GetFullPath(path)))
		{
			Debug.Log("Not Find: " + path);
			return new byte[0];
		}
		AssetBundleFile file = manager.LoadBundleFile(this.GetFullPath(path), false).file;
		MemoryStream bundleStream = new MemoryStream();
		file.Unpack(new AssetsFileWriter(bundleStream));
		return bundleStream.GetBuffer();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x000038D0 File Offset: 0x00001AD0
	private string GetYdkID(string mdID)
	{
		return Cid2Ydk.GetYDK(int.Parse(mdID)).ToString();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x000038F0 File Offset: 0x00001AF0
	private string GetFullPath(string path)
	{
		if (path.Length <= 2)
		{
			Debug.Log("Too short: " + path);
			return string.Empty;
		}
		return this.pathAB + path.Substring(0, 2) + "/" + path;
	}

	// Token: 0x04000225 RID: 549
	public Text text;

	// Token: 0x04000226 RID: 550
	private static Text sText;

	// Token: 0x04000227 RID: 551
	public TMP_InputField input;

	// Token: 0x04000228 RID: 552
	public bool mode_Window = true;

	// Token: 0x04000229 RID: 553
	public bool mode_Android;

	// Token: 0x0400022A RID: 554
	public bool mode_IOS;

	// Token: 0x0400022B RID: 555
	public bool mode_Swtich;

	// Token: 0x0400022C RID: 556
	public bool fullCopy;

	// Token: 0x0400022D RID: 557
	private string pathAB;

	// Token: 0x0400022E RID: 558
	private string pathStore;

	// Token: 0x0400022F RID: 559
	private const string PATH_AB_WINDOWS = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/16165626/0000/";

	// Token: 0x04000230 RID: 560
	private const string PATH_AB_ANDROID = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/Android/0000/";

	// Token: 0x04000231 RID: 561
	private const string PATH_AB_IOS = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/iOS/0000/";

	// Token: 0x04000232 RID: 562
	private const string PATH_AB_Swtich = "D:/Game/Steam/steamapps/common/Yu-Gi-Oh!  Master Duel/LocalData/Swtich/";

	// Token: 0x04000233 RID: 563
	private const string PATH_STORE_WINDOWS = "Platforms/StandaloneWindows64/Robber/";

	// Token: 0x04000234 RID: 564
	private const string PATH_STORE_ANDROID = "Platforms/Android/Robber/";

	// Token: 0x04000235 RID: 565
	private const string PATH_STORE_IOS = "Platforms/iOS/Robber/";

	// Token: 0x04000236 RID: 566
	private const string PATH_STORE_Swtich = "Platforms/Switch/Robber/";

	// Token: 0x04000237 RID: 567
	public static int fileCount;

	// Token: 0x04000238 RID: 568
	public static int currentFileCount;

	// Token: 0x04000239 RID: 569
	private readonly object _lock = new object();

	// Token: 0x0400023A RID: 570
	private bool noSave;

	// Token: 0x0400023B RID: 571
	private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

	// Token: 0x0400023C RID: 572
	private int count;

	// Token: 0x0400023D RID: 573
	private global::AssetStudio.AssetsManager assetManager;

	// Token: 0x0400023E RID: 574
	private readonly int threads = 32;

	// Token: 0x0400023F RID: 575
	private AssetBundleRobber.AssetType copyAssetType = AssetBundleRobber.AssetType.All;

	// Token: 0x04000240 RID: 576
	public static List<AssetBundleRobber.AssetbundleInfo> files = new List<AssetBundleRobber.AssetbundleInfo>();

	// Token: 0x04000241 RID: 577
	public static List<AssetBundleRobber.AssetbundleInfo> newFiles = new List<AssetBundleRobber.AssetbundleInfo>();

	// Token: 0x04000242 RID: 578
	private ConcurrentQueue<int> indexQueue;

	// Token: 0x04000243 RID: 579
	private List<Thread> workerThreads;

	// Token: 0x04000244 RID: 580
	private bool isProcessing;

	// Token: 0x04000245 RID: 581
	private const string prefix = "assets/resourcesassetbundle";

	// Token: 0x04000246 RID: 582
	private const string LABEL_SD = "/sd/";

	// Token: 0x04000247 RID: 583
	private const string LABEL_HD = "/highend_hd/";

	// Token: 0x04000248 RID: 584
	private const string EXTENSION_PNG = ".png";

	// Token: 0x04000249 RID: 585
	private const string EXTENSION_PREFAB = ".prefab";

	// Token: 0x0400024A RID: 586
	private const string EXTENSION_WAV = ".wav";

	// Token: 0x0400024B RID: 587
	private const string EXTENSION_MAT = ".mat";

	// Token: 0x0400024C RID: 588
	private const string EXTENSION_XML = ".xml";

	// Token: 0x02000061 RID: 97
	public struct AssetbundleInfo
	{
		// Token: 0x0400024D RID: 589
		public string path;

		// Token: 0x0400024E RID: 590
		public string name;

		// Token: 0x0400024F RID: 591
		public List<string> dependencies;
	}

	// Token: 0x02000062 RID: 98
	public enum AssetType
	{
		// Token: 0x04000251 RID: 593
		None,
		// Token: 0x04000252 RID: 594
		All,
		// Token: 0x04000253 RID: 595
		AvatarStand,
		// Token: 0x04000254 RID: 596
		AvatarStandIcon,
		// Token: 0x04000255 RID: 597
		AvatarStandIconHD,
		// Token: 0x04000256 RID: 598
		Card,
		// Token: 0x04000257 RID: 599
		DeckCaseIcon,
		// Token: 0x04000258 RID: 600
		DeckCaseL,
		// Token: 0x04000259 RID: 601
		DeckCaseLHD,
		// Token: 0x0400025A RID: 602
		DeckCaseReverse,
		// Token: 0x0400025B RID: 603
		DeckCaseReverseHD,
		// Token: 0x0400025C RID: 604
		DeckCaseOpen,
		// Token: 0x0400025D RID: 605
		DeckCaseOpenHD,
		// Token: 0x0400025E RID: 606
		FrameIcon,
		// Token: 0x0400025F RID: 607
		FrameL,
		// Token: 0x04000260 RID: 608
		FrameLHD,
		// Token: 0x04000261 RID: 609
		FrameMat,
		// Token: 0x04000262 RID: 610
		Grave,
		// Token: 0x04000263 RID: 611
		GraveIcon,
		// Token: 0x04000264 RID: 612
		GraveIconHD,
		// Token: 0x04000265 RID: 613
		Mat,
		// Token: 0x04000266 RID: 614
		MatIcon,
		// Token: 0x04000267 RID: 615
		MatIconHD,
		// Token: 0x04000268 RID: 616
		Mate,
		// Token: 0x04000269 RID: 617
		MateIcon,
		// Token: 0x0400026A RID: 618
		ProfileIcon,
		// Token: 0x0400026B RID: 619
		ProfileIconL,
		// Token: 0x0400026C RID: 620
		ProfileIconLHD,
		// Token: 0x0400026D RID: 621
		MonsterCutin,
		// Token: 0x0400026E RID: 622
		Protector,
		// Token: 0x0400026F RID: 623
		Wallpaper,
		// Token: 0x04000270 RID: 624
		WallpaperIcon,
		// Token: 0x04000271 RID: 625
		Background,
		// Token: 0x04000272 RID: 626
		SpecialWin,
		// Token: 0x04000273 RID: 627
		BGM,
		// Token: 0x04000274 RID: 628
		SE_DUEL,
		// Token: 0x04000275 RID: 629
		SE_FIELD,
		// Token: 0x04000276 RID: 630
		SE_MATE,
		// Token: 0x04000277 RID: 631
		SE_SYS,
		// Token: 0x04000278 RID: 632
		XML
	}
}
