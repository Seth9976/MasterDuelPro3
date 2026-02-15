using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000030 RID: 48
	internal interface IDtdParserAdapter
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000194 RID: 404
		XmlNameTable NameTable { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000195 RID: 405
		IXmlNamespaceResolver NamespaceResolver { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000196 RID: 406
		Uri BaseUri { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000197 RID: 407
		char[] ParsingBuffer { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000198 RID: 408
		int ParsingBufferLength { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000199 RID: 409
		// (set) Token: 0x0600019A RID: 410
		int CurrentPosition { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019B RID: 411
		int LineNo { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019C RID: 412
		int LineStartPosition { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019D RID: 413
		bool IsEof { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600019E RID: 414
		int EntityStackLength { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600019F RID: 415
		bool IsEntityEolNormalized { get; }

		// Token: 0x060001A0 RID: 416
		int ReadData();

		// Token: 0x060001A1 RID: 417
		void OnNewLine(int pos);

		// Token: 0x060001A2 RID: 418
		int ParseNumericCharRef(StringBuilder internalSubsetBuilder);

		// Token: 0x060001A3 RID: 419
		int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x060001A4 RID: 420
		void ParsePI(StringBuilder sb);

		// Token: 0x060001A5 RID: 421
		void ParseComment(StringBuilder sb);

		// Token: 0x060001A6 RID: 422
		bool PushEntity(IDtdEntityInfo entity, out int entityId);

		// Token: 0x060001A7 RID: 423
		bool PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId);

		// Token: 0x060001A8 RID: 424
		bool PushExternalSubset(string systemId, string publicId);

		// Token: 0x060001A9 RID: 425
		void PushInternalDtd(string baseUri, string internalDtd);

		// Token: 0x060001AA RID: 426
		void OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo);

		// Token: 0x060001AB RID: 427
		void OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo);

		// Token: 0x060001AC RID: 428
		void Throw(Exception e);
	}
}
