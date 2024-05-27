# Paywise

## Design Patterns

Here are the design patterns used in the `Paywise` repository along with where and how they were used:

### 1. Factory Pattern
- **ServiceFactory.cs**: This file implements a service factory to create instances of services. The `ServiceFactory` class implements the `IServiceFactory` interface, providing a generic method `CreateService<T>` to instantiate services.

### 2. Dependency Injection
- **Program.cs**: This file shows extensive use of Dependency Injection to register services such as `IUserService`, `ICategoryService`, `IExpenseService`, and `IReportService`. The services are then injected into controllers as dependencies.

### 3. Repository Pattern
- **ApplicationDbContext.cs**: This file acts as a repository providing methods to interact with MongoDB collections for `User`, `Category`, and `Expense` entities.

### 4. Adapter Pattern
- **SimpleLoggingLibraryAdapter.cs**: This file adapts the `SimpleLoggingLibrary` to the `IAppLogger` interface, allowing the application to use the logging library without modifying its code.

### 5. Singleton Pattern
- **Program.cs**: Several services such as `IMongoClient`, `ApplicationDbContext`, `SimpleLoggingLibrary`, and various application services are registered as singletons.

### 6. Strategy Pattern
- **UserService.cs**, **CategoryService.cs**, **ExpenseService.cs**, **ReportService.cs**: These service classes implement different strategies for handling user, category, expense, and report-related operations.

### 7. MVC (Model-View-Controller)
- **Controllers Folder**: Contains various controllers (`AccountController.cs`, `CategoryController.cs`, `ExpenseController.cs`, `ReportController.cs`) that manage the flow of the application, handle user inputs, and return appropriate views.
- **Views Folder**: Contains the views corresponding to the different controllers.
- **Models Folder**: Contains the models (`Category.cs`, `Expense.cs`, `Report.cs`) that represent the data structure of the application.

## Example Usage
- **Factory Pattern**: The `ServiceFactory` class is used to create instances of services in a decoupled manner, allowing for easy management and scalability of service creation.
- **Dependency Injection**: Services like `IUserService` are injected into controllers (e.g., `AccountController.cs`) to separate the concerns of service creation and business logic, promoting loose coupling and easier testing.
- **Repository Pattern**: `ApplicationDbContext` provides a centralized class to manage all data-related operations, improving code organization and maintainability.
- **Adapter Pattern**: `SimpleLoggingLibraryAdapter` allows the application to use a custom logging library while conforming to the `IAppLogger` interface, demonstrating flexibility in integrating third-party libraries.
- **Singleton Pattern**: Ensuring that only one instance of critical services like `IMongoClient` and `ApplicationDbContext` exists throughout the application lifecycle, providing a global point of access and better resource management.
- **Strategy Pattern**: Different service classes encapsulate specific behaviors and operations related to different entities (users, categories, expenses, reports), demonstrating the strategy pattern.
- **MVC Pattern**: The overall structure of the application follows the MVC pattern, with controllers handling user inputs, models representing the data, and views displaying the data to the user.

## SOLID Principles

### 1. Single Responsibility Principle (SRP)
- **Controllers**: Each controller (e.g., `AccountController.cs`, `CategoryController.cs`, `ExpenseController.cs`) has a single responsibility, managing a specific part of the application (e.g., user accounts, categories, expenses).
- **Services**: Each service (e.g., `UserService.cs`, `CategoryService.cs`, `ExpenseService.cs`) handles a single aspect of the business logic related to its respective domain.

### 2. Open/Closed Principle (OCP)
- **Services**: Services like `UserService`, `CategoryService`, and `ExpenseService` are designed to be extended with new functionality without modifying their existing code. For example, new methods can be added to `IUserService` without altering the existing interface.
- **Program.cs**: The use of Dependency Injection allows for adding new services or replacing existing ones without changing the main application code.

### 3. Liskov Substitution Principle (LSP)
- **Interfaces and Implementations**: Interfaces such as `IUserService`, `ICategoryService`, and their respective implementations (`UserService`, `CategoryService`) ensure that derived classes can be substituted for their base interfaces without affecting the correctness of the program.

### 4. Interface Segregation Principle (ISP)
- **Service Interfaces**: The application defines specific interfaces for each service (e.g., `IUserService`, `ICategoryService`, `IExpenseService`), ensuring that classes implementing these interfaces are not forced to depend on methods they do not use.

### 5. Dependency Inversion Principle (DIP)
- **Program.cs**: The application depends on abstractions rather than concrete implementations. For example, controllers depend on interfaces such as `IUserService` rather than specific implementations like `UserService`, making the system more flexible and easier to maintain.

## Further Documentation

To get a detailed overview and further documentation of the design patterns used, please review the following files:
- [AccountController.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Controllers/AccountController.cs)
- [CategoryController.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Controllers/CategoryController.cs)
- [ExpenseController.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Controllers/ExpenseController.cs)
- [ReportController.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Controllers/ReportController.cs)
- [ApplicationDbContext.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Data/ApplicationDbContext.cs)
- [CategoryService.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Services/CategoryService.cs)
- [ExpenseService.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Services/ExpenseService.cs)
- [ReportService.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Services/ReportService.cs)
- [ServiceFactory.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Services/ServiceFactory.cs)
- [UserService.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Services/UserService.cs)
- [Category.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Models/Category.cs)
- [Expense.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Models/Expense.cs)
- [Report.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Models/Report.cs)
- [Program.cs](https://github.com/GeorgeAyy/Paywise/blob/main/Program.cs)
- [MongoSettings.cs](https://github.com/GeorgeAyy/Paywise/blob/main/MongoSettings.cs)


