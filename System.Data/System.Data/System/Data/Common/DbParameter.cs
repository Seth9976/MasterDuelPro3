using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FD RID: 253
	public abstract class DbParameter : MarshalByRefObject
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000D53 RID: 3411
		// (set) Token: 0x06000D54 RID: 3412
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract DbType DbType { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000D55 RID: 3413
		// (set) Token: 0x06000D56 RID: 3414
		[DefaultValue(ParameterDirection.Input)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract ParameterDirection Direction { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000D57 RID: 3415
		// (set) Token: 0x06000D58 RID: 3416
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000D59 RID: 3417
		// (set) Token: 0x06000D5A RID: 3418
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020B RID: 523
		// (set) Token: 0x06000D5B RID: 3419
		public abstract int Size { set; }

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000D5C RID: 3420
		// (set) Token: 0x06000D5D RID: 3421
		[DefaultValue("")]
		public abstract string SourceColumn { get; set; }

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020D RID: 525
		// (set) Token: 0x06000D5E RID: 3422
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract bool SourceColumnNullMapping { set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000453C3 File Offset: 0x000435C3
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00003FD2 File Offset: 0x000021D2
		[DefaultValue(DataRowVersion.Current)]
		public virtual DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Default;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000D61 RID: 3425
		// (set) Token: 0x06000D62 RID: 3426
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(null)]
		public abstract object Value { get; set; }
	}
}
