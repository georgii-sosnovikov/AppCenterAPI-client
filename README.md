# AppCenterAPI-client

Implemented the simplest C# client for investigating AppCenter in general and AppCenterAPI.

This application is a console that displays data about all branches of the application from the AppCenter and creates new builds for all branches.

The following endpoints were used when making requests to API:
- GET: /v0.1/apps/{owner_name}/{app_name}/branches
- GET: /v0.1/apps/{owner_name}/{app_name}/builds/{build_id}/downloads/logs
- POST: /v0.1/apps/{owner_name}/{app_name}/branches/{branch}/builds

To use this application, you need to insert:
- the AppCenterAPI access token in `string token = "YOUR_TOKEN";`
- Owner name and app name in URI `client.BaseAddress = new Uri("https://api.appcenter.ms/v0.1/apps/OWNER_NAME/APP_NAME/");`

After starting the application, information about branches and just created builds will be received and displayed automatically.
