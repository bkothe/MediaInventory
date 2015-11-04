using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MediaInventory.Infrastructure.Framework.Linq;
using NHibernate;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public static class NHibernateExtensions
    {
        public static bool IsNotValueTypeDefault(this object value)
        {
            return value != null && value.GetType().IsValueType &&
                !Activator.CreateInstance(value.GetType()).Equals(value);
        }

        public static bool IsReferenceType(this object value)
        {
            return value != null && !value.GetType().IsValueType;
        }

        public static bool SetArrayValueByName(this object[] values, string[] nameIndex, string name, object value)
        {
            var index = Array.IndexOf(nameIndex, name);
            if (index == -1 || values[index].IsNotValueTypeDefault() || values[index].IsReferenceType()) return false;
            values[index] = value;
            return true;
        }

        public static bool SetArrayValueByName<T>(
            this object[] values,
            string[] nameIndex,
            Expression<Func<T, object>> property,
            object value)
        {
            return values.SetArrayValueByName(nameIndex, property.GetPropertyName(), value);
        }

        public static global::NHibernate.Cfg.Configuration SetInterceptors(this global::NHibernate.Cfg.Configuration configuration, params IInterceptor[] interceptors)
        {
            if (interceptors.Any()) configuration.Interceptor = new CompositeInterceptor(interceptors);
            return configuration;
        }

        public static global::NHibernate.Cfg.Configuration CommandTimeout(this global::NHibernate.Cfg.Configuration configuration, int seconds)
        {
            configuration.SetProperty("command_timeout", seconds.ToString());
            return configuration;
        }

        public static global::NHibernate.Cfg.Configuration AutoQuote(this global::NHibernate.Cfg.Configuration configuration)
        {
            configuration.SetProperty("hbm2ddl.keywords", "auto-quote");
            return configuration;
        }

        public static FluentConfiguration Sql2012Database(this FluentConfiguration configuration, string connectionString, IsolationLevel isolationLevel, bool showSql)
        {
            var persistenceConfigurer = MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).IsolationLevel(isolationLevel);
            if (showSql) persistenceConfigurer.ShowSql().FormatSql();
            return configuration.Database(persistenceConfigurer);
        }

        //public static FluentConfiguration HibernatingRhinosProfiler(this FluentConfiguration configuration, bool enabled)
        //{
        //    if (enabled) HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        //    return configuration;
        //}

        public static bool IsDebugMode(this ILazySession session)
        {
            return session.GetSession().GetSessionImplementation().Factory.Settings.SqlStatementLogger.IsDebugEnabled;
        }
    }
}
