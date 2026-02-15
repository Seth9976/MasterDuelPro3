using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KonamiCommonIAB;
using YgomSystem.Network;

namespace YgomSystem.Billing
{
	// Token: 0x02000791 RID: 1937
	public abstract class Billing_Base : IBilling
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003C2E RID: 15406 RVA: 0x0000216D File Offset: 0x0000036D
		public int NetworkErrorCode
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003C30 RID: 15408
		public abstract bool canMakePayment();

		// Token: 0x06003C31 RID: 15409
		protected abstract void GetItemList(string[] productIds, Action<List<ProductInfo>> callback);

		// Token: 0x06003C32 RID: 15410
		protected abstract void checkUnfinishedPurchase(ProductInfo product, Action<ResultCode, Purchase> callback);

		// Token: 0x06003C33 RID: 15411
		protected abstract void checkUnfinishedPurchase(Action<ResultCode, List<Purchase>> callback);

		// Token: 0x06003C34 RID: 15412
		protected abstract bool BuyItemFromPlatform(ProductInfo product, IabDelegate.OnBuyFinishedDelegate cb);

		// Token: 0x06003C35 RID: 15413
		protected abstract void OnPurchaseFinished(Purchase purchase);

		// Token: 0x06003C36 RID: 15414 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Handle API_Billing_reservation(int shopID, int merchId, ProductInfo product)
		{
			return null;
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Handle API_Billing_purchase(Purchase purchase)
		{
			return null;
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Handle API_Billing_re_store(Purchase purchase)
		{
			return null;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000F38D3 File Offset: 0x000F1AD3
		protected bool CheckMaintenance(Handle handle, out ResultCode res)
		{
			res = ResultCode.NONE;
			return false;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LockUI()
		{
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UnLockUI()
		{
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize()
		{
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadItemList(IList<string> productIds, Action<List<ProductInfo>> callback)
		{
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductInfo GetItem(string productId)
		{
			return null;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0000216D File Offset: 0x0000036D
		public void DoRestore(Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x0000216D File Offset: 0x0000036D
		public void DoRestore(Action<ResultCode, List<Purchase>> callback)
		{
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x0000216D File Offset: 0x0000036D
		public void BuyItem(int shopId, string productId, Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RequestRevervation(int shopId, ProductInfo product)
		{
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckUserAge(int shopId, ProductInfo product)
		{
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenUserAgeSelectSheet(Action<int> selectedCallback, Action canceledCallback)
		{
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenUserAgeConfirm(string selectedLabel, Action decidedCallback, Action canceledCallback)
		{
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnBuyPlatformFinish(Result result, Purchase purchase)
		{
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RequestPurchase(Purchase purchase, Action<ResultCode> callback)
		{
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RequestAddItem(Purchase purchase, Action<ResultCode> callback)
		{
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RequestCancel(Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RequestRestore(List<Purchase> purchases, Action<ResultCode> callback)
		{
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RequestRestore(List<Purchase> purchases, List<Purchase> compPurchases, Action<ResultCode, List<Purchase>> callback)
		{
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RequestRestore(Purchase purchase, Action<ResultCode> callback)
		{
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnBuyFinish(ResultCode code)
		{
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void PurchaseFinishTransaction(Purchase purchase)
		{
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDoubleNotationDisplayPrice(string productId)
		{
			return null;
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string GetDoubleNotationDisplayPrice(ProductInfo productInfo)
		{
			return null;
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetErrorMsg(string msg)
		{
		}

		// Token: 0x040034E3 RID: 13539
		protected InAppBilling m_inAppBilling;

		// Token: 0x040034E4 RID: 13540
		protected Dictionary<string, ProductInfo> m_productDic;

		// Token: 0x040034E5 RID: 13541
		private Action<ResultCode> OnBuyCallback;
	}
}
