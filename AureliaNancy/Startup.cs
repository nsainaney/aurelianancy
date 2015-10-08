namespace AureliaNancy
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.StaticFiles;
    using Nancy.Owin;

    public class Startup
    {        
        string WebRoot { get; set; }
        public Startup(IHostingEnvironment env)
        {
            WebRoot = env.WebRootPath;
        }
        public void Configure(IApplicationBuilder app)
        {   
            var options = new FileServerOptions();
            options.EnableDefaultFiles = true;
            options.FileProvider = new PhysicalFileProvider(WebRoot + "/wwwroot");
            options.StaticFileOptions.FileProvider = options.FileProvider;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] {"index.html"};
            app.UseFileServer(options);
                    
            app.Map("/api", api=>{
                api.UseOwin(x => x.UseNancy());                
            });            
        }
    }
}
