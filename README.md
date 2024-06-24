# Ecommerce

## Description

Ecommerce is a project whose purpose is to show a technical and practical evaluation that helps to have an overview of the best of my experience as a developer

## Beginning

### Prerequisites

To run this project, you will need to install the following dependencies on your system:

1. **.NET Core SDK**: You will need the .NET Core SDK to develop .NET applications.
2. **Node.js and npm**: Node.js is a runtime environment for JavaScript and npm is a package manager for Node.js. Both are necessary to work with React.
3. **TypeScript**: TypeScript is a superset of JavaScript that adds static typing and class-based objects. You will need to install it to work with TypeScript in React.
4. **React and React-DOM**: These are the React libraries. React is the main library, while ReactDOM is used to manipulate the DOM in web applications.
5. **Visual Studio or VS Code**: You will need a development environment like Visual Studio or VS Code.
6. **NuGet Packages**: NuGet packages are code packages that you can add to your .NET projects from the NuGet registry.

### Installation

To install the project dependencies, you can use the following commands:

```bash
# Install .NET dependencies
dotnet restore

# Install Node.js dependencies
npm install
```

### Initial configuration

This project uses SQL Server as its database. To set up the database connection for development, you will need to modify the `appsettings.Development.js` file.

## Steps

1. **Define the Connection String**: In the `appsettings.Development.js` file, you will find a section named `ConnectionStrings`. Here, you need to define `DefaultConnection`.

2. **Specify the Data Source**: Replace the data source in the `DefaultConnection` with the SQL Server address you want to use.

3. **Name the Database**: The database should be named `Ecommerce`. This will be used for defining the tables.

4. **Update the Database**: Once you have set up the connection string, you can create or update the database schema using the Entity Framework Core migrations. To do this, open the Package Manager Console and run the command `update-database`.

Here is an example of what your `ConnectionStrings` section might look like:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YourSqlServerAddress;Database=Ecommerce;Trusted_Connection=True;"
}
```
Please replace ``YourSqlServerAddress`` with the address of your SQL Server.

Remember to save the ``appsettings.Development.js`` file after making these changes.

## Note
The above instructions are for a development environment. For a production environment, you would need to make similar changes to the appsettings.Production.js file (or equivalent) instead.

# Database Scripts

The following are the SQL scripts generated for the database setup and migrations:

```sql
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [IdCategory] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([IdCategory])
);
GO

CREATE TABLE [Products] (
    [IdProduct] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([IdProduct])
);
GO

CREATE TABLE [Users] (
    [IdUser] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([IdUser])
);
GO

CREATE TABLE [CategoryProducts] (
    [IdProduct] int NOT NULL,
    [IdCategory] int NOT NULL,
    CONSTRAINT [PK_CategoryProducts] PRIMARY KEY ([IdCategory], [IdProduct]),
    CONSTRAINT [FK_CategoryProducts_Categories_IdCategory] FOREIGN KEY ([IdCategory]) REFERENCES [Categories] ([IdCategory]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryProducts_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [IdOrder] int NOT NULL IDENTITY,
    [IdUser] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([IdOrder]),
    CONSTRAINT [FK_Orders_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderProducts] (
    [IdOrder] int NOT NULL,
    [IdProduct] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_OrderProducts] PRIMARY KEY ([IdProduct], [IdOrder]),
    CONSTRAINT [FK_OrderProducts_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProducts_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CategoryProducts_IdProduct] ON [CategoryProducts] ([IdProduct]);
GO

CREATE INDEX [IX_OrderProducts_IdOrder] ON [OrderProducts] ([IdOrder]);
GO

CREATE INDEX [IX_Orders_IdUser] ON [Orders] ([IdUser]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240620062336_InitialCreate', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [Quantity] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621212822_Purchase', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240623233558_UserAndRoles', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UserName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [UserName] nvarchar(50) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [Name] nvarchar(50) NOT NULL;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240623235730_AddIdentities', N'8.0.6');
GO

COMMIT;
GO
```
These scripts create the necessary tables for the application, such as ``Categories``, ``Products``, ``Users``, ``CategoryProducts``, ``Orders``, and ``OrderProducts``. They also handle the creation of indices and the insertion of migration history.

Please run these scripts in your SQL Server to set up the database for the application. Make sure to replace any placeholders with actual values where necessary.

Remember to update the connection string in the ``appsettings.Development.js`` file with the correct SQL Server address and database name.

# Usage and Execution

Once you have installed all the dependencies and set up the database, you can run the project. Here are the steps to do so:

1. **Run the Project**: You can run the project from the latest version of Visual Studio by clicking the "Start" or "Run" button.

2. **Create a User**: After the database and tables have been created, you need to create a user. To do this, go to the endpoint `/api/Users/register`.

3. **Copy the Token**: Once the user is created, a token will be generated. Copy this token.

4. **Authorize**: Go to the top section of the Swagger interface and click on "Authorize". You will see a field with the value "Values".

5. **Enter the Token**: In the "Values" field, write "Bearer ", followed by a space, and then paste the generated token. This will give you global authorization for the endpoints and allow you to test all the API endpoints.

Please note that these instructions are for a development environment. For a production environment, you would need to make similar changes to the `appsettings.Production.js` file (or equivalent) instead.


## Contributions

Contributions are welcome! Here's how you can contribute to the project:

## Contribution Process

1. **Fork the Repository**: Fork the repository to your personal GitHub account.

2. **Clone the Repository**: Clone the forked repository to your local machine.

3. **Create a Branch**: Create a new branch on your local copy of the repository. The branch name should be descriptive and reflect the changes you plan to make.

4. **Make your Changes**: Make the changes or add the features you want to your branch.

5. **Make a Commit**: Commit your changes with a descriptive message.

6. **Make a Push**: Push your changes to your GitHub repository.

7. **Create a Pull Request**: From your GitHub repository, create a pull request to the original repository.

## Code Review Guidelines

When you make a pull request, your code will be reviewed according to the following standards:

- **Code Quality**: Your code should be clean, readable, and follow coding best practices.

- Testing**: Your code must pass all existing tests and you must include new tests for any new functionality you add.

- Documentation**: Any new functionality or major changes to existing functionality must be documented.

- Compatibility**: Your code must not break any existing functionality.

Please note that your pull request may be rejected if it does not comply with these rules. If you have any questions about the contribution process or code review rules, feel free to ask.

## Credits

This project was developed by **Leonardo Fabían Hernández Peña**, a Systems Engineer graduated from Manuela Beltrán University, with 5 years of programming experience. The sources were obtained from official documentation and third-party libraries, programming forums, consultations with professionals, and more.

## License

This project is licensed under the terms of the **GNU General Public License v3.0**.

