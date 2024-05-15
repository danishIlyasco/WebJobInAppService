
using System.Text;
using System.Threading;
new Thread(() =>
{
    Thread.CurrentThread.IsBackground = true;
    /* run your code here */

    if (!File.Exists("temp.txt"))
    {
        using (FileStream fs = System.IO.File.Create("temp.txt"))
        {
            byte[] content = new UTF8Encoding(true).GetBytes("File Created" + Environment.NewLine);

            fs.Write(content, 0, content.Length);
        }
    }


    while (true)
    {
        File.AppendAllText("temp.txt", "I am Called after sleep!" + DateTime.Now + Environment.NewLine);

        TimeSpan oneDay = TimeSpan.FromDays(1);
        Thread.Sleep(oneDay);
    }

}).Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
