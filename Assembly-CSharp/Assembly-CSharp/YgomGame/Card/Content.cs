using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using YgomGame.Duel;

namespace YgomGame.Card
{
	// Token: 0x02001116 RID: 4374
	public sealed class Content
	{
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06008232 RID: 33330 RVA: 0x0000216A File Offset: 0x0000036A
		public static Content Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06008233 RID: 33331 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06008234 RID: 33332 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoadSuccess
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008235 RID: 33333 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create()
		{
		}

		// Token: 0x06008236 RID: 33334 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Destroy()
		{
		}

		// Token: 0x06008237 RID: 33335 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init()
		{
		}

		// Token: 0x06008238 RID: 33336 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadBinaryAsync()
		{
			return null;
		}

		// Token: 0x06008239 RID: 33337 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadTutorialBinary()
		{
		}

		// Token: 0x0600823A RID: 33338 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadTutorialBinaryAsync()
		{
			return null;
		}

		// Token: 0x0600823B RID: 33339 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeclaraCardCheckOpenCard(Dictionary<string, object> dicOpenCard)
		{
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x0000216D File Offset: 0x0000036D
		public void Release()
		{
		}

		// Token: 0x0600823D RID: 33341 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reload()
		{
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x0000216A File Offset: 0x0000036A
		private byte[] GetBytesDecryptionData(string path)
		{
			return null;
		}

		// Token: 0x0600823F RID: 33343 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator GetBytesDecryptionDataAsync(string path, Action<byte[]> resultCallback)
		{
			return null;
		}

		// Token: 0x06008240 RID: 33344 RVA: 0x0000216A File Offset: 0x0000036A
		private byte[] GetBytesDecryptionDataCore(byte[] outData)
		{
			return null;
		}

		// Token: 0x06008241 RID: 33345 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetName(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRubyName(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDesc(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008244 RID: 33348 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRawDesc(int cardId)
		{
			return null;
		}

		// Token: 0x06008245 RID: 33349 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDescWithoutPendulum(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDescOfPendulum(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008247 RID: 33351 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDescWithoutPendulumForCardInfo(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008248 RID: 33352 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDescOfPendulumForCardInfo(int cardId, bool replaceAlnum = true)
		{
			return null;
		}

		// Token: 0x06008249 RID: 33353 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddChangeLineTag(ref string str)
		{
		}

		// Token: 0x0600824A RID: 33354 RVA: 0x0000216A File Offset: 0x0000036A
		private string getString(int id, Stream indexStream, Stream textStream)
		{
			return null;
		}

		// Token: 0x0600824B RID: 33355 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReplaceAlnum(string str)
		{
			return null;
		}

		// Token: 0x0600824C RID: 33356 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDialogText(int dlgtextId)
		{
			return null;
		}

		// Token: 0x0600824D RID: 33357 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetWordText(int wordId)
		{
			return null;
		}

		// Token: 0x0600824E RID: 33358 RVA: 0x0000216A File Offset: 0x0000036A
		private string getDescActiveEffect(int cardId, int numOfEfx)
		{
			return null;
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTextIDOfActiveEffect(int cardId, int numOfEfx)
		{
			return 0;
		}

		// Token: 0x06008250 RID: 33360 RVA: 0x000F6988 File Offset: 0x000F4B88
		public ValueTuple<int, int> GetActiveEffectPosInText(int textId)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06008251 RID: 33361 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardIdFromTextId(int textId)
		{
			return 0;
		}

		// Token: 0x06008252 RID: 33362 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPendulumTag()
		{
			return null;
		}

		// Token: 0x06008253 RID: 33363 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetAttributeText(Content.Attribute attr)
		{
			return null;
		}

		// Token: 0x06008254 RID: 33364 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetAttributeRuby(Content.Attribute attr)
		{
			return null;
		}

		// Token: 0x06008255 RID: 33365 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTypeText(Content.Type type)
		{
			return null;
		}

		// Token: 0x06008256 RID: 33366 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconText(Content.Icon icon)
		{
			return null;
		}

		// Token: 0x06008257 RID: 33367 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconFullText(int cardId)
		{
			return null;
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetMagicFullText(Content.Icon icon)
		{
			return null;
		}

		// Token: 0x06008259 RID: 33369 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTrapFullText(Content.Icon icon)
		{
			return null;
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetKindText(Content.Kind kind)
		{
			return null;
		}

		// Token: 0x0600825B RID: 33371 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSortName(int mrk)
		{
			return null;
		}

		// Token: 0x0600825C RID: 33372 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetAllCardList()
		{
			return null;
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExists(int cardid)
		{
			return false;
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsValid(int cardid)
		{
			return false;
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator coCreateCardList()
		{
			return null;
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x0000216D File Offset: 0x0000036D
		private void releaseAllCardList()
		{
		}

		// Token: 0x06008261 RID: 33377 RVA: 0x000F1669 File Offset: 0x000EF869
		public long GetCardGenre(int mrk)
		{
			return 0L;
		}

		// Token: 0x06008262 RID: 33378 RVA: 0x000029CC File Offset: 0x00000BCC
		private int readInt8(byte[] bytes, int pos)
		{
			return 0;
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x000029CC File Offset: 0x00000BCC
		private int readInt16(byte[] bytes, int pos)
		{
			return 0;
		}

		// Token: 0x06008264 RID: 33380 RVA: 0x000029CC File Offset: 0x00000BCC
		private int readInt32(byte[] bytes, int pos)
		{
			return 0;
		}

		// Token: 0x06008265 RID: 33381 RVA: 0x000F1669 File Offset: 0x000EF869
		private long readInt64(byte[] bytes, int pos)
		{
			return 0L;
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSearchName(byte[] data)
		{
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x0000216A File Offset: 0x0000036A
		private int[] createDeclarationCard(byte[] bytes)
		{
			return null;
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetDeclarationCard(string path)
		{
			return null;
		}

		// Token: 0x06008269 RID: 33385 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseDeclarationCard()
		{
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x0000216D File Offset: 0x0000036D
		private void createSortIdxList(byte[] bin)
		{
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x0000216D File Offset: 0x0000036D
		private void releaseSortIdxList()
		{
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSortIndexFromMRK(int mrk)
		{
			return 0;
		}

		// Token: 0x0600826D RID: 33389 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExtraDeckCard(int mrk)
		{
			return false;
		}

		// Token: 0x0600826E RID: 33390 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x0600826F RID: 33391 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsNormalMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEffectMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008271 RID: 33393 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTokenCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008272 RID: 33394 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRitualMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008273 RID: 33395 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLinkMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPendulumMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008275 RID: 33397 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsXyzMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008276 RID: 33398 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSyncMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008277 RID: 33399 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFusionMonsterCard(int cardId)
		{
			return false;
		}

		// Token: 0x06008278 RID: 33400
		[PreserveSig]
		private static extern int DLL_SetCardProperty(byte[] data, int size);

		// Token: 0x06008279 RID: 33401
		[PreserveSig]
		private static extern void DLL_SetInternalID(byte[] data);

		// Token: 0x0600827A RID: 33402
		[PreserveSig]
		private static extern void DLL_SetCardSame(byte[] data, int size);

		// Token: 0x0600827B RID: 33403
		[PreserveSig]
		private static extern void DLL_SetCardNamed(byte[] data);

		// Token: 0x0600827C RID: 33404
		[PreserveSig]
		private static extern void DLL_SetCardLink(byte[] data, int size);

		// Token: 0x0600827D RID: 33405
		[PreserveSig]
		private static extern void DLL_SetCardGenre(byte[] data);

		// Token: 0x0600827E RID: 33406
		[PreserveSig]
		private static extern int DLL_CardGetInternalID(int cardId);

		// Token: 0x0600827F RID: 33407
		[PreserveSig]
		private static extern int DLL_CardGetType(int cardId);

		// Token: 0x06008280 RID: 33408
		[PreserveSig]
		private static extern int DLL_CardGetAttr(int cardId);

		// Token: 0x06008281 RID: 33409
		[PreserveSig]
		private static extern int DLL_CardGetStar(int cardId);

		// Token: 0x06008282 RID: 33410
		[PreserveSig]
		private static extern int DLL_CardGetLevel(int cardId);

		// Token: 0x06008283 RID: 33411
		[PreserveSig]
		private static extern int DLL_CardGetRank(int cardId);

		// Token: 0x06008284 RID: 33412
		[PreserveSig]
		private static extern int DLL_CardGetScaleL(int cardId);

		// Token: 0x06008285 RID: 33413
		[PreserveSig]
		private static extern int DLL_CardGetScaleR(int cardId);

		// Token: 0x06008286 RID: 33414
		[PreserveSig]
		private static extern int DLL_CardGetLinkNum(int cardId);

		// Token: 0x06008287 RID: 33415
		[PreserveSig]
		private static extern int DLL_CardGetLinkMask(int cardId);

		// Token: 0x06008288 RID: 33416
		[PreserveSig]
		private static extern int DLL_CardGetIcon(int cardId);

		// Token: 0x06008289 RID: 33417
		[PreserveSig]
		private static extern int DLL_CardGetFrame(int cardId);

		// Token: 0x0600828A RID: 33418
		[PreserveSig]
		private static extern int DLL_CardGetKind(int cardId);

		// Token: 0x0600828B RID: 33419
		[PreserveSig]
		private static extern int DLL_CardGetAtk(int cardId);

		// Token: 0x0600828C RID: 33420
		[PreserveSig]
		private static extern int DLL_CardGetDef(int cardId);

		// Token: 0x0600828D RID: 33421
		[PreserveSig]
		private static extern int DLL_CardGetAtk2(int cardId);

		// Token: 0x0600828E RID: 33422
		[PreserveSig]
		private static extern int DLL_CardGetDef2(int cardId);

		// Token: 0x0600828F RID: 33423
		[PreserveSig]
		private static extern int DLL_CardGetLimitation(int cardId);

		// Token: 0x06008290 RID: 33424
		[PreserveSig]
		private static extern int DLL_CardIsThisCardGenre(int cardId, int genreId);

		// Token: 0x06008291 RID: 33425
		[PreserveSig]
		private static extern int DLL_CardIsThisSameCard(int cardA, int cardB);

		// Token: 0x06008292 RID: 33426
		[PreserveSig]
		private static extern int DLL_CardGetOriginalID(int cardId);

		// Token: 0x06008293 RID: 33427
		[PreserveSig]
		private static extern int DLL_CardGetOriginalID2(int cardId);

		// Token: 0x06008294 RID: 33428
		[PreserveSig]
		private static extern int DLL_CardGetAlterID(int cardId);

		// Token: 0x06008295 RID: 33429
		[PreserveSig]
		private static extern int DLL_CardGetAltCardID(int cardId, int alterID);

		// Token: 0x06008296 RID: 33430
		[PreserveSig]
		private static extern int DLL_CardCheckName(int cardId, int nameType);

		// Token: 0x06008297 RID: 33431
		[PreserveSig]
		private static extern int DLL_CardGetLinkCards(int cardId, IntPtr pLinkID);

		// Token: 0x06008298 RID: 33432
		[PreserveSig]
		private static extern int DLL_CardGetBasicVal(int cardId, ref Engine.BasicVal pVal);

		// Token: 0x06008299 RID: 33433
		[PreserveSig]
		private static extern int DLL_CardIsThisTunerMonster(int cardId);

		// Token: 0x0600829A RID: 33434 RVA: 0x000029CC File Offset: 0x00000BCC
		private int setCardProperty(byte[] data)
		{
			return 0;
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x0000216D File Offset: 0x0000036D
		private void setInternalID(byte[] data)
		{
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x000029CC File Offset: 0x00000BCC
		private int getInternalID(int cardId)
		{
			return 0;
		}

		// Token: 0x0600829D RID: 33437 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardSame(byte[] data)
		{
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardNamed(byte[] data)
		{
		}

		// Token: 0x0600829F RID: 33439 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardLink(byte[] data)
		{
		}

		// Token: 0x060082A0 RID: 33440 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardGenre(byte[] data)
		{
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x000029CC File Offset: 0x00000BCC
		public Content.Type GetType(int cardId)
		{
			return Content.Type.Null;
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x000029CC File Offset: 0x00000BCC
		public Content.Attribute GetAttr(int cardId)
		{
			return Content.Attribute.Null;
		}

		// Token: 0x060082A3 RID: 33443 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetStar(int cardId)
		{
			return 0;
		}

		// Token: 0x060082A4 RID: 33444 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardStar(int cardId)
		{
			return 0;
		}

		// Token: 0x060082A5 RID: 33445 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsNoStarCard(int cardId)
		{
			return false;
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLevel(int cardId)
		{
			return 0;
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetRank(int cardId)
		{
			return 0;
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetScaleL(int cardId)
		{
			return 0;
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetScaleR(int cardId)
		{
			return 0;
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLinkNum(int cardId)
		{
			return 0;
		}

		// Token: 0x060082AB RID: 33451 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLinkMask(int cardId)
		{
			return 0;
		}

		// Token: 0x060082AC RID: 33452 RVA: 0x000029CC File Offset: 0x00000BCC
		public Content.Icon GetIcon(int cardId)
		{
			return Content.Icon.Null;
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x000029CC File Offset: 0x00000BCC
		public Content.Frame GetFrame(int cardId)
		{
			return Content.Frame.Normal;
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x000029CC File Offset: 0x00000BCC
		public Content.Kind GetKind(int cardId)
		{
			return Content.Kind.Normal;
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAtk(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDef(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAtk2(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B2 RID: 33458 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDef2(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B3 RID: 33459 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsThisCardGenre(int cardId, Content.Genre genre)
		{
			return false;
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsThisSameCard(int cardA, int cardB)
		{
			return false;
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsThisTunerMonster(int cardId)
		{
			return false;
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetOriginalID(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetOriginalID2(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAlterID(int cardId)
		{
			return 0;
		}

		// Token: 0x060082B9 RID: 33465 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAltCardID(int cardId, int altId)
		{
			return 0;
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckCardName(int cardId, Content.NameType type)
		{
			return false;
		}

		// Token: 0x060082BB RID: 33467 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetLinkCards(int cardId)
		{
			return null;
		}

		// Token: 0x060082BC RID: 33468 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetLinkCardsOfAvailable(int cardId)
		{
			return null;
		}

		// Token: 0x060082BD RID: 33469 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetBasicVal(int cardId, ref Engine.BasicVal val)
		{
		}

		// Token: 0x0400BA87 RID: 47751
		private static Content s_instance;

		// Token: 0x0400BA88 RID: 47752
		private const int Streams_CardIndex = 0;

		// Token: 0x0400BA89 RID: 47753
		private const int Streams_CardName = 1;

		// Token: 0x0400BA8A RID: 47754
		private const int Streams_CardDesc = 2;

		// Token: 0x0400BA8B RID: 47755
		private const int Streams_WordIndex = 3;

		// Token: 0x0400BA8C RID: 47756
		private const int Streams_WordText = 4;

		// Token: 0x0400BA8D RID: 47757
		private const int Streams_DialogIndex = 5;

		// Token: 0x0400BA8E RID: 47758
		private const int Streams_DialogText = 6;

		// Token: 0x0400BA8F RID: 47759
		private const int Streams_CardPidx = 7;

		// Token: 0x0400BA90 RID: 47760
		private const int Streams_CardPart = 8;

		// Token: 0x0400BA91 RID: 47761
		private const int Streams_RubyIndex = 9;

		// Token: 0x0400BA92 RID: 47762
		private const int Streams_RubyName = 10;

		// Token: 0x0400BA93 RID: 47763
		private const int Streams_Length = 11;

		// Token: 0x0400BA94 RID: 47764
		private readonly string[] DataPathForStream;

		// Token: 0x0400BA95 RID: 47765
		private Stream[] cardStreams;

		// Token: 0x0400BA96 RID: 47766
		private const string IntIdPath = "Card/Data/b0ea6a33a5e8917c/#/CARD_IntID";

		// Token: 0x0400BA97 RID: 47767
		private const string PropPath = "Card/Data/b0ea6a33a5e8917c/#/CARD_Prop";

		// Token: 0x0400BA98 RID: 47768
		private const string SamePath = "Card/Data/b0ea6a33a5e8917c/MD/CARD_Same";

		// Token: 0x0400BA99 RID: 47769
		private const string GenrePath = "Card/Data/b0ea6a33a5e8917c/#/CARD_Genre";

		// Token: 0x0400BA9A RID: 47770
		private const string NamedPath = "Card/Data/b0ea6a33a5e8917c/#/CARD_Named";

		// Token: 0x0400BA9B RID: 47771
		private const string LinkPath = "Card/Data/b0ea6a33a5e8917c/MD/CARD_Link";

		// Token: 0x0400BA9C RID: 47772
		private const string SearchNamePath = "Card/Data/b0ea6a33a5e8917c/MD/CARD_SearchName";

		// Token: 0x0400BA9D RID: 47773
		public const string NEWLINE_TAG = "\n";

		// Token: 0x0400BA9E RID: 47774
		private byte[] cardIntIds;

		// Token: 0x0400BA9F RID: 47775
		private byte[] cardProps;

		// Token: 0x0400BAA0 RID: 47776
		private byte[] cardSame;

		// Token: 0x0400BAA1 RID: 47777
		private byte[] cardNamed;

		// Token: 0x0400BAA2 RID: 47778
		private byte[] cardLink;

		// Token: 0x0400BAA3 RID: 47779
		private byte[] cardGenre;

		// Token: 0x0400BAA4 RID: 47780
		private const string BuiltinCardData = "Card/Data/b0ea6a33a5e8917c/MD/builtin_card";

		// Token: 0x0400BAA5 RID: 47781
		public const string DeclaraCardAllPath = "Card/Data/b0ea6a33a5e8917c/MD/cards_all";

		// Token: 0x0400BAA6 RID: 47782
		public const string DeclaraCardDeckPath = "Card/Data/b0ea6a33a5e8917c/MD/cards_in_maindeck";

		// Token: 0x0400BAA7 RID: 47783
		public const string DeclaraCardMonstPath = "Card/Data/b0ea6a33a5e8917c/MD/monsters_in_maindeck";

		// Token: 0x0400BAA8 RID: 47784
		public const string DeclaraCardMonst2Path = "Card/Data/b0ea6a33a5e8917c/MD/all_monsters";

		// Token: 0x0400BAA9 RID: 47785
		public const string DeclaraCardGadgetPath = "Card/Data/b0ea6a33a5e8917c/MD/all_gadget_monsters";

		// Token: 0x0400BAAA RID: 47786
		private Dictionary<string, int[]> dicDeclaraCard;

		// Token: 0x0400BAAB RID: 47787
		private Dictionary<int, string> dicSearchName;

		// Token: 0x0400BAAC RID: 47788
		private int cardNum;

		// Token: 0x0400BAAD RID: 47789
		private bool isReady;

		// Token: 0x0400BAAE RID: 47790
		private bool isLoadSuccess;

		// Token: 0x0400BAAF RID: 47791
		private bool m_AdditionalChangeLineTag;

		// Token: 0x0400BAB0 RID: 47792
		private const string dclsymbol = "\n\n";

		// Token: 0x0400BAB1 RID: 47793
		private const string clsymbol = "\n";

		// Token: 0x0400BAB2 RID: 47794
		private const string periodsymbol = "\ufffd";

		// Token: 0x0400BAB3 RID: 47795
		private const string periodclsymbol = "\ufffd";

		// Token: 0x0400BAB4 RID: 47796
		private const string periodclsymbolblacket = "。";

		// Token: 0x0400BAB5 RID: 47797
		private const string periodblacket = "\ufffd";

		// Token: 0x0400BAB6 RID: 47798
		private static string[] spiltTags;

		// Token: 0x0400BAB7 RID: 47799
		private static string[] spiltTags_Korean;

		// Token: 0x0400BAB8 RID: 47800
		private List<int> allCardList;

		// Token: 0x0400BAB9 RID: 47801
		private Dictionary<int, int> sortIdx;

		// Token: 0x0400BABA RID: 47802
		private int m_iCryptoKey;

		// Token: 0x0400BABB RID: 47803
		public const float Witdh = 5.9f;

		// Token: 0x0400BABC RID: 47804
		public const float Height = 8.6f;

		// Token: 0x0400BABD RID: 47805
		public const float Aspect = 0.6860465f;

		// Token: 0x0400BABE RID: 47806
		public const int AttributeWordStart = 10;

		// Token: 0x0400BABF RID: 47807
		public const int TypeWordStart = 100;

		// Token: 0x0400BAC0 RID: 47808
		public const int IconWordStart = 50;

		// Token: 0x0400BAC1 RID: 47809
		public const int FrameNum = 20;

		// Token: 0x0400BAC2 RID: 47810
		public const int KindWordStart = 200;

		// Token: 0x0400BAC3 RID: 47811
		public const int GenreMax = 49;

		// Token: 0x0400BAC4 RID: 47812
		private const string LIBNAME = "duel";

		// Token: 0x0400BAC5 RID: 47813
		private List<int> noStarCardID;

		// Token: 0x02001117 RID: 4375
		public struct Property
		{
			// Token: 0x170010A6 RID: 4262
			// (get) Token: 0x060082BF RID: 33471 RVA: 0x000029CC File Offset: 0x00000BCC
			public int MRK
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010A7 RID: 4263
			// (get) Token: 0x060082C0 RID: 33472 RVA: 0x000029CC File Offset: 0x00000BCC
			public Content.Kind Kind
			{
				get
				{
					return Content.Kind.Normal;
				}
			}

			// Token: 0x170010A8 RID: 4264
			// (get) Token: 0x060082C1 RID: 33473 RVA: 0x000029CC File Offset: 0x00000BCC
			public Content.Attribute Attr
			{
				get
				{
					return Content.Attribute.Null;
				}
			}

			// Token: 0x170010A9 RID: 4265
			// (get) Token: 0x060082C2 RID: 33474 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Level
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010AA RID: 4266
			// (get) Token: 0x060082C3 RID: 33475 RVA: 0x000029CC File Offset: 0x00000BCC
			public Content.LvType LvType
			{
				get
				{
					return Content.LvType.Null;
				}
			}

			// Token: 0x170010AB RID: 4267
			// (get) Token: 0x060082C4 RID: 33476 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Atk
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010AC RID: 4268
			// (get) Token: 0x060082C5 RID: 33477 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Def
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010AD RID: 4269
			// (get) Token: 0x060082C6 RID: 33478 RVA: 0x000029CC File Offset: 0x00000BCC
			public Content.Icon Icon
			{
				get
				{
					return Content.Icon.Null;
				}
			}

			// Token: 0x170010AE RID: 4270
			// (get) Token: 0x060082C7 RID: 33479 RVA: 0x000029CC File Offset: 0x00000BCC
			public Content.Type Type
			{
				get
				{
					return Content.Type.Null;
				}
			}

			// Token: 0x170010AF RID: 4271
			// (get) Token: 0x060082C8 RID: 33480 RVA: 0x000029CC File Offset: 0x00000BCC
			public int ScaleL
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170010B0 RID: 4272
			// (get) Token: 0x060082C9 RID: 33481 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Exist
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400BAC6 RID: 47814
			private ushort mrk;

			// Token: 0x0400BAC7 RID: 47815
			private BitVector32.Section kind;

			// Token: 0x0400BAC8 RID: 47816
			private BitVector32.Section attr;

			// Token: 0x0400BAC9 RID: 47817
			private BitVector32.Section level;

			// Token: 0x0400BACA RID: 47818
			private BitVector32.Section lvtype;

			// Token: 0x0400BACB RID: 47819
			private BitVector32.Section attack;

			// Token: 0x0400BACC RID: 47820
			private BitVector32.Section defence;

			// Token: 0x0400BACD RID: 47821
			private BitVector32.Section icon;

			// Token: 0x0400BACE RID: 47822
			private BitVector32.Section type;

			// Token: 0x0400BACF RID: 47823
			private BitVector32.Section scaleL;

			// Token: 0x0400BAD0 RID: 47824
			private BitVector32.Section exist;

			// Token: 0x0400BAD1 RID: 47825
			private BitVector32 bit1;

			// Token: 0x0400BAD2 RID: 47826
			private BitVector32 bit2;
		}

		// Token: 0x02001118 RID: 4376
		public enum Rarity
		{
			// Token: 0x0400BAD4 RID: 47828
			None,
			// Token: 0x0400BAD5 RID: 47829
			Normal,
			// Token: 0x0400BAD6 RID: 47830
			Rare,
			// Token: 0x0400BAD7 RID: 47831
			SuperRare,
			// Token: 0x0400BAD8 RID: 47832
			UltraRare
		}

		// Token: 0x02001119 RID: 4377
		public enum Kirarity
		{
			// Token: 0x0400BADA RID: 47834
			None,
			// Token: 0x0400BADB RID: 47835
			Normal,
			// Token: 0x0400BADC RID: 47836
			Gold,
			// Token: 0x0400BADD RID: 47837
			Kira,
			// Token: 0x0400BADE RID: 47838
			Holo
		}

		// Token: 0x0200111A RID: 4378
		public enum Attribute
		{
			// Token: 0x0400BAE0 RID: 47840
			Null,
			// Token: 0x0400BAE1 RID: 47841
			Light,
			// Token: 0x0400BAE2 RID: 47842
			Dark,
			// Token: 0x0400BAE3 RID: 47843
			Water,
			// Token: 0x0400BAE4 RID: 47844
			Fire,
			// Token: 0x0400BAE5 RID: 47845
			Earth,
			// Token: 0x0400BAE6 RID: 47846
			Wind,
			// Token: 0x0400BAE7 RID: 47847
			God,
			// Token: 0x0400BAE8 RID: 47848
			Magic,
			// Token: 0x0400BAE9 RID: 47849
			Trap
		}

		// Token: 0x0200111B RID: 4379
		public enum Type
		{
			// Token: 0x0400BAEB RID: 47851
			Null,
			// Token: 0x0400BAEC RID: 47852
			Dragon,
			// Token: 0x0400BAED RID: 47853
			Undead,
			// Token: 0x0400BAEE RID: 47854
			Devil,
			// Token: 0x0400BAEF RID: 47855
			Flame,
			// Token: 0x0400BAF0 RID: 47856
			Poseidon,
			// Token: 0x0400BAF1 RID: 47857
			Sandrock,
			// Token: 0x0400BAF2 RID: 47858
			Machine,
			// Token: 0x0400BAF3 RID: 47859
			Fish,
			// Token: 0x0400BAF4 RID: 47860
			Dinosaurs,
			// Token: 0x0400BAF5 RID: 47861
			Insect,
			// Token: 0x0400BAF6 RID: 47862
			Beast,
			// Token: 0x0400BAF7 RID: 47863
			BeastBtl,
			// Token: 0x0400BAF8 RID: 47864
			Botanical,
			// Token: 0x0400BAF9 RID: 47865
			Aquarius,
			// Token: 0x0400BAFA RID: 47866
			Soldier,
			// Token: 0x0400BAFB RID: 47867
			Bird,
			// Token: 0x0400BAFC RID: 47868
			Angel,
			// Token: 0x0400BAFD RID: 47869
			Wizard,
			// Token: 0x0400BAFE RID: 47870
			Thunder,
			// Token: 0x0400BAFF RID: 47871
			Reptiles,
			// Token: 0x0400BB00 RID: 47872
			Psychic,
			// Token: 0x0400BB01 RID: 47873
			Mystdragon,
			// Token: 0x0400BB02 RID: 47874
			Cyverse,
			// Token: 0x0400BB03 RID: 47875
			God,
			// Token: 0x0400BB04 RID: 47876
			Illusioner,
			// Token: 0x0400BB05 RID: 47877
			Creator,
			// Token: 0x0400BB06 RID: 47878
			Magic = 0,
			// Token: 0x0400BB07 RID: 47879
			Trap = 0
		}

		// Token: 0x0200111C RID: 4380
		public enum Icon
		{
			// Token: 0x0400BB09 RID: 47881
			Null,
			// Token: 0x0400BB0A RID: 47882
			Counter,
			// Token: 0x0400BB0B RID: 47883
			Field,
			// Token: 0x0400BB0C RID: 47884
			Equip,
			// Token: 0x0400BB0D RID: 47885
			Continuous,
			// Token: 0x0400BB0E RID: 47886
			QuickPlay,
			// Token: 0x0400BB0F RID: 47887
			Ritual
		}

		// Token: 0x0200111D RID: 4381
		public enum Frame
		{
			// Token: 0x0400BB11 RID: 47889
			Normal,
			// Token: 0x0400BB12 RID: 47890
			Effect,
			// Token: 0x0400BB13 RID: 47891
			Ritual,
			// Token: 0x0400BB14 RID: 47892
			Fusion,
			// Token: 0x0400BB15 RID: 47893
			Oberisk,
			// Token: 0x0400BB16 RID: 47894
			Osiris,
			// Token: 0x0400BB17 RID: 47895
			Ra,
			// Token: 0x0400BB18 RID: 47896
			Magic,
			// Token: 0x0400BB19 RID: 47897
			Trap,
			// Token: 0x0400BB1A RID: 47898
			Token,
			// Token: 0x0400BB1B RID: 47899
			Sync,
			// Token: 0x0400BB1C RID: 47900
			Dsync,
			// Token: 0x0400BB1D RID: 47901
			Xyz,
			// Token: 0x0400BB1E RID: 47902
			Pend,
			// Token: 0x0400BB1F RID: 47903
			PendFx,
			// Token: 0x0400BB20 RID: 47904
			XyzPend,
			// Token: 0x0400BB21 RID: 47905
			SyncPend,
			// Token: 0x0400BB22 RID: 47906
			FusionPend,
			// Token: 0x0400BB23 RID: 47907
			Link,
			// Token: 0x0400BB24 RID: 47908
			RitualPend
		}

		// Token: 0x0200111E RID: 4382
		public enum Kind
		{
			// Token: 0x0400BB26 RID: 47910
			Normal,
			// Token: 0x0400BB27 RID: 47911
			Effect,
			// Token: 0x0400BB28 RID: 47912
			Fusion,
			// Token: 0x0400BB29 RID: 47913
			FusionFx,
			// Token: 0x0400BB2A RID: 47914
			Ritual,
			// Token: 0x0400BB2B RID: 47915
			RitualFx,
			// Token: 0x0400BB2C RID: 47916
			Toon,
			// Token: 0x0400BB2D RID: 47917
			Spirit,
			// Token: 0x0400BB2E RID: 47918
			Union,
			// Token: 0x0400BB2F RID: 47919
			Dual,
			// Token: 0x0400BB30 RID: 47920
			Token,
			// Token: 0x0400BB31 RID: 47921
			God,
			// Token: 0x0400BB32 RID: 47922
			Dummy,
			// Token: 0x0400BB33 RID: 47923
			Magic,
			// Token: 0x0400BB34 RID: 47924
			Trap,
			// Token: 0x0400BB35 RID: 47925
			Tuner,
			// Token: 0x0400BB36 RID: 47926
			TunerFx,
			// Token: 0x0400BB37 RID: 47927
			Sync,
			// Token: 0x0400BB38 RID: 47928
			SyncFx,
			// Token: 0x0400BB39 RID: 47929
			SyncTuner,
			// Token: 0x0400BB3A RID: 47930
			Dtuner,
			// Token: 0x0400BB3B RID: 47931
			Dsync,
			// Token: 0x0400BB3C RID: 47932
			Xyz,
			// Token: 0x0400BB3D RID: 47933
			XyzFx,
			// Token: 0x0400BB3E RID: 47934
			Flip,
			// Token: 0x0400BB3F RID: 47935
			Pend,
			// Token: 0x0400BB40 RID: 47936
			PendFx,
			// Token: 0x0400BB41 RID: 47937
			SpEffect,
			// Token: 0x0400BB42 RID: 47938
			SpToon,
			// Token: 0x0400BB43 RID: 47939
			SpSpirit,
			// Token: 0x0400BB44 RID: 47940
			SpTuner,
			// Token: 0x0400BB45 RID: 47941
			SpDtuner,
			// Token: 0x0400BB46 RID: 47942
			FlipTuner,
			// Token: 0x0400BB47 RID: 47943
			PendTuner,
			// Token: 0x0400BB48 RID: 47944
			XyzPend,
			// Token: 0x0400BB49 RID: 47945
			PendFlip,
			// Token: 0x0400BB4A RID: 47946
			SyncPend,
			// Token: 0x0400BB4B RID: 47947
			UnionTuner,
			// Token: 0x0400BB4C RID: 47948
			RitualSpirit,
			// Token: 0x0400BB4D RID: 47949
			FusionTuner,
			// Token: 0x0400BB4E RID: 47950
			SpPend,
			// Token: 0x0400BB4F RID: 47951
			FusionPend,
			// Token: 0x0400BB50 RID: 47952
			Link,
			// Token: 0x0400BB51 RID: 47953
			LinkFx,
			// Token: 0x0400BB52 RID: 47954
			PendNTuner,
			// Token: 0x0400BB53 RID: 47955
			PendSpirit,
			// Token: 0x0400BB54 RID: 47956
			Maximum,
			// Token: 0x0400BB55 RID: 47957
			RirualTunerFX,
			// Token: 0x0400BB56 RID: 47958
			FusionTunerFX,
			// Token: 0x0400BB57 RID: 47959
			TokenTuner,
			// Token: 0x0400BB58 RID: 47960
			R_Fusion,
			// Token: 0x0400BB59 RID: 47961
			R_FusionFX,
			// Token: 0x0400BB5A RID: 47962
			RitualPend,
			// Token: 0x0400BB5B RID: 47963
			RitualFlip
		}

		// Token: 0x0200111F RID: 4383
		public enum SubKind
		{
			// Token: 0x0400BB5D RID: 47965
			Null,
			// Token: 0x0400BB5E RID: 47966
			NoFx,
			// Token: 0x0400BB5F RID: 47967
			Normal,
			// Token: 0x0400BB60 RID: 47968
			Effect,
			// Token: 0x0400BB61 RID: 47969
			Flip,
			// Token: 0x0400BB62 RID: 47970
			Toon,
			// Token: 0x0400BB63 RID: 47971
			Spirit,
			// Token: 0x0400BB64 RID: 47972
			Union,
			// Token: 0x0400BB65 RID: 47973
			Dual,
			// Token: 0x0400BB66 RID: 47974
			Maximum
		}

		// Token: 0x02001120 RID: 4384
		public enum LvType
		{
			// Token: 0x0400BB68 RID: 47976
			Null,
			// Token: 0x0400BB69 RID: 47977
			Level,
			// Token: 0x0400BB6A RID: 47978
			Rank,
			// Token: 0x0400BB6B RID: 47979
			Link,
			// Token: 0x0400BB6C RID: 47980
			LvMax = 12,
			// Token: 0x0400BB6D RID: 47981
			LvMask = 8190,
			// Token: 0x0400BB6E RID: 47982
			RankMax = 13,
			// Token: 0x0400BB6F RID: 47983
			RankMask = 16382,
			// Token: 0x0400BB70 RID: 47984
			LinkMax = 8
		}

		// Token: 0x02001121 RID: 4385
		public enum LinkMarker
		{
			// Token: 0x0400BB72 RID: 47986
			UpLeft,
			// Token: 0x0400BB73 RID: 47987
			Up,
			// Token: 0x0400BB74 RID: 47988
			UpRight,
			// Token: 0x0400BB75 RID: 47989
			Left,
			// Token: 0x0400BB76 RID: 47990
			Right,
			// Token: 0x0400BB77 RID: 47991
			DownLeft,
			// Token: 0x0400BB78 RID: 47992
			Down,
			// Token: 0x0400BB79 RID: 47993
			DownRight
		}

		// Token: 0x02001122 RID: 4386
		[Flags]
		public enum LinkMarkerBit
		{
			// Token: 0x0400BB7B RID: 47995
			UpLeft = 1,
			// Token: 0x0400BB7C RID: 47996
			Up = 2,
			// Token: 0x0400BB7D RID: 47997
			UpRight = 4,
			// Token: 0x0400BB7E RID: 47998
			Left = 8,
			// Token: 0x0400BB7F RID: 47999
			Right = 16,
			// Token: 0x0400BB80 RID: 48000
			DownLeft = 32,
			// Token: 0x0400BB81 RID: 48001
			Down = 64,
			// Token: 0x0400BB82 RID: 48002
			DownRight = 128
		}

		// Token: 0x02001123 RID: 4387
		public enum Genre
		{
			// Token: 0x0400BB84 RID: 48004
			LpUp,
			// Token: 0x0400BB85 RID: 48005
			LpDown,
			// Token: 0x0400BB86 RID: 48006
			Draw,
			// Token: 0x0400BB87 RID: 48007
			SpSummon,
			// Token: 0x0400BB88 RID: 48008
			Disable,
			// Token: 0x0400BB89 RID: 48009
			DeckSearch,
			// Token: 0x0400BB8A RID: 48010
			UseGrave,
			// Token: 0x0400BB8B RID: 48011
			Power,
			// Token: 0x0400BB8C RID: 48012
			Position,
			// Token: 0x0400BB8D RID: 48013
			Control,
			// Token: 0x0400BB8E RID: 48014
			BreakMonst,
			// Token: 0x0400BB8F RID: 48015
			BreakMagic,
			// Token: 0x0400BB90 RID: 48016
			HandDes,
			// Token: 0x0400BB91 RID: 48017
			DeckDes,
			// Token: 0x0400BB92 RID: 48018
			RemoveCard,
			// Token: 0x0400BB93 RID: 48019
			CardBack,
			// Token: 0x0400BB94 RID: 48020
			Spear,
			// Token: 0x0400BB95 RID: 48021
			DirectAtk,
			// Token: 0x0400BB96 RID: 48022
			ManyAtk,
			// Token: 0x0400BB97 RID: 48023
			Unbreak,
			// Token: 0x0400BB98 RID: 48024
			LimitAtk,
			// Token: 0x0400BB99 RID: 48025
			CantSummon,
			// Token: 0x0400BB9A RID: 48026
			Reverse,
			// Token: 0x0400BB9B RID: 48027
			Toon,
			// Token: 0x0400BB9C RID: 48028
			Spirit,
			// Token: 0x0400BB9D RID: 48029
			Union,
			// Token: 0x0400BB9E RID: 48030
			Dual,
			// Token: 0x0400BB9F RID: 48031
			LevelUp,
			// Token: 0x0400BBA0 RID: 48032
			Original,
			// Token: 0x0400BBA1 RID: 48033
			Fusion,
			// Token: 0x0400BBA2 RID: 48034
			Ritual,
			// Token: 0x0400BBA3 RID: 48035
			Token,
			// Token: 0x0400BBA4 RID: 48036
			Counter,
			// Token: 0x0400BBA5 RID: 48037
			Gamble,
			// Token: 0x0400BBA6 RID: 48038
			Attribute,
			// Token: 0x0400BBA7 RID: 48039
			Type,
			// Token: 0x0400BBA8 RID: 48040
			Tuner,
			// Token: 0x0400BBA9 RID: 48041
			Sync,
			// Token: 0x0400BBAA RID: 48042
			DropGrave,
			// Token: 0x0400BBAB RID: 48043
			Normal,
			// Token: 0x0400BBAC RID: 48044
			AttrLight,
			// Token: 0x0400BBAD RID: 48045
			AttrDark,
			// Token: 0x0400BBAE RID: 48046
			AttrEarth,
			// Token: 0x0400BBAF RID: 48047
			AttrWater,
			// Token: 0x0400BBB0 RID: 48048
			AttrFire,
			// Token: 0x0400BBB1 RID: 48049
			AttrWind,
			// Token: 0x0400BBB2 RID: 48050
			Xyz,
			// Token: 0x0400BBB3 RID: 48051
			LvUpDown,
			// Token: 0x0400BBB4 RID: 48052
			Pendulum,
			// Token: 0x0400BBB5 RID: 48053
			Link,
			// Token: 0x0400BBB6 RID: 48054
			HalfDamage
		}

		// Token: 0x02001124 RID: 4388
		public enum NameType
		{
			// Token: 0x0400BBB8 RID: 48056
			Null,
			// Token: 0x0400BBB9 RID: 48057
			Toon,
			// Token: 0x0400BBBA RID: 48058
			Demon,
			// Token: 0x0400BBBB RID: 48059
			GraveKeeper,
			// Token: 0x0400BBBC RID: 48060
			Guardian,
			// Token: 0x0400BBBD RID: 48061
			DarkScorpion,
			// Token: 0x0400BBBE RID: 48062
			Amazoness,
			// Token: 0x0400BBBF RID: 48063
			Ninja,
			// Token: 0x0400BBC0 RID: 48064
			Level,
			// Token: 0x0400BBC1 RID: 48065
			EHero,
			// Token: 0x0400BBC2 RID: 48066
			DHero,
			// Token: 0x0400BBC3 RID: 48067
			NeosMaterial,
			// Token: 0x0400BBC4 RID: 48068
			NeosFusion,
			// Token: 0x0400BBC5 RID: 48069
			Neos,
			// Token: 0x0400BBC6 RID: 48070
			Ojama,
			// Token: 0x0400BBC7 RID: 48071
			Battery,
			// Token: 0x0400BBC8 RID: 48072
			DarkWorld,
			// Token: 0x0400BBC9 RID: 48073
			BES,
			// Token: 0x0400BBCA RID: 48074
			Antique,
			// Token: 0x0400BBCB RID: 48075
			Sphinx,
			// Token: 0x0400BBCC RID: 48076
			Machiners,
			// Token: 0x0400BBCD RID: 48077
			Harpie,
			// Token: 0x0400BBCE RID: 48078
			Roid,
			// Token: 0x0400BBCF RID: 48079
			Vehicloid,
			// Token: 0x0400BBD0 RID: 48080
			NeoSpacian,
			// Token: 0x0400BBD1 RID: 48081
			Cocoon,
			// Token: 0x0400BBD2 RID: 48082
			Alien,
			// Token: 0x0400BBD3 RID: 48083
			MythicalBeast,
			// Token: 0x0400BBD4 RID: 48084
			Hero,
			// Token: 0x0400BBD5 RID: 48085
			AllureQueen,
			// Token: 0x0400BBD6 RID: 48086
			Gadget,
			// Token: 0x0400BBD7 RID: 48087
			SixSamurai,
			// Token: 0x0400BBD8 RID: 48088
			Jewel,
			// Token: 0x0400BBD9 RID: 48089
			Volcanic,
			// Token: 0x0400BBDA RID: 48090
			BlazeCanon,
			// Token: 0x0400BBDB RID: 48091
			Venom,
			// Token: 0x0400BBDC RID: 48092
			Cloudian,
			// Token: 0x0400BBDD RID: 48093
			Gladial,
			// Token: 0x0400BBDE RID: 48094
			Weapon,
			// Token: 0x0400BBDF RID: 48095
			Takemitsu,
			// Token: 0x0400BBE0 RID: 48096
			EvHero,
			// Token: 0x0400BBE1 RID: 48097
			Drunk,
			// Token: 0x0400BBE2 RID: 48098
			Arcana,
			// Token: 0x0400BBE3 RID: 48099
			Fossil,
			// Token: 0x0400BBE4 RID: 48100
			Gunner,
			// Token: 0x0400BBE5 RID: 48101
			Forbidden,
			// Token: 0x0400BBE6 RID: 48102
			Rainbow,
			// Token: 0x0400BBE7 RID: 48103
			CyberFusion,
			// Token: 0x0400BBE8 RID: 48104
			IceBarrier,
			// Token: 0x0400BBE9 RID: 48105
			AOJ,
			// Token: 0x0400BBEA RID: 48106
			Saber,
			// Token: 0x0400BBEB RID: 48107
			Worm,
			// Token: 0x0400BBEC RID: 48108
			LightLord,
			// Token: 0x0400BBED RID: 48109
			Frog,
			// Token: 0x0400BBEE RID: 48110
			Nitro,
			// Token: 0x0400BBEF RID: 48111
			Genex,
			// Token: 0x0400BBF0 RID: 48112
			MistValley,
			// Token: 0x0400BBF1 RID: 48113
			Flamebell,
			// Token: 0x0400BBF2 RID: 48114
			NeosNHero,
			// Token: 0x0400BBF3 RID: 48115
			Deformer,
			// Token: 0x0400BBF4 RID: 48116
			Chain,
			// Token: 0x0400BBF5 RID: 48117
			Natul,
			// Token: 0x0400BBF6 RID: 48118
			Clear,
			// Token: 0x0400BBF7 RID: 48119
			RedEyes,
			// Token: 0x0400BBF8 RID: 48120
			BlackFeather,
			// Token: 0x0400BBF9 RID: 48121
			SlashBuster,
			// Token: 0x0400BBFA RID: 48122
			Roaring,
			// Token: 0x0400BBFB RID: 48123
			Jurac,
			// Token: 0x0400BBFC RID: 48124
			RealGenex,
			// Token: 0x0400BBFD RID: 48125
			EarthBindGod,
			// Token: 0x0400BBFE RID: 48126
			Koakimail,
			// Token: 0x0400BBFF RID: 48127
			Infernity,
			// Token: 0x0400BC00 RID: 48128
			XSaber,
			// Token: 0x0400BC01 RID: 48129
			FortuneLady,
			// Token: 0x0400BC02 RID: 48130
			Dragnity,
			// Token: 0x0400BC03 RID: 48131
			FortuneWitch,
			// Token: 0x0400BC04 RID: 48132
			Synchron,
			// Token: 0x0400BC05 RID: 48133
			Saviour,
			// Token: 0x0400BC06 RID: 48134
			Reptiles,
			// Token: 0x0400BC07 RID: 48135
			Shien,
			// Token: 0x0400BC08 RID: 48136
			Junk,
			// Token: 0x0400BC09 RID: 48137
			Tomabo,
			// Token: 0x0400BC0A RID: 48138
			Sin,
			// Token: 0x0400BC0B RID: 48139
			Gem,
			// Token: 0x0400BC0C RID: 48140
			GemKnight,
			// Token: 0x0400BC0D RID: 48141
			Laval,
			// Token: 0x0400BC0E RID: 48142
			Vailon,
			// Token: 0x0400BC0F RID: 48143
			Scrap,
			// Token: 0x0400BC10 RID: 48144
			Eleki,
			// Token: 0x0400BC11 RID: 48145
			Fusion,
			// Token: 0x0400BC12 RID: 48146
			Infinity,
			// Token: 0x0400BC13 RID: 48147
			Wisel,
			// Token: 0x0400BC14 RID: 48148
			TG,
			// Token: 0x0400BC15 RID: 48149
			Karakuri,
			// Token: 0x0400BC16 RID: 48150
			Ritua,
			// Token: 0x0400BC17 RID: 48151
			Gusta,
			// Token: 0x0400BC18 RID: 48152
			Invelds,
			// Token: 0x0400BC19 RID: 48153
			Reactor,
			// Token: 0x0400BC1A RID: 48154
			Agent,
			// Token: 0x0400BC1B RID: 48155
			PoleStar,
			// Token: 0x0400BC1C RID: 48156
			PoleStarBeast,
			// Token: 0x0400BC1D RID: 48157
			PoleStarGhost,
			// Token: 0x0400BC1E RID: 48158
			PoleStarAngel,
			// Token: 0x0400BC1F RID: 48159
			PoleStarItem,
			// Token: 0x0400BC20 RID: 48160
			PoleGod,
			// Token: 0x0400BC21 RID: 48161
			SoundWarrior,
			// Token: 0x0400BC22 RID: 48162
			Resonator,
			// Token: 0x0400BC23 RID: 48163
			MHero,
			// Token: 0x0400BC24 RID: 48164
			VHero,
			// Token: 0x0400BC25 RID: 48165
			MeklordEmperor,
			// Token: 0x0400BC26 RID: 48166
			MeklordSoldier,
			// Token: 0x0400BC27 RID: 48167
			Meklord,
			// Token: 0x0400BC28 RID: 48168
			Zenmai,
			// Token: 0x0400BC29 RID: 48169
			Penguin,
			// Token: 0x0400BC2A RID: 48170
			Evold,
			// Token: 0x0400BC2B RID: 48171
			Evolder,
			// Token: 0x0400BC2C RID: 48172
			TrapHole,
			// Token: 0x0400BC2D RID: 48173
			TimeGod,
			// Token: 0x0400BC2E RID: 48174
			Sacred,
			// Token: 0x0400BC2F RID: 48175
			Velds,
			// Token: 0x0400BC30 RID: 48176
			Numbers,
			// Token: 0x0400BC31 RID: 48177
			Gagaga,
			// Token: 0x0400BC32 RID: 48178
			Gogogo,
			// Token: 0x0400BC33 RID: 48179
			Photon,
			// Token: 0x0400BC34 RID: 48180
			Ninjutsu,
			// Token: 0x0400BC35 RID: 48181
			Inzector,
			// Token: 0x0400BC36 RID: 48182
			Invasion,
			// Token: 0x0400BC37 RID: 48183
			Bouncer,
			// Token: 0x0400BC38 RID: 48184
			Butterfly,
			// Token: 0x0400BC39 RID: 48185
			HolySeal,
			// Token: 0x0400BC3A RID: 48186
			Majin,
			// Token: 0x0400BC3B RID: 48187
			Heroic,
			// Token: 0x0400BC3C RID: 48188
			Ooparts,
			// Token: 0x0400BC3D RID: 48189
			SpellBook,
			// Token: 0x0400BC3E RID: 48190
			MaDolce,
			// Token: 0x0400BC3F RID: 48191
			GearGear,
			// Token: 0x0400BC40 RID: 48192
			Xyz,
			// Token: 0x0400BC41 RID: 48193
			Poseidon,
			// Token: 0x0400BC42 RID: 48194
			Mermail,
			// Token: 0x0400BC43 RID: 48195
			Abyss,
			// Token: 0x0400BC44 RID: 48196
			Magical,
			// Token: 0x0400BC45 RID: 48197
			Nimble,
			// Token: 0x0400BC46 RID: 48198
			Duston,
			// Token: 0x0400BC47 RID: 48199
			Medallion,
			// Token: 0x0400BC48 RID: 48200
			NobleKnight,
			// Token: 0x0400BC49 RID: 48201
			FireKing,
			// Token: 0x0400BC4A RID: 48202
			Galaxy,
			// Token: 0x0400BC4B RID: 48203
			HolySword,
			// Token: 0x0400BC4C RID: 48204
			FireStar,
			// Token: 0x0400BC4D RID: 48205
			FireDance,
			// Token: 0x0400BC4E RID: 48206
			HazeBeast,
			// Token: 0x0400BC4F RID: 48207
			Haze,
			// Token: 0x0400BC50 RID: 48208
			ZexalWeapon,
			// Token: 0x0400BC51 RID: 48209
			Hope,
			// Token: 0x0400BC52 RID: 48210
			GimmickPuppet,
			// Token: 0x0400BC53 RID: 48211
			Dododo,
			// Token: 0x0400BC54 RID: 48212
			BK,
			// Token: 0x0400BC55 RID: 48213
			PhantomMek,
			// Token: 0x0400BC56 RID: 48214
			FireKingBeast,
			// Token: 0x0400BC57 RID: 48215
			ChaosNumbers,
			// Token: 0x0400BC58 RID: 48216
			ChaosXyz,
			// Token: 0x0400BC59 RID: 48217
			GearGearno,
			// Token: 0x0400BC5A RID: 48218
			SDRobo,
			// Token: 0x0400BC5B RID: 48219
			SDRobo2,
			// Token: 0x0400BC5C RID: 48220
			Umbral,
			// Token: 0x0400BC5D RID: 48221
			HolyLightning,
			// Token: 0x0400BC5E RID: 48222
			Bujin,
			// Token: 0x0400BC5F RID: 48223
			Kowakuma,
			// Token: 0x0400BC60 RID: 48224
			Hole,
			// Token: 0x0400BC61 RID: 48225
			CNo39,
			// Token: 0x0400BC62 RID: 48226
			HChallenger,
			// Token: 0x0400BC63 RID: 48227
			MaliceBolus,
			// Token: 0x0400BC64 RID: 48228
			Ghostrick,
			// Token: 0x0400BC65 RID: 48229
			Vampire,
			// Token: 0x0400BC66 RID: 48230
			Cat,
			// Token: 0x0400BC67 RID: 48231
			CyberDragon,
			// Token: 0x0400BC68 RID: 48232
			Cybernetic,
			// Token: 0x0400BC69 RID: 48233
			Shinra,
			// Token: 0x0400BC6A RID: 48234
			Necrovalley,
			// Token: 0x0400BC6B RID: 48235
			Zubaba,
			// Token: 0x0400BC6C RID: 48236
			Fishborg,
			// Token: 0x0400BC6D RID: 48237
			RUM,
			// Token: 0x0400BC6E RID: 48238
			Medallion2,
			// Token: 0x0400BC6F RID: 48239
			Artifact,
			// Token: 0x0400BC70 RID: 48240
			EvolKaiser,
			// Token: 0x0400BC71 RID: 48241
			GalaxyEyes,
			// Token: 0x0400BC72 RID: 48242
			Tachyon,
			// Token: 0x0400BC73 RID: 48243
			Over100,
			// Token: 0x0400BC74 RID: 48244
			Wizard,
			// Token: 0x0400BC75 RID: 48245
			OddEyes,
			// Token: 0x0400BC76 RID: 48246
			LegendDragon,
			// Token: 0x0400BC77 RID: 48247
			LegendKnight,
			// Token: 0x0400BC78 RID: 48248
			WingedKuriboh,
			// Token: 0x0400BC79 RID: 48249
			Stardust,
			// Token: 0x0400BC7A RID: 48250
			Sprout,
			// Token: 0x0400BC7B RID: 48251
			Artorius,
			// Token: 0x0400BC7C RID: 48252
			Lancelot,
			// Token: 0x0400BC7D RID: 48253
			SuperHeavy,
			// Token: 0x0400BC7E RID: 48254
			Genso,
			// Token: 0x0400BC7F RID: 48255
			TellarKnight,
			// Token: 0x0400BC80 RID: 48256
			Shadoll,
			// Token: 0x0400BC81 RID: 48257
			DragonStar,
			// Token: 0x0400BC82 RID: 48258
			EM,
			// Token: 0x0400BC83 RID: 48259
			Change,
			// Token: 0x0400BC84 RID: 48260
			Higan,
			// Token: 0x0400BC85 RID: 48261
			UA,
			// Token: 0x0400BC86 RID: 48262
			DD,
			// Token: 0x0400BC87 RID: 48263
			DDD,
			// Token: 0x0400BC88 RID: 48264
			Furnimal,
			// Token: 0x0400BC89 RID: 48265
			DeathToy,
			// Token: 0x0400BC8A RID: 48266
			Qliphot,
			// Token: 0x0400BC8B RID: 48267
			Bunborg,
			// Token: 0x0400BC8C RID: 48268
			Goblin,
			// Token: 0x0400BC8D RID: 48269
			Cthulhu,
			// Token: 0x0400BC8E RID: 48270
			Contract,
			// Token: 0x0400BC8F RID: 48271
			Gottoms,
			// Token: 0x0400BC90 RID: 48272
			Yosen,
			// Token: 0x0400BC91 RID: 48273
			Necroth,
			// Token: 0x0400BC92 RID: 48274
			SpiritAll,
			// Token: 0x0400BC93 RID: 48275
			SpiritTamer,
			// Token: 0x0400BC94 RID: 48276
			SpiritBeast,
			// Token: 0x0400BC95 RID: 48277
			RR,
			// Token: 0x0400BC96 RID: 48278
			Infernoid,
			// Token: 0x0400BC97 RID: 48279
			Jinzo,
			// Token: 0x0400BC98 RID: 48280
			Gaia,
			// Token: 0x0400BC99 RID: 48281
			Monarch,
			// Token: 0x0400BC9A RID: 48282
			Charmer,
			// Token: 0x0400BC9B RID: 48283
			Possessed,
			// Token: 0x0400BC9C RID: 48284
			Crystal,
			// Token: 0x0400BC9D RID: 48285
			Warrior,
			// Token: 0x0400BC9E RID: 48286
			PowerTool,
			// Token: 0x0400BC9F RID: 48287
			BMG,
			// Token: 0x0400BCA0 RID: 48288
			EdgeImp,
			// Token: 0x0400BCA1 RID: 48289
			Sephira,
			// Token: 0x0400BCA2 RID: 48290
			GensoPrincess,
			// Token: 0x0400BCA3 RID: 48291
			SpiritRider,
			// Token: 0x0400BCA4 RID: 48292
			StellarKnight,
			// Token: 0x0400BCA5 RID: 48293
			Void,
			// Token: 0x0400BCA6 RID: 48294
			Em,
			// Token: 0x0400BCA7 RID: 48295
			DragonSword,
			// Token: 0x0400BCA8 RID: 48296
			IgKnight,
			// Token: 0x0400BCA9 RID: 48297
			Aroma,
			// Token: 0x0400BCAA RID: 48298
			Empowered,
			// Token: 0x0400BCAB RID: 48299
			AetherWeapon,
			// Token: 0x0400BCAC RID: 48300
			FortunePrincess,
			// Token: 0x0400BCAD RID: 48301
			AquaActress,
			// Token: 0x0400BCAE RID: 48302
			Aquarium,
			// Token: 0x0400BCAF RID: 48303
			ChaosSoldier,
			// Token: 0x0400BCB0 RID: 48304
			Majespecter,
			// Token: 0x0400BCB1 RID: 48305
			Gradle,
			// Token: 0x0400BCB2 RID: 48306
			Kozmo,
			// Token: 0x0400BCB3 RID: 48307
			Kaiju,
			// Token: 0x0400BCB4 RID: 48308
			SR,
			// Token: 0x0400BCB5 RID: 48309
			PsyFrame,
			// Token: 0x0400BCB6 RID: 48310
			RedDemon,
			// Token: 0x0400BCB7 RID: 48311
			Burgestoma,
			// Token: 0x0400BCB8 RID: 48312
			Dante,
			// Token: 0x0400BCB9 RID: 48313
			BusterBlader,
			// Token: 0x0400BCBA RID: 48314
			BusterSword,
			// Token: 0x0400BCBB RID: 48315
			Dynamist,
			// Token: 0x0400BCBC RID: 48316
			Shiranui,
			// Token: 0x0400BCBD RID: 48317
			DragonDevil,
			// Token: 0x0400BCBE RID: 48318
			Exodia,
			// Token: 0x0400BCBF RID: 48319
			PhantomKnight,
			// Token: 0x0400BCC0 RID: 48320
			Phantom,
			// Token: 0x0400BCC1 RID: 48321
			Super,
			// Token: 0x0400BCC2 RID: 48322
			SuperQuantum,
			// Token: 0x0400BCC3 RID: 48323
			SuperMachine,
			// Token: 0x0400BCC4 RID: 48324
			BlueEyes,
			// Token: 0x0400BCC5 RID: 48325
			HopeX,
			// Token: 0x0400BCC6 RID: 48326
			Moonlight,
			// Token: 0x0400BCC7 RID: 48327
			Amorphage,
			// Token: 0x0400BCC8 RID: 48328
			ElfSwordsman,
			// Token: 0x0400BCC9 RID: 48329
			MagicianGirl,
			// Token: 0x0400BCCA RID: 48330
			BlackMagic,
			// Token: 0x0400BCCB RID: 48331
			Metalphose,
			// Token: 0x0400BCCC RID: 48332
			Tramid,
			// Token: 0x0400BCCD RID: 48333
			ABF,
			// Token: 0x0400BCCE RID: 48334
			Houkai,
			// Token: 0x0400BCCF RID: 48335
			Chaos,
			// Token: 0x0400BCD0 RID: 48336
			CyberAngel,
			// Token: 0x0400BCD1 RID: 48337
			Cypher,
			// Token: 0x0400BCD2 RID: 48338
			Cardian,
			// Token: 0x0400BCD3 RID: 48339
			SilentSword,
			// Token: 0x0400BCD4 RID: 48340
			SilentMagic,
			// Token: 0x0400BCD5 RID: 48341
			MagnetWarrior,
			// Token: 0x0400BCD6 RID: 48342
			BlackMagic2,
			// Token: 0x0400BCD7 RID: 48343
			Kuriboh,
			// Token: 0x0400BCD8 RID: 48344
			Crystron,
			// Token: 0x0400BCD9 RID: 48345
			Kagoju,
			// Token: 0x0400BCDA RID: 48346
			ApoQliphot,
			// Token: 0x0400BCDB RID: 48347
			SubTerror,
			// Token: 0x0400BCDC RID: 48348
			SubTerrorMalice,
			// Token: 0x0400BCDD RID: 48349
			Spyral,
			// Token: 0x0400BCDE RID: 48350
			SpyralGear,
			// Token: 0x0400BCDF RID: 48351
			MakaiGekidan,
			// Token: 0x0400BCE0 RID: 48352
			MakaiDaihon,
			// Token: 0x0400BCE1 RID: 48353
			FallenAngel,
			// Token: 0x0400BCE2 RID: 48354
			WW,
			// Token: 0x0400BCE3 RID: 48355
			Beast12,
			// Token: 0x0400BCE4 RID: 48356
			PendDragon,
			// Token: 0x0400BCE5 RID: 48357
			SpyralMission,
			// Token: 0x0400BCE6 RID: 48358
			Predator,
			// Token: 0x0400BCE7 RID: 48359
			PredatorPlants,
			// Token: 0x0400BCE8 RID: 48360
			SuperHeavySoul,
			// Token: 0x0400BCE9 RID: 48361
			SummonBeast,
			// Token: 0x0400BCEA RID: 48362
			XyzDragon,
			// Token: 0x0400BCEB RID: 48363
			SyncDragon,
			// Token: 0x0400BCEC RID: 48364
			FusionDragon,
			// Token: 0x0400BCED RID: 48365
			PendulumGraph,
			// Token: 0x0400BCEE RID: 48366
			SkyScraper,
			// Token: 0x0400BCEF RID: 48367
			WizardSpell,
			// Token: 0x0400BCF0 RID: 48368
			LL,
			// Token: 0x0400BCF1 RID: 48369
			HaohGate,
			// Token: 0x0400BCF2 RID: 48370
			HaohKenRyu,
			// Token: 0x0400BCF3 RID: 48371
			TrueDragon,
			// Token: 0x0400BCF4 RID: 48372
			GenOhRyu,
			// Token: 0x0400BCF5 RID: 48373
			Pendulum,
			// Token: 0x0400BCF6 RID: 48374
			Gandra,
			// Token: 0x0400BCF7 RID: 48375
			TrickStar,
			// Token: 0x0400BCF8 RID: 48376
			Gouki,
			// Token: 0x0400BCF9 RID: 48377
			Chalice,
			// Token: 0x0400BCFA RID: 48378
			Relics,
			// Token: 0x0400BCFB RID: 48379
			ClearWing,
			// Token: 0x0400BCFC RID: 48380
			StarveVenom,
			// Token: 0x0400BCFD RID: 48381
			CyberDark,
			// Token: 0x0400BCFE RID: 48382
			Bonding,
			// Token: 0x0400BCFF RID: 48383
			CodeTalker,
			// Token: 0x0400BD00 RID: 48384
			Bullet,
			// Token: 0x0400BD01 RID: 48385
			AlterGeist,
			// Token: 0x0400BD02 RID: 48386
			Crawler,
			// Token: 0x0400BD03 RID: 48387
			Metaphys,
			// Token: 0x0400BD04 RID: 48388
			VenDead,
			// Token: 0x0400BD05 RID: 48389
			FA,
			// Token: 0x0400BD06 RID: 48390
			Madan,
			// Token: 0x0400BD07 RID: 48391
			Weather,
			// Token: 0x0400BD08 RID: 48392
			Parshath,
			// Token: 0x0400BD09 RID: 48393
			ShadowSix,
			// Token: 0x0400BD0A RID: 48394
			Tindangle,
			// Token: 0x0400BD0B RID: 48395
			JackKnights,
			// Token: 0x0400BD0C RID: 48396
			MagicBeast,
			// Token: 0x0400BD0D RID: 48397
			EvolutionPill,
			// Token: 0x0400BD0E RID: 48398
			Barrel,
			// Token: 0x0400BD0F RID: 48399
			EyesSacrifice,
			// Token: 0x0400BD10 RID: 48400
			ArmedDragon,
			// Token: 0x0400BD11 RID: 48401
			GearMagic,
			// Token: 0x0400BD12 RID: 48402
			Troymare,
			// Token: 0x0400BD13 RID: 48403
			ElementSaber,
			// Token: 0x0400BD14 RID: 48404
			ElementLord,
			// Token: 0x0400BD15 RID: 48405
			KugaDan,
			// Token: 0x0400BD16 RID: 48406
			SentouKi,
			// Token: 0x0400BD17 RID: 48407
			Sentou,
			// Token: 0x0400BD18 RID: 48408
			Paradion,
			// Token: 0x0400BD19 RID: 48409
			DeviRitual,
			// Token: 0x0400BD1A RID: 48410
			BlueEyesMagic,
			// Token: 0x0400BD1B RID: 48411
			GoldenCastle,
			// Token: 0x0400BD1C RID: 48412
			CyberNet,
			// Token: 0x0400BD1D RID: 48413
			SalamanGreat,
			// Token: 0x0400BD1E RID: 48414
			DinoWrestler,
			// Token: 0x0400BD1F RID: 48415
			Orphgoal,
			// Token: 0x0400BD20 RID: 48416
			ThunderDragon,
			// Token: 0x0400BD21 RID: 48417
			ForbiddenMagic,
			// Token: 0x0400BD22 RID: 48418
			Danger,
			// Token: 0x0400BD23 RID: 48419
			PhotonGalaxy,
			// Token: 0x0400BD24 RID: 48420
			Nephthys,
			// Token: 0x0400BD25 RID: 48421
			PlanKids,
			// Token: 0x0400BD26 RID: 48422
			Mayakashi,
			// Token: 0x0400BD27 RID: 48423
			Valkyrie,
			// Token: 0x0400BD28 RID: 48424
			Youtou,
			// Token: 0x0400BD29 RID: 48425
			NeosMagic,
			// Token: 0x0400BD2A RID: 48426
			HarpieMagic,
			// Token: 0x0400BD2B RID: 48427
			MachineAngel,
			// Token: 0x0400BD2C RID: 48428
			RoseDragon,
			// Token: 0x0400BD2D RID: 48429
			Sanctuary,
			// Token: 0x0400BD2E RID: 48430
			Bushido,
			// Token: 0x0400BD2F RID: 48431
			Smile,
			// Token: 0x0400BD30 RID: 48432
			BusterMode,
			// Token: 0x0400BD31 RID: 48433
			ChronoDiver,
			// Token: 0x0400BD32 RID: 48434
			MugenKidou,
			// Token: 0x0400BD33 RID: 48435
			WitchCraft,
			// Token: 0x0400BD34 RID: 48436
			EvilEye,
			// Token: 0x0400BD35 RID: 48437
			Endymion,
			// Token: 0x0400BD36 RID: 48438
			Marincess,
			// Token: 0x0400BD37 RID: 48439
			TenI,
			// Token: 0x0400BD38 RID: 48440
			Simorgh,
			// Token: 0x0400BD39 RID: 48441
			BeeForce,
			// Token: 0x0400BD3A RID: 48442
			Message,
			// Token: 0x0400BD3B RID: 48443
			DarkFusion,
			// Token: 0x0400BD3C RID: 48444
			Destroy,
			// Token: 0x0400BD3D RID: 48445
			DestroyGod,
			// Token: 0x0400BD3E RID: 48446
			DreamMirror,
			// Token: 0x0400BD3F RID: 48447
			Zanki,
			// Token: 0x0400BD40 RID: 48448
			DragonMaid,
			// Token: 0x0400BD41 RID: 48449
			Generade,
			// Token: 0x0400BD42 RID: 48450
			Ignister,
			// Token: 0x0400BD43 RID: 48451
			Ai,
			// Token: 0x0400BD44 RID: 48452
			SenKa,
			// Token: 0x0400BD45 RID: 48453
			Megalith,
			// Token: 0x0400BD46 RID: 48454
			Oracle,
			// Token: 0x0400BD47 RID: 48455
			Onomato,
			// Token: 0x0400BD48 RID: 48456
			Future,
			// Token: 0x0400BD49 RID: 48457
			Rose,
			// Token: 0x0400BD4A RID: 48458
			Rebellion,
			// Token: 0x0400BD4B RID: 48459
			CodeBreaker,
			// Token: 0x0400BD4C RID: 48460
			Nemesis,
			// Token: 0x0400BD4D RID: 48461
			Barbaros,
			// Token: 0x0400BD4E RID: 48462
			Pirates,
			// Token: 0x0400BD4F RID: 48463
			Adamassiah,
			// Token: 0x0400BD50 RID: 48464
			Rikka,
			// Token: 0x0400BD51 RID: 48465
			Eldlich,
			// Token: 0x0400BD52 RID: 48466
			Eldlixir,
			// Token: 0x0400BD53 RID: 48467
			GoldenLand,
			// Token: 0x0400BD54 RID: 48468
			Phantasm,
			// Token: 0x0400BD55 RID: 48469
			PhantasmCard,
			// Token: 0x0400BD56 RID: 48470
			GaiaCard,
			// Token: 0x0400BD57 RID: 48471
			Dragma,
			// Token: 0x0400BD58 RID: 48472
			Melfy,
			// Token: 0x0400BD59 RID: 48473
			Potan,
			// Token: 0x0400BD5A RID: 48474
			Roland,
			// Token: 0x0400BD5B RID: 48475
			KoakimailCard,
			// Token: 0x0400BD5C RID: 48476
			RaCard,
			// Token: 0x0400BD5D RID: 48477
			MeklordGod,
			// Token: 0x0400BD5E RID: 48478
			JinzoCard,
			// Token: 0x0400BD5F RID: 48479
			FossilCard,
			// Token: 0x0400BD60 RID: 48480
			Numeron,
			// Token: 0x0400BD61 RID: 48481
			GateOfNumeron,
			// Token: 0x0400BD62 RID: 48482
			Kikai,
			// Token: 0x0400BD63 RID: 48483
			Hyoui,
			// Token: 0x0400BD64 RID: 48484
			SpiritEarth,
			// Token: 0x0400BD65 RID: 48485
			SpiritWater,
			// Token: 0x0400BD66 RID: 48486
			SpiritFire,
			// Token: 0x0400BD67 RID: 48487
			SpiritWind,
			// Token: 0x0400BD68 RID: 48488
			ToonCard,
			// Token: 0x0400BD69 RID: 48489
			TriBrigade,
			// Token: 0x0400BD6A RID: 48490
			DennoKai,
			// Token: 0x0400BD6B RID: 48491
			DennoKaiMon,
			// Token: 0x0400BD6C RID: 48492
			SouTen,
			// Token: 0x0400BD6D RID: 48493
			Magistus,
			// Token: 0x0400BD6E RID: 48494
			KissKill,
			// Token: 0x0400BD6F RID: 48495
			LeeLa,
			// Token: 0x0400BD70 RID: 48496
			LiveTwin,
			// Token: 0x0400BD71 RID: 48497
			EvilTwin,
			// Token: 0x0400BD72 RID: 48498
			Drytron,
			// Token: 0x0400BD73 RID: 48499
			Myutant,
			// Token: 0x0400BD74 RID: 48500
			Spriggans,
			// Token: 0x0400BD75 RID: 48501
			SForce,
			// Token: 0x0400BD76 RID: 48502
			WightCard,
			// Token: 0x0400BD77 RID: 48503
			Sacrifice,
			// Token: 0x0400BD78 RID: 48504
			CypherDragon,
			// Token: 0x0400BD79 RID: 48505
			SaintAvalon,
			// Token: 0x0400BD7A RID: 48506
			SaintVine,
			// Token: 0x0400BD7B RID: 48507
			HolyKnights,
			// Token: 0x0400BD7C RID: 48508
			Amazement,
			// Token: 0x0400BD7D RID: 48509
			Attraction,
			// Token: 0x0400BD7E RID: 48510
			Brand,
			// Token: 0x0400BD7F RID: 48511
			ZexalServus,
			// Token: 0x0400BD80 RID: 48512
			Zexal,
			// Token: 0x0400BD81 RID: 48513
			RDM,
			// Token: 0x0400BD82 RID: 48514
			WarCry,
			// Token: 0x0400BD83 RID: 48515
			Matereactor,
			// Token: 0x0400BD84 RID: 48516
			DollMonster,
			// Token: 0x0400BD85 RID: 48517
			BlackRoseCard,
			// Token: 0x0400BD86 RID: 48518
			Underworld,
			// Token: 0x0400BD87 RID: 48519
			DoReMiCode,
			// Token: 0x0400BD88 RID: 48520
			Bearkuty,
			// Token: 0x0400BD89 RID: 48521
			Despear,
			// Token: 0x0400BD8A RID: 48522
			ForestSpirit,
			// Token: 0x0400BD8B RID: 48523
			MagicKey,
			// Token: 0x0400BD8C RID: 48524
			StardustCard,
			// Token: 0x0400BD8D RID: 48525
			GunKan,
			// Token: 0x0400BD8E RID: 48526
			Cyber,
			// Token: 0x0400BD8F RID: 48527
			Kragen,
			// Token: 0x0400BD90 RID: 48528
			Numeronius,
			// Token: 0x0400BD91 RID: 48529
			ArcanaCard,
			// Token: 0x0400BD92 RID: 48530
			ACounter,
			// Token: 0x0400BD93 RID: 48531
			KuribohMagic,
			// Token: 0x0400BD94 RID: 48532
			NumbersCard,
			// Token: 0x0400BD95 RID: 48533
			SouKen,
			// Token: 0x0400BD96 RID: 48534
			HiSui,
			// Token: 0x0400BD97 RID: 48535
			Fuwandaries,
			// Token: 0x0400BD98 RID: 48536
			Topologic,
			// Token: 0x0400BD99 RID: 48537
			AlbazCard,
			// Token: 0x0400BD9A RID: 48538
			DHeroMagic,
			// Token: 0x0400BD9B RID: 48539
			DualMagic,
			// Token: 0x0400BD9C RID: 48540
			SaintSeed,
			// Token: 0x0400BD9D RID: 48541
			BeeTrooper,
			// Token: 0x0400BD9E RID: 48542
			Hyperion,
			// Token: 0x0400BD9F RID: 48543
			PUNK,
			// Token: 0x0400BDA0 RID: 48544
			ExoSister,
			// Token: 0x0400BDA1 RID: 48545
			BraveToken,
			// Token: 0x0400BDA2 RID: 48546
			Dinorfear,
			// Token: 0x0400BDA3 RID: 48547
			DevilLady,
			// Token: 0x0400BDA4 RID: 48548
			BlueEyesCard,
			// Token: 0x0400BDA5 RID: 48549
			Seventh,
			// Token: 0x0400BDA6 RID: 48550
			Barians,
			// Token: 0x0400BDA7 RID: 48551
			Leviathan,
			// Token: 0x0400BDA8 RID: 48552
			SeaStealth,
			// Token: 0x0400BDA9 RID: 48553
			Umi,
			// Token: 0x0400BDAA RID: 48554
			Puppet,
			// Token: 0x0400BDAB RID: 48555
			Libromancer,
			// Token: 0x0400BDAC RID: 48556
			Serions,
			// Token: 0x0400BDAD RID: 48557
			Scarecrow,
			// Token: 0x0400BDAE RID: 48558
			Barbarian,
			// Token: 0x0400BDAF RID: 48559
			Variants,
			// Token: 0x0400BDB0 RID: 48560
			Labryrinth,
			// Token: 0x0400BDB1 RID: 48561
			Welcome,
			// Token: 0x0400BDB2 RID: 48562
			Rune,
			// Token: 0x0400BDB3 RID: 48563
			EHeroMagic,
			// Token: 0x0400BDB4 RID: 48564
			Sprite,
			// Token: 0x0400BDB5 RID: 48565
			Tiaraments,
			// Token: 0x0400BDB6 RID: 48566
			HaruKeSho,
			// Token: 0x0400BDB7 RID: 48567
			Wingman,
			// Token: 0x0400BDB8 RID: 48568
			MokeyMokey,
			// Token: 0x0400BDB9 RID: 48569
			ExchangeCard,
			// Token: 0x0400BDBA RID: 48570
			AJewel,
			// Token: 0x0400BDBB RID: 48571
			DoodleBeast,
			// Token: 0x0400BDBC RID: 48572
			DoodleBook,
			// Token: 0x0400BDBD RID: 48573
			GGolem,
			// Token: 0x0400BDBE RID: 48574
			Bridge,
			// Token: 0x0400BDBF RID: 48575
			Ghotis,
			// Token: 0x0400BDC0 RID: 48576
			Beasted,
			// Token: 0x0400BDC1 RID: 48577
			Ksatrira,
			// Token: 0x0400BDC2 RID: 48578
			BFDCard,
			// Token: 0x0400BDC3 RID: 48579
			RAce,
			// Token: 0x0400BDC4 RID: 48580
			Purely,
			// Token: 0x0400BDC5 RID: 48581
			Mikanko,
			// Token: 0x0400BDC6 RID: 48582
			ChaosSync,
			// Token: 0x0400BDC7 RID: 48583
			AquaMirror,
			// Token: 0x0400BDC8 RID: 48584
			InferNobleKnight,
			// Token: 0x0400BDC9 RID: 48585
			VisasCard,
			// Token: 0x0400BDCA RID: 48586
			LabyrinthWall,
			// Token: 0x0400BDCB RID: 48587
			MashinCard,
			// Token: 0x0400BDCC RID: 48588
			GateGuardian,
			// Token: 0x0400BDCD RID: 48589
			GP,
			// Token: 0x0400BDCE RID: 48590
			FireWall,
			// Token: 0x0400BDCF RID: 48591
			ManaDoom,
			// Token: 0x0400BDD0 RID: 48592
			Nemreria,
			// Token: 0x0400BDD1 RID: 48593
			GranDoReMiCode,
			// Token: 0x0400BDD2 RID: 48594
			Favorite,
			// Token: 0x0400BDD3 RID: 48595
			VS,
			// Token: 0x0400BDD4 RID: 48596
			Nouvellez,
			// Token: 0x0400BDD5 RID: 48597
			Recipe,
			// Token: 0x0400BDD6 RID: 48598
			Visas,
			// Token: 0x0400BDD7 RID: 48599
			InferHolySword,
			// Token: 0x0400BDD8 RID: 48600
			Sync,
			// Token: 0x0400BDD9 RID: 48601
			RedDragonCard,
			// Token: 0x0400BDDA RID: 48602
			ChimeraCard,
			// Token: 0x0400BDDB RID: 48603
			CharlesCard,
			// Token: 0x0400BDDC RID: 48604
			Max
		}
	}
}
