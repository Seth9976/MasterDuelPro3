using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000140 RID: 320
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002F92C File Offset: 0x0002DB2C
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0002F934 File Offset: 0x0002DB34
		public string Id { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0002F93D File Offset: 0x0002DB3D
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0002F945 File Offset: 0x0002DB45
		public string Title { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002F94E File Offset: 0x0002DB4E
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0002F956 File Offset: 0x0002DB56
		public bool? Required { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002F95F File Offset: 0x0002DB5F
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0002F967 File Offset: 0x0002DB67
		public bool? ReadOnly { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002F970 File Offset: 0x0002DB70
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0002F978 File Offset: 0x0002DB78
		public bool? Hidden { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002F981 File Offset: 0x0002DB81
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0002F989 File Offset: 0x0002DB89
		public bool? Transient { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002F992 File Offset: 0x0002DB92
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002F99A File Offset: 0x0002DB9A
		public string Description { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002F9A3 File Offset: 0x0002DBA3
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0002F9AB File Offset: 0x0002DBAB
		public JsonSchemaType? Type { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		public string Pattern { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002F9C5 File Offset: 0x0002DBC5
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002F9CD File Offset: 0x0002DBCD
		public int? MinimumLength { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002F9D6 File Offset: 0x0002DBD6
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		public int? MaximumLength { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002F9E7 File Offset: 0x0002DBE7
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002F9EF File Offset: 0x0002DBEF
		public double? DivisibleBy { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0002FA00 File Offset: 0x0002DC00
		public double? Minimum { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002FA09 File Offset: 0x0002DC09
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0002FA11 File Offset: 0x0002DC11
		public double? Maximum { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002FA1A File Offset: 0x0002DC1A
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002FA22 File Offset: 0x0002DC22
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002FA2B File Offset: 0x0002DC2B
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0002FA33 File Offset: 0x0002DC33
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002FA3C File Offset: 0x0002DC3C
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0002FA44 File Offset: 0x0002DC44
		public int? MinimumItems { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002FA4D File Offset: 0x0002DC4D
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0002FA55 File Offset: 0x0002DC55
		public int? MaximumItems { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002FA5E File Offset: 0x0002DC5E
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0002FA66 File Offset: 0x0002DC66
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002FA6F File Offset: 0x0002DC6F
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0002FA77 File Offset: 0x0002DC77
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002FA80 File Offset: 0x0002DC80
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0002FA88 File Offset: 0x0002DC88
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002FA91 File Offset: 0x0002DC91
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0002FA99 File Offset: 0x0002DC99
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002FAA2 File Offset: 0x0002DCA2
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002FAAA File Offset: 0x0002DCAA
		public bool UniqueItems { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002FAB3 File Offset: 0x0002DCB3
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0002FABB File Offset: 0x0002DCBB
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002FACC File Offset: 0x0002DCCC
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002FAD5 File Offset: 0x0002DCD5
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0002FADD File Offset: 0x0002DCDD
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002FAE6 File Offset: 0x0002DCE6
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0002FAEE File Offset: 0x0002DCEE
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002FAF7 File Offset: 0x0002DCF7
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002FAFF File Offset: 0x0002DCFF
		public string Requires { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002FB08 File Offset: 0x0002DD08
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002FB10 File Offset: 0x0002DD10
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002FB19 File Offset: 0x0002DD19
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002FB21 File Offset: 0x0002DD21
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002FB2A File Offset: 0x0002DD2A
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0002FB32 File Offset: 0x0002DD32
		public JToken Default { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002FB3B File Offset: 0x0002DD3B
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0002FB43 File Offset: 0x0002DD43
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002FB4C File Offset: 0x0002DD4C
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0002FB54 File Offset: 0x0002DD54
		public string Format { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002FB5D File Offset: 0x0002DD5D
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x0002FB65 File Offset: 0x0002DD65
		internal string Location { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002FB6E File Offset: 0x0002DD6E
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002FB76 File Offset: 0x0002DD76
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002FB7E File Offset: 0x0002DD7E
		internal string DeferredReference { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002FB87 File Offset: 0x0002DD87
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002FB8F File Offset: 0x0002DD8F
		internal bool ReferencesResolved { get; set; }

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002FB98 File Offset: 0x0002DD98
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002FBD1 File Offset: 0x0002DDD1
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002FBDE File Offset: 0x0002DDDE
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002FC02 File Offset: 0x0002DE02
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002FC10 File Offset: 0x0002DE10
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema jsonSchema;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				jsonSchema = JsonSchema.Read(jsonReader, resolver);
			}
			return jsonSchema;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002FC6A File Offset: 0x0002DE6A
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002FC90 File Offset: 0x0002DE90
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040005E9 RID: 1513
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
