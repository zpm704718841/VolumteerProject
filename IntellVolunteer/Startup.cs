﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dtol;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using AutoMapper;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutofacSerilogIntegration;
using Microsoft.Extensions.FileProviders;
using Dto.Repository.IntellVolunteer;
using Dto.IRepository.IntellVolunteer;
using ViewModel.WeChatViewModel.MiddleModel;

namespace IntellVolunteer
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public Startup(IHostingEnvironment env)
        {
            //register config file 
            Configuration = setConfig(env, "appsettings.json");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var Service = Assembly.Load("Dto.Service");
            var IService = Assembly.Load("Dto.IService");
            var IRepository = Assembly.Load("Dto.IRepository");
            var Repository = Assembly.Load("Dto.Repository");
            var valitorAssembly = Assembly.Load("ViewModel");
            #region HttpClientFactory

            services.AddHttpClient("WeChatToken", client =>
            {
                client.BaseAddress = new Uri("https://api.weixin.qq.com/cgi-bin/token");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            });

            #endregion
            #region 配置文件
            services.AddOptions();
            services.Configure<WeChartTokenMiddles>(Configuration.GetSection("WeChatToken"));
            #endregion
            #region EFCore
            //志愿者小程序 数据库连接
            var connection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<DtolContext>(options =>
            options.UseSqlServer(connection));
            services.AddDbContext<DtolContext>
                (options =>
                {
                    //sqlServerOptions数据库提供程序级别的可选行为选择器
                    //UseQueryTrackingBehavior 为通用EF Core行为选择器
                    options.UseSqlServer(connection, sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure();
                        sqlServerOptions.CommandTimeout(60);
                    })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            //微官网数据库连接
            var WGWconnection = Configuration.GetConnectionString("WGWConnection");
            services.AddDbContext<WGWDtolContext>(options =>
            options.UseSqlServer(WGWconnection));
            services.AddDbContext<WGWDtolContext>
                (options =>
                {
                    //sqlServerOptions数据库提供程序级别的可选行为选择器
                    //UseQueryTrackingBehavior 为通用EF Core行为选择器
                    options.UseSqlServer(WGWconnection, sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure();
                        sqlServerOptions.CommandTimeout(60);
                    })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            //泰便利小程序用户中心对接 连接字符串
            var Easyconnection = Configuration.GetConnectionString("EasyConnection");
            services.AddDbContext<EasyDtolContext>(options =>
            options.UseSqlServer(Easyconnection));
            services.AddDbContext<EasyDtolContext>
                (options =>
                {
                    //sqlServerOptions数据库提供程序级别的可选行为选择器
                    //UseQueryTrackingBehavior 为通用EF Core行为选择器
                    options.UseSqlServer(Easyconnection, sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure();
                        sqlServerOptions.CommandTimeout(60);
                    })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "接口文档",
                    Description = "志愿者模块接口",
                    Contact = new Contact
                    {
                        Name = "赵平梅",
                        Email = "704718841@qq.com",
                    },
                    Version = "v1"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "IntellVolunteer.xml");
                var xmlPathModel = Path.Combine(basePath, "ViewModel.xml");
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(xmlPathModel);
            });
            #endregion

            #region AutoMapper

            services.AddAutoMapper(Service);
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var builder = new ContainerBuilder();

            //   builder


            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖


            builder.RegisterAssemblyTypes(IRepository, Repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();
            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IService, Service)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();

            //builder.RegisterLogger(autowireProperties: true);


            //20191108  直接SQL实现
            builder.Register(c => new SQLRepository(connection))
                 .As<ISQLRepository>()
                 .InstancePerLifetimeScope();


            //20191207  直接SQL实现 微官网（AI泰达）数据库
            builder.Register(d => new AISQLRepository(WGWconnection))
                 .As<IAISQLRepository>()
                 .InstancePerLifetimeScope();


            //将services填充到Autofac容器生成器中
            builder.Populate(services);
            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
        
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //开发环境 生成swagger，生成环境不生成  2020724
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //，启用中间件为生成的 JSON 文档和 Swagger UI 提供服务
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "用户管理文档 V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //允许所有的域
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "file")),
                RequestPath = "/file"
            });


            // app.UseHttpsRedirection();


            //app.UseHttpsRedirection();
            app.UseMvc();
            //ContextSeed.SeedAsync(app, loggerFactory).Wait();
        }

        public IConfiguration setConfig(IHostingEnvironment env, String config)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile(config, optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
