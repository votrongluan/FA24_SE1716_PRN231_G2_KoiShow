# KoiShow

KoiShow is a management and scoring system for Koi fish contests, designed to help organizers, judges, and participants manage contests, register Koi fish, submit entries, and track results. The project provides both backend services and data models for contest operations, Koi registration, judging, and result calculation.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [API Overview](#api-overview)
- [Screenshots](#screenshots)
- [Authors and Acknowledgment](#authors-and-acknowledgment)
- [License](#license)
- [Project Status](#project-status)

## Features

- Manage Koi contests with flexible rules and scoring.
- Register users and Koi fish for participation.
- Handle contest registration forms and payments.
- Judge/scoring system for evaluating contestants.
- Track and display contest results.

## Getting Started

This project is primarily a backend service developed in .NET, using a layered architecture with repositories, services, and an API.

### Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/en-us/download)
- SQL Server (or other compatible database)
- Visual Studio or VS Code (recommended)
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/votrongluan/FA24_SE1716_PRN231_G2_KoiShow.git
   cd FA24_SE1716_PRN231_G2_KoiShow
   ```

2. **Set up the database**
   - Configure your connection string in `appsettings.json` or via environment variables.
   - Run database migrations if available.

3. **Build and run the API**
   ```bash
   cd KoiShow.APIService
   dotnet build
   dotnet run
   ```

4. **Swagger documentation**
   - Once running, navigate to `http://localhost:<port>/swagger` to explore the API endpoints.

## Usage

- Use the provided REST API to register users, add Koi fish, create contests, and handle judging.
- Refer to the Swagger UI for request/response formats.

### API Overview

The API provides endpoints to manage:
- Accounts (users, admins, judges)
- Koi fish and their varieties
- Contest creation, registration, and scoring
- Registration forms and payments
- Contest results and points

## Screenshots

<div style="text-align: center;">
    <img src="" style="width: 70%;"/>
    <p><em>Screenshot 1</em></p>
</div>

<hr/>
<br/>

<div style="text-align: center;">
    <img src=""  style="width: 70%;"/>
    <p><em>Screenshot 2</em></p>
</div>

<hr/>
<br/>

<div style="text-align: center;">
    <img src=""  style="width: 70%;"/>
    <p><em>Screenshot 3</em></p>
</div>

<hr/>
<br/>

## Authors and Acknowledgment

Thanks to all the contributors who have helped develop this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Project Status

**Not Actively Maintained**

Thank you for your interest in this project! Unfortunately, we regret to inform you that this project is no longer actively maintained. While contributions are always welcome, we recommend checking for forks or alternative projects for the latest features and support.

If you have any questions or need further assistance, feel free to reach out. We appreciate your understanding and support!
