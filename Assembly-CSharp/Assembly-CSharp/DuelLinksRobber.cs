using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AssetStudio;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000069 RID: 105
public class DuelLinksRobber : MonoBehaviour
{
	// Token: 0x060001EB RID: 491 RVA: 0x00005F44 File Offset: 0x00004144
	private void Start()
	{
		this.assetManager = base.GetComponent<AssetsManager>();
		Application.targetFrameRate = 0;
		this.fullCopy = false;
		this.Initialize();
		base.StartCoroutine(this.RefreshFileResources());
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00005F74 File Offset: 0x00004174
	private void Initialize()
	{
		if (!Directory.Exists(this.workingPlace))
		{
			Directory.CreateDirectory(this.workingPlace);
		}
		string fullText = "";
		if (File.Exists(this.workingPlace + this.fileList))
		{
			fullText = File.ReadAllText(this.workingPlace + this.fileList);
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
					DuelLinksRobber.files.Add(file);
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
				file.dependencies.Add(line.Substring(2));
			}
			else
			{
				file.name = line.Substring(1);
			}
		}
		if (!this.noSave)
		{
			Debug.Log("Preloged：" + DuelLinksRobber.files.Count.ToString());
			return;
		}
		Debug.Log("No FileList to load.");
	}

	// Token: 0x060001ED RID: 493 RVA: 0x000060D4 File Offset: 0x000042D4
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
		AssetBundleRobber.AssetbundleInfo filestruct = default(AssetBundleRobber.AssetbundleInfo);
		filestruct.path = filePath;
		filestruct.name = fileName;
		filestruct.dependencies = new List<string>();
		object @lock = this._lock;
		lock (@lock)
		{
			DuelLinksRobber.files.Add(filestruct);
			DuelLinksRobber.newFiles.Add(filestruct);
		}
		string content = string.Empty;
		content = content + filestruct.path + "\r\n";
		content = content + "-" + filestruct.name + "\r\n";
		foreach (string depend in filestruct.dependencies)
		{
			content = content + "--" + depend + "\r\n";
		}
		this.logQueue.Enqueue(content);
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00006290 File Offset: 0x00004490
	private IEnumerator RefreshFileResources()
	{
		IEnumerator ie = this.assetManager.LoadFolderAsync(this.duelLinksStreamingAssetsPath);
		while (ie.MoveNext())
		{
			yield return null;
		}
		ie = this.assetManager.LoadFolderAsync(this.duelLinksLocalDataPath);
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
				File.AppendAllText(this.workingPlace + this.fileList, log);
			}
			yield return null;
		}
		string log2;
		while (this.logQueue.TryDequeue(out log2))
		{
			File.AppendAllText(this.workingPlace + this.fileList, log2);
			this.text.text = "Writing Left: " + this.logQueue.Count.ToString();
			yield return null;
		}
		base.StartCoroutine(this.CopyBundles());
		yield break;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x000062A0 File Offset: 0x000044A0
	public void StopProcessingLogs()
	{
		this.isProcessing = false;
		foreach (Thread thread in this.workerThreads)
		{
			thread.Join();
		}
		this.workerThreads.Clear();
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00006304 File Offset: 0x00004504
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

	// Token: 0x060001F1 RID: 497 RVA: 0x0000631C File Offset: 0x0000451C
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

	// Token: 0x060001F2 RID: 498 RVA: 0x00006352 File Offset: 0x00004552
	public void OnApplicationQuit()
	{
		this.StopProcessingLogs();
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000635A File Offset: 0x0000455A
	private IEnumerator CopyBundles()
	{
		AssetBundleRobber.fileCount = DuelLinksRobber.files.Count;
		AssetBundleRobber.currentFileCount = 0;
		Debug.Log(AssetBundleRobber.fileCount);
		List<AssetBundleRobber.AssetbundleInfo> targetFiles;
		if (this.fullCopy)
		{
			targetFiles = DuelLinksRobber.files;
		}
		else
		{
			targetFiles = DuelLinksRobber.newFiles;
		}
		string storePath = this.workingPlace + "VoiceAB/";
		if (!Directory.Exists(storePath))
		{
			Directory.CreateDirectory(storePath);
		}
		foreach (AssetBundleRobber.AssetbundleInfo file in targetFiles)
		{
			yield return null;
			AssetBundleRobber.currentFileCount++;
			this.text.text = "Copy: " + AssetBundleRobber.currentFileCount.ToString() + "/" + AssetBundleRobber.fileCount.ToString();
			if (file.name.Contains("ja-jp") && file.name.EndsWith(".wav"))
			{
				string filePath = this.duelLinksStreamingAssetsPath + file.path.Substring(0, 2) + "/" + file.path;
				if (!File.Exists(filePath))
				{
					filePath = this.duelLinksLocalDataPath + file.path.Substring(0, 2) + "/" + file.path;
				}
				if (!File.Exists(filePath))
				{
					Debug.Log("File not found: " + file.path);
					continue;
				}
				if (File.Exists(storePath + file.path))
				{
					File.Delete(storePath + file.path);
				}
				File.Copy(filePath, storePath + file.path);
			}
			file = default(AssetBundleRobber.AssetbundleInfo);
		}
		List<AssetBundleRobber.AssetbundleInfo>.Enumerator enumerator = default(List<AssetBundleRobber.AssetbundleInfo>.Enumerator);
		this.text.text = "Copy done. ";
		yield break;
		yield break;
	}

	// Token: 0x04000295 RID: 661
	public Text text;

	// Token: 0x04000296 RID: 662
	private AssetsManager assetManager;

	// Token: 0x04000297 RID: 663
	private bool fullCopy;

	// Token: 0x04000298 RID: 664
	private bool noSave;

	// Token: 0x04000299 RID: 665
	private string duelLinksStreamingAssetsPath = "../../../Game/Steam/steamapps/common/Yu-Gi-Oh! Duel Links/dlpc_Data/StreamingAssets/";

	// Token: 0x0400029A RID: 666
	private string duelLinksLocalDataPath = "../../../Game/Steam/steamapps/common/Yu-Gi-Oh! Duel Links/LocalData/92a05e34/";

	// Token: 0x0400029B RID: 667
	private string workingPlace = "Sound/";

	// Token: 0x0400029C RID: 668
	private string fileList = "FileList.txt";

	// Token: 0x0400029D RID: 669
	private readonly object _lock = new object();

	// Token: 0x0400029E RID: 670
	private ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

	// Token: 0x0400029F RID: 671
	public static List<AssetBundleRobber.AssetbundleInfo> files = new List<AssetBundleRobber.AssetbundleInfo>();

	// Token: 0x040002A0 RID: 672
	public static List<AssetBundleRobber.AssetbundleInfo> newFiles = new List<AssetBundleRobber.AssetbundleInfo>();

	// Token: 0x040002A1 RID: 673
	private int count;

	// Token: 0x040002A2 RID: 674
	private int threads = 32;

	// Token: 0x040002A3 RID: 675
	private ConcurrentQueue<int> indexQueue;

	// Token: 0x040002A4 RID: 676
	private List<Thread> workerThreads;

	// Token: 0x040002A5 RID: 677
	private bool isProcessing;
}
