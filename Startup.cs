using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 在 ASP.NET Core 中，資料庫內容等服務必須向相依性插入 (DI) 容器註冊，此容器會將服務提供給控制器
            services.AddDbContext<TodoContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("TodoContext")));

            services.AddControllers();
            // 包含 AddMvcCore() 所做的動作外
            // 身分驗證服務
            // Swagger/Open API 等 API 文件動態產生功能
            // Data Annotation - 支援 Attribute 資料檢核及 IValidateObject
            // Formatter Mapping - 依 Request 需求提供不同格式(JSON/XML)內容
            // CORS - 支援跨網域整合
            // 參考https://docs.microsoft.com/zh-tw/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollers?view=aspnetcore-6.0

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 用來定義應用程式該如何回應 HTTP 請求
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            //強制使用HTTPS
            app.UseHttpsRedirection();

            // 路由會負責比對傳入的 HTTP 要求，並將這些要求分派至應用程式的可執行端點
            app.UseRouting();

            // 在允許使用者存取安全資源之前先驗證使用者
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
