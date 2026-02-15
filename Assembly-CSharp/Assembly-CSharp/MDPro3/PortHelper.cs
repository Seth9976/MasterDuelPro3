using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MDPro3.Utility;
using SFB;

namespace MDPro3
{
	// Token: 0x0200123C RID: 4668
	public class PortHelper
	{
		// Token: 0x060089EE RID: 35310 RVA: 0x0010E3FA File Offset: 0x0010C5FA
		public static void ImportFiles()
		{
			PortHelper.ChooseFiles();
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x0010E401 File Offset: 0x0010C601
		public static void ImportBG()
		{
			PortHelper.ChooseBGPicture();
		}

		// Token: 0x060089F0 RID: 35312 RVA: 0x0010E408 File Offset: 0x0010C608
		private static void ChooseFiles()
		{
			ExtensionFilter[] extensions = new ExtensionFilter[]
			{
				new ExtensionFilter(InterString.Get("所有文件", 0), new string[] { "*" }),
				new ExtensionFilter(InterString.Get("卡组码文件", 0), new string[] { "ydk" }),
				new ExtensionFilter(InterString.Get("回放文件", 0), new string[] { "yrp", "yrp3d" }),
				new ExtensionFilter(InterString.Get("扩展卡文件", 0), new string[] { "ypk" }),
				new ExtensionFilter(InterString.Get("数据库文件", 0), new string[] { "cdb" }),
				new ExtensionFilter(InterString.Get("字段文件", 0), new string[] { "conf" }),
				new ExtensionFilter(InterString.Get("图片文件", 0), new string[] { "png", "jpg" }),
				new ExtensionFilter(InterString.Get("动态卡图", 0), new string[] { "mp4" })
			};
			StandaloneFileBrowser.OpenFilePanelAsync(InterString.Get("请选择需要导入的文件", 0), "", extensions, true, delegate(string[] paths)
			{
				PortHelper.CopyFilesToGame(paths);
			});
		}

		// Token: 0x060089F1 RID: 35313 RVA: 0x0010E58C File Offset: 0x0010C78C
		private static void ChooseBGPicture()
		{
			ExtensionFilter[] extensions = new ExtensionFilter[]
			{
				new ExtensionFilter(InterString.Get("图片文件", 0), new string[] { "png", "jpg" })
			};
			StandaloneFileBrowser.OpenFilePanelAsync(InterString.Get("请选择需要导入的文件", 0), "", extensions, false, delegate(string[] paths)
			{
				PortHelper.CopyBGToGame(paths);
			});
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x0010E604 File Offset: 0x0010C804
		private static void CopyBGToGame(IEnumerable<string> files)
		{
			foreach (string text in files)
			{
				if (File.Exists("Picture/DIY/Background.png"))
				{
					File.Delete("Picture/DIY/Background.png");
				}
				File.Copy(text, "Picture/DIY/Background.png");
				MessageManager.Cast(InterString.Get("导入背景图成功。", 0));
				Program.instance.background_.Refresh();
			}
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x0010E684 File Offset: 0x0010C884
		private static void MovePictureToGameBG(string file)
		{
			PortHelper.CopyBGToGame(new List<string> { file });
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x0010E698 File Offset: 0x0010C898
		private static void CopyFilesToGame(IEnumerable<string> files)
		{
			bool newDataAdded = false;
			bool newArtVideosAdded = false;
			foreach (string path in files)
			{
				string fileName = Path.GetFileName(path);
				try
				{
					if (path.ToLower().EndsWith(".ydk"))
					{
						File.Copy(path, "Deck/" + fileName, true);
						MessageManager.Cast(InterString.Get("导入卡组「[?]」成功。", fileName.Replace(".ydk", string.Empty), 0));
					}
					else if (path.ToLower().EndsWith(".yrp") || path.ToLower().EndsWith(".yrp3d"))
					{
						File.Copy(path, "Replay/" + fileName, true);
						MessageManager.Cast(InterString.Get("导入回放「[?]」成功。", fileName, 0));
					}
					else if (path.ToLower().EndsWith(".ypk") || path.ToLower().EndsWith(".zip") || path.ToLower().EndsWith(".cdb") || path.ToLower().EndsWith(".conf"))
					{
						File.Copy(path, "Expansions/" + fileName, true);
						newDataAdded = true;
						if (fileName.ToLower().EndsWith(".ypk") || fileName.ToLower().EndsWith(".zip"))
						{
							MessageManager.Cast(InterString.Get("导入扩展卡文件「[?]」成功。", fileName, 0));
						}
						else if (fileName.ToLower().EndsWith(".cdb"))
						{
							MessageManager.Cast(InterString.Get("导入卡片数据库「[?]」成功。", fileName, 0));
						}
						else if (fileName.ToLower().EndsWith(".conf"))
						{
							MessageManager.Cast(InterString.Get("导入字段文件「[?]」成功。", fileName, 0));
						}
					}
					else if (path.ToLower().EndsWith(".png") || path.ToLower().EndsWith(".jpg"))
					{
						File.Copy(path, "Picture/Art2/" + Path.GetFileName(path), true);
						MessageManager.Cast(InterString.Get("导入自定义卡图「[?]」成功。", fileName, 0));
					}
					else if (path.ToLower().EndsWith(".mp4"))
					{
						File.Copy(path, "Video/Art/" + Path.GetFileName(path), true);
						MessageManager.Cast(InterString.Get("导入动态卡图「[?]」成功。", fileName, 0));
						newArtVideosAdded = true;
					}
				}
				catch
				{
				}
			}
			if (newDataAdded)
			{
				Program.instance.InitializeForDataChange();
			}
			if (newArtVideosAdded)
			{
				CardImageLoader.ReloadArtVideos();
			}
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x0010E944 File Offset: 0x0010CB44
		private static void MoveFilesToGame(string[] files)
		{
			bool newDataAdded = false;
			bool newArtVideosAdded = false;
			foreach (string path in files)
			{
				try
				{
					if (path.ToLower().EndsWith(".ydk"))
					{
						File.Move(path, "Deck/" + Path.GetFileName(path));
					}
					if (path.ToLower().EndsWith(".yrp") || path.ToLower().EndsWith(".yrp3d"))
					{
						File.Move(path, "Replay/" + Path.GetFileName(path));
					}
					if (path.ToLower().EndsWith(".ypk") || path.ToLower().EndsWith(".zip") || path.ToLower().EndsWith(".cdb") || path.ToLower().EndsWith(".conf"))
					{
						File.Move(path, "Expansions/" + Path.GetFileName(path));
						newDataAdded = true;
					}
					if (path.ToLower().EndsWith(".png") || path.ToLower().EndsWith(".jpg") || path.ToLower().EndsWith(".jpeg"))
					{
						File.Move(path, "Picture/Art2/" + Path.GetFileName(path));
					}
					if (path.ToLower().EndsWith(".mp4"))
					{
						File.Move(path, "Video/Art/" + Path.GetFileName(path));
						newArtVideosAdded = true;
					}
				}
				catch
				{
				}
			}
			if (newDataAdded)
			{
				Program.instance.InitializeForDataChange();
			}
			if (newArtVideosAdded)
			{
				CardImageLoader.ReloadArtVideos();
			}
		}

		// Token: 0x060089F6 RID: 35318 RVA: 0x0010EAF4 File Offset: 0x0010CCF4
		private static void ExportResult(bool sucess)
		{
			if (sucess)
			{
				MessageManager.Cast(InterString.Get("导出成功。", 0));
				foreach (string text in PortHelper.filesToDelete)
				{
					File.Delete(text);
				}
				PortHelper.filesToDelete.Clear();
				return;
			}
			MessageManager.Cast(InterString.Get("导出失败。", 0));
		}

		// Token: 0x060089F7 RID: 35319 RVA: 0x0010EB74 File Offset: 0x0010CD74
		public static void ExportAllDecks()
		{
			if (!Directory.Exists("Deck/"))
			{
				Directory.CreateDirectory("Deck/");
			}
			PortHelper.Export(Directory.GetFiles("Deck/"), true);
		}

		// Token: 0x060089F8 RID: 35320 RVA: 0x0010EB9D File Offset: 0x0010CD9D
		public static void ExportAllReplays()
		{
			if (!Directory.Exists("Replay/"))
			{
				Directory.CreateDirectory("Replay/");
			}
			PortHelper.Export(Directory.GetFiles("Replay/"), true);
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x0010EBC6 File Offset: 0x0010CDC6
		public static void ExportAllPictures()
		{
			if (!Directory.Exists("Picture/CardGenerated/"))
			{
				Directory.CreateDirectory("Picture/CardGenerated/");
			}
			PortHelper.Export(Directory.GetFiles("Picture/CardGenerated/"), false);
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x0010EBF0 File Offset: 0x0010CDF0
		private static void Export(string[] filePaths, bool copy = true)
		{
			StandaloneFileBrowser.OpenFolderPanelAsync(InterString.Get("请选择导出目录", 0), "", false, delegate(string[] paths)
			{
				PortHelper.ExportFiles(paths, filePaths, copy);
			});
		}

		// Token: 0x060089FB RID: 35323 RVA: 0x0010EC34 File Offset: 0x0010CE34
		private static void ExportFiles(string[] result, string[] filePaths, bool copy = true)
		{
			try
			{
				foreach (string file in filePaths)
				{
					if (copy)
					{
						File.Copy(file, Path.Combine(result.FirstOrDefault<string>(), Path.GetFileName(file)));
					}
					else
					{
						File.Move(file, Path.Combine(result.FirstOrDefault<string>(), Path.GetFileName(file)));
					}
				}
				PortHelper.ExportResult(true);
			}
			catch
			{
				PortHelper.ExportResult(false);
			}
		}

		// Token: 0x0400C518 RID: 50456
		private static List<string> filesToDelete = new List<string>();

		// Token: 0x0400C519 RID: 50457
		private static string[] pictureFormat = new string[] { "image/png", "image/jpeg" };

		// Token: 0x0400C51A RID: 50458
		private const string bgPath = "Picture/DIY/Background.png";
	}
}
