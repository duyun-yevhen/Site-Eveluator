using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;
using AutoMapper;
using WebCrawlerWebAPI.Mappers;

namespace WebCrawlerWebAPI
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddControllers();
			services.AddEfRepository<WebCrawlerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SiteCrawlerDB")));
			services.AddScoped<SiteCrawlerWorker>();
			services.AddWebCrawlerLogicServices();
			services.AddAutoMapper(typeof (PerformanseResultMapperProfile));
			services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
								  builder =>
								  {
									  builder.AllowAnyHeader()
											 .AllowAnyMethod()
											 .AllowAnyOrigin();
								  });
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCrawlerWebAPI", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCrawlerWebAPI v1"));
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			app.UseCors(MyAllowSpecificOrigins);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
