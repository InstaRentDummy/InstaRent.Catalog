using InstaRent.Catalog.MongoDB;
using InstaRent.Catalog.Grpc;
//using InstaRent.Catalog.EntityFrameworkCore;
using InstaRent.Catalog.MultiTenancy;
//using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
//using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
//using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
//using Volo.Abp.EntityFrameworkCore;
//using Volo.Abp.EntityFrameworkCore.SqlServer;
//using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
//using Volo.Abp.PermissionManagement.EntityFrameworkCore;
//using Volo.Abp.Security.Claims;
//using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Uow;
//using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.VirtualFileSystem;

namespace InstaRent.Catalog;

[DependsOn(
    typeof(CatalogApplicationModule),
    //typeof(CatalogEntityFrameworkCoreModule),
    typeof(CatalogHttpApiModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    //typeof(AbpEntityFrameworkCoreSqlServerModule),
    //typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    //typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    //typeof(AbpSettingManagementEntityFrameworkCoreModule),
    //typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(CatalogMongoDbModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    )]
public class CatalogHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        //Configure<AbpDbContextOptions>(options =>
        //{
        //    options.UseSqlServer();
        //});

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CatalogDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}InstaRent.Catalog.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CatalogDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}InstaRent.Catalog.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CatalogApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}InstaRent.Catalog.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CatalogApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}InstaRent.Catalog.Application", Path.DirectorySeparatorChar)));
            });
        }

        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                {"Catalog", "Catalog API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Catalog API", Version = "v1"});
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
                options.HideAbpEndpoints();
            });

        //Configure<AbpLocalizationOptions>(options =>
        //{
        //    options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
        //    options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
        //    options.Languages.Add(new LanguageInfo("en", "en", "English"));
        //    options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
        //    options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
        //    options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
        //    options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
        //    options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
        //    options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
        //    options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
        //    options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
        //    options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
        //    options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
        //    options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
        //    options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
        //    options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        //    options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
        //    options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
        //    options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        //});

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "Catalog";
            });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Catalog:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Catalog");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Catalog-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            //Standalone MongoDB servers don't support transactions
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });
        context.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("Catalog");
        });
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<PublicBagGrpService>();
        });
    }
}
