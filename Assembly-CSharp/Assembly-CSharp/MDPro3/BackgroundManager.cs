using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YgomSystem.Effect;

namespace MDPro3
{
	// Token: 0x02001265 RID: 4709
	public class BackgroundManager : Manager
	{
		// Token: 0x06008A8D RID: 35469 RVA: 0x001152F6 File Offset: 0x001134F6
		public void Refresh()
		{
			this.Change(this.cid);
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x00115304 File Offset: 0x00113504
		public void Change(int id)
		{
			global::UnityEngine.Object.Destroy(this.back);
			if (id == 0)
			{
				int random = global::UnityEngine.Random.Range(0, BackgroundManager.backgrounds.Count);
				id = Tools.GetNthDictionaryElement<int, string>(BackgroundManager.backgrounds, random).Key;
			}
			this.cid = id;
			if (id == 50)
			{
				id = 1;
			}
			if (id == 51)
			{
				id = 2;
			}
			if (id == 52)
			{
				id = 11;
			}
			if (id == 53)
			{
				id = 5;
			}
			if (id == 54)
			{
				id = 4;
			}
			if (id == 55)
			{
				id = 6;
			}
			if (id == 56)
			{
				id = 7;
			}
			if (id == 57)
			{
				id = 8;
			}
			if (id == 58)
			{
				id = 9;
			}
			if (id == 59)
			{
				id = 10;
			}
			string endString = id.ToString("D4");
			this.back = ABLoader.LoadFromFolder<SpriteRenderer>("MasterDuel/Background/Back" + endString, true, true);
			SpriteScaler spriteScaler;
			if (this.back.TryGetComponent<SpriteScaler>(out spriteScaler))
			{
				spriteScaler.isApplyOnUpdate = true;
				spriteScaler.SetFitMode(SpriteScaler.FitMode.FitHighestResolutionMaintainAspectRatio);
			}
			Tools.ChangeLayer(this.back, "2D", false);
			this.back.transform.SetParent(base.transform, false);
			if (id == 12 || this.cid >= 50)
			{
				this.SetDIYBGAsync(this.back.GetComponent<SpriteRenderer>(), id);
			}
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x0011542C File Offset: 0x0011362C
		private async UniTask SetDIYBGAsync(SpriteRenderer renderer, int id)
		{
			string bg = "Picture/DIY/Background";
			if (File.Exists(bg + ".png"))
			{
				bg += ".png";
			}
			else
			{
				if (!File.Exists(bg + ".jpg"))
				{
					return;
				}
				bg += ".jpg";
			}
			Texture2D pic = await TextureManager.LoadPicFromFileAsync(bg);
			int targetHeight = 1080;
			Texture2D scaledTexture;
			if (pic.height != targetHeight)
			{
				scaledTexture = TextureManager.ResizeTexture2D(pic, Mathf.RoundToInt((float)pic.width * ((float)targetHeight / (float)pic.height)), targetHeight);
			}
			else
			{
				scaledTexture = pic;
			}
			if (renderer != null)
			{
				renderer.sprite = TextureManager.Texture2Sprite(scaledTexture);
			}
			if (id == 9)
			{
				renderer.material.SetTexture("_MainTex01", scaledTexture);
			}
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x00115478 File Offset: 0x00113678
		public int GetIDByName(string bgName)
		{
			int id = 0;
			if (bgName == InterString.Get("随机", 0))
			{
				return 0;
			}
			foreach (KeyValuePair<int, string> background in BackgroundManager.backgrounds)
			{
				if (bgName == background.Value)
				{
					id = background.Key;
					break;
				}
			}
			return id;
		}

		// Token: 0x0400C626 RID: 50726
		private GameObject back;

		// Token: 0x0400C627 RID: 50727
		private int cid;

		// Token: 0x0400C628 RID: 50728
		public static readonly Dictionary<int, string> backgrounds = new Dictionary<int, string>
		{
			{ 1, "Classic" },
			{ 2, "Classic2" },
			{ 11, "ClassicRed" },
			{ 5, "ClassicPink" },
			{ 4, "ClassicPinkShine" },
			{ 7, "WCS" },
			{ 8, "Shop" },
			{ 9, "Knowledge" },
			{ 3, "PurpleDarkFantasy" },
			{ 10, "DeepDarkFantasy" },
			{ 12, "DIY Green" },
			{ 14, "Neos" },
			{ 16, "Classic Grid" },
			{ 17, "4th Anniversary" },
			{ 50, "DIY Classic" },
			{ 51, "DIY Classic2" },
			{ 52, "DIY Red" },
			{ 53, "DIY Pink" },
			{ 54, "DIY PinkShine" },
			{ 56, "DIY WCS" },
			{ 57, "DIY SHOP" },
			{ 58, "DIY Knowledge" },
			{ 59, "DIY DeepDarkFantasy" }
		};
	}
}
