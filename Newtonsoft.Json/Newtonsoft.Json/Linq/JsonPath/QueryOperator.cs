using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x0200019E RID: 414
	internal enum QueryOperator
	{
		// Token: 0x040007AD RID: 1965
		None,
		// Token: 0x040007AE RID: 1966
		Equals,
		// Token: 0x040007AF RID: 1967
		NotEquals,
		// Token: 0x040007B0 RID: 1968
		Exists,
		// Token: 0x040007B1 RID: 1969
		LessThan,
		// Token: 0x040007B2 RID: 1970
		LessThanOrEquals,
		// Token: 0x040007B3 RID: 1971
		GreaterThan,
		// Token: 0x040007B4 RID: 1972
		GreaterThanOrEquals,
		// Token: 0x040007B5 RID: 1973
		And,
		// Token: 0x040007B6 RID: 1974
		Or,
		// Token: 0x040007B7 RID: 1975
		RegexEquals,
		// Token: 0x040007B8 RID: 1976
		StrictEquals,
		// Token: 0x040007B9 RID: 1977
		StrictNotEquals
	}
}
