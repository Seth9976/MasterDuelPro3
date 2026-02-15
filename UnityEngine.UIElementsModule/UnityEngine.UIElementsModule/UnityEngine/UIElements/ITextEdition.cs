using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000453 RID: 1107
	public interface ITextEdition
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002089 RID: 8329
		// (set) Token: 0x0600208A RID: 8330
		bool multiline { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600208B RID: 8331
		// (set) Token: 0x0600208C RID: 8332
		bool isReadOnly { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600208D RID: 8333
		// (set) Token: 0x0600208E RID: 8334
		int maxLength { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600208F RID: 8335
		// (set) Token: 0x06002090 RID: 8336
		string placeholder { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002091 RID: 8337
		// (set) Token: 0x06002092 RID: 8338
		bool isDelayed { get; set; }

		// Token: 0x06002093 RID: 8339
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		void SaveValueAndText();

		// Token: 0x06002094 RID: 8340
		void RestoreValueAndText();

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002095 RID: 8341
		// (set) Token: 0x06002096 RID: 8342
		Func<char, bool> AcceptCharacter { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002097 RID: 8343
		// (set) Token: 0x06002098 RID: 8344
		Action<bool> UpdateScrollOffset { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002099 RID: 8345
		// (set) Token: 0x0600209A RID: 8346
		Action UpdateValueFromText { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600209B RID: 8347
		// (set) Token: 0x0600209C RID: 8348
		Action UpdateTextFromValue { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600209D RID: 8349
		// (set) Token: 0x0600209E RID: 8350
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action MoveFocusToCompositeRoot { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600209F RID: 8351
		// (set) Token: 0x060020A0 RID: 8352
		Func<string> GetDefaultValueType { get; set; }

		// Token: 0x060020A1 RID: 8353
		void UpdateText(string value);

		// Token: 0x060020A2 RID: 8354
		string CullString(string s);

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060020A3 RID: 8355
		// (set) Token: 0x060020A4 RID: 8356
		char maskChar { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060020A5 RID: 8357
		// (set) Token: 0x060020A6 RID: 8358
		bool isPassword { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060020A7 RID: 8359
		// (set) Token: 0x060020A8 RID: 8360
		bool hidePlaceholderOnFocus { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x00078754 File Offset: 0x00076954
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x00078787 File Offset: 0x00076987
		bool autoCorrection
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for autoCorrection. Calling ITextEdition.autoCorrection of this type will always return false.");
				return false;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for autoCorrection. Assigning a value to ITextEdition.autoCorrection will not update its value.");
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000787AC File Offset: 0x000769AC
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x000787DF File Offset: 0x000769DF
		bool hideMobileInput
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideMobileInput. Calling ITextEdition.hideMobileInput of this type will always return false.");
				return false;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideMobileInput. Assigning a value to ITextEdition.hideMobileInput will not update its value.");
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00078804 File Offset: 0x00076A04
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x00078837 File Offset: 0x00076A37
		TouchScreenKeyboardType keyboardType
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for keyboardType. Calling ITextEdition.keyboardType of this type will always return Default.");
				return TouchScreenKeyboardType.Default;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for keyboardType. Assigning a value to ITextEdition.keyboardType will not update its value.");
			}
		}
	}
}
