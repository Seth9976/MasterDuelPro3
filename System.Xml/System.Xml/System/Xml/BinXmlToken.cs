using System;

namespace System.Xml
{
	// Token: 0x02000011 RID: 17
	internal enum BinXmlToken
	{
		// Token: 0x04000031 RID: 49
		Error,
		// Token: 0x04000032 RID: 50
		NotImpl = -2,
		// Token: 0x04000033 RID: 51
		EOF,
		// Token: 0x04000034 RID: 52
		XmlDecl = 254,
		// Token: 0x04000035 RID: 53
		Encoding = 253,
		// Token: 0x04000036 RID: 54
		DocType = 252,
		// Token: 0x04000037 RID: 55
		System = 251,
		// Token: 0x04000038 RID: 56
		Public = 250,
		// Token: 0x04000039 RID: 57
		Subset = 249,
		// Token: 0x0400003A RID: 58
		Element = 248,
		// Token: 0x0400003B RID: 59
		EndElem = 247,
		// Token: 0x0400003C RID: 60
		Attr = 246,
		// Token: 0x0400003D RID: 61
		EndAttrs = 245,
		// Token: 0x0400003E RID: 62
		PI = 244,
		// Token: 0x0400003F RID: 63
		Comment = 243,
		// Token: 0x04000040 RID: 64
		CData = 242,
		// Token: 0x04000041 RID: 65
		EndCData = 241,
		// Token: 0x04000042 RID: 66
		Name = 240,
		// Token: 0x04000043 RID: 67
		QName = 239,
		// Token: 0x04000044 RID: 68
		XmlText = 237,
		// Token: 0x04000045 RID: 69
		Nest = 236,
		// Token: 0x04000046 RID: 70
		EndNest = 235,
		// Token: 0x04000047 RID: 71
		Extn = 234,
		// Token: 0x04000048 RID: 72
		NmFlush = 233,
		// Token: 0x04000049 RID: 73
		SQL_BIT = 6,
		// Token: 0x0400004A RID: 74
		SQL_TINYINT,
		// Token: 0x0400004B RID: 75
		SQL_SMALLINT = 1,
		// Token: 0x0400004C RID: 76
		SQL_INT,
		// Token: 0x0400004D RID: 77
		SQL_BIGINT = 8,
		// Token: 0x0400004E RID: 78
		SQL_REAL = 3,
		// Token: 0x0400004F RID: 79
		SQL_FLOAT,
		// Token: 0x04000050 RID: 80
		SQL_MONEY,
		// Token: 0x04000051 RID: 81
		SQL_SMALLMONEY = 20,
		// Token: 0x04000052 RID: 82
		SQL_DATETIME = 18,
		// Token: 0x04000053 RID: 83
		SQL_SMALLDATETIME,
		// Token: 0x04000054 RID: 84
		SQL_DECIMAL = 10,
		// Token: 0x04000055 RID: 85
		SQL_NUMERIC,
		// Token: 0x04000056 RID: 86
		SQL_UUID = 9,
		// Token: 0x04000057 RID: 87
		SQL_VARBINARY = 15,
		// Token: 0x04000058 RID: 88
		SQL_BINARY = 12,
		// Token: 0x04000059 RID: 89
		SQL_IMAGE = 23,
		// Token: 0x0400005A RID: 90
		SQL_CHAR = 13,
		// Token: 0x0400005B RID: 91
		SQL_VARCHAR = 16,
		// Token: 0x0400005C RID: 92
		SQL_TEXT = 22,
		// Token: 0x0400005D RID: 93
		SQL_NVARCHAR = 17,
		// Token: 0x0400005E RID: 94
		SQL_NCHAR = 14,
		// Token: 0x0400005F RID: 95
		SQL_NTEXT = 24,
		// Token: 0x04000060 RID: 96
		SQL_UDT = 27,
		// Token: 0x04000061 RID: 97
		XSD_KATMAI_DATE = 127,
		// Token: 0x04000062 RID: 98
		XSD_KATMAI_DATETIME = 126,
		// Token: 0x04000063 RID: 99
		XSD_KATMAI_TIME = 125,
		// Token: 0x04000064 RID: 100
		XSD_KATMAI_DATEOFFSET = 124,
		// Token: 0x04000065 RID: 101
		XSD_KATMAI_DATETIMEOFFSET = 123,
		// Token: 0x04000066 RID: 102
		XSD_KATMAI_TIMEOFFSET = 122,
		// Token: 0x04000067 RID: 103
		XSD_BOOLEAN = 134,
		// Token: 0x04000068 RID: 104
		XSD_TIME = 129,
		// Token: 0x04000069 RID: 105
		XSD_DATETIME,
		// Token: 0x0400006A RID: 106
		XSD_DATE,
		// Token: 0x0400006B RID: 107
		XSD_BINHEX,
		// Token: 0x0400006C RID: 108
		XSD_BASE64,
		// Token: 0x0400006D RID: 109
		XSD_DECIMAL = 135,
		// Token: 0x0400006E RID: 110
		XSD_BYTE,
		// Token: 0x0400006F RID: 111
		XSD_UNSIGNEDSHORT,
		// Token: 0x04000070 RID: 112
		XSD_UNSIGNEDINT,
		// Token: 0x04000071 RID: 113
		XSD_UNSIGNEDLONG,
		// Token: 0x04000072 RID: 114
		XSD_QNAME
	}
}
