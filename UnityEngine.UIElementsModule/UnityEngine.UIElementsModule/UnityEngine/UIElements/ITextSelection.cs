using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000454 RID: 1108
	public interface ITextSelection
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060020AF RID: 8367
		// (set) Token: 0x060020B0 RID: 8368
		bool isSelectable { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060020B1 RID: 8369
		// (set) Token: 0x060020B2 RID: 8370
		Color cursorColor { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060020B3 RID: 8371
		// (set) Token: 0x060020B4 RID: 8372
		Color selectionColor { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060020B5 RID: 8373
		// (set) Token: 0x060020B6 RID: 8374
		int cursorIndex { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060020B7 RID: 8375
		// (set) Token: 0x060020B8 RID: 8376
		bool doubleClickSelectsWord { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060020B9 RID: 8377
		// (set) Token: 0x060020BA RID: 8378
		int selectIndex { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060020BB RID: 8379
		// (set) Token: 0x060020BC RID: 8380
		bool tripleClickSelectsLine { get; set; }

		// Token: 0x060020BD RID: 8381
		bool HasSelection();

		// Token: 0x060020BE RID: 8382
		void SelectAll();

		// Token: 0x060020BF RID: 8383
		void SelectNone();

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060020C0 RID: 8384
		// (set) Token: 0x060020C1 RID: 8385
		bool selectAllOnFocus { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060020C2 RID: 8386
		// (set) Token: 0x060020C3 RID: 8387
		bool selectAllOnMouseUp { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060020C4 RID: 8388
		Vector2 cursorPosition { get; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060020C5 RID: 8389
		float lineHeightAtCursorPosition { get; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060020C6 RID: 8390
		float cursorWidth { get; }
	}
}
