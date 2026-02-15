using System;
using System.Data.Common;
using System.Reflection;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000018 RID: 24
	public sealed class SqliteFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000AB8F File Offset: 0x00008D8F
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ISQLiteSchemaExtensions) || (SqliteFactory._dbProviderServicesType != null && serviceType == SqliteFactory._dbProviderServicesType))
			{
				return this.GetSQLiteProviderServicesInstance();
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		private object GetSQLiteProviderServicesInstance()
		{
			if (SqliteFactory._sqliteServices == null)
			{
				Type type = Type.GetType("Mono.Data.Sqlite.SQLiteProviderServices, Mono.Data.Sqlite.Linq, Version=2.0.38.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", false);
				if (type != null)
				{
					FieldInfo field = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
					SqliteFactory._sqliteServices = field.GetValue(null);
				}
			}
			return SqliteFactory._sqliteServices;
		}

		// Token: 0x04000081 RID: 129
		public static readonly SqliteFactory Instance = new SqliteFactory();

		// Token: 0x04000082 RID: 130
		private static Type _dbProviderServicesType = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);

		// Token: 0x04000083 RID: 131
		private static object _sqliteServices;
	}
}
