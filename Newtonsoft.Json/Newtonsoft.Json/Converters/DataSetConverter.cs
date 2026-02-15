using System;
using System.Data;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001AE RID: 430
	[NullableContext(1)]
	[Nullable(0)]
	public class DataSetConverter : JsonConverter
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00040738 File Offset: 0x0003E938
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DataSet dataSet = (DataSet)value;
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			DataTableConverter dataTableConverter = new DataTableConverter();
			writer.WriteStartObject();
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(dataTable.TableName) : dataTable.TableName);
				dataTableConverter.WriteJson(writer, dataTable, serializer);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000407E0 File Offset: 0x0003E9E0
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DataSet dataSet = ((objectType == typeof(DataSet)) ? new DataSet() : ((DataSet)Activator.CreateInstance(objectType)));
			DataTableConverter dataTableConverter = new DataTableConverter();
			reader.ReadAndAssert();
			while (reader.TokenType == JsonToken.PropertyName)
			{
				DataTable dataTable = dataSet.Tables[(string)reader.Value];
				bool flag = dataTable != null;
				dataTable = (DataTable)dataTableConverter.ReadJson(reader, typeof(DataTable), dataTable, serializer);
				if (!flag)
				{
					dataSet.Tables.Add(dataTable);
				}
				reader.ReadAndAssert();
			}
			return dataSet;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0004087F File Offset: 0x0003EA7F
		public override bool CanConvert(Type valueType)
		{
			return typeof(DataSet).IsAssignableFrom(valueType);
		}
	}
}
