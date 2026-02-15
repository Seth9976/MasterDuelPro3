using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000066 RID: 102
public class CDRobber : MonoBehaviour
{
	// Token: 0x060001DE RID: 478 RVA: 0x000058E9 File Offset: 0x00003AE9
	private void Start()
	{
		Application.targetFrameRate = 0;
		this.LoadList();
		this.Copy();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00005900 File Offset: 0x00003B00
	private void Copy()
	{
		Directory.CreateDirectory(this.path + "Copy");
		foreach (CDRobber.AssetbundleInfo file in CDRobber.files)
		{
			if (file.dependencies.Count == 0)
			{
				File.Copy(this.path + "CrossDuelAddressables/" + file.path, this.path + "Copy/" + file.path);
			}
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x000059A0 File Offset: 0x00003BA0
	private void LoadList()
	{
		string[] array = File.ReadAllText(this.path + "FileList.txt").Replace("\r", "").Split('\n', StringSplitOptions.None);
		CDRobber.AssetbundleInfo file = default(CDRobber.AssetbundleInfo);
		file.path = "";
		file.cab = "";
		file.dependencies = new List<string>();
		foreach (string line in array)
		{
			if (!line.StartsWith("-") && !line.StartsWith("*"))
			{
				if (file.path.Length > 0)
				{
					CDRobber.files.Add(file);
					file = default(CDRobber.AssetbundleInfo);
					file.path = line;
					file.cab = "";
					file.dependencies = new List<string>();
				}
				else
				{
					file.path = line;
				}
			}
			else if (line.StartsWith("-"))
			{
				file.cab = line.Replace("-", "");
			}
			else if (line.StartsWith("*"))
			{
				file.dependencies.Add(line.Replace("*", ""));
			}
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00005AD3 File Offset: 0x00003CD3
	private IEnumerator RefreshList()
	{
		string[] fileInfos = Directory.GetFiles(this.path);
		List<string> fileNames = new List<string>();
		foreach (string fileInfo in fileInfos)
		{
			fileNames.Add(Path.GetFileName(fileInfo));
		}
		AssetsManager manager = new AssetsManager();
		int j;
		for (int i = 0; i < fileNames.Count; i = j + 1)
		{
			AssetBundleFile bundle = manager.LoadBundleFile(fileInfos[i], false).file;
			MemoryStream bundleStream = new MemoryStream();
			bundle.Unpack(new AssetsFileWriter(bundleStream));
			CDRobber.AssetbundleInfo info = default(CDRobber.AssetbundleInfo);
			info.path = fileNames[i];
			info.cab = bundle.GetAllFileNames()[0].Replace(".resource", string.Empty).Replace(".resS", "").Replace(".sharedAssets", "");
			info.dependencies = this.GetDependencise(info.path, bundleStream.GetBuffer());
			CDRobber.files.Add(info);
			this.text.text = i.ToString() + "/" + fileNames.Count.ToString();
			yield return null;
			j = i;
		}
		string all = "";
		foreach (CDRobber.AssetbundleInfo file in CDRobber.files)
		{
			all = all + file.path + "\r\n";
			all = all + "-" + file.cab + "\r\n";
			foreach (string depend in file.dependencies)
			{
				all = all + "--" + depend + "\r\n";
			}
		}
		File.WriteAllText(this.path + "../FileList.txt", all);
		yield break;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00005AE4 File Offset: 0x00003CE4
	private List<string> GetDependencise(string name, byte[] bytes)
	{
		List<string> returnValue = new List<string>();
		byte[] nameData = new byte[]
		{
			46, 98, 117, 110, 100, 108, 101, 0, 5, 0,
			0, 0, 36, 0, 0, 0, 99, 97, 98, 45
		};
		List<int> point = new List<int>();
		for (int i = 0; i < bytes.Length; i++)
		{
			bool yes = true;
			if (i + nameData.Length < bytes.Length)
			{
				for (int j = 0; j < nameData.Length; j++)
				{
					if (bytes[i + j] != nameData[j] && j != 12 && j != 16)
					{
						yes = false;
						break;
					}
				}
				if (yes)
				{
					point.Add(i + 16);
					Debug.Log(name + "-" + i.ToString());
				}
			}
		}
		if (point.Count == 0)
		{
			Debug.Log("没找到: " + name);
		}
		else if (point.Count > 1)
		{
			Debug.Log("找到多个: " + name);
		}
		else
		{
			bool still = true;
			while (still)
			{
				List<byte> bs = new List<byte>();
				for (int k = point[0]; k < point[0] + 36; k++)
				{
					bs.Add(bytes[k]);
				}
				byte[] ab = bs.ToArray();
				returnValue.Add(Encoding.UTF8.GetString(ab));
				if (bytes[point[0] + 36] == 36 && bytes[point[0] + 37] == 0 && bytes[point[0] + 38] == 0 && bytes[point[0] + 39] == 0)
				{
					List<int> list = point;
					list[0] = list[0] + 40;
				}
				else
				{
					still = false;
				}
			}
		}
		return returnValue;
	}

	// Token: 0x04000288 RID: 648
	private string path = "StreamingAssets/CrossDuel/";

	// Token: 0x04000289 RID: 649
	public Text text;

	// Token: 0x0400028A RID: 650
	public static List<CDRobber.AssetbundleInfo> files = new List<CDRobber.AssetbundleInfo>();

	// Token: 0x02000067 RID: 103
	public struct AssetbundleInfo
	{
		// Token: 0x0400028B RID: 651
		public string path;

		// Token: 0x0400028C RID: 652
		public string cab;

		// Token: 0x0400028D RID: 653
		public List<string> dependencies;
	}
}
