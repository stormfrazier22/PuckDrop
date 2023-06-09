
# Puck Drop

Puck Drop is a subscription service to send automated texts about upcoming NHL games, that's it.

Puck Drop is inspired by NHL's existing reminder service, but without all the marketing spam. 

Puck Drop is NOT a production ready service. If you are reading this, it is because I have sent you this application to play around with. The end goal is to create a UI where users can sign up and customize their notifications. As of today, the backend functionality is built, but the frontend is still a work in progress. You will find areas of the code that need improvements as it has NOT been optimized for a production release.

In order to run and test Puck Drop, you will have to use a service like Postman in order to create/delete users and send reminder texts.

Enjoy.

## Features

- Create Users
- Delete Users
- Send automated reminder texts


## API Reference

#### Get Team Id

```https
  GET /api/team/getTeamId
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Name` | `string` | **Required**. Your team name |


#### Create User

```https
  POST /api/user/register
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Username` | `string` | **Required**. Your username |
| `Password` | `string` | **Required**. Your password |
| `PhoneNumber` | `string` | **Required**. Your phone number |
| `TeamId` | `int` | **Required**. Your team id |

#### Delete User

```https
  DELETE /api/user/deleteUser
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Username` | `string` | **Required**. Your username |
| `Password` | `string` | **Required**. Your password |

#### Send Reminder Texts

```https
  POST /api/game/sendGameInfo
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `SecretKey` | `string` | **Required**. Your API key |





## Environment Variables

To run this project locally, you will need two config files added.

Add the following variables to your appsettings.json file inside the Web API project :

`NHL_API_BASE_URL : https://statsapi.web.nhl.com/api/v1/`

  `NotificationSettings : { PHONE_NUMBER: Your phone # }`

`EmailSettings : { EmailSender: Your email address, EmailPassword: Your email password }`

`TWILIO_ACCOUNT_ID : Your Twilio account id`

`TWILIO_AUTH_TOKEN : Your Twilio auth token`

`TWILIO_PHONE_NUMBER : Your Twilio phone number`

`ConnectionStrings: { AppConnection: Data Source=NHLReminder.db }` 

`SecretKey : Your API Key`

Add the following variables to your local.settings.json file inside the Azure Functions project :

`NHLReminderSecretKey : Your API Key`

`NHLReminderBaseUrl : Your base url for the Web Api Project`



## Demo

I will use Postman for this demo.

Begin by grabbing the Team Id from the NHLReminder.db database provided. This Team Id is needed in order for the service to know which team you would like to receive texts for.

![Screenshot](https://media.graphassets.com/GpIts9InTrSN1luICpoj)

Then create a user account that will receive notifications, remember to fill in the TeamId field with the Id you got from the first request.

![Screenshot](https://media.graphassets.com/uMZti0LURmGDuJNfEj5I)

Either run the Azure Timer Trigger Function, or call the end point directly to send texts to your users.

![Screenshot](https://media.graphassets.com/1Q83vMfqRyOVMXh8cs71)


## Deployment

This service is meant to eventually be deployed to an Azure App Service. For the time being, this is just a local side project I run for myself

