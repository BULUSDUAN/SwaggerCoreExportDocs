using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using SwaggerExtensions.Customer;
using SwaggerExtensions.Middleware;
using SwaggerExtensions.Swagger.Version;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace SwaggerExtensions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //��汾
            //services.AddApiVersioning();
            //services.AddVersionedApiExplorer();
            /*���汾����*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Test API",
                        Version = "1",
                        TermsOfService = new Uri("http://www.baidu.com"),
                        Description = "����һ���������ԵĽӿ�",
                        License = new OpenApiLicense()
                        {
                            Name = "Zero",
                            Url = new Uri("http://www.baidu.com")
                        },
                        Contact = new OpenApiContact()
                        {
                            Email = "2287991080@qq.com",
                            Name = "Zero",
                            Url = new Uri("http://www.baidu.com")
                        }
                    });
                c.EnableAnnotations();
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwaggerExtensions.xml");
                c.IncludeXmlComments(path);
            });
            /*��汾����*/
            //services.AddSwaggerGen();
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //, IApiVersionDescriptionProvider provider
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.Test();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });
         
            
            app.UseSwaggerUI(c =>
            {


               
                #region ���汾����
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");//����ʲô�����仰�Ǳ�����Ҫ��
                #endregion
                #region ��汾����
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    c.SwaggerEndpoint($"/swagger/{description.ApiVersion.ToString()}/swagger.json", description.GroupName.ToLowerInvariant());
                    
                //}
                //foreach (var version in typeof(SwaggerVersion).GetEnumNames())
                //{
                //    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", description.GroupName.ToLowerInvariant());
                //}
                #endregion
                #region �滻HTML
                //var stream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Swagger/index.html"), FileMode.Open);//ʹ�ø÷�����ʱ�������ļ��ŵ�binĿ¼����
                ////var stream= GetType().Assembly.GetManifestResourceStream("SwaggerExtensions.Swagger.index.html");//���ص�ǰ�����е���ҳ �����ṩ������������ܹ���ȡ���ģ����Լ��������޷���ȡ���ġ�
                //if (stream!=null)
                //{
                //    c.IndexStream = () => stream;
                //}
                #endregion
                #region һЩ�����Ƿ���ʾ
                // Core
                // Display
                c.DefaultModelExpandDepth(0);
                //c.DefaultModelRendering(ModelRendering.Example);//�������ͣ�����չʾ��ʽ��Ĭ��չʾExample
                c.DefaultModelsExpandDepth(-1);//���ڿ���������չʾ��ģ�ͣ�-1��ȫ������
                //c.DisplayOperationId();//���Ʋ����б��в���ID����ʾ
                c.DisplayRequestDuration();//��������������������ʱ�䣨�Ժ���Ϊ��λ������ʾ
                c.DocExpansion(DocExpansion.List);//list չ���������µ����нӿڣ�Full�������нӿڵĵ��Խ��棬Noneȫ������,�����õ��������list
                c.EnableDeepLinking();//Ϊ��ǺͲ��������������
                c.EnableFilter();//expression�������������������Ĭ����ʾ
                c.ShowExtensions();
                // Network
                //c.EnableValidator();//������ʹ�ô˲�������swagger ui��������֤����badge�����ܣ���������Ϊnull��������֤
                //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post);//���õ�����ʽ,�����������ô���޷�����
                #endregion
                #region �Զ�����ʽ
                //// Other
                //c.DocumentTitle = "�����ҵĲ����ĵ�";
                //c.InjectStylesheet("/css/swagger-ui.css");
                //c.InjectJavascript("/ext/custom-javascript.js");
                #endregion
            });
        }
    }
}
