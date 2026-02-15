using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.PromoCodes
{
	// Token: 0x02000A15 RID: 2581
	public class PromoCodesViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool OpenWithValidate(bool pushOnHome = false)
		{
			return false;
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool OpenWithValidate(int promoCodeId, bool pushOnHome = false)
		{
			return false;
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLaunchFailed()
		{
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yProgressUpdateRoutine(Action onComplete)
		{
			return null;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshView()
		{
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x000029CC File Offset: 0x00000BCC
		private InputField.ContentType GetPromoCodeFormatContentType(PromoCodeFormat format)
		{
			return InputField.ContentType.Standard;
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartInputCooltimeRoutine()
		{
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInputCooltimeRoutine()
		{
			return null;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSubmitInput(string input)
		{
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCodeDecided(string promoCode)
		{
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCodeSendResult(Handle h)
		{
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCodeSuccess(Dictionary<string, object> resultWork)
		{
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCodeFailed(Dictionary<string, object> resultWork, PromoCodesCode resultCode)
		{
		}

		// Token: 0x04008906 RID: 35078
		private const string k_VCPath = "PromoCodes/PromoCodes";

		// Token: 0x04008907 RID: 35079
		private const string k_Args_PromoCodeId = "promoCodeId";

		// Token: 0x04008908 RID: 35080
		private const string k_ELabel_HeaderPromoNameText = "PromoNameText";

		// Token: 0x04008909 RID: 35081
		private const string k_ELabel_InputFormRoot = "InputFormRoot";

		// Token: 0x0400890A RID: 35082
		private const string k_ELabel_CooltimeLoadingIcon = "CooltimeLoadingIcon";

		// Token: 0x0400890B RID: 35083
		private const string k_ELabel_InputField = "InputField";

		// Token: 0x0400890C RID: 35084
		private const string k_ELabel_CompleteLabelRoot = "CompleteLabelRoot";

		// Token: 0x0400890D RID: 35085
		private const string k_TLabel_CooltimeLoadingIconShow = "Show";

		// Token: 0x0400890E RID: 35086
		private const string k_OLabel_ScrollBarDefault = "Default";

		// Token: 0x0400890F RID: 35087
		private const string k_OLabel_ScrollBarOnFooter = "OnFooter";

		// Token: 0x04008910 RID: 35088
		[SerializeField]
		private float m_CoolTime;

		// Token: 0x04008911 RID: 35089
		private IEnumerator m_ProgressUpdateRoutine;

		// Token: 0x04008912 RID: 35090
		private Coroutine m_InputCooltimeRoutine;

		// Token: 0x04008913 RID: 35091
		private List<string> m_LoadedTextGroups;

		// Token: 0x04008914 RID: 35092
		private int m_PromoCodesId;

		// Token: 0x04008915 RID: 35093
		private Dictionary<string, object> m_PromoCodeDataWork;

		// Token: 0x04008916 RID: 35094
		private InputFieldWidget m_InputFieldWidget;
	}
}
