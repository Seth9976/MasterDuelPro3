using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000178 RID: 376
	[NullableContext(1)]
	[Nullable(0)]
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x00037962 File Offset: 0x00035B62
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003796C File Offset: 0x00035B6C
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00037974 File Offset: 0x00035B74
		[NullableContext(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000153AD File Offset: 0x000135AD
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00037990 File Offset: 0x00035B90
		[NullableContext(2)]
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken jtoken = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = jtoken;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000379C5 File Offset: 0x00035BC5
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000379D1 File Offset: 0x00035BD1
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000379DD File Offset: 0x00035BDD
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
