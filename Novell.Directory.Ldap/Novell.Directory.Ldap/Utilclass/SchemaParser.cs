using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000064 RID: 100
	public class SchemaParser
	{
		// Token: 0x0600038A RID: 906 RVA: 0x000101B2 File Offset: 0x0000E3B2
		private void InitBlock()
		{
			this.usage = 0;
			this.qualifiers = new ArrayList();
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000101C6 File Offset: 0x0000E3C6
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000101CE File Offset: 0x0000E3CE
		public virtual string RawString
		{
			get
			{
				return this.rawString;
			}
			set
			{
				this.rawString = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000101D7 File Offset: 0x0000E3D7
		public virtual string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000101DF File Offset: 0x0000E3DF
		public virtual IEnumerator Qualifiers
		{
			get
			{
				return this.qualifiers.GetEnumerator();
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000101EC File Offset: 0x0000E3EC
		public virtual string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000101F4 File Offset: 0x0000E3F4
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000101FC File Offset: 0x0000E3FC
		public virtual string Syntax
		{
			get
			{
				return this.syntax;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00010204 File Offset: 0x0000E404
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001020C File Offset: 0x0000E40C
		public virtual bool Single
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00010214 File Offset: 0x0000E414
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001021C File Offset: 0x0000E41C
		public virtual string Equality
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00010224 File Offset: 0x0000E424
		public virtual string Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001022C File Offset: 0x0000E42C
		public virtual string Substring
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00010234 File Offset: 0x0000E434
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001023C File Offset: 0x0000E43C
		public virtual bool UserMod
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00010244 File Offset: 0x0000E444
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0001024C File Offset: 0x0000E44C
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00010254 File Offset: 0x0000E454
		public virtual string[] Superiors
		{
			get
			{
				return this.superiors;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001025C File Offset: 0x0000E45C
		public virtual string[] Required
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00010264 File Offset: 0x0000E464
		public virtual string[] Optional
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001026C File Offset: 0x0000E46C
		public virtual string[] Auxiliary
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010274 File Offset: 0x0000E474
		public virtual string[] Precluded
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001027C File Offset: 0x0000E47C
		public virtual string[] Applies
		{
			get
			{
				return this.applies;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010284 File Offset: 0x0000E484
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00010284 File Offset: 0x0000E484
		public virtual string ObjectClass
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001028C File Offset: 0x0000E48C
		public SchemaParser(string aString)
		{
			this.InitBlock();
			int num;
			if ((num = aString.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(aString.Substring(0, num));
				for (int i = num; i < aString.Length; i++)
				{
					stringBuilder.Append(aString[i]);
					if (aString[i] == '\\')
					{
						stringBuilder.Append('\\');
					}
				}
				this.rawString = stringBuilder.ToString();
			}
			else
			{
				this.rawString = aString;
			}
			SchemaTokenCreator schemaTokenCreator = new SchemaTokenCreator(new StringReader(this.rawString));
			schemaTokenCreator.OrdinaryCharacter(46);
			schemaTokenCreator.OrdinaryCharacters(48, 57);
			schemaTokenCreator.OrdinaryCharacter(123);
			schemaTokenCreator.OrdinaryCharacter(125);
			schemaTokenCreator.OrdinaryCharacter(95);
			schemaTokenCreator.OrdinaryCharacter(59);
			schemaTokenCreator.WordCharacters(46, 57);
			schemaTokenCreator.WordCharacters(123, 125);
			schemaTokenCreator.WordCharacters(95, 95);
			schemaTokenCreator.WordCharacters(59, 59);
			try
			{
				if (-1 != schemaTokenCreator.nextToken() && schemaTokenCreator.lastttype == 40)
				{
					if (-3 == schemaTokenCreator.nextToken())
					{
						this.id = schemaTokenCreator.StringValue;
					}
					while (-1 != schemaTokenCreator.nextToken())
					{
						if (schemaTokenCreator.lastttype == -3)
						{
							if (schemaTokenCreator.StringValue.ToUpper().Equals("NAME".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.names = new string[1];
									this.names[0] = schemaTokenCreator.StringValue;
								}
								else if (schemaTokenCreator.lastttype == 40)
								{
									ArrayList arrayList = new ArrayList();
									while (schemaTokenCreator.nextToken() == 39)
									{
										if (schemaTokenCreator.StringValue != null)
										{
											arrayList.Add(schemaTokenCreator.StringValue);
										}
									}
									if (arrayList.Count > 0)
									{
										this.names = new string[arrayList.Count];
										ArrayList arrayList2 = arrayList;
										object[] array = this.names;
										SupportClass.ArrayListSupport.ToArray(arrayList2, array);
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("DESC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.description = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SYNTAX".ToUpper()))
							{
								this.result = schemaTokenCreator.nextToken();
								if (this.result == -3 || this.result == 39)
								{
									this.syntax = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("EQUALITY".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.equality = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ORDERING".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.ordering = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUBSTR".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.substring = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("FORM".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.nameForm = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.objectClass = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUP".ToUpper()))
							{
								ArrayList arrayList3 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList3.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList3.Add(schemaTokenCreator.StringValue);
									this.superior = schemaTokenCreator.StringValue;
								}
								if (arrayList3.Count > 0)
								{
									this.superiors = new string[arrayList3.Count];
									ArrayList arrayList4 = arrayList3;
									object[] array = this.superiors;
									SupportClass.ArrayListSupport.ToArray(arrayList4, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SINGLE-VALUE".ToUpper()))
							{
								this.single = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OBSOLETE".ToUpper()))
							{
								this.obsolete = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("COLLECTIVE".ToUpper()))
							{
								this.collective = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NO-USER-MODIFICATION".ToUpper()))
							{
								this.userMod = false;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MUST".ToUpper()))
							{
								ArrayList arrayList5 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList5.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList5.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList5.Count > 0)
								{
									this.required = new string[arrayList5.Count];
									ArrayList arrayList6 = arrayList5;
									object[] array = this.required;
									SupportClass.ArrayListSupport.ToArray(arrayList6, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MAY".ToUpper()))
							{
								ArrayList arrayList7 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList7.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList7.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList7.Count > 0)
								{
									this.optional = new string[arrayList7.Count];
									ArrayList arrayList8 = arrayList7;
									object[] array = this.optional;
									SupportClass.ArrayListSupport.ToArray(arrayList8, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NOT".ToUpper()))
							{
								ArrayList arrayList9 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList9.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList9.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList9.Count > 0)
								{
									this.precluded = new string[arrayList9.Count];
									ArrayList arrayList10 = arrayList9;
									object[] array = this.precluded;
									SupportClass.ArrayListSupport.ToArray(arrayList10, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUX".ToUpper()))
							{
								ArrayList arrayList11 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList11.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList11.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList11.Count > 0)
								{
									this.auxiliary = new string[arrayList11.Count];
									ArrayList arrayList12 = arrayList11;
									object[] array = this.auxiliary;
									SupportClass.ArrayListSupport.ToArray(arrayList12, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ABSTRACT".ToUpper()))
							{
								this.type = 0;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("STRUCTURAL".ToUpper()))
							{
								this.type = 1;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUXILIARY".ToUpper()))
							{
								this.type = 2;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("USAGE".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									string text = schemaTokenCreator.StringValue;
									if (text.ToUpper().Equals("directoryOperation".ToUpper()))
									{
										this.usage = 1;
									}
									else if (text.ToUpper().Equals("distributedOperation".ToUpper()))
									{
										this.usage = 2;
									}
									else if (text.ToUpper().Equals("dSAOperation".ToUpper()))
									{
										this.usage = 3;
									}
									else if (text.ToUpper().Equals("userApplications".ToUpper()))
									{
										this.usage = 0;
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("APPLIES".ToUpper()))
							{
								ArrayList arrayList13 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList13.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList13.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList13.Count > 0)
								{
									this.applies = new string[arrayList13.Count];
									ArrayList arrayList14 = arrayList13;
									object[] array = this.applies;
									SupportClass.ArrayListSupport.ToArray(arrayList14, array);
								}
							}
							else
							{
								string text = schemaTokenCreator.StringValue;
								AttributeQualifier attributeQualifier = this.parseQualifier(schemaTokenCreator, text);
								if (attributeQualifier != null)
								{
									this.qualifiers.Add(attributeQualifier);
								}
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010C74 File Offset: 0x0000EE74
		private AttributeQualifier parseQualifier(SchemaTokenCreator st, string name)
		{
			ArrayList arrayList = new ArrayList(5);
			try
			{
				if (st.nextToken() == 39)
				{
					arrayList.Add(st.StringValue);
				}
				else if (st.lastttype == 40)
				{
					while (st.nextToken() == 39)
					{
						arrayList.Add(st.StringValue);
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			string[] array = new string[arrayList.Count];
			ArrayList arrayList2 = arrayList;
			object[] array2 = array;
			array = (string[])SupportClass.ArrayListSupport.ToArray(arrayList2, array2);
			return new AttributeQualifier(name, array);
		}

		// Token: 0x04000224 RID: 548
		internal string rawString;

		// Token: 0x04000225 RID: 549
		internal string[] names;

		// Token: 0x04000226 RID: 550
		internal string id;

		// Token: 0x04000227 RID: 551
		internal string description;

		// Token: 0x04000228 RID: 552
		internal string syntax;

		// Token: 0x04000229 RID: 553
		internal string superior;

		// Token: 0x0400022A RID: 554
		internal string nameForm;

		// Token: 0x0400022B RID: 555
		internal string objectClass;

		// Token: 0x0400022C RID: 556
		internal string[] superiors;

		// Token: 0x0400022D RID: 557
		internal string[] required;

		// Token: 0x0400022E RID: 558
		internal string[] optional;

		// Token: 0x0400022F RID: 559
		internal string[] auxiliary;

		// Token: 0x04000230 RID: 560
		internal string[] precluded;

		// Token: 0x04000231 RID: 561
		internal string[] applies;

		// Token: 0x04000232 RID: 562
		internal bool single;

		// Token: 0x04000233 RID: 563
		internal bool obsolete;

		// Token: 0x04000234 RID: 564
		internal string equality;

		// Token: 0x04000235 RID: 565
		internal string ordering;

		// Token: 0x04000236 RID: 566
		internal string substring;

		// Token: 0x04000237 RID: 567
		internal bool collective;

		// Token: 0x04000238 RID: 568
		internal bool userMod = true;

		// Token: 0x04000239 RID: 569
		internal int usage;

		// Token: 0x0400023A RID: 570
		internal int type = -1;

		// Token: 0x0400023B RID: 571
		internal int result;

		// Token: 0x0400023C RID: 572
		internal ArrayList qualifiers;
	}
}
