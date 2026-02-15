using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MDPro3
{
	// Token: 0x020011ED RID: 4589
	public class Boot : MonoBehaviour
	{
		// Token: 0x0600882F RID: 34863 RVA: 0x000F9A59 File Offset: 0x000F7C59
		private void Start()
		{
			Application.targetFrameRate = 0;
			this.InitializeLanguage();
			base.StartCoroutine(this.LoadMainSceneAsync());
		}

		// Token: 0x06008830 RID: 34864 RVA: 0x000F9A78 File Offset: 0x000F7C78
		private void Update()
		{
			this.time += Time.deltaTime;
			if (this.time > 0.33f)
			{
				this.time = 0f;
				this.dots += ".";
				if (this.dots == "....")
				{
					this.dots = "";
				}
			}
			if (this.extracting && this.totalNum != 0)
			{
				float progress = (float)this.nowNum / (float)this.totalNum;
				this.progressBar.value = progress;
			}
			if (this.totalNum == 0)
			{
				this.text.text = this.title + this.dots;
				return;
			}
			this.text.text = string.Concat(new string[]
			{
				this.title,
				"(",
				this.nowNum.ToString(),
				"/",
				this.totalNum.ToString(),
				")"
			});
		}

		// Token: 0x06008831 RID: 34865 RVA: 0x000F9B88 File Offset: 0x000F7D88
		private bool InitializeLanguage()
		{
			if (!Directory.Exists("Data/"))
			{
				Directory.CreateDirectory("Data/");
				Config.Initialize("Data/config.conf");
				return true;
			}
			Config.Initialize("Data/config.conf");
			if (Config.Get("Version", "Version") == "Version")
			{
				return true;
			}
			InterString.Initialize();
			return false;
		}

		// Token: 0x06008832 RID: 34866 RVA: 0x000F9BE5 File Offset: 0x000F7DE5
		private IEnumerator CheckFile()
		{
			foreach (string zip in this.zips)
			{
				if (!Config.GetBool(zip + "_install", false))
				{
					IEnumerator enumerator = this.Check(zip);
					base.StartCoroutine(enumerator);
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						yield return obj;
					}
					Config.Set(zip + "_install", "1");
					Config.Save();
					GC.Collect();
				}
				zip = null;
			}
			List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
			yield return null;
			base.StartCoroutine(this.LoadMainSceneAsync());
			yield break;
			yield break;
		}

		// Token: 0x06008833 RID: 34867 RVA: 0x000F9BF4 File Offset: 0x000F7DF4
		private IEnumerator Check(string type)
		{
			Boot.<Check>d__13 <Check>d__ = new Boot.<Check>d__13(0);
			<Check>d__.<>4__this = this;
			<Check>d__.type = type;
			return <Check>d__;
		}

		// Token: 0x06008834 RID: 34868 RVA: 0x000F9C0A File Offset: 0x000F7E0A
		private IEnumerator LoadMainSceneAsync()
		{
			this.nowNum = 0;
			this.totalNum = 0;
			this.progressBar.value = 0f;
			this.title = InterString.Get("正在初始化", 0);
			Program.SetRoot();
			ABLoader.CacheMasterDuelOutDuelBundles();
			while (!ABLoader.mdCached)
			{
				this.progressBar.value = ABLoader.mdCachedProgress;
				yield return null;
			}
			Config.Initialize("Data/config.conf");
			Config.Set("Version", Application.version.Substring(0, 5));
			Config.Save();
			AsyncOperationHandle<IResourceLocator> ini = Addressables.InitializeAsync();
			while (!ini.IsDone)
			{
				this.progressBar.value = ini.PercentComplete;
				yield return null;
			}
			this.title = InterString.Get("正在读取数据", 0);
			AsyncOperationHandle<Items> handle = Addressables.LoadAssetAsync<Items>("ScriptableObjects/Items.asset");
			while (!handle.IsDone)
			{
				this.progressBar.value = handle.PercentComplete;
				yield return null;
			}
			Program.items = handle.Result;
			this.title = InterString.Get("正在进入游戏", 0);
			AsyncOperationHandle<SceneInstance> load = Addressables.LoadSceneAsync("SceneMain", LoadSceneMode.Single, true, 100, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded);
			while (!load.IsDone)
			{
				yield return null;
				this.progressBar.value = load.PercentComplete;
			}
			yield break;
		}

		// Token: 0x06008835 RID: 34869 RVA: 0x000F9C19 File Offset: 0x000F7E19
		public static void FastExtractZipFile(string file, string dir, string password = "")
		{
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			new FastZip
			{
				Password = password
			}.ExtractZip(file, dir, "");
		}

		// Token: 0x06008836 RID: 34870 RVA: 0x000F9C42 File Offset: 0x000F7E42
		private IEnumerator ExtractZipFile(byte[] data, string outFolder)
		{
			Boot.<ExtractZipFile>d__16 <ExtractZipFile>d__ = new Boot.<ExtractZipFile>d__16(0);
			<ExtractZipFile>d__.<>4__this = this;
			<ExtractZipFile>d__.data = data;
			<ExtractZipFile>d__.outFolder = outFolder;
			return <ExtractZipFile>d__;
		}

		// Token: 0x06008837 RID: 34871 RVA: 0x000F9C60 File Offset: 0x000F7E60
		private bool VersionCheck()
		{
			bool firstInstall = this.InitializeLanguage();
			string installVersion = Application.version;
			string installedVersion = Config.Get("Version", "Version");
			if (installedVersion == "Version")
			{
				firstInstall = true;
			}
			if (firstInstall)
			{
				if (installVersion.Length > 5 || !installVersion.EndsWith("0"))
				{
					this.title = "不能直接安装更新包。Can not install update apk directly.";
					Directory.Delete("Data/");
					return false;
				}
				return true;
			}
			else
			{
				if (installVersion == installedVersion)
				{
					return true;
				}
				if (installVersion.Length > 5)
				{
					if (installVersion.EndsWith("0"))
					{
						if (this.InstallNext(installedVersion, installVersion))
						{
							return true;
						}
						if (installVersion.Substring(0, 5) == installedVersion)
						{
							return true;
						}
						this.title = InterString.Get("当前更新包需要的版本：「[?]」。", this.VersionPre(installVersion), 0);
						this.title += InterString.Get("已安装版本：「[?]」。", installedVersion, 0);
						return false;
					}
					else
					{
						if (installVersion.Substring(0, 5) == installedVersion)
						{
							return true;
						}
						this.title = InterString.Get("当前更新包需要的版本：「[?]」。", installVersion.Substring(0, 5), 0);
						this.title += InterString.Get("已安装版本：「[?]」。", installedVersion, 0);
						return false;
					}
				}
				else
				{
					if (installVersion.EndsWith("0"))
					{
						return true;
					}
					if (this.VersionPre(installVersion) == installedVersion.Substring(0, 5))
					{
						return true;
					}
					this.title = InterString.Get("当前更新包需要的版本：「[?]」。", this.VersionPre(installVersion), 0);
					this.title += InterString.Get("已安装版本：「[?]」。", installedVersion, 0);
					return false;
				}
			}
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x000F9DEC File Offset: 0x000F7FEC
		private bool InstallNext(string installedVersion, string installVersion)
		{
			int installedInt = this.GetVersionInt(installedVersion);
			return this.GetVersionInt(installVersion) - installedInt == 1;
		}

		// Token: 0x06008839 RID: 34873 RVA: 0x000F9E10 File Offset: 0x000F8010
		private string VersionPre(string version)
		{
			string returnValue = (this.GetVersionInt(version) - 1).ToString("D3");
			return string.Concat(new string[]
			{
				returnValue.Substring(0, 1),
				".",
				returnValue.Substring(1, 1),
				".",
				returnValue.Substring(2, 1)
			});
		}

		// Token: 0x0600883A RID: 34874 RVA: 0x000F9E70 File Offset: 0x000F8070
		private int GetVersionInt(string version)
		{
			return int.Parse(version.Substring(0, 1) + version.Substring(2, 1) + version.Substring(4, 1));
		}

		// Token: 0x0400C310 RID: 49936
		public Slider progressBar;

		// Token: 0x0400C311 RID: 49937
		public Text text;

		// Token: 0x0400C312 RID: 49938
		private string title;

		// Token: 0x0400C313 RID: 49939
		private string dots;

		// Token: 0x0400C314 RID: 49940
		private float time;

		// Token: 0x0400C315 RID: 49941
		private bool extracting;

		// Token: 0x0400C316 RID: 49942
		private int totalNum;

		// Token: 0x0400C317 RID: 49943
		private int nowNum;

		// Token: 0x0400C318 RID: 49944
		private readonly List<string> zips = new List<string>();
	}
}
