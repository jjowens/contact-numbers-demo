# contact-numbers-demo
Demo website to add contact numbers for each customer

## Overview
With this website you can do the following.

-- You can create new customers and delete them.

-- You can create new contact number and delete them. There is a validation check in place to see if the contact number already exists. If it already exists, it will reject with no warning onscreen.

-- You can also set different contact number types, Home, Work or Mobile

-- If you are deleting a record, you will be asked to confirm

Note: There are no validation checks in place to see if a customer already exists.

## Database

You can restore the database from a backup file "Database-Backup.bak".

Alternatively, you can run either script "Create Database With Schema and Data.sql" or "Create Database With Schema Only.sql". The former will create a database with new tables and insert data. The latter will do the same except it won't create any records in ContactTypes.

The file "Truncate Tables and Add New Contact Types.sql" will reset all tables and create ContactTypes. One way of clearing out old data.

The database name is "ContactsDemo". Credentials are - UserID: Contactuser, password=password100. They are not used anywhere else in other repos.

## NPM commands

All of the npm commands can be found in the package.json file at top level. Those commands are available to use.

### dev and prod

```npm run tailwinds-dev``` or ```npm run tailwinds-prod```

Running either of those commands will export a Tailwinds CSS stylesheet to the public folder. The difference between two commands is the filesize of the exported CSS file. 

```npm run tailwinds-dev``` will create a CSS file with a large file size. It will include all CSS style rules, regardless if you used some or all of them. 

```npm run tailwinds-prod``` will create a CSS file with a smaller file size, suitable for production server. It will only include CSS style rules used in the webpages or javascript. It will look for any CSS style rule names. ** NOTE: the production CSS export is a buggy at the moment. Please use the tailswinds-dev for a better experience**

## Nuget Packages

A wee heads up. You might need to install nuget packages for each project.

### ContactNumbers.Engine - Database Project
EntityFramework

### ContactNumbers.Main - Website Project
EntityFramework
Ninject
Ninject.MVC5
Ninject.Web.Common
Ninject.Web.Common.Webhost
WebActivatorEx

### ContactNumbers.Models - Models/classes for database/website
No packages required

### ContactNumbers.Services - Services Project
Automapper
EntityFramework
