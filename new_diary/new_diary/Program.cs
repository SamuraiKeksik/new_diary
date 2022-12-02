using new_diary;
using System.Text.RegularExpressions;

List<Person> users = new List<Person>
{
    new(Guid.NewGuid().ToString(), "Vasya", 18),
    new(Guid.NewGuid().ToString(), "Igor", 21),
    new(Guid.NewGuid().ToString(), "Misha", 34),
};

    

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.Run(async context =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";


    if (path == "/api/users" && request.Method == "GET")
    {
        await response.WriteAsJsonAsync(users);
    }   
    else if(Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        var id = path.Value?.Split('/')[3];
        var user = users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { Message = "None" });
        }
        else
        {
            await response.WriteAsJsonAsync(user);
        }
    }
    else if(Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        var id = path.Value?.Split('/')[3];
        var user = users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { Message = "None" });
        }
        else
        {
            users.Remove(user);
            await response.WriteAsJsonAsync(user);
        }
    }
    else if(path == "/api/users" && request.Method == "POST")
    {
        try
        {
            var newUser = await request.ReadFromJsonAsync<Person>();
            if (newUser == null)
            {
                 throw new Exception("Wrong data");
            }
            else
            {
                newUser.Id = Guid.NewGuid().ToString();
                users.Add(newUser);
            }
        }
        catch (Exception){ await response.WriteAsJsonAsync(new { Message = "Wrong Data" }); }
    }
    else if (path == "/api/users/" && request.Method == "PUT")
    {
        try
        {
            var userData = await request.ReadFromJsonAsync<Person>();
            if(userData == null)
            {
                throw new Exception("Wrong Data");
            }
            else
            {
                var user = users.FirstOrDefault(x => x.Id == userData.Id);
                if(user == null)
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { Message = "User not found" });
                }
                else
                {
                    user.Age = userData.Age;
                    user.Name = userData.Name;
                    await response.WriteAsJsonAsync(user);
                }
            }
        }
        catch (Exception)
        {
            await response.WriteAsJsonAsync(new { Message = "Wrong Data" });
        }
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});



app.Run();





