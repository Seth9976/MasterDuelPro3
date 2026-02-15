using System;

namespace System.Xml.Schema
{
	// Token: 0x02000311 RID: 785
	internal abstract class XmlValueConverter
	{
		// Token: 0x060022E7 RID: 8935
		public abstract bool ToBoolean(long value);

		// Token: 0x060022E8 RID: 8936
		public abstract bool ToBoolean(int value);

		// Token: 0x060022E9 RID: 8937
		public abstract bool ToBoolean(double value);

		// Token: 0x060022EA RID: 8938
		public abstract bool ToBoolean(DateTime value);

		// Token: 0x060022EB RID: 8939
		public abstract bool ToBoolean(string value);

		// Token: 0x060022EC RID: 8940
		public abstract bool ToBoolean(object value);

		// Token: 0x060022ED RID: 8941
		public abstract int ToInt32(bool value);

		// Token: 0x060022EE RID: 8942
		public abstract int ToInt32(long value);

		// Token: 0x060022EF RID: 8943
		public abstract int ToInt32(double value);

		// Token: 0x060022F0 RID: 8944
		public abstract int ToInt32(DateTime value);

		// Token: 0x060022F1 RID: 8945
		public abstract int ToInt32(string value);

		// Token: 0x060022F2 RID: 8946
		public abstract int ToInt32(object value);

		// Token: 0x060022F3 RID: 8947
		public abstract long ToInt64(bool value);

		// Token: 0x060022F4 RID: 8948
		public abstract long ToInt64(int value);

		// Token: 0x060022F5 RID: 8949
		public abstract long ToInt64(double value);

		// Token: 0x060022F6 RID: 8950
		public abstract long ToInt64(DateTime value);

		// Token: 0x060022F7 RID: 8951
		public abstract long ToInt64(string value);

		// Token: 0x060022F8 RID: 8952
		public abstract long ToInt64(object value);

		// Token: 0x060022F9 RID: 8953
		public abstract decimal ToDecimal(string value);

		// Token: 0x060022FA RID: 8954
		public abstract decimal ToDecimal(object value);

		// Token: 0x060022FB RID: 8955
		public abstract double ToDouble(bool value);

		// Token: 0x060022FC RID: 8956
		public abstract double ToDouble(int value);

		// Token: 0x060022FD RID: 8957
		public abstract double ToDouble(long value);

		// Token: 0x060022FE RID: 8958
		public abstract double ToDouble(DateTime value);

		// Token: 0x060022FF RID: 8959
		public abstract double ToDouble(string value);

		// Token: 0x06002300 RID: 8960
		public abstract double ToDouble(object value);

		// Token: 0x06002301 RID: 8961
		public abstract float ToSingle(double value);

		// Token: 0x06002302 RID: 8962
		public abstract float ToSingle(string value);

		// Token: 0x06002303 RID: 8963
		public abstract float ToSingle(object value);

		// Token: 0x06002304 RID: 8964
		public abstract DateTime ToDateTime(bool value);

		// Token: 0x06002305 RID: 8965
		public abstract DateTime ToDateTime(int value);

		// Token: 0x06002306 RID: 8966
		public abstract DateTime ToDateTime(long value);

		// Token: 0x06002307 RID: 8967
		public abstract DateTime ToDateTime(double value);

		// Token: 0x06002308 RID: 8968
		public abstract DateTime ToDateTime(DateTimeOffset value);

		// Token: 0x06002309 RID: 8969
		public abstract DateTime ToDateTime(string value);

		// Token: 0x0600230A RID: 8970
		public abstract DateTime ToDateTime(object value);

		// Token: 0x0600230B RID: 8971
		public abstract DateTimeOffset ToDateTimeOffset(DateTime value);

		// Token: 0x0600230C RID: 8972
		public abstract DateTimeOffset ToDateTimeOffset(string value);

		// Token: 0x0600230D RID: 8973
		public abstract DateTimeOffset ToDateTimeOffset(object value);

		// Token: 0x0600230E RID: 8974
		public abstract string ToString(bool value);

		// Token: 0x0600230F RID: 8975
		public abstract string ToString(int value);

		// Token: 0x06002310 RID: 8976
		public abstract string ToString(long value);

		// Token: 0x06002311 RID: 8977
		public abstract string ToString(decimal value);

		// Token: 0x06002312 RID: 8978
		public abstract string ToString(float value);

		// Token: 0x06002313 RID: 8979
		public abstract string ToString(double value);

		// Token: 0x06002314 RID: 8980
		public abstract string ToString(DateTime value);

		// Token: 0x06002315 RID: 8981
		public abstract string ToString(DateTimeOffset value);

		// Token: 0x06002316 RID: 8982
		public abstract string ToString(object value);

		// Token: 0x06002317 RID: 8983
		public abstract string ToString(object value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002318 RID: 8984
		public abstract object ChangeType(bool value, Type destinationType);

		// Token: 0x06002319 RID: 8985
		public abstract object ChangeType(int value, Type destinationType);

		// Token: 0x0600231A RID: 8986
		public abstract object ChangeType(long value, Type destinationType);

		// Token: 0x0600231B RID: 8987
		public abstract object ChangeType(decimal value, Type destinationType);

		// Token: 0x0600231C RID: 8988
		public abstract object ChangeType(double value, Type destinationType);

		// Token: 0x0600231D RID: 8989
		public abstract object ChangeType(DateTime value, Type destinationType);

		// Token: 0x0600231E RID: 8990
		public abstract object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver);

		// Token: 0x0600231F RID: 8991
		public abstract object ChangeType(object value, Type destinationType);

		// Token: 0x06002320 RID: 8992
		public abstract object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver);
	}
}
