using System;
using YgomGame.Menu;

namespace YgomGame.Solo
{
	// Token: 0x020008FB RID: 2299
	public class SoloModeSortViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(SoloModeSortViewController.SortData[] sortDatas, Action<SoloFilterSortUtil.GateSort> decideAction, SoloFilterSortUtil.GateSort currentSort, string title = "")
		{
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDisp()
		{
		}

		// Token: 0x04008199 RID: 33177
		private const string PREF_PATH = "Solo/SoloModeSort";

		// Token: 0x0400819A RID: 33178
		private const string ARGS_SORT_DATAS = "ArgsSortDatas";

		// Token: 0x0400819B RID: 33179
		private const string ARGS_CALLBACK = "ArgsKeyCallback";

		// Token: 0x0400819C RID: 33180
		private const string ARGS_CURRENT_SORT = "ArgsKeyCurrentSort";

		// Token: 0x0400819D RID: 33181
		private const string ARGS_TITLE = "ArgsKeyTitle";

		// Token: 0x0400819E RID: 33182
		private const string E_TextTitle = "TextTitle";

		// Token: 0x0400819F RID: 33183
		private const string E_ButtonFooter = "ButtonFooter";

		// Token: 0x040081A0 RID: 33184
		private const string E_Label = "Label";

		// Token: 0x040081A1 RID: 33185
		private const string E_Template = "Template";

		// Token: 0x040081A2 RID: 33186
		private const string E_Button0 = "Button0";

		// Token: 0x040081A3 RID: 33187
		private const string E_ImageOn0 = "ImageOn0";

		// Token: 0x040081A4 RID: 33188
		private const string E_ImageOff0 = "ImageOff0";

		// Token: 0x040081A5 RID: 33189
		private const string E_Button1 = "Button1";

		// Token: 0x040081A6 RID: 33190
		private const string E_ImageOn1 = "ImageOn1";

		// Token: 0x040081A7 RID: 33191
		private const string E_ImageOff1 = "ImageOff1";

		// Token: 0x040081A8 RID: 33192
		private SoloModeSortViewController.SortData[] sortDatas;

		// Token: 0x040081A9 RID: 33193
		private Action<SoloFilterSortUtil.GateSort> decideAction;

		// Token: 0x040081AA RID: 33194
		private SoloFilterSortUtil.GateSort currentSort;

		// Token: 0x040081AB RID: 33195
		private string title;

		// Token: 0x020008FC RID: 2300
		public class SortData
		{
			// Token: 0x06004323 RID: 17187 RVA: 0x00002739 File Offset: 0x00000939
			public SortData(string label, SoloFilterSortUtil.GateSort ascendSort, SoloFilterSortUtil.GateSort descendSort)
			{
			}

			// Token: 0x040081AC RID: 33196
			public string label;

			// Token: 0x040081AD RID: 33197
			public SoloFilterSortUtil.GateSort ascendSort;

			// Token: 0x040081AE RID: 33198
			public SoloFilterSortUtil.GateSort descendSort;
		}
	}
}
