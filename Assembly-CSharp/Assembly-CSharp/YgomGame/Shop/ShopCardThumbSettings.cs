using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Shop
{
	// Token: 0x02000949 RID: 2377
	public class ShopCardThumbSettings : ScriptableObject
	{
		// Token: 0x060045EF RID: 17903 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCardThumbSettings.ThumbSetting GetSetting(ShopCardThumbSettings.Format format, int mrk)
		{
			return null;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCardThumbSettings.ImageThumbSetting GetImageSetting(ShopCardThumbSettings.Format format, string path)
		{
			return null;
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExitsSetting(ShopCardThumbSettings.Format format, int mrk, string path = null)
		{
			return false;
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExitsImageSetting(ShopCardThumbSettings.Format format, string path)
		{
			return false;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool HasAspectRatioSetting(ShopCardThumbSettings.Format format)
		{
			return false;
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportData(ShopCardThumbSettings.Format format, int mrk, RawImage rawImage)
		{
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportImageData(ShopCardThumbSettings.Format format, string path, RawImage rawImage)
		{
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportAspectRatio(ShopCardThumbSettings.Format format, RectTransform rectTransform)
		{
		}

		// Token: 0x04008412 RID: 33810
		private const string k_SettingPath = "Definition/Shop/CardThumbSettings";

		// Token: 0x04008413 RID: 33811
		[SerializeField]
		private float m_LargeAspect;

		// Token: 0x04008414 RID: 33812
		[SerializeField]
		private float m_SmallAspect;

		// Token: 0x04008415 RID: 33813
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_LargeMap;

		// Token: 0x04008416 RID: 33814
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_SmallPackMap;

		// Token: 0x04008417 RID: 33815
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_PickMap;

		// Token: 0x04008418 RID: 33816
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_PickWideTrimMap;

		// Token: 0x04008419 RID: 33817
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_Additional_1_Map;

		// Token: 0x0400841A RID: 33818
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_Additional_2_Map;

		// Token: 0x0400841B RID: 33819
		[SerializeField]
		private ShopCardThumbSettings.SettingMap m_Additional_3_Map;

		// Token: 0x0400841C RID: 33820
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_LargeImageMap;

		// Token: 0x0400841D RID: 33821
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_SmallImageMap;

		// Token: 0x0400841E RID: 33822
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_PickImageMap;

		// Token: 0x0400841F RID: 33823
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_PickWideTrimImageMap;

		// Token: 0x04008420 RID: 33824
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_Additional_1_ImageMap;

		// Token: 0x04008421 RID: 33825
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_Additional_2_ImageMap;

		// Token: 0x04008422 RID: 33826
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSettingMap m_Additional_3_ImageMap;

		// Token: 0x04008423 RID: 33827
		[SerializeField]
		private ShopCardThumbSettings.ThumbSetting m_NoneSetting;

		// Token: 0x04008424 RID: 33828
		[SerializeField]
		private ShopCardThumbSettings.ImageThumbSetting m_NoneImageSetting;

		// Token: 0x0200094A RID: 2378
		[Serializable]
		public class ThumbSetting : IRawImageUVSetting
		{
			// Token: 0x060045F8 RID: 17912 RVA: 0x00002739 File Offset: 0x00000939
			public ThumbSetting(int mrk)
			{
			}

			// Token: 0x060045F9 RID: 17913 RVA: 0x00002739 File Offset: 0x00000939
			public ThumbSetting(int mrk, ShopCardThumbSettings.ThumbSetting source)
			{
			}

			// Token: 0x060045FA RID: 17914 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x060045FB RID: 17915 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x04008425 RID: 33829
			public int mrk;

			// Token: 0x04008426 RID: 33830
			public Vector2 uvRectPos;

			// Token: 0x04008427 RID: 33831
			public Vector2 uvRectSize;

			// Token: 0x04008428 RID: 33832
			[NonSerialized]
			public float aspectRatio;
		}

		// Token: 0x0200094B RID: 2379
		[Serializable]
		private class SettingMap
		{
			// Token: 0x060045FC RID: 17916 RVA: 0x0000216A File Offset: 0x0000036A
			public ShopCardThumbSettings.ThumbSetting GetSetting(int mrk)
			{
				return null;
			}

			// Token: 0x060045FD RID: 17917 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExists(int mrk)
			{
				return false;
			}

			// Token: 0x060045FE RID: 17918 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(int mrk, RawImage rawImage)
			{
			}

			// Token: 0x04008429 RID: 33833
			[SerializeField]
			private List<ShopCardThumbSettings.ThumbSetting> m_Settings;

			// Token: 0x0400842A RID: 33834
			private Dictionary<int, ShopCardThumbSettings.ThumbSetting> m_SettingsMap;

			// Token: 0x0400842B RID: 33835
			[SerializeField]
			private ShopCardThumbSettings.ThumbSetting m_DefaultSetting;
		}

		// Token: 0x0200094C RID: 2380
		[Serializable]
		public class ImageThumbSetting : IRawImageUVSetting
		{
			// Token: 0x06004600 RID: 17920 RVA: 0x00002739 File Offset: 0x00000939
			public ImageThumbSetting(string path)
			{
			}

			// Token: 0x06004601 RID: 17921 RVA: 0x00002739 File Offset: 0x00000939
			public ImageThumbSetting(string path, ShopCardThumbSettings.ImageThumbSetting source)
			{
			}

			// Token: 0x06004602 RID: 17922 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x06004603 RID: 17923 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x0400842C RID: 33836
			public string path;

			// Token: 0x0400842D RID: 33837
			public Vector2 uvRectPos;

			// Token: 0x0400842E RID: 33838
			public Vector2 uvRectSize;

			// Token: 0x0400842F RID: 33839
			[NonSerialized]
			public float aspectRatio;
		}

		// Token: 0x0200094D RID: 2381
		[Serializable]
		private class ImageThumbSettingMap
		{
			// Token: 0x06004604 RID: 17924 RVA: 0x0000216A File Offset: 0x0000036A
			public ShopCardThumbSettings.ImageThumbSetting GetSetting(string path)
			{
				return null;
			}

			// Token: 0x06004605 RID: 17925 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExists(string path)
			{
				return false;
			}

			// Token: 0x06004606 RID: 17926 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(string path, RawImage rawImage)
			{
			}

			// Token: 0x04008430 RID: 33840
			[SerializeField]
			private List<ShopCardThumbSettings.ImageThumbSetting> m_Settings;

			// Token: 0x04008431 RID: 33841
			private Dictionary<string, ShopCardThumbSettings.ImageThumbSetting> m_SettingsMap;

			// Token: 0x04008432 RID: 33842
			[SerializeField]
			private ShopCardThumbSettings.ImageThumbSetting m_DefaultSetting;
		}

		// Token: 0x0200094E RID: 2382
		public enum Format
		{
			// Token: 0x04008434 RID: 33844
			None = -1,
			// Token: 0x04008435 RID: 33845
			SmallPack,
			// Token: 0x04008436 RID: 33846
			Large,
			// Token: 0x04008437 RID: 33847
			PickThumb,
			// Token: 0x04008438 RID: 33848
			PickWideTrimThumb,
			// Token: 0x04008439 RID: 33849
			Additional_1 = 10,
			// Token: 0x0400843A RID: 33850
			Additional_2,
			// Token: 0x0400843B RID: 33851
			Additional_3
		}
	}
}
