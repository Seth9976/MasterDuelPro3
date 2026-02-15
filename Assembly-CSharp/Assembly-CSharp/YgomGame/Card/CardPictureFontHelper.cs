using System;
using System.Collections.Generic;

namespace YgomGame.Card
{
	// Token: 0x0200110A RID: 4362
	public class CardPictureFontHelper
	{
		// Token: 0x060081DA RID: 33242 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create()
		{
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x000F37CF File Offset: 0x000F19CF
		public static bool GetNormalTextFontSize(int cardid, out float fontsize)
		{
			fontsize = 0f;
			return false;
		}

		// Token: 0x060081DC RID: 33244 RVA: 0x000F37CF File Offset: 0x000F19CF
		public static bool GetPendulumTextFontSize(int cardid, out float fontsize)
		{
			fontsize = 0f;
			return false;
		}

		// Token: 0x060081DD RID: 33245 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadDataToTable()
		{
		}

		// Token: 0x060081DE RID: 33246 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetTable()
		{
		}

		// Token: 0x060081DF RID: 33247 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddData(int cardid, float fontsize, bool ispendulum)
		{
		}

		// Token: 0x060081E0 RID: 33248 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearData()
		{
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RewriteData()
		{
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearTable()
		{
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x060081E5 RID: 33253 RVA: 0x000F46DA File Offset: 0x000F28DA
		private bool GetNormalTextFontSizeImpl(int cardid, out float fontsize)
		{
			fontsize = 0f;
			return false;
		}

		// Token: 0x060081E6 RID: 33254 RVA: 0x000F46DA File Offset: 0x000F28DA
		private bool GetPendulumTextFontSizeImpl(int cardid, out float fontsize)
		{
			fontsize = 0f;
			return false;
		}

		// Token: 0x0400BA43 RID: 47683
		private Dictionary<short, float> m_NormalFontSizeTable;

		// Token: 0x0400BA44 RID: 47684
		private Dictionary<short, float> m_PendulumFontSizeTable;

		// Token: 0x0400BA45 RID: 47685
		private const string DATAFILEPATHBASE = "Card/ScriptableObjects/#/CardPictureFontSetting";

		// Token: 0x0400BA46 RID: 47686
		public const byte AUTOSIZE = 0;

		// Token: 0x0400BA47 RID: 47687
		private static CardPictureFontHelper m_Instance;

		// Token: 0x0400BA48 RID: 47688
		public static CardPictureFontHelper.Mode m_Mode;

		// Token: 0x0200110B RID: 4363
		public enum Mode
		{
			// Token: 0x0400BA4A RID: 47690
			Normal,
			// Token: 0x0400BA4B RID: 47691
			CardCheck
		}
	}
}
