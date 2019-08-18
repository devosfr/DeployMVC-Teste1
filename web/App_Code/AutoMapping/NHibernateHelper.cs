using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Modelos;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Repository;
using Repository.Conventions;
using System;
using System.Diagnostics;
using System.Web;
public class NHibernateHelper
{
    public static ISessionFactory SessionFactory
    {
        set
        {

            HttpContext.Current.Application["SessionFactory"] = value;

        }
        get
        {

            if (HttpContext.Current.Application["SessionFactory"] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["SessionFactory"] = CreateSessionFactory();
                HttpContext.Current.Application.UnLock();
            }
            return (ISessionFactory)HttpContext.Current.Application["SessionFactory"];
        }
    }

    public static ISession OpenSession()
    {
        Debug.WriteLine("Session Created");
        return NHibernateHelper.SessionFactory.OpenSession();

    }

    public static ISession CurrentSession
    {
        get { return (ISession)HttpContext.Current.Items["current.session"]; }
        set { HttpContext.Current.Items["current.session"] = value; }
    }

    public static ISessionFactory CreateSessionFactory()
    {
        try
        {
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;

            string connectionString = @"Database=modelo3auto;Data Source=localhost;User Id=root;Password=root;charset=utf8;";

            FluentNHibernate.Cfg.FluentConfiguration configuracaoFNH = null;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                string complemento = "1";
                connectionString = @"Database=" + Configuracoes.getSetting("BDdatabase" + complemento) + ";Data Source=" + Configuracoes.getSetting("BDdatasource" + complemento) + ";User Id=" + Configuracoes.getSetting("BDuser" + complemento) + ";Password=" + Configuracoes.getSetting("BDpassword" + complemento) + ";charset=utf8;";
                configuracaoFNH = Fluently.Configure().ExposeConfiguration(cfg => BuildSchema(cfg)).Database(
                MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql().FormatSql()).ExposeConfiguration(x =>
                {
                    x.SetInterceptor(new SqlStatementInterceptor());
                    x.SetProperty("command_timeout", "0");
                });
            }
            else
            {
                string complemento = "";
                connectionString = @"Database=" + Configuracoes.getSetting("BDdatabase" + complemento) + ";Data Source=" + Configuracoes.getSetting("BDdatasource" + complemento) + ";User Id=" + Configuracoes.getSetting("BDuser" + complemento) + ";Password=" + Configuracoes.getSetting("BDpassword" + complemento) + ";charset=utf8;";

                configuracaoFNH = Fluently.Configure().ExposeConfiguration(cfg => BuildSchema(cfg)).Database(
MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql().FormatSql()).ExposeConfiguration(x =>
{
    x.SetInterceptor(new SqlStatementInterceptor());
});


                //    configuracaoFNH = Fluently.Configure().Database(
                //MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql().FormatSql()).ExposeConfiguration(x =>
                //{
                //    x.SetInterceptor(new SqlStatementInterceptor());
                //});
                //    configuracaoFNH = Fluently.Configure().Database(
                //MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql().FormatSql()).ExposeConfiguration(x =>
                //{
                //    x.SetInterceptor(new SqlStatementInterceptor());
                //});
            }


            //        configuracaoFNH.Cache(c => c
            //.UseQueryCache()
            //.ProviderClass<HashtableCacheProvider>());
            configuracaoFNH.Mappings(val => val.AutoMappings.Add(CreateAutomappings()));

            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
            return configuracaoFNH.BuildSessionFactory();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private static void BuildSchema(Configuration config)
    {
        // delete the existing db on each run
        //if (File.Exists(DbFile))
        //    File.Delete(DbFile);

        // this NHibernate tool takes a configuration (with mapping info in)
        // and exports a database schema from it
        new SchemaUpdate(config).Execute(false, true);
    }

    static AutoPersistenceModel CreateAutomappings()
    {
        // This is the actual automapping - use AutoMap to start automapping,
        // then pick one of the static methods to specify what to map (in this case
        // all the classes in the assembly that contains Employee), and then either
        // use the Setup and Where methods to restrict that behaviour, or (preferably)
        // supply a configuration instance of your definition to control the automapper.
        return AutoMap.AssemblyOf<ModeloBase>(new AutomappingConfiguration()).Conventions.Setup(con =>
        {
            con.Add<DefaultTableNameConvention>();
            con.Add<DefaultPrimaryKeyConvention>();
            con.Add<DefaultStringLengthConvention>();
            con.Add<DefaultReferenceConvention>();
            con.Add<DefaultHasManyConvention>();
            con.Add<CustomManyToManyTableNameConvention>();
            con.Add<ManyToManyConvention>();
            con.Add<NumericalConvention>();

            }).UseOverridesFromAssemblyOf<ProdutoOverride>();
        //}).Conventions.Add(ConventionBuilder.HasMany.Always(x => x.Cascade.AllDeleteOrphan())).UseOverridesFromAssemblyOf<ProdutoOverride>();
                    
    }
}