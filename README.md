# Standup Rota Manager

Our scrum master manually maintains a page on confluence that has to be updated regularly to keep the standup rota going. I find this inefficient.
And is an opportunity to try out writing a solution in C#. There maybe several other ways to solve this, but having the ability to predict who is *the chosen one*, on a certain date, could be applied to many different problems. Probably a waste of time and a problem already solved, but hey, I'm bored.

## What is a standup rota?

In an agile team, a standup is where the team shares information about how the team has been making progress to achieve the sprint goal.

So I thought I would spend some time pondering some ways to automate it in a way that I don't have to spend too much time or money - no database required.
I am wrong at the *too much time* part. I realise I can only do this in my free time, which is not a lot of time. But at least, I will have this small interesting problem to keep me entertained.

## What is the idea?

Have a process run on a daily basis and notify an MS Teams channel before the time of the standup, who is *on call* aka - who is the scrum master of the day.

## What are the constraints

- Avoid a database if possible. Use configuration or Azure Storage Tables to store any basic information
- All you need is a list of team members and their basic details - including who's currently on call
- When someone leaves the team, remove them from the list and order the list again. In other words provide new list of team members

Calculate who is *on call* i.e. the scrum master running the standup, at any given date based on:

- The date on which the process runs - the date for which the on-call has to be determined.
- The order of the person on call the previous day i.e. currently on call

## Where do you host it?

The plan is to host this on Azure, as a time triggered azure function, written in C#, .NET Core, hosted on a [consumption plan](https://docs.microsoft.com/en-us/azure/azure-functions/consumption-plan) to start with.
Ensure that no more than one instance of the function runs at any given time as the minimum viable product assumes, there is only one team and one rota.

## What if this kicks off?

Some ideas I have are:

- Create REST API to update team members
- Expose existing capability via REST API
- Create MS Teams app that can tell you who is on call anytime by chatting with a bot
- Support custom weekends
- Support public holidays  at least for the country where the team operates