using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000505 RID: 1285
	internal sealed class ParseRecord
	{
		// Token: 0x060028AE RID: 10414 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal ParseRecord()
		{
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000A6E68 File Offset: 0x000A5068
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x0400146B RID: 5227
		internal static int parseRecordIdCount = 1;

		// Token: 0x0400146C RID: 5228
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x0400146D RID: 5229
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x0400146E RID: 5230
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x0400146F RID: 5231
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04001470 RID: 5232
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04001471 RID: 5233
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04001472 RID: 5234
		internal string PRname;

		// Token: 0x04001473 RID: 5235
		internal string PRvalue;

		// Token: 0x04001474 RID: 5236
		internal object PRvarValue;

		// Token: 0x04001475 RID: 5237
		internal string PRkeyDt;

		// Token: 0x04001476 RID: 5238
		internal Type PRdtType;

		// Token: 0x04001477 RID: 5239
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x04001478 RID: 5240
		internal bool PRisEnum;

		// Token: 0x04001479 RID: 5241
		internal long PRobjectId;

		// Token: 0x0400147A RID: 5242
		internal long PRidRef;

		// Token: 0x0400147B RID: 5243
		internal string PRarrayElementTypeString;

		// Token: 0x0400147C RID: 5244
		internal Type PRarrayElementType;

		// Token: 0x0400147D RID: 5245
		internal bool PRisArrayVariant;

		// Token: 0x0400147E RID: 5246
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x0400147F RID: 5247
		internal int PRrank;

		// Token: 0x04001480 RID: 5248
		internal int[] PRlengthA;

		// Token: 0x04001481 RID: 5249
		internal int[] PRpositionA;

		// Token: 0x04001482 RID: 5250
		internal int[] PRlowerBoundA;

		// Token: 0x04001483 RID: 5251
		internal int[] PRupperBoundA;

		// Token: 0x04001484 RID: 5252
		internal int[] PRindexMap;

		// Token: 0x04001485 RID: 5253
		internal int PRmemberIndex;

		// Token: 0x04001486 RID: 5254
		internal int PRlinearlength;

		// Token: 0x04001487 RID: 5255
		internal int[] PRrectangularMap;

		// Token: 0x04001488 RID: 5256
		internal bool PRisLowerBound;

		// Token: 0x04001489 RID: 5257
		internal long PRtopId;

		// Token: 0x0400148A RID: 5258
		internal long PRheaderId;

		// Token: 0x0400148B RID: 5259
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x0400148C RID: 5260
		internal bool PRisValueTypeFixup;

		// Token: 0x0400148D RID: 5261
		internal object PRnewObj;

		// Token: 0x0400148E RID: 5262
		internal object[] PRobjectA;

		// Token: 0x0400148F RID: 5263
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x04001490 RID: 5264
		internal bool PRisRegistered;

		// Token: 0x04001491 RID: 5265
		internal object[] PRmemberData;

		// Token: 0x04001492 RID: 5266
		internal SerializationInfo PRsi;

		// Token: 0x04001493 RID: 5267
		internal int PRnullCount;
	}
}
